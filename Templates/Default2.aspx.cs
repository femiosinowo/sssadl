using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
public partial class Templates_Default2 : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                long contentId = EktUtility.ParseLong(Request.QueryString["id"]);
                //    this.GetContentData(contentId);
            }
        }







        if (!IsPostBack)
        {
            // getAllNews();
            DL_newslist.DataSource = getAllNews();
            DL_newslist.DataBind();
        }

    }







    protected void DL_newslist_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;



            HyperLink NewsTitle = (HyperLink)ditem.FindControl("NewsTitle");
            Literal NewsDate = (Literal)ditem.FindControl("NewsDate");



            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.LoadXml(item["content_html"].ToString());

            string HeadLine = commonfunctions.getFieldValue(XMLDoc, "Headline", "/News");
            string Date = commonfunctions.getFieldValue(XMLDoc, "Date", "/News");
            string Teaser = commonfunctions.getFieldValue(XMLDoc, "Teaser", "/News");

            DateTime DateShown = Convert.ToDateTime(Date);
            long newsId = long.Parse(item["content_id"].ToString());
            NewsDate.Text = DateShown.ToString("MMMM dd, yyyy");

            NewsTitle.Text = HeadLine;
            NewsTitle.NavigateUrl = commonfunctions.getQuickLink(newsId); ;


        }
    }

    protected void Datapager_prender(object sender, EventArgs e)
    {
        DL_newslist.DataSource = getAllNews();
        //  DL_newslist.DataBind();

        DL_newslist.DataBind();
        base.OnPreRender(e);
    }



    private DataTable getAllNews()
    {
        string sql = "SELECT * FROM [ViewAlllNews] order by Date DESC";
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
}