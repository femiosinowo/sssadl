using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using SSADL.Summon;
using SSADL.CMS;
using System.Xml;
using System.Data;

 
 
 
using System.Web.UI;
using System.Web.UI.WebControls;




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

            if (!string.IsNullOrEmpty(Request.QueryString["field"]))
            {
                try
                {
                    field = Request.QueryString["field"].ToString();
                    search = Request.QueryString["s"];
                    pageNo = Request.QueryString["pageNo"].ToString();
                   // pagesize = Request.QueryString["pagesize"].ToString();

                    if (long.Parse(pageNo) <= 1) PrevBtn.Visible = false;
                    if (long.Parse(pageNo) == 20) NextBtn.Visible = false;
                    if (long.Parse(pageNo) > 20) pageNo = "1";
                    string ApiId = System.Web.Configuration.WebConfigurationManager.AppSettings["SummonSSADLApiId"];
                    string ApiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["SummonSSADLApiKey"];


                    var summon = new SummonXml(ApiId, ApiKey);
                    summon.Debug = true;


                    string getXMLs = summon.Query("s.q=Title:(" + search + ")&s.fvf=ContentType," + field + ",false&s.ps=50&s.pn=" + pageNo);


                    var docXML = new XmlDocument();
                    docXML.LoadXml(getXMLs);


                    DataTable DT_SearchResults = getAllDataItems(docXML);

                    listView.DataSource = DT_SearchResults;
                    listView.DataBind();

                    getSummary(docXML, field, search);
                }
                catch
                {
                    field = Request.QueryString["field"].ToString();
                    search = Request.QueryString["s"];

                    //http://digitallibraryadmin.ba.ad.ssa.gov/searchsummondetails.aspx?pageNo=1&field=Newspaper Article&s=books&pagesize=50
                    Response.Redirect(commonfunctions.host + "/searchsummondetails.aspx?field=" + field + "&pageNo=" + pageNo + "&s=" + search);
                }

            }
        }


        contentId = long.Parse(Request.QueryString["id"]);

        RightSideContent.ccontentID = contentId.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = contentId.ToString();
        uxBreadcrumb.contentID = contentId.ToString();
        uxPageTitle.ResourceTypeId = "2";

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
    private void getSummary(XmlDocument xmlDoc, string fieldName, string searchstring)
    {
        XmlNodeList xmlfacetFields = xmlDoc.SelectNodes("/response");
        foreach (XmlNode node in xmlfacetFields)
        {
            Count = node.Attributes["recordCount"].Value;

            long totalrowcount = long.Parse(Count); // DataPager1.TotalRowCount;
            long pagesiz = 50; // long.Parse(pagesize);
            long startrowindex = (long.Parse(pageNo) - 1) * pagesiz;

            double howmanyPages = Math.Ceiling((double)totalrowcount / pagesiz);
            if (howmanyPages > 20)
            {
                labelTotalPages.Text = "20";
            }
            else
            {
                  labelTotalPages.Text = howmanyPages.ToString("N0");
            }

            for (double i = 1; i <= howmanyPages; i++)
            {
                pager_dropdown.Items.Add(i.ToString());
                if (i == 20) break;

            }

          //  labelTotalPages.Text = howmanyPages.ToString("N0");

            long toCountNumber = startrowindex + pagesiz > totalrowcount ? totalrowcount : startrowindex + pagesiz;
            idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString("N0");
           

            try
            {
                pager_dropdown.Items.FindByValue(pageNo.ToString()).Selected = true;
            }
            catch { }

            if (totalrowcount < 50)
            {
                NextBtn.Visible = false;
                pagerDown.Visible = false;
            }
            if (long.Parse(pageNo) == howmanyPages) NextBtn.Visible = false;
        }


      
    }


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

            dtResult.Rows.Add(title, Link, Author, Year, "4", Description, ID);

        }
        //}
        //catch { }


        return dtResult;
    }

    public string Count { get; set; }



    //protected void Datapager_prender(object sender, EventArgs e)
    //{

    //    //string searchString = Request.QueryString["s"];
    //    //if (searchString != null)
    //    //{




    //    int totalrowcount = Convert.ToInt16(Count); // DataPager1.TotalRowCount;
    //    int pagesize = DataPager1.PageSize;
    //    int startrowindex = DataPager1.StartRowIndex;


    //    labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();

    //    int toCountNumber = DataPager1.StartRowIndex + DataPager1.PageSize > DataPager1.TotalRowCount ? DataPager1.TotalRowCount : DataPager1.StartRowIndex + DataPager1.PageSize;
    //    idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString();
    //    // //   idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + " of " + totalrowcount.ToString();
    //    //}

    //}

    //protected void Index_Changed(Object sender, EventArgs e)
    //{
    //    DataPager1.PageSize = Convert.ToInt16(show_results.SelectedValue);


    //}

    protected void goBtnClick(object sender, EventArgs e)
    {
        if (pager_textbox.Text != "")
        {
            field = Request.QueryString["field"].ToString();
            search = Request.QueryString["s"];
            pageNo = Request.QueryString["pageNo"].ToString();
            pagesize = Request.QueryString["pagesize"].ToString();
            string url = "/searchsummondetails.aspx?pageNo=" + pager_textbox.Text + "&field=" + field + "&s=" + search + "&pagesize=" + pagesize;
            Response.Redirect(url);
        }
        //  DataPager1.SetPageProperties((int.Parse(pager_textbox.Text) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
    }



    protected void pager_dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        field = Request.QueryString["field"].ToString();
        search = Request.QueryString["s"];
        // pageNo = Request.QueryString["pageNo"].ToString();
        //  pagesize =Request.QueryString["pagesize"].ToString();
        string url = "/searchsummondetails.aspx?pageNo=" + pager_dropdown.SelectedValue + "&field=" + field + "&s=" + search;
        Response.Redirect(url);
    }

    public string searchString { get; set; }
    public string searchMessage { get; set; }

    public string searchCount { get; set; }
    public string SortedbyDate { get; set; }
    public string SortedbyRank { get; set; }


    public long contentId { get; set; }



    public string pageNo { get; set; }

    public string search { get; set; }

    public string field { get; set; }

    public string pagesize { get; set; }
    protected void NextBtn_Click(object sender, EventArgs e)
    {
        field = Request.QueryString["field"].ToString();
        search = Request.QueryString["s"];
        pageNo = Request.QueryString["pageNo"].ToString();
      //  pagesize = Request.QueryString["pagesize"].ToString();
        string url = "/searchsummondetails.aspx?pageNo=" + (long.Parse(pageNo) + 1).ToString() + "&field=" + field + "&s=" + search ;
        Response.Redirect(url);
    }
    protected void PrevBtn_Click(object sender, EventArgs e)
    {
        field = Request.QueryString["field"].ToString();
        search = Request.QueryString["s"];
        pageNo = Request.QueryString["pageNo"].ToString();
     //   pagesize = Request.QueryString["pagesize"].ToString();

        string url = "/searchsummondetails.aspx?pageNo=" + (long.Parse(pageNo) - 1).ToString() + "&field=" + field + "&s=" + search ;
        Response.Redirect(url);
    }
}