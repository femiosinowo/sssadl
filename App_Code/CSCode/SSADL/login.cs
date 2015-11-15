using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms.Framework.User;
using Ektron.Cms;
using System.Data;
using System.Net;
using System.Xml;
 

using System.Net.Sockets;
/// <summary>
/// Summary description for login
/// </summary>
namespace SSADL.CMS
{

    public class loginSSA
    {
        //
        // TODO: Add constructor logic here
        //
         

        //   static string myPIN;
        public static string myPIN
        {
            get
            {
                try
                {
                    return (string)HttpContext.Current.Request.Cookies["User"]["PIN"];
                }
                catch
                {
                    loginSSA a = new loginSSA();
                    a.createCookies();
                    return (string)HttpContext.Current.Request.Cookies["User"]["PIN"];
                }
            }
            //set
            //{
            //    return (string)HttpContext.Current.Request.Cookies["User"]["PIN"];
            //}
        }

        // static string myLastName;
        public static string myLastName
        {
            //get
            //{
            //    return myLastName;
            //}
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["LastName"];
            }
        }
        //static string myFirstName;
        public static string myFirstName
        {
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["FirstName"];
            }
            //set
            //{
            //    myFirstName = (string)HttpContext.Current.Request.Cookies["User"]["FirstName"];
            //}
        }

        // static string myEmail;
        public static string myEmail
        {
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["EMail"];
            }
            //set
            //{
            //    myEmail = (string)HttpContext.Current.Request.Cookies["User"]["EMail"];
            //}
        }

        //  static string myOffice;
        public static string myOffice
        {
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["Office"];
            }
            //set
            //{
            //    myOffice = (string)HttpContext.Current.Request.Cookies["User"]["Office"];
            //}
        }
        //   static string myServer;
        public static string myServer
        {
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["Server"];
            }
            //set
            //{
            //    myServer = (string)HttpContext.Current.Request.Cookies["User"]["Server"];
            //}
        }

        //  static string myUserDomain;
        public static string myUserDomain
        {
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["UserDomain"];
            }
            //set
            //{
            //    myUserDomain = (string)HttpContext.Current.Request.Cookies["User"]["UserDomain"];
            //}
        }

        //    static string myPhone;
        public static string myPhone
        {
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["Phone"];
            }
            //set
            //{
            //    myPhone = (string)HttpContext.Current.Request.Cookies["User"]["Phone"];
            //}
        }

        // static string myComponent;
        public static string myComponent
        {
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["Component"];
            }
            //set
            //{
            //    myComponent = (string)HttpContext.Current.Request.Cookies["User"]["Component"];
            //}
        }
        // static string myTitle;
        public static string myTitle
        {
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["Title"];
            }
            //set
            //{
            //    myTitle = (string)HttpContext.Current.Request.Cookies["User"]["Title"];
            //}
        }
        public static string myDDS
        {
            get
            {
                return (string)HttpContext.Current.Request.Cookies["User"]["DDS"];
            }
            //set
            //{
            //    myTitle = (string)HttpContext.Current.Request.Cookies["User"]["Title"];
            //}
        }

        // public static string myPIN { get; set; } // = (string)HttpContext.Current.Request.Cookies["User"]["PIN"];
        //  public static string myLastName { get; set; } //= (string)HttpContext.Current.Request.Cookies["User"]["LastName"];
        // public static string myFirstName { get; set; } //= (string)HttpContext.Current.Request.Cookies["User"]["FirstName"];
        //  public static string myEmail { get; set; } //= (string)HttpContext.Current.Request.Cookies["User"]["EMail"];

        // public static string myOffice { get; set; } //= (string)HttpContext.Current.Request.Cookies["User"]["Office"];
        //  public static string myServer { get; set; } //= (string)HttpContext.Current.Request.Cookies["User"]["Server"];
        //  public static string myUserDomain { get; set; } //= (string)HttpContext.Current.Request.Cookies["User"]["UserDomain"];
        //  public static string myPhone { get; set; }

        //   public static string myComponent { get; set; }

        // public static string myTitle { get; set; }



        public static string coreEmailLink = System.Configuration.ConfigurationManager.AppSettings["coreEmailLink"].ToString();
        public static string coreIVFLink = System.Configuration.ConfigurationManager.AppSettings["coreIVFLink"].ToString();
        public static string coreOLVLink = System.Configuration.ConfigurationManager.AppSettings["coreOLVLink"].ToString();


        public static string WorkAreadPassword = "Ektron0249"; //{ get; set; }
        public static string WorkAreadUsername = "Admin"; //{ get; set; }
        public string sSession { get; set; }

        public static string EktronMemberDefaultPassword = "SSAMemberLog1nPassw0rd";
        public static void logInSSAMembersToEktron()
        {
            if (commonfunctions.Environment == "DEV")
            {
                if (isAdminUser() && !Ektron.Cms.Framework.Context.UserContextService.Current.IsLoggedIn)
                {
                    string userID = loginSSA.myPIN; // (string)HttpContext.Current.Request.Cookies["User"]["PIN"] ?? "None";

                    if (userID != "None")
                    {
                        try
                        {
                            UserManager Usermanager = new UserManager();
                            UserData userdata = Usermanager.Login(userID, EktronMemberDefaultPassword);
                        }
                        catch
                        {

                            //Response.Write(getEmail(userID));
                            //create user
                            createSSAEktronMember(userID);

                        }
                    }


                }
            }
        }
        public static bool isAdminUser()
        {

            string myAdminID = AdminFunc.getUserDBID(loginSSA.myPIN);
            if (myAdminID != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isLoggedIn()
        {
            try
            {

                //sSession = (string)HttpContext.Current.Session["aid"] ?? "None";
                string sSession = (string)HttpContext.Current.Request.Cookies["User"]["PIN"] ?? "None";
                if (sSession != "None")
                {

                    return true;
                }
                else
                {

                    return false;
                }

            }
            catch { return false; }
        }


        public static void createSSAEktronMember(string PIN)
        {
            //try
            //{

            Dictionary<string, string> UserDetails = loginSSA.GetUsersDetails(PIN);


            UserManager Usermanager = new UserManager();
            CustomAttributeList attrList = new CustomAttributeList();
            CustomAttribute timeZone = new CustomAttribute();
            timeZone.Name = "Time Zone";
            timeZone.Value = "Eastern Standard Time";
            attrList.Add(timeZone);

            UserData newUserdata = new UserData()
            {
                Username = PIN,
                Password = EktronMemberDefaultPassword,
                FirstName = UserDetails["FirstName"],
                LastName = UserDetails["LastName"],
                DisplayName = UserDetails["DisplayName"],
                Email = UserDetails["Email"],
                CustomProperties = attrList,
                // IsMemberShip = true
            };

            if (Ektron.Cms.Framework.Context.UserContextService.Current.IsLoggedIn)
            {
                Usermanager.Add(newUserdata);
                // add user to group MSBA Members
                UserGroupManager UserGroupmanager = new UserGroupManager();
                //Add a User  to a UserGroup
                UserGroupmanager.AddUser(1, newUserdata.Id);
            }
            //}
            //catch { }
        }

        public void createCookies()
        {
            string pin = "";
            string domain = "";
            if (commonfunctions.Environment == "FIGLEAF")
            {
                pin = "067736"; 
            }
            else
            {
                string userFullDetails = HttpContext.Current.User.Identity.Name;
                string[] userDetails = userFullDetails.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                pin =  userDetails[1].ToString();
                domain = userDetails[0].ToString();

            }

            //Dictionary<string, string> UserDetails = loginSSA.GetUsersDetails(userDetails[1].ToString());
            //Figleaf Test only
            //string pin = "067736";
            Dictionary<string, string> UserDetails = loginSSA.GetUsersDetails(pin);
            //
            try
            {
                HttpContext.Current.Response.Cookies["User"]["PIN"] = pin;
                HttpContext.Current.Response.Cookies["User"]["LastName"] = UserDetails["LastName"];
                HttpContext.Current.Response.Cookies["User"]["FirstName"] = UserDetails["FirstName"];
                HttpContext.Current.Response.Cookies["User"]["EMail"] = UserDetails["Email"];
                HttpContext.Current.Response.Cookies["User"]["Office"] = UserDetails["OfficeCode"];
                HttpContext.Current.Response.Cookies["User"]["Server"] = getMYIP();// UserDetails["LastName"];
                HttpContext.Current.Response.Cookies["User"]["UserDomain"] = domain; // userDetails[0].ToString(); //UserDetails["UPN"];
                HttpContext.Current.Response.Cookies["User"]["Title"] = UserDetails["Title"];
                HttpContext.Current.Response.Cookies["User"]["Component"] = UserDetails["Department"];
                HttpContext.Current.Response.Cookies["User"]["Phone"] = UserDetails["Telephone"];
                HttpContext.Current.Response.Cookies["User"]["DDS"] = UserDetails["DDS"];
                HttpContext.Current.Response.Cookies["User"].Expires = DateTime.Now.AddDays(1);
                string Environment = System.Configuration.ConfigurationManager.AppSettings["Environment"].ToString();

                logInSSAMembersToEktron();

            }
            catch
            {
                //HttpContext.Current.Response.Write("Not a valid user");

                HttpContext.Current.Response.Cookies["User"]["PIN"] = pin;
                HttpContext.Current.Response.Cookies["User"]["LastName"] = "Service ";
                HttpContext.Current.Response.Cookies["User"]["FirstName"] = "Account";
                HttpContext.Current.Response.Cookies["User"]["EMail"] = pin + "@ssa.gov";
                HttpContext.Current.Response.Cookies["User"]["Office"] = "N/A ";
                HttpContext.Current.Response.Cookies["User"]["Server"] = getMYIP();
                HttpContext.Current.Response.Cookies["User"]["UserDomain"] = domain; // userDetails[0].ToString(); //UserDetails["UPN"];
                HttpContext.Current.Response.Cookies["User"]["Title"] = "N/A ";
                HttpContext.Current.Response.Cookies["User"]["Component"] = "N/A ";
                HttpContext.Current.Response.Cookies["User"]["Phone"] = "N/A ";
                HttpContext.Current.Response.Cookies["User"]["DDS"] = "N/A ";
                HttpContext.Current.Response.Cookies["User"].Expires = DateTime.Now.AddDays(1);
            }



        }



        public static Dictionary<string, string> GetUsersDetails(string PIN, string EMAIL = "")
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
                        lastNameCore = Names[0];
                        firstNameCore = Names[1];
                    }
                    catch { }


                  //  displayName = Names[1] + " " + Names[0];
                    if (displayName == "") { UserDetails.Add("DisplayName", CorePIN); } else { UserDetails.Add("DisplayName", displayName); }
                    if (email == "") { UserDetails.Add("Email", "Anonymous"); } else { UserDetails.Add("Email", email); }
                    if (ocd == "") { UserDetails.Add("OfficeCode", "Anonymous"); } else { UserDetails.Add("OfficeCode", ocd); }
                    if (title == "") { UserDetails.Add("Title", "Anonymous"); } else { UserDetails.Add("Title", title); }
                    if (telephone == "") { UserDetails.Add("Telephone", "Anonymous"); } else { UserDetails.Add("Telephone", telephone); }
                    if (EmailDepartment == "") { UserDetails.Add("Department", "Anonymous"); } else { UserDetails.Add("Department", EmailDepartment); }
                    if (UPN == "") { UserDetails.Add("UPN", "Anonymous"); } else { UserDetails.Add("UPN", UPN); }
                    if (dj9OfcTypeTxt == "") { UserDetails.Add("DDS", "Anonymous"); } else { UserDetails.Add("DDS", dj9OfcTypeTxt); }
                    if (lastName == "") { UserDetails.Add("LastName", "Anonymous"); } else { UserDetails.Add("LastName", lastName); }
                    if (firstName == "") { UserDetails.Add("FirstName", "Anonymous"); } else { UserDetails.Add("FirstName", firstName); }
                    if (getMYIP() == "") { UserDetails.Add("Server", "Anonymous"); } else { UserDetails.Add("Server", getMYIP()); }
                    //  if (Domain == "") { UserDetails.Add("Email", email); } else { U }
                    if (PINN == "") { UserDetails.Add("PIN", "Anonymous"); } else { UserDetails.Add("PIN", PINN); }
                    if (Component == "") { UserDetails.Add("Component", "Anonymous"); } else { UserDetails.Add("Component", Component); }

                    UserDetails.Add("Domain", "Domain");












                }
            }

            return UserDetails;

        }

        public static void checkSiteMaintenance()
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
                            HttpContext.Current.Response.StatusCode = 301;
                            HttpContext.Current.Response.Status = "301 Moved Permanently";
                            HttpContext.Current.Response.RedirectLocation = "/sitemaintenance.aspx";
                            HttpContext.Current.Response.End();
                        }

                    }

                }

            }
        }

        public static string getMYIP()
        {
            try{
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
            } catch {
                return "";
            }
            //Response.Write(Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork));
        }
        public static DateTime convertTimeToDateTime(string time)
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







        public static string lastNameCore { get; set; }

        public static string firstNameCore { get; set; }
    }
}