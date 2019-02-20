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
    public class DataLayer //Data layer class that allows other classes to read from. Validates users and security. Also grabs data set information to list within grid views and data sets.
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //Connects to database through ConnectionString in the Web.Config file
        SqlConnection con;
        SqlCommand com;
        SqlDataReader myReader; //Provides a way of reading a forward-only stream of rows from a SQL Server database. This class cannot be inherited.
        SqlDataAdapter adapter; //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database. This class cannot be inherited.
        DataSet ds = new DataSet(); //Creates data set
        
        //Method that validates user for login security
        public string validateUser(string username, string password)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open(); //Opens connection
                com = new SqlCommand(); //Creates new SqlCommand
                com.Connection = con;
                com.CommandText = "SELECT * FROM Register WHERE Username = '" + username + "' and Password = '" + password + "' "; //SQL command to select username and password from Register database


                using (myReader = com.ExecuteReader()) //Sends the CommandText to the Connection and builds a SqlDataReader.
                {
                    while (myReader.Read())
                    {
                        if (myReader["Password"].ToString() == password)
                        {
                            string returnVal = myReader["Security"].ToString();
                            con.Close();
                            return returnVal;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString()); //Lists exception if user encounters an error
            }
            con.Close(); //Closes connection
            return "";
        }
        
        //Method that takes information from data set and lists within gridview (if method is called)
        public DataSet grabDataSet(string sqlString)
        {
            DataSet dataSet = new DataSet();

            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = sqlString;
                adapter = new SqlDataAdapter(com);
                adapter.Fill(dataSet);

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                com.Connection.Close();
            }
            return dataSet;
        }





    }
}
