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




        try
        {
            pageNo = Request.QueryString["pageNo"].ToString();
        }
        catch { pageNo = "1"; }

        if (pageNo == "1") PrevBtn.Enabled = false;
        search = Request.QueryString["s"];

        ViewTypeURL = "/searchsummon.aspx?s=" + search;

        if (search != null)
        {

            try{
            string sfvf = "&s.fvf=ContentType,Journal / eJournal,false&s.fvf=ContentType,Book / eBook,false&s.fvf=ContentType,Newspaper Article,false";


            string ApiId = System.Web.Configuration.WebConfigurationManager.AppSettings["SummonSSADLApiId"];
            string ApiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["SummonSSADLApiKey"];



            var summon = new SummonXml(ApiId, ApiKey);
            summon.Debug = false;
           string   searchStringg = string.Empty;
         //   string fields = "Title;SubjectTerms;Author;PublicationTitle;";
            string fields = "Title;";
            string[] fieldss = fields.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string field in fieldss)
            {
                searchStringg += "s.q=" + field + ":(" + search + ")&";
            }
           // string buildString = "s.q=Title:(" + search + ")&s.q=SubjectTerms:(" + search + ")&s.q=Author:(" + search + ")&s.q=PublicationTitle:(" + search + ")&s.ps=20";
            string getArticles = summon.Query(searchStringg + "s.ff=ContentType,or,1,50&s.ps=20" + sfvf);




            var docArticles = new XmlDocument();
            docArticles.LoadXml(getArticles);
            getSummary(docArticles, "Article", search);
            DT_Articles = getAllDataItems(docArticles);
            listViewArticles.DataSource = DT_Articles;
            listViewArticles.DataBind();
            }
            catch
            {
                Response.Redirect(commonfunctions.host + "/searchsummonlist.aspx?s=" + search);
            }
        }
    }






    protected void listViewDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;



            HyperLink ResourceTitle = (HyperLink)ditem.FindControl("ResourceTitle");
            Literal ResourceYear = (Literal)ditem.FindControl("ResourceYear");
            Literal ResourceAuthor = (Literal)ditem.FindControl("ResourceAuthor");
            Literal DescriptionT = (Literal)ditem.FindControl("Description");
            Literal Location = (Literal)ditem.FindControl("Location");
            Literal SummonContentType = (Literal)ditem.FindControl("SummonContentType");
            Literal myfavicons = (Literal)ditem.FindControl("myfavicons");


            string ID = item["ID"].ToString();
            string year = item["Year"].ToString();
            string Title = item["Title"].ToString();
            string Link = item["Link"].ToString();
            string Author = item["Author"].ToString();
            string ContentType = item["ContentType"].ToString();
            DescriptionT.Text = item["Description"].ToString();
            SummonContentType.Text = item["SummonContentType"].ToString();
            Location.Text = item["Location"].ToString();


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
        dtResult.Columns.Add("SummonContentType");
        dtResult.Columns.Add("Location");
        
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
            string Location = "";
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
                    case "GeographicLocations":
                        Location = node2.InnerText;
                        break;
                        
                }

            }

            dtResult.Rows.Add(title, Link, Author, Year, "4", Description, ID, contenttype, Location);

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
            pagesize = "20";
            long totalrowcount = Convert.ToInt64(Count); // DataPager1.TotalRowCount;
            long pagesiz = Convert.ToInt64(pagesize);
            long startrowindex = (Convert.ToInt64(pageNo) - 1) * pagesiz;


            labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesiz).ToString("N0");

            long toCountNumber = startrowindex + pagesiz > totalrowcount ? totalrowcount : startrowindex + pagesiz;
         //   idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString("N0");
            if (pageNo == "1")
            {
                h3Results = "First 20 Results";
            }
            else
            {
                h3Results = "Showing  " + (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString("N0") + " Results";

            }

        }


    }
    protected void NextBtn_Click(object sender, EventArgs e)
    {
        moveToPage("next");
    }
    protected void PrevBtn_Click(object sender, EventArgs e)
    {

        moveToPage("prev");
    }

    protected void goBtnClick(object sender, EventArgs e)
    {
        if (pager_textbox.Text != "")
        {

            //   search = Request.QueryString["s"];
            // pageNo = Request.QueryString["pageNo"].ToString();
            //   pagesize = Request.QueryString["pagesize"].ToString();
            string url = "/searchsummonlist.aspx?pageNo=" + pager_textbox.Text + "&s=" + search;
            Response.Redirect(url);
        }
        //  DataPager1.SetPageProperties((int.Parse(pager_textbox.Text) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
    }
    protected void moveToPage(string nextPrev)
    {
        // search = Request.QueryString["s"];
        // pageNo = Request.QueryString["pageNo"].ToString();

        if (nextPrev == "next") pageNo = (Convert.ToInt16(pageNo) + 1).ToString();
        if (nextPrev == "prev") pageNo = (Convert.ToInt16(pageNo) - 1).ToString();

        string url = "/searchsummonlist.aspx?pageNo=" + pageNo + "&s=" + search;
        Response.Redirect(url);
    }

    public string pageNo { get; set; }

    public string search { get; set; }

    public string field { get; set; }

    public string pagesize { get; set; }



    public string ViewTypeURL { get; set; }

    public string h3Results { get; set; }
}