using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;

public partial class MasterPages_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //check if i am admin 
        if (loginSSA.isAdminUser())
        {
            //  adminPanelLinks.Visible = true;
            loginSSA.logInSSAMembersToEktron();
        }
        else 
        {
            if (commonfunctions.Environment == "DEV")
            {
                string fromURL = "http://digitallibrary.ba.ad.ssa.gov/" + Request.RawUrl.ToString();
                Response.Redirect(fromURL);
            }
           // Response.Redirect("/admin/accessdenied.aspx");
        }

       // uxMetaDataTitles.Title = "SSA Digital Library";
       // uxMetaDataTitles.Fill();
        string pageTitle = "SSA Digital Library";
        try
        {
            
            long requestID = 0;
            if (Request.QueryString["id"] != null)
            {
                requestID = long.Parse(Request.QueryString["id"].ToString());
            }
            else
            {
                requestID = long.Parse(Request.QueryString["ekfrm"].ToString()); // uxMetaDataTitles.DynamicParameter = "ekfrm";
            }

            mainContent.DefaultContentID = requestID;

            mainContent.Fill();
            mainContent.Text = "<!-- -->";
            Ektron.Cms.CustomAttributeList metadataValues = new Ektron.Cms.CustomAttributeList();
            metadataValues = mainContent.GetMetaData();

             pageTitle = metadataValues["title"].Value.ToString();
            if (pageTitle == "")
            {
                pageTitle = mainContent.Title + " - SSA Digital Library";
            }
            // Response.Write(mainContent.Title);
           
           // MetaDataInfo += "<title>" + pageTitle + "</title>";
            //MetaDataInfo += "<title>" + pageTitle + "</title>";
        }

        catch
        {

        }
        MetaDataInfo.Text = "<title>" + pageTitle + "</title>";
      

        
        
       
        //login me in
      //  Response.Write(loginSSA.myPIN);
       
        //try
        //{
        //    Response.Write(loginSSA.myPIN);
        //    string MemberPIN = (string)HttpContext.Current.Request.Cookies["User"]["PIN"];
        //    if (MemberPIN == "None")
        //    {
        //        loginSSA lgssa = new loginSSA();
        //        lgssa.createCookies();
        //    }
        //}
        //catch
        //{
        //    loginSSA lgssa = new loginSSA();
        //    lgssa.createCookies();
        //   // loginSSA.createCookies();
        //}


      //  Response.Write(loginSSA.myPIN);

        if ((string)loginSSA.myPIN == "")
        {
            loginSSA lgssa = new loginSSA();
            lgssa.createCookies();

        }




    }

   // public string MetaDataInfo { get; set; }
    
}
