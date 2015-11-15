using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
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
                  contentId = long.Parse(Request.QueryString["id"]);
            //    this.GetContentData(contentId);
              
            }
        }


        getResearchGuidesTypes();
        resourceGuide();
        mainContent.DefaultContentID = contentId;
        mainContent.Fill();
        
        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        mainContent.Text = "<!-- -->";
        uxPageTitle.ResourceTypeId = "2";
    }

    private void getResearchGuidesTypes()
    {
        if (contentId > 0)
        {


            XmlDocument xmlDoc = commonfunctions.getContentXML(contentId);

            bannerImage = commonfunctions.getFieldAttributeValue(xmlDoc, "BannerImage", "img", "src", "/root/ResearchPageSection");
            bannerImageALT = commonfunctions.getFieldAttributeValue(xmlDoc, "BannerImage", "img", "alt", "/root/ResearchPageSection");

            if (bannerImage != "")
            {
                bannerImage = "<img src='" + bannerImage + "' alt='" + bannerImageALT + "'>";
            }


            displayContent = commonfunctions.getFieldValue(xmlDoc, "Content", "/root/ResearchPageSection");






            XmlNodeList xnRGTData = xmlDoc.SelectNodes("/root/ResearchGuideType");

            DataTable dtRGT = new DataTable();
            dtRGT.Columns.Add("Name");
            dtRGT.Columns.Add("Description");
            dtRGT.Columns.Add("RGXML");
            DataTable sorteddtRGT = new DataTable();
            foreach (XmlElement xnRGTItem in xnRGTData)
            {
                string Name = xnRGTItem["Name"].InnerText;
                string Description = xnRGTItem["Description"].InnerXml;
                string Active = xnRGTItem["Active"].InnerText;
                string allRGIds = string.Empty;

                if (Active == "true")
                {

                    try
                    {

                        //if no research guide it will throw an error
                        string ResearchGuide = xnRGTItem["ResearchGuide"].InnerXml;
                       

                        XmlNodeList xnRGTDataGuides = xnRGTItem.SelectNodes("ResearchGuide");
                        if (xnRGTDataGuides.Count > 0)
                        {

                          //  outputResults += "<h4>" + Name + "</h4>";
                          //  outputResults += Description;

                            foreach (XmlElement xnRGTGuideItem in xnRGTDataGuides)
                            {
                            //    outputResults += getResearchGuidesItems(xnRGTGuideItem.ChildNodes[0].InnerText);
                                allRGIds += xnRGTGuideItem.ChildNodes[0].InnerText + ";";
                            }
                        }

                        dtRGT.Rows.Add(Name, Description, allRGIds);
                    }
                    catch { }
                }


            }

            
            //sort the datatable 
          //  if (dtRGT.Rows.Count > 0)
           // {
                DataView dv = dtRGT.DefaultView;
                dv.Sort = "Name ASC";
                sorteddtRGT = dv.ToTable();
                RGTLV.DataSource = sorteddtRGT;
                RGTLV.DataBind();
           // }
 
        }
    }

    protected void resourceGuide()
    {
        ///get research guide from resources
        ///
        DataTable dtRGT = new DataTable();
        dtRGT.Columns.Add("title");
        dtRGT.Columns.Add("description");
        dtRGT.Columns.Add("pdfurl");

        SqlCommand cmd = new SqlCommand("ResourceResearchGuide");
        DataTableReader dtt = DataBase.executeStoreProcudure(cmd).CreateDataReader();
        if (dtt.HasRows)
        {
            headerH4.Text = "<h4>Resource Guides</h4>";
            while (dtt.Read())
            {
                string Title = dtt["Title"].ToString();
                string URL = dtt["URL"].ToString();
                string Descriptiona = dtt["Description"].ToString();

                dtRGT.Rows.Add(Title, Descriptiona, URL);

            }
        }
        ListViewResourceSearchGuide.DataSource = dtRGT;
        ListViewResourceSearchGuide.DataBind();

    }

    protected void ListViewResourceSearchGuide_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;
            Literal description = (Literal)ditem.FindControl("description");
            HyperLink pdfurl = (HyperLink)ditem.FindControl("pdfurl");

            description.Text = item["description"].ToString();
            pdfurl.Text = item["title"].ToString();
            pdfurl.NavigateUrl = item["pdfurl"].ToString();

        }
    }
    protected void RGTLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader

            Literal headerH4 = (Literal)ditem.FindControl("headerH4");
            Repeater rgsrpt = (Repeater)ditem.FindControl("RGsRepeater");
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;

            string Name = item["Name"].ToString();
            string Description = item["Description"].ToString();
            string RGXML = item["RGXML"].ToString();          


            DataTable dtSort = processRGITs(RGXML);
            if (dtSort.Rows.Count > 0)
            {
                headerH4.Text = "<h4>" + Name + "</h4>" + Description;
                rgsrpt.DataSource = dtSort;
                rgsrpt.DataBind();

            }
         

        }
         
    }

 

    protected void RGsItem_ItemDatabound(Object Sender, RepeaterItemEventArgs e)
    {



        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RepeaterItem ditem = (RepeaterItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;

            Literal description = (Literal)ditem.FindControl("description");
            HyperLink pdfurl = (HyperLink)ditem.FindControl("pdfurl");

            description.Text = item["description"].ToString();
            pdfurl.Text = item["title"].ToString();
            pdfurl.NavigateUrl = item["pdfurl"].ToString();
            
        }
    }

    private DataTable processRGITs(string RGXML)
    {
        DataTable dtSort = new DataTable();
        DataTable dt = new DataTable();

        dt.Columns.Add("title");       
        dt.Columns.Add("description");
        dt.Columns.Add("pdfurl");

        string[] rgIds = RGXML.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string rgID in rgIds)
        {            

            XmlDocument xmlDoc = commonfunctions.getContentXML(long.Parse(rgID));
            string Title = commonfunctions.getFieldValue(xmlDoc, "Title", "/ResearchGuide");
            string Description = commonfunctions.getFieldValue(xmlDoc, "Description", "/ResearchGuide");
            string Active = commonfunctions.getFieldValue(xmlDoc, "Active", "/ResearchGuide");
            string PDFUrl = commonfunctions.getFieldAttributeValue(xmlDoc, "PDFUrl", "a", "href", "/ResearchGuide");

            if (Active == "true")
            {
                dt.Rows.Add(Title, Description, PDFUrl);

            }


        }

        //sort the datatable 
        if (dt.Rows.Count > 0)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = "title ASC";
            dtSort = dv.ToTable();
            
        }

        return dtSort;
    }
    //private string getResearchGuidesItems(string guideID)
    //{


    //    string returnoutput = string.Empty;
    //    XmlDocument xmlDoc = commonfunctions.getContentXML(long.Parse(guideID));
    //    string Title = commonfunctions.getFieldValue(xmlDoc, "Title", "/ResearchGuide");
    //    string Description = commonfunctions.getFieldValue(xmlDoc, "Description", "/ResearchGuide");
    //    string Active = commonfunctions.getFieldValue(xmlDoc, "Active", "/ResearchGuide");

    //    string PDFUrl = commonfunctions.getFieldAttributeValue(xmlDoc, "PDFUrl", "a", "href", "/ResearchGuide");

    //    if (Active == "true")
    //    {
    //        returnoutput += "<p><a href='" + PDFUrl + "'>" + Title + "</a><br/>";
    //        returnoutput += Description + "</p>";

    //    }

    //    return returnoutput;

    //}

    public string bannerImage { get; set; }

    public string bannerImageALT { get; set; }

    public string displayContent { get; set; }

    public long contentId { get; set; }

    public string outputResults { get; set; }
}