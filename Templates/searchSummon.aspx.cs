using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;

using SSADL.Summon;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;


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


        string sql = "SELECT  * FROM [ViewAllSubjectArea_SSADL] order by Name ASC";
        DataTable dtSA = DataBase.dbDataTable(sql, "Ektron.DbConnection");


        search = Server.UrlDecode(Request.QueryString["s"].Replace("/",""));
        viewListURL = "/searchsummonlist.aspx?s=" + search;
        if (search != null)
        {
            try
            {
                DataTable dtResultSubjectAreas = new DataTable();
                dtResultSubjectAreas.Columns.Add("ID");
                dtResultSubjectAreas.Columns.Add("Title");
                dtResultSubjectAreas.Columns.Add("URL");

                string Author = "";
                string Year = "";
                string contenttype = "";
                DataTable dtResult = new DataTable();
                dtResult.Columns.Add("Title");
                dtResult.Columns.Add("Link");
                dtResult.Columns.Add("Author");
                dtResult.Columns.Add("Year");
                dtResult.Columns.Add("Issue");
                dtResult.Columns.Add("ContentType");
                dtResult.Columns.Add("Description");
                dtResult.Columns.Add("ID");
                DataTableReader dtr = DataBase.dbDataTable("Select top 5 * from Resources where ResourceName like '%" + search + "%' or Description like '%" + search + "%' ").CreateDataReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string ID = dtr["ID"].ToString().Trim();
                        string Title = dtr["ResourceName"].ToString().Trim();
                        string Link = "/dynamicdb.aspx?resourceid=" + dtr["ID"].ToString().Trim();
                        string Description = dtr["Description"].ToString().Trim();

                        string SubjectAreasIDs = dtr["SubjectAreasTaxonomy"].ToString().Trim();


                        if (SubjectAreasIDs != "")
                        {
                            string[] SubjectAreaIDss = SubjectAreasIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string SubjectAreaID in SubjectAreaIDss)
                            {

                                DataRow[] result = dtSA.Select("TaxID='" + SubjectAreaID + "'");
                                foreach (DataRow row in result)
                                {
                                    dtResultSubjectAreas.Rows.Add(SubjectAreaID, row["Name"].ToString(), row["URL"].ToString());
                                }

                            }
                        }


                        dtResult.Rows.Add(Title, Link, Author, Year, "", "1", Description, ID);
                    }
                }

                //Response.Write(dtResultSubjectAreas.Rows.Count);
                listSubJectArea.DataSource = DataBase.RemoveDuplicateRows(dtResultSubjectAreas, "ID");
                listSubJectArea.DataBind();

                listViewDatabases.DataSource = dtResult;
                listViewDatabases.DataBind();



                string ApiId = System.Web.Configuration.WebConfigurationManager.AppSettings["SummonSSADLApiId"];
                string ApiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["SummonSSADLApiKey"];


                //  Console.WriteLine("XML");  http://api.summon.serialssolutions.com/2.0.0/search?s.hl=false&s.ho=true&s.q=Law

                var summon = new SummonXml(ApiId, ApiKey);
                summon.Debug = false;

                string buildString = "s.q=Title:(" + search + ")&s.ps=25";
                string getArticles = summon.Query(buildString + "&s.fvf=ContentType,Newspaper Article,false");
                string geteBooks = summon.Query(buildString + "&s.fvf=ContentType,Book / eBook,false");
                string getJournals = summon.Query(buildString + "&s.fvf=ContentType,Journal Article,false");




                var docArticles = new XmlDocument();
                var docEBooks = new XmlDocument();
                var docJournals = new XmlDocument();


                docArticles.LoadXml(getArticles);
                docEBooks.LoadXml(geteBooks);
                docJournals.LoadXml(getJournals);


                getSummary(docArticles, "Article", search);
                getSummary(docEBooks, "Book", search);
                getSummary(docJournals, "Journal", search);

                getOtherSummary(docArticles, "Article", search);

                try { DT_Articles = getAllDataItems(docArticles).AsEnumerable().Take(5).CopyToDataTable(); }
                catch { }
                try { DT_EBooks = getAllDataItems(docEBooks).AsEnumerable().Take(5).CopyToDataTable(); }
                catch { }
                try { DT_Journals = getAllDataItems(docJournals).AsEnumerable().Take(5).CopyToDataTable(); }
                catch { }

                listViewArticles.DataSource = DT_Articles;
                listViewArticles.DataBind();

                listVieweBooks.DataSource = DT_EBooks;
                listVieweBooks.DataBind();

                listViewJournals.DataSource = DT_Journals;
                listViewJournals.DataBind();
                ///

                // getSummary(docJournals);
                ///Content DM Search
                int GetContentDMCount = getReports("CISOSEARCHALL^" + search + "^any");
                if (GetContentDMCount > 0)
                {
                    ContentDMSearchResults = "  <a href='/resources/ssareportsarchive.aspx?show=simple&s=" + search + "'>Government Reports (" + GetContentDMCount.ToString() + ")</a> ";
                }

                //get other resources
            }
            catch
            {
                Response.Redirect(commonfunctions.host + "/searchsummon.aspx?s=" + search);
            }

        }
    }





    private string contentTypes = "Journal Article:Article:Book / eBook";
    private void fillListViews()
    {




    }



    protected void listSubJectAreaDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;
            HyperLink saLinkTitle = (HyperLink)ditem.FindControl("saLinkTitle");

            saLinkTitle.Text = item["Title"].ToString();
            saLinkTitle.NavigateUrl = "/" + item["URL"].ToString();
        }

    }
    protected void listViewDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;

            //dtResult.Columns.Add("Title");
            //dtResult.Columns.Add("Link");
            //dtResult.Columns.Add("Author");
            //dtResult.Columns.Add("Year");
            //dtResult.Columns.Add("Issue");
            //dtResult.Columns.Add("ContentType");


            HyperLink ResourceTitle = (HyperLink)ditem.FindControl("ResourceTitle");
            Literal ResourceYear = (Literal)ditem.FindControl("ResourceYear");
            Literal ResourceAuthor = (Literal)ditem.FindControl("ResourceAuthor");
            Literal DescriptionT = (Literal)ditem.FindControl("Description");
            Literal myfavicons = (Literal)ditem.FindControl("myfavicons");


            string ID = item["ID"].ToString();
            string year = item["Year"].ToString();
            string Title = item["Title"].ToString();
            string Link = item["Link"].ToString();
            string Author = item["Author"].ToString();
            string ContentType = item["ContentType"].ToString();
            DescriptionT.Text = item["Description"].ToString();

            ResourceTitle.Text = Title; // +" - " + ContentType;
            ResourceTitle.NavigateUrl = Link;
            if (year != "")
            {
                ResourceYear.Text = "(" + year + ")";
            }
            if (Author != "") ResourceAuthor.Text = "by " + Author;



            string collection = "0";
            myfavicons.Text = commonfunctions.getMyFavIcons(ID, ContentType, Title, collection, Link, year, Author);
        }

    }

    public DataTable DT_Articles;
    public DataTable DT_EBooks;
    public DataTable DT_Journals;
    public DataTable DT_Databases;

    private DataTable getAllDataItems(XmlDocument xmlDoc)
    {
        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("Title");
        dtResult.Columns.Add("Link");
        dtResult.Columns.Add("Author");
        dtResult.Columns.Add("Year");

        dtResult.Columns.Add("ContentType");
        dtResult.Columns.Add("Description");
        dtResult.Columns.Add("ID");
        XmlNodeList xmlDocuments = xmlDoc.SelectNodes("/response/documents/document");
        // try
        // {

        foreach (XmlNode node in xmlDocuments)
        {

            XmlNodeList fields = node.SelectNodes("field");



            string title = ""; //levelDict["PublicationTitle"];
            string ID = "0";
            string Author = "";
            string Year = "";
            string contenttype = "";
            string Link = "";
            string Description = "";
            foreach (XmlNode node2 in fields)
            {

                string name = node2.Attributes["name"].Value;
                switch (name)
                {
                    case "Title":
                        title = commonfunctions.StripTagsRegex(node2.InnerText);
                        break;
                    case "PublicationYear":
                        Year = node2.InnerText;
                        break;
                    case "Author":
                        Author = node2.InnerText;
                        break;
                    case "ContentType":
                        contenttype = node2.InnerText;
                        break;
                    case "URI":
                        Link = node2.InnerText;
                        break;
                    case "ID":
                        ID = node2.InnerText;
                        break;
                    case "Snippet":
                        Description = node2.InnerText;
                        break;
                }

            }

            if (Link != "")
            {
                dtResult.Rows.Add(title, Link, Author, Year, "4", Description, ID);
            }

        }
        //}
        //catch { }


        return dtResult;
    }
    private void getSummary(XmlDocument xmlDoc, string fieldName, string searchstring)
    {

        XmlNodeList xmlfacetFields = xmlDoc.SelectNodes("/response");

        foreach (XmlNode node in xmlfacetFields)
        {
            // Response.Write(node.Attributes["value"].Value + "<br/>");
            string Count = node.Attributes["recordCount"].Value;
            //string facetValue = node.Attributes["value"].Value;

            //<span class="italic">Showing all results for Databases</span>

            switch (fieldName)
            {
                case "Journal":
                    //Journal Article
                    JournalsHyperLink.Text = "View " + long.Parse(Count).ToString("N0") + " results in Journals >";
                    JournalsHyperLink.NavigateUrl = "/searchsummondetails.aspx?pageNo=1&field=Journal Article&s=" + searchstring + "&pagesize=50";
                    break;

                case "Article":
                    //Newspaper Article
                    ArticlesHyperLink.Text = "View " + long.Parse(Count).ToString("N0") + " results in Articles >";
                    ArticlesHyperLink.NavigateUrl = "/searchsummondetails.aspx?pageNo=1&field=Newspaper Article&s=" + searchstring + "&pagesize=50";
                    break;

                case "Book":
                    //Book / eBook
                    ebooksHyperLink.Text = "View " + long.Parse(Count).ToString("N0") + " results in eBooks >";
                    ebooksHyperLink.NavigateUrl = "/searchsummondetails.aspx?pageNo=1&field=Book / eBook&s=" + searchstring + "&pagesize=50";
                    break;


                //case "Databases":
                //    DataBasesHyperLink.Text = "View " + long.Parse(Count).ToString("N0") + " results in Databases >";
                //    break;

            }



        }


    }


    private void getOtherSummary(XmlDocument xmlDoc, string fieldName, string searchstring)
    {

        XmlNodeList xmlfacetFields = xmlDoc.SelectNodes("/response/facetFields/facetField/facetCount");

        foreach (XmlNode node in xmlfacetFields)
        {
            // Response.Write(node.Attributes["value"].Value + "<br/>");
            string Count = node.Attributes["count"].Value;
            string facetValue = node.Attributes["value"].Value;

            //<span class="italic">Showing all results for Databases</span>

            switch (facetValue)
            {
                case "Audio Recording":
                    if (long.Parse(Count) > 0)
                    {
                        AudioHyperLink.Text = facetValue + " (" + long.Parse(Count).ToString("N0") + ")";
                        AudioHyperLink.NavigateUrl = "/searchsummondetails.aspx?pageNo=1&field=" + facetValue + "&s=" + searchstring + "&pagesize=50";
                    }
                    break;

                case "Video Recording":
                    if (long.Parse(Count) > 0)
                    {
                        VideoHyperLink.Text = facetValue + " (" + long.Parse(Count).ToString("N0") + ")";
                        VideoHyperLink.NavigateUrl = "/searchsummondetails.aspx?pageNo=1&field=" + facetValue + "&s=" + searchstring + "&pagesize=50";
                    }
                    break;

                case "Photograph":
                    if (long.Parse(Count) > 0)
                    {
                        PhotographHyperLink.Text = facetValue + " (" + long.Parse(Count).ToString("N0") + ")";
                        PhotographHyperLink.NavigateUrl = "/searchsummondetails.aspx?pageNo=1&field=" + facetValue + "&s=" + searchstring + "&pagesize=50";
                    }
                    break;


                //case "Databases":
                //    DataBasesHyperLink.Text = "View " + long.Parse(Count).ToString("N0") + " results in Databases >";
                //    break;

            }



        }


    }
    private int getReports(string searchString)
    {

        ////        "searchstrings is a four-part, ^-delimited group in the order field^string^mode^operator.
        ////Use "CISOSEARCHALL" for all fields; mode can be "all", "any", "exact", or "none"; operator can be "and" or "or".
        ////Multiple words in string need to be separated by "+".
        ////Up to six groups can be included, delimited by "!".
        ////To browse a collection, specify a single alias and "0" as a searchstrings value. The operator for the last searchstring will be ignored."
        ///http://www.contentdm.org/help6/custom/customize2h.asp

        string fields = "title!descri!creato!publis!date";  //!-delimited list of field nicknames, listing the fields for which metadata should be returned. A maximum of five fields may be specified. A maximum of 100 bytes is returned for each field.
        string sortby = "title"; // !-delimited list of field nicknames, detailing how the result should be sorted, in field order.  The field nicknames must appear in the field array. If the last element in the array is specified as "reverse", the sort will be in reverse (descending) order. Use "nosort" to sort the query by relevance.


        string maxrecs = "1024"; //maximum number of records to return, from 1 to 1024.
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

        //WebRequest.DefaultWebProxy = new WebProxy("http://access.lb.ssa.gov:80/", true);





        WebRequest request = WebRequest.Create(url);
        if (commonfunctions.Environment == "PROD" || commonfunctions.Environment == "DEV" )
        {
            WebProxy wp = new WebProxy("http://access.lb.ssa.gov:80/", true);
            request.Proxy = wp;
        }
        WebResponse response = request.GetResponse();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(response.GetResponseStream());


        XmlNodeList xmlDocuments = xmlDoc.SelectNodes("/results/records/record");

        return xmlDocuments.Count;


    }




    public string ContentDMSearchResults { get; set; }

    public string viewListURL { get; set; }

    public string search { get; set; }
}