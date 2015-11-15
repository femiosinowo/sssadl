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
        if (!IsPostBack)
        {
       
         
        }
        ResourceName = AdminFunc.getResourceName(Request.QueryString["PIN"].ToString());
        
    }



    private void getResources()
    {


        string searchString = sqlSearchString; // searchStringHF.Value;


        string sqlS = "SELECT a.* ,   (Select [ResourceName]  from  [dbo].[Resources]  b where LTRIM(RTRIM(b.ID)) =a.ResourceID ) as ResourceName ";
        
        sqlS += "   from [PasswordAssignments] a  " + sqlSearchString + " " + sqlOrderBy;


        currentViewSQL.Value = sqlS;
        passwordAssistanceLV.DataSource = DataBase.dbDataTable(sqlS); // DataBase.dbDataTable("select * from  Resources  " + sqlSearchString + " " + sqlOrderBy);
        passwordAssistanceLV.DataBind();
       // Response.Write(sqlS);

    }
    protected void passwordAssistanceLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;

            Literal rowClick = (Literal)ditem.FindControl("rowClick");
            rowClick.Text = " ondblclick='javascript:goto(" + item["RequestID"].ToString() + ")' ";
            Dictionary<string, string> uDetails = loginSSA.GetUsersDetails(item["userpin"].ToString());
            FullName = uDetails["DisplayName"].ToString();


            HyperLink ResourceName = (HyperLink)ditem.FindControl("ResourceName");
            ResourceName.Text = item["ResourceName"].ToString();
            ResourceName.NavigateUrl = "/admin/requests/approvedRequest.aspx?reqid=" + item["RequestID"].ToString().Trim() + "";


        }
    }

 

 

    public string getDatabaseDateformat(string datetimeString)
    {

        try
        {
            DateTime dt = new DateTime();
            dt = DateTime.Parse(datetimeString);
            return dt.ToShortDateString();
        }
        catch
        {

            return "";
        }
    }

    protected void Datapager_prender(object sender, EventArgs e)
    { 
            getResources();
            int totalrowcount = DataPager1.TotalRowCount;
            int pagesize = DataPager1.PageSize;
            int startrowindex = DataPager1.StartRowIndex;
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


            labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();
            int toCountNumber = DataPager1.StartRowIndex + DataPager1.PageSize > DataPager1.TotalRowCount ? DataPager1.TotalRowCount : DataPager1.StartRowIndex + DataPager1.PageSize;
            idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString();

            long currentPage = (startrowindex / DataPager1.PageSize) + 1;

            pager_dropdown.Items.Clear();
            double howmanyPages = Math.Ceiling((double)totalrowcount / pagesize);
            labelTotalPages.Text = howmanyPages.ToString();

            for (double i = 1; i <= howmanyPages; i++)
            {
                pager_dropdown.Items.Add(i.ToString());
            }
            try
            {
                pager_dropdown.Items.FindByValue(currentPage.ToString()).Selected = true;
            }
            catch { }

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
    }


    protected void Index_Changed(Object sender, EventArgs e)
    {
        DataPager1.PageSize = Convert.ToInt16(show_results.SelectedValue);


    }
    protected void pager_dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataPager1.SetPageProperties((int.Parse(pager_dropdown.SelectedValue) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
        // pager_dropdown.Items.FindByValue(pager_dropdown.SelectedValue).Selected = true;
    }
    protected void goBtnClick(object sender, EventArgs e)
    {
        if (pager_textbox.Text != "")
            DataPager1.SetPageProperties((int.Parse(pager_textbox.Text) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
    }


    public object searchWord { get; set; }

    
  
 
    protected void ExcelClick_Click(object sender, EventArgs e)
    {

        CsvExport myExport = new CsvExport();
        //   Then you can do any of the following three output options:

        DataTableReader reader = DataBase.dbDataTable(currentViewSQL.Value).CreateDataReader();
        while (reader.Read())
        {
            myExport.AddRow();

            myExport["ResourceName"] = reader["ResourceName"].ToString().Trim();
            myExport["PasswordActive"] = reader["PasswordActive"].ToString().Trim();
          
        }

        reader.Close();
        myCsv = myExport.Export();

        string attachment = "attachment; filename=resource-data-" + DateTime.Now.ToShortDateString() + ".csv";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.AddHeader("Pragma", "public");
        HttpContext.Current.Response.Write(myCsv);
        HttpContext.Current.Response.End();


    }

    public string myCsv { get; set; }
 


    protected void passwordAssistanceLV_OnSorting(object sender, ListViewSortEventArgs e)
    {
        LinkButton ResourceNameBtn = passwordAssistanceLV.FindControl("ResourceNameBtn") as LinkButton;
        ImageButton ResourceNameImg = passwordAssistanceLV.FindControl("ResourceNameImg") as ImageButton;


        LinkButton AccessStatusBtn = passwordAssistanceLV.FindControl("AccessStatusBtn") as LinkButton;
        ImageButton AccessStatusImg = passwordAssistanceLV.FindControl("AccessStatusImg") as ImageButton;

 

        string DefaultSortIMG = "~/admin/img/asc.png";
        string imgUrl = "~/admin/img/desc.png";


        string SortDirection = "DESC";

        if (ViewState["SortExpression"] != null)
        {
            if (ViewState["SortExpression"].ToString() == e.SortExpression)
            {
                ViewState["SortExpression"] = null;
                SortDirection = "ASC";
                imgUrl = DefaultSortIMG;
            }
            else
            {
                ViewState["SortExpression"] = e.SortExpression;
            }
        }
        else
        {
            ViewState["SortExpression"] = e.SortExpression;
        }



        switch (e.SortExpression)
        {
            case "ResourceName":
                if (ResourceNameImg != null)
                    ResourceNameImg.ImageUrl = DefaultSortIMG;
                
                break;

            case "PasswordActive":
                if (AccessStatusImg != null)
                    AccessStatusImg.ImageUrl = DefaultSortIMG;

                break;

 
           
             
        }


        //sortOrderbyHF.Value = " order by " + e.SortExpression + " " + SortDirection;
        sqlOrderBy = " order by " + e.SortExpression + " " + SortDirection;
        //getResources();
    }

    public virtual string sqlOrderBy
    {
        get
        {
            if (ViewState["sqlOrderBy"] == null)
            {
                ViewState["sqlOrderBy"] = "  order by ResourceName asc";
            }
            return (string)ViewState["sqlOrderBy"];
        }
        set
        {
            ViewState["sqlOrderBy"] = value;
        }
    }


    public virtual string sqlSearchString
    {
        get
        {
            if (ViewState["sqlSearchString"] == null)
            {
                ViewState["sqlSearchString"] = " where UserPIN = '" + Request.QueryString["PIN"].ToString() + "' ";
            }
            return (string)ViewState["sqlSearchString"];
        }
        set
        {
            ViewState["sqlSearchString"] = value;
        }
    }






    public string ResourceName { get; set; }

    public string FullName { get; set; }
    protected void AddBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/requests/UsersAddEpassword.aspx?PIN=" + Request.QueryString["PIN"].ToString());
    }
}