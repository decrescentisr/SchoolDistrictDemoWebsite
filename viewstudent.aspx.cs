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
           
         //Allows user to download files within the gridview by clicking on "Download" linkbutton    
        protected void DownloadFile(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument); //Parses LinkButton
            byte[] bytes;
            string fileName, contentType;
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //Connects to database through ConnectionString in the Web.Config file
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand()) //Represents a Transact-SQL statement or stored procedure to execute against a SQL Server database. This class cannot be inherited.
                {
                    cmd.CommandText = "SELECT Name, Data, ContentType from Photo WHERE Id=@Id"; //SQL command line to select information and list within the gridview control
                    cmd.Parameters.AddWithValue("@Id", id); //Adds parameters with value to database based on information submitted in the form
                    cmd.Connection = con; //Connects to the command
                    con.Open(); //Opens the connection
                    using (SqlDataReader sdr = cmd.ExecuteReader()) //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database.   
                    {
                        sdr.Read(); //Reads data
                        bytes = (byte[])sdr["Data"];
                        contentType = sdr["ContentType"].ToString(); //Lists ContentType to string
                        fileName = sdr["Name"].ToString();
                    }
                    con.Close(); //Closes connection
                }
                Response.Clear(); //Clears all content output from the buffer stream.
                Response.Buffer = true; //Gets or sets a value indicating whether to buffer output and send it after the complete response is finished processing.
                Response.Charset = ""; //Gets or sets the HTTP character set of the output stream.
                Response.Cache.SetCacheability(HttpCacheability.NoCache); //Gets the caching policy (such as expiration time, privacy settings, and vary clauses) of a Web page.
                Response.ContentType = contentType; //Gets or sets the HTTP MIME type of the output stream.
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName); //Adds an HTTP header to the output stream.
                Response.BinaryWrite(bytes); //Writes a string of binary characters to the HTTP output stream.
                Response.Flush(); //Sends all currently buffered output to the client.
                Response.End(); //Sends all currently buffered output to the client, stops execution of the page, and raises the EndRequest event.

            }
        }

        //Method that allows users to search for students using first and last name strings within textbox controls  
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //Connects to database through ConnectionString in the Web.Config file 
            using (SqlConnection con = new SqlConnection(constr)) 
            {
                using (SqlCommand cmd = new SqlCommand()) //Represents a Transact-SQL statement or stored procedure to execute against a SQL Server database. This class cannot be inherited.
                {
                    string sql = "SELECT Students.FirstName, Students.LastName, Students.Email, Students.Phone, Students.DOB, Students.Grade, Students.Teacher, Photo.Id, Photo.Name from Students, Photo"; //SQL statement which selects information and lists within gridview
                    if (!string.IsNullOrEmpty(txtFirst.Text.Trim())) //If textbox controls are empty, list all data
                    {
                        sql += " WHERE LastName LIKE @LastName + '%'";
                        cmd.Parameters.AddWithValue("@LastName", txtLast.Text.Trim());

                    }
                    cmd.CommandText = sql; //Gets or sets the Transact-SQL statement, table name or stored procedure to execute at the data source.
                    cmd.Connection = con; //Connects to command
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database. 
                    {
                        DataTable dt = new DataTable(); //Uses data table
                        sda.Fill(dt); //Fills data table with data
                        grdRoster.DataSource = dt; //Uses data source for gridview control
                        grdRoster.DataBind(); //Binds data to gridview control
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
