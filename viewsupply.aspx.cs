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
        //Method that allows users to view school supply information
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //Connects to database through ConnectionString in the Web.Config file
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand()) //Represents a Transact-SQL statement or stored procedure to execute against a SQL Server database. This class cannot be inherited. 
                {
                    string sql = "SELECT Teacher, Supply1, Supply2, Supply3, Supply4, Supply5, Supply6, Supply7, Supply8 FROM Supplies"; //SQL command which allows users to view all data from Teacher table
                    if (!string.IsNullOrEmpty(txtLastName.Text.Trim())) //Lists all teacher school supply information if textbox control is empty
                    {
                        sql += " WHERE Teacher LIKE @Teacher + '%'"; //Allows users to view information by teacher name
                        cmd.Parameters.AddWithValue("@Teacher", txtLastName.Text.Trim());
                    }
                    cmd.CommandText = sql; //Gets or sets the Transact-SQL statement, table name or stored procedure to execute at the data source.
                    cmd.Connection = con; //Connects to command
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database.  
                    {
                        DataTable dt = new DataTable(); //Creates new data table
                        sda.Fill(dt); //Fills data table
                        grdSupply.DataSource = dt; //Uses data source to fill gridview control
                        grdSupply.DataBind(); //Binds data to grid view
                    }
                }
            }

        }
        
        //Method that allows users to print gridview information if pressing Print button
        protected void btnPrints_Click(object sender, EventArgs e)
        {
            btnPrints.Attributes.Add("onclick", "return printing()"); //Creates JavaScript onclick method that populates print options screen
        }
    }
}
