using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSADL.CMS;

public partial class admin_users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
                    h3Output = "Password Assistance Forms";
                
    }
    protected void SearchResult_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;


            //// Literal imageLink = (Literal)ditem.FindControl("imageLink");
            HyperLink viewLink = (HyperLink)ditem.FindControl("viewLink");
            HyperLink editLink = (HyperLink)ditem.FindControl("editLink");


            viewLink.Text = "View";
            viewLink.NavigateUrl = "/admin/contactforms/passwordassistance/view.aspx?id=" + item["ID"].ToString().Trim() + "&keepThis=true&TB_iframe=true&height=650&width=600";
            viewLink.CssClass = "thickbox";

            editLink.Text = "Edit";
            editLink.NavigateUrl = "/admin/contactforms/passwordassistance/edit.aspx?id=" + item["ID"].ToString().Trim() + "&keepThis=true&TB_iframe=true&height=650&width=600";
            editLink.CssClass = "thickbox";

            //&keepThis=true&TB_iframe=true&height=650&width=600'  class='thickbox'
        //   Literal SubmittedByPIN = (Literal)ditem.FindControl("SubmittedByPIN");
          //  Literal searchLink = (Literal)ditem.FindControl("searchLink");

            //Literal myFav = (Literal)ditem.FindControl("myFav");

            //string contentId = item["ContentID"].ToString().Trim();
            //string contentTitle = item["Title"].ToString().Trim();

            //myFav.Text = commonfunctions.getMyFavIcons(contentId, "2", contentTitle);


            //// host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
            //searchTitleLink.Text = item["Title"].ToString().Trim(); // cf.getFieldValue(XMLDoc, "NewsArticleTitle");
            //searchTitleLink.NavigateUrl = item["QuickLink"].ToString(); // newsLink;
            //// searchLink.Text = commonfunctions.host + item["QuickLink"].ToString();

            //string teaser = string.Empty;
            //teaser = item["Blurb"].ToString().Trim();
            //long teaserlength = teaser.Length;
            //if (teaserlength > 150)
            //{
            //    teaser = teaser.Substring(0, 150);
            //}
            //else if (teaserlength != 0)
            //{
            //    teaser = teaser.Substring(0, Convert.ToInt16(teaserlength));
            //}

            //SearchTeaser.Text = teaser.Trim();


        }
    }

    protected void Datapager_prender(object sender, EventArgs e)
    {

          searchString = "Select * from PasswordAssistanceForm;";
        
        if (searchString != null)
        {


            SearchResultLV.DataSource =  DataBase.dbDataTable(searchString); ;
            SearchResultLV.DataBind();

            int totalrowcount = DataPager1.TotalRowCount;
            int pagesize = DataPager1.PageSize;
            int startrowindex = DataPager1.StartRowIndex;


         //   labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();
           // idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + " of " + totalrowcount.ToString();
        }

    }
     

 

    public string h3Output { get; set; }

    protected void SearchDates_Click(object sender, EventArgs e)
    {
        string dateFrom = DateFrom.Text;
        string dateto = DateTo.Text;

        searchString = "Select * from PasswordAssistanceForm where submissionDate between '" + dateFrom + "' and  '" + dateto + "'";

    }

    public string searchString { get; set; }
}