using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;
using System.Collections;

public partial class admin_reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        RunMEssage = Reports.getLastRunDate("3");


        //update last run date
        Reports.RecordRunBy("3");


        Dictionary<string, string> getSchedulingMessage = new Dictionary<string, string>();

        getSchedulingMessage = Reports.ReportSchedule2("3");
        SchedulingMessage = getSchedulingMessage["Message"];
        scheduledreportid = getSchedulingMessage["ID"];
        SchedulingMessageTag = getSchedulingMessage["UpdateCreate"]; 

        //DataTableReader dtr = DataBase.dbDataTable("Select * from ReportSchedule where reportid=3").CreateDataReader();
        //if (dtr.HasRows)
        //{
        //    dtr.Read();
        //    string[] allRecepients = dtr["EmailAddress"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        //    SchedulingMessage = "This report is schedule to run on the " + dtr["RunFrequency"] + " and goes to " + allRecepients.Length + " recipients.";
        //    SchedulingMessageTag = "Update";
        //}
        //else
        //{
        //    SchedulingMessage = "No scheduled reports.";
        //    SchedulingMessageTag = "Create";
        //}
 
       
        dtResults.Columns.Add("RequestType");
        dtResults.Columns.Add("LastYearFull");
        dtResults.Columns.Add("LastYeartoDate");
        dtResults.Columns.Add("thisYeartoDate");
        dtResults.Columns.Add("percentage");
        


        Dictionary<string, string> AccessToResourceForm = getResults("AccessToResourceForm");
        Dictionary<string, string> ResearchAssistanceForm = getResults("ResearchAssistanceForm");
        Dictionary<string, string> PasswordAssistanceForm = getResults("PasswordAssistanceForm");
        Dictionary<string, string> RequestForAnArticle = getResults("RequestForAnArticle");
        Dictionary<string, string> SuggestingNewResourceForm = getResults("SuggestingNewResourceForm");
        Dictionary<string, string> TrainingRequestForm = getResults("TrainingRequestForm");
        Dictionary<string, string> ReportingProblemForm = getResults("ReportingProblemForm");
        Dictionary<string, string> OtherForm = getResults("OtherForm");

        long ALLH_LastYearFull = long.Parse(AccessToResourceForm["LastYearFull"]) + long.Parse(ResearchAssistanceForm["LastYearFull"]) + long.Parse(PasswordAssistanceForm["LastYearFull"]) + long.Parse(RequestForAnArticle["LastYearFull"]) +
            long.Parse(SuggestingNewResourceForm["LastYearFull"]) + long.Parse(TrainingRequestForm["LastYearFull"]) + long.Parse(ReportingProblemForm["LastYearFull"]) + long.Parse(OtherForm["LastYearFull"]);

        long ALLH_LastYeartoDate = long.Parse(AccessToResourceForm["LastYeartoDate"]) + long.Parse(ResearchAssistanceForm["LastYeartoDate"]) + long.Parse(PasswordAssistanceForm["LastYeartoDate"]) + long.Parse(RequestForAnArticle["LastYeartoDate"]) +
           long.Parse(SuggestingNewResourceForm["LastYeartoDate"]) + long.Parse(TrainingRequestForm["LastYeartoDate"]) + long.Parse(ReportingProblemForm["LastYeartoDate"]) + long.Parse(OtherForm["LastYeartoDate"]);

        long ALLH_thisYeartoDate = long.Parse(AccessToResourceForm["thisYeartoDate"]) + long.Parse(ResearchAssistanceForm["thisYeartoDate"]) + long.Parse(PasswordAssistanceForm["thisYeartoDate"]) + long.Parse(RequestForAnArticle["thisYeartoDate"]) +
           long.Parse(SuggestingNewResourceForm["thisYeartoDate"]) + long.Parse(TrainingRequestForm["thisYeartoDate"]) + long.Parse(ReportingProblemForm["thisYeartoDate"]) + long.Parse(OtherForm["thisYeartoDate"]);

        string ALLH_percentage = AdminFunc.CalculatePercentage(ALLH_LastYeartoDate, ALLH_thisYeartoDate);
        
        dtResults.Rows.Add("All Requests", ALLH_LastYearFull.ToString("N0"), ALLH_LastYeartoDate.ToString("N0"), ALLH_thisYeartoDate.ToString("N0"), ALLH_percentage); 
        dtResults.Rows.Add("e-Password", AccessToResourceForm["LastYearFull"], AccessToResourceForm["LastYeartoDate"], AccessToResourceForm["thisYeartoDate"], AccessToResourceForm["percentage"]); 
        dtResults.Rows.Add("Research Assistance", ResearchAssistanceForm["LastYearFull"], ResearchAssistanceForm["LastYeartoDate"], ResearchAssistanceForm["thisYeartoDate"], ResearchAssistanceForm["percentage"]);    
        dtResults.Rows.Add("Password Assistance", PasswordAssistanceForm["LastYearFull"], PasswordAssistanceForm["LastYeartoDate"], PasswordAssistanceForm["thisYeartoDate"], PasswordAssistanceForm["percentage"]); 
        dtResults.Rows.Add("Request For An Article", RequestForAnArticle["LastYearFull"], RequestForAnArticle["LastYeartoDate"], RequestForAnArticle["thisYeartoDate"], RequestForAnArticle["percentage"]);  
        dtResults.Rows.Add("Suggesting New Resource", SuggestingNewResourceForm["LastYearFull"], SuggestingNewResourceForm["LastYeartoDate"], SuggestingNewResourceForm["thisYeartoDate"], SuggestingNewResourceForm["percentage"]);         
        dtResults.Rows.Add("Training Request", TrainingRequestForm["LastYearFull"], TrainingRequestForm["LastYeartoDate"], TrainingRequestForm["thisYeartoDate"], TrainingRequestForm["percentage"]);      
        dtResults.Rows.Add("Reporting Problem", ReportingProblemForm["LastYearFull"], ReportingProblemForm["LastYeartoDate"], ReportingProblemForm["thisYeartoDate"], ReportingProblemForm["percentage"]);    
        dtResults.Rows.Add("Other", OtherForm["LastYearFull"], OtherForm["LastYeartoDate"], OtherForm["thisYeartoDate"], OtherForm["percentage"]);
                
        resultsLV.DataSource = dtResults;
        resultsLV.DataBind();

    }
    public DataTable dtResults = new DataTable();
    public Dictionary<string, string> getResults(string tableName)
    {


        Dictionary<string, string> returnedArray = new Dictionary<string, string>();

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
        string sql = " select count(*)   from " + tableName;
        string sqlLastYearFull = sql + " where (SubmissionDateandTime between '" + LastYearFY_Start + "' and '" + LastYearFY_End + "') ";
        string sqlLastYeartoDate = sql + " where (SubmissionDateandTime between '" + LastYearFY_Start + "' and '" + thisTimeLastYear + "') ";
        string sqlthisYeartoDate = sql + " where (SubmissionDateandTime between '" + thisYearFY_Start + "' and '" + toDate + "') ";


        string LastYearFull = DataBase.returnOneValue(sqlLastYearFull);
        string LastYeartoDate = DataBase.returnOneValue(sqlLastYeartoDate);
        string thisYeartoDate = DataBase.returnOneValue(sqlthisYeartoDate);

        string percentage = AdminFunc.CalculatePercentage(long.Parse(LastYeartoDate), long.Parse(thisYeartoDate));

        returnedArray.Add("LastYearFull", long.Parse(LastYearFull).ToString("N0"));
        returnedArray.Add("LastYeartoDate", long.Parse(LastYeartoDate).ToString("N0"));
        returnedArray.Add("thisYeartoDate", long.Parse(thisYeartoDate).ToString("N0"));
        returnedArray.Add("percentage", percentage);

        return returnedArray;
    }


    protected void ExcelClick_Click(object sender, EventArgs e)
    {
        CsvExport myExport = new CsvExport();
        ArrayList columns = new ArrayList();
        DataTableReader dtr = dtResults.CreateDataReader();

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

        string attachment = "attachment; filename=AdHocHelpRequestsReports-data-" + DateTime.Now.ToShortDateString() + ".csv";
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

    public string RunMEssage { get; set; }

    public string scheduledreportid { get; set; }
}