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
            //getResources();


            //Subject Areas Taxonomy
            
                SubjectAreasTaxonomy.DataSource = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(136), "TaxName", "ASC");
                SubjectAreasTaxonomy.DataBind();
           
        }

        string aaa = searchWordTxt.Text;
        if (aaa != "")
        {
            sqlSearchString = "   ResourceName like '%" + aaa + "%' or  Description like '%" + aaa + "%'  ";
        }
    }


    private void getResources()
    {
        string searchString = sqlSearchString; // searchStringHF.Value;
        // string sortOrderby = sortOrderbyHF.Value;

        string sivalue = ShowInactive.Value;

        string sqlactive = " ResourceDisplayStatus = 'Enabled' ";

        if (sivalue == "True")
        {
            sqlactive = " ResourceDisplayStatus in ('Disabled' , 'Inactive') ";
        }

        if (searchString.Trim() == "")
        {
            searchString = " where " + sqlactive;

        }
        else
        {
            searchString = "  where " + sqlactive + " and " + searchString;
        }
        string allSQL = "Select * from View_AllResources " + searchString + sqlOrderBy;
        currentSQL.Value = allSQL;
        resourcesLV.DataSource = DataBase.dbDataTable(allSQL);
        resourcesLV.DataBind();

        //  Response.Write("Select * from View_AllResources " + searchString + sqlOrderBy);
    }
    protected void ResourceLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;

            Literal SubjectAreasTaxonomyLit = (Literal)ditem.FindControl("SubjectAreasTaxonomyLit");
            Literal ePass = (Literal)ditem.FindControl("ePass");

            Literal ActiveLit = (Literal)ditem.FindControl("Active");

            HyperLink ResourceName = (HyperLink)ditem.FindControl("ResourceName");
            ResourceName.Text = item["ResourceName"].ToString().Trim();
            ResourceName.NavigateUrl = "/admin/resources/view.aspx?resourceID=" + item["ID"].ToString();

            Literal rowClick = (Literal)ditem.FindControl("rowClick");
            rowClick.Text = " ondblclick='javascript:goto(" + item["ID"].ToString() + ")' ";

            string Active = item["ResourceDisplayStatus"].ToString().Trim();


            string SubjectAreasTaxonomyDB = item["SubjectAreasTaxonomy"].ToString().Trim();
            // string LimitedNumberPasswordsAvailableDB = item["LimitedNumberPasswordsAvailable"].ToString().Trim();
            string ePassDB = item["ePass"].ToString().Trim();
            string SubjectAreasTaxonomyDisplay = string.Empty;
            if (SubjectAreasTaxonomyDB != "")
            {
                string[] taxIds = SubjectAreasTaxonomyDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string taxId in taxIds)
                {
                    SubjectAreasTaxonomyDisplay += commonfunctions.GetTaxonomyNameFromID(long.Parse(taxId)) + " , ";
                }
            }
            ePass.Text = ePassDB;


            SubjectAreasTaxonomyLit.Text = SubjectAreasTaxonomyDisplay.Trim().TrimEnd(',');

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
        getResources();
        int totalrowcount = DataPager1.TotalRowCount;
        int pagesize = DataPager1.PageSize;
        int startrowindex = DataPager1.StartRowIndex;
        //if (totalrowcount > 0)
        //{
        //    pagerbelow.Visible = true;
        //    pagertop.Visible = true;

        //}


        if (totalrowcount <= 0)
        {
            //    pagerbelow.Visible = true;
            pagertop.Visible = false;

        }
        else
        {
          
            pagertop.Visible = true;
        }
        if (totalrowcount < 10)
        {
            show_results.Enabled = false;
        }
        else
        {
            show_results.Enabled = true;
        }

        labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();
        int toCountNumber = DataPager1.StartRowIndex + DataPager1.PageSize > DataPager1.TotalRowCount ? DataPager1.TotalRowCount : DataPager1.StartRowIndex + DataPager1.PageSize;
        idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString();
        // 


        //if ((startrowindex + 1) >= totalrowcount)
        //{
        //    show_results.Visible = false;
        //    DataPager1.Visible = false;
        //    pagerbelow.Visible = false;
        //}


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
        else
        {
            pagerbelow.Visible = true;
        }
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
    protected void pager_dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataPager1.SetPageProperties((int.Parse(pager_dropdown.SelectedValue) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
        // pager_dropdown.Items.FindByValue(pager_dropdown.SelectedValue).Selected = true;
    }

    protected void updateList_Click(object sender, EventArgs e)
    {
        string aaa = searchWordTxt.Text;

        sqlSearchString = "   ResourceName like '%" + aaa + "%' or  Description like '%" + aaa + "%'  ";
        getResources();
    }

    protected void ShowInactiveResources_Click(object sender, EventArgs e)
    {
        string sivalue = ShowInactive.Value;

        if (sivalue == "False")
        {

            ShowInactive.Value = "True";
            ShowInactiveResources.Text = "Hide Inactive Resources";
        }
        else
        {
            ShowInactive.Value = "False";
            ShowInactiveResources.Text = "Show Inactive Resources";
        }

        getResources();
    }
    protected void ExcelClick_Click(object sender, EventArgs e)
    {
        CsvExport myExport = new CsvExport();
        //   Then you can do any of the following three output options:

        DataTableReader reader = DataBase.dbDataTable(currentViewSQL, "Admin.DbConnection").CreateDataReader();
        while (reader.Read())
        {
            myExport.AddRow();
            string ePass = "";
            string SubjectAreasTaxonomyDB = reader["SubjectAreasTaxonomy"].ToString().Trim();
            string AccessTypeTaxonomyDB = reader["AccessTypeTaxonomy"].ToString().Trim();
            string ePassDB = reader["ePass"].ToString().Trim();
            string SubjectAreasTaxonomyDisplay = string.Empty;
            if (SubjectAreasTaxonomyDB != "")
            {
                string[] taxIds = SubjectAreasTaxonomyDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string taxId in taxIds)
                {
                    SubjectAreasTaxonomyDisplay += commonfunctions.GetTaxonomyNameFromID(long.Parse(taxId)) + " , ";
                }
            }

            ePass = ePassDB;




            myExport["Resource Name"] = reader["ResourceName"].ToString().Trim();
            myExport["Subject Areas"] = SubjectAreasTaxonomyDisplay.Trim().TrimEnd(',');
            myExport["e-Pass"] = ePass;
            myExport["Status"] = reader["ResourceDisplayStatus"].ToString().Trim();

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



    protected void resourcesLV_OnSorting(object sender, ListViewSortEventArgs e)
    {
        LinkButton ResourceNameBtn = resourcesLV.FindControl("ResourceNameBtn") as LinkButton;
        ImageButton ResourceNameImg = resourcesLV.FindControl("ResourceNameImg") as ImageButton;

        //LinkButton SubjectAreasBtn = resourcesLV.FindControl("SubjectAreasBtn") as LinkButton;
        //ImageButton SubjectAreasImg = resourcesLV.FindControl("SubjectAreasImg") as ImageButton;


        LinkButton ePassBtn = resourcesLV.FindControl("ePassBtn") as LinkButton;
        ImageButton ePassImg = resourcesLV.FindControl("ePassImg") as ImageButton;


        LinkButton StatusBtn = resourcesLV.FindControl("StatusBtn") as LinkButton;
        ImageButton StatusImg = resourcesLV.FindControl("StatusImg") as ImageButton;



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
                    ResourceNameImg.ImageUrl = imgUrl;

                if (ePassImg != null)
                    ePassImg.ImageUrl = DefaultSortIMG;
                if (StatusImg != null)
                    StatusImg.ImageUrl = DefaultSortIMG;

                break;

            //case "SubjectAreas":
            //    if (SubjectAreasImg != null)
            //        SubjectAreasImg.ImageUrl = DefaultSortIMG;

            //    break;


            case "ePass":
                if (ePassImg != null)
                    ePassImg.ImageUrl = imgUrl;

                if (ResourceNameImg != null)
                    ResourceNameImg.ImageUrl = DefaultSortIMG;
                if (StatusImg != null)
                    StatusImg.ImageUrl = DefaultSortIMG;
                break;


            case "Status":
                if (StatusImg != null)
                    StatusImg.ImageUrl = imgUrl;

                if (ResourceNameImg != null)
                    ResourceNameImg.ImageUrl = DefaultSortIMG;

                if (ePassImg != null)
                    ePassImg.ImageUrl = DefaultSortIMG;

                break;

        }


        //sortOrderbyHF.Value = " order by " + e.SortExpression + " " + SortDirection;
        sqlOrderBy = " order by " + e.SortExpression + " " + SortDirection;
        // getResources();
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
                ViewState["sqlSearchString"] = " ";
            }
            return (string)ViewState["sqlSearchString"];
        }
        set
        {
            ViewState["sqlSearchString"] = value;
        }
    }


    protected void AddResource_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/resources/add.aspx");
    }


    protected void UpdateListBtnForSubJEPass_Click(object sender, EventArgs e)
    {
        string subjectAreaIDs = string.Empty;
        //string ePassword = string.Empty;
        int i = 0;

        foreach (ListItem li in SubjectAreasTaxonomy.Items)
        {
            if (li.Selected == true)
            {
                if (i == 0)
                {
                    subjectAreaIDs = "   SubjectAreasTaxonomy like '%," + li.Value + ",%'";
                }
                else
                {
                    subjectAreaIDs += " or SubjectAreasTaxonomy like '%," + li.Value + ",%'";
                }
                i++;
            }
        }

        ////////////////E password
        string epassSelect = ePassDD.SelectedValue;
        string epassS = string.Empty;
        switch (epassSelect)
        {
            case "All":
                epassS = " ePass in ('Yes','-') ";
                break;

            case "ePassword":
                epassS = " ePass = 'Yes' ";
                break;

            case "noneePassword":
                epassS = " ePass = '-' ";
                break;
        }


        if (subjectAreaIDs == "")
        {
            epassS = "    " + epassS;
        }
        else
        {
            epassS = " and  " + epassS;
        }
        //Response.Write(subjectAreaIDs + epassS);
        sqlSearchString = subjectAreaIDs + epassS;
         getResources();
    }


    public string returnSelectedItemsValue(ListBox lsBox)
    {
        string seletectedItemsValue = ","; // string.Empty;

        foreach (ListItem li in lsBox.Items)
        {
            if (li.Selected == true)
            {
                seletectedItemsValue += li.Value + ",";
            }
        }

        return seletectedItemsValue;
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
}