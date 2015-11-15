using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;
using System.Data.SqlClient;
public partial class admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string AllMyRequestSQL = "SELECT FormStatus FROM dbo.AccessToResourceForm INNER JOIN dbo.Resources ON LTRIM(RTRIM(dbo.AccessToResourceForm.ResourceToAccess)) = dbo.Resources.ID where FormStatus in ('New','Open','Approved-Not-Notified','Declined-Not-Notified') and (dbo.Resources.SendEpasswordTo LIKE N'%," + myAdminID + ",%')";

        DataTable dtt = DataBase.dbDataTable(AllMyRequestSQL);
        myNewRequestsCount = dtt.Select("FormStatus in ('New','Open')").Length.ToString();
        myOutstandingRequestsCount = dtt.Select("FormStatus in ('Approved-Not-Notified','Declined-Not-Notified')").Length.ToString();


        string AllRequestSQL = "SELECT FormStatus FROM dbo.AccessToResourceForm INNER JOIN dbo.Resources ON LTRIM(RTRIM(dbo.AccessToResourceForm.ResourceToAccess)) = dbo.Resources.ID where FormStatus in ('New','Open','Approved-Not-Notified','Declined-Not-Notified')";

        DataTable dtts = DataBase.dbDataTable(AllRequestSQL);
        AllIncomingRequestsCount = dtts.Select("FormStatus in ('New','Open')").Length.ToString();
        AllOutstandingRequestsCount = dtts.Select("FormStatus in ('Approved-Not-Notified','Declined-Not-Notified')").Length.ToString();

        //myNewRequestsCount = DataBase.returnOneValue(newRequestSQL);

        //string newRequestSQL = "SELECT count(*) FROM dbo.AccessToResourceForm_SSADL INNER JOIN dbo.Resources_SSADL ON LTRIM(RTRIM(dbo.AccessToResourceForm_SSADL.ResourceToAccess)) = dbo.Resources_SSADL.ID where FormStatus in ('New','Open') and (dbo.Resources_SSADL.SendEpasswordTo LIKE N'%," + myAdminID + ",%')";
        //myNewRequestsCount = DataBase.returnOneValue(newRequestSQL);

        /////////////Contract div
        contractsExpiring = "";
        string AllContractSQL = "Select *  from ViewResourceContract where (DiffDate = NotifyOfExpirationThisManyDaysInAdvance or DiffDate < NotifyOfExpirationThisManyDaysInAdvance and DiffDate > 0) ";
        DataTableReader dtr = DataBase.dbDataTable(AllContractSQL).CreateDataReader();
        if (dtr.HasRows)
        {
            ContractPanel.Visible = true;
            while (dtr.Read())
            {
                string expiringDate = Convert.ToDateTime(dtr["PeriodofPerformanceEnd"]).ToString("M/dd/yy");
                contractExpiringCount++;
                contractsExpiring += " <li><a href='/admin/resources/view.aspx?resourceid=" + dtr["ResourceID"].ToString() + "'>" + dtr["ResourceName"].ToString() + "</a>";
                contractsExpiring += "  <span>(expires " + expiringDate + ")</span>";
                contractsExpiring += "</li>";

            }
        }



        ////////////////////////Help requests



        try
        {
            SqlCommand cmd = new SqlCommand("ReturnAllHelpRequestsCount");
            cmd.Parameters.AddWithValue("@userID", myAdminID);

            DataTableReader dtrr = DataBase.executeStoreProcudure(cmd).CreateDataReader();
            if (dtrr.HasRows)
            {
                dtrr.Read();
                allNewRequestsCount = dtrr["allNewRequestsCount"].ToString();
                allOutStandingRequestsCount = dtrr["allOutStandingRequestsCount"].ToString();
                myNewRequestsCount = dtrr["myNewRequestsCount"].ToString();
                myOutStandingRequestsCount = dtrr["myOutStandingRequestsCount"].ToString();
            }

            /////Epass
            //New for me
            string sqlEpass = "SELECT count(*)    FROM dbo.AccessToResourceForm INNER JOIN  dbo.Resources ON LTRIM(RTRIM(dbo.AccessToResourceForm.ResourceToAccess)) = dbo.Resources.ID  where  FormStatus in ('New')   and (dbo.Resources.SendEpasswordTo LIKE N'%," + myAdminID + ",%')  ";
            MyNewEpassCount = DataBase.returnOneValue(sqlEpass);
            //outstanding for me
            sqlEpass = "SELECT count(*)    FROM dbo.AccessToResourceForm INNER JOIN  dbo.Resources ON LTRIM(RTRIM(dbo.AccessToResourceForm.ResourceToAccess)) = dbo.Resources.ID  where  FormStatus in ('Approved-Not-Notified','Declined-Not-Notified','Open')   and (dbo.Resources.SendEpasswordTo LIKE N'%," + myAdminID + ",%')  ";
            MyOutEpassCount = DataBase.returnOneValue(sqlEpass);

            //new all
            sqlEpass = "SELECT count(*)    FROM dbo.AccessToResourceForm INNER JOIN  dbo.Resources ON LTRIM(RTRIM(dbo.AccessToResourceForm.ResourceToAccess)) = dbo.Resources.ID  where  FormStatus in ('New')   ";
            AllNewEpassCount = DataBase.returnOneValue(sqlEpass);

            //outstading all
            sqlEpass = "SELECT count(*)    FROM dbo.AccessToResourceForm INNER JOIN  dbo.Resources ON LTRIM(RTRIM(dbo.AccessToResourceForm.ResourceToAccess)) = dbo.Resources.ID  where  FormStatus in ('Approved-Not-Notified','Declined-Not-Notified','Open') ;   ";
            AllOutEpassCount = DataBase.returnOneValue(sqlEpass);

        }
        catch { }




        //////////////////////////////////////// Reports



        // string AllReportsSQL = "Select top 3 *  from ReportInstances where RunbyPIN ='" + loginSSA.myPIN + "' order by DateTimRun desc ";


        string AllReportsSQL = "  SELECT        TOP (3) dbo.ReportInstances.ID, dbo.ReportInstances.ReportID, dbo.ReportInstances.DateTimRun, dbo.ReportInstances.RunbyPIN, ";
        AllReportsSQL += "    dbo.ReportInstances.DisplayName, dbo.Report.Name, dbo.Report.ReportType ";
        AllReportsSQL += " FROM            dbo.ReportInstances INNER JOIN ";
        AllReportsSQL += "   dbo.Report ON dbo.ReportInstances.ReportID = dbo.Report.ID  where RunbyPIN ='" + loginSSA.myPIN + "' order by DateTimRun desc ";
        //Response.Write(AllReportsSQL);
        DataTableReader dtrS = DataBase.dbDataTable(AllReportsSQL).CreateDataReader();
        if (dtrS.HasRows)
        {

            while (dtrS.Read())
            {
                string runDate = Convert.ToDateTime(dtrS["DateTimRun"]).ToString("M/d/yy @ h:mm tt");
                string name = dtrS["Name"].ToString().Trim();
                string ReportType = dtrS["ReportType"].ToString().Trim();
                string ReportIDDB = dtrS["ReportID"].ToString().Trim();
                string link = string.Empty;

                switch (ReportType)
                {
                    case "Ad Hoc Contract Report":
                        link = "/admin/reports/resources/contracts.aspx?reportId=" + ReportIDDB + "#sb=0";
                        break;

                    case "Ad Hoc Resource Report":
                        link = "/admin/reports/resources/adhocresource.aspx?reportId=" + ReportIDDB + "#sb=0";
                        break;

                    case "Ad Hoc Help Report":
                        link = "/admin/reports/helprequests/AdHocHelpReports.aspx?reportid=" + ReportIDDB + "#sb=1";
                        break;

                    case "Unique Visitors and Total Hits Reports":
                        link = "/admin/reports/resources/#sb=0";
                        break;

                    case "Total Help Requests":
                        link = "/admin/reports/helprequests/#sb=1";
                        break;

                    case "Clicks per Resource":
                        link = "/admin/reports/resources/clicksperresource.aspx#sb=0";
                        break;


                }
                // Response.Write(link);
                reportMessage += "<p><a href='" + link + "'>" + name + "</a>";
                reportMessage += "<span>" + runDate + "</span></p>";

            }
        }

    }

    public string MyNewEpassCount { get; set; }
    public string MyOutEpassCount { get; set; }
    public string AllNewEpassCount { get; set; }
    public string AllOutEpassCount { get; set; }

    public string myNewRequestsCount { get; set; }

    public string myOutstandingRequestsCount { get; set; }
    public string AllIncomingRequestsCount { get; set; }

    public string AllOutstandingRequestsCount { get; set; }

    public virtual string myAdminID
    {
        get
        {
            if (ViewState["myAdminID"] == null)
            {
                ViewState["myAdminID"] = AdminFunc.getUserDBID(loginSSA.myPIN);
            }
            return (string)ViewState["myAdminID"];
        }
        set
        {
            ViewState["myAdminID"] = value;
        }
    }


    public int contractExpiringCount { get; set; }

    public string contractsExpiring { get; set; }
    public string allOutStandingRequestsCount { get; set; }

    public string allNewRequestsCount { get; set; }

    public string myOutStandingRequestsCount { get; set; }

    public string reportMessage { get; set; }
}