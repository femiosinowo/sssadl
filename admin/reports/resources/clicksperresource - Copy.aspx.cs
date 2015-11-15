using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;

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
        RunMEssage = Reports.getLastRunDate("2");

        Reports.RecordRunBy("2");
        if (!loginSSA.isAdminUser()) SideNav.Visible = false;

        DataTableReader dtr = DataBase.dbDataTable("Select * from ReportSchedule where reportid='2'").CreateDataReader();
        if (dtr.HasRows)
        {
            dtr.Read();
            scheduledreportid = dtr["ID"].ToString();
            string[] allRecepients = dtr["EmailAddress"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            SchedulingMessage = "This report is schedule to run on the " + dtr["RunFrequency"] + " and goes to " + allRecepients.Length + " recipients";
            SchedulingMessageTag = "Update";
        }
        else
        {
            SchedulingMessage = "No scheduled reports";
            SchedulingMessageTag = "Create";
        }

        //        select count(DISTINCT  ClickedByPIN) as uniqueVisitors , count(*) as NumberofHits    from [ClickTracking] where ClickedDateTime between '10/1/2013' and '09/30/2014' and resource > 0
        //select count(DISTINCT  ClickedByPIN) as uniqueVisitors , count(*) as NumberofHits    from [ClickTracking] where ClickedDateTime between '10/1/2013' and '09/30/2015' and resource > 0
        /////select count(DISTINCT  ClickedByPIN) as uniqueVisitors , count(*) as NumberofHits    from [ClickTracking] where ClickedDateTime between '10/1/2014' and '09/30/2015' and resource > 0


        //i need to know current FY
        //2014, which began on October 1, 2013 and ended on September 30, 2014.  FY14(2013,10,1 - 2014,09,30) FY15(2014,10,1 - 2015,9,30)
          

        int thisYear = AdminFunc.CurrentFiscalYear(DateTime.Now); // DateTime.Now.Year;
        int lastYear = thisYear - 1;
        int last2Year = lastYear - 1;

        lastYearDisplay = lastYear.ToString().Substring(2, 2);
        thisYearDisplay = thisYear.ToString().Substring(2, 2);

        string LastYearFY_Start = "10/1/" + last2Year.ToString();
        string LastYearFY_End = "09/30/" + lastYear.ToString();

        string thisYearFY_Start = "10/1/" + lastYear.ToString();
        string thisYearFY_End = "09/30/" + thisYear.ToString();

        string toDate = DateTime.Now.ToString("d");

        string thisTimeLastYear = DateTime.Now.AddYears(-1).ToString("d");

        string sql = " select count(DISTINCT  ClickedByPIN) as uniqueVisitors , count(*) as NumberofHits    from [ClickTracking] where   resource  > 0 ";
        string sqlLastYearFull = sql + " and (ClickedDateTime between '" + LastYearFY_Start + "' and '" + LastYearFY_End + "') ";
        string sqlLastYeartoDate = sql + " and (ClickedDateTime between '" + LastYearFY_Start + "' and '" + thisTimeLastYear + "') ";
        string sqlthisYeartoDate = sql + " and (ClickedDateTime between '" + thisYearFY_Start + "' and '" + toDate + "') ";


        DataTableReader dtrLastYearFull = DataBase.dbDataTable(sqlLastYearFull).CreateDataReader();
        DataTableReader dtrLastYeartoDate = DataBase.dbDataTable(sqlLastYeartoDate).CreateDataReader();
        DataTableReader dtrthisYeartoDate = DataBase.dbDataTable(sqlthisYeartoDate).CreateDataReader();

        dtrLastYearFull.Read();
        dtrLastYeartoDate.Read();
        dtrthisYeartoDate.Read();

        uniqueVistor_LastYearFull = dtrLastYearFull["uniqueVisitors"].ToString();
        NumberofHits_LastYearFull = dtrLastYearFull["NumberofHits"].ToString();


        uniqueVistor_LastYeartoDate = dtrLastYeartoDate["uniqueVisitors"].ToString();
        NumberofHits_LastYeartoDate = dtrLastYeartoDate["NumberofHits"].ToString();

        uniqueVistor_thisYeartoDate = dtrthisYeartoDate["uniqueVisitors"].ToString();
        NumberofHits_thisYeartoDate = dtrthisYeartoDate["NumberofHits"].ToString();

        percentDetails_uniqueVisitor = AdminFunc.CalculatePercentage(long.Parse(uniqueVistor_LastYeartoDate), long.Parse(uniqueVistor_thisYeartoDate));
        percentDetails_NumberofHits = AdminFunc.CalculatePercentage(long.Parse(NumberofHits_LastYeartoDate), long.Parse(NumberofHits_thisYeartoDate));


    }
    protected void ExcelClick_Click(object sender, EventArgs e)
    {
        CsvExport myExport = new CsvExport();
        //   Then you can do any of the following three output options:
        myExport.AddRow();
        myExport["Statistic"] = "Unique Visitors";
        myExport["FY" + lastYearDisplay + "(Full)"] = long.Parse(uniqueVistor_LastYearFull).ToString("N0");
        myExport["FY" + lastYearDisplay + "(to Date)"] = long.Parse(uniqueVistor_LastYeartoDate).ToString("N0");
        myExport["FY" + thisYearDisplay + "(to Date)"] = long.Parse(uniqueVistor_thisYeartoDate).ToString("N0");
        myExport["% Difference to Date"] = percentDetails_uniqueVisitor;

        myExport.AddRow();
        myExport["Statistic"] = "Number of Hits";
        myExport["FY" + lastYearDisplay + "(Full)"] = long.Parse(NumberofHits_LastYearFull).ToString("N0");
        myExport["FY" + lastYearDisplay + "(to Date)"] = long.Parse(NumberofHits_LastYeartoDate).ToString("N0");
        myExport["FY" + thisYearDisplay + "(to Date)"] = long.Parse(NumberofHits_thisYeartoDate).ToString("N0");
        myExport["% Difference to Date"] = percentDetails_NumberofHits;

        myCsv = myExport.Export();

        string attachment = "attachment; filename=uniqueVisitors-data-" + DateTime.Now.ToShortDateString() + ".csv";
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

    public string scheduledreportid { get; set; }
    public string RunMEssage { get; set; }
}