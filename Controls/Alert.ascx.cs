using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Net.Sockets;
public partial class Controls_Alert : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
        //////// if not admin user 
         checkSiteMaintenance();
       

        //see if I need to show alert

        string NewMessage = DataBase.returnOneValue("Select TOP 1 NewMessage from UserNotificationDismissal where UserPIN = '" + loginSSA.myPIN + "' order by SiteWideNotificationLastDismissedDateTime DESC ");
         //Response.Write(NewMessage);
         if (NewMessage.Trim() == "Y" || NewMessage.Trim() == "")
         {
             ///////////////////alert things
                 getAlert();     
         }
         
    }

    private void getAlert()
    {
        DataTableReader dtR = DataBase.dbDataTable("Select * from SystemSettings").CreateDataReader();

        while (dtR.Read())
        {
            ///Alert
            ///
             
            string value = dtR["SettingValue"].ToString().Trim();
            string key = dtR["SettingName"].ToString().Trim();
            switch (key)
            {
                case "AlertTitle":
                    AlertTitle = value;
                    break;
                case "AlertDescription":
                    AlertDescription = value;
                    break;

                case "AlertStartOn":
                    AlertStartOn = value;
                    break;
                case "AlertEndOn":
                    AlertEndOn = value;
                    break;

                case "AlertActive":
                    AlertActive = value;
                    break;

            }

        }


        if (AlertActive == "Y")
        {
            DateTime startDate = Convert.ToDateTime(AlertStartOn);
            DateTime expireddate = Convert.ToDateTime(AlertEndOn);
            DateTime nowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            int result = DateTime.Compare(nowDate, expireddate);
            int seresult = DateTime.Compare(nowDate, startDate);
            // Response.Write(seresult.ToString() + " " + AlertEndOn + " " + DateTime.Now.ToShortDateString());

            if (result < 0 || result == 0) //it has not expired  or today
            {

                
                if (seresult > 0 || seresult == 0)
                { 
                    alertPanel.Visible = true;
                }

            }

        }
    }
    public   void checkSiteMaintenance()
    {

        DataTableReader dtR_SiteMT = DataBase.dbDataTable("Select * from SiteMaintenance where ID='1'").CreateDataReader();

        while (dtR_SiteMT.Read())
        {
            string StartTime = dtR_SiteMT["StartTime"].ToString().Trim();
            string EndTime = dtR_SiteMT["EndTime"].ToString().Trim();
            string MessageToDisplay = dtR_SiteMT["MessageToDisplay"].ToString().Trim();
            string DaysOfWeek = dtR_SiteMT["DaysOfWeek"].ToString().Trim();
            string MessageTitle = dtR_SiteMT["MessageTitle"].ToString().Trim();
            string Enable = dtR_SiteMT["Enable"].ToString().Trim();
            int day = (int)DateTime.Now.DayOfWeek;

            string[] selectedDays = DaysOfWeek.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            bool DisplayThisDay = false;
            foreach (string dayS in selectedDays)
            {
                if (day.ToString() == dayS)
                {
                    DisplayThisDay = true;
                }
            }



            //  Response.Write( convertTimeToDateTime(EndTime).ToLongDateString());
            if (Enable == "Y" && DisplayThisDay)
            {
                //lets checkTime
                DateTime startDate = convertTimeToDateTime(StartTime);
                DateTime expireddate = convertTimeToDateTime(EndTime);
                DateTime nowDate = DateTime.Now;

                int result = DateTime.Compare(nowDate, expireddate);
                int seresult = DateTime.Compare(nowDate, startDate);
                // Response.Write(expireddate.ToShortTimeString() + " " + startDate.ToLongDateString() + " " + DateTime.Now.ToShortTimeString());

                if (result < 0 || result == 0) //it has not expired  or today
                {


                    if (seresult > 0 || seresult == 0)
                    {
                        //Response.Write("Shut down");
                        //HttpContext.Current.Response.StatusCode = 301;
                        //HttpContext.Current.Response.Status = "301 Moved Permanently";
                       // HttpContext.Current.Response.RedirectLocation = "/sitemaintenance.aspx";
                        Response.Redirect("/sitemaintenance.aspx");
                        //HttpContext.Current.Response.End();
                    }

                }

            }

        }
    }

    public   DateTime convertTimeToDateTime(string time)
    {

        string[] Time = time.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        string Hr = Time[0].ToString().Trim();
        string mm = Time[1].ToString().Trim();
        string AMPM = Time[2].ToString().Trim();
        string timetouse = Hr + ":" + mm + ":" + "00" + " " + AMPM;

        string myDateString = DateTime.Now.ToShortDateString() + " " + timetouse; //  "2/17/2011 6:46:01 PM";
        DateTime dt = DateTime.Parse(myDateString);
        //string timeString = datetime.ToShortTimeString();

        return dt;
    }

    protected void Oklink_Click(object sender, EventArgs e)
    {
    }



 


    public string AlertStartOn { get; set; }

    public string AlertTitle { get; set; }

    public string AlertMessage { get; set; }

    public string AlertEndOn { get; set; }

    public string AlertActive { get; set; }

    public string OutageTitle { get; set; }

    public string OutageMessage { get; set; }

    public string OutageActive { get; set; }

    public string AlertImageSRC { get; set; }

    public string AlertImageALT { get; set; }


    public string OutageDescription { get; set; }

    public string AlertDescription { get; set; }
}