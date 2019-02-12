using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolDistWeb
{
    public partial class viewstudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Sets security level to Parent, Admins and Educators only
            if (Session["Security"].ToString() == "Parents" || Session["Security"].ToString() == "Educators" || Session["Security"].ToString() == "Admin")
            {
                //Do nothing
            }
            else if (Session["Security"].ToString() == "")
            {
                //If the user is not a parent, then they are redirected back to the portal
                Server.Transfer("portal.html", true);
            }
            else
            {
                //Anyone else is redirected back to the login page
                Server.Transfer("login.aspx", true);
            }

            /* if (!IsPostBack)
             {
                 BindGrid();
             }*/


        }
        /* private void BindGrid()
           {
               string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
               using(SqlConnection con = new SqlConnection(constr))
               {
                   using(SqlCommand cmd = new SqlCommand())
                   {
                       cmd.CommandText = "SELECT Students.FirstName, Students.LastName, Students.Email, Students.Phone, Students.DOB, Students.Grade, Students.Teacher, Photo.Id, Photo.Name from Students, Photo";
                       cmd.Connection = con;
                       con.Open();
                       grdRoster.DataSource = cmd.ExecuteReader();
                       grdRoster.DataBind();
                       con.Close();
                   }
               }
           }*/
        protected void DownloadFile(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Name, Data, ContentType from Photo WHERE Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Data"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["Name"].ToString();
                    }
                    con.Close();
                }
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT Students.FirstName, Students.LastName, Students.Email, Students.Phone, Students.DOB, Students.Grade, Students.Teacher, Photo.Id, Photo.Name from Students, Photo";
                    if (!string.IsNullOrEmpty(txtFirst.Text.Trim()))
                    {
                        sql += " WHERE LastName LIKE @LastName + '%'";
                        cmd.Parameters.AddWithValue("@LastName", txtLast.Text.Trim());

                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        grdRoster.DataSource = dt;
                        grdRoster.DataBind();
                    }
                }
            }


            /* SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE FirstName = '" + txtFirst + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
           if(dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "RunCode", "javascript:alert('Sorry No Records Found with this Keyword.....');document.location.href='viewstudent.aspx';", true);
            }
            else
            {
                grdRoster.DataSource = dt;
                grdRoster.DataBind();
            }
            conn.Open();*/

            /*if (strSearch == null || strSearch.Trim() == "")
            {
                SqlCommand cmd = new SqlCommand("SELECT * from Students", conn);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE FirstName = '" + strSearch + "'", conn);
            }*/


            /* try
             {
                 SqlParameter search = new SqlParameter();
                 search.ParameterName = "FirstName";
                 search.Value = btnSearch.Text.Trim();

                 cmd.Parameters.Add(search);
                 SqlDataReader dr = cmd.ExecuteReader();
                 DataTable dt = new DataTable();
                 dt.Load(dr);

                 grdRoster.DataSource = dt;
                 grdRoster.DataBind();


             }
             catch(Exception ex)
             {
                 Response.Write(ex.Message);
                 //lblError.Text = "Could not find student. Please re-enter name and try again.";
             }
             finally
             {
                 //Connection Object Closed
                 conn.Close();
             }*/
        }
    }
}