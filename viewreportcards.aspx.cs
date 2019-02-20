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
    public partial class viewreportcards : System.Web.UI.Page
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
                    cmd.CommandText = "SELECT Name, Data, ContentType from Cards WHERE Id=@Id"; //SQL Command to select information to list within gridview
                    cmd.Parameters.AddWithValue("@Id", id); //Adds parameters with value to database based on information submitted in the form
                    cmd.Connection = con; //Connects to command
                    con.Open(); //Opens connection
                    using (SqlDataReader sdr = cmd.ExecuteReader()) //Provides a way of reading a forward-only stream of rows from a SQL Server database. This class cannot be inherited.
                    {
                        sdr.Read(); //Advances the SqlDataReader to the next record.
                        bytes = (byte[])sdr["Data"];
                        contentType = sdr["ContentType"].ToString(); //Sends ContentType to a string
                        fileName = sdr["Name"].ToString();
                    }
                    con.Close();
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
        
        //Method which allows user to click search button and displays results
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //Connects to database through ConnectionString in the Web.Config file
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand()) //Represents a Transact-SQL statement or stored procedure to execute against a SQL Server database. This class cannot be inherited.
                {
                    string sql = "SELECT FirstName, LastName, Id, Name from Cards"; //SQL command line to list data within gridview
                    if (!string.IsNullOrEmpty(txtFirst.Text.Trim())) //If user leaves box empty, list all records. If they do not leave empty, list records searched.
                    {
                        sql += " WHERE LastName LIKE @LastName + '%'";
                        cmd.Parameters.AddWithValue("@LastName", txtLast.Text.Trim());

                    }
                    cmd.CommandText = sql; //Gets or sets the Transact-SQL statement, table name or stored procedure to execute at the data source.
                    cmd.Connection = con; //Connects to command
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd)) //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database. This class cannot be inherited.
                    {
                        DataTable dt = new DataTable(); //Uses data table to list results
                        sda.Fill(dt); //Fills data table
                        grdCards.DataSource = dt; //Connects gridview to data source
                        grdCards.DataBind(); //Binds data to grid view

                    }

                }
            }
        }
    }
}
