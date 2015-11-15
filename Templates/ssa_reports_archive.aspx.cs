using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.IO;
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
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                long contentId = long.Parse(Request.QueryString["id"]);
                //    this.GetContentData(contentId);
            }
        }




        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();



        if (!string.IsNullOrEmpty(Request.QueryString["show"]))
        {
            string show = Request.QueryString["show"].ToString();
            if (show == "advance")
            {

                AdvanceSearch.Visible = true;
                simpleSearch.Visible = false;

                //     getAdvanceSearchOptions();

            }
            if (show == "simple")
            {

                AdvanceSearch.Visible = false;
                simpleSearch.Visible = true;


                if (!string.IsNullOrEmpty(Request.QueryString["s"]))
                {
                    searchWord = Request.QueryString["s"].ToString();

                }

                searchString = "CISOSEARCHALL^" + searchWord + "^any";
            }

        }

        ///https://server16760.contentdm.oclc.org/dmwebservices/index.php?q=dmGetCollectionList/param1/param2/xml
        ///http://CdmServer.com:port/dmwebservices/index.php?q=dmQuery/alias/searchstrings/fields/sortby/maxrecs/start/suppress/docptr/suggest/facets/showunpub/denormalizeFacets/format     
        //////https://server16760.contentdm.oclc.org/dmwebservices/index.php?q=dmQuery/all/dollars/title!subjec!creato!publis!date/title/250/1/0/0/0/0/0/0/xml 
        ///Tutorials from : http://www.contentdm.org/help6/custom/customize2h.asp
        ///


        /////cdm/compoundobject/collection/p16760coll5/id/9984/rec/2




    }



    protected void SearchResult_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;
            string id = (e.Item.DataItemIndex + 1).ToString();

            // Literal imageLink = (Literal)ditem.FindControl("imageLink");
            HyperLink TitleLink = (HyperLink)ditem.FindControl("TitleLink");
            Literal Teaser = (Literal)ditem.FindControl("Teaser");
            // Literal searchLink = (Literal)ditem.FindControl("searchLink");

            Literal myFav = (Literal)ditem.FindControl("myFav");
            string collection = item["Collection"].ToString();
            string contentId = item["ID"].ToString().Trim();
            string contentTitle = item["Title"].ToString().Trim();

            myFav.Text = commonfunctions.getMyFavIcons(contentId, "3", contentTitle, collection);


            string publisher = item["Publisher"].ToString();
            if (publisher != "") publisher = "<br/>" + publisher;
            string year = item["Year"].ToString();
            if (year != "") year = "<br/> Published: " + year;

            TitleLink.Text = item["Title"].ToString().Trim();
            //http://cdm16760.contentdm.oclc.org/cdm/compoundobject/collection/p16760coll2/id/991/rec/2
            //https://cdm16760.contentdm.oclc.org/cdm/compoundobject/collection//p16760coll2/id/991/rec/2
            TitleLink.NavigateUrl = "https://cdm16760.contentdm.oclc.org/cdm/compoundobject/collection" + collection + "/id/" + item["ID"].ToString() + "/rec/" + id;
            TitleLink.Target = "_blank";
            // TitleLink.NavigateUrl = "/templates/ssa_reportsdetails.aspx?collection=" + collection + "&pointer=" + item["ID"].ToString();
            Teaser.Text = item["Description"].ToString().Trim() + " <i style='font-size:small'>" + publisher + " " + year + "</i>";

            //  DataPager1.Visible = DataPager1.TotalRowCount > DataPager1.MaximumRows;
        }
    }
    protected void Datapager_prender(object sender, EventArgs e)
    {


        if (searchWord != null && searchWord != "")
        {


            SearchResultLV.DataSource = getReports(searchString); ;
            SearchResultLV.DataBind();


            int totalrowcount = DataPager1.TotalRowCount;
            int pagesize = DataPager1.PageSize;
            int startrowindex = DataPager1.StartRowIndex;
            if (totalrowcount > 0)
            {
                pagerbelow.Visible = true;
                pagertop.Visible = true;

            }
            else
            {
                pagerbelow.Visible = false;
                pagertop.Visible = false;
            }

            // Response.Write(totalrowcount.ToString());
            labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();


            long currentPage = (startrowindex / DataPager1.PageSize) + 1;


            pager_dropdown.Items.Clear();
            double howmanyPages = Math.Ceiling((double)totalrowcount / pagesize);
            labelTotalPages.Text = howmanyPages.ToString();

            for (double i = 1; i <= howmanyPages; i++)
            {
                pager_dropdown.Items.Add(i.ToString());
            }
            try
            {
                pager_dropdown.Items.FindByValue(currentPage.ToString()).Selected = true;
            }
            catch { }


            int toCountNumber = DataPager1.StartRowIndex + DataPager1.PageSize > DataPager1.TotalRowCount ? DataPager1.TotalRowCount : DataPager1.StartRowIndex + DataPager1.PageSize;
            idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString();
            //idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + " of " + totalrowcount.ToString();

            //long currentPage = (startrowindex / DataPager1.PageSize) + 1;
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
                //  pagerupper.Visible = false;
            }

        }

    }

    private DataTable getReports(string searchString)
    {

        ////        "searchstrings is a four-part, ^-delimited group in the order field^string^mode^operator.
        ////Use "CISOSEARCHALL" for all fields; mode can be "all", "any", "exact", or "none"; operator can be "and" or "or".
        ////Multiple words in string need to be separated by "+".
        ////Up to six groups can be included, delimited by "!".
        ////To browse a collection, specify a single alias and "0" as a searchstrings value. The operator for the last searchstring will be ignored."
        ///http://www.contentdm.org/help6/custom/customize2h.asp

        string fields = "title!descri!creato!publis!date";  //!-delimited list of field nicknames, listing the fields for which metadata should be returned. A maximum of five fields may be specified. A maximum of 100 bytes is returned for each field.
        string sortby = "title"; // !-delimited list of field nicknames, detailing how the result should be sorted, in field order.  The field nicknames must appear in the field array. If the last element in the array is specified as "reverse", the sort will be in reverse (descending) order. Use "nosort" to sort the query by relevance.


        string maxrecs = "250"; //maximum number of records to return, from 1 to 1024.
        string start = "0"; // starting number of the first item returned in the result.
        string suppress = "1"; //specifies whether to suppress compound object pages from the search. Use "1" to suppress or "0" to not suppress (i.e., include pages).
        string docptr = "0"; // specifies the pointer value of a compound object to restrict the query just to the pages in that compound object. This requires that a single alias be specified. Use "0" if not specified.
        string suggest = "0"; //specifies whether to return a spelling suggestion, if available for the searchstring's string. Use "1" to get suggestions or use "0" to not return a suggestion.
        string facets = "0"; //an optional !-delimited list of field nicknames to return as facets. Use "0" if not requesting facets.
        string showunpub = "0"; //specifies whether to show or not show items from unpublished collections. Use "1" if requesting items from unpublished collections or "0" to hide items from unpublished collections.
        string denormalizeFacets = "0"; //specifies whether to show capitalization and diacritics in facet fields that have shared Controlled Vocabulary. Use "1" if requesting capitalization and diacritics or "0" to turn off capitalization and diacritics.
        string format = "xml"; //either "xml" or "json".
        string url = commonfunctions.contentDMServer + "dmwebservices/index.php?q=dmQuery/all/" + searchString + "/" + fields + "/" + sortby + "/" + maxrecs + "/" + start + "/" + suppress + "";
        url += "/" + docptr + "/" + suggest + "/" + facets + "/" + showunpub + "/" + denormalizeFacets + "/" + format;

        //https://server16760.contentdm.oclc.org/dmwebservices/index.php?q=dmQuery/all/CISOSEARCHALL^policy^any^and!
        //http://contentdm2.clemson.edu:2012/dmwebservices/index.php?q=dmQuery/rvl/CISOSEARCHALL^Oxypolis^any^and!
        //Response.Write(url);

        //http://cdm16760.contentdm.oclc.org/cdm/compoundobject/collection/p16760coll2/id/991/rec/2
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("ID");
        dtResult.Columns.Add("Title");
        dtResult.Columns.Add("Description");
        dtResult.Columns.Add("Publisher");
        dtResult.Columns.Add("Year");
        dtResult.Columns.Add("Collection");
        WebRequest request = WebRequest.Create(url);
        if (commonfunctions.Environment == "PROD" || commonfunctions.Environment == "DEV")
        {
            WebProxy wp = new WebProxy("http://access.lb.ssa.gov:80/", true);
            request.Proxy = wp;
        }
        WebResponse response = request.GetResponse();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(response.GetResponseStream());


        XmlNodeList xmlDocuments = xmlDoc.SelectNodes("/results/records/record");

        // Response.Write(xmlDocuments.Count);
        foreach (XmlNode node in xmlDocuments)
        {

            string id = node["pointer"].InnerText;
            string title = node["title"].InnerText;
            string descri = node["descri"].InnerText;
            string creato = node["creato"].InnerText;
            string publis = node["publis"].InnerText;
            string date = node["date"].InnerText;
            string collection = node["collection"].InnerText;
            // Response.Write(title);


            dtResult.Rows.Add(id, title, descri, publis, date, collection);
        }



        return dtResult;

    }


    private string returnCDATA(XmlNode cDataNode)
    {

        if (cDataNode.NodeType == XmlNodeType.CDATA)
        {
            return " ";

        }

        else
        {
            return "";
        }
    }

    protected void Index_Changed(Object sender, EventArgs e)
    {
        DataPager1.PageSize = Convert.ToInt16(show_results.SelectedValue);


    }

    protected void pager_dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataPager1.SetPageProperties((int.Parse(pager_dropdown.SelectedValue) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);

    }


    protected void goBtnClick(object sender, EventArgs e)
    {
        if (pager_textbox.Text != "")
            DataPager1.SetPageProperties((int.Parse(pager_textbox.Text) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
    }



    public string searchString { get; set; }

    protected void SearchAdvance_Click(object sender, EventArgs e)
    {
        searchWord = "Advance";
        string kywrd = KeywordTxt.Text;
        string auth = AuthorTxt.Text;
        string titl = TitleTxt.Text;
        string yr = YearTxt.Text;
        string subj = SubjectTxt.Text;


        if (auth != "")
        {

            searchString += "!publis^" + auth + "^all^and";
        }
        if (titl != "")
        {

            searchString += "!title^" + titl + "^all^and";
        }
        if (yr != "")
        {

            searchString += "!date^" + yr + "^all^and";
        }
        if (subj != "")
        {

            searchString += "!subjec^" + subj + "^all^and";
        }
        // Response.Write(searchString);
    }

    public string searchWord { get; set; }
}