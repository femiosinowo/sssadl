using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using Ektron.Cms.Instrumentation;
using System.Xml;
using Ektron.Cms.Content;
using Ektron.Cms;
using Ektron.Cms.Framework.Content;

public partial class Templates_Default5 : System.Web.UI.Page
{
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //try
            //{
            //get different search links content
            long searchLinksCId = ConfigHelper.GetValueLong("HomePageSearchPageLinksCId");
            if (searchLinksCId > 0)
            {
                mainContent.DefaultContentID = searchLinksCId;
                mainContent.Fill();
                mainContent.Text = "<!-- -->";
                XmlDocument contentXML = commonfunctions.getContentXML(searchLinksCId);

                ResourceSpotlight = commonfunctions.getFieldValue(contentXML, "ResourceSpotlight");
                MainBackgroundImage = commonfunctions.getFieldAttributeValue(contentXML, "MainBackgroundImage", "img", "src", "/root");
                MainBackgroundImageALT = commonfunctions.getFieldAttributeValue(contentXML, "MainBackgroundImage", "img", "alt", "/root");
                LibraryNewsCount = commonfunctions.getFieldValue(contentXML, "LibraryNewsCount");
                //  HeaderTitle = commonfunctions.getFieldValue(contentXML, "HeaderTitle", "/root/Links/Fields/");

                SearchTip.Text = "Search Tips";
                SearchTip.NavigateUrl = commonfunctions.getFieldAttributeValue(contentXML, "SearchTipURL", "a", "href", "/root");
                //img bgg
                if (MainBackgroundImage != string.Empty)
                {
                    homeContent.Attributes.Add("style", "background:url('" + MainBackgroundImage + "') no-repeat scroll center top #ffffff");
                }
                long CollectionID = 0;
                footerNavlinks = "";
                XmlNodeList xList = contentXML.SelectNodes("/root/Links/Fields");
                foreach (XmlElement xn in xList)
                {
                    HeaderTitle = xn["HeaderTitle"].InnerXml;
                    CollectionField = xn["CollectionField"].InnerXml;
                    LinksCount = xn["LinksCount"].InnerXml;
                    //   ImageBannerSRC = xn["BannerImage"].SelectSingleNode("img").Attributes["src"].Value;
                    footerNavlinks += "<div class=\"column-3\">";
                    footerNavlinks += " <h3>" + HeaderTitle + "</h3> ";
                    footerNavlinks += "<ul class=\"divider no-margin\">";
                    footerNavlinks += getLinks(long.Parse(CollectionField), long.Parse(LinksCount));
                    footerNavlinks += "  </ul>";
                    footerNavlinks += "</div><!-- column 3-->";

                }


                //latest news
                this.GetLatestNews(Convert.ToInt32(LibraryNewsCount));
                this.GetNewsSpotLightContent(long.Parse(ResourceSpotlight));
            }


            searchMenu.DefaultMenuID = 83;
            searchMenu.Fill();
            generateMenu(searchMenu.XmlDoc.InnerXml);
            //Response.Write(searchMenu.XmlDoc.InnerXml);
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteError(ex.Message + " : " + ex.StackTrace);
            //}
        }

    }

    private void generateMenu(string menuXML)
    {
        XmlDocument menuXMLDoc = new XmlDocument();
        menuXMLDoc.LoadXml(menuXML);

        XmlNodeList xnMenuData = menuXMLDoc.SelectNodes("/MenuDataResult/Item/Item");
        //  Response.Write(menuXML);
        int ii = 0;
        foreach (XmlElement xnItem in xnMenuData)
        {
            string ItemType = xnItem["ItemType"].InnerText;

            if (ItemType == "content")
            {

                string ItemSelected = xnItem["ItemSelected"].InnerText;
                string ItemLink = xnItem["ItemLink"].InnerText;
                string ItemQuickLink = xnItem["ItemQuickLink"].InnerText;
                string ItemTarget = getTarget(xnItem["ItemTarget"].InnerText);
                string ItemTitle = xnItem["ItemTitle"].InnerText;
                string ItemId = xnItem["ItemId"].InnerText;

                if (ii != 0)
                {
                    navSearchHTML += "  |  ";
                }

                navSearchHTML += "<a title='" + ItemTitle + "'  aria-label='" + ItemTitle + "' target='" + ItemTarget + "' href='" + ItemLink + "' >" + ItemTitle + "</a>";


                ii++;
                //  buildUL += buildULS(ItemTarget, ItemLink, ItemTitle, ItemId);

            }


            if (ItemType == "Submenu")
            {
                XmlNodeList xnSubmenuItems = xnItem.SelectNodes("Menu");


                foreach (XmlElement xnSubItem in xnSubmenuItems)
                {

                    string Title = xnSubItem["Title"].InnerText;
                    string Link = xnSubItem["Link"].InnerText;
                    string MenuSelected = xnSubItem["MenuSelected"].InnerText;
                    string ChildMenuSelected = xnSubItem["ChildMenuSelected"].InnerText;
                    //Response.Write(Link + " : >> " + Request.RawUrl);
                    Response.Write(Title);

                }
            }



        }



    }


    private string getLinks(long collectionID, long linkscount = 4)
    {

        if (collectionID > 0)
        {

            string resultreturned = "";
            var contentManager2 = new Ektron.Cms.Framework.Content.ContentManager();
            var criteria2 = new ContentCollectionCriteria();
            criteria2.AddFilter(collectionID);
            criteria2.OrderByCollectionOrder = true;
            var contentList2 = contentManager2.GetList(criteria2);

            for (int jk = 0; jk < linkscount; jk++)
            {
                string quicklink = contentList2[jk].Quicklink;
                string title = contentList2[jk].Title;
                resultreturned += "<li><a href=" + quicklink + " title=" + title + " aria-label=" + title + ">" + title + "</a></li>";


            }


            return resultreturned;
        }
        else
        {
            return "";
        }
    }
    /// <summary>
    /// This method is used to get top 4 new items
    /// </summary>
    private void GetLatestNews(int count = 4)
    {
        var latestNews = SiteDataManager.GetLatestNews();
        if (latestNews != null && latestNews.Any())
        {
            var homePageNews = latestNews.Take(count).ToList();
            lvLatestNews.DataSource = homePageNews;
            lvLatestNews.DataBind();
        }
    }

    /// <summary>
    /// This method is used to get spot light news content from metadata
    /// </summary>
    private void GetNewsSpotLightContent(long ResourceSpotlightID)
    {
        ContentManager contentManager = new ContentManager();
        ContentData cData = new ContentData();
        Boolean returnMetadata = true;
        string result = string.Empty;

        cData = contentManager.GetItem(ResourceSpotlightID, returnMetadata);
        string teaser = getSubString(cData.Teaser);
        ltrNewsSpotlight.Text = "<p><a href=\"" + cData.Quicklink + "\">" + teaser + "</a></p>";

        //long homeContentId = ConfigHelper.GetValueLong("HomePageCId");
        //var homeContentData = ContentHelper.GetContentById(homeContentId, true);
        //if (homeContentData != null && homeContentData.MetaData != null)
        //{
        //    long metaSpotLightId = ConfigHelper.GetValueLong("MetaHomePageNewsSpotLightId");
        //    var metaHomeNews = homeContentData.MetaData.Where(x => x.Id == metaSpotLightId).FirstOrDefault();
        //    if (metaHomeNews != null && metaHomeNews.Text != string.Empty)
        //    {
        //        long newsSpotLightCId = EktUtility.ParseLong(metaHomeNews.Text);
        //        var newsSpotLightData = SiteDataManager.GetNewsById(newsSpotLightCId);
        //        if (newsSpotLightData != null && newsSpotLightData.SmartForm != null)
        //        {
        //            if (!string.IsNullOrEmpty(newsSpotLightData.SmartForm.Teaser))
        //                ltrNewsSpotlight.Text = "<p><a href=\"" + newsSpotLightData.Content.Quicklink + "\">" + newsSpotLightData.SmartForm.Teaser + "</a></p>";
        //            else
        //                ltrNewsSpotlight.Text = "<p><a href=\"" + newsSpotLightData.Content.Quicklink + "\">" + newsSpotLightData.SmartForm.Headline + "</a></p>";
        //        }
        //    }
        //}
    }
    public string getSubString(string value)
    {
        if (value.Length > 150)
        {

            return value.Substring(0, 150);
        }
        else
        {

            return value;
        }

    }



    public string getTarget(string no)
    {
        if (no == "1") return "_blank";
        if (no == "2") return "_self";
        if (no == "3") return "_parent";
        if (no == "4") return "_top";
        if (no == "") return "_self";
        return "_self";
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

    public string MainBackgroundImage { get; set; }

    public string MainBackgroundImageALT { get; set; }

    public string LibraryNewsCount { get; set; }

    public string HeaderTitle { get; set; }

    public string CollectionField { get; set; }

    public string LinksCount { get; set; }

    public string ResourceSpotlight { get; set; }

    public string footerNavlinks { get; set; }

    public object navSearchHTML { get; set; }
}