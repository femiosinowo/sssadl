using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Net;
using System.Xml;
using System.Data;
using System.Net.Sockets;

public partial class admin_users_ajaxgetUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //903000
        string pin = Request.QueryString["pin"].ToString();
        Dictionary<string, string> UsersDetails = loginSSA.GetUsersDetails(pin);
        //Dictionary<string, string> UsersDetails =  GetUsersDetails(pin);
        try
        {
            
            Userfirstname = UsersDetails["LastName"];
            Userlastname = UsersDetails["FirstName"];
            UserServer = "myserver"; // UsersDetails["LastName"];
            UserDomain = "mydomain";  //UsersDetails["LastName"];



            fullname = UsersDetails["LastName"] + " " + UsersDetails["FirstName"];
            userTitle = UsersDetails["Title"];
            userComponent = UsersDetails["UPN"];
            userofficecode = UsersDetails["OfficeCode"];
            useremail = UsersDetails["Email"];
            userphone = UsersDetails["Telephone"];
        }
        catch
        {

            Response.Write("<script>alert('There is no user for that PIN')</script>");
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
                urlgetPIN = loginSSA.coreEmailLink + "?REQTYPE=GETALL&PIN=" + PIN;
            }
            else if (PIN == "" && EMAIL != "")
            {
                //lets get pin from here                
                urlgetPIN = loginSSA.coreEmailLink + "?REQTYPE=GETPIN&EMAIL=" + EMAIL;
            }
            WebRequest requestEMAIL = WebRequest.Create(urlgetPIN);
            WebResponse responseEMAIL = requestEMAIL.GetResponse();

            XmlDocument xmlDocEMAIL = new XmlDocument();
            xmlDocEMAIL.Load(responseEMAIL.GetResponseStream());

            CorePIN = commonfunctions.getFieldValue(xmlDocEMAIL, "pin", "/service");



            if (CorePIN != "")
            {
                url = loginSSA.coreEmailLink + "?REQTYPE=GETALL&PIN=" + CorePIN;
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



                string urlD =loginSSA.coreIVFLink + "?PIN=" + CorePIN + "&Application=Test";
                WebRequest requestD = WebRequest.Create(urlD);
                WebResponse responseD = requestD.GetResponse();

                XmlDocument xmlDocD = new XmlDocument();
                xmlDocD.Load(responseD.GetResponseStream());
                string ocd = commonfunctions.getFieldValue(xmlDocD, "ocd", "/service/data");
                string department = commonfunctions.getFieldValue(xmlDocD, "dept", "/service/data");
                string firstName = commonfunctions.getFieldValue(xmlDocD, "fnm", "/service/data");
                string lastName = commonfunctions.getFieldValue(xmlDocD, "lnm", "/service/data");

                string urlDDS = loginSSA.coreOLVLink + "?OFFICECODEA=" + ocd + "&QUERYTYPE=1&APPLICATION=DDL_WEBSITE";
                WebRequest requestDDS = WebRequest.Create(urlDDS);
                WebResponse responseDDS = requestDDS.GetResponse();

                XmlDocument xmlDocDDS = new XmlDocument();
                xmlDocDDS.Load(responseDDS.GetResponseStream());
                string dj9OfcTypeTxt = commonfunctions.getFieldValue(xmlDocDDS, "dj9OfcTypeTxt", "/service");  ///If this is DDS then the user is a DDS member




                string lastNameCore = "";
                string firstNameCore = "";
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
                    lastName = displayName; // Names[0];
                    firstName = displayName;// Names[1];
                }
                catch { }
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


    public static string getMYIP()
    {
        try
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
        }
        catch
        {
            return "";
        }
        //Response.Write(Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork));
    }



    public string fullname { get; set; }

    public string userphone { get; set; }

    public string useremail { get; set; }

    public string userofficecode { get; set; }

    public string userTitle { get; set; }

    public string userComponent { get; set; }

    public string Userfirstname { get; set; }

    public string Userlastname { get; set; }

    public string UserDomain { get; set; }

    public string UserServer { get; set; }

 
}