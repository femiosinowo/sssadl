using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSADL.CMS;

public partial class admin_controls_auditLog : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        message = "";
        DataTableReader dtr ;
        string sql = "select 1=1";
        if (tableName != "")
        {

            if (tableName2 == "")
            {
                sql = "select top 10 * from " + tableName + "ChangeHistory where CHID='" + CHID + "' order by ChangeDateTime desc ";
                dtr = DataBase.dbDataTable(sql).CreateDataReader();
            }
            else
            {
                // sql = "Select b.ID from " + tableName + " a inner join " + tableName2 + " b on a.ID= b.RequestID   where a.ID= '" + CHID + "'";
                //    Response.Write(tableName2);
                SqlCommand cmd = new SqlCommand("ReturnLogsFromTwoTables");
                cmd.Parameters.AddWithValue("@tableNameA", tableName);  //char(10),>
                cmd.Parameters.AddWithValue("@tableNameB", tableName2);  //char(150),>
                cmd.Parameters.AddWithValue("@tableNameAID", CHID);  //ext,>
                cmd.Parameters.AddWithValue("@ForeignColumnName", ForeignColumnName);  //ext,>
                dtr = DataBase.executeStoreProcudure(cmd).CreateDataReader();
            }

            while (dtr.Read())
            {

                string NameofFieldChanged = dtr["NameofFieldChanged"].ToString();
                string BeforeValue = dtr["BeforeValue"].ToString();
                string AfterValue = dtr["AfterValue"].ToString();
                string ChangemadebyPIN = dtr["ChangemadebyPIN"].ToString();
                string ChangeDateTime = Convert.ToDateTime(dtr["ChangeDateTime"].ToString()).ToString("M/d/yy @ h:mm tt");
                Dictionary<string, string> userdetials = loginSSA.GetUsersDetails(ChangemadebyPIN);
                string fullname = userdetials["LastName"] + " " + userdetials["FirstName"];
                string changesdescription = "changed " + NameofFieldChanged + " from " + BeforeValue + " to " + AfterValue;
                message += "<li>" + ChangeDateTime + " change by " + fullname + " : " + changesdescription + "</li>";

            }
        }
    }


    public string ForeignColumnName
    {
        get
        {
            if (ViewState["ForeignColumnName"] == null)
                return string.Empty;
            return ViewState["ForeignColumnName"].ToString();

        }
        set
        {
            ViewState["ForeignColumnName"] = value;
        }
    }
    public string tableName
    {
        get
        {
            if (ViewState["tableName"] == null)
                return string.Empty;
            return ViewState["tableName"].ToString();

        }
        set
        {
            ViewState["tableName"] = value;
        }
    }

    public string tableName2
    {
        get
        {
            if (ViewState["tableName2"] == null)
                return string.Empty;
            return ViewState["tableName2"].ToString();

        }
        set
        {
            ViewState["tableName2"] = value;
        }
    }

    public string CHID
    {
        get
        {
            if (ViewState["CHID"] == null)
                return string.Empty;
            return ViewState["CHID"].ToString();

        }
        set
        {
            ViewState["CHID"] = value;
        }
    }

    public string message { get; set; }
}