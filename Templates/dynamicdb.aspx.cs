using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            commonfunctions cf = new commonfunctions();

            if (!string.IsNullOrEmpty(Request.QueryString["resourceid"]))
            {
                string resourceid = Request.QueryString["resourceid"].ToString();
                DataTable dtResource = DataBase.dbDataTable("Select * from Resources where ID ='" + resourceid + "'");
                DL_Resourceslist.DataSource = dtResource;
                DL_Resourceslist.DataBind();



                  subjectArea = "";
                string sql = "SELECT  * FROM [ViewAllSubjectArea_SSADL] order by Name ASC";
                DataTable dtSA = DataBase.dbDataTable(sql, "Ektron.DbConnection");

                DataTableReader dtr = dtResource.CreateDataReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string ID = dtr["ID"].ToString().Trim();
                          Title = dtr["ResourceName"].ToString().Trim();
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
                                     
                                    subjectArea += "<li><a href='" + row["URL"].ToString() + "'>" + row["Name"].ToString() + "</a></li>";

                                }

                            }
                        }


                        
                    }
                }



            }
        }
        
        uxPageTitle.pgTitle = Title; // mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        mainContent.Text = "<!-- -->";
        uxPageTitle.ResourceTypeId = "2";
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();

    }
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


            Title = item["ResourceName"].ToString().Trim();
            Description.Text = item["Description"].ToString().Trim();
            string ResourceURLlink = item["ResourceURLlink"].ToString().Trim();
            ResourceTitle.Text = Title;
            ResourceTitle.NavigateUrl = ResourceURLlink;
            if (item["ShowInNewWindow"].ToString().Trim() == "Y")
            {
                ResourceTitle.Target = "_blank";
            }

            ResourceTitle.CssClass = "outboundResource";
            ResourceTitle.Attributes.Add("resourceid", rId.ToString());

            myfavicons.Text = commonfunctions.getMyFavIcons(rId.ToString(), "1", Title, "0");
            Literal bottomLinks = (Literal)ditem.FindControl("bottomLinks");
            bottomLinks.Text = commonfunctions.getResourceBottomLinks(item);
        }
    }




    public string FullDescription { get; set; }
    public string Title { get; set; }
    public string Teaser { get; set; }
     
    public string NewsDate { get; set; }

    public string MainBackgroundImageALT { get; set; }

    public string MainBackgroundImage { get; set; }

    public string Headline { get; set; }

    public string subjectArea { get; set; }
}