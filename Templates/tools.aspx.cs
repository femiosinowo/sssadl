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
        //if (!Page.IsPostBack)
        //{
        //    if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        //    {
        //          contentId = EktUtility.ParseLong(Request.QueryString["id"]);
        //    //    this.GetContentData(contentId);
        //    }
        //}
        contentId = mainContent.EkItem.Id.ToString();



        RightSideContent.ccontentID = contentId;
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = contentId;
        uxBreadcrumb.contentID = contentId;
        uxPageTitle.ResourceTypeId = "2";
        mainContent.Text = "<!-- -->";
      

        string description = mainContent.EkItem.Html;
        //Response.Write(assignCat);

        int maxAllowedshown = 300;
        int wholeLength = description.Length;
        if (wholeLength > maxAllowedshown)
        {

            SADescription = "<p>" + description.Substring(0, maxAllowedshown) + "</p><a href=\"#\" class=\"show_text\">show more >></a> ";
            SADescription += "  <span class='slidingDiv hide'>" + description.Substring(maxAllowedshown, wholeLength - maxAllowedshown) + "<br /><a href='#' class='hide_text'> << show less</a></span>  ";
        }
        else
        {
            SADescription = description;
        }


        string assignCat = getAudienceTax(contentId);

        DL_Toollist.DataSource = getRelatedItems(long.Parse(assignCat));
        DL_Toollist.DataBind();

    }

    private DataTable getRelatedItems(long taxID)
    {
        string sql = "select * from [ViewAllTools_SSADL] where taxonomy_id = '" + taxID + "'  ";
        return commonfunctions.sortDataTable(DataBase.dbDataTable(sql , "Ektron.Dbconnection"), "Name", "ASC");

    }


    protected void DL_Toollist_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;


            long toolID = long.Parse(item["content_id"].ToString().Trim());

            XmlDocument xmlDoc = commonfunctions.getContentXML(toolID);


            ///Tools/Name
            ///Tools/Description
            ///Tools/url/a/text()
            ///Tools/url/a/@href
            ///Tools/Active

            HyperLink toolTitle = (HyperLink)ditem.FindControl("toolTitle");
            Literal Description = (Literal)ditem.FindControl("Description");


            Literal myfavIcon = (Literal)ditem.FindControl("myfavIcon");
         

            string Title = commonfunctions.getFieldValue(xmlDoc, "Name", "/Tools"); // item["toolName"].ToString();
            Description.Text = commonfunctions.getFieldValue(xmlDoc, "Description", "/Tools");

            string toolUrl = commonfunctions.getFieldAttributeValue(xmlDoc, "url" , "a" , "href" , "/Tools");
            string toolUrlTarget = commonfunctions.getFieldAttributeValue(xmlDoc, "url", "a", "target", "/Tools");
           // string toolUrlTarget = commonfunctions.getFieldAttributeValue(xmlDoc, "url", "a", "target", "/Tools");


            myfavIcon.Text = commonfunctions.getMyFavIcons(toolID.ToString(), "6", Title);

            toolTitle.Text = Title;
            toolTitle.NavigateUrl = toolUrl;

            if (toolUrlTarget=="_blank")
            {
                toolTitle.Target = "_blank";
            }

            
        }
    }


    private string getAudienceTax(string cntID)
    {
        ArrayList assignCat = new ArrayList();
        string AudTaxID = string.Empty;
        assignCat = commonfunctions.GetAssignedTaxonomyArray(long.Parse(cntID));

        if (assignCat.Count > 0)
        {
            foreach (string taxID in assignCat)
            {

                Ektron.Cms.Controls.Directory taxControl = new Ektron.Cms.Controls.Directory();
                taxControl.TaxonomyId = long.Parse(taxID);
                taxControl.Page = this.Page;
                taxControl.Fill();
                Ektron.Cms.TaxonomyData taxData = new Ektron.Cms.TaxonomyData();
                taxData = taxControl.TaxonomyTreeData;
                long parentId = taxData.ParentId;

                if (parentId == 96)
                {
                    AudTaxID = taxID;
                    
                    break;
                }
            }
        }

        return AudTaxID;
    }


    public string contentId { get; set; }

    public object SADescription { get; set; }
}