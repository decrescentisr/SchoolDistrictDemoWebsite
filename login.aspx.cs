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
        DataSet ds = new DataSet(); //Uses system generated dataset for database information
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString); //Connects to database
        SqlDataAdapter sda = new SqlDataAdapter(); //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database.        
        SqlCommand cmd = new SqlCommand(); //Represents a Transact-SQL statement or stored procedure to execute against a SQL Server database. This class cannot be inherited.

        DataLayer useDatabase; //Initiates DataLayer class

        protected void Page_Load(object sender, EventArgs e)
        {
            useDatabase = new DataLayer(); //uses the 'useDatabase' method within the DataLayer() class for security information
            Session["Username"] = "";
            Session["Security"] = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string SecurityLevel;
            SecurityLevel = useDatabase.validateUser(txtUsername.Text, txtPassword.Text); //Uses DataLayer 'useDatabase' method to validate users

            if (SecurityLevel != "") //if else statement to allow login and allows system to read security level user registers with
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
