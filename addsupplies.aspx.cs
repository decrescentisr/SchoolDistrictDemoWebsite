using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolDistWeb
{
    public partial class addsupplies : System.Web.UI.Page
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
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            try
            {
                con.Open();
                string command = "INSERT into Supplies(Teacher,Supply1,Supply2,Supply3,Supply4,Supply5,Supply6,Supply7,Supply8) VALUES(@teacher, @supply1, @supply2, @supply3, @supply4, @supply5, @supply6, @supply7, @supply8)";

                SqlCommand com = new SqlCommand(command, con);

                com.Parameters.AddWithValue("@teacher", txtTeacher.Text);
                com.Parameters.AddWithValue("@supply1", txtSupply1.Text);
                com.Parameters.AddWithValue("@supply2", txtSupply2.Text);
                com.Parameters.AddWithValue("@supply3", txtSupply3.Text);
                com.Parameters.AddWithValue("@supply4", txtSupply4.Text);
                com.Parameters.AddWithValue("@supply5", txtSupply5.Text);
                com.Parameters.AddWithValue("@supply6", txtSupply6.Text);
                com.Parameters.AddWithValue("@supply7", txtSupply7.Text);
                com.Parameters.AddWithValue("@supply8", txtSupply8.Text);

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
            txtTeacher.Text = string.Empty;
            txtSupply1.Text = string.Empty;
            txtSupply2.Text = string.Empty;
            txtSupply3.Text = string.Empty;
            txtSupply4.Text = string.Empty;
            txtSupply5.Text = string.Empty;
            txtSupply6.Text = string.Empty;
            txtSupply7.Text = string.Empty;
            txtSupply8.Text = string.Empty;
        }
    }
}