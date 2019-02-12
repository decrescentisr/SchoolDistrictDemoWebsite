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
    public partial class viewsupply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Sets security level to Parent and Admin only
            if (Session["Security"].ToString() == "Parents" || Session["Security"].ToString() == "Admin")
            {
                //Do nothing
            }
            else if (Session["Security"].ToString() == "Educators")
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT Teacher, Supply1, Supply2, Supply3, Supply4, Supply5, Supply6, Supply7, Supply8 FROM Supplies";
                    if (!string.IsNullOrEmpty(txtLastName.Text.Trim()))
                    {
                        sql += " WHERE Teacher LIKE @Teacher + '%'";
                        cmd.Parameters.AddWithValue("@Teacher", txtLastName.Text.Trim());
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        grdSupply.DataSource = dt;
                        grdSupply.DataBind();
                    }
                }
            }

        }

        protected void btnPrints_Click(object sender, EventArgs e)
        {
            btnPrints.Attributes.Add("onclick", "return printing()");
        }
    }
}