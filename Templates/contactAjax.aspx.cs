using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Templates_contactAjax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ///Templates/contactConfirm.aspx
        if (!Page.IsPostBack)
        {
            formBlk.DefaultFormID = long.Parse(Request.QueryString["formId"].ToString());
            formBlk.Fill();
        }
        //formBlk.Page = Page;



        
    }
}