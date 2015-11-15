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

        
    }
}
