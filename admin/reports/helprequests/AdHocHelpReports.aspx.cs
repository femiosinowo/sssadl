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
using System.Xml;

public partial class admin_reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            
            //dtAllResouces = getAllResouces();
            //ListResources.DataSource = dtAllResouces;
            //ListResources.DataBind();
            RequestType.DataSource = DataBase.dbDataTable("Select ID, Form from AutoReplies ");
            RequestType.DataBind();


            if (Request.QueryString["reportId"] != null)
            {
                getData(Request.QueryString["reportId"].ToString());
                RunReport.Visible = false;
                SaveReport.Visible = true;
            }
            else
            {
                RequestClosedDateDD.Items.FindByValue("Last 30 Days").Selected = true;
                RequestSubmittedDateDD.Items.FindByValue("Last 30 Days").Selected = true;
                LimitSubmittedDateRange.Checked = true;
                limitClosedDate.Checked = false;
                customDatSDDisplay = " style='display:none' ";
                customDatCDDisplay = " style='display:none' ";
                SaveReport.Visible = false;
            }
            

        }
    }
    private void getData(string reportID){

        DataTableReader dtr = DataBase.dbDataTable("Select * from report where ID='" + reportID + "'").CreateDataReader();
        if (dtr.HasRows)
        {
            dtr.Read();
            string xmldata = dtr["ReportXMLData"].ToString();
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.LoadXml(xmldata);

            string formIDDBs = commonfunctions.getFieldValue(XMLDoc, "FormIDs");

            string[] formIDs = formIDDBs.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (string formId in formIDs)
            {
                try
                {
                    RequestType.Items.FindByValue(formId).Selected = true;
                }
                catch { }
            }

            //
            string RequestStatus = commonfunctions.getFieldValue(XMLDoc, "RequestStatus");
            RequestStatusDD.Items.FindByValue(RequestStatus).Selected = true;

            string checkSubmittedDate = commonfunctions.getFieldValue(XMLDoc, "checkSubmittedDate");
            if (checkSubmittedDate == "true") LimitSubmittedDateRange.Checked = true;

            string SubmittedDate = commonfunctions.getFieldValue(XMLDoc, "SubmittedDate");
            RequestSubmittedDateDD.Items.FindByValue(SubmittedDate).Selected = true;
            if (SubmittedDate == "Custom Dates")
            {

                customDatSDDisplay = " style='display:block' ";
            }
            else
            {
                customDatSDDisplay = " style='display:none' ";
            }

            string customDateRangeSubmittedDateStart = commonfunctions.getFieldValue(XMLDoc, "customDateRangeSubmittedDateStart");
            CustomDatesSubmittedDate_StartDate.Text = customDateRangeSubmittedDateStart;

            string customDateRangeSubmittedDateEnd = commonfunctions.getFieldValue(XMLDoc, "customDateRangeSubmittedDateEnd");
            CustomDatesSubmittedDate_EndDate.Text = customDateRangeSubmittedDateEnd;

            string checkClosedDate = commonfunctions.getFieldValue(XMLDoc, "checkClosedDate");
            if (checkClosedDate == "true")
            {
                limitClosedDate.Checked = true;
                RequestClosedDateDD.Enabled = true;
            }


            string ClosedDate = commonfunctions.getFieldValue(XMLDoc, "ClosedDate");
            RequestClosedDateDD.Items.FindByValue(ClosedDate).Selected = true;
            if (ClosedDate == "Custom Dates")
            {
                customDatCDDisplay = " style='display:block' ";

            }
            else
            {
                customDatCDDisplay = " style='display:none' ";
            }

            string customDateRangeClosedDateStart = commonfunctions.getFieldValue(XMLDoc, "customDateRangeClosedDateStart");
            CustomDatesClosedDate_StartDate.Text = customDateRangeClosedDateStart;

            string customDateRangeClosedDateEnd = commonfunctions.getFieldValue(XMLDoc, "customDateRangeClosedDateEnd");
            CustomDatesClosedDate_EndDate.Text = customDateRangeClosedDateEnd;

            string DisplayResultsBy = commonfunctions.getFieldValue(XMLDoc, "DisplayResultsBy");
            DisplayResutlsbyDD.Items.FindByValue(DisplayResultsBy).Selected = true;


        }
    }



    protected void SaveReport_Click(object sender, EventArgs e)
    {
        string XMLData = getXML();
        string reportID = Request.QueryString["reportid"].ToString();
        string sql = "UPDATE [dbo].[Report] SET [LastModifiedDate] = GETDATE()   ,[ReportXMLData] = @ReportXMLData ,[ReportSQLStatement] = @ReportSQLStatement ";
        sql += " WHERE ID=" + reportID;
     //   Response.Write(sql);
        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@ReportXMLData", XMLData);
        sqlcmd.Parameters.AddWithValue("@ReportSQLStatement", getSQL());
       
        DataBase.executeCommandWithParameters(sqlcmd);
        Response.Redirect("/admin/reports/helprequests/adhocHelpReportsResults.aspx?reportid=" + reportID + "#sb=1");

    }

   


    protected void RunReport_Click(object sender, EventArgs e)
    {

      string XMLData =  getXML();


        string sql = " INSERT INTO [dbo].[Report]([Name],[CreatedbyPIN],[CreateDate],[LastModifiedDate],[LastRunDate],[ReportExecutionFile],[ReportSQLStatement],[ReportXMLData], ReportType) ";
        sql += "  VALUES(@Name,@CreatedbyPIN ,GETDATE() ,GETDATE() ,GETDATE(),@ReportExecutionFile ,@ReportSQLStatement ,@ReportXMLData, 'Ad Hoc Help Report' ) ";

        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@Name", "Ad Hoc Help Report");
        sqlcmd.Parameters.AddWithValue("@CreatedbyPIN", loginSSA.myPIN);
        sqlcmd.Parameters.AddWithValue("@ReportExecutionFile", "");
        sqlcmd.Parameters.AddWithValue("@ReportSQLStatement", getSQL());
        sqlcmd.Parameters.AddWithValue("@ReportXMLData", XMLData);
        string reportID = DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmd);
        Response.Redirect("/admin/reports/helprequests/adhocHelpReportsResults.aspx?reportid=" + reportID + "#sb=1");

    }



    protected string getXML()
    {
        string XMLData = string.Empty;

        string SelectedFormIDs = string.Empty;
        foreach (ListItem li in RequestType.Items)
        {
            if (li.Selected == true)
            {
                SelectedFormIDs += li.Value + ",";
            }

        }
        string sqlFormIDs = SelectedFormIDs.TrimEnd(',');



        XMLData += "<root>";
        XMLData += "<FormIDs>";
        XMLData += sqlFormIDs;
        XMLData += "</FormIDs>";


        XMLData += "<RequestStatus>";
        XMLData += RequestStatusDD.SelectedValue;
        XMLData += "</RequestStatus>";


        XMLData += "<checkSubmittedDate>";
        if (LimitSubmittedDateRange.Checked == true)
        {
            XMLData += "true";
        }
        else
        {
            XMLData += "false";
        }
        XMLData += "</checkSubmittedDate>";

        XMLData += "<SubmittedDate>";
        XMLData += RequestSubmittedDateDD.SelectedItem.Value;
        XMLData += "</SubmittedDate>";

        XMLData += "<customDateRangeSubmittedDateStart>";
        XMLData += CustomDatesSubmittedDate_StartDate.Text;
        XMLData += "</customDateRangeSubmittedDateStart>";

        XMLData += "<customDateRangeSubmittedDateEnd>";
        XMLData += CustomDatesSubmittedDate_EndDate.Text;
        XMLData += "</customDateRangeSubmittedDateEnd>";

        XMLData += "<checkClosedDate>";
        if (limitClosedDate.Checked == true)
        {
            XMLData += "true";
        }
        else
        {
            XMLData += "false";
        }
        XMLData += "</checkClosedDate>";



        XMLData += "<ClosedDate>";
        XMLData += RequestClosedDateDD.SelectedItem.Value;
        XMLData += "</ClosedDate>";


        XMLData += "<customDateRangeClosedDateStart>";
        XMLData += CustomDatesClosedDate_StartDate.Text;
        XMLData += "</customDateRangeClosedDateStart>";

        XMLData += "<customDateRangeClosedDateEnd>";
        XMLData += CustomDatesClosedDate_EndDate.Text;
        XMLData += "</customDateRangeClosedDateEnd>";


        XMLData += "<DisplayResultsBy>";
        XMLData += DisplayResutlsbyDD.SelectedItem.Value;
        XMLData += "</DisplayResultsBy>";

        XMLData += "</root>";



        ///////////////////////////////////////////////

        return XMLData;
    }
   
    private string getSQL(){



        string SelectedFormIDs = string.Empty;
        foreach (ListItem li in RequestType.Items)
        {
            if (li.Selected == true)
            {
                SelectedFormIDs += li.Value + ",";
            }

        }
        string sqlFormIDs = SelectedFormIDs.TrimEnd(',');
 



        string status = RequestStatusDD.SelectedValue; // commonfunctions.getFieldValue(XMLDoc, "RequestStatus");
        string SelectedFormStatus = getSelectedStatus(status);


        string SelectedSubmittedDate = "";
        string checkSubmittedDate = string.Empty; // commonfunctions.getFieldValue(XMLDoc, "checkSubmittedDate");

        if (LimitSubmittedDateRange.Checked == true)
        {
            checkSubmittedDate = "true";
        }
        else
        {
            checkSubmittedDate = "false";
        }

        string customDateRangeSubmittedDateStart = CustomDatesClosedDate_StartDate.Text; // commonfunctions.getFieldValue(XMLDoc, "customDateRangeSubmittedDateStart");

        string customDateRangeSubmittedDateEnd = CustomDatesClosedDate_EndDate.Text;// commonfunctions.getFieldValue(XMLDoc, "customDateRangeSubmittedDateEnd");

        if (checkSubmittedDate == "true")
        {
            SubmittedDate = RequestSubmittedDateDD.SelectedItem.Value; //commonfunctions.getFieldValue(XMLDoc, "SubmittedDate");
            SelectedSubmittedDate = Reports.getSelectedDateSql("CAST([SubmissionDateandTime] AS DATE)", SubmittedDate, customDateRangeSubmittedDateStart, customDateRangeSubmittedDateEnd);
           
            // BuildAllSQL = SelectedFormStatus;
        }



        string SelectedClosedDate = "";
        string ClosedDate = RequestClosedDateDD.SelectedItem.Value; //commonfunctions.getFieldValue(XMLDoc, "ClosedDate");
        string checkClosedDate = string.Empty; // RequestClosedDateDD.SelectedItem.Value; //commonfunctions.getFieldValue(XMLDoc, "checkClosedDate");
        if (limitClosedDate.Checked == true)
        {
            checkClosedDate = "true";
        }
        else
        {
            checkClosedDate = "false";
        }
        string customDateRangeClosedDateStart = CustomDatesClosedDate_StartDate.Text; // commonfunctions.getFieldValue(XMLDoc, "customDateRangeClosedDateStart");

        string customDateRangeClosedDateEnd = CustomDatesClosedDate_EndDate.Text; //commonfunctions.getFieldValue(XMLDoc, "customDateRangeClosedDateEnd");
        if (checkClosedDate == "true")
        {

            //SelectedClosedDate = Reports.getSelectedDateSql("CAST([ModifiedDateTime] AS DATE)", ClosedDate, customDateRangeClosedDateStart, customDateRangeClosedDateEnd);


            //messageDate = " Closed Date " + ClosedDate;
            //if (customDateRangeClosedDateStart != "")
            //{
            //    messageDate += " ( " + customDateRangeClosedDateStart + " " + customDateRangeClosedDateEnd + " )";
            //}

            BuildAllSQL = " where LTRIM(RTRIM(FormStatus)) in ('Closed' , 'Approved-Closed','Declined-Closed') "; 
        }
        else
        {

            if (SelectedSubmittedDate != "")
            {
                BuildAllSQL += " where " + SelectedSubmittedDate + " ";
            }
        }


        string formIDDBs = sqlFormIDs; // commonfunctions.getFieldValue(XMLDoc, "FormIDs");
        if (formIDDBs != "")
        {
            BuildAllSQL += " and FormID in (" + formIDDBs + ") ";
        }


        // string sqlOrderBy = commonfunctions.getFieldValue(XMLDoc, "DisplayResultsBy");
        DisplayResultsBy = DisplayResutlsbyDD.SelectedItem.Value; // commonfunctions.getFieldValue(XMLDoc, "DisplayResultsBy");


        BuildAllSQL += SelectedFormStatus;



        return BuildAllSQL;

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
    protected void ExcelClick_Click(object sender, EventArgs e)
    {



        SqlCommand cmd = new SqlCommand("HelpRequests_Reports_By");
        cmd.Parameters.AddWithValue("@BuildAllSQL", getSQL());
        cmd.Parameters.AddWithValue("@DisplayBy", DisplayResutlsbyDD.SelectedValue);

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

    public string customDatSDDisplay { get; set; }

    public string customDatCDDisplay { get; set; }

    public string myCsv { get; set; }

    public string SubmittedDate { get; set; }
}