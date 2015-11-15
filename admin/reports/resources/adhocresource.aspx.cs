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
            ListBox_AccessType.DataSource = AdminFunc.CreateAccessTypeOptions(118, false);
            ListBox_AccessType.DataBind();
            ListBox_OfficeCode.DataSource = uniqueValuesFromDB("ClickedByOffice");
            ListBox_OfficeCode.DataBind();
            ListBox_UserDomain.DataSource = uniqueValuesFromDB("ClickedByUserDomain");
            ListBox_UserDomain.DataBind();
            ListBox_CoreServicesOffice.DataSource = uniqueValuesFromDB("ClickedByOffice");
            ListBox_CoreServicesOffice.DataBind();
            ListBox_PIN.DataSource = uniqueValuesFromDB("ClickedByPIN");
            ListBox_PIN.DataBind();
            ListBox_Email.DataSource = uniqueValuesFromDB("ClickedByEMail");
            ListBox_Email.DataBind();
            ListBox_FirstName.DataSource = uniqueValuesFromDB("ClickedByFirstName");
            ListBox_FirstName.DataBind();
            ListBox_LastName.DataSource = uniqueValuesFromDB("ClickedByLastName");
            ListBox_LastName.DataBind();
            ListBox_Server.DataSource = uniqueValuesFromDB("ClickedByServer");
            ListBox_Server.DataBind();

            customDatSDDisplay = " style='display:none' ";





            if (Request.QueryString["reportId"] != null)
            {
                getExistingData(Request.QueryString["reportId"].ToString());
                RunReport.Visible = false;
                SaveReport.Visible = true;

            }
            else
            {

                DateRangeDD.Items.FindByValue("Last 30 Days").Selected = true;
                // ShowDataforDD.Items.FindByValue(ShowDatafor).Selected = true;
                DisplayDD.Items.FindByValue("All").Selected = true;
                Display2DD.Items.FindByValue("Visitors").Selected = true;
                DisplayByDD.Items.FindByValue("Date").Selected = true;
                SaveReport.Visible = false;
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


            ShowDatafor = commonfunctions.getFieldValue(XMLDoc, "ShowDatafor");
            AssociatedNetworkTxt = commonfunctions.getFieldValue(XMLDoc, "AssociatedNetwork");
            Resources = commonfunctions.getFieldValue(XMLDoc, "Resources");
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
            string coreServicesServer = commonfunctions.getFieldValue(XMLDoc, "coreServicesServer");

            ShowDataforDD.Items.FindByValue(ShowDatafor).Selected = true;
            DisplayDD.Items.FindByValue(DisplayDDDB).Selected = true;
            Display2DD.Items.FindByValue(Display2DDDB).Selected = true;
            DisplayByDD.Items.FindByValue(DisplayByDDDB).Selected = true;

            switch (ShowDatafor)
            {
                case "Applications in a Network":
                    editPanel.Visible = true;
                    AssociatedNetwork.Items.FindByValue(AssociatedNetworkTxt).Selected = true;
                    break;
                

            }



            DateRangeDD.Items.FindByValue(DateRangeTxt).Selected = true;
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

            //advanced view
            selectThisValuesAdvanceView(ListBox_OfficeCode, coreServicesOfficeCode);


            try
            {
                BreakOutDataByDD.Items.FindByValue(coreServicesBreakOutDataByDD).Selected = true;
            }
            catch { }

            selectThisValuesAdvanceView(ListBox_AccessType, coreServicesAccessType);
            selectThisValuesAdvanceView(ListBox_UserDomain, coreServicesUserDomain);
            selectThisValuesAdvanceView(ListBox_CoreServicesOffice, coreServicesOfficeCode);
            selectThisValuesAdvanceView(ListBox_PIN, coreServicesPIN);
            selectThisValuesAdvanceView(ListBox_Email, coreServicesEmail);
            selectThisValuesAdvanceView(ListBox_FirstName, coreServicesFirstName);
            selectThisValuesAdvanceView(ListBox_LastName, coreServicesLastName);
            selectThisValuesAdvanceView(ListBox_Server, coreServicesServer);



        }

    }

    private void selectThisValuesAdvanceView(ListBox ListBoxID, string coreServicesOfficeCode)
    {
        string[] formIDs = coreServicesOfficeCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string formId in formIDs)
        {
            try
            {
               string formId2 = formId.TrimEnd('\'').TrimStart('\'');
               ListBoxID.Items.FindByValue(formId2).Selected = true;
            }
            catch { }
        }

    }
    private string getXML()
    {
        string xmlData = "<root>";

        string ShowDatafor = ShowDataforDD.SelectedValue;
        xmlData += "<ShowDatafor>" + ShowDatafor + "</ShowDatafor>";

        string AssociatedNetworkTxt = AssociatedNetwork.SelectedValue;
        xmlData += "<AssociatedNetwork>" + AssociatedNetworkTxt + "</AssociatedNetwork>";

        //  Response.Write(getResouces());

        string Resources = getMultipleOptionsValues("ListResourcesDD" , true);
        xmlData += "<Resources>" + Resources + "</Resources>";


        string DateRangeTxt = DateRangeDD.SelectedValue;
        xmlData += "<DateRange>" + DateRangeTxt + "</DateRange>";


        xmlData += "<StartDate>" + StartDate.Text + "</StartDate>";
        xmlData += "<EndDate>" + EndDate.Text + "</EndDate>";


        string DisplayDDTxt = DisplayDD.SelectedValue;
        xmlData += "<DisplayDD>" + DisplayDDTxt + "</DisplayDD>";

        string Display2DDTxt = Display2DD.SelectedValue;
        xmlData += "<Display2DD>" + Display2DDTxt + "</Display2DD>";

        string DisplayByDDTxt = DisplayByDD.SelectedValue;
        xmlData += "<DisplayByDD>" + DisplayByDDTxt + "</DisplayByDD>";




        /////////////Advance Stuff

        xmlData += "<coreServicesAccessType>" + returnSelectedItemsValue(ListBox_AccessType) + "</coreServicesAccessType>";
        xmlData += "<coreServicesBreakOutDataByDD>" + BreakOutDataByDD.SelectedValue + "</coreServicesBreakOutDataByDD>";
        xmlData += "<coreServicesOfficeCode>" + returnSelectedItemsValue(ListBox_OfficeCode) + "</coreServicesOfficeCode>";
        xmlData += "<coreServicesUserDomain>" + returnSelectedItemsValue(ListBox_UserDomain) + "</coreServicesUserDomain>";
        xmlData += "<coreServicesCoreServicesOffice>" + returnSelectedItemsValue(ListBox_CoreServicesOffice) + "</coreServicesCoreServicesOffice>";
        xmlData += "<coreServicesPIN>" + returnSelectedItemsValue(ListBox_PIN) + "</coreServicesPIN>";
        xmlData += "<coreServicesEmail>" + returnSelectedItemsValue(ListBox_Email) + "</coreServicesEmail>";
        xmlData += "<coreServicesFirstName>" + returnSelectedItemsValue(ListBox_FirstName) + "</coreServicesFirstName>";
        xmlData += "<coreServicesLastName>" + returnSelectedItemsValue(ListBox_LastName) + "</coreServicesLastName>";
        xmlData += "<coreServicesServer>" + returnSelectedItemsValue(ListBox_Server) + "</coreServicesServer>";
        xmlData += "</root>";

        return xmlData;

    }


    private string getSQL()
    {

        string ShowDatafor = ShowDataforDD.SelectedValue;
        string AssociatedNetworkTxt = AssociatedNetwork.SelectedValue;
        string Resources = getMultipleOptionsValues("ListResourcesDD", true);
        string DateRangeTxt = DateRangeDD.SelectedValue;
        string DisplayDDTxt = DisplayDD.SelectedValue;
        string Display2DDTxt = Display2DD.SelectedValue;
        string DisplayByDDTxt = DisplayByDD.SelectedValue;

        string StartDateTxt = StartDate.Text; // commonfunctions.getFieldValue(XMLDoc, "StartDate");
        string EndDateTxt = EndDate.Text; // commonfunctions.getFieldValue(XMLDoc, "EndDate");

        string DisplayDDDB = DisplayDD.SelectedValue; //        commonfunctions.getFieldValue(XMLDoc, "DisplayDD");
        string Display2DDDB = Display2DD.SelectedValue;  //commonfunctions.getFieldValue(XMLDoc, "Display2DD");

        string DisplayByDDDB = DisplayByDD.SelectedValue; // commonfunctions.getFieldValue(XMLDoc, "DisplayByDD");


        string coreServicesAccessType = returnSelectedItemsValue(ListBox_AccessType); // commonfunctions.getFieldValue(XMLDoc, "coreServicesAccessType");
        string coreServicesBreakOutDataByDD = BreakOutDataByDD.SelectedValue; // commonfunctions.getFieldValue(XMLDoc, "coreServicesBreakOutDataByDD");
        string coreServicesOfficeCode = returnSelectedItemsValue(ListBox_OfficeCode); // commonfunctions.getFieldValue(XMLDoc, "coreServicesOfficeCode");
        string coreServicesUserDomain = returnSelectedItemsValue(ListBox_UserDomain); // commonfunctions.getFieldValue(XMLDoc, "coreServicesUserDomain");
        string coreServicesCoreServicesOffice = returnSelectedItemsValue(ListBox_CoreServicesOffice); // commonfunctions.getFieldValue(XMLDoc, "coreServicesCoreServicesOffice");
        string coreServicesPIN = returnSelectedItemsValue(ListBox_PIN); //commonfunctions.getFieldValue(XMLDoc, "coreServicesPIN");
        string coreServicesEmail = returnSelectedItemsValue(ListBox_Email); //commonfunctions.getFieldValue(XMLDoc, "coreServicesEmail");
        string coreServicesFirstName = returnSelectedItemsValue(ListBox_FirstName); // commonfunctions.getFieldValue(XMLDoc, "coreServicesFirstName");
        string coreServicesLastName = returnSelectedItemsValue(ListBox_LastName); //commonfunctions.getFieldValue(XMLDoc, "coreServicesLastName");
        string coreServicesServer = returnSelectedItemsValue(ListBox_Server); // commonfunctions.getFieldValue(XMLDoc, "coreServicesLastName");

        string buildSQL = string.Empty;
        string DisplayCount = getDisplayCount(DisplayDDDB);
        ArrayList DisplayBYSQL = getDisplayBY(DisplayByDDDB);
        string innerJoin = " FROM dbo.ClickTracking INNER JOIN  dbo.Resources ON dbo.ClickTracking.Resource = dbo.Resources.ID ";

        if (Display2DDDB == "Visitors")
        {
            // buildSQL = "Select  count(DISTINCT  ClickedByPIN) as uniqueVisitors from ClickTracking where Resource in (" + Resources + ") ";
            buildSQL = "Select distinct " + DisplayCount + "  " + DisplayBYSQL[1] + " , count(DISTINCT  ClickedByPIN) as Visitors " + innerJoin + " where Resource in (" + Resources + ") ";

        }
        else
        {
            
          //  buildSQL = "Select " + DisplayCount + " " + innerJoin + " where Resource in (" + Resources + ") ";
            buildSQL = "Select distinct " + DisplayCount + "  " + DisplayBYSQL[1] + "  , count(*) as  Hits " + innerJoin + " where Resource in (" + Resources + ") ";

        }

        string dateselected = getSelectedDateSql("ClickedDateTime", DateRangeTxt, StartDateTxt, EndDateTxt);
        buildSQL += " and " + dateselected;
        
        ///lets get advance stuff
        ///
        if (coreServicesAccessType != "") buildSQL += " and AccessTypeTaxonomy in (" + coreServicesAccessType + ") ";
        if (coreServicesOfficeCode != "") buildSQL += " and ClickedByOffice in (" + coreServicesOfficeCode + ") ";
        if (coreServicesUserDomain != "") buildSQL += " and ClickedByUserDomain in (" + coreServicesUserDomain + ") ";
        if (coreServicesCoreServicesOffice != "") buildSQL += " and AccessTypeTaxonomy in (" + coreServicesCoreServicesOffice + ") ";
        if (coreServicesPIN != "") buildSQL += " and ClickedByPIN in (" + coreServicesPIN + ") ";
        if (coreServicesEmail != "") buildSQL += " and ClickedByEMail in (" + coreServicesEmail + ") ";
        if (coreServicesFirstName != "") buildSQL += " and ClickedByFirstName in (" + coreServicesFirstName + ") ";
        if (coreServicesLastName != "") buildSQL += " and ClickedByLastName in (" + coreServicesLastName + ") ";
        if (coreServicesServer != "") buildSQL += " and ClickedByServer in (" + coreServicesServer + ") ";



        string groupBy = " group by " + DisplayBYSQL[0] + " order by " + DisplayBYSQL[0];
        buildSQL += groupBy;

        return buildSQL;
    }

    private ArrayList getDisplayBY(string DisplayByDDDB)
    {
        ArrayList result = new ArrayList();
        switch (DisplayByDDDB)
        {
            case "Date":
                result.Add("ClickedDateTime");
                result.Add("ClickedDateTime as Date");
                break;

            case "Month/Year":
              //  result = " MONTH(ClickedDateTime) ,  YEAR(ClickedDateTime) ";
                result.Add("MONTH(ClickedDateTime) ,  YEAR(ClickedDateTime)");
                result.Add("MONTH(ClickedDateTime) As Month ,  YEAR(ClickedDateTime) as Year");
                break;


            case "Application":
                //result = "ResourceName";
                result.Add("ResourceName" );
                result.Add("ResourceName  As [Resource Name]");
                break;


            case "Mandatory/Discretionary":
               // result = "Mandatory";
                result.Add("Mandatory");
                result.Add("Mandatory  as Mandatory");
                break;

            case "Office":
               // result = "ClickedByOffice";
                result.Add("ClickedByOffice");
                result.Add("ClickedByOffice  as  Office");
                break;

            case "Server":
               // result = "ClickedByServer";
                result.Add("ClickedByServer");
                result.Add("ClickedByServer   as  Server");
                break;


            case "Domain":
                //result = "ClickedByUserDomain";
                result.Add("ClickedByUserDomain" );
                result.Add("ClickedByUserDomain as  [User Domain]");
                break;

            case "Email":
               // result = "ClickedByEMail";
                result.Add("ClickedByEMail");
                result.Add("ClickedByEMail as  Email");
                break;

            case "PIN":
               // result = "ClickedByPIN";
                result.Add("ClickedByPIN" );
                result.Add("ClickedByPIN  as  PIN");
                break;


            case "LastName":
               // result = "ClickedByLastName";
                result.Add("ClickedByLastName");
                result.Add("ClickedByLastName  as   [Last Name]");
                break;

            case "FirstName":
               // result = "ClickedByFirstName";
                result.Add("ClickedByFirstName" );
                result.Add("ClickedByFirstName  as   [First Name]");
                break;


        }

        return   result ;
    }

    private string getDisplayCount(string DisplayDDDB)
    {
        string result = string.Empty;
        switch (DisplayDDDB)
        {
            case "All":
                result = " ";
                break;

            case "All repeat users":
                result = "   ";
                break;


            case "The top 100 percent":
                result = " top 100 PERCENT   ";
                break;


            case "The top 50 percent":
                result = "  top 50 PERCENT * ";
                break;


            case "The top 500":
                result = " top 500   ";
                break;


            case "The top 250":
                result = " top 250   ";
                break;

            case "The top 100":
                result = " top 100   ";
                break;


        }

        return result;
    }

 

    protected void RunReport_Click(object sender, EventArgs e)
    {
        string xmlData = getXML();
        string SQLData = getSQL();

        string sql = " INSERT INTO [dbo].[Report]([Name],[CreatedbyPIN],[CreateDate],[LastModifiedDate],[LastRunDate],[ReportExecutionFile],[ReportSQLStatement],[ReportXMLData], ReportType) ";
        sql += "  VALUES(@Name,@CreatedbyPIN ,GETDATE() ,GETDATE() ,GETDATE(),@ReportExecutionFile ,@ReportSQLStatement ,@ReportXMLData, 'Ad Hoc Resource Report' ) ";

        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@Name", "Ad Hoc Help Report");
        sqlcmd.Parameters.AddWithValue("@CreatedbyPIN", loginSSA.myPIN);
        sqlcmd.Parameters.AddWithValue("@ReportExecutionFile", "");
        sqlcmd.Parameters.AddWithValue("@ReportSQLStatement", SQLData);
        sqlcmd.Parameters.AddWithValue("@ReportXMLData", xmlData);
        string reportIDD = DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmd);
        Response.Redirect("/admin/reports/resources/adhocresourceResults.aspx?reportid=" + reportIDD + "#sb=0");
       // Response.Write(SQLData);
    }



    protected void SaveReport_Click(object sender, EventArgs e)
    {
        string XMLData = getXML();
        string SQLData = getSQL();
        string reportID = Request.QueryString["reportid"].ToString();
        string sql = "UPDATE [dbo].[Report] SET [LastModifiedDate] = GETDATE()   ,[ReportXMLData] = @ReportXMLData  , [ReportSQLStatement]=@ReportSQLStatement ";
        sql += " WHERE ID=" + reportID;
        // Response.Write(sql);
        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@ReportXMLData", XMLData);
        sqlcmd.Parameters.AddWithValue("@ReportSQLStatement", SQLData);
        
        DataBase.executeCommandWithParameters(sqlcmd);
         Response.Redirect("/admin/reports/resources/adhocresourceResults.aspx?reportid=" + reportID + "#sb=0");
     //   Response.Write(SQLData);
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
                //  output = fieldName + "='" + today.ToString("d") + "'";
                output = fieldName + " BETWEEN  '" + today.ToString("d") + " 00:00:00'  and '" + today.ToString("d") + " 23:59:59' ";
                break;
            case "This Week":

                delta = DayOfWeek.Sunday - today.DayOfWeek;
                DateTime BeginSunday = today.AddDays(delta);
                DateTime EndSaturday = BeginSunday.AddDays(6);
                output = fieldName + " BETWEEN  '" + BeginSunday.ToString("d") + " 00:00:00'  and '" + EndSaturday.ToString("d") + " 23:59:59' ";

                break;

            case "Last Week":
                delta = DayOfWeek.Sunday - today.DayOfWeek;
                DateTime BeginLastWeekSunday = today.AddDays(delta).AddDays(-7);
                DateTime EndLastWeekSaturday = BeginLastWeekSunday.AddDays(6);
                output = fieldName + " BETWEEN  '" + BeginLastWeekSunday.ToString("d") + " 00:00:00'  and '" + EndLastWeekSaturday.ToString("d") + " 23:59:59' ";

                break;

            case "Last 30 Days":
                DateTime Last30Days = today.AddDays(-30);
                output = fieldName + " BETWEEN  '" + Last30Days.ToString("d") + " 00:00:00'  and '" + today.ToString("d") + " 23:59:59' ";

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
                output = fieldName + " BETWEEN  '" + BeginofLastMonth.ToString("d") + " 00:00:00'  and '" + EndOfLastMonth.ToString("d") + " 23:59:59' ";
                break;

            case "This Quarter":
                Dictionary<string, string> getQuaterDates1 = getQuaterDates(GetQuarter(today));
                output = fieldName + " BETWEEN  '" + getQuaterDates1["StartDate"] + " 00:00:00'  and '" + getQuaterDates1["EndDate"] + " 23:59:59' ";
                break;

            case "Last Quarter":
                Dictionary<string, string> getQuaterDates2 = getQuaterDates(GetQuarter(today) - 1);
                output = fieldName + " BETWEEN  '" + getQuaterDates2["StartDate"] + " 00:00:00'  and '" + getQuaterDates2["EndDate"] + " 23:59:59' ";
                break;


            case "This Fiscal Year":
                Dictionary<string, string> FiscalDates = getFiscalDates(AdminFunc.CurrentFiscalYear(DateTime.Now));
                output = fieldName + " BETWEEN  '" + FiscalDates["StartDate"] + " 00:00:00'  and '" + FiscalDates["EndDate"] + " 23:59:59' ";

                break;


            case "Last Fiscal Year":
                Dictionary<string, string> FiscalDates2 = getFiscalDates(AdminFunc.CurrentFiscalYear(DateTime.Now) - 1);
                output = fieldName + " BETWEEN  '" + FiscalDates2["StartDate"] + " 00:00:00'  and '" + FiscalDates2["EndDate"] + " 23:59:59' ";
                break;

            case "Custom Dates":
                output = fieldName + " BETWEEN  '" + CustomStartDate + " 00:00:00'  and '" + CustomEndDate + " 23:59:59' ";
                break;

        }

        return output;
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


    public DataTable uniqueValuesFromDB(string fieldname)
    {
        return DataBase.dbDataTable("Select Distinct(LTRIM(RTRIM(" + fieldname + "))) as value from [dbo].[ClickTracking] order by value ;");

    }

    public string returnSelectedItemsValue(ListBox lsBox , bool resource=false)
    {
        string seletectedItemsValue = string.Empty;

        foreach (ListItem li in lsBox.Items)
        {
            if (li.Selected == true)
            {
                if (resource)
                {
                    seletectedItemsValue +=   li.Value + ",";
                }
                else
                {
                    seletectedItemsValue += "'" + li.Value + "',";
                }
            }
        }

        return seletectedItemsValue.TrimEnd(',');
    }

    private string getMultipleOptionsValues(string FormControlName, bool resource = false)
    {

        string AllSelectedResourceID = string.Empty;
        try
        {
            string[] ListResourcesDD = this.Request.Form.GetValues(FormControlName);
            foreach (string resourceID in ListResourcesDD)
            {
                if (resource)
                {
                    AllSelectedResourceID += resourceID + ",";
                }
                else
                {
                    AllSelectedResourceID += "'" + resourceID + "',";
                }
            }

        }
        catch { }
        return AllSelectedResourceID.TrimEnd(',');
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





    public string customDatSDDisplay { get; set; }

    public string ShowDatafor { get; set; }

    public string AssociatedNetworkTxt { get; set; }

    public string Resources { get; set; }
    protected void DownloadReport_Click(object sender, EventArgs e)
    {
        string SQLData = getSQL();
        CsvExport myExport = new CsvExport();
        ArrayList columns = new ArrayList();
        DataTableReader dtr = DataBase.dbDataTable(SQLData).CreateDataReader();

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

    public string myCsv { get; set; }
}