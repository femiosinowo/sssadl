using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSADL.CMS;

public partial class Templates_NewsList : PageBase
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
            this.FillYearDropDown();
            int currentYear = DateTime.Now.Year;
            this.GetNewsList(currentYear);
            this.GetNewsSpotLightContent();
        }
    }    

    /// <summary>
    /// button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid == true && ddlArchiveYear.SelectedIndex > 0)
        {
            int selectedYear = int.Parse(ddlArchiveYear.SelectedValue);
            lblYearSelected.Text = selectedYear.ToString();
            this.GetNewsList(selectedYear);
        }
    }

    /// <summary>
    /// Fill year drop down with the avaliable news year
    /// </summary>
    private void FillYearDropDown()
    {
        var allNewsArticles = SiteDataManager.GetLatestNews();
        if (allNewsArticles != null && allNewsArticles.Any())
        {
            var yearList = allNewsArticles.Select(x => x.SmartForm.Date.Year).Distinct().ToList();
            if(yearList != null && yearList.Any())
            {
                int currentYear = DateTime.Now.Year;
                lblYearSelected.Text = currentYear.ToString();

                ListItem item = null;
                //initial item
                item = new ListItem("-Select a year-", "");
                ddlArchiveYear.Items.Add(item);

                foreach(var y in yearList)
                {                    
                    item = new ListItem(y.ToString(), y.ToString());
                    if (y == currentYear)
                        item.Selected = true;
                    ddlArchiveYear.Items.Add(item);
                }
            }
        }
    }

    /// <summary>
    /// Get all the news as per the year selected
    /// </summary>
    /// <param name="selectedYear"></param>
    private void GetNewsList(int selectedYear)
    {
        var allNewsArticles = SiteDataManager.GetLatestNews();
        if (allNewsArticles != null && allNewsArticles.Any())
        {
            var filteredList = allNewsArticles.Where(x => x.SmartForm.Date.Year == selectedYear).ToList();
            if (filteredList != null && filteredList.Any())
            {
                lvNewsList.DataSource = filteredList;
                lvNewsList.DataBind();
            }
        }
    }

    /// <summary>
    /// This method is used to get spot light news content from metadata
    /// </summary>
    private void GetNewsSpotLightContent()
    {
        long homeContentId = ConfigHelper.GetValueLong("HomePageCId");
        var homeContentData = ContentHelper.GetContentById(homeContentId, true);
        if (homeContentData != null && homeContentData.MetaData != null)
        {
            long metaSpotLightId = ConfigHelper.GetValueLong("MetaHomePageNewsSpotLightId");
            var metaHomeNews = homeContentData.MetaData.Where(x => x.Id == metaSpotLightId).FirstOrDefault();
            if (metaHomeNews != null && metaHomeNews.Text != string.Empty)
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

}