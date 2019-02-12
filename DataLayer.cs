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
    public class DataLayer
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand com;
        SqlDataReader myReader;
        SqlDataAdapter adapter;
        DataSet ds = new DataSet();

        public string validateUser(string username, string password)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT * FROM Register WHERE Username = '" + username + "' and Password = '" + password + "' ";


                using (myReader = com.ExecuteReader())
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
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            con.Close();
            return "";
        }

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