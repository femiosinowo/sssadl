using System;

using SSADL.CMS;


public partial class UserControls_UtilityNav : System.Web.UI.UserControl
{
    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{

            string sqlCount = "Select count(*) from MyResources where PINNumber = '" + loginSSA.myPIN + "'";
            myResourcesCount = DataBase.returnOneValue(sqlCount);



            if (loginSSA.isAdminUser() && commonfunctions.Environment=="DEV")
            {
                adminPanelLinks.Visible = true;
                loginSSA.logInSSAMembersToEktron();
            }
          
            //get different search links content
            //long logoCId = 34; // ConfigHelper.GetValueLong("SiteLogoTitleCId");
            //if (logoCId > 0)
            //{
            //    var contentData = ContentHelper.GetContentById(logoCId);
            //    if (contentData != null)
            //        ltrLogoText.Text = contentData.Html;
            //}
      //  }

        if (Request.QueryString["s"] != null)
        {

            searchWord = Request.QueryString["s"].ToString();

        }
    }


    public string searchWord { get; set; }
    public string myResourcesCount { get; set; }
}