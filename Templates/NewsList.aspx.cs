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
        mainContent.Text = "<!-- -->";
        uxPageTitle.ResourceTypeId = "2";
        

        if (!IsPostBack)
        {
            // getAllNews();
            DL_newslist.DataSource = getAllNews();
            DL_newslist.DataBind();
                     
            yearArchieve.Items.Add(new ListItem("- Select a year -",""));

            using (DataTableReader reader = getAllNewsUniqueYears().CreateDataReader())
            {
                do
                {
                    if (!reader.HasRows)
                    {
                        // Console.WriteLine("Empty DataTableReader");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                ListItem li = new ListItem();
                                li.Text = reader[i].ToString();
                                li.Value = reader[i].ToString();

                                yearArchieve.Items.Add(li);
                            }
                          
                        }
                    }
                    
                } while (reader.NextResult());
            }
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
        string year = yearArchieve.SelectedItem.Value;
        if (year != "")
        {
            archiveH3.Text = "<h3>News Archives for " + year + "</h3>";
            DL_newslist.DataSource = getSearchNewsArcieve(year);
        }
        else
        {
            DL_newslist.DataSource = getAllNews();
        }

        DL_newslist.DataBind();
        base.OnPreRender(e);
        int totalrowcount = DataPager1.TotalRowCount;
        int pagesize = DataPager1.PageSize;
        int startrowindex = DataPager1.StartRowIndex;
        //if (totalrowcount > 0)
        //{
        //    pagerbelow.Visible = true;
        //    pagertop.Visible = true;

        //}



      //  labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();
        int toCountNumber = DataPager1.StartRowIndex + DataPager1.PageSize > DataPager1.TotalRowCount ? DataPager1.TotalRowCount : DataPager1.StartRowIndex + DataPager1.PageSize;
       // idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString();
        // 


        //if ((startrowindex + 1) >= totalrowcount)
        //{
        //    show_results.Visible = false;
        //    DataPager1.Visible = false;
        //    pagerbelow.Visible = false;
        //}


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
           // pagerbelow.Visible = false;
        }
    }

    private DataTable getAllNews()
    {
        string sql = "SELECT * FROM [ViewAlllNews_SSADL] order by Date DESC";
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

    private DataTable getAllNewsUniqueYears()
    {
        string sql = "SELECT distinct YEAR(Date) As Date FROM [ViewAlllNews_SSADL] order by Date DESC";
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

 


    public string newsOutput { get; set; }


     


    protected void searchYearArchieves_Click(object sender, EventArgs e)
    {
 
    }

    private DataTable getSearchNewsArcieve(string year)
    {
        string sql = "SELECT * FROM [ViewAlllNews_SSADL] where  YEAR(Date)='" + year + "' order by Date DESC";
       // Response.Write(sql);
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