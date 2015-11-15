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
            customDatSDDisplay = " style='display:none' ";
            //dtAllResouces = getAllResouces();
            //ListResources.DataSource = dtAllResouces;
            //ListResources.DataBind();
            if (Request.QueryString["reportid"] != null)
            {
                defaultResourceLoad.Visible = false;
                reportID = Request.QueryString["reportid"].ToString();
                getExistingData(reportID);
                RunReport.Visible = false;
                SaveReport.Visible = true;

            }
            else
            {
                DateRange.Items.FindByValue("This Fiscal Year").Selected = true;
                CompareDataAgainst.Items.FindByValue("Prior FY").Selected = true;
            }
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


             string reportType = commonfunctions.getFieldValue(XMLDoc, "reportType");
             string compare = commonfunctions.getFieldValue(XMLDoc, "compare");
               Resources = commonfunctions.getFieldValue(XMLDoc, "Resources");
               ProcurementMethod = commonfunctions.getFieldValue(XMLDoc, "ProcurementMethod");
               ResourceTypeTaxonomy = commonfunctions.getFieldValue(XMLDoc, "ResourceTypeTaxonomy");
             string DateRangeTxt = commonfunctions.getFieldValue(XMLDoc, "DateRange");
             string StartDateTxt = commonfunctions.getFieldValue(XMLDoc, "StartDate");
             string EndDateTxt = commonfunctions.getFieldValue(XMLDoc, "EndDate");
             string CompareDataAgainstTxt = commonfunctions.getFieldValue(XMLDoc, "CompareDataAgainst");

             ReportTypeDD.Items.FindByValue(reportType).Selected = true;
             CompareDD.Items.FindByValue(compare).Selected = true;
             compareDisplay = compare;
            // getCompareDisplayData(compare);
            // Response.Write("<script>loadCompareResults('" + compare + "');</script>");


             selectedCompareData = compare;            
             switch (compare)
             {
                 case "Resources":

                     break;

                 case "Resources by Procurement Method":
                     compareEditDiv.Visible = true;
                     SelectedcompareID = ProcurementMethod;
                     break;
                 case "Resources by Type":
                     compareEditDiv.Visible = true;
                     SelectedcompareID = ResourceTypeTaxonomy;
                     break;

             }

            
                 
             DateRange.Items.FindByValue(DateRangeTxt).Selected = true;
             if (DateRangeTxt == "Custom Dates")
             {
                 StartDate.Text = StartDateTxt;
                 EndDate.Text = EndDateTxt;
                 customDatSDDisplay = " style='display:block' ";
             }
             else
             {
                 customDatSDDisplay = " style='display:none' ";
             }

             CompareDataAgainst.Items.FindByValue(CompareDataAgainstTxt).Selected = true;

             EditPanel.Visible = true;

         }

    }

    private void getCompareDisplayData(string compare)
    {
        ResourceCat = compare;
        selectedCompareData = compare;
        compareEditDiv.Visible = true;
        switch (compare)
        {
            case "Resources":
               
                break;

            case "Resources by Procurement Method":
                
                break;


            case "Resources by Type":
                
                break;




        }
    }



    //public DataTable dtAllResouces  ;
    //private DataTable getAllResouces()
    //{
    //    return DataBase.dbDataTable("Select * from Resources order by ResourceName");

    //}

    protected void RunReport_Click(object sender, EventArgs e)
    {
        string xmlData = getXML();

        string sql = " INSERT INTO [dbo].[Report]([Name],[CreatedbyPIN],[CreateDate],[LastModifiedDate],[LastRunDate],[ReportExecutionFile],[ReportSQLStatement],[ReportXMLData], ReportType) ";
        sql += "  VALUES(@Name,@CreatedbyPIN ,GETDATE() ,GETDATE() ,GETDATE(),@ReportExecutionFile ,@ReportSQLStatement ,@ReportXMLData, 'Ad Hoc Contract Report' ) ";

        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@Name", "Ad Hoc Help Report");
        sqlcmd.Parameters.AddWithValue("@CreatedbyPIN", loginSSA.myPIN);
        sqlcmd.Parameters.AddWithValue("@ReportExecutionFile", "");
        sqlcmd.Parameters.AddWithValue("@ReportSQLStatement", "");
        sqlcmd.Parameters.AddWithValue("@ReportXMLData", xmlData);
        string reportIDD = DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmd);
         Response.Redirect("/admin/reports/resources/contractsResults.aspx?reportid=" + reportIDD + "#sb=0");
         //Response.Write(Request.Form);
    }



    protected void SaveReport_Click(object sender, EventArgs e)
    {
        string XMLData = getXML();
        string reportID = Request.QueryString["reportid"].ToString();
        string sql = "UPDATE [dbo].[Report] SET [LastModifiedDate] = GETDATE()   ,[ReportXMLData] = @ReportXMLData  ";
        sql += " WHERE ID=" + reportID;
       // Response.Write(sql);
        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@ReportXMLData", XMLData);
        DataBase.executeCommandWithParameters(sqlcmd);
        Response.Redirect("/admin/reports/resources/contractsResults.aspx?reportid=" + reportID + "#sb=0");

    }


    private string getXML()
    {
        string xmlData = "<root>";

        string reportType = ReportTypeDD.SelectedValue;
        xmlData += "<reportType>" + reportType + "</reportType>";

        string compare = CompareDD.SelectedValue;
        xmlData += "<compare>" + compare + "</compare>";

      //  Response.Write(getResouces());

        string Resources = getResources();
        xmlData += "<Resources>" + Resources + "</Resources>";


        string ProcurementMethod = getValueFromForm("ProcurementMethod");
        xmlData += "<ProcurementMethod>" + ProcurementMethod + "</ProcurementMethod>";


        string ResourceTypeTaxonomy = getValueFromForm("ResourceTypeTaxonomy");
        xmlData += "<ResourceTypeTaxonomy>" + ResourceTypeTaxonomy + "</ResourceTypeTaxonomy>";


        string DateRangeTxt = DateRange.SelectedValue;
        xmlData += "<DateRange>" + DateRangeTxt + "</DateRange>";

       // string StartDate = getValueFromForm("ctl00_mainContentCP_StartDate");
        xmlData += "<StartDate>" + StartDate.Text + "</StartDate>";


       // string EndDate = getValueFromForm("ctl00_mainContentCP_EndDate");
        xmlData += "<EndDate>" + EndDate.Text + "</EndDate>";



        string CompareDataAgainstTxt = CompareDataAgainst.SelectedValue;
        xmlData += "<CompareDataAgainst>" + CompareDataAgainstTxt + "</CompareDataAgainst>";
        


        xmlData += "</root>";

        return xmlData;

    }

    private string getValueFromForm(string whatField)
    {
        try
        {
            return Request.Form[whatField].ToString();
        }
        catch
        {

            return "";
        }
    }

    private string getResources()
    {

        string AllSelectedResourceID = string.Empty;
        try
        {
            string[] ListResourcesDD = this.Request.Form.GetValues("ListResourcesDD");
            foreach (string resourceID in ListResourcesDD)
            {

                AllSelectedResourceID += resourceID + ",";
            }
        }
        catch { }

        return AllSelectedResourceID.TrimEnd(',');
    }




    /// <summary>
    /// ///////////////////////////////////
    /// 
    /// 
    /// 
    /// 
    /// </summary>




    DataTable dtResults = new DataTable();

    public string myCsv { get; set; }
    protected void DownloadReport_Click(object sender, EventArgs e)
    {
        StartDateTxt = StartDate.Text;
        EndDateTxt = EndDate.Text;
        string CompareDataAgainstTxt = CompareDataAgainst.SelectedValue;
        string DateRangeTxt = DateRange.SelectedValue;
        Resources = getResources();
        switch (ReportTypeDD.SelectedValue)
        {
            case "Comparative Resource Pricing":
                showComparativeResults(Resources, CompareDataAgainstTxt, DateRangeTxt);
                break;

            case "Pricing Over Time":
                showPricingOver(Resources, CompareDataAgainstTxt, DateRangeTxt);
                break;


            case "Procurement Method":
                showProcurementMethod(Resources, CompareDataAgainstTxt, DateRangeTxt);
                break;

            case "Contract Expiration":
                showExpirationContracts(Resources, CompareDataAgainstTxt, DateRangeTxt);
                break;
        }


        //ResultsGridView.DataSource = dtResults;


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

    private void showExpirationContracts(string Resources, string CompareDataAgainstTxt, string DateRangeTxt)
    {

        string sql = "";
        string sqlProir = string.Empty;

        Dictionary<string, DateTime> getSelectedDates = Reports.getSelectedDates(DateRangeTxt, StartDateTxt, EndDateTxt);
        DateTime StartDate = getSelectedDates["StartDate"];
        DateTime EndDate = getSelectedDates["EndDate"];
        DateTime PriorStartDate = getSelectedDates["StartDate"].AddYears(-1);
        DateTime PriorEndDate = getSelectedDates["EndDate"].AddYears(-1);

        string dateRangeSQL = " PeriodofPerformanceEnd  BETWEEN  '" + StartDate.ToString("d") + " 00:00:00'  and '" + EndDate.ToString("d") + " 23:59:59' ";
        string PriorDateSQL = " PeriodofPerformanceEnd  BETWEEN  '" + PriorStartDate.ToString("d") + " 00:00:00'  and '" + PriorEndDate.ToString("d") + " 23:59:59' ";
        DataTableReader dtrr;
        string sqlAllProinThisRange = "";

        switch (CompareDataAgainstTxt)
        {
            case "None":

                dtResults.Columns.Add("Resource Name");
                dtResults.Columns.Add("Performance End date");
                //    sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where ResourceID in (" + Resources + ") and  " + dateRangeSQL + " and ProcurementMethod is not null  group by [ProcurementMethod]  ";


                if (Resources != "")
                {
                    string[] ResourcesIDs = Resources.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string ResourcesID in ResourcesIDs)
                    {
                        string ResourceName = AdminFunc.getResourceName(ResourcesID);
                        sqlAllProinThisRange = "select ResourceID , PeriodofPerformanceEnd  from ResourcesContract where ResourceID in (" + ResourcesID + ") and  " + dateRangeSQL + " ";
                        dtrr = DataBase.dbDataTable(sqlAllProinThisRange).CreateDataReader();
                        while (dtrr.Read())
                        {
                            dtResults.Rows.Add(ResourceName, dtrr["PeriodofPerformanceEnd"].ToString());
                        }
                    }


                }
                else
                {

                    sqlAllProinThisRange = "select ResourceID , PeriodofPerformanceEnd  from ResourcesContract where   " + dateRangeSQL + " ";
                    dtrr = DataBase.dbDataTable(sqlAllProinThisRange).CreateDataReader();
                    while (dtrr.Read())
                    {
                        string ResourceName = AdminFunc.getResourceName(dtrr["ResourceID"].ToString());

                        dtResults.Rows.Add(ResourceName, dtrr["PeriodofPerformanceEnd"].ToString());
                    }
                }

                break;

            case "Prior FY":

                dtResults.Columns.Add("Resource Name");
                dtResults.Columns.Add("Selected End date");
                dtResults.Columns.Add("Prior End date");
                //    sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where ResourceID in (" + Resources + ") and  " + dateRangeSQL + " and ProcurementMethod is not null  group by [ProcurementMethod]  ";


                if (Resources != "")
                {
                    string[] ResourcesIDs = Resources.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string ResourcesID in ResourcesIDs)
                    {
                        string ResourceName = AdminFunc.getResourceName(ResourcesID);
                        sqlAllProinThisRange = "select ResourceID , PeriodofPerformanceEnd  from ResourcesContract where ResourceID in (" + ResourcesID + ") and  " + dateRangeSQL + " ";
                        dtrr = DataBase.dbDataTable(sqlAllProinThisRange).CreateDataReader();
                        while (dtrr.Read())
                        {
                            string PriorDateSQLU = "    ResourceID in (" + ResourcesID + ") and  " + PriorDateSQL;
                            dtResults.Rows.Add(ResourceName, dtrr["PeriodofPerformanceEnd"].ToString(), getExpirationData(PriorDateSQLU));
                        }
                    }


                }
                else
                {

                    sqlAllProinThisRange = "select ResourceID , PeriodofPerformanceEnd  from ResourcesContract where   " + dateRangeSQL + " ";
                    dtrr = DataBase.dbDataTable(sqlAllProinThisRange).CreateDataReader();
                    while (dtrr.Read())
                    {
                        string ResourceName = AdminFunc.getResourceName(dtrr["ResourceID"].ToString());

                        dtResults.Rows.Add(ResourceName, dtrr["PeriodofPerformanceEnd"].ToString(), getExpirationData(PriorDateSQL));
                    }
                }


                break;


            case "All Prior FY on Record":
                dtResults.Columns.Add("Resource Name");
                int intYear = 1;

                sql = "SELECT  DIstinct TOP 1 YEAR(PeriodofPerformanceEnd) as Year    FROM  dbo.ResourcesContract     where PeriodofPerformanceEnd < '" + EndDate.ToString("d") + "' and YEAR(PeriodofPerformanceEnd) !='1900' and ResourceID in (" + Resources + ")   order by Year asc";
                //Response.Write(sql);

                string stopAtYEar = DataBase.returnOneValue(sql);
                if (stopAtYEar != "")
                {

                    int HowManyColumnsToCreate = EndDate.Year - Convert.ToInt32(stopAtYEar);


                    dtResults.Columns.Add(EndDate.Year.ToString());


                    for (int ii = 1; ii <= HowManyColumnsToCreate; ii++)
                    {
                        string columnName = (EndDate.Year - ii).ToString();
                        dtResults.Columns.Add(columnName);




                    }



                    if (Resources != "")
                    {
                        string[] ResourcesIDs = Resources.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string ResourcesID in ResourcesIDs)
                        {
                            // AllResource.Add(AdminFunc.getResourceName(ResourcesID));
                            DataRow dra = dtResults.NewRow();
                            string PeriodofPerformanceEnd1 = " -- ";
                            dra["Resource Name"] = AdminFunc.getResourceName(ResourcesID);


                            sqlAllProinThisRange = "select  PeriodofPerformanceEnd  from ResourcesContract where ResourceID in (" + ResourcesID + ") and  " + dateRangeSQL + " ";
                            //Response.Write(sqlAllProinThisRange + "<br/>");
                            dtrr = DataBase.dbDataTable(sqlAllProinThisRange).CreateDataReader();
                            if (dtrr.HasRows)
                            {
                                dtrr.Read();
                                PeriodofPerformanceEnd1 = dtrr["PeriodofPerformanceEnd"].ToString(); ///////////First Data
                            }


                            dra[EndDate.Year.ToString()] = PeriodofPerformanceEnd1;


                            int loopStartDate = EndDate.Year - 1;

                            while (loopStartDate >= Convert.ToInt32(stopAtYEar))
                            {

                                PriorStartDate = getSelectedDates["StartDate"].AddYears(-intYear);
                                PriorEndDate = getSelectedDates["EndDate"].AddYears(-intYear);

                                string columnName = loopStartDate.ToString();
                                string PeriodofPerformanceEndDB = " -- ";

                                PriorDateSQL = " PeriodofPerformanceEnd  BETWEEN  '" + PriorStartDate.ToString("d") + " 00:00:00'  and '" + PriorEndDate.ToString("d") + " 23:59:59' ";
                                sql = "select PeriodofPerformanceEnd  from ResourcesContract   ";
                                sql += "  where ResourceID in (" + ResourcesID + ") and " + PriorDateSQL + "";
                                //    Response.Write(sql + "<br/>");
                                DataTableReader dtr = DataBase.dbDataTable(sql).CreateDataReader();
                                if (dtr.HasRows)
                                {
                                    dtr.Read();
                                    PeriodofPerformanceEndDB = dtr["PeriodofPerformanceEnd"].ToString();
                                    dra[columnName] = PeriodofPerformanceEndDB;
                                }
                                else
                                {
                                    dra[columnName] = "--";
                                }




                                loopStartDate--;
                                intYear++;

                            }


                            dtResults.Rows.Add(dra);
                        }

                    }
                }



                break;



        }


        dtResults = DataBase.sortDataTable(dtResults, "Resource Name", "ASC");
    }

    private string getExpirationData(string PriorDateSQL)
    {
        return DataBase.returnOneValue("select   PeriodofPerformanceEnd  from ResourcesContract where " + PriorDateSQL);
    }

    private void showProcurementMethod(string Resources, string CompareDataAgainstTxt, string DateRangeTxt)
    {
        string sql = "";
        string sqlProir = string.Empty;

        Dictionary<string, DateTime> getSelectedDates = Reports.getSelectedDates(DateRangeTxt, StartDateTxt, EndDateTxt);
        DateTime StartDate = getSelectedDates["StartDate"];
        DateTime EndDate = getSelectedDates["EndDate"];
        DateTime PriorStartDate = getSelectedDates["StartDate"].AddYears(-1);
        DateTime PriorEndDate = getSelectedDates["EndDate"].AddYears(-1);

        string dateRangeSQL = " PeriodofPerformanceStart  BETWEEN  '" + StartDate.ToString("d") + " 00:00:00'  and '" + EndDate.ToString("d") + " 23:59:59' ";
        string PriorDateSQL = " PeriodofPerformanceStart  BETWEEN  '" + PriorStartDate.ToString("d") + " 00:00:00'  and '" + PriorEndDate.ToString("d") + " 23:59:59' ";
        DataTableReader dtrr;
        string sqlAllProinThisRange = "";

        switch (CompareDataAgainstTxt)
        {
            case "None":

                dtResults.Columns.Add("Procurement Method");
                dtResults.Columns.Add("FY Cost");
                //    sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where ResourceID in (" + Resources + ") and  " + dateRangeSQL + " and ProcurementMethod is not null  group by [ProcurementMethod]  ";


                if (Resources != "")
                {
                    //  sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where ResourceID in (" + Resources + ") and PeriodofPerformanceStart BETWEEN '" + PriorStartDate.ToString("d") + " 00:00:00' and '" + EndDate.ToString("d") + " 23:59:59' group by [ProcurementMethod]  ";
                    sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where ResourceID in (" + Resources + ") and  " + dateRangeSQL + " and ProcurementMethod is not null  group by [ProcurementMethod]  ";
                }
                else
                {

                    sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where   " + dateRangeSQL + " and ProcurementMethod is not null  group by [ProcurementMethod]  ";
                }



                dtrr = DataBase.dbDataTable(sqlAllProinThisRange).CreateDataReader();
                while (dtrr.Read())
                {

                    string getFYCurrentProc = processProc(dtrr["ProcurementMethod"].ToString(), dateRangeSQL, Resources);
                    Double longthisFYCostNone;
                    if (!Double.TryParse(getFYCurrentProc, out longthisFYCostNone)) longthisFYCostNone = 0;



                    string procurementMethodName = commonfunctions.GetTaxonomyNameFromID(long.Parse(dtrr["ProcurementMethod"].ToString()));

                    dtResults.Rows.Add(procurementMethodName, longthisFYCostNone.ToString("N3"));
                }





                break;


            case "Prior FY":

                dtResults.Columns.Add("Procurement Method");
                dtResults.Columns.Add("Prior FY Cost");
                dtResults.Columns.Add("Current FY Cost");
                dtResults.Columns.Add("Dollar Difference");
                dtResults.Columns.Add("Percentage");


                if (Resources != "")
                {
                    sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where ResourceID in (" + Resources + ") and ProcurementMethod is not null and PeriodofPerformanceStart BETWEEN '" + PriorStartDate.ToString("d") + " 00:00:00' and '" + EndDate.ToString("d") + " 23:59:59' group by [ProcurementMethod]  ";
                }
                else
                {
                    sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where PeriodofPerformanceStart BETWEEN '" + PriorStartDate.ToString("d") + " 00:00:00' and '" + EndDate.ToString("d") + " 23:59:59'   and ProcurementMethod is not null group by [ProcurementMethod]  ";
                }
                dtrr = DataBase.dbDataTable(sqlAllProinThisRange).CreateDataReader();
                while (dtrr.Read())
                {

                    string getFYCurrentProc = processProc(dtrr["ProcurementMethod"].ToString(), dateRangeSQL, Resources);
                    string getFYProirProc = processProc(dtrr["ProcurementMethod"].ToString(), PriorDateSQL, Resources);


                    Double longthisFYCost;
                    if (!Double.TryParse(getFYCurrentProc, out longthisFYCost)) longthisFYCost = 0;
                    Double longpriorFYCost;
                    if (!Double.TryParse(getFYProirProc, out longpriorFYCost)) longpriorFYCost = 0;

                    Double DollarDiff = longthisFYCost - longpriorFYCost;

                    Double percnPriorCost = longpriorFYCost;
                    Double percnThisCost = longthisFYCost;

                    if (percnPriorCost == 0) percnPriorCost = 1;
                    if (percnThisCost == 0) percnThisCost = 1;

                    string percentage = AdminFunc.CalculatePercentage((-1) * percnPriorCost, (-1) * percnThisCost);
                    string procurementMethodName = commonfunctions.GetTaxonomyNameFromID(long.Parse(dtrr["ProcurementMethod"].ToString()));

                    dtResults.Rows.Add(procurementMethodName, longpriorFYCost.ToString("N3"), longthisFYCost.ToString("N3"), DollarDiff.ToString("N3"), percentage);
                }






                break;


            case "All Prior FY on Record":

                dtResults.Columns.Add("Procurement Method");
                // ArrayList YearList = new ArrayList();




                string thisFYCost = "0";
                sql = "SELECT  DIstinct TOP 1 YEAR(PeriodofPerformanceStart) as Year    FROM  dbo.ResourcesContract     where PeriodofPerformanceStart < '" + StartDate.ToString("d") + "' and YEAR(PeriodofPerformanceStart) !='1900' and ResourceID in (" + Resources + ")   order by Year asc";
                string stopAtYEar = DataBase.returnOneValue(sql);


                if (stopAtYEar != "")
                {
                    int HowManyColumnsToCreate = StartDate.Year - Convert.ToInt32(stopAtYEar);
                    string dollarDiffer = "";
                    string percentageDiffer = "";

                    dtResults.Columns.Add(StartDate.Year.ToString());


                    for (int ii = 1; ii <= HowManyColumnsToCreate; ii++)
                    {
                        string columnName = (StartDate.Year - ii).ToString();
                        dtResults.Columns.Add(columnName);
                        dollarDiffer = columnName + " Dollar Diff";
                        dtResults.Columns.Add(dollarDiffer);

                        percentageDiffer = columnName + " % Diff";
                        dtResults.Columns.Add(percentageDiffer);
                    }

                    //  Response.Write(sql.ToString());
                    DataRow dra = dtResults.NewRow();

                    int intYear = 1;
                    if (Resources != "")
                    {
                        sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where ResourceID in (" + Resources + ") and ProcurementMethod is not null and PeriodofPerformanceStart BETWEEN '" + PriorStartDate.ToString("d") + " 00:00:00' and '" + EndDate.ToString("d") + " 23:59:59' group by [ProcurementMethod]  ";
                    }
                    else
                    {
                        sqlAllProinThisRange = "select distinct [ProcurementMethod]  from ResourcesContract where PeriodofPerformanceStart BETWEEN '" + PriorStartDate.ToString("d") + " 00:00:00' and '" + EndDate.ToString("d") + " 23:59:59'   and ProcurementMethod is not null group by [ProcurementMethod]  ";
                    }
                    dtrr = DataBase.dbDataTable(sqlAllProinThisRange).CreateDataReader();
                    if (dtrr.HasRows)
                    {
                        while (dtrr.Read())
                        {

                            string getFYCurrentProc = processProc(dtrr["ProcurementMethod"].ToString(), dateRangeSQL, Resources);
                            dra[StartDate.Year.ToString()] = getFYCurrentProc;
                            int loopStartDate = StartDate.Year - 1;

                            while (loopStartDate >= Convert.ToInt32(stopAtYEar))
                            {

                                PriorStartDate = getSelectedDates["StartDate"].AddYears(-intYear);
                                PriorEndDate = getSelectedDates["EndDate"].AddYears(-intYear);

                                PriorDateSQL = " PeriodofPerformanceStart  BETWEEN  '" + PriorStartDate.ToString("d") + " 00:00:00'  and '" + PriorEndDate.ToString("d") + " 23:59:59' ";
                                string columnName = loopStartDate.ToString();
                                string priorFYCost = processProc(dtrr["ProcurementMethod"].ToString(), PriorDateSQL, Resources);


                                Double longthisFYCost = 0;
                                Double longpriorFYCost;
                                if (!Double.TryParse(priorFYCost, out longpriorFYCost)) longpriorFYCost = 0;

                                Double DollarDiff = longthisFYCost - longpriorFYCost;



                                dra[columnName] = priorFYCost;
                                dra[dollarDiffer] = DollarDiff;

                                Double percnPriorCost = longpriorFYCost;
                                Double percnThisCost = longthisFYCost;

                                if (percnPriorCost == 0) percnPriorCost = 1;
                                if (percnThisCost == 0) percnThisCost = 1;

                                string percentage = AdminFunc.CalculatePercentage((-1) * percnPriorCost, (-1) * percnThisCost);
                                string procurementMethodName = commonfunctions.GetTaxonomyNameFromID(long.Parse(dtrr["ProcurementMethod"].ToString()));


                                loopStartDate--;
                                intYear++;
                                // dtResults.Rows.Add(procurementMethodName, longpriorFYCost.ToString("N3"), longthisFYCost.ToString("N3"), DollarDiff.ToString("N3"), percentage);
                                dtResults.Rows.Add(dra);
                            }
                        }
                    }
                }





                break;
                dtResults = DataBase.sortDataTable(dtResults, "Procurement Method", "ASC");

        }
    }




    private string processProc(string procID, string DateSQL, string Resources)
    {
        // return DataBase.returnOneValue("Select sum(AnnualContractCost) as amt from ";
        string sql = string.Empty;

        if (Resources != "")
        {
            sql = "select  Sum(isnull(cast(AnnualContractCost as float),0)) as Amt   from ResourcesContract ";
            sql += "  where ResourceID in (" + Resources + ") and " + DateSQL + "  and ProcurementMethod='" + procID + "' ";


        }
        else
        {
            sql = "select   Sum(isnull(cast(AnnualContractCost as float),0)) as Amt   from ResourcesContract  ";
            sql += "  where " + DateSQL + "  and ProcurementMethod='" + procID + "' ";


        }
        //  Response.Write(sql + "<br/>");
        return DataBase.returnOneValue(sql).Trim();
    }
    private void showComparativeResults(string Resources, string CompareDataAgainstTxt, string DateRange)
    {
        // dtResults.Columns.Add("Resource Name");
        DataRow dra = dtResults.NewRow();
        Dictionary<string, DateTime> getSelectedDates = Reports.getSelectedDates(DateRange, StartDateTxt, EndDateTxt);
        DateTime StartDate = getSelectedDates["StartDate"];
        DateTime EndDate = getSelectedDates["EndDate"];
        string dateRangeSQL = " PeriodofPerformanceStart  BETWEEN  '" + StartDate.ToString("d") + " 00:00:00'  and '" + EndDate.ToString("d") + " 23:59:59' ";



        string[] ResourcesIDs = Resources.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        if (ResourcesIDs.Length == 2)
        {
            // Dictionary<string, string> allDetails = new Dictionary<string, string>();
            ArrayList allAmount = new ArrayList();
            dtResults.Columns.Add("Date Range");
            dra["Date Range"] = DateRange;
            foreach (string ResourcesID in ResourcesIDs)
            {

                string resourceName = AdminFunc.getResourceName(ResourcesID);
                dtResults.Columns.Add(resourceName);
                double amount = GetAnnualContractCost(ResourcesID, dateRangeSQL);
                dra[resourceName] = amount.ToString("N2");
                // allDetails["ResourceName"] = resourceName;

                allAmount.Add(amount);
            }

            dtResults.Columns.Add("Dollar Difference");
            dtResults.Columns.Add("% Difference");
            double amnt1 = double.Parse(allAmount[0].ToString());
            double amnt2 = double.Parse(allAmount[1].ToString());

            string percentage = AdminFunc.CalculatePercentage((-1) * amnt1, (-1) * amnt2);

            dra["Dollar Difference"] = (amnt1 - amnt2).ToString("N2");
            dra["% Difference"] = percentage;
            dtResults.Rows.Add(dra);
        }
        else
        {
            showPricingOver(Resources, CompareDataAgainstTxt, DateRange);
        }


    }

    private double GetAnnualContractCost(string resId, string DateRange)
    {
        DataTableReader dtr;
        double AnnualContractCost = 0;
        string sql = "SELECT  dbo.ResourcesContract.ResourceID, dbo.ResourcesContract.FiscalYear, dbo.ResourcesContract.AnnualContractCost ";
        sql += "FROM  dbo.ResourcesContract ";
        sql += "  where ResourceID in (" + resId + ") and " + DateRange + "";
        //   Response.Write(sql.ToString() + "<br/>");
        dtr = DataBase.dbDataTable(sql).CreateDataReader();
        if (dtr.HasRows)
        {
            dtr.Read();
            if (dtr["AnnualContractCost"] != null)
            {
                AnnualContractCost = double.Parse(dtr["AnnualContractCost"].ToString().Trim());
            }
        }
        dtr.Close();
        return AnnualContractCost;
    }



    private void showPricingOver(string Resources, string CompareDataAgainstTxt, string DateRange)
    {

        DataTableReader dtr;
        // ComparativeResultsPanel.Visible = true;
        //  string dateRangeSQL = Reports.getSelectedDateSql("PeriodofPerformanceStart", DateRange, StartDateTxt, EndDateTxt);

        //  string PriorDateSQL = Reports.getSelectedDateSql("PeriodofPerformanceStart", "Last Fiscal Year", "", "");


        Dictionary<string, DateTime> getSelectedDates = Reports.getSelectedDates(DateRange, StartDateTxt, EndDateTxt);
        DateTime StartDate = getSelectedDates["StartDate"];
        DateTime EndDate = getSelectedDates["EndDate"];
        DateTime PriorStartDate = getSelectedDates["StartDate"].AddYears(-1);
        DateTime PriorEndDate = getSelectedDates["EndDate"].AddYears(-1);

        string dateRangeSQL = " PeriodofPerformanceStart  BETWEEN  '" + StartDate.ToString("d") + " 00:00:00'  and '" + EndDate.ToString("d") + " 23:59:59' ";
        string PriorDateSQL = " PeriodofPerformanceStart  BETWEEN  '" + PriorStartDate.ToString("d") + " 00:00:00'  and '" + PriorEndDate.ToString("d") + " 23:59:59' ";
        //int fiscalyear = AdminFunc.CurrentFiscalYear(DateTime.Now);

        string[] ResourcesIDs = Resources.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string sql = "";
        switch (CompareDataAgainstTxt)
        {
            case "None":
                return;

                break;

            case "Prior FY":

                dtResults.Columns.Add("Resource Name");
                dtResults.Columns.Add("Prior FY Cost");
                dtResults.Columns.Add("Current FY Cost");
                dtResults.Columns.Add("Dollar Difference");
                dtResults.Columns.Add("Percentage");

                //lastfiscal = 
                // Reports.getFiscalDates(AdminFunc.CurrentFiscalYear(DateTime.Now) - 1);
                int FY_Year = AdminFunc.CurrentFiscalYear(DateTime.Now) - 1;
                int priorFY_Year = FY_Year - 1;
                string thisFYCost = "0";
                string priorFYCost = "0";
                string resourceName = string.Empty;
                Double DollarDiff = 0;


                //  string[] ResourcesIDs = Resources.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string ResourcesID in ResourcesIDs)
                {

                    resourceName = AdminFunc.getResourceName(ResourcesID);
                    sql = "SELECT  dbo.ResourcesContract.ResourceID, dbo.ResourcesContract.FiscalYear, dbo.ResourcesContract.AnnualContractCost ";
                    sql += "FROM  dbo.ResourcesContract ";
                    sql += "  where ResourceID in (" + ResourcesID + ") and " + PriorDateSQL + "";
                    //   Response.Write(sql.ToString() + "<br/>");
                    dtr = DataBase.dbDataTable(sql).CreateDataReader();
                    if (dtr.HasRows)
                    {
                        dtr.Read();
                        priorFYCost = NullToString(dtr["AnnualContractCost"].ToString().Trim());
                    }


                    sql = "SELECT  dbo.ResourcesContract.ResourceID, dbo.ResourcesContract.FiscalYear, dbo.ResourcesContract.AnnualContractCost ";
                    sql += "FROM  dbo.ResourcesContract  ";
                    sql += "  where ResourceID in (" + ResourcesID + ") and " + dateRangeSQL + " ";
                    // Response.Write(sql.ToString() + "<br/>");
                    dtr = DataBase.dbDataTable(sql).CreateDataReader();
                    if (dtr.HasRows)
                    {
                        dtr.Read();
                        thisFYCost = NullToString(dtr["AnnualContractCost"].ToString().Trim());
                    }


                    Double longthisFYCost;
                    if (!Double.TryParse(thisFYCost, out longthisFYCost)) longthisFYCost = 0;
                    Double longpriorFYCost;
                    if (!Double.TryParse(priorFYCost, out longpriorFYCost)) longpriorFYCost = 0;

                    DollarDiff = longthisFYCost - longpriorFYCost;

                    Double percnPriorCost = longpriorFYCost;
                    Double percnThisCost = longthisFYCost;

                    if (percnPriorCost == 0) percnPriorCost = 1;
                    if (percnThisCost == 0) percnThisCost = 1;

                    string percentage = AdminFunc.CalculatePercentage((-1) * percnPriorCost, (-1) * percnThisCost);
                    dtResults.Rows.Add(resourceName, longpriorFYCost.ToString("N3"), longthisFYCost.ToString("N3"), DollarDiff.ToString("N3"), percentage);

                }

                break;

            case "All Prior FY on Record":


                dtResults.Columns.Add("Resource Name");
                ArrayList YearList = new ArrayList();

                sql = "SELECT  DIstinct TOP 1 YEAR(PeriodofPerformanceStart) as Year    FROM  dbo.ResourcesContract     where PeriodofPerformanceStart < '" + StartDate.ToString("d") + "' and YEAR(PeriodofPerformanceStart) !='1900' and ResourceID in (" + Resources + ")   order by Year asc";
                string stopAtYEar = DataBase.returnOneValue(sql);
                int HowManyColumnsToCreate = StartDate.Year - Convert.ToInt32(stopAtYEar);
                string dollarDiffer = "";
                string percentageDiffer = "";

                dtResults.Columns.Add(StartDate.Year.ToString());


                for (int ii = 1; ii <= HowManyColumnsToCreate; ii++)
                {
                    string columnName = (StartDate.Year - ii).ToString();
                    dtResults.Columns.Add(columnName);
                    dollarDiffer = columnName + " Dollar Diff";
                    dtResults.Columns.Add(dollarDiffer);

                    percentageDiffer = columnName + " % Diff";
                    dtResults.Columns.Add(percentageDiffer);
                }

                //  Response.Write(sql.ToString());


                dateRangeSQL = " PeriodofPerformanceStart  BETWEEN  '" + StartDate.ToString("d") + " 00:00:00'  and '" + EndDate.ToString("d") + " 23:59:59' ";
                ArrayList AllDates = new ArrayList();
                ArrayList AllCosts = new ArrayList();
                ArrayList AllResource = new ArrayList();
                int intYear = 1;
                Dictionary<string, string> results = new Dictionary<string, string>();



                foreach (string ResourcesID in ResourcesIDs)
                {
                    // AllResource.Add(AdminFunc.getResourceName(ResourcesID));
                    DataRow dra = dtResults.NewRow();


                    dra["Resource Name"] = AdminFunc.getResourceName(ResourcesID);

                    thisFYCost = "0";
                    ///Selected Date Range 
                    sql = "SELECT  dbo.ResourcesContract.ResourceID, dbo.ResourcesContract.FiscalYear, dbo.ResourcesContract.AnnualContractCost ";
                    sql += "FROM  dbo.ResourcesContract  ";
                    sql += "  where ResourceID in (" + ResourcesID + ") and " + dateRangeSQL + " ";

                    dtr = DataBase.dbDataTable(sql).CreateDataReader();
                    if (dtr.HasRows)
                    {
                        dtr.Read();
                        thisFYCost = NullToString(dtr["AnnualContractCost"].ToString().Trim());
                        //  Response.Write(thisFYCost.ToString() + "<br/>");

                    }


                    dra[StartDate.Year.ToString()] = thisFYCost;

                    Double longthisFYCost;
                    if (!Double.TryParse(thisFYCost, out longthisFYCost)) longthisFYCost = 0;

                    int loopStartDate = StartDate.Year - 1;

                    while (loopStartDate >= Convert.ToInt32(stopAtYEar))
                    {

                        PriorStartDate = getSelectedDates["StartDate"].AddYears(-intYear);
                        PriorEndDate = getSelectedDates["EndDate"].AddYears(-intYear);

                        string columnName = loopStartDate.ToString();

                        priorFYCost = "0";
                        PriorDateSQL = " PeriodofPerformanceStart  BETWEEN  '" + PriorStartDate.ToString("d") + " 00:00:00'  and '" + PriorEndDate.ToString("d") + " 23:59:59' ";
                        sql = "SELECT  dbo.ResourcesContract.ResourceID, dbo.ResourcesContract.FiscalYear, dbo.ResourcesContract.AnnualContractCost ";
                        sql += "FROM  dbo.ResourcesContract ";
                        sql += "  where ResourceID in (" + ResourcesID + ") and " + PriorDateSQL + "";
                        // Response.Write(sql.ToString() + "<br/>");
                        dtr = DataBase.dbDataTable(sql).CreateDataReader();
                        if (dtr.HasRows)
                        {
                            dtr.Read();
                            priorFYCost = NullToString(dtr["AnnualContractCost"].ToString().Trim());


                        }

                        //dollarDiffer = columnName + "Dollar Diff";

                        Double longpriorFYCost;
                        if (!Double.TryParse(priorFYCost, out longpriorFYCost)) longpriorFYCost = 0;

                        DollarDiff = longthisFYCost - longpriorFYCost;



                        dra[columnName] = long.Parse(priorFYCost).ToString("N1");
                        dra[dollarDiffer] = DollarDiff.ToString("N1");


                        if (longpriorFYCost == 0) longpriorFYCost = 1;
                        if (longthisFYCost == 0) longthisFYCost = 1;




                        string percentage = AdminFunc.CalculatePercentage((-1) * longpriorFYCost, (-1) * longthisFYCost);
                        dra[percentageDiffer] = percentage;

                        loopStartDate--;
                        intYear++;

                    }


                    dtResults.Rows.Add(dra);
                }



                break;



        }



    }


    private string NullToString(object Value)
    {

        // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
        return Value == null ? "0" : Value.ToString();

        // If this is not what you want then this form may suit you better, handles 'Null' and DBNull otherwise tries a straight cast
        // which will throw if Value isn't actually a string object.
        //return Value == null || Value == DBNull.Value ? "" : (string)Value;


    }

    public string reportID { get; set; }

    public string compareDisplay { get; set; }

    public string Resources { get; set; }

    public string ResourceCat { get; set; }

    public string selectedCompareData { get; set; }

    public string SelectedcompareID { get; set; }

    public string customDatSDDisplay { get; set; }

    public string ProcurementMethod { get; set; }

    public string ResourceTypeTaxonomy { get; set; }


    public string StartDateTxt { get; set; }

    public string EndDateTxt { get; set; }
}