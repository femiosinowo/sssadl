using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;

public partial class admin_requests_ajaxGetUsersDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string pin = Request.QueryString["pin"].ToString();
            Dictionary<string, string> UsersDetails = loginSSA.GetUsersDetails(pin);

            if (UsersDetails.ContainsKey("PIN"))
            {

                if (UsersDetails["Email"].Trim() == "N/A")
                {
                    Response.Write("<script> alert('No PIN found'); document.getElementById('ctl00_mainContentCP_RequestorPIN').value = ''</script>");
                }
                PINN = UsersDetails["PIN"];
                Userfirstname = UsersDetails["LastName"];
                Userlastname = UsersDetails["FirstName"];
                UserServer = UsersDetails["Server"];
                UserDomain = UsersDetails["Domain"];

                fullname = UsersDetails["DisplayName"];
                userTitle = UsersDetails["Title"];
                userComponent = UsersDetails["Component"];
                userofficecode = UsersDetails["OfficeCode"];
                useremail = UsersDetails["Email"];
                userphone = UsersDetails["Telephone"];


            }
            else
            {
                Response.Write("<script> alert('No PIN found'); document.getElementById('ctl00_mainContentCP_RequestorPIN').value = ''</script>");
            }
        }
        catch
        {
            Response.Write("<script> alert('No PIN found'); document.getElementById('ctl00_mainContentCP_RequestorPIN').value = ''</script>");
        }

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

    public string PINN { get; set; }
}