using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms;
using System.Configuration;
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
                contentId = long.Parse(Request.QueryString["id"]);
                //    this.GetContentData(contentId);

                // XmlDocument xmlD = commonfunctions.getContentXML(contentId);





            }



            //Resource Type Taxonomy  

            ResourceTypeTaxonomy.DataSource = AdminFunc.ResourceTypeTaxonomyDataTable; // commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(123), "TaxName", "ASC");
            ResourceTypeTaxonomy.DataBind();
            //ListItem li = new ListItem();
            //li.Text = "All Resources";
            //li.Value = "";
            //li.Selected = true;
            //ResourceTypeTaxonomy.Items.Add(li);




        }




        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();
        mainContent.Text = "<!-- -->";
        uxPageTitle.ResourceTypeId = "2";


        XmlDocument XMLDoc = new XmlDocument();
        XMLDoc.LoadXml(mainContent.EkItem.Html);

        SActive = commonfunctions.getFieldValue(XMLDoc, "Active", "/SubjectAreas");
        if (SActive == "true")
        {
            SATitle = commonfunctions.getFieldValue(XMLDoc, "Name", "/SubjectAreas");

            string sadescript = commonfunctions.getFieldValue(XMLDoc, "FullDescription", "/SubjectAreas");

            int maxAllowedshown = 400;
            int wholeLength = sadescript.Length;
            if (wholeLength > maxAllowedshown)
            {

                SADescription = sadescript.Substring(0, maxAllowedshown) + "<a href=\"#\" class=\"show_text\">show more >></a>";
                SADescription += "  <span class='slidingDiv hide'>" + sadescript.Substring(maxAllowedshown, wholeLength - maxAllowedshown) + "<a href='#' class='hide_text'> << show less</a></span>";
            }
            else
            {
                SADescription = sadescript;
            }


            string allTaxonomy = commonfunctions.GetAssignedTaxonomyList(contentId);

            //make sure this is a subject area taxonomy - just in case this content is attached to many taxonomies
            string[] txIds = allTaxonomy.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var taxID in txIds)
            {
                Ektron.Cms.Controls.Directory taxControl = new Ektron.Cms.Controls.Directory();
                taxControl.TaxonomyId = long.Parse(taxID);
                taxControl.Page = this.Page;
                taxControl.Fill();
                Ektron.Cms.TaxonomyData taxData = new Ektron.Cms.TaxonomyData();
                taxData = taxControl.TaxonomyTreeData;
                long parentId = taxData.ParentId;

                if (parentId == 136)
                {
                    SubjectAreaTaxID = taxID;
                    SubjectAreaTaxIDHF.Value = taxID;
                    break;
                }

            }

        }
      //  myFavCollection = commonfunctions.getAllmyFavorites();
        // Response.Write(SubjectAreaTaxID);
        if (IsPostBack)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MOVEHERE';", true);
        }
    }


    public ArrayList myFavCollection = new ArrayList();


    protected void DL_Resourceslist_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;



            HyperLink ResourceTitle = (HyperLink)ditem.FindControl("ResourceTitle");
            Literal Description = (Literal)ditem.FindControl("Description");
            Literal LoginInfomation = (Literal)ditem.FindControl("LoginInfomation");

            Literal myfavicons = (Literal)ditem.FindControl("myfavicons");
             

            long rId = long.Parse(item["ID"].ToString().Trim());

            //bool amIfav = amIfavorite(rId.ToString());
            //if (amIfav)
            //{
            //    notFav.Text = " style=\"display:none\"";
            //}
            //else
            //{

            //    myFav.Text = " style=\"display:none\"";
            //}


            string Title = item["ResourceName"].ToString().Trim();
            Description.Text = item["Description"].ToString().Trim();
            string ResourceURLlink = item["ResourceURLlink"].ToString().Trim(); //"/dynamicdb.aspx?resourceid=" + item["ID"].ToString(); //
            ResourceTitle.Text = Title;
            ResourceTitle.NavigateUrl = ResourceURLlink;
            ResourceTitle.Target = "_blank";

            ResourceTitle.CssClass = "outboundResource";
            ResourceTitle.Attributes.Add("resourceid", rId.ToString());
       

             
            if (item["ShowInNewWindow"].ToString().Trim() == "Y")
            {
                ResourceTitle.Target = "_blank";
            }

            myfavicons.Text = commonfunctions.getMyFavIcons(rId.ToString(), "1", Title, "0");
            Literal bottomLinks = (Literal)ditem.FindControl("bottomLinks");
            bottomLinks.Text = commonfunctions.getResourceBottomLinks(item);
        }
    }

   

 

    protected void Datapager_prender(object sender, EventArgs e)
    {


        // Response.Write(sql);



        string resType = ResourceTypeTaxonomy.SelectedValue;
        if (resType != "")
        {
            sql = "select * from [Resources] where ([SubjectAreasTaxonomy] like '%," + SubjectAreaTaxIDHF.Value + ",%' and ResourceTypeTaxonomy='" + resType + "')  and ResourceDisplayStatus in ('Enabled' , 'Disabled')";
        }
        else
        {
            sql = "select * from [Resources] where [SubjectAreasTaxonomy] like '%," + SubjectAreaTaxIDHF.Value + ",%' and ResourceDisplayStatus in ('Enabled' , 'Disabled') ";
        }


         // Response.Write(sql);
        DL_Resourceslist.DataSource = commonfunctions.sortDataTable(DataBase.dbDataTable(sql), "ResourceName", "ASC");
        DL_Resourceslist.DataBind();


        int totalrowcount = DataPager1.TotalRowCount;
        int pagesize = DataPager1.PageSize;
        int startrowindex = DataPager1.StartRowIndex;

        labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();
        int toCountNumber = DataPager1.StartRowIndex + DataPager1.PageSize > DataPager1.TotalRowCount ? DataPager1.TotalRowCount : DataPager1.StartRowIndex + DataPager1.PageSize;
         idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString();
         if (toCountNumber <= 0) idResultsLabel.Visible = false;


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
         }
         

        base.OnPreRender(e);
    }


    protected void goBtnClick(object sender, EventArgs e)
    {
        if (pager_textbox.Text != "")
            DataPager1.SetPageProperties((int.Parse(pager_textbox.Text) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
    }


    public string SATitle { get; set; }

    public string SADescription { get; set; }

    public long contentId { get; set; }

    public string SubjectAreaTaxID { get; set; }

    protected void BtnShowSelectedResourceType_Click(object sender, EventArgs e)
    {
        // ShowSelectedResourceType




    }

    public string sql { get; set; }

    public string SActive { get; set; }
}