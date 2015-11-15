using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace SSADL.CMS
{

    /// <summary>
    /// Summary description for Reports
    /// </summary>
    public class Reports
    {
        public Reports()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void RecordRunBy(string reportID)
        {
             
            string DisplayName = loginSSA.myLastName + ", " + loginSSA.myFirstName;
            string sqlRecordLastRun = "INSERT INTO [dbo].[ReportInstances]([ReportID],[DateTimRun],[RunbyPIN],[DisplayName])";
            sqlRecordLastRun += " VALUES('" + reportID + "',GETDATE(),'" + loginSSA.myPIN + "', '" + DisplayName + "');";
            sqlRecordLastRun += "Update   report set LastRunDate=GETDATE() where ID='" + reportID + "'";
            DataBase.executeCommand(sqlRecordLastRun);
        }
        public static string ReportSchedule(string rePortID)
        {
            string SchedulingMessage = "";
            DataTableReader dtr = DataBase.dbDataTable("Select * from ReportSchedule where reportid=" + rePortID + "").CreateDataReader();
            if (dtr.HasRows)
            {
                dtr.Read();
                string[] allRecepients = dtr["EmailAddress"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                SchedulingMessage = "This report is schedule to run on the " + dtr["RunFrequency"] + " and goes to " + allRecepients.Length + " recipients.";
              //  SchedulingMessageTag = "Update";
            }
            else
            {
                SchedulingMessage = "No scheduled reports.";
                //SchedulingMessageTag = "Create";
            }
            return SchedulingMessage;
        }

        public static Dictionary<string,string> ReportSchedule2(string rePortID)
        {

            Dictionary<string, string> SchedulingMessage = new Dictionary<string, string>();
            DataTableReader dtr = DataBase.dbDataTable("Select * from ReportSchedule where reportid=" + rePortID + "").CreateDataReader();
            if (dtr.HasRows)
            {
                dtr.Read();
                string[] allRecepients = dtr["EmailAddress"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                SchedulingMessage.Add("Message" , "This report is schedule to run on the " + dtr["RunFrequency"] + " and goes to " + allRecepients.Length + " recipients");
                SchedulingMessage.Add("ID", dtr["ID"].ToString());
                SchedulingMessage.Add("UpdateCreate", "Update");
                //  SchedulingMessageTag = "Update";
            }
            else
            {
                SchedulingMessage.Add("ID", "0");
                SchedulingMessage.Add("Message", "No scheduled reports");
                SchedulingMessage.Add("UpdateCreate", "Create");
                //SchedulingMessageTag = "Create";
            }
            return SchedulingMessage;
        }

        public static string getSelectedDateSql(string fieldName, string RequestSubmittedDateDD, string CustomStartDate, string CustomEndDate)
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

                case "Q1":
                    Dictionary<string, string> getQuaterDatesQ1 = getQuaterDates(1);
                    output = fieldName + " BETWEEN  '" + getQuaterDatesQ1["StartDate"] + " 00:00:00'  and '" + getQuaterDatesQ1["EndDate"] + " 23:59:59' ";
                    break;

                case "Q2":
                    Dictionary<string, string> getQuaterDatesQ2 = getQuaterDates(2);
                    output = fieldName + " BETWEEN  '" + getQuaterDatesQ2["StartDate"] + " 00:00:00'  and '" + getQuaterDatesQ2["EndDate"] + " 23:59:59' ";
                    break;


                case "Q3":
                    Dictionary<string, string> getQuaterDatesQ3 = getQuaterDates(3);
                    output = fieldName + " BETWEEN  '" + getQuaterDatesQ3["StartDate"] + " 00:00:00'  and '" + getQuaterDatesQ3["EndDate"] + " 23:59:59' ";
                    break;


                case "Q4":
                    Dictionary<string, string> getQuaterDatesQ4 = getQuaterDates(4);
                    output = fieldName + " BETWEEN  '" + getQuaterDatesQ4["StartDate"] + " 00:00:00'  and '" + getQuaterDatesQ4["EndDate"] + " 23:59:59' ";
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
        public static Dictionary<string, DateTime> getSelectedDates(string RequestSubmittedDateDD, string CustomStartDate, string CustomEndDate)
        {

            Dictionary<string, DateTime> DicDate = new Dictionary<string, DateTime>();
             
            int delta = 0;
            DateTime today = DateTime.Now;
            switch (RequestSubmittedDateDD)
            {
                case "Today":
                    //  output = fieldName + "='" + today.ToString("d") + "'";
                    //   output = fieldName + " BETWEEN  '" + today.ToString("d") + " 00:00:00'  and '" + today.ToString("d") + " 23:59:59' ";
                 //   DicDate.Add(today, today);
                    DicDate.Add("StartDate", today);
                    DicDate.Add("EndDate", today);
                    break;
                case "This Week":

                    delta = DayOfWeek.Sunday - today.DayOfWeek;
                    DateTime BeginSunday = today.AddDays(delta);
                    DateTime EndSaturday = BeginSunday.AddDays(6);
                    //  output = fieldName + " BETWEEN  '" + BeginSunday.ToString("d") + " 00:00:00'  and '" + EndSaturday.ToString("d") + " 23:59:59' ";
                  //  DicDate.Add(BeginSunday, EndSaturday);
                    DicDate.Add("StartDate", BeginSunday);
                    DicDate.Add("EndDate", EndSaturday);
                    break;

                case "Last Week":
                    delta = DayOfWeek.Sunday - today.DayOfWeek;
                    DateTime BeginLastWeekSunday = today.AddDays(delta).AddDays(-7);
                    DateTime EndLastWeekSaturday = BeginLastWeekSunday.AddDays(6);
                    //  output = fieldName + " BETWEEN  '" + BeginLastWeekSunday.ToString("d") + " 00:00:00'  and '" + EndLastWeekSaturday.ToString("d") + " 23:59:59' ";
                  //  DicDate.Add(BeginLastWeekSunday, EndLastWeekSaturday);
                    DicDate.Add("StartDate", BeginLastWeekSunday);
                    DicDate.Add("EndDate", EndLastWeekSaturday);
                    break;

                case "Last 30 Days":
                    DateTime Last30Days = today.AddDays(-30);
                    // output = fieldName + " BETWEEN  '" + Last30Days.ToString("d") + " 00:00:00'  and '" + today.ToString("d") + " 23:59:59' ";
                   // DicDate.Add(Last30Days, today);
                    DicDate.Add("StartDate", Last30Days);
                    DicDate.Add("EndDate", today);
                    break;

                case "This Month":

                    DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
                    DateTime firstOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);
                    DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);
                    // output = fieldName + " BETWEEN  '" + startOfMonth.ToString("d") + " 00:00:00'  and '" + lastOfThisMonth.ToString("d") + " 23:59:59' ";
                   // DicDate.Add(startOfMonth, lastOfThisMonth);
                    DicDate.Add("StartDate", startOfMonth);
                    DicDate.Add("EndDate", lastOfThisMonth);
                    break;

                case "Last Month":

                    DateTime BeginofLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
                    DateTime EndOfLastMonth = new DateTime(today.Year, today.Month, 1).AddDays(-1);
                    //    output = fieldName + " BETWEEN  '" + BeginofLastMonth.ToString("d") + " 00:00:00'  and '" + EndOfLastMonth.ToString("d") + " 23:59:59' ";
                   // DicDate.Add(BeginofLastMonth, EndOfLastMonth);
                    DicDate.Add("StartDate", Convert.ToDateTime(BeginofLastMonth));
                    DicDate.Add("EndDate", Convert.ToDateTime(EndOfLastMonth));
                    break;

                case "This Quarter":
                    Dictionary<string, string> getQuaterDates1 = getQuaterDates(GetQuarter(today));
                    //   output = fieldName + " BETWEEN  '" + getQuaterDates1["StartDate"] + " 00:00:00'  and '" + getQuaterDates1["EndDate"] + " 23:59:59' ";
                  //  DicDate.Add(Convert.ToDateTime(getQuaterDates1["StartDate"]), Convert.ToDateTime(getQuaterDates1["EndDate"]));
                    DicDate.Add("StartDate", Convert.ToDateTime(getQuaterDates1["StartDate"]));
                    DicDate.Add("EndDate", Convert.ToDateTime(getQuaterDates1["EndDate"]));
                    break;

                case "Last Quarter":
                    Dictionary<string, string> getQuaterDates2 = getQuaterDates(GetQuarter(today) - 1);
                    //    output = fieldName + " BETWEEN  '" + getQuaterDates2["StartDate"] + " 00:00:00'  and '" + getQuaterDates2["EndDate"] + " 23:59:59' ";
                   // DicDate.Add(Convert.ToDateTime(getQuaterDates2["StartDate"]), Convert.ToDateTime(getQuaterDates2["EndDate"]));
                    DicDate.Add("StartDate", Convert.ToDateTime(getQuaterDates2["StartDate"]));
                    DicDate.Add("EndDate", Convert.ToDateTime(getQuaterDates2["EndDate"]));
                    break;

                case "Q1":
                    Dictionary<string, string> getQuaterDatesQ1 = getQuaterDates(1);
                    //   output = fieldName + " BETWEEN  '" + getQuaterDatesQ1["StartDate"] + " 00:00:00'  and '" + getQuaterDatesQ1["EndDate"] + " 23:59:59' ";
                   // DicDate.Add(Convert.ToDateTime(getQuaterDatesQ1["StartDate"]), Convert.ToDateTime(getQuaterDatesQ1["EndDate"]));
                    DicDate.Add("StartDate", Convert.ToDateTime(getQuaterDatesQ1["StartDate"]));
                    DicDate.Add("EndDate", Convert.ToDateTime(getQuaterDatesQ1["EndDate"]));
                    break;

                case "Q2":
                    Dictionary<string, string> getQuaterDatesQ2 = getQuaterDates(2);
                    //    output = fieldName + " BETWEEN  '" + getQuaterDatesQ2["StartDate"] + " 00:00:00'  and '" + getQuaterDatesQ2["EndDate"] + " 23:59:59' ";
                   // DicDate.Add(Convert.ToDateTime(getQuaterDatesQ2["StartDate"]), Convert.ToDateTime(getQuaterDatesQ2["EndDate"]));
                    DicDate.Add("StartDate", Convert.ToDateTime(getQuaterDatesQ2["StartDate"]));
                    DicDate.Add("EndDate", Convert.ToDateTime(getQuaterDatesQ2["EndDate"]));
                    break;


                case "Q3":
                    Dictionary<string, string> getQuaterDatesQ3 = getQuaterDates(3);
                    //   output = fieldName + " BETWEEN  '" + getQuaterDatesQ3["StartDate"] + " 00:00:00'  and '" + getQuaterDatesQ3["EndDate"] + " 23:59:59' ";
                  //  DicDate.Add(Convert.ToDateTime(getQuaterDatesQ3["StartDate"]), Convert.ToDateTime(getQuaterDatesQ3["EndDate"]));
                    DicDate.Add("StartDate", Convert.ToDateTime(getQuaterDatesQ3["StartDate"]));
                    DicDate.Add("EndDate", Convert.ToDateTime(getQuaterDatesQ3["EndDate"]));
                    break;


                case "Q4":
                    Dictionary<string, string> getQuaterDatesQ4 = getQuaterDates(4);
                    //   output = fieldName + " BETWEEN  '" + getQuaterDatesQ4["StartDate"] + " 00:00:00'  and '" + getQuaterDatesQ4["EndDate"] + " 23:59:59' ";
                    //DicDate.Add(Convert.ToDateTime(getQuaterDatesQ4["StartDate"]), Convert.ToDateTime(getQuaterDatesQ4["EndDate"]));
                    DicDate.Add("StartDate", Convert.ToDateTime(getQuaterDatesQ4["StartDate"]));
                    DicDate.Add("EndDate", Convert.ToDateTime(getQuaterDatesQ4["EndDate"]));
                    break;


                case "This Fiscal Year":
                    Dictionary<string, string> FiscalDates = getFiscalDates(AdminFunc.CurrentFiscalYear(DateTime.Now));
                    //     output = fieldName + " BETWEEN  '" + FiscalDates["StartDate"] + " 00:00:00'  and '" + FiscalDates["EndDate"] + " 23:59:59' ";
                   // DicDate.Add(Convert.ToDateTime(FiscalDates["StartDate"]), Convert.ToDateTime(FiscalDates["EndDate"]));
                    DicDate.Add("StartDate", Convert.ToDateTime(FiscalDates["StartDate"]));
                    DicDate.Add("EndDate", Convert.ToDateTime(FiscalDates["EndDate"]));
                    break;


                case "Last Fiscal Year":
                    Dictionary<string, string> FiscalDates2 = getFiscalDates(AdminFunc.CurrentFiscalYear(DateTime.Now) - 1);
                    //     output = fieldName + " BETWEEN  '" + FiscalDates2["StartDate"] + " 00:00:00'  and '" + FiscalDates2["EndDate"] + " 23:59:59' ";
                    DicDate.Add("StartDate", Convert.ToDateTime(FiscalDates2["StartDate"]));
                    DicDate.Add("EndDate",   Convert.ToDateTime(FiscalDates2["EndDate"]));
                    break;

                case "Custom Dates":
                    //   output = fieldName + " BETWEEN  '" + CustomStartDate + " 00:00:00'  and '" + CustomEndDate + " 23:59:59' ";
                   // DicDate.Add(Convert.ToDateTime(CustomStartDate), Convert.ToDateTime(CustomEndDate));
                    DicDate.Add("StartDate", Convert.ToDateTime(CustomStartDate));
                    DicDate.Add("EndDate", Convert.ToDateTime(CustomEndDate));
                    break;

            }

            return DicDate;
        }
        public static int GetQuarter(DateTime date)
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
        public static Dictionary<string, string> getFiscalDates(int fiscalyear)
        {
            Dictionary<string, string> startEndDates = new Dictionary<string, string>();
            DateTime start = new DateTime(fiscalyear - 1, 10, 1);
            DateTime end = new DateTime(fiscalyear, 9, 1).AddMonths(1).AddDays(-1);
            startEndDates.Add("StartDate", start.ToString("d"));
            startEndDates.Add("EndDate", end.ToString("d"));
            return startEndDates;
        }
        public static Dictionary<string, string> getQuaterDates(int Quater)
        {
            Dictionary<string, string> startEndDates = new Dictionary<string, string>();
            int fiscealyear = AdminFunc.CurrentFiscalYear(DateTime.Now);
            switch (Quater)
            {
                case 1: //(date.Month >= 10 && date.Month <= 12)

                    DateTime start1 = new DateTime(fiscealyear - 1, 10, 1);
                    DateTime end1 = new DateTime(fiscealyear - 1, 12, 1).AddMonths(1).AddDays(-1);
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

        public static string getLastRunDate(string reportID)
        {
            string result = string.Empty;
            DataTableReader dtr = DataBase.dbDataTable("Select top 1 * from ReportInstances where ReportID='" + reportID + "' order by DateTimRun desc ").CreateDataReader();
            //Response.Write("Select top 1 * from ReportInstances where ReportID='" + reportID + "' order by DateTimRun desc ");
            if (dtr.HasRows)
            {
                dtr.Read();
                DateTime LastRunDate = Convert.ToDateTime(dtr["DateTimRun"].ToString());
                string Date = LastRunDate.ToString("d");
                string Time = LastRunDate.ToString("t");
                string PIN = dtr["RunbyPIN"].ToString();

                Dictionary<string, string> UserDetails = loginSSA.GetUsersDetails(PIN);
                result = "Run " + Date + " at " + Time + " by " + UserDetails["DisplayName"].ToString();

            }
            return result;
        }
        public static string getOrderBY(string DisplayByDDDB)
        {
            string result = string.Empty;
            switch (DisplayByDDDB)
            {
                case "Date":
                    result = "ClickedDateTime";
                    break;

                case "Month/Year":
                    result = " * ";
                    break;


                case "Application":
                    result = "ResourceName";
                    break;


                case "Mandatory/Discretionary":
                    result = "Mandatory";
                    break;

                case "Office":
                    result = "ClickedByOffice";
                    break;

                case "Server":
                    result = "ClickedByServer";
                    break;


                case "Domain":
                    result = "ClickedByUserDomain";
                    break;

                case "Email":
                    result = "ClickedByEMail";
                    break;

                case "PIN":
                    result = "ClickedByPIN";
                    break;


                case "Last Name":
                    result = "ClickedByLastName";
                    break;

                case "First Name":
                    result = "ClickedByFirstName";
                    break;


            }

            return "order by " + result + " ASC";
        }

        public static string getDisplayCount(string DisplayDDDB)
        {
            string result = string.Empty;
            switch (DisplayDDDB)
            {
                case "All":
                    result = " * ";
                    break;

                case "All repeat users":
                    result = " * ";
                    break;


                case "The top 100 percent":
                    result = " top 100 PERCENT * ";
                    break;


                case "The top 50 percent":
                    result = "  top 50 PERCENT * ";
                    break;


                case "The top 500":
                    result = " top 500 * ";
                    break;


                case "The top 250":
                    result = " top 250 * ";
                    break;

                case "The top 100":
                    result = " top 100 * ";
                    break;


            }

            return result;
        }


        public static string getAccessTypeName(string coreServicesAccessType)
        {
            string allNames = "";


            string[] taxIDs = coreServicesAccessType.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string taxId in taxIDs)
            {
                try
                {
                    string taxId2 = taxId.TrimEnd('\'').TrimStart('\'');
                    allNames += commonfunctions.GetTaxonomyNameFromID(long.Parse(taxId2)) + ",";

                }
                catch { }
            }

            return allNames.TrimEnd(',');
        }

        public static string getResourcesName(string Resources)
        {
            string allNames = "";
            DataTableReader dtr = DataBase.dbDataTable("Select   ResourceName from Resources where ID in (" + Resources + ")").CreateDataReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    allNames += dtr["ResourceName"].ToString() + ",";
                }
            }

            return allNames.TrimEnd(',');
        }





    }
}