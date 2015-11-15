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

        //if (!IsPostBack)
        //{
        reportID = Request.QueryString["reportid"].ToString();
        DataTableReader dtrSch = DataBase.dbDataTable("Select * from ReportSchedule  where reportID=" + reportID).CreateDataReader();
        if (dtrSch.HasRows)
        {
            dtrSch.Read();
            string EmailAddress = dtrSch["EmailAddress"].ToString().Trim();


            DataTableReader dtr = DataBase.dbDataTable("Select * from Report  where ID=" + reportID).CreateDataReader();
            if (dtr.HasRows)
            {
                dtr.Read();
                ReportName = dtr["Name"].ToString().Trim();
                ReportType = dtr["ReportType"].ToString().Trim();
                string url = ""; // commonfunctions.host + "/admin/reports/viewReports.aspx?reportid=" + reportID;


                switch (ReportType)
                {
                    case "Ad Hoc Contract Report":
                        url = commonfunctions.host + "/admin/reports/resources/contracts.aspx?reportId=" + reportID;
                        break;
                    case "Ad Hoc Resource Report":
                        url = commonfunctions.host + "/admin/reports/resources/adhocresource.aspx?reportId=" + reportID;
                        break;
                    case "Ad Hoc Help Report":
                        url = commonfunctions.host + "/admin/reports/helprequests/AdHocHelpReports.aspx?reportid=" + reportID;
                        break;

                    case "Unique Visitors and Total Hits Reports":
                        url = commonfunctions.host + "/admin/reports/resources/";
                        break;
                    case "Total Help Requests":
                        url = commonfunctions.host + "/admin/reports/helprequests/";
                        break;
                    case "Clicks per Resource":
                        url = commonfunctions.host + "/admin/reports/resources/clicksperresource.aspx";
                        break;


                }

                string emailSendTo = EmailAddress;
                string EmailMessage = "Hello , <br> Here is a link to view " + ReportName + "<br/> " + url;
                if (commonfunctions.Environment == "DEV" || commonfunctions.Environment == "PROD")
                {
                    commonfunctions.sendEmailMessage(emailSendTo, "Digital.Library.NoReply@ssa.gov", ReportName, EmailMessage);
                }
                //EmailSent.Visible = true;
            }

        }
    }
    
    public string reportID { get; set; }
    public string invalidEmailsFound { get; set; }
    public string ReportName { get; set; }
    public string ReportType { get; set; }
}