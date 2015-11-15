using System;
using SSADL.CMS;

public partial class Templates_Content : PageBase
{
    /// <summary>
    /// Page Init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                long contentId = EktUtility.ParseLong(Request.QueryString["id"]);
                this.GetContentData(contentId);
            }
        }
    }

    /// <summary>
    /// This method is used to get content data
    /// </summary>
    /// <param name="contentId"></param>
    private void GetContentData(long contentId)
    {
        if (contentId > 0)
        {
            var cData = ContentHelper.GetContentById(contentId);
            if (cData != null && cData.Id > 0)
            {
              //  ltrNewsBody.Text = cData.Html;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
    }
}