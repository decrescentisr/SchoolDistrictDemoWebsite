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
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            try
            {
                con.Open();
                string command = "INSERT INTO Register(Username,FirstName,LastName,Password,Confirm,DOB,Email,Phone,Security) VALUES(@username, @firstname, @lastname, @password, @confirm, @dob, @email, @phone,@security)";
                SqlCommand com = new SqlCommand(command, con);

                com.Parameters.AddWithValue("@username", txtUsername.Text);
                com.Parameters.AddWithValue("@firstname", txtFirst.Text);
                com.Parameters.AddWithValue("@lastname", txtLast.Text);
                com.Parameters.AddWithValue("@password", txtPassword.Text);
                com.Parameters.AddWithValue("@confirm", txtConfirm.Text);
                com.Parameters.AddWithValue("@dob", txtDOB.Text);
                com.Parameters.AddWithValue("@email", txtEmail.Text);
                com.Parameters.AddWithValue("@phone", txtPhone.Text);
                com.Parameters.AddWithValue("@security", ddlSecurity.SelectedItem.Text);

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
                Response.AddHeader("REFRESH", "10;URL=login.aspx");
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtFirst.Text = string.Empty;
            txtLast.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtDOB.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirm.Text = string.Empty;
            txtUsername.Text = string.Empty;

        }
    }
}