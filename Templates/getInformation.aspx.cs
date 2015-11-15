using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
public partial class Templates_getInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ///need to get this information from ssa
            ///
            string pin = Request.QueryString["pin"].ToString();
            string email = Request.QueryString["email"].ToString();
           
            //Userfirstname = UsersDetails["LastName"];
            //Userlastname = UsersDetails["FirstName"];
            //UserServer = UsersDetails["Server"];   // UsersDetails["LastName"];
            //UserDomain = UsersDetails["Domain"];    //UsersDetails["LastName"];



            //fullname = UsersDetails["DisplayName"];
            //userTitle = UsersDetails["Title"];
            //userComponent = UsersDetails["UPN"];
            //userofficecode = UsersDetails["OfficeCode"];
            //useremail = UsersDetails["Email"];
            //userphone = UsersDetails["Telephone"];

            try
            {

                Dictionary<string, string> UsersDetails = new Dictionary<string, string>();

                UsersDetails = loginSSA.GetUsersDetails(pin, email);

                IndividualName = UsersDetails["DisplayName"];
                IndividualTitle = UsersDetails["Title"];
                IndividualComponent = UsersDetails["Component"];
                IndividualOfficeCode = UsersDetails["OfficeCode"];
                IndividualPhone = UsersDetails["Telephone"];
                IndividualEmail = UsersDetails["Email"];
                IndividualsPIN = UsersDetails["PIN"];

                SubmittedForPIN = UsersDetails["PIN"];
                SubmittedForLastName = UsersDetails["LastName"];
                SubmittedForFirstName = UsersDetails["FirstName"];
                SubmittedForEMail = UsersDetails["Email"];
                SubmittedForOffice = UsersDetails["OfficeCode"];
                SubmittedForServer = UsersDetails["Server"];
                SubmittedForUserDomain = UsersDetails["Domain"];
            }
            catch
            {

                Response.Write("<script>alert('There is no user for that PIN')</script>");
            }

        }
    }

    public string IndividualsPIN { get; set; }
    public string IndividualName { get; set; }
    public string IndividualEmail { get; set; }
    public string IndividualTitle { get; set; }

    public string IndividualOfficeCode { get; set; }

    public string IndividualPhone { get; set; }

    public string IndividualComponent { get; set; }

    public string SubmittedForPIN { get; set; }

    public string SubmittedForLastName { get; set; }

    public string SubmittedForFirstName { get; set; }

    public string SubmittedForEMail { get; set; }

    public string SubmittedForOffice { get; set; }

    public string SubmittedForServer { get; set; }

    public string SubmittedForUserDomain { get; set; }
}