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
            WhereFrom.Value = Request.UrlReferrer.ToString();
            DataTableReader dtr = DataBase.dbDataTable("Select * from Report  where ID=" + reportID).CreateDataReader();
            if (dtr.HasRows)
            {
                dtr.Read();
                ReportName = dtr["Name"].ToString().Trim();
                ReportType = dtr["ReportType"].ToString().Trim();

            }

      //  }
    }





    public string reportID { get; set; }
    protected void Send_Click(object sender, EventArgs e)
    {
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


        //lets send this email
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

        string emailSendTo = emails.Text;
        string EmailMessage = "Hello , <br> Here is a link to view " + ReportName + "<br/> " + url;
        if (commonfunctions.Environment == "DEV")
        {
            emailSendTo = commonfunctions.devEnvironmentEmails;
        }
        commonfunctions.sendEmailMessage(emailSendTo, "Digital.Library.NoReply@ssa.gov", ReportName, EmailMessage);

        EmailSent.Visible = true;

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

    public string ReportName { get; set; }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        
        Response.Redirect(WhereFrom.Value);

    }

public  string ReportType { get; set; }}