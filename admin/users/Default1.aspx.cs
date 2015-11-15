using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;

public partial class admin_users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
            getUsers(searchString);
        
    }


    private void getUsers(string searchString)
    {
        DataTable dtResult = new DataTable(); 
        dtResult = DataBase.dbDataTable("Select * from users " + searchString);
        usersListGV.DataSource = dtResult;
        usersListGV.DataBind();



        int totalrowcount = usersListGV.Rows.Count;
        int pagesize = usersListGV.PageSize;
        Response.Write(totalrowcount.ToString());
        int startrowindex = 0; // DataPager1.StartRowIndex;
        if (totalrowcount > 0)
        {
            pagerbelow.Visible = true;
            pagertop.Visible = true;

        }
        else
        {
            pagerbelow.Visible = false;
            pagertop.Visible = false;
        }

        //        // Response.Write(totalrowcount.ToString());
                 labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();
        //        idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + " of " + totalrowcount.ToString();
    }
    protected void SearchResult_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;
 
            Literal CORExpireDateLit = (Literal)ditem.FindControl("CORExpireDate");
            Literal LastAccessedLit = (Literal)ditem.FindControl("LastAccessed");
            Literal ActiveLit = (Literal)ditem.FindControl("Active");

            string CORExpireDate = commonfunctions.getDatabaseDateformat(item["CORExpireDate"].ToString().Trim());
            string LastAccessed = commonfunctions.getDatabaseDateformat(item["LastAccessed"].ToString().Trim());
            string Active = item["Active"].ToString().Trim();

            if (Active == "Y") Active = "Yes";
            if (Active == "N") Active = "No";

            LastAccessedLit.Text = LastAccessed;
            CORExpireDateLit.Text = CORExpireDate;
            ActiveLit.Text = Active;
             
        }
    }


    protected void UsersGridView_RowDataBound(Object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            GridViewRow ditem = (GridViewRow)e.Row;
            DataRowView item = e.Row.DataItem as DataRowView;


            Literal CORExpireDateLit = (Literal)ditem.FindControl("CORExpireDate");
            Literal LastAccessedLit = (Literal)ditem.FindControl("LastAccessed");
            Literal ActiveLit = (Literal)ditem.FindControl("Active");
            string CORExpireDate = commonfunctions.getDatabaseDateformat(item["CORExpireDate"].ToString().Trim());
            string LastAccessed = commonfunctions.getDatabaseDateformat(item["LastAccessed"].ToString().Trim());
            string Active = item["Active"].ToString().Trim();

            if (Active == "Y") Active = "Yes";
            if (Active == "N") Active = "No";

            LastAccessedLit.Text = LastAccessed;
            CORExpireDateLit.Text = CORExpireDate;
            ActiveLit.Text = Active;
        }

 

    }




    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        usersListGV.PageIndex = e.NewPageIndex;
        getUsers(searchString);
        //searchEvents();
      //  getUsers(searchString);
         
    }


    //protected void Datapager_prender(object sender, EventArgs e)
    //{

 

            


    //        int totalrowcount = DataPager1.TotalRowCount;
    //        int pagesize = DataPager1.PageSize;
    //        int startrowindex = DataPager1.StartRowIndex;
    //        if (totalrowcount > 0)
    //        {
    //            pagerbelow.Visible = true;
    //            pagertop.Visible = true;

    //        }
    //        else
    //        {
    //            pagerbelow.Visible = false;
    //            pagertop.Visible = false;
    //        }

    //        // Response.Write(totalrowcount.ToString());
    //        labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();
    //        idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + " of " + totalrowcount.ToString();
        
    //}


    protected void Index_Changed(Object sender, EventArgs e)
    {
       // DataPager1.PageSize = Convert.ToInt16(show_results.SelectedValue);
        usersListGV.PageSize = Convert.ToInt16(show_results.SelectedValue);
        getUsers(searchString);
    }

    protected void goBtnClick(object sender, EventArgs e)
    {
        //if (pager_textbox.Text != "")
        //    DataPager1.SetPageProperties((int.Parse(pager_textbox.Text) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
    }


    public object searchWord { get; set; }

    public string searchString { get; set; }
    protected void updateList_Click(object sender, EventArgs e)
    {
        string aaa = searchWordTxt.Text;
        searchString = " where  EmailAddress like '%" + aaa + "%' or  FirstName like '%" + aaa + "%' or  LastName like '%" + aaa + "%' ";
    }
    protected void AddUser_Click(object sender, EventArgs e)
    {

    }
    protected void ShowInactiveEmployees_Click(object sender, EventArgs e)
    {
        searchString = " where Active='N' ";
    }
    protected void ExcelClick_Click(object sender, EventArgs e)
    {
        //string attachment = "attachment; filename=users-data-" + DateTime.Now.ToShortDateString() + ".csv";
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.ClearHeaders();
        //HttpContext.Current.Response.ClearContent();
        //HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        //HttpContext.Current.Response.ContentType = "text/csv";
        //HttpContext.Current.Response.AddHeader("Pragma", "public");
        //HttpContext.Current.Response.Write(myCsv);
        //HttpContext.Current.Response.End();


        //CsvExport myExport = new CsvExport();
        //myExport.AddRow();
        //myExport["Registration ID"] = regID;
        //myExport["First Name"] = first_name;
        //myExport["Last Name"] = last_name;
        //myExport["Email"] = email;
        //myExport["Phone"] = phone;
        //myExport["Invoice Number"] = InvoiceNumber;
        //myExport["Amount Paid"] = amount;
        //myExport["Paid For"] = PaidFor;
        //myExport["Registration Date"] = RegDate;


        ////   Then you can do any of the following three output options:
        //myCsv = myExport.Export();
       


    }

    public char myCsv { get; set; }
}