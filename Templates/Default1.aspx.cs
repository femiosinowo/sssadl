using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSADL.CMS;
using Ektron.Cms.Instrumentation;

public partial class Templates_Default : PageBase
{
    /// <summary>
    /// Page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                //get different search links content
                long searchLinksCId = ConfigHelper.GetValueLong("HomePageSearchPageLinksCId");
                if (searchLinksCId > 0)
                {
                    var contentData = ContentHelper.GetContentById(searchLinksCId);
                    if (contentData != null)
                    {
                        ltrContentBlockText.Text = contentData.Html;

                        //get home page banner from meta data img
                        if (contentData.Image != string.Empty && contentData.Image.IndexOf("workarea") < 0)
                            homeContent.Attributes.Add("style", "background:url('/" + contentData.Image + "') no-repeat scroll center top #ffffff");
                    }
                }

                //get footer links
                long homePageMenuTreeId = ConfigHelper.GetValueLong("HomePageNavId");
                var menuData = MenuHelper.GetMenuTree(homePageMenuTreeId);
                if (menuData != null && menuData.Items != null)
                {
                    lvFooterNav.DataSource = menuData.Items;
                    lvFooterNav.DataBind();
                }

                //latest news
                this.GetLatestNews();
                this.GetNewsSpotLightContent();
            }
            catch(Exception ex)
            {
                Log.WriteError(ex.Message + " : " + ex.StackTrace);
            }
        }
    }

    /// <summary>
    /// This method is used to get top 4 new items
    /// </summary>
    private void GetLatestNews()
    {
        var latestNews = SiteDataManager.GetLatestNews();
        if(latestNews != null && latestNews.Any())
        {
            var homePageNews = latestNews.Take(4).ToList();
            lvLatestNews.DataSource = homePageNews;
            lvLatestNews.DataBind();
        }
    }

    /// <summary>
    /// This method is used to get spot light news content from metadata
    /// </summary>
    private void GetNewsSpotLightContent()
    {
        long homeContentId = ConfigHelper.GetValueLong("HomePageCId");
        var homeContentData = ContentHelper.GetContentById(homeContentId, true);
        if(homeContentData != null && homeContentData.MetaData != null)
        {
            long metaSpotLightId = ConfigHelper.GetValueLong("MetaHomePageNewsSpotLightId");
            var metaHomeNews = homeContentData.MetaData.Where(x => x.Id == metaSpotLightId).FirstOrDefault();
            if(metaHomeNews != null && metaHomeNews.Text != string.Empty)
            {
                long newsSpotLightCId = EktUtility.ParseLong(metaHomeNews.Text);
                var newsSpotLightData = SiteDataManager.GetNewsById(newsSpotLightCId);
                if (newsSpotLightData != null && newsSpotLightData.SmartForm != null)
                {
                    if (!string.IsNullOrEmpty(newsSpotLightData.SmartForm.Teaser))
                        ltrNewsSpotlight.Text = "<p><a href=\"" + newsSpotLightData.Content.Quicklink + "\">" + newsSpotLightData.SmartForm.Teaser + "</a></p>";
                    else
                        ltrNewsSpotlight.Text = "<p><a href=\"" + newsSpotLightData.Content.Quicklink + "\">" + newsSpotLightData.SmartForm.Headline + "</a></p>";
                }
            }
        }        
    }


    /// <summary>
    /// This method is used to format the Ekton menu URL's
    /// </summary>
    /// <param name="url">menu link field</param>
    /// <param name="title">menu title text</param>
    /// <param name="className">menu description text</param>
    /// <returns></returns>
    public string FormatLink(string url, string title, string description)
    {
        if ((!string.IsNullOrEmpty(url)) && (!url.StartsWith("/")))
            url = "/" + url;

        string formattedLink = "<a href=\"" + url + "\" title=\"" + title + "\" aria-label=\"" + title + "\">" + title + "</a>";
        if ((!string.IsNullOrEmpty(description)) && (description.Trim().ToLower().Equals("inactivelink")))
            formattedLink = title;
        return formattedLink;
    }

}