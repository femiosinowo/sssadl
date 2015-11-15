using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;

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
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        uxPageTitle.ResourceTypeId = "2";

        long tid = 100; //The Taxonomy ID
        Ektron.Cms.Framework.Organization.TaxonomyManager tManager = new Ektron.Cms.Framework.Organization.TaxonomyManager();
        Ektron.Cms.TaxonomyData tData = tManager.GetTree(tid, 3, true, null, Ektron.Cms.Common.EkEnumeration.TaxonomyType.Content); //gets the taxonomy 3 levels deep
        level1.DataSource = tData.Taxonomy; //databind to the listview/repeater
        level1.DataBind();
        
    }


    protected void FAQLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            Ektron.Cms.TaxonomyData item = (Ektron.Cms.TaxonomyData)ditem.DataItem;
            Literal headerH3 = (Literal)ditem.FindControl("headerH3");
            if (item.TaxonomyItems.Length > 0)
            {
                headerH3.Text = "<h3>" + item.Name + "</h3>";
            }
        }

    }


    protected void faqsItem_ItemDatabound(Object Sender, RepeaterItemEventArgs e)
    {



        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RepeaterItem ditem = (RepeaterItem)e.Item;
            //data reader
            Ektron.Cms.TaxonomyItemData item = (Ektron.Cms.TaxonomyItemData)ditem.DataItem;
            Literal outputText = (Literal)ditem.FindControl("outputText");

            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.LoadXml(item.Html);
            string idsend = item.ItemId.ToString();
            outputText.Text += processFAQs(XMLDoc, idsend);

            
           

        }
    }



    private string processFAQs(XmlDocument XMLDoc, string idsend)
    {

        string output = "";
        string Question = commonfunctions.getFieldValue(XMLDoc, "Question", "/FAQs");
        string Answer = commonfunctions.getFieldValue(XMLDoc, "Answer", "/FAQs");
        string Active = commonfunctions.getFieldValue(XMLDoc, "Active", "/FAQs");
        if (Active == "true")
        {

            output = "<p><a class=\"icon\" href=\"#\">Q: " + Question + "?</a></p>";
            output += " <div>";
            output += "  <p>" + Answer + "</p>";
            output += " </div>";
        }




        return output;
    }





    /// <summary>
    /// This method is used to get content data
    /// </summary>
    /// <param name="contentId"></param>
    //private void GetContentData(long contentId)
    //{
    //    if (contentId > 0)
    //    {
    //        var cData = ContentHelper.GetContentById(contentId);
    //        if (cData != null && cData.Id > 0)
    //        {
    //            //ltrFAQs.Text = cData.Html;
    //        }
    //    }
    //}

}