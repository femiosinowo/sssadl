using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;
using System.Collections;

public partial class admin_users_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
      //  if (!IsPostBack)
        //    getUsers();

        try {

            string action = Request.QueryString["action"].ToString();
            switch (action)
            {
                case "successadduser":
                    actionMessages = "New user was successfully added";
                    actionMessage.Visible = true;
                    break;

                case "userupdated":
                    actionMessages = "User profile was successfully updated";
                    actionMessage.Visible = true;
                    break;

            }
        }
        catch { }

    }


    private void getUsers()
    {
        string searchString = sqlSearchString; // searchStringHF.Value;
        // string sortOrderby = sortOrderbyHF.Value;

        string sivalue = ShowInactive.Value;
        string sqlactive = " Active='Y' ";

        if (sivalue == "True")
        {
            sqlactive = " Active='N' ";
        }


        if (searchString.Trim() == "")
        {
            searchString = " where " + sqlactive;

        }
        else
        {
            searchString = "  where " + sqlactive + " and " + searchString;
        }


        usersLV.DataSource = DataBase.dbDataTable("Select * from users " + searchString + sqlOrderBy);
        usersLV.DataBind();
    }
    protected void userst_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;

            Literal CORExpireDateLit = (Literal)ditem.FindControl("CORExpireDate");
            Literal LastAccessedLit = (Literal)ditem.FindControl("LastAccessed");
            Literal ActiveLit = (Literal)ditem.FindControl("Active");

            HyperLink LastName = (HyperLink)ditem.FindControl("LastName");
            LastName.Text = item["LastName"].ToString().Trim();
            LastName.NavigateUrl = "/admin/users/view.aspx?userid=" + item["ID"].ToString();

            Literal rowClick = (Literal)ditem.FindControl("rowClick");
            rowClick.Text = " ondblclick='javascript:goto(" + item["ID"].ToString() + ")' ";

            string CORExpireDate = " -- ";
            string cor = item["COR"].ToString().Trim();
            if (cor == "Y")
            {
                CORExpireDate = getDatabaseDateformat(item["CORExpireDate"].ToString().Trim());
            }
            string LastAccessed = " -- ";
            try
            {
                LastAccessed = Convert.ToDateTime(item["LastAccessed"]).ToString("M/d/yy @ h:mm tt"); // getDatabaseDateformat(item["LastAccessed"].ToString().Trim());
            }
            catch { }

            string Active = item["Active"].ToString().Trim();

            if (Active == "Y") Active = "-";
            if (Active == "N") Active = "Yes";

            LastAccessedLit.Text = LastAccessed;
            CORExpireDateLit.Text = CORExpireDate;
            ActiveLit.Text = Active;

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
        getUsers();
        int totalrowcount = DataPager1.TotalRowCount;
        int pagesize = DataPager1.PageSize;
        int startrowindex = DataPager1.StartRowIndex;
        //if (totalrowcount > 0)
        //{
        //    pagerbelow.Visible = true;
        //    pagertop.Visible = true;

        //}
        //else
        //{
        //    pagerbelow.Visible = false;
        //    pagertop.Visible = false;
        //}

     
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

    
    protected void updateList_Click(object sender, EventArgs e)
    {
        string aaa = searchWordTxt.Text;

        sqlSearchString = "   EmailAddress like '%" + aaa + "%' or  FirstName like '%" + aaa + "%' or  LastName like '%" + aaa + "%' ";
        getUsers();
    }
    protected void AddUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/users/add.aspx");
    }
    protected void ShowInactiveEmployees_Click(object sender, EventArgs e)
    {
        string sivalue = ShowInactive.Value;

        if (sivalue == "False")
        {

            ShowInactive.Value = "True";
            ShowInactiveEmployees.Text = "Hide Inactive Employees";
        }
        else
        {
            ShowInactive.Value = "False";
            ShowInactiveEmployees.Text = "Show Inactive Employees";
        }
         
       // getUsers();
    }
    protected void ExcelClick_Click(object sender, EventArgs e)
    {
        CsvExport myExport = new CsvExport();
        ArrayList columns = new ArrayList();
        DataTableReader dtr = DataBase.dbDataTable("Select * from users " + sqlSearchString + sqlOrderBy).CreateDataReader();

        for (int i = 0; i < dtr.FieldCount; i++)
        {
            columns.Add(dtr.GetName(i));
        }



        if (dtr.HasRows)
        {
            while (dtr.Read())
            {

                //   Then you can do any of the following three output options:
                myExport.AddRow();
                foreach (var ii in columns)
                {
                    string columnName = ii.ToString();
                    myExport[columnName] = dtr[columnName].ToString();
                }





            }
        }


        myCsv = myExport.Export();

        string attachment = "attachment; filename=AdHocHelpRequestsReports-data-" + DateTime.Now.ToShortDateString() + ".csv";
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



    protected void UsersListView_OnSorting(object sender, ListViewSortEventArgs e)
    {
        LinkButton FirstNameBtn = usersLV.FindControl("FirstNameBtn") as LinkButton;
        LinkButton LastNameBtn = usersLV.FindControl("LastNameBtn") as LinkButton;
        LinkButton PINBtn = usersLV.FindControl("PINBtn") as LinkButton;
        LinkButton AccessLevelBtn = usersLV.FindControl("AccessLevelBtn") as LinkButton;
        LinkButton CORExpireDateBtn = usersLV.FindControl("CORExpireDateBtn") as LinkButton;
        LinkButton ActiveBtn = usersLV.FindControl("ActiveBtn") as LinkButton;
        LinkButton LastAccessedBtn = usersLV.FindControl("LastAccessedBtn") as LinkButton;

        ImageButton FirstNameImg = usersLV.FindControl("FirstNameImg") as ImageButton;
        ImageButton LastNameImg = usersLV.FindControl("LastNameImg") as ImageButton;
        ImageButton PINImg = usersLV.FindControl("PINImg") as ImageButton;
        ImageButton AccessLevelImg = usersLV.FindControl("AccessLevelImg") as ImageButton;
        ImageButton CORExpireDateImg = usersLV.FindControl("CORExpireDateImg") as ImageButton;
        ImageButton ActiveImg = usersLV.FindControl("ActiveImg") as ImageButton;
        ImageButton LastAccessedImg = usersLV.FindControl("LastAccessedImg") as ImageButton;




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


        if (LastNameImg != null)
            LastNameImg.ImageUrl = DefaultSortIMG;
        if (FirstNameImg != null)
            FirstNameImg.ImageUrl = DefaultSortIMG;
        if (PINImg != null)
            PINImg.ImageUrl = DefaultSortIMG;
        if (AccessLevelImg != null)
            AccessLevelImg.ImageUrl = DefaultSortIMG;
        if (CORExpireDateImg != null)
            CORExpireDateImg.ImageUrl = DefaultSortIMG;
        if (ActiveImg != null)
            ActiveImg.ImageUrl = DefaultSortIMG;
        if (LastAccessedImg != null)
            LastAccessedImg.ImageUrl = DefaultSortIMG;

        switch (e.SortExpression)
        {
            case "LastName":

                if (LastNameImg != null)
                    LastNameImg.ImageUrl = imgUrl;
                break;
            case "FirstName":

                if (FirstNameImg != null)
                    FirstNameImg.ImageUrl = imgUrl;
                break;

            case "PIN":
                if (PINImg != null)
                    PINImg.ImageUrl = imgUrl;

                break;

            case "AccessLevel":
                if (AccessLevelImg != null)
                    AccessLevelImg.ImageUrl = imgUrl;

                break;

            case "COR":
                if (CORExpireDateImg != null)
                    CORExpireDateImg.ImageUrl = imgUrl;

                break;

            case "Active":
                if (ActiveImg != null)
                    ActiveImg.ImageUrl = imgUrl;

                break;

            case "LastAccessed":
                if (LastAccessedImg != null)
                    LastAccessedImg.ImageUrl = imgUrl;

                break;
        }


        //sortOrderbyHF.Value = " order by " + e.SortExpression + " " + SortDirection;
        sqlOrderBy = " order by " + e.SortExpression + " " + SortDirection;
        getUsers();
    }

    public virtual string sqlOrderBy
    {
        get
        {
            if (ViewState["sqlOrderBy"] == null)
            {
                ViewState["sqlOrderBy"] = "  order by lastname asc";
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
                ViewState["sqlSearchString"] = " ";
            }
            return (string)ViewState["sqlSearchString"];
        }
        set
        {
            ViewState["sqlSearchString"] = value;
        }
    }


    public virtual string currentViewSQL
    {
        get
        {
            if (ViewState["currentViewSQL"] == null)
            {
                ViewState["currentViewSQL"] = "";
            }
            return (string)ViewState["currentViewSQL"];
        }
        set
        {
            ViewState["currentViewSQL"] = value;
        }
    }

    public string actionMessages { get; set; }
}