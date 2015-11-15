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

    void Page_PreInit(Object sender, EventArgs e)
    {
        if (!loginSSA.isAdminUser())
        {
            this.MasterPageFile = "~/admin/Masters/AdminMainNoCheck.master";
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!loginSSA.isAdminUser()) SideNav.Visible = false;
        

        if (!IsPostBack)
        {
          

            if (Request.QueryString["reportId"] != null)
            {
                reportID = Request.QueryString["reportId"].ToString();
                getReport(reportID);
                // RunReport.Visible = false;
                //SaveReport.Visible = true;
                checkIfOnSchdule(reportID);

               // SchedulingMessage = Reports.ReportSchedule(reportID);

                Dictionary<string, string> getSchedulingMessage = new Dictionary<string, string>();

                getSchedulingMessage = Reports.ReportSchedule2(reportID);
                SchedulingMessage = getSchedulingMessage["Message"];
                scheduledreportid = getSchedulingMessage["ID"];
                SchedulingMessageTag = getSchedulingMessage["UpdateCreate"]; 
                


                //update last run date
                Reports.RecordRunBy(reportID);


                RunMEssage = Reports.getLastRunDate(reportID);
                
            }
        }

    }

    private void checkIfOnSchdule(string reportID)
    {
        string count = DataBase.returnOneValue("Select count(*) from ReportSchedule where ReportID='" + reportID + "'");
        if (count == "0")
        {
            RunSchudule.Visible = true;
            RunSchudule.NavigateUrl = "/admin/reports/scheduledreport.aspx?reportid=" + reportID ;

        }
        else
        {
            SchedulePanel.Visible = true;
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
                SelectedSubmittedDate = Reports.getSelectedDateSql("CAST([SubmissionDateandTime] AS DATE)", SubmittedDate, customDateRangeSubmittedDateStart, customDateRangeSubmittedDateEnd);
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

                SelectedClosedDate = Reports.getSelectedDateSql("CAST([ModifiedDateTime] AS DATE)", ClosedDate, customDateRangeClosedDateStart, customDateRangeClosedDateEnd);


                messageDate = " Closed Date " + ClosedDate;
                if (customDateRangeClosedDateStart != "")
                {
                    messageDate += " ( " + customDateRangeClosedDateStart + " " + customDateRangeClosedDateEnd + " )";
                }

                BuildAllSQL = " where LTRIM(RTRIM(FormStatus)) in ('Closed' , 'Approved-Closed','Declined-Closed') "; 
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


            // string sqlOrderBy = commonfunctions.getFieldValue(XMLDoc, "DisplayResultsBy");
              DisplayResultsBy = commonfunctions.getFieldValue(XMLDoc, "DisplayResultsBy");


            BuildAllSQL += SelectedFormStatus;


            SqlCommand cmd = new SqlCommand("HelpRequests_Reports_By");
            cmd.Parameters.AddWithValue("@BuildAllSQL", BuildAllSQL);
            cmd.Parameters.AddWithValue("@DisplayBy", DisplayResultsBy);

            ResultsGridView.DataSource = DataBase.executeStoreProcudure(cmd);
            ResultsGridView.DataBind();

            if (ResultsGridView.Rows.Count > 0)
            {
                ResultsGridView.HeaderRow.TableSection = TableRowSection.TableHeader;


            }

            ReportSummary = "<i>Showing Data for Help Requests (" + getFormNames(formIDDBs) + ") for ";
            ReportSummary += SubmittedDate;
            ReportSummary += " with Status of " + status + " in between " + messageDate + " <br> Displayed by " + DisplayResultsBy;
            ReportSummary += " <br/>  </i>";
            ChangeParameters.NavigateUrl = "/admin/reports/helprequests/AdHocHelpReports.aspx?reportid=" + reportID;

             //Response.Write(BuildAllSQL);
            //Response.Write(formIDDBs);
        }
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            try
            {
                e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).ToShortDateString();
            }
            catch { }
            try
            {
                e.Row.Cells[3].Text = DateTime.Parse(e.Row.Cells[3].Text).ToShortDateString();
            }
            catch { }

            try
            {
                string formStatus =  e.Row.Cells[1].Text.ToLower();
                if (formStatus.Contains("close"))
                {
                    e.Row.Cells[4].Text = DateTime.Parse(e.Row.Cells[4].Text).ToShortDateString();
                }
                else
                {
                    e.Row.Cells[4].Text =   " -- ";
                }
            }
            catch { }

            try
            {
                 e.Row.Cells[5].Text = getResourceRequestsAdminUsers(e.Row.Cells[5].Text);
                
            }
            catch { }
        }
    }

    private string getResourceRequestsAdminUsers(string requestRecipients)
    {
        string allRecipientsName = string.Empty;

        try
        {
            string allRecipients = requestRecipients.TrimEnd(',').TrimStart(',');
            DataTableReader dtr = DataBase.dbDataTable("select concat(RTRIM(LTRIM([FirstName])) , ' ' ,RTRIM(LTRIM([LastName])) ) as Names from [dbo].[users] where id in (" + allRecipients + ") ", "Admin.DbConnection").CreateDataReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    allRecipientsName += dtr["Names"].ToString() + ",";

                }

            }
        }
        catch { }
        return allRecipientsName.TrimEnd(',');

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



  


    private string getSelectedStatus(string selectedStatus)
    {

        string output = "";
        switch (selectedStatus)
        {
            case "Any":
                output = "'New' , 'Open' , 'Closed', 'Approved-Closed','Approved-Not-Notified','Declined-Not-Notified','Declined-Closed','Resolved-Not-Notified' ";
                break;
            case "New":
                output = "'New' ";
                break;

            case "Open":
                output = "'Open'";
                break;

            case "New or Open":
                output = "'New' , 'Open','Approved-Not-Notified','Declined-Not-Notified','Resolved-Not-Notified' ";
                break;

            case "Closed":
                output = "'Closed', 'Approved-Closed','Declined-Closed' ";
                break;

        }

        return " and LTRIM(RTRIM(FormStatus)) in (" + output + ")";
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

    public virtual string DisplayResultsBy
    {
        get
        {
            if (ViewState["DisplayResultsBy"] == null)
            {
                ViewState["DisplayResultsBy"] = "Form";
            }
            return (string)ViewState["DisplayResultsBy"];
        }
        set
        {
            ViewState["DisplayResultsBy"] = value;
        }
    }

    
    //public DataTable dtAllResouces  ;
    //private DataTable getAllResouces()
    //{
    //    return DataBase.dbDataTable("Select * from Resources order by ResourceName");

    //}
    protected void ExcelClick_Click(object sender, EventArgs e)
    {
       


        SqlCommand cmd = new SqlCommand("HelpRequests_Reports_By");
        cmd.Parameters.AddWithValue("@BuildAllSQL", BuildAllSQL);
        cmd.Parameters.AddWithValue("@DisplayBy", DisplayResultsBy);

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
                    if (columnName == "Assigned To")
                    {
                        myExport[columnName] = getResourceRequestsAdminUsers(dtr[columnName].ToString());
                    }
                    else
                    {
                        myExport[columnName] = dtr[columnName].ToString();
                    }
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

    public string scheduledreportid { get; set; }
}