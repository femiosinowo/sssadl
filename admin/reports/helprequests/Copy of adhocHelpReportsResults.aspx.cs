using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Collections;

public partial class admin_reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["reportId"] != null)
            {
                reportID = Request.QueryString["reportId"].ToString();
                getReport(reportID);
                // RunReport.Visible = false;
                //SaveReport.Visible = true;
                checkIfOnSchdule(reportID);

                //update last run date
                DataBase.executeCommand("Update   report set LastRunDate=GETDATE() where ID='" + reportID + "'");
                string sqlRecordLastRun = "INSERT INTO [dbo].[ReportInstances]([ReportID],[DateTimRun],[RunbyPIN])";
                sqlRecordLastRun += " VALUES('" + reportID + "',GETDATE(),'" + loginSSA.myPIN + "')";
                DataBase.executeCommand(sqlRecordLastRun);


                RunMEssage = Reports.getLastRunDate(reportID);
                string sqlRecordLastRun2 = "INSERT INTO [dbo].[ReportInstances]([ReportID],[DateTimRun],[RunbyPIN])";
                sqlRecordLastRun2 += " VALUES('" + reportID + "',GETDATE(),'" + loginSSA.myPIN + "')";
                DataBase.executeCommand(sqlRecordLastRun2);
            }
        }

    }

    private void checkIfOnSchdule(string reportID)
    {
        string count = DataBase.returnOneValue("Select count(*) from ReportSchedule where ReportID='" + reportID + "'");
        if (count == "0")
        {
            RunSchudule.Visible = true;
            RunSchudule.NavigateUrl = "/admin/reports/scheduledreport.aspx?reportid=" + reportID;
        }
    }

    protected void getReport(string reportID)
    {
        //  ReportSummary = "Show data for ";
        DataTableReader dtr = DataBase.dbDataTable("Select * from report where ID='" + reportID + "'").CreateDataReader();
        if (dtr.HasRows)
        {
            dtr.Read();
            string xmldata = dtr["ReportXMLData"].ToString();
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.LoadXml(xmldata);

            string messageDate = "";
            string status = commonfunctions.getFieldValue(XMLDoc, "RequestStatus");
            string SelectedFormStatus = getSelectedStatus(status);


            string SelectedSubmittedDate = "";
            string checkSubmittedDate = commonfunctions.getFieldValue(XMLDoc, "checkSubmittedDate");
            string customDateRangeSubmittedDateStart = commonfunctions.getFieldValue(XMLDoc, "customDateRangeSubmittedDateStart");

            string customDateRangeSubmittedDateEnd = commonfunctions.getFieldValue(XMLDoc, "customDateRangeSubmittedDateEnd");

            if (checkSubmittedDate == "true")
            {
                SubmittedDate = commonfunctions.getFieldValue(XMLDoc, "SubmittedDate");

                //if (SubmittedDate == "Custom Dates")
                //{

                //    SelectedSubmittedDate = getSelectedDateSql("CAST([SubmissionDateandTime] AS DATE)", SubmittedDate, customDateRangeSubmittedDateStart, customDateRangeSubmittedDateEnd);
                //}
                //else
                //{
                //    customDatSDDisplay = " style='display:none' ";
                //}
                SelectedSubmittedDate = getSelectedDateSql("CAST([SubmissionDateandTime] AS DATE)", SubmittedDate, customDateRangeSubmittedDateStart, customDateRangeSubmittedDateEnd);
                messageDate = " Submitted Date  " + SubmittedDate;
                if (customDateRangeSubmittedDateStart != "")
                {
                    messageDate += " ( " + customDateRangeSubmittedDateStart + " " + customDateRangeSubmittedDateEnd + " )";
                }
                // BuildAllSQL = SelectedFormStatus;
            }



            string SelectedClosedDate = "";
            string ClosedDate = commonfunctions.getFieldValue(XMLDoc, "ClosedDate");
            string checkClosedDate = commonfunctions.getFieldValue(XMLDoc, "checkClosedDate");
            string customDateRangeClosedDateStart = commonfunctions.getFieldValue(XMLDoc, "customDateRangeClosedDateStart");

            string customDateRangeClosedDateEnd = commonfunctions.getFieldValue(XMLDoc, "customDateRangeClosedDateEnd");
            if (checkClosedDate == "true")
            {

                SelectedClosedDate = getSelectedDateSql("CAST([ModifiedDateTime] AS DATE)", ClosedDate, customDateRangeClosedDateStart, customDateRangeClosedDateEnd);


                messageDate = " Closed Date " + ClosedDate;
                if (customDateRangeClosedDateStart != "")
                {
                    messageDate += " ( " + customDateRangeClosedDateStart + " " + customDateRangeClosedDateEnd + " )";
                }

                BuildAllSQL = " where LTRIM(RTRIM(FormStatus)) in ('Closed') "; ;
            }
            else
            {

                if (SelectedSubmittedDate != "")
                {
                    BuildAllSQL += " where " + SelectedSubmittedDate + " ";
                }
            }


            string formIDDBs = commonfunctions.getFieldValue(XMLDoc, "FormIDs");
            if (formIDDBs != "")
            {
                BuildAllSQL += " and FormID in (" + formIDDBs + ") ";
            }


            string sqlOrderBy = commonfunctions.getFieldValue(XMLDoc, "DisplayResultsBy");
            string DisplayResultsBy = commonfunctions.getFieldValue(XMLDoc, "DisplayResultsBy");
            if (sqlOrderBy == "Date")
            {
                sqlOrderBy = "SubmissionDateandTime";

            }
            else
            {
                sqlOrderBy = "RequestType";
            }

            BuildAllSQL += SelectedFormStatus + " order by " + sqlOrderBy + "; ";

            SqlCommand cmd = new SqlCommand("ReturnAllHelpRequests_ForReports");
            cmd.Parameters.AddWithValue("@BuildAllSQL", BuildAllSQL);
   
            passwordAssistanceLV.DataSource = DataBase.executeStoreProcudure(cmd);
            passwordAssistanceLV.DataBind();

            ReportSummary = "<i>Showing Data for Help Requests (" + getFormNames(formIDDBs) + ") for ";
            ReportSummary += SubmittedDate;
            ReportSummary += " with Status of " + status + " in between " + messageDate + " order by " + DisplayResultsBy;
            ReportSummary +=" <br/>  </i>";
            ChangeParameters.NavigateUrl = "/admin/reports/helprequests/AdHocHelpReports.aspx?reportid=" + reportID;

           // Response.Write(BuildAllSQL);

        }
    }

    private string getFormNames(string formIDDBs)
    {
        string allNames = "";
        DataTableReader dtr = DataBase.dbDataTable("Select   Form from AutoReplies where ID in (" + formIDDBs + ")").CreateDataReader();
        if (dtr.HasRows)
        {
            while (dtr.Read())
            {
                allNames += dtr["Form"].ToString() + ",";
            }
        }

        return allNames.TrimEnd(',');
    }



    private string getSelectedDateSql(string fieldName, string RequestSubmittedDateDD, string CustomStartDate, string CustomEndDate)
    {
        // string sss = RequestSubmittedDateDD.SelectedValue;
        string output = "";
        int delta = 0;
        DateTime today = DateTime.Now;
        switch (RequestSubmittedDateDD)
        {
            case "Today":
                output = fieldName + "='" + today.ToString("d") + "'";
                break;
            case "This Week":

                delta = DayOfWeek.Sunday - today.DayOfWeek;
                DateTime BeginSunday = today.AddDays(delta);
                DateTime EndSaturday = BeginSunday.AddDays(6);
                output = fieldName + " BETWEEN  '" + BeginSunday.ToString("d") + "'  and '" + EndSaturday.ToString("d") + "' ";

                break;

            case "Last Week":
                delta = DayOfWeek.Sunday - today.DayOfWeek;
                DateTime BeginLastWeekSunday = today.AddDays(delta).AddDays(-7);
                DateTime EndLastWeekSaturday = BeginLastWeekSunday.AddDays(6);
                output = fieldName + " BETWEEN  '" + BeginLastWeekSunday.ToString("d") + "'  and '" + EndLastWeekSaturday.ToString("d") + "' ";

                break;

            case "Last 30 Days":
                DateTime Last30Days = today.AddDays(-30);
                output = fieldName + " BETWEEN  '" + Last30Days.ToString("d") + "'  and '" + today.ToString("d") + "' ";

                break;

            case "This Month":

                DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
                DateTime firstOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);
                DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
                output = fieldName + " BETWEEN  '" + startOfMonth.ToString("d") + " 00:00:00'  and '" + lastOfThisMonth.ToString("d") + " 23:59:59' ";
                break;

            case "Last Month":

                DateTime BeginofLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
                DateTime EndOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
                output = fieldName + " BETWEEN  '" + BeginofLastMonth.ToString("d") + "'  and '" + EndOfLastMonth.ToString("d") + "' ";
                break;

            case "This Quarter":
                Dictionary<string, string> getQuaterDates1 = getQuaterDates(GetQuarter(today));
                output = fieldName + " BETWEEN  '" + getQuaterDates1["StartDate"] + "'  and '" + getQuaterDates1["EndDate"] + "' ";
                break;

            case "Last Quarter":
                Dictionary<string, string> getQuaterDates2 = getQuaterDates(GetQuarter(today) - 1);
                output = fieldName + " BETWEEN  '" + getQuaterDates2["StartDate"] + "'  and '" + getQuaterDates2["EndDate"] + "' ";
                break;


            case "This Fiscal Year":
                Dictionary<string, string> FiscalDates = getFiscalDates(AdminFunc.CurrentFiscalYear(DateTime.Now));
                output = fieldName + " BETWEEN  '" + FiscalDates["StartDate"] + "'  and '" + FiscalDates["EndDate"] + "' ";

                break;


            case "Last Fiscal Year":
                Dictionary<string, string> FiscalDates2 = getFiscalDates(AdminFunc.CurrentFiscalYear(DateTime.Now) - 1);
                output = fieldName + " BETWEEN  '" + FiscalDates2["StartDate"] + "'  and '" + FiscalDates2["EndDate"] + "' ";
                break;

            case "Custom Dates":
                output = fieldName + " BETWEEN  '" + CustomStartDate + "'  and '" + CustomEndDate + "' ";
                break;

        }

        return output;
    }


    private string getSelectedStatus(string selectedStatus)
    {

        string output = "";
        switch (selectedStatus)
        {
            case "Any":
                output = "'New' , 'Open' , 'Closed' ";
                break;
            case "New":
                output = "'New' ";
                break;

            case "Open":
                output = "'Open'";
                break;

            case "New or Open":
                output = "'New' , 'Open' ";
                break;

            case "Closed":
                output = "'Closed' ";
                break;

        }

        return " and LTRIM(RTRIM(FormStatus)) in (" + output + ")";
    }


    public int GetQuarter(DateTime date)
    {
        if (date.Month >= 10 && date.Month <= 12)
            return 1;
        else if (date.Month >= 1 && date.Month <= 3)
            return 2;
        else if (date.Month >= 4 && date.Month <= 6)
            return 3;
        else
            return 4;

    }
    public Dictionary<string, string> getFiscalDates(int fiscalyear)
    {
        Dictionary<string, string> startEndDates = new Dictionary<string, string>();
        DateTime start = new DateTime(fiscalyear - 1, 10, 1);
        DateTime end = new DateTime(fiscalyear, 9, 1).AddMonths(1).AddDays(-1);
        startEndDates.Add("StartDate", start.ToString("d"));
        startEndDates.Add("EndDate", end.ToString("d"));
        return startEndDates;
    }
    public Dictionary<string, string> getQuaterDates(int Quater)
    {
        Dictionary<string, string> startEndDates = new Dictionary<string, string>();
        int fiscealyear = AdminFunc.CurrentFiscalYear(DateTime.Now);
        switch (Quater)
        {
            case 1: //(date.Month >= 10 && date.Month <= 12)

                DateTime start1 = new DateTime(fiscealyear, 10, 1);
                DateTime end1 = new DateTime(fiscealyear, 12, 1).AddMonths(1).AddDays(-1);
                //DateTime firstOfNextMonth = end1.AddMonths(1).AddDays(-1);


                startEndDates.Add("StartDate", start1.ToString("d"));
                startEndDates.Add("EndDate", end1.ToString("d"));
                break;

            case 2: //(date.Month >= 1 && date.Month <= 3)
                DateTime start2 = new DateTime(fiscealyear, 1, 1);
                DateTime end2 = new DateTime(fiscealyear, 3, 1).AddMonths(1).AddDays(-1); ;
                startEndDates.Add("StartDate", start2.ToString("d"));
                startEndDates.Add("EndDate", end2.ToString("d"));
                break;

            case 3: //(date.Month >= 4 && date.Month <= 6)
                DateTime start3 = new DateTime(fiscealyear, 4, 1);
                DateTime end3 = new DateTime(fiscealyear, 6, 1).AddMonths(1).AddDays(-1); ;
                startEndDates.Add("StartDate", start3.ToString("d"));
                startEndDates.Add("EndDate", end3.ToString("d"));
                break;

            case 4: //(date.Month >= 7 && date.Month <= 9)
                DateTime start4 = new DateTime(fiscealyear, 7, 1);
                DateTime end4 = new DateTime(fiscealyear, 9, 1).AddMonths(1).AddDays(-1); ;

                startEndDates.Add("StartDate", start4.ToString("d"));
                startEndDates.Add("EndDate", end4.ToString("d"));
                break;
        }



        return startEndDates;
    }



    protected void passwordAssistanceLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;
            Literal SubmittedByPINTxt = (Literal)ditem.FindControl("SubmittedByPIN");
            Literal SubmissionDateandTimeTxt = (Literal)ditem.FindControl("SubmissionDateandTime");
           // Literal AssignedTo = (Literal)ditem.FindControl("AssignedTo");
            Literal ActiveLit = (Literal)ditem.FindControl("Active");
            Literal RequestType = (Literal)ditem.FindControl("RequestType");
            //   HyperLink ViewEdit = (HyperLink)ditem.FindControl("ViewEdit");

            Literal rowClick = (Literal)ditem.FindControl("rowClick");
            rowClick.Text = " ondblclick='javascript:goto(" + item["ID"].ToString() + " , " + item["FormID"].ToString() + ")' ";

            //  string resourceID = item["ResourceToAccess"].ToString();
            //  ViewEdit.NavigateUrl = "/admin/requests/editrequest.aspx?reqid=" + item["ID"].ToString();
            string requestorPIN = item["SubmittedForPIN"].ToString().Trim();
            if (requestorPIN == "" || requestorPIN == "None") requestorPIN = item["SubmittedByPIN"].ToString().Trim();



            string SubmissionDateandTime = item["SubmissionDateandTime"].ToString().Trim();
            SubmissionDateandTimeTxt.Text = getDatabaseDateformat(SubmissionDateandTime);


            //  Dictionary<string, string> userdetails = loginSSA.GetUsersDetails(requestorPIN);
            //try
            //{
            //    SubmittedByPINTxt.Text = userdetails["LastName"] + " " + userdetails["FirstName"]; //getUserNameByPIN(item["SubmittedForPIN"].ToString().Trim());
            //}
            //catch { }
            SubmittedByPINTxt.Text = item["SubmittedForLastName"] + " " + item["SubmittedForFirstName"];
            // AssignedTo.Text = getResourceRequestsAdminUsers(item["RequestsSentTo"].ToString().Trim());
            string Active = item["FormStatus"].ToString().Trim();
            ActiveLit.Text = Active;


            RequestType.Text = item["RequestType"].ToString().Trim();

        }
    }


    public string getDatabaseDateformat(string datetimeString)
    {

        try
        {
            DateTime dt = new DateTime();
            dt = DateTime.Parse(datetimeString);
            return dt.ToShortDateString();
        }
        catch
        {

            return "";
        }
    }
    //public virtual string sqlFormIDs
    //{
    //    get
    //    {
    //        if (ViewState["sqlFormIDs"] == null)
    //        {
    //            ViewState["sqlFormIDs"] = "";
    //        }
    //        return (string)ViewState["sqlFormIDs"];
    //    }
    //    set
    //    {
    //        ViewState["sqlFormIDs"] = value;
    //    }
    //}
    //public virtual string sqlOrderBy
    //{
    //    get
    //    {
    //        if (ViewState["sqlOrderBy"] == null)
    //        {
    //            ViewState["sqlOrderBy"] = " ";
    //        }
    //        return (string)ViewState["sqlOrderBy"];
    //    }
    //    set
    //    {
    //        ViewState["sqlOrderBy"] = value;
    //    }
    //}
    public virtual string BuildAllSQL
    {
        get
        {
            if (ViewState["BuildAllSQL"] == null)
            {
                ViewState["BuildAllSQL"] = " ";
            }
            return (string)ViewState["BuildAllSQL"];
        }
        set
        {
            ViewState["BuildAllSQL"] = value;
        }
    }

    //public DataTable dtAllResouces  ;
    //private DataTable getAllResouces()
    //{
    //    return DataBase.dbDataTable("Select * from Resources order by ResourceName");

    //}
    protected void ExcelClick_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("ReturnAllHelpRequests_ForReports");
        cmd.Parameters.AddWithValue("@BuildAllSQL", BuildAllSQL);       


        CsvExport myExport = new CsvExport();
        ArrayList columns = new ArrayList();
        DataTableReader dtr = DataBase.executeStoreProcudure(cmd).CreateDataReader();

        for (int i = 0; i < dtr.FieldCount; i++)
        {
            columns.Add(dtr.GetName(i));
        }



        if (dtr.HasRows)
        {
            while (dtr.Read())
            {

                //   Then you can do any of the following three output options:
                myExport.AddRow();
                foreach (var ii in columns)
                {
                    string columnName = ii.ToString();
                    myExport[columnName] = dtr[columnName].ToString();
                }





            }
        }


        myCsv = myExport.Export();

        string attachment = "attachment; filename=AdHocResourcesReports-data-" + DateTime.Now.ToShortDateString() + ".csv";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.AddHeader("Pragma", "public");
        HttpContext.Current.Response.Write(myCsv);
        HttpContext.Current.Response.End();
    }

    

    public string uniqueVistor_LastYearFull { get; set; }

    public string NumberofHits_LastYearFull { get; set; }

    public string uniqueVistor_LastYeartoDate { get; set; }

    public string NumberofHits_LastYeartoDate { get; set; }

    public string uniqueVistor_thisYeartoDate { get; set; }

    public string NumberofHits_thisYeartoDate { get; set; }

    public string lastYearDisplay { get; set; }

    public string thisYearDisplay { get; set; }

    public string percentDetails_uniqueVisitor { get; set; }

    public string percentDetails_NumberofHits { get; set; }

    public string SchedulingMessage { get; set; }

    public string SchedulingMessageTag { get; set; }

    public string myCsv { get; set; }

    public string ReportSummary { get; set; }

    public string SubmittedDate { get; set; }

    public string reportID { get; set; }

    public string RunMEssage { get; set; }
}