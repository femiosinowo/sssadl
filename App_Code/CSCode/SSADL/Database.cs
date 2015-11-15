using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.ComponentModel;
using System.Collections;

/// <summary>
/// Summary description for DataBase1
/// </summary>
namespace SSADL.CMS
{
     
    public class DataBase
    {


        private static SqlConnection con = new SqlConnection();
        private static SqlCommand cmd = new SqlCommand();
        private static SqlDataAdapter adapter = new SqlDataAdapter();
        private static SqlDataReader reader;
        private static DataSet ds = new DataSet();
        private static DataTable dt = new DataTable();

        //public DataBase(string whatConnection)
        //{
            
        //    ///// TODO: Add constructor logic here


           
        //    try
        //    {
        //        string str = ConfigurationManager.ConnectionStrings[whatConnection].ConnectionString;
        //        con.ConnectionString = str;
                
        //        con.Open();
        //        cmd.Connection = con;

        //    }
        //    catch (SqlException msg) 
        //    {
        //        throw new Exception("Connection Error!!!");
        //    }


        //}

        public static SqlDataReader dbReader(string SQL, string ConnectionString = "Admin.DbConnection")
        {
            ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
            //   SqlDataReader reader = null;  
            using (SqlConnection cnn = new SqlConnection(ConnectionStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SQL;
                    cnn.Open();
                   // reader = cmd.ExecuteReader();

                     reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                }
            }
           // reader.Close();
            return reader;
        }


        public static int executeCommand(string SQL, string ConnectionString = "Admin.DbConnection")
        {
            
            int results = 0;
            ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
            using (SqlConnection cnn = new SqlConnection(ConnectionStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    cmd.Connection = cnn;


                    try
                    {
                        cmd.CommandText = SQL;
                        results = cmd.ExecuteNonQuery();
                    }
                    catch { results = 0; }
                }

            }

            return results;

        }




        public static int executeCommandWithParameters(SqlCommand cmd, string ConnectionString = "Admin.DbConnection")
        {

            int results = 0;
            ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
            using (SqlConnection cnn = new SqlConnection(ConnectionStr))
            {
                    try
                    {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    cmd.Connection = cnn;
                    

                 
                        
                        results = cmd.ExecuteNonQuery();
                    }
                    catch { results = 0; }
                    finally
                    {

                        cnn.Close();

                        cmd.Dispose();

                    }
                 

            }

            return results;
  

        }


        public static string executeCommandWithParametersReturnIDENTITY(SqlCommand cmd, string ConnectionString = "Admin.DbConnection")
        {

            string results = "0";
            ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
            using (SqlConnection cnn = new SqlConnection(ConnectionStr))
            {
                try
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    cmd.Connection = cnn;




                    cmd.ExecuteNonQuery();


                    SqlDataReader readers;
                    cmd.CommandText = "SELECT @@IDENTITY";
                    readers = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (readers.Read())
                    {
                        results = readers[0].ToString();
                        readers.Close();
                    }

                }
                catch { results = "0"; }
                finally
                {

                    cnn.Close();
                    cmd.Dispose();

                }




            }

            return results;


        }

        public static DataTable executeStoreProcudure(SqlCommand cmd, string ConnectionString = "Admin.DbConnection")
        {

            SqlDataAdapter adapterDS = new SqlDataAdapter();
            DataTable dtDs = new DataTable();

            ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
            using (SqlConnection cnn = new SqlConnection(ConnectionStr))
            {
               // try
                //{
                    cmd.CommandType = CommandType.StoredProcedure;
                    cnn.Open();
                    cmd.Connection = cnn;

                   // cmd.ExecuteNonQuery();
                    adapterDS.SelectCommand = cmd;
                    adapterDS.Fill(dtDs);
                //}
                //catch {   }
                //finally
                //{

                    cnn.Close();

                    cmd.Dispose();

               // }


            }

            return dtDs;


        }



        public static SqlDataAdapter dbAdapter(string SQL, string ConnectionString = "Admin.DbConnection")
        {
             ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
             using (SqlConnection cnn = new SqlConnection(ConnectionStr))
             {
                 using (SqlCommand cmd = new SqlCommand())
                 {
                     cmd.Connection = cnn;
                     cmd.CommandType = CommandType.Text;
                     cnn.Open();

                     cmd.CommandText = SQL;
                     adapter.SelectCommand = cmd;
                 }
             }
            return adapter;
        }

        public static string executeCommanAndReturnIDENTITY(string SQL, string ConnectionString = "Admin.DbConnection")
        {
             ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
             string results = string.Empty;
             using (SqlConnection cnn = new SqlConnection(ConnectionStr))
             {
                 using (SqlCommand cmd = new SqlCommand())
                 {
                     cmd.Connection = cnn;
                     cmd.CommandType = CommandType.Text;
                     cnn.Open();

                     cmd.CommandText = SQL;
                     cmd.ExecuteNonQuery();
                    

                     SqlDataReader readers;
                     cmd.CommandText = "SELECT @@IDENTITY";
                     readers = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                     if (readers.Read())
                     {
                         results = readers[0].ToString();
                         readers.Close();
                     }
                 }

             }
            return results;
        }


        public static string executeCommanAndReturnSCOPE_IDENTITY(string SQL, string ConnectionString = "Admin.DbConnection")
        {
            ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
            string results = string.Empty;
            using (SqlConnection cnn = new SqlConnection(ConnectionStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();

                    cmd.CommandText = SQL;
                    cmd.ExecuteNonQuery();


                    SqlDataReader readers;
                    cmd.CommandText = "SELECT SCOPE_IDENTITY () ";
                    readers = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (readers.Read())
                    {
                        results = readers[0].ToString();
                        readers.Close();
                    }
                }

            }
            return results;
        }

        public static DataSet dbDataSet(string SQL, string ConnectionString = "Admin.DbConnection")
        {


           // SqlConnection conDS = new SqlConnection();
      //   SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapterDS = new SqlDataAdapter();
        DataSet dsDs = new DataSet();
             ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
             string results = string.Empty;
             using (SqlConnection cnn = new SqlConnection(ConnectionStr))
             {
                 using (SqlCommand cmd = new SqlCommand())
                 {
                     cmd.Connection = cnn;
                     cmd.CommandType = CommandType.Text;
                     cnn.Open();

                     cmd.CommandText = SQL;
                     adapterDS.SelectCommand = cmd;
                     adapterDS.Fill(dsDs);

                 }
             }
             return dsDs;
        }


        public static DataTable sortDataTable(DataTable dt, string columnToSort, string direction)
        {

            DataView dv = dt.DefaultView;

            dv.Sort = columnToSort + " " + direction;
            return dv.ToTable();

        }

        public static DataTable dbDataTable(string SQL, string ConnectionString = "Admin.DbConnection")
        {
            SqlDataAdapter adapterDS = new SqlDataAdapter();
            DataTable dtDs = new DataTable();
            ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
            string results = string.Empty;
            using (SqlConnection cnn = new SqlConnection(ConnectionStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();

                    cmd.CommandText = SQL;
                    adapterDS.SelectCommand = cmd;
                    adapterDS.Fill(dtDs);
                }
            }
            return dtDs;
        }




        public static string returnOneValue(string SQL, string ConnectionString = "Admin.DbConnection")
        {
            string results = string.Empty;
             ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
             
             using (SqlConnection cnn = new SqlConnection(ConnectionStr))
             {
                 using (SqlCommand cmd = new SqlCommand())
                 {

                     cmd.Connection = cnn;
                     cmd.CommandType = CommandType.Text;
                     cnn.Open();

                     SqlDataReader readers;
                     cmd.CommandText = SQL;
                     readers = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                     if (readers.Read())
                     {
                         results = readers[0].ToString();
                         readers.Close();
                     }

                 }
             }
            return results;
        }

        //public string TimeStamp()
        //{
        //    string SQL = "select convert(varchar ,getdate(), 102) +''+ convert(varchar ,getdate(), 8)";
        //    cmd.CommandText = SQL;
        //    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    reader.Read();
        //    string timestamp = reader[0].ToString();
        //    timestamp = timestamp.Replace(":", "").Replace(".", "");
        //    reader.Close();
        //    return timestamp;


        //}

        public string putHypen(string value)
        {
            value = value.Replace("(", "");
            value = value.Replace(")", "");
            value = value.Replace("/", "");
            value = value.Replace(" ", "-");
            return value;
        }

        public string fixString(string value)
        {
            value = value.Replace("'", "''");
            return value;
        }


        public static DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }



        ~DataBase()
        {
            // con.Close();
            //reader.Close();
            // cmd.Dispose();
        }





        public static string ConnectionStr { get; set; }
    }

  

}