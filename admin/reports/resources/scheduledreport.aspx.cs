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
using System.Configuration;

public partial class admin_reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        reportID = Request.QueryString["reportid"].ToString();
        string scheduledreportid = Request.QueryString["reportid"].ToString();
        if (scheduledreportid != "0")
        {
            AuditLogUX.tableName = "ReportSchedule";
            AuditLogUX.tableName2 = "";
            AuditLogUX.ForeignColumnName = "";
            AuditLogUX.CHID = reportID;
        }

        if (!IsPostBack)
        {
            DataTableReader dtr = DataBase.dbDataTable("Select * from ReportSchedule where ReportID=" + reportID).CreateDataReader();
            if (dtr.HasRows)
            {
                dtr.Read();
                frequency.Items.FindByText(dtr["RunFrequency"].ToString().Trim()).Selected = true;
                // frequency.SelectedValue = dtr["RunFrequency"].ToString();
                ReportName.Text = dtr["ReportName"].ToString();
                emails.Text = dtr["EmailAddress"].ToString();
                startDate.Text = Convert.ToDateTime(dtr["StartDate"].ToString()).ToShortDateString();
                ReportNameData = dtr["ReportName"].ToString();
                showRemove.Visible = true;
                ScheduleReportID = dtr["ID"].ToString();
                Save.Visible = true;
            }
            else
            {
                Send.Visible = true;
                showRemove.Visible = false;
            }
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["deleteReportID"].ToString()))
                {
                    string deleteSQL = "Delete from ReportSchedule where ID=" + Request.QueryString["deleteReportID"].ToString();
                    DataBase.executeCommand(deleteSQL);
                }
            }
            catch { }
        }

    }





    public string reportID { get; set; }
    protected void Save_Click(object sender, EventArgs e)
    {
        string scheduledreportid = Request.QueryString["reportid"].ToString();
        invalidEmailsFound = "";
        string[] allEmails = emails.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string validEmails = "";
        foreach (string enteredemail in allEmails)
        {
            string vem = checkValidEmailWithCore(enteredemail);
            if (vem != "")
            {
                validEmails += vem + ",";
            }
            else
            {
                // Response.Write(enteredemail + " is invalid");
                //  break; 
                invalidEmailsFound += enteredemail + "<br/>";
            }
            //  ArrayList validEmails = new ArrayList();

        }

        if (invalidEmailsFound != "")
        {
            InvalidEmailPanel.Visible = true;
            return;
        }
        else
        {
            InvalidEmailPanel.Visible = false;
        }


        reportID = Request.QueryString["reportid"].ToString();
        string sql = "  UPDATE [dbo].[ReportSchedule]    SET [ReportName] = @ReportName ,[RunFrequency] = @RunFrequency ,[StartDate] = @StartDate ,[EmailAddress] = @EmailAddress ";
        sql += " WHERE ID=" + reportID;
        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@ReportName", ReportName.Text); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@RunFrequency", frequency.SelectedItem.Value); //varchar(max),>
        sqlcmd.Parameters.AddWithValue("@StartDate", startDate.Text); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@EmailAddress", validEmails.TrimEnd(',')); //nchar(50),>

        DataBase.executeCommandWithParameters(sqlcmd);
        AuditLogs.log_Changes(scheduledreportid, "ReportSchedule");
    }



    protected void Send_Click(object sender, EventArgs e)
    {
        reportID = Request.QueryString["reportid"].ToString();
        invalidEmailsFound = "";
        string[] allEmails = emails.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string validEmails = "";
        foreach (string enteredemail in allEmails)
        {
            string vem = checkValidEmailWithCore(enteredemail);
            if (vem != "")
            {
                validEmails += vem + ",";
            }
            else
            {
                // Response.Write(enteredemail + " is invalid");
                //  break; 
                invalidEmailsFound += enteredemail + "<br/>";
            }
            //  ArrayList validEmails = new ArrayList();

        }

        if (invalidEmailsFound != "")
        {
            InvalidEmailPanel.Visible = true;
            return;
        }
        else
        {
            InvalidEmailPanel.Visible = false;
        }

        string sql = "   INSERT INTO [dbo].[ReportSchedule]([ReportID],ReportName,[RunFrequency],[StartDate],[EmailAddress]) ";
        sql += " VALUES (@ReportID,@ReportName,@RunFrequency,@StartDate,@EmailAddress )";

        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@ReportID", reportID); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@ReportName", ReportName.Text); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@RunFrequency", frequency.Text); //varchar(max),>
        sqlcmd.Parameters.AddWithValue("@StartDate", startDate.Text); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@EmailAddress", validEmails.TrimEnd(',')); //nchar(50),>
        DataBase.executeCommandWithParameters(sqlcmd);
    }
    private string checkValidEmailWithCore(string email)
    {
        Dictionary<string, string> UsersDetails = loginSSA.GetUsersDetails("", email);
        if (UsersDetails.ContainsKey("PIN"))
        {
            return email;
        }
        else
        {
            return "";
        }

        // throw new NotImplementedException();
    }

    public string invalidEmailsFound { get; set; }

    public string ReportNameData { get; set; }

    public string ScheduleReportID { get; set; }
}