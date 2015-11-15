using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.Summon;
using SSADL.CMS;
using System.Xml;
using System.Data;

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

            }


            if (!string.IsNullOrEmpty(Request.QueryString["field"]))
            {
                string field = Request.QueryString["field"].ToString();
                string search = Request.QueryString["s"];
                string pageNo = Request.QueryString["pageNo"].ToString();

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

               

            }
        }
        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = Title; // mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        mainContent.Text = "<!-- -->";
        uxPageTitle.ResourceTypeId = "2";
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();

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
    private void getSummary(XmlDocument xmlDoc, string fieldName, string searchstring)
    {
        XmlNodeList xmlfacetFields = xmlDoc.SelectNodes("/response");
        foreach (XmlNode node in xmlfacetFields)
        {             
              Count = node.Attributes["recordCount"].Value;
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
                }

            }

            dtResult.Rows.Add(title, Link, Author, Year, "4", Description, ID);

        }
        //}
        //catch { }


        return dtResult;
    }

    public string Count { get; set; }
}