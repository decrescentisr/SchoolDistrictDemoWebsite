using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolDistWeb
{
    public partial class studentregister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Sets security level to Educators and Admin only
            if (Session["Security"].ToString() == "Educators" || Session["Security"].ToString() == "Admin")
            {
                //Do nothing
            }
            else if (Session["Security"].ToString() == "Parents")
            {
                //If the user is not a educator, then they are redirected back to the portal
                Server.Transfer("portal.html", true);
            }
            else
            {
                //Anyone else is redirected back to the login page
                Server.Transfer("login.aspx", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // dataLayer = new DataLayer();
            // myDict = dataLayer.getStudents(Session["FirstName"].ToString());
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            string filename = Path.GetFileName(fileupPicture.PostedFile.FileName);
            string contentType = fileupPicture.PostedFile.ContentType;
            using (Stream fs = fileupPicture.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(constr))
                    {
                        string query = "INSERT into Photo(Name,ContentType,Data) VALUES(@Name, @ContentType, @Data)";
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@Name", filename);
                            cmd.Parameters.AddWithValue("@ContentType", contentType);
                            cmd.Parameters.AddWithValue("@Data", bytes);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }



            try
            {
                con.Open();
                string command = "INSERT INTO Students(FirstName,LastName,Email,Phone,DOB,Grade,Teacher) VALUES(@firstname, @lastname, @email, @phone, @dob, @grade, @teacher)";
                SqlCommand com = new SqlCommand(command, con);

                com.Parameters.AddWithValue("@firstname", txtFirst.Text);
                com.Parameters.AddWithValue("@lastname", txtLast.Text);
                com.Parameters.AddWithValue("@email", txtEmail.Text);
                com.Parameters.AddWithValue("@phone", txtPhone.Text);
                com.Parameters.AddWithValue("@dob", txtDOB.Text);
                com.Parameters.AddWithValue("@grade", txtGrade.Text);
                com.Parameters.AddWithValue("@teacher", txtTeacher.Text);

                com.ExecuteNonQuery();

                lblMessage.Visible = true;

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Something went wrong. Please try again.";
                throw;

            }
            finally
            {
                con.Close();
                Response.AddHeader("REFRESH", "10;URL=portal.html");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtGrade.Text = string.Empty;
        }
    }
}