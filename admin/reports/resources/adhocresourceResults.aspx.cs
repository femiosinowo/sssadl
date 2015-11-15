using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;
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

        if (Request.QueryString["reportid"] != null)
        {
            reportID = Request.QueryString["reportid"].ToString();
            //   getReport(reportID);
            // RunReport.Visible = false;
            //SaveReport.Visible = true;
            checkIfOnSchdule(reportID);
            Dictionary<string, string> getSchedulingMessage = new Dictionary<string, string>();

            getSchedulingMessage = Reports.ReportSchedule2(reportID);
            SchedulingMessage = getSchedulingMessage["Message"];
            scheduledreportid = getSchedulingMessage["ID"];
            SchedulingMessageTag = getSchedulingMessage["UpdateCreate"]; 

            getExistingData(reportID);

            RunMEssage = Reports.getLastRunDate(reportID);
            //update last run date
          

            Reports.RecordRunBy(reportID);

            //Response.Write(ReportSQLStatement);
            ResultsGridView.DataSource = DataBase.dbDataTable(ReportSQLStatement);
            ResultsGridView.DataBind();
            if (ResultsGridView.Rows.Count > 0)
            {
                ResultsGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            
        }

    }




     

    public virtual string ReportSQLStatement
    {
        get
        {
            if (ViewState["ReportSQLStatement"] == null)
            {
                ViewState["ReportSQLStatement"] = "";
            }
            return (string)ViewState["ReportSQLStatement"];
        }
        set
        {
            ViewState["ReportSQLStatement"] = value;
        }
    }

    

   

    private void getExistingData(string reportID)
    {
        DataTableReader dtr = DataBase.dbDataTable("Select * from report where ID='" + reportID + "'").CreateDataReader();

        if (dtr.HasRows)
        {
            dtr.Read();
            string xmldata = dtr["ReportXMLData"].ToString();
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.LoadXml(xmldata);


            string ShowDatafor = commonfunctions.getFieldValue(XMLDoc, "ShowDatafor");
            string AssociatedNetworkTxt = commonfunctions.getFieldValue(XMLDoc, "AssociatedNetwork");
            string Resources = commonfunctions.getFieldValue(XMLDoc, "Resources");
            string DateRangeTxt = commonfunctions.getFieldValue(XMLDoc, "DateRange");

            string StartDateTxt = commonfunctions.getFieldValue(XMLDoc, "StartDate");
            string EndDateTxt = commonfunctions.getFieldValue(XMLDoc, "EndDate");

            string DisplayDDDB = commonfunctions.getFieldValue(XMLDoc, "DisplayDD");
            string Display2DDDB = commonfunctions.getFieldValue(XMLDoc, "Display2DD");

            string DisplayByDDDB = commonfunctions.getFieldValue(XMLDoc, "DisplayByDD");

            string coreServicesAccessType = commonfunctions.getFieldValue(XMLDoc, "coreServicesAccessType");
            string coreServicesBreakOutDataByDD = commonfunctions.getFieldValue(XMLDoc, "coreServicesBreakOutDataByDD");
            string coreServicesOfficeCode = commonfunctions.getFieldValue(XMLDoc, "coreServicesOfficeCode");
            string coreServicesUserDomain = commonfunctions.getFieldValue(XMLDoc, "coreServicesUserDomain");
            string coreServicesCoreServicesOffice = commonfunctions.getFieldValue(XMLDoc, "coreServicesCoreServicesOffice");
            string coreServicesPIN = commonfunctions.getFieldValue(XMLDoc, "coreServicesPIN");
            string coreServicesEmail = commonfunctions.getFieldValue(XMLDoc, "coreServicesEmail");
            string coreServicesFirstName = commonfunctions.getFieldValue(XMLDoc, "coreServicesFirstName");
            string coreServicesLastName = commonfunctions.getFieldValue(XMLDoc, "coreServicesLastName");
            string coreServicesServer = commonfunctions.getFieldValue(XMLDoc, "coreServicesLastName");


            ReportSQLStatement = dtr["ReportSQLStatement"].ToString();
           //  Response.Write(ReportSQLStatement);
            DisplayParameters = "<i><font size='small'>Show Data for " + ShowDatafor;
            if (ShowDatafor == "")
            {
                DisplayParameters += " for associated network " + AssociatedNetworkTxt;
            }
            DisplayParameters += " for Resource(s) (" + Reports.getResourcesName(Resources) + ") <br/>";
            DisplayParameters += "Date Range: " + DateRangeTxt + " <br/> ";
            DisplayParameters += "Displaying " + DisplayDDDB + " for " + Display2DDDB;
            DisplayParameters += " sorted by " + DisplayByDDDB;
            ///advance field
            if (coreServicesBreakOutDataByDD != "") DisplayParameters += "<br/>Break out data by : " + coreServicesBreakOutDataByDD;
            if (coreServicesAccessType != "") DisplayParameters += "<br/>Access Type  : " + Reports.getAccessTypeName(coreServicesAccessType);
            if (coreServicesOfficeCode != "") DisplayParameters += "<br/>CoreServices Office Code  : " + coreServicesOfficeCode;
            if (coreServicesUserDomain != "") DisplayParameters += "<br/>CoreServices User Domain  : " + coreServicesUserDomain;
            if (coreServicesCoreServicesOffice != "") DisplayParameters += "<br/>CoreServices Office : " + coreServicesCoreServicesOffice;
            if (coreServicesPIN != "") DisplayParameters += "<br/>CoreServices PIN  : " + coreServicesPIN;
            if (coreServicesEmail != "") DisplayParameters += "<br/>CoreServices Email : " + coreServicesEmail;
            if (coreServicesFirstName != "") DisplayParameters += "<br/>CoreServices First Name : " + coreServicesFirstName;
            if (coreServicesLastName != "") DisplayParameters += "<br/>CoreServices Last Name : " + coreServicesLastName;
            if (coreServicesServer != "") DisplayParameters += "<br/>CoreServices Server : " + coreServicesServer;

            DisplayParameters += "</font>";
            DisplayParameters += "</i>";

            ChangeParameters.NavigateUrl = "/admin/reports/resources/adhocresource.aspx?reportid=" + reportID;

            //////////////////////////////////////////////////////////////////////////////////////////////// SQL ///////////////////////////////////////////////////////////

 

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


    protected void resultsLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;
            Literal ResourceName = (Literal)ditem.FindControl("ResourceName");
            Literal DateClicked = (Literal)ditem.FindControl("DateClicked");
            Literal PIN = (Literal)ditem.FindControl("PIN");
            Literal Name = (Literal)ditem.FindControl("Name");
            ResourceName.Text = item["ResourceName"].ToString();
            DateClicked.Text = Convert.ToDateTime(item["ClickedDateTime"].ToString()).ToString("d");
            PIN.Text = item["ClickedByPIN"].ToString();
            //string SubmissionDateandTime = item["SubmissionDateandTime"].ToString().Trim();
            //SubmissionDateandTimeTxt.Text = getDatabaseDateformat(SubmissionDateandTime);


            ////  Dictionary<string, string> userdetails = loginSSA.GetUsersDetails(requestorPIN);
            ////try
            ////{
            ////    SubmittedByPINTxt.Text = userdetails["LastName"] + " " + userdetails["FirstName"]; //getUserNameByPIN(item["SubmittedForPIN"].ToString().Trim());
            ////}
            ////catch { }
            //SubmittedByPINTxt.Text = item["SubmittedForLastName"] + " " + item["SubmittedForFirstName"];
            //// AssignedTo.Text = getResourceRequestsAdminUsers(item["RequestsSentTo"].ToString().Trim());
            //string Active = item["FormStatus"].ToString().Trim();
            //ActiveLit.Text = Active;


            //RequestType.Text = item["RequestType"].ToString().Trim();

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
        }
    }

    protected void ExcelClick_Click(object sender, EventArgs e)
    {
        CsvExport myExport = new CsvExport();
        ArrayList columns = new ArrayList();
        DataTableReader dtr = DataBase.dbDataTable(ReportSQLStatement).CreateDataReader();

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

    public string reportID { get; set; }

    public string DisplayParameters { get; set; }

    public string RunMEssage { get; set; }



    public string scheduledreportid { get; set; }
}