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
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Connects to database through ConnectionString in the Web.Config file

            string filename = Path.GetFileName(fileupPicture.PostedFile.FileName); //Gets file name from file upload control on .aspx. 
            string contentType = fileupPicture.PostedFile.ContentType; //Verifies content type
            using (Stream fs = fileupPicture.PostedFile.InputStream) //Gets the underlying HttpPostedFile object for a file that is uploaded by using the FileUpload control.
            {
                using (BinaryReader br = new BinaryReader(fs)) //Reads primitive data types as binary values in a specific encoding.
                {
                    byte[] bytes = br.ReadBytes((Int32)fs.Length); //Uses Int32 to read bytes
                    string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(constr)) 
                    {
                        string query = "INSERT into Photo(Name,ContentType,Data) VALUES(@Name, @ContentType, @Data)"; //inserts information to database after form submission
                        using (SqlCommand cmd = new SqlCommand(query)) //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database.
                        {
                            cmd.Connection = con; //Initializes command connection
                            cmd.Parameters.AddWithValue("@Name", filename); //Adds parameters with value to database based on information submitted in the form
                            cmd.Parameters.AddWithValue("@ContentType", contentType);
                            cmd.Parameters.AddWithValue("@Data", bytes);
                            con.Open(); //Opens database connection
                            cmd.ExecuteNonQuery();
                            con.Close(); //Closes connection
                        }
                    }
                }
            }


            //Inserts into Students database for rest of form information outside of file upload control
            try
            {
                con.Open(); //Opens connection
                string command = "INSERT INTO Students(FirstName,LastName,Email,Phone,DOB,Grade,Teacher) VALUES(@firstname, @lastname, @email, @phone, @dob, @grade, @teacher)";
                SqlCommand com = new SqlCommand(command, con); //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database. 
                //Adds parameters with value to database based on information submitted in the form
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
            catch (Exception ex) //posts exception if system error
            {
                lblMessage.Text = "Something went wrong. Please try again.";
                throw;

            }
            finally
            {
                con.Close(); //Closes connection
                Response.AddHeader("REFRESH", "10;URL=portal.html"); //Refreshes and redirects to portal.html within 10s.
            }
        }
       
        //Clear method when user clicks clear button
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
