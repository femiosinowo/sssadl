using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;

public partial class admin_users_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        requestorName = Request.QueryString["reqname"].ToString();
        resourceName = Request.QueryString["rsname"].ToString();
        whatPanel = Request.QueryString["panel"].ToString();

        switch (whatPanel)
        {
            case "newrequest":
                newRequestPanel.Visible = true;
                break;
            case "approved":
                approvedConfirmedPanel.Visible = true;
                break;

            case "declined":
                declinedPanel.Visible = true;
                break;

            case "newrequestskipmessage":
                newrequestskipmessagePanel.Visible = true;
                break;
                
        }

    }





    public string requestorName { get; set; }

    public string resourceName { get; set; }

    public string whatPanel { get; set; }
}