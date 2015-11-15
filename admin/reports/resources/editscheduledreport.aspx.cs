using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

public partial class admin_reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        AuditLogUX.tableName = "ReportSchedule";
        AuditLogUX.tableName2 = "";
        AuditLogUX.ForeignColumnName = "";
        AuditLogUX.CHID = reportID;

        
    }





    public string reportID { get; set; }
    protected void Send_Click(object sender, EventArgs e)
    {

        string[] allEmails = emails.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string validEmails = "";
        foreach (string enteredemail in allEmails)
        {
            string vem = checkValidEmailWithCore(enteredemail);
            if (vem != "")
            {
                validEmails += vem + ",";
            }
            //  ArrayList validEmails = new ArrayList();

        }



     string sql = "   INSERT INTO [dbo].[ReportSchedule]([ReportID],[RunFrequency],[StartDate],[EmailAddress]) ";
     sql += " VALUES (@ReportID,@RunFrequency,@StartDate,@EmailAddress )";

     SqlCommand sqlcmd = new SqlCommand(sql);
     sqlcmd.Parameters.AddWithValue("@ReportID", ReportName.Text); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@RunFrequency", frequency.Text); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@StartDate", startDate.Text ); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@EmailAddress", validEmails); //nchar(50),>
    }

    private string checkValidEmailWithCore(string email)
    {
      Dictionary<string, string> UsersDetails =  loginSSA.GetUsersDetails("", email);
      if (UsersDetails.ContainsKey("PIN"))
      {
          return email;
      } else {
          return "";
      } 

       // throw new NotImplementedException();
    }
}