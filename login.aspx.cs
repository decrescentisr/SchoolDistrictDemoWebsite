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
    public partial class login : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();

        DataLayer useDatabase;

        protected void Page_Load(object sender, EventArgs e)
        {
            useDatabase = new DataLayer();
            Session["Username"] = "";
            Session["Security"] = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string SecurityLevel;
            SecurityLevel = useDatabase.validateUser(txtUsername.Text, txtPassword.Text);

            if (SecurityLevel != "")
            {
                Session["Username"] = txtUsername.Text;
                Session["Security"] = SecurityLevel;
                Server.Transfer("portal.html", true);
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine("Can Invalid User Info Login? False");
            }
        }

    }
}