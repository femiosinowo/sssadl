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
  
        whatPanel = Request.QueryString["panel"].ToString();
        ResourceName = Request.QueryString["rn"].ToString();
        switch (whatPanel)
        {
            case "new":
                newPanel.Visible = true;
                ResourceName = Request.QueryString["rn"].ToString();
                break;
            case "edit":
                editPanel.Visible = true;
                break;

           
                
        }

    }




 

    public string whatPanel { get; set; }

    public string ResourceName { get; set; }
}