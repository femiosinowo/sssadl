using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;

public partial class admin_Masters_AdminMain : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (commonfunctions.Environment == "PROD")
        {
            string fromURL = "http://digitallibraryadmin.ba.ad.ssa.gov/" + Request.RawUrl.ToString();
            Response.Redirect(fromURL);

        }

        //  Response.Write(loginSSA.myPIN);

        if ((string)loginSSA.myPIN == "")
        {
            loginSSA lgssa = new loginSSA();
            lgssa.createCookies();

        }
        // loginSSA.logInSSAMembersToEktron();

        //check if i am admin 
        if (loginSSA.isAdminUser())
        {
          //  adminPanelLinks.Visible = true;
            loginSSA.logInSSAMembersToEktron();
        }
        else
        {

            Response.Redirect("/admin/accessdenied.aspx");
        }

        //update last access
        string sql = "UPDATE [dbo].[users] SET LastAccessed = GETDATE() where PIN ='" + loginSSA.myPIN + "'";       
        DataBase.executeCommand(sql);


    }
}
