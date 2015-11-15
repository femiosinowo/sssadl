using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI;

using SSADL.CMS;
using Ektron.Cms.Instrumentation;
using System.Web;

public partial class Main : MasterBase
{

    Uri uri = HttpContext.Current.Request.Url;
    protected void Page_Load(object sender, EventArgs e)
    {
        siteHost = uri.Scheme + Uri.SchemeDelimiter + uri.Host + "/";
    }

    /// <summary>
    /// page Init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        long contentId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
            long.TryParse(Request.QueryString["sid"], out contentId);
        if (!string.IsNullOrEmpty(Request.QueryString["pageid"]))
            long.TryParse(Request.QueryString["pageid"], out contentId);
        else if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            long.TryParse(Request.QueryString["id"], out contentId);

        if (contentId == 0 && Request.RawUrl.Equals("/"))
            contentId = ConfigHelper.GetValueLong("HomePageCId");

        this.GetSEOSiteInfomration(contentId);        
    }
            
    /// <summary>
    /// This method is used to write meta info on to the page header
    /// </summary>
    /// <param name="contentId"></param>
    private void GetSEOSiteInfomration(long contentId)
    {
        try
        {
            HtmlMeta meta = new HtmlMeta(); 
            var cntData = ContentHelper.GetContentById(contentId, true);
            if (cntData != null && cntData.MetaData != null && cntData.MetaData.Count() > 0)
            {
                var dataList = cntData.MetaData.ToList();

                //checking for keyword meta tag 
                var keywordMetaData = dataList.SingleOrDefault(x => x.Id == ConfigHelper.GetValueLong("MetaKeywordsId"));
                if (keywordMetaData != null && keywordMetaData.Text != null && keywordMetaData.Text.ToString() != "")
                {
                    meta = new HtmlMeta();
                    meta.Name = "Keywords";
                    meta.Content = keywordMetaData.Text.Replace(";", ",");
                    this.Page.Header.Controls.AddAt(0, meta);
                }

                //checking for description meta tag 
                var descritptionMetaData = dataList.SingleOrDefault(x => x.Id == ConfigHelper.GetValueLong("MetaDescriptionId"));
                if (descritptionMetaData != null && descritptionMetaData.Text != null && descritptionMetaData.Text.ToString() != "")
                {
                    string metaDescription = descritptionMetaData.Text;                    
                    meta = new HtmlMeta();
                    meta.Name = "Description";
                    meta.Content = metaDescription;
                    this.Page.Header.Controls.AddAt(0, meta);
                }                

                //checking for Title meta tag 
                var titleMetaData = dataList.SingleOrDefault(x => x.Id == ConfigHelper.GetValueLong("MetaPageTitleId"));
                if (titleMetaData != null && titleMetaData.Text != null && titleMetaData.Text.ToString() != "")
                {
                    string metaTitle = titleMetaData.Text;
                    Page.Header.Controls.AddAt(0, new LiteralControl("<title>" + metaTitle + "</title>"));
                }
            }
        }
        catch(Exception ex)
        {
            Log.WriteError(ex.Message + " :: " + ex.StackTrace);
        }
    }


    public string siteHost { get; set; }
}
