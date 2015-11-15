using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;
using System.Web;
using Ektron.Cms.Framework.User;
using Ektron.Cms;
using System.Data;
using System.Net;
using System.Xml;
using TaskScheduler;

public partial class Templates_Default5 : System.Web.UI.Page
{

    // Run in every 1 hour
    //private void CreateTaskRunRepeatly()
    //{
    //    // Run daily
    //    using (TaskService ts = new TaskService())
    //    {
    //        TaskDefinition td = ts.NewTask();
    //        td.RegistrationInfo.Description = "My first task scheduler";

    //        TimeTrigger trigger = new TimeTrigger();
    //        trigger.StartBoundary = DateTime.Now;
    //        trigger.Repetition.Interval = TimeSpan.FromMinutes(60);
    //        td.Triggers.Add(trigger);

    //        td.Actions.Add(new ExecAction(@"C:/sample.exe", null, null));
    //        ts.RootFolder.RegisterTaskDefinition("TaskName", td);
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

        printUsersDetails(Request.QueryString["pin"].ToString());
      //  HttpContext.Current.Response.Cookies["User"].Expires = DateTime.Now;

        //  WebRequest request = WebRequest.Create(url);
        // WebResponse response = request.GetResponse();

        //create task service instance
        //ITaskService taskService = new TaskSchedulerClass();
        //taskService.Connect();
        //ITaskDefinition taskDefinition = taskService.NewTask(0);
        //taskDefinition.Settings.Enabled = true;
        //taskDefinition.Settings.Compatibility = _TASK_COMPATIBILITY.TASK_COMPATIBILITY_V2_1;

        ////create trigger for task creation.
        //ITriggerCollection _iTriggerCollection = taskDefinition.Triggers;
        ////ITrigger _trigger = _iTriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME);
        ////_trigger.StartBoundary = DateTime.Now.AddSeconds(15).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
        ////_trigger.EndBoundary = DateTime.Now.AddMinutes(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
        ////_trigger.Enabled = true;


        //IDailyTrigger tt = (IDailyTrigger)_iTriggerCollection.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_DAILY);
        //tt.DaysInterval = 14;
        //tt.StartBoundary = DateTime.Now.AddSeconds(15).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
        //tt.EndBoundary = DateTime.Now.AddMinutes(1).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
        //tt.Enabled = true;


        /////get actions.
        //IActionCollection actions = taskDefinition.Actions;
        //_TASK_ACTION_TYPE actionType = _TASK_ACTION_TYPE.TASK_ACTION_EXEC;

        ////create new action
        //IAction action = actions.Create(actionType);
        //IExecAction execAction = action as IExecAction;
        //execAction.Path = @"C:\Windows\System32\notepad.exe";
        //ITaskFolder rootFolder = taskService.GetFolder(@"\SSADL");

        ////register task.
        //rootFolder.RegisterTaskDefinition("test", taskDefinition, Convert.ToInt16(_TASK_CREATION.TASK_CREATE_OR_UPDATE), "administrator", "P@ssw0rd", _TASK_LOGON_TYPE.TASK_LOGON_PASSWORD, null);




































        //rootFolder.RegisterTaskDefinition( "Test Registration Trigger", taskDefinition, 6, , , 3);
        //rootFolder.RegisterTaskDefinition(
        // ITaskService taskService = new TaskSchedulerClass();
        // taskService.Connect();
        // ITaskDefinition taskDefinition = taskService.NewTask(0);
        // taskDefinition.RegistrationInfo.Description = "Test from Site";
        // taskDefinition.RegistrationInfo.Author = "Femi";
        // taskDefinition.Settings.Enabled = true;
        //// taskDefinition.Settings.Hidden = "true";
        // taskDefinition.Settings.Hidden = false;
        // taskDefinition.Settings.Compatibility = _TASK_COMPATIBILITY.TASK_COMPATIBILITY_V2_1;
        // TimeSpan timespan = TimeSpan.FromMinutes(15);
        // taskDefinition.Settings.ExecutionTimeLimit = XmlConvert.ToString(timespan);




        //         string userFullDetails = HttpContext.Current.User.Identity.Name;
        //                string[] userDetails = userFullDetails.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
        //                string PIN = userDetails[1].ToString();
        //               string domain = userDetails[0].ToString();
        //        string EMAIL = string.Empty;
        //                string CorePIN = "";
        //                // Dictionary<string, string> UserDetails = new Dictionary<string, string>();
        //                string url = "";
        //                string urlgetPIN = "";
        //                //string sql = "SELECT * , CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [AccurintUsers] where PIN='" + PIN + "' ";
        //                if (PIN != "")
        //                {
        //                    urlgetPIN = coreEmailLink + "?REQTYPE=GETALL&PIN=" + PIN;
        //                }
        //                else if (PIN == "" && EMAIL != "")
        //                {
        //                    //lets get pin from here                
        //                    urlgetPIN = coreEmailLink + "?REQTYPE=GETPIN&EMAIL=" + EMAIL;
        //                }

        //                //  HttpContext.Current.Response.Write(urlgetPIN);

        //         WebRequest requestEMAIL = WebRequest.Create(urlgetPIN);
        //                WebResponse responseEMAIL = requestEMAIL.GetResponse();

        //                XmlDocument xmlDocEMAIL = new XmlDocument();
        //                xmlDocEMAIL.Load(responseEMAIL.GetResponseStream());

        //        // HttpContext.Current.Response.Write(xmlDocEMAIL.InnerXml);


        //                        CorePIN = commonfunctions.getFieldValue(xmlDocEMAIL, "pin", "/service");



        //                if (CorePIN != "")
        //                {
        //                    //url = coreEmailLink + "?REQTYPE=GETALL&PIN=" + CorePIN;
        //                    //WebRequest request = WebRequest.Create(url);
        //                    //WebResponse response = request.GetResponse();

        //                    //XmlDocument xmlDoc = new XmlDocument();
        //                    //xmlDoc.Load(response.GetResponseStream());






        //                }
        //         string urlD = coreIVFLink + "?PIN=" + PIN + "&Application=SDL";
        //         //HttpContext.Current.Response.Write(urlD);
        //                    WebRequest requestD = WebRequest.Create(urlD);
        //                    WebResponse responseD = requestD.GetResponse();
        //                     XmlDocument xmlDocD = new XmlDocument();
        //                    xmlDocD.Load(responseD.GetResponseStream());

        //        // HttpContext.Current.Response.Write(xmlDocD.InnerXml);
        //                    string ocd = commonfunctions.getFieldValue(xmlDocD, "ocd", "/service/data");
        //        // HttpContext.Current.Response.Write(ocd);
        //                     string urlDDS = coreOLVLink + "?OFFICECODEA=" + ocd + "&QUERYTYPE=1&APPLICATION=SSA_Digital_Library";
        //                    WebRequest requestDDS = WebRequest.Create(urlDDS);
        //                    WebResponse responseDDS = requestDDS.GetResponse();

        //                    XmlDocument xmlDocDDS = new XmlDocument();
        //                    xmlDocDDS.Load(responseDDS.GetResponseStream());
        //                     HttpContext.Current.Response.Write(xmlDocDDS.InnerXml);

        ////        Dictionary<string, string> GetUsersDetails1 = GetUsersDetails("133439");
        ////       // Response.Write(GetUsersDetails1["office"].ToString());
        ////        string officc = GetUsersDetails1["office"].ToString();
        ////         string dept = GetUsersDetails1["Department"].ToString();


        ////        string[] officcee = officc.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
        ////        if(officcee.Length == 3){
        ////            Response.Write(officcee + " 3");
        ////            //dont do anything
        ////        } else  {

        //Response.Write(dept + "/" + officc+ "/" + officcee.Length.ToString());
        //        }

    }


    //<add key="coreEmailLink" value="http://coreEmail.ba.ssa.gov/emailcore/EmailCoreService" />
    // <add key="coreIVFLink" value="http://coreIVF.ba.ssa.gov/services/IVFCoreService" />
    // <add key="coreOLVLink" value="http://coreOL.ba.ssa.gov/olcore/OfficeLookupCoreService" />

    //  <!--
    // <add key="coreEmailLink" value="http://coreemailval.sspf.ssa.gov/emailcore/EmailCoreService" />
    // <add key="coreIVFLink" value="http://coreivfval.sspf.ssa.gov/services/IVFCoreService" />
    // <add key="coreOLVLink" value="http://coreolval.sspf.ssa.gov/olcore/OfficeLookupCoreService" />-->


    public void printUsersDetails(string PIN, string EMAIL = "")
    {
        Dictionary<string, string> UserDetails = new Dictionary<string, string>();
        if (commonfunctions.Environment == "FIGLEAF")
        {

            string sql = "SELECT top 1 *   FROM [AccurintUsers] where PIN='" + PIN + "' or EMAIL='" + EMAIL + "' ";


            try
            {
                DataTableReader reader = DataBase.dbDataTable(sql).CreateDataReader();
                reader.Read();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    UserDetails.Add(reader.GetName(i), reader[i].ToString().Trim());

                }
                reader.Close();
            }
            catch { }


        }
        else
        {


            string CorePIN = "";
            // Dictionary<string, string> UserDetails = new Dictionary<string, string>();
            string url = "";
            string urlgetPIN = "";
            //string sql = "SELECT * , CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [AccurintUsers] where PIN='" + PIN + "' ";
            if (PIN != "")
            {
                urlgetPIN = coreEmailLink + "?REQTYPE=GETALL&PIN=" + PIN;
            }
            else if (PIN == "" && EMAIL != "")
            {
                //lets get pin from here                
                urlgetPIN = coreEmailLink + "?REQTYPE=GETPIN&EMAIL=" + EMAIL;
            }
            WebRequest requestEMAIL = WebRequest.Create(urlgetPIN);
            WebResponse responseEMAIL = requestEMAIL.GetResponse();

            XmlDocument xmlDocEMAIL = new XmlDocument();
            xmlDocEMAIL.Load(responseEMAIL.GetResponseStream());

            CorePIN = commonfunctions.getFieldValue(xmlDocEMAIL, "pin", "/service");



            if (CorePIN != "")
            {
                url = coreEmailLink + "?REQTYPE=GETALL&PIN=" + CorePIN;
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());

                string displayName = commonfunctions.getFieldValue(xmlDoc, "displayName", "/service");
                string email = commonfunctions.getFieldValue(xmlDoc, "email", "/service");
                string office = commonfunctions.getFieldValue(xmlDoc, "office", "/service");
                string title = commonfunctions.getFieldValue(xmlDoc, "title", "/service");
                string telephone = commonfunctions.getFieldValue(xmlDoc, "telephone", "/service");
                string EmailDepartment = commonfunctions.getFieldValue(xmlDoc, "department", "/service");
                string UPN = commonfunctions.getFieldValue(xmlDoc, "UPN", "/service");
                string PINN = commonfunctions.getFieldValue(xmlDoc, "pin", "/service");

               

                string urlD = coreIVFLink + "?PIN=" + CorePIN + "&Application=Test";
                WebRequest requestD = WebRequest.Create(urlD);
                WebResponse responseD = requestD.GetResponse();

                XmlDocument xmlDocD = new XmlDocument();
                xmlDocD.Load(responseD.GetResponseStream());
                string ocd = commonfunctions.getFieldValue(xmlDocD, "ocd", "/service/data");
                string department = commonfunctions.getFieldValue(xmlDocD, "dept", "/service/data");
                string firstName = commonfunctions.getFieldValue(xmlDocD, "fnm", "/service/data");
                string lastName = commonfunctions.getFieldValue(xmlDocD, "lnm", "/service/data");

                string urlDDS = coreOLVLink + "?OFFICECODEA=" + ocd + "&QUERYTYPE=1&APPLICATION=DDL_WEBSITE";
                WebRequest requestDDS = WebRequest.Create(urlDDS);
                WebResponse responseDDS = requestDDS.GetResponse();
               
                XmlDocument xmlDocDDS = new XmlDocument();
                xmlDocDDS.Load(responseDDS.GetResponseStream());
                string dj9OfcTypeTxt = commonfunctions.getFieldValue(xmlDocDDS, "dj9OfcTypeTxt", "/service/olData");  ///If this is DDS then the user is a DDS member
                 //Response.Write(xmlDocDDS.InnerXml);
                 aaa.Text = xmlDocDDS.InnerXml;

                // string[] Names = displayName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                string Component = String.Empty;
                try
                {
                    string[] officcee = office.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    if (officcee.Length == 3)
                    {
                        Component = office;

                    }
                    else
                    {
                        Component = EmailDepartment + "/" + office;
                    }
                }
                catch { }

                try
                {
                    string[] Names = displayName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    displayName = Names[1] + " " + Names[0];
               //     lastNameCore = Names[0];
                 //   firstNameCore = Names[1];
                }
                catch { }


      

                Response.Write("displayName:" + displayName + "<br/>");
                Response.Write("email:" + email + "<br/>");
                Response.Write("office:" + office + "<br/>");
                Response.Write("title:" + title + "<br/>");
                Response.Write("telephone:" + telephone + "<br/>");
                Response.Write("EmailDepartment:" + EmailDepartment + "<br/>");
                Response.Write("UPN:" + UPN + "<br/>");
                Response.Write("PINN:" + PINN + "<br/>");
                Response.Write("ocd:" + ocd + "<br/>");
                Response.Write("department:" + department + "<br/>"); 
                Response.Write("firstName:" + firstName + "<br/>");
                Response.Write("lastName:" + lastName + "<br/>");
                Response.Write("DDS:" + dj9OfcTypeTxt + "<br/>");
                //Response.Write("aaa", aaa + "<br/>");
                //Response.Write("aaa", aaa + "<br/>");
                //Response.Write("aaa", aaa + "<br/>");
                //Response.Write("aaa", aaa + "<br/>");
                //Response.Write("aaa", aaa + "<br/>");
                //Response.Write("aaa", aaa + "<br/>"); Response.Write("aaa", aaa + "<br/>");
                //Response.Write("aaa", aaa + "<br/>");
                //Response.Write("aaa", aaa + "<br/>");































                ////  displayName = Names[1] + " " + Names[0];
                //if (displayName == "") { UserDetails.Add("DisplayName", CorePIN); } else { UserDetails.Add("DisplayName", displayName); }
                //if (email == "") { UserDetails.Add("Email", "Anonymous"); } else { UserDetails.Add("Email", email); }
                //if (ocd == "") { UserDetails.Add("OfficeCode", "Anonymous"); } else { UserDetails.Add("OfficeCode", ocd); }
                //if (title == "") { UserDetails.Add("Title", "Anonymous"); } else { UserDetails.Add("Title", title); }
                //if (telephone == "") { UserDetails.Add("Telephone", "Anonymous"); } else { UserDetails.Add("Telephone", telephone); }
                //if (EmailDepartment == "") { UserDetails.Add("Department", "Anonymous"); } else { UserDetails.Add("Department", EmailDepartment); }
                //if (UPN == "") { UserDetails.Add("UPN", "Anonymous"); } else { UserDetails.Add("UPN", UPN); }
                //if (dj9OfcTypeTxt == "") { UserDetails.Add("DDS", "Anonymous"); } else { UserDetails.Add("DDS", dj9OfcTypeTxt); }
                //if (lastName == "") { UserDetails.Add("LastName", "Anonymous"); } else { UserDetails.Add("LastName", lastName); }
                //if (firstName == "") { UserDetails.Add("FirstName", "Anonymous"); } else { UserDetails.Add("FirstName", firstName); }
                ////if (getMYIP() == "") { UserDetails.Add("Server", "Anonymous"); } else { UserDetails.Add("Server", getMYIP()); }
                ////  if (Domain == "") { UserDetails.Add("Email", email); } else { U }
                //if (PINN == "") { UserDetails.Add("PIN", "Anonymous"); } else { UserDetails.Add("PIN", PINN); }
                //if (Component == "") { UserDetails.Add("Component", "Anonymous"); } else { UserDetails.Add("Component", Component); }

                //UserDetails.Add("Domain", "Domain");












            }
        }

       // return UserDetails;

    }
    public string coreEmailLink = System.Configuration.ConfigurationManager.AppSettings["coreEmailLink"].ToString();
    public string coreIVFLink = System.Configuration.ConfigurationManager.AppSettings["coreIVFLink"].ToString();
    public string coreOLVLink = System.Configuration.ConfigurationManager.AppSettings["coreOLVLink"].ToString();
    public Dictionary<string, string> GetUsersDetails(string PIN, string EMAIL = "")
    {
        Dictionary<string, string> UserDetails = new Dictionary<string, string>();
        if (commonfunctions.Environment == "FIGLEAF")
        {

            string sql = "SELECT top 1 *   FROM [AccurintUsers] where PIN='" + PIN + "' or EMAIL='" + EMAIL + "' ";


            try
            {
                DataTableReader reader = DataBase.dbDataTable(sql).CreateDataReader();
                reader.Read();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    UserDetails.Add(reader.GetName(i), reader[i].ToString().Trim());

                }
                reader.Close();
            }
            catch { }


        }
        else
        {

            //string coreEmailLink = "";
            //string coreIVFLink = "";
            //string coreOLVLink = "";
            //if (commonfunctions.Environment == "DEV")
            //{
            //    coreEmailLink = "http://coreemailval.sspf.ssa.gov/emailcore/EmailCoreService";
            //    coreIVFLink = "http://coreivfval.sspf.ssa.gov/services/IVFCoreService";
            //    coreOLVLink = "http://coreolval.sspf.ssa.gov/olcore/OfficeLookupCoreService";
            //}
            //if (commonfunctions.Environment == "PROD")
            //{
            //   // coreEmailLink = "http://coreEmail.ba.ssa.gov/emailcore/EmailCoreService"; ////Not accepting this
            //   // coreIVFLink = "http://coreIVF.ba.ssa.gov/services/IVFCoreService";   //Not accepting this
            //    coreOLVLink = "http://coreOL.ba.ssa.gov/olcore/OfficeLookupCoreService";

            //   coreEmailLink = "http://coreemailval.sspf.ssa.gov/emailcore/EmailCoreService";
            //     coreIVFLink = "http://coreivfval.sspf.ssa.gov/services/IVFCoreService";
            //   // coreOLVLink = "http://coreolval.sspf.ssa.gov/olcore/OfficeLookupCoreService";


            //}



            string CorePIN = "";
            // Dictionary<string, string> UserDetails = new Dictionary<string, string>();
            string url = "";
            string urlgetPIN = "";
            //string sql = "SELECT * , CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [AccurintUsers] where PIN='" + PIN + "' ";
            if (PIN != "")
            {
                urlgetPIN = coreEmailLink + "?REQTYPE=GETALL&PIN=" + PIN;
            }
            else if (PIN == "" && EMAIL != "")
            {
                //lets get pin from here                
                urlgetPIN = coreEmailLink + "?REQTYPE=GETPIN&EMAIL=" + EMAIL;
            }
            WebRequest requestEMAIL = WebRequest.Create(urlgetPIN);
            WebResponse responseEMAIL = requestEMAIL.GetResponse();

            XmlDocument xmlDocEMAIL = new XmlDocument();
            xmlDocEMAIL.Load(responseEMAIL.GetResponseStream());

            CorePIN = commonfunctions.getFieldValue(xmlDocEMAIL, "pin", "/service");



            if (CorePIN != "")
            {
                url = coreEmailLink + "?REQTYPE=GETALL&PIN=" + CorePIN;
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());

                string displayName = commonfunctions.getFieldValue(xmlDoc, "displayName", "/service");
                string email = commonfunctions.getFieldValue(xmlDoc, "email", "/service");
                string office = commonfunctions.getFieldValue(xmlDoc, "office", "/service");
                string title = commonfunctions.getFieldValue(xmlDoc, "title", "/service");
                string telephone = commonfunctions.getFieldValue(xmlDoc, "telephone", "/service");
                string EmailDepartment = commonfunctions.getFieldValue(xmlDoc, "department", "/service");
                string UPN = commonfunctions.getFieldValue(xmlDoc, "UPN", "/service");
                string PINN = commonfunctions.getFieldValue(xmlDoc, "pin", "/service");



                string urlD = coreIVFLink + "?PIN=" + CorePIN + "&Application=Test";
                WebRequest requestD = WebRequest.Create(urlD);
                WebResponse responseD = requestD.GetResponse();

                XmlDocument xmlDocD = new XmlDocument();
                xmlDocD.Load(responseD.GetResponseStream());
                string ocd = commonfunctions.getFieldValue(xmlDocD, "ocd", "/service/data");
                string department = commonfunctions.getFieldValue(xmlDocD, "dept", "/service/data");
                string firstName = commonfunctions.getFieldValue(xmlDocD, "fnm", "/service/data");
                string lastName = commonfunctions.getFieldValue(xmlDocD, "lnm", "/service/data");

                string urlDDS = coreOLVLink + "?OFFICECODEA=" + ocd + "&QUERYTYPE=1&APPLICATION=VALTEST";
                WebRequest requestDDS = WebRequest.Create(urlDDS);
                WebResponse responseDDS = requestDDS.GetResponse();

                XmlDocument xmlDocDDS = new XmlDocument();
                xmlDocDDS.Load(responseDDS.GetResponseStream());
                string dj9OfcTypeTxt = commonfunctions.getFieldValue(xmlDocDDS, "dj9OfcTypeTxt", "/service");  ///If this is DDS then the user is a DDS member



                string[] Names = displayName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                UserDetails.Add("DisplayName", displayName);
                UserDetails.Add("Email", email);
                UserDetails.Add("OfficeCode", ocd);
                UserDetails.Add("Title", title);
                UserDetails.Add("Telephone", telephone);
                UserDetails.Add("Department", EmailDepartment);
                UserDetails.Add("UPN", UPN);
                UserDetails.Add("DDS", dj9OfcTypeTxt);
                UserDetails.Add("LastName", Names[0]);
                UserDetails.Add("FirstName", Names[1]);
                // UserDetails.Add("Server", getMYIP());
                UserDetails.Add("Domain", "Domain");
                UserDetails.Add("PIN", PINN);
                UserDetails.Add("Component", department);
                UserDetails.Add("office", office);


            }
        }

        return UserDetails;

    }



    public DateTime convertTimeToDateTime(string time)
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
}