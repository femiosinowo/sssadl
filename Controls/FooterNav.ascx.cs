using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ektron.Cms;
using System.Text;
using Ektron.Cms.Instrumentation;
using System.Xml;
using System.Data;
using System.Text.RegularExpressions;
using SSADL.CMS;


public partial class UserControls_FooterNav : System.Web.UI.UserControl
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
            FillNavContent();
    }

    /// <summary>
    /// This method is used to bind menu HTML on the page
    /// </summary>
    private void FillNavContent()
    {
        try
        {
            long mainNavId = 61; // ConfigHelper.GetValueLong("FooterNavId");
            var menuData = MenuHelper.GetMenuTree(mainNavId);

            if (menuData != null && menuData.Items != null)
            {
                lvFooterMenu.DataSource = menuData.Items;
                lvFooterMenu.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " :: " + ex.StackTrace);
        }
    }

}