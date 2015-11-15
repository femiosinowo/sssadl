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
using TaskScheduler;
using System.IO;
using System.Text;

public partial class admin_reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        reportID = Request.QueryString["reportid"].ToString();
        try
        {
            string scheduledreportid = Request.QueryString["scheduledreportid"].ToString();
            if (scheduledreportid != "0")
            {
                AuditLogUX.tableName = "ReportSchedule";
                AuditLogUX.tableName2 = "";
                AuditLogUX.ForeignColumnName = "";
                AuditLogUX.CHID = scheduledreportid;
            }
        }
        catch { }
        if (!IsPostBack)
        {
            try
            {
                WhereFrom.Value = Request.UrlReferrer.ToString();
            }
            catch { }
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
                SendTime.Text = dtr["RunTime"].ToString();
                showRemove.Visible = true;
                ScheduleReportID = dtr["ID"].ToString();
                Save.Visible = true;
            }
            else
            {
                Send.Visible = true;
                showRemove.Visible = false;
            }
            
        }

    }





    public string reportID { get; set; }
    protected void Save_Click(object sender, EventArgs e)
    {
        string scheduledreportid = Request.QueryString["scheduledreportid"].ToString();
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


        reportID = Request.QueryString["reportid"].ToString();
        string sql = "  UPDATE [dbo].[ReportSchedule]    SET LastRunDate=GETDATE(), [ReportName] = @ReportName , RunTime=@RunTime ,[RunFrequency] = @RunFrequency ,[StartDate] = @StartDate ,[EmailAddress] = @EmailAddress ";
        sql += " WHERE ReportID=" + reportID;
        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@ReportName", ReportName.Text); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@RunFrequency", frequency.SelectedItem.Value); //varchar(max),>
        sqlcmd.Parameters.AddWithValue("@StartDate", startDate.Text); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@EmailAddress", validEmails.TrimEnd(',')); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@RunTime", SendTime.Text); //nchar(50),>
        
        DataBase.executeCommandWithParameters(sqlcmd);


        AuditLogs.log_Changes(scheduledreportid, "ReportSchedule");

        SchedCreatePanel.Visible = true;
        UpdSaveMessage = "Report Schedule Has Been Updated";
        //  Response.Write(sql);


        if (scheduledreportid != "0")
        {
            AuditLogUX.tableName = "ReportSchedule";
            AuditLogUX.tableName2 = "";
            AuditLogUX.ForeignColumnName = "";
            AuditLogUX.CHID = scheduledreportid;
        }

       // createTask(ReportName.Text, frequency.Text, startDate.Text, SendTime.Text, reportID);

        Response.Redirect("/admin/reports/confirm.aspx");
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


        string sql = "   INSERT INTO [dbo].[ReportSchedule]([ReportID],ReportName,[RunFrequency],[StartDate],[EmailAddress],RunTime,LastRunDate) ";
        sql += " VALUES (@ReportID,@ReportName,@RunFrequency,@StartDate,@EmailAddress,@RunTime,GETDATE() )";

        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@ReportID", reportID); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@ReportName", ReportName.Text); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@RunFrequency", frequency.Text); //varchar(max),>
        sqlcmd.Parameters.AddWithValue("@StartDate", startDate.Text); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@EmailAddress", validEmails.TrimEnd(',')); //nchar(50),>
        sqlcmd.Parameters.AddWithValue("@RunTime", SendTime.Text); //nchar(50),>
        DataBase.executeCommandWithParameters(sqlcmd);
        SchedCreatePanel.Visible = true;
        UpdSaveMessage = "Report Has been Scheduled";



       // createTask(ReportName.Text, frequency.Text, startDate.Text, SendTime.Text, reportID);

        Response.Redirect("/admin/reports/confirm.aspx");
    }



    private void createTask(string reportName, string Frequency, string scheduleDate, string scheduleTime, string reportID)
    {

        //Create the bat file to run
        // string path = @"c:\temp\MyTest.txt";
        string path = commonfunctions.BaseDirectory + "/batFiles/report_" + reportID + ".bat";
        string localPath = new Uri(path).LocalPath;

        // Delete the file if it exists. 
        if (File.Exists(path))
        {
            File.Delete(path);
        }


        TextWriter tw = new StreamWriter(path);
        string urlTo = "http://s078v31.ba.ad.ssa.gov/admin/reports/sendScheduledReport.aspx?reportID=" + reportID;

        // write a line of text to the file
        tw.WriteLine("taskkill /F /IM iexplore.exe");
        tw.WriteLine("START iexplore " + urlTo);
        // close the stream
        tw.Close();



        //create task service instance
        ITaskService taskService = new TaskSchedulerClass();
        taskService.Connect();
        ITaskDefinition taskDefinition = taskService.NewTask(0);
        taskDefinition.Settings.Enabled = true;
        taskDefinition.Settings.Compatibility = _TASK_COMPATIBILITY.TASK_COMPATIBILITY_V2_1;
        //taskDefinition.Settings.AllowDemandStart = true;
       // taskDefinition.Settings.Priority = 1;
        //create trigger for task creation.
        ITriggerCollection _iTriggerCollection = taskDefinition.Triggers;
        //ITrigger _trigger = _iTriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_WEEKLY);
        //_trigger.StartBoundary = DateTime.Now.AddSeconds(15).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
        //_trigger.EndBoundary = DateTime.Now.AddMinutes(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
        //_trigger.Enabled = true;


        DateTime scheduleDateDT = convertTimeToDateTime(scheduleDate, scheduleTime);

        switch (Frequency)
        {
            case "Daily":
                IDailyTrigger tt = (IDailyTrigger)_iTriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);
                tt.DaysInterval = 1;
                tt.StartBoundary = scheduleDateDT.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                //  tt.EndBoundary = scheduleDateDT.AddMinutes(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                tt.Enabled = true;
                break;

            case "Weekly":
            case "Bi-Weekly":
                IWeeklyTrigger tWeekly = (IWeeklyTrigger)_iTriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_WEEKLY);
                if (Frequency == "Weekly")
                {
                    tWeekly.WeeksInterval = 1;
                }
                else
                {
                    tWeekly.WeeksInterval = 2;
                }
                tWeekly.StartBoundary = scheduleDateDT.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                // tWeekly.EndBoundary = scheduleDateDT.AddMinutes(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                tWeekly.Enabled = true;
                break;


            case "Monthly":
            case "Quarterly":

                IMonthlyTrigger tMontly = (IMonthlyTrigger)_iTriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_MONTHLY);
                if (Frequency == "Quarterly")
                {
                    tMontly.MonthsOfYear = 1;
                    tMontly.MonthsOfYear = 512;
                }
                else
                {
                    // tMontly.MonthsOfYear = 2;
                }
                tMontly.DaysOfMonth = 1;
                tMontly.StartBoundary = scheduleDateDT.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                //  tMontly.EndBoundary = scheduleDateDT.AddMinutes(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                tMontly.Enabled = true;
                break;




            case "Annually":

                break;
        }



        ///get actions.
        IActionCollection actions = taskDefinition.Actions;
        _TASK_ACTION_TYPE actionType = _TASK_ACTION_TYPE.TASK_ACTION_EXEC;

        //create new action
        IAction action = actions.Create(actionType);
        IExecAction execAction = action as IExecAction;
        execAction.Path = commonfunctions.BaseDirectory + "batFiles\\report_" + reportID + ".bat"; // @"C:\Windows\System32\notepad.exe";
        ITaskFolder rootFolder = taskService.GetFolder(@"\SSADL");

        //register task.
        rootFolder.RegisterTaskDefinition(reportName, taskDefinition, Convert.ToInt16(_TASK_CREATION.TASK_CREATE_OR_UPDATE), null, null, _TASK_LOGON_TYPE.TASK_LOGON_SERVICE_ACCOUNT, null);
    }

    public DateTime convertTimeToDateTime(string scheduleDate, string time)
    {
        DateTime scheduleDateDT = Convert.ToDateTime(scheduleDate);
        string[] Time = time.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        string Hr = Time[0].ToString().Trim();
        string mm = Time[1].ToString().Trim();
        string AMPM = Time[2].ToString().Trim();
        string timetouse = Hr + ":" + mm + ":" + "00" + " " + AMPM;

        string myDateString = scheduleDateDT.ToShortDateString() + " " + timetouse; //  "2/17/2011 6:46:01 PM";
        DateTime dt = DateTime.Parse(myDateString);
        //string timeString = datetime.ToShortTimeString();

        return dt;
    }


    private string checkValidEmailWithCore(string email)
    {
        if (commonfunctions.Environment != "FIGLEAF")
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
        }
        else
        {
            return email;
        }

        // throw new NotImplementedException();
    }

    public string invalidEmailsFound { get; set; }

    public string ReportNameData { get; set; }

    public string ScheduleReportID { get; set; }

    public string UpdSaveMessage { get; set; }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect(WhereFrom.Value);
    }
}