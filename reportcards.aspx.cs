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
    public partial class reportcards : System.Web.UI.Page
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
                //If the user is not a parent, then they are redirected back to the portal
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
            try
            {
                string filename = Path.GetFileName(upReport.PostedFile.FileName); //Gets file name from file upload control
                string contentType = upReport.PostedFile.ContentType; //Verifies content type
                using (Stream fs = upReport.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs)) //Reads primitive data types as binary values in a specific encoding.
                    {
                        byte[] bytes = br.ReadBytes((Int32)fs.Length); //Uses Int32 to read bytes
                        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        using (SqlConnection connection = new SqlConnection(constr)) //Connects to database
                        {
                            con.Open(); //Opens database connection
                            //inserts information to database after form submission
                            string query = "INSERT into Cards(FirstName,LastName,Name,ContentType,Data) VALUES(@FirstName, @LastName, @Name, @ContentType, @Data)";
                            using (SqlCommand cmd = new SqlCommand(query)) //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database. 
                            {
                                cmd.Connection = con; //Connects to SqlCommand
                                
                                //Adds parameters with value to database based on information submitted in the form
                                cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);  
                                cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
                                cmd.Parameters.AddWithValue("@Name", filename);
                                cmd.Parameters.AddWithValue("@ContentType", contentType);
                                cmd.Parameters.AddWithValue("@Data", bytes);


                                cmd.ExecuteNonQuery();

                                lblMessage.Visible = true;
                            }
                        }
                    }
                }
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
    }
}
