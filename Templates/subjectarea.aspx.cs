using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
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
        uxPageTitle.ResourceTypeId = "2";
        DL_SubjectArealist.DataSource = getAllActiveSubjectAreas();
        DL_SubjectArealist.DataBind();
        
    }


 


    private DataTable getAllActiveSubjectAreas()
    {
        string sql = "SELECT * FROM [ViewAllSubjectArea_SSADL] order by Name ASC";
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        using (SqlConnection cnn = new SqlConnection(commonfunctions.ektronConnectionStr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;
                cnn.Open();
                cmd.CommandText = sql;
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);


            }
        }
        return dt;

    }



    protected void DL_SubjectArealist_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;
            Literal myfavicons = (Literal)ditem.FindControl("myfavicons");
            HyperLink SubjectAreaTitle = (HyperLink)ditem.FindControl("SubjectAreaTitle");
            Literal Description = (Literal)ditem.FindControl("Description");

            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.LoadXml(item["content_html"].ToString());

            string ShortDescription = commonfunctions.getFieldValue(XMLDoc, "ShortDescription", "/SubjectAreas");
            string Name = commonfunctions.getFieldValue(XMLDoc, "Name", "/SubjectAreas");
         
            long saId = long.Parse(item["content_id"].ToString());
            Description.Text = ShortDescription;
            myfavicons.Text = commonfunctions.getMyFavIcons(saId.ToString(), "2", Title, "0");
            SubjectAreaTitle.Text = Name;
            SubjectAreaTitle.NavigateUrl = commonfunctions.getQuickLink(saId); ;


        }
    }

}