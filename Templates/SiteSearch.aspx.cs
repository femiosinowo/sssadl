using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Web;


 
 
 
using System.Web.UI;
using System.Web.UI.WebControls;

 

using Ektron.Cms;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.Framework.UI.Controls.EktronUI;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;
using Ektron.Cms.WebSearch.SearchData;
using Ektron.Cms.Search.Compatibility;
using Ektron.Cms.Search;
using Ektron.Cms.Search.Expressions;


public partial class Templates_Content : System.Web.UI.Page
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

        }


        contentId = long.Parse(Request.QueryString["id"]);

        RightSideContent.ccontentID = contentId.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = contentId.ToString();
        uxBreadcrumb.contentID = contentId.ToString();
        uxPageTitle.ResourceTypeId = "2";
        if (Request.QueryString["s"] != null)
        {

            searchWord = Request.QueryString["s"].ToString();

        }
        else
        {
            pagerupper.Visible = false;
            pagerbelow.Visible = false;
        }
    }


    protected void SearchResult_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            //ListViewDataItem ditem = (ListViewDataItem)e.Item;
            ////data reader
            //System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            //   System.Data.Common.DbDataRecord item = (System.Data.Common.DbDataRecord)ditem.DataItem;
            DataRowView item = (DataRowView)ditem.DataItem;


            // Literal imageLink = (Literal)ditem.FindControl("imageLink");
            HyperLink searchTitleLink = (HyperLink)ditem.FindControl("searchTitleLink");
            Literal SearchTeaser = (Literal)ditem.FindControl("SearchTeaser");
            // Literal searchLink = (Literal)ditem.FindControl("searchLink");

            Literal myFav = (Literal)ditem.FindControl("myFav");

            string contentId = item["ContentID"].ToString().Trim();
            string contentTitle = item["Title"].ToString().Trim();

            myFav.Text = commonfunctions.getMyFavIcons(contentId, "2", contentTitle);


            // host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
            searchTitleLink.Text = item["Title"].ToString().Trim(); // cf.getFieldValue(XMLDoc, "NewsArticleTitle");
            searchTitleLink.NavigateUrl = item["QuickLink"].ToString(); // newsLink;
            // searchLink.Text = commonfunctions.host + item["QuickLink"].ToString();

            string teaser = string.Empty;
            teaser = item["Blurb"].ToString().Trim();
            long teaserlength = teaser.Length;
            if (teaserlength > 150)
            {
                teaser = teaser.Substring(0, 150);
            }
            else if (teaserlength != 0)
            {
                teaser = teaser.Substring(0, Convert.ToInt16(teaserlength));
            }

            SearchTeaser.Text = teaser.Trim();


        }
    }

    public DataTable dtSearch(string searchTerm, string category = "", long smartformID = 0)
    {

        DataTable dtresults = new DataTable();

        dtresults.Columns.Add("ContentID");
        dtresults.Columns.Add("Title");
        dtresults.Columns.Add("Blurb");
        dtresults.Columns.Add("QuickLink");
        //    dtresults.Columns.Add("CopyRightDate");

        List<string> contentIDs = new List<string>();


        // searching the whole site for content that contains test
        //  Ektron.Cms.WebSearch.SearchData.SearchResponseData[] srdlist = GetSearchResultsBasic(searchTerm, category); //page 1 of search results
        Ektron.Cms.WebSearch.SearchData.SearchResponseData[] srdlist = null;
        srdlist = GetSearchResultsBasic(searchTerm); //page 1 of search results

        if (srdlist.Length > 0)
        {
            searchCount = "Showing " + srdlist.Length.ToString() + " results";

            foreach (Ektron.Cms.WebSearch.SearchData.SearchResponseData srd in srdlist)
            {

                long lngContentID = long.Parse(srd.ContentID.ToString());

                //add no image
                if (!srd.QuickLink.Contains("uploadedImages"))
                {
                    dtresults.Rows.Add(srd.ContentID, srd.Title, srd.Summary, srd.QuickLink);
                    //contentIDs.Add(srd.ContentID.ToString());
                }

            }



        }


        return dtresults;


    }

    public string SortByURL { get; set; }
    ///// <summary>
    ///// Returns Ektron.Cms.WebSearch.SearchData.SearchResponseData()
    ///// </summary>
    ///// <param name="folderId">Folder id. Set to 0 if searching all folders.</param>
    ///// <param name="recursive">True includes subfolders, False just one folder</param>
    ///// <param name="pageNumber">Page number of results, first page is 0</param>
    public Ektron.Cms.WebSearch.SearchData.SearchResponseData[] GetSearchResultsBasic(string searchString, string Category = "")
    {

        Ektron.Cms.API.Search.SearchManager search = new Ektron.Cms.API.Search.SearchManager();
        Ektron.Cms.WebSearch.SearchData.SearchRequestData searchRequest = new Ektron.Cms.WebSearch.SearchData.SearchRequestData();
        Ektron.Cms.WebSearch.SearchData.SearchResponseData[] searchResponse = null;

        //Ektron.Cms.ContentSearchCondition isOnlyContent = new Ektron.Cms.ContentSearchCondition();
        //isOnlyContent.setType = Ektron.Cms.Common.EkEnumeration.SearchType.AND;
        //isOnlyContent.setVariable = Ektron.Cms.Common.EkEnumeration.Content.

        Int32 resultCount = 0;
        //Build Search Object

        if (SortByURL == "date")
        {

            searchRequest.OrderBy = WebSearchOrder.DateCreated;
        }
        else
        {

            searchRequest.OrderBy = Ektron.Cms.WebSearch.SearchData.WebSearchOrder.Rank;
        }

        //  searchRequest.OrderBy = Ektron.Cms.WebSearch.SearchData.WebSearchOrder.Rank;
        searchRequest.SearchObjectType = Ektron.Cms.WebSearch.SearchData.SearchForType.Content;
        searchRequest.SearchText = searchString;
        searchRequest.Recursive = true; // Choose whether to include child folders in search results
        searchRequest.EnablePaging = true;
        searchRequest.LanguageID = 1033;
        //searchRequest.EnablePaging = true;
        searchRequest.PageSize = 200; // number of results you want per page
        searchRequest.SearchFor = Ektron.Cms.WebSearch.SearchDocumentType.all;

        // searchRequest.CurrentPage = pageNumber; // the page number of the results

        if (Category != "")
        {
            searchRequest.Category = Category; // "\\ACA Topics , \\ACA Document Types";
        }
        searchResponse = search.Search(searchRequest, HttpContext.Current, ref resultCount);
        return searchResponse;
    }



    protected void Datapager_prender(object sender, EventArgs e)
    {

         searchString = Request.QueryString["s"];
        if (searchString != null)
        {


            SearchResultLV.DataSource = dtSearch(searchString); ;
            SearchResultLV.DataBind();

            int totalrowcount = DataPager1.TotalRowCount;
            int pagesize = DataPager1.PageSize;
            int startrowindex = DataPager1.StartRowIndex;


            labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();

            int toCountNumber = DataPager1.StartRowIndex + DataPager1.PageSize > DataPager1.TotalRowCount ? DataPager1.TotalRowCount : DataPager1.StartRowIndex + DataPager1.PageSize;
            idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString();
         //   idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + " of " + totalrowcount.ToString();
       
        
                long currentPage = (startrowindex / DataPager1.PageSize) + 1;
        LinkButton PreviousLink = DataPager1.Controls[1].Controls[0] as LinkButton;
        LinkButton NextLink = DataPager1.Controls[3].Controls[0] as LinkButton;

        if (PreviousLink != null)
        {
            if (currentPage.Equals(1))
            {
                PreviousLink.Visible = false;
            }
            else if (currentPage > 1)
            {
                PreviousLink.Visible = true;
            }
        }
        if (NextLink != null)
        {
            if ((currentPage * DataPager1.PageSize) >= DataPager1.TotalRowCount)
            {
                NextLink.Visible = false;
            }
            else
            {
                NextLink.Visible = true;
            }
        }

              if (Math.Ceiling((double)totalrowcount / pagesize) <= 1)
        {
            pagerbelow.Visible = false;
                  pagerupper.Visible = false;
        }


        }

    }

    protected void Index_Changed(Object sender, EventArgs e)
    {
        DataPager1.PageSize = Convert.ToInt16(show_results.SelectedValue);


    }

    protected void goBtnClick(object sender, EventArgs e)
    {
        if (pager_textbox.Text != "")
            DataPager1.SetPageProperties((int.Parse(pager_textbox.Text) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
    }




    //    protected void searchsmartForm(long smartformID, string searchString)
    //    {
    //        // Session["whatSearch"] = "categorysearch";

    //        // Response.Write(SortByURL);





    //        DataTable dtresults2 = new DataTable();

    //        dtresults2.Columns.Add("ContentID");
    //        dtresults2.Columns.Add("Title");
    //        dtresults2.Columns.Add("Blurb");
    //        dtresults2.Columns.Add("QuickLink");



    //        KeywordSearchCriteria criteria = new KeywordSearchCriteria();
    //        if (SortByURL == "date")
    //        {


    //            criteria.OrderBy = new List<OrderData>()
    //{
    //    new OrderData(SearchContentProperty.DateCreated, OrderDirection.Descending)
    //};
    //        }
    //        else
    //        {

    //            criteria.OrderBy = new List<OrderData>()
    //{
    //    new OrderData(SearchContentProperty.Rank, OrderDirection.Descending)
    //};

    //        }

    //        criteria.PagingInfo = new PagingInfo(500);
    //        criteria.PagingInfo.CurrentPage = 1;
    //        criteria.ReturnProperties = new HashSet<PropertyExpression>()
    //{
    //    SearchContentProperty.Id,
    //    SearchContentProperty.Title,
    //    SearchContentProperty.QuickLink,
    //    SearchContentProperty.HighlightedSummary
    //};

    //        ContentManager contentManager = new ContentManager();
    //        Expression expressionTree = Ektron.Cms.Search.SearchType.IsNonUserContent();
    //        PropertyExpression propertyExpression = SearchContentProperty.XmlConfigId;
    //        Expression expression = new EqualsExpression(propertyExpression, smartformID);
    //        if (contentManager.RequestInformation.ContentLanguage == 0)
    //            expressionTree &= SearchContentProperty.Language.EqualTo(contentManager.RequestInformation.DefaultContentLanguage);
    //        else if (contentManager.RequestInformation.ContentLanguage > 0)
    //            expressionTree &= SearchContentProperty.Language.EqualTo(contentManager.RequestInformation.ContentLanguage);

    //        criteria.ExpressionTree = expression;

    //        //Pass in query text
    //        criteria.QueryText = searchString; // uxSearchText.Text;

    //        ISearchManager manager = ObjectFactory.GetSearchManager();

    //        Ektron.Cms.Search.SearchResponseData response = manager.Search(criteria);


    //        searchCount = "Showing " + response.Results.Count.ToString() + " results";
    //        foreach (Ektron.Cms.Search.SearchResultData srd in response.Results)
    //        {

    //            string rID = srd[SearchContentProperty.Id].ToString();
    //            string rTitle = srd[SearchContentProperty.Title].ToString();
    //            string rBlurb = srd[SearchContentProperty.HighlightedSummary].ToString();
    //            string rQuickLink = srd[SearchContentProperty.QuickLink].ToString();

    //            dtresults2.Rows.Add(rID, rTitle, rBlurb, rQuickLink);
    //        }
    //        // Response.Write(dtresults2.Rows.Count.ToString());
    //        SearchResultLV.DataSource = dtresults2;
    //        SearchResultLV.DataBind();
    //        // SearchResultLV.Visible = false;
    //    }
    public string searchString { get; set; }
    public string searchMessage { get; set; }

    public string searchCount { get; set; }
    public string SortedbyDate { get; set; }
    public string SortedbyRank { get; set; }


    public long contentId { get; set; }
     public string searchWord { get; set; }
 
}