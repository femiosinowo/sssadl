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
        if (IsPostBack)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MOVEHERE';", true);
        }
       
    }
    protected DataTable allUsers = AdminFunc.getAllAdminUsers();
 

    private void  getResources()
    {

        if (IsPostBack)
        {
           // string searchString = sqlSearchString; // searchStringHF.Value;

           //// Response.Write(sqlSearchString);


           // passwordAssistanceLV.DataSource = DataBase.dbDataTable("Select * from AccurintUsers   " + sqlSearchString + " " + sqlOrderBy, "Admin.DbConnection");
           // passwordAssistanceLV.DataBind();

           // currentSQL.Value = "Select * from AccurintUsers   " + searchString + sqlOrderBy;




            string sqlS = "SELECT distinct(userpin) ,UserLastName , UserFirstName, UserEmail, UserOfficeCode from PasswordAssignments    " + sqlSearchString + " " + sqlOrderBy;

            passwordAssistanceLV.DataSource = DataBase.dbDataTable(sqlS); // DataBase.dbDataTable("select * from  Resources  " + sqlSearchString + " " + sqlOrderBy);
            passwordAssistanceLV.DataBind();

            currentSQL.Value = sqlS;
            //Response.Write(sqlS);
        }
        
    }
    protected void passwordAssistanceLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;
            Literal rowClick = (Literal)ditem.FindControl("rowClick");
            string apin = "\"" + item["UserPIN"].ToString().Trim() + "\"";
            rowClick.Text = " ondblclick='javascript:goto(" + apin + ")' ";
            HyperLink UserLastName = (HyperLink)ditem.FindControl("UserLastName");
            UserLastName.Text = item["UserLastName"].ToString();
            UserLastName.NavigateUrl = "/admin/requests/usersProfileAccess.aspx?PIN=" + item["UserPIN"].ToString().Trim() + "";

           

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
     // 


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

    protected void pager_dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataPager1.SetPageProperties((int.Parse(pager_dropdown.SelectedValue) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
        // pager_dropdown.Items.FindByValue(pager_dropdown.SelectedValue).Selected = true;
    }
    protected void Index_Changed(Object sender, EventArgs e)
    {
        DataPager1.PageSize = Convert.ToInt16(show_results.SelectedValue);


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

        DataTableReader reader = DataBase.dbDataTable(currentViewSQL, "Admin.DbConnection").CreateDataReader();
        while (reader.Read())
        {
            myExport.AddRow();
            
            myExport["LastName"] = reader["UserLastName"].ToString().Trim();
            myExport["FirstName"] = reader["UserFirstName"].ToString().Trim();
            myExport["Email"] = reader["UserEmail"].ToString().Trim();
            myExport["PIN"] = reader["UserPIN"].ToString().Trim();
            myExport["OfficeCode"] = reader["UserOfficeCode"].ToString().Trim();

        }

        reader.Close();
        myCsv = myExport.Export();

        string attachment = "attachment; filename=users-data-" + DateTime.Now.ToShortDateString() + ".csv";
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
        LinkButton LastNameBtn = passwordAssistanceLV.FindControl("LastNameBtn") as LinkButton;
        ImageButton LastNameImg = passwordAssistanceLV.FindControl("LastNameImg") as ImageButton;

        LinkButton FirstNameBtn = passwordAssistanceLV.FindControl("FirstNameBtn") as LinkButton;
        ImageButton FirstNameImg = passwordAssistanceLV.FindControl("FirstNameImg") as ImageButton;

        LinkButton EmailAddressBtn = passwordAssistanceLV.FindControl("EmailAddressBtn") as LinkButton;
        ImageButton EmailAddressImg = passwordAssistanceLV.FindControl("EmailAddressImg") as ImageButton;

        LinkButton PINBtn = passwordAssistanceLV.FindControl("PINBtn") as LinkButton;
        ImageButton PINImg = passwordAssistanceLV.FindControl("PINImg") as ImageButton;

        LinkButton OfficeCodeBtn = passwordAssistanceLV.FindControl("OfficeCodeBtn") as LinkButton;
        ImageButton OfficeCodeImg = passwordAssistanceLV.FindControl("OfficeCodeImg") as ImageButton;

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
            case "UserLastName":
                if (LastNameImg != null)
                    LastNameImg.ImageUrl = imgUrl;
                if (FirstNameImg != null)
                    FirstNameImg.ImageUrl = DefaultSortIMG;
                if (EmailAddressImg != null)
                    EmailAddressImg.ImageUrl = DefaultSortIMG;
                if (PINImg != null)
                    PINImg.ImageUrl = DefaultSortIMG;
                if (OfficeCodeImg != null)
                    OfficeCodeImg.ImageUrl = DefaultSortIMG;
                break;

            case "UserFirstName":
                if (LastNameImg != null)
                    LastNameImg.ImageUrl = DefaultSortIMG;
                if (FirstNameImg != null)
                    FirstNameImg.ImageUrl = imgUrl;
                if (EmailAddressImg != null)
                    EmailAddressImg.ImageUrl = DefaultSortIMG;
                if (PINImg != null)
                    PINImg.ImageUrl = DefaultSortIMG;
                if (OfficeCodeImg != null)
                    OfficeCodeImg.ImageUrl = DefaultSortIMG;
                break;


            case "UserEmail":
                if (LastNameImg != null)
                    LastNameImg.ImageUrl = DefaultSortIMG;
                if (FirstNameImg != null)
                    FirstNameImg.ImageUrl = DefaultSortIMG;
                if (EmailAddressImg != null)
                    EmailAddressImg.ImageUrl = imgUrl;
                if (PINImg != null)
                    PINImg.ImageUrl = DefaultSortIMG;
                if (OfficeCodeImg != null)
                    OfficeCodeImg.ImageUrl = DefaultSortIMG;
                break;


            case "UserPIN":
                if (LastNameImg != null)
                    LastNameImg.ImageUrl = DefaultSortIMG;
                if (FirstNameImg != null)
                    FirstNameImg.ImageUrl = DefaultSortIMG;
                if (EmailAddressImg != null)
                    EmailAddressImg.ImageUrl = DefaultSortIMG;
                if (PINImg != null)
                    PINImg.ImageUrl = imgUrl;
                if (OfficeCodeImg != null)
                    OfficeCodeImg.ImageUrl = DefaultSortIMG;
                break;



            case "UserOfficeCode":
                if (LastNameImg != null)
                    LastNameImg.ImageUrl = DefaultSortIMG;

                if (FirstNameImg != null)
                    FirstNameImg.ImageUrl = DefaultSortIMG;
                if (EmailAddressImg != null)
                    EmailAddressImg.ImageUrl = DefaultSortIMG;
                if (PINImg != null)
                    PINImg.ImageUrl = DefaultSortIMG;
                if (OfficeCodeImg != null)
                    OfficeCodeImg.ImageUrl = imgUrl;

                break;
             
        }


        //sortOrderbyHF.Value = " order by " + e.SortExpression + " " + SortDirection;
        sqlOrderBy = " order by " + e.SortExpression + " " + SortDirection;
        getResources();
    }

    public virtual string sqlOrderBy
    {
        get
        {
            if (ViewState["sqlOrderBy"] == null)
            {
                ViewState["sqlOrderBy"] = "  order by UserLastName asc";
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
                ViewState["sqlSearchString"] = " where 1=1 ";
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
                ViewState["currentViewSQL"] = currentSQL.Value;
            }
            return (string)ViewState["currentViewSQL"];
        }
        set
        {
            ViewState["currentViewSQL"] = value;
        }
    }


    protected void SearchBtn_Click(object sender, EventArgs e)
    {
        PanelResult.Visible = true;
        sqlSearchString = " where 1=1 ";
        string firstNametxt = FirstName.Text;
        string lastNametxt = LastName.Text;
        string emailtxt = Email.Text;
        string officecodetxt = OfficeCode.Text;
        string pintxt = PIN.Text;

        if (firstNametxt != "") sqlSearchString += " and UserFirstName like '%" + firstNametxt + "%' ";
        if (lastNametxt != "") sqlSearchString += " and UserLastName  like '%" + lastNametxt + "%' ";
        if (emailtxt != "") sqlSearchString += " and UserEmail  like '%" + emailtxt + "%' ";
        if (officecodetxt != "") sqlSearchString += " and UserOfficeCode  like '%" + officecodetxt + "%' ";
        if (pintxt != "") sqlSearchString += " and UserPIN like '%" + pintxt + "%' ";

       
    }
}