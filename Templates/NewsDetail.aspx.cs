using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSADL.CMS;
using System.Xml;

public partial class Templates_NewsDetail : System.Web.UI.Page
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                long contentId = long.Parse(Request.QueryString["id"]);
                getNewsData(contentId);
            }
        }

       
        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle =  mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        mainContent.Text = "<!-- -->";
        uxPageTitle.ResourceTypeId = "2";
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();
        
    }

    private void getNewsData(long contentId)
    {

        XmlDocument contentXML = commonfunctions.getContentXML(contentId);


        //        /News/Headline
        ///News/Date
        ///News/Image/img/@alt
        ///News/Image/img/@src
        ///News/Teaser
        ///News/FullDescription
        ///News/NewsActive


        Headline = commonfunctions.getFieldValue(contentXML, "Headline", "/News");
        MainBackgroundImage = commonfunctions.getFieldAttributeValue(contentXML, "Image", "img", "src", "/News");
        MainBackgroundImageALT = commonfunctions.getFieldAttributeValue(contentXML, "Image", "img", "alt", "/News");
        NewsDate = commonfunctions.getFieldValue(contentXML, "Date", "/News");
        Teaser = commonfunctions.getFieldValue(contentXML, "Teaser", "/News");
        FullDescription = commonfunctions.getFieldValue(contentXML, "FullDescription", "/News");
        if (MainBackgroundImage != "")
        {
            ltrNewsImg.Text = "<img title=\"" + MainBackgroundImageALT + "\" alt=\"" + MainBackgroundImageALT + "\" src=\"" + MainBackgroundImage + "\" />";
        }
        ltrNewsBody.Text = FullDescription;
    }

    /// <summary>
    /// This method is used to get news article data
    /// </summary>
    /// <param name="contentId"></param>
    //private void GetNewsData(long contentId)
    //{
    //    if(contentId > 0)
    //    {
    //        var newsData = SiteDataManager.GetNewsById(contentId);
    //        if(newsData != null && newsData.SmartForm != null)
    //        {
    //            if(newsData.SmartForm.Image != null && newsData.SmartForm.Image.img != null)
    //            {
    //                string imgsrc = newsData.SmartForm.Image.img.src.Trim();
    //                if (imgsrc != "")
    //                {
    //                    imgDiv.Visible = true;
    //                    ltrNewsImg.Text = "<img title=\"" + newsData.SmartForm.Image.img.alt + "\" alt=\"" + newsData.SmartForm.Image.img.alt + "\" src=\"" + newsData.SmartForm.Image.img.src + "\" />";
    //                }
    //            }
    //            ltrNewsBody.Text = EktUtility.GetHTMLNodesFromEktRich(newsData.SmartForm.FullDescription);
    //        }
    //    }
    //}


    public string FullDescription { get; set; }

    public string Teaser { get; set; }

    public string NewsDate { get; set; }

    public string MainBackgroundImageALT { get; set; }

    public string MainBackgroundImage { get; set; }

    public string Headline { get; set; }
}