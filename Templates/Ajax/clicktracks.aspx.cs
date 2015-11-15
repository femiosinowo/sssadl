using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms;
using SSADL.CMS;
public partial class Templates_Ajax_clicktracks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (commonfunctions.Environment == "PROD")
        {
            string link = Request.QueryString["link"].ToString().Trim();
            string rid = "0";
            // Response.Write(link);
            if (Request.QueryString["rid"] != null)
            {
                rid = Request.QueryString["rid"].ToString().Trim();
            }
            if (link != "#" && link != "")
            {
                string host = commonfunctions.host;
                if (!link.Contains(host)) link = host + link;
                string ClickedByPIN = loginSSA.myPIN;
                string ClickedByLastName = loginSSA.myLastName;
                string ClickedByFirstName = loginSSA.myFirstName;
                string ClickedByEMail = loginSSA.myEmail;
                string ClickedByOffice = loginSSA.myOffice;
                string ClickedByServer = loginSSA.myServer;
                string ClickedByUserDomain = loginSSA.myUserDomain;

                string DLSiteLinkTargetURL = link;
                string DLSiteURLWhereLinkWasClicked = Request.UrlReferrer.AbsoluteUri;

                sqlInsert = "   INSERT INTO [dbo].[ClickTracking]([ClickedByPIN],[ClickedByLastName],[ClickedByFirstName],[ClickedByEMail],[ClickedByOffice],[ClickedByServer],[ClickedByUserDomain],[ClickedDateTime],[DLSiteURLWhereLinkWasClicked],[DLSiteLinkTargetURL] , Resource , ClickedTime) ";
                sqlInsert += "  VALUES ('" + ClickedByPIN + "','" + ClickedByLastName + "','" + ClickedByFirstName + "','" + ClickedByEMail + "','" + ClickedByOffice + "','" + ClickedByServer + "','" + ClickedByUserDomain + "', GETDATE(),'" + DLSiteURLWhereLinkWasClicked + "','" + DLSiteLinkTargetURL + "' , '" + rid + "' , GETDATE()) ";
                DataBase.executeCommand(sqlInsert);



            }


        }



    }

    public string sqlInsert { get; set; }
}