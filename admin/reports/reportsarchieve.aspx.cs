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

           // AllReports = DataBase.dbDataTable("Select * from Report");

            AllReports = DataBase.dbDataTable("Select * from Report");
            ReportTypeListBox.DataSource = AllReports;
            ReportTypeListBox.DataBind();

            foreach (ListItem item in ReportTypeListBox.Items)
            {
                item.Selected = true;
            }

            RunByListBox.DataSource = DataBase.dbDataTable("Select distinct RTRIM(LTRIM(RunbyPIN)) as RunbyPIN,  RTRIM(LTRIM(displayname)) as displayname  from ReportInstances  order by displayname ");
            RunByListBox.DataBind();
            try
            {
                RunByListBox.Items.FindByValue(loginSSA.myPIN).Selected = true;
            }
            catch { }

        }
    }
    private void getReports()
    {
        string pins = string.Empty;
       

        if (!IsPostBack)
        {
           
            buildSearchSQL = " and RunbyPIN in (" + loginSSA.myPIN +") ";
        }
        else
        {
          
            string selectedRunByPins = string.Empty;
            selectedRunByPins = returnSeletedValue(RunByListBox);
             
            if (selectedRunByPins != "")
            {
                buildSearchSQL = " and RunbyPIN in (" + selectedRunByPins + ") ";
            }

            string selectedReportTypes = returnSeletedValue(ReportTypeListBox);
    

            if (selectedReportTypes != "")
            {
                if (selectedRunByPins != "")
                {
                    buildSearchSQL += " and ReportID in (" + selectedReportTypes + ") ";
                }
                 
            }

            string searchWord = searchWordTxt.Text;
            if (searchWord != "")
            {
                buildSearchSQL += " and ReportType like '%" + searchWord + "%' ";

            }



        }

        string sql = "SELECT  ReportID, DisplayName, RTRIM(LTRIM(RunbyPIN)) as RunbyPIN, DateTimRun,ReportType, Name ";
        sql += " FROM   ReportInstances INNER JOIN  Report ON  Report.ID = ReportInstances.ReportID where 1=1  " + buildSearchSQL + " " + sqlOrderBy;
       // Response.Write(sql);

        reportsLV.DataSource = DataBase.dbDataTable(sql); // DataBase.dbDataTable(sql); AllReports.Select("RunbyPIN in (" + pins + ")").CopyToDataTable();
        reportsLV.DataBind();

        //var myReports = from p in AllReports.AsEnumerable()
        //                  where p.Field<string>("RunbyPIN") == loginSSA.myPIN
        //                  select new
        //                  {
        //                      DisplayName = p.Field<string>("DisplayName").Trim(),
        //                      RunbyPIN = p.Field<string>("RunbyPIN").Trim(),
        //                      DateTimRun = p.Field<DateTime>("DateTimRun")
        //                  };

    }

    private string returnSeletedValue(ListBox RunByListBox)
    {
        string result = string.Empty;
        foreach (ListItem li in RunByListBox.Items)
        {
            if (li.Selected == true)
            {
                result += li.Value + ",";
            }
        }

        return result.TrimEnd(',');
    }

    DataTable AllReports = new DataTable();
     

    protected void reportsLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;

          

            Literal DateTimRun = (Literal)ditem.FindControl("DateTimRun");
 
            Literal rowClick = (Literal)ditem.FindControl("rowClick");

            string vaaa = item["ReportID"].ToString().Trim() + ",\"" + item["ReportType"].ToString().Trim() + "\" ";
            rowClick.Text = " ondblclick='javascript:goto(" + vaaa + ")' ";

            string runtimedate = Convert.ToDateTime(item["DateTimRun"].ToString()).ToString("M/d/yy @ h:mm tt");  //Convert.ToDateTime(item["DateTimRun"].ToString()).ToString("d");

            DateTimRun.Text = runtimedate;


            //  DisplayName, RunbyPIN, DateTimRun,ReportType

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
        
            getReports();
         
        int totalrowcount = DataPager1.TotalRowCount;
        int pagesize = DataPager1.PageSize;
        int startrowindex = DataPager1.StartRowIndex;
        if (totalrowcount > 0)
        {
            pagerbelow.Visible = true;
            pagertop.Visible = true;

        }



        labelTotalPages.Text = Math.Ceiling((double)totalrowcount / pagesize).ToString();
        int toCountNumber = DataPager1.StartRowIndex + DataPager1.PageSize > DataPager1.TotalRowCount ? DataPager1.TotalRowCount : DataPager1.StartRowIndex + DataPager1.PageSize;
        idResultsLabel.Text = (startrowindex + 1).ToString() + "-" + toCountNumber.ToString() + " of " + totalrowcount.ToString();
        // 


        if (toCountNumber >= totalrowcount)
        {
            //show_results.Visible = false;
            //DataPager1.Visible = false;
            //pagerbelow.Visible = false;
            //pagertop.Visible = false;
        }



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
        getReports();
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



    protected void reportsLV_OnSorting(object sender, ListViewSortEventArgs e)
    {
        LinkButton RunDateTimeBtn = reportsLV.FindControl("RunDateTimeBtn") as LinkButton;
        ImageButton RunDateTimeImg = reportsLV.FindControl("RunDateTimeImg") as ImageButton;

        LinkButton ReportTypeBtn = reportsLV.FindControl("ReportTypeBtn") as LinkButton;
        ImageButton ReportTypeImg = reportsLV.FindControl("ReportTypeImg") as ImageButton;

        LinkButton DisplayNameBtn = reportsLV.FindControl("DisplayNameBtn") as LinkButton;
        ImageButton DisplayNameImg = reportsLV.FindControl("DisplayNameImg") as ImageButton;

        string DefaultSortIMG = "~/admin/img/desc.png";
        string imgUrl = "~/admin/img/asc.png";


        string SortDirection = "ASC";

        if (ViewState["SortExpression"] != null)
        {
            if (ViewState["SortExpression"].ToString() == e.SortExpression)
            {
                ViewState["SortExpression"] = null;
                SortDirection = "DESC";
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
            case "DateTimRun":
                if (RunDateTimeBtn != null)
                    RunDateTimeImg.ImageUrl = imgUrl;

                break;

            case "ReportType":
                if (ReportTypeBtn != null)
                    ReportTypeImg.ImageUrl = imgUrl;

                break;

            case "DisplayName":
                if (DisplayNameBtn != null)
                    DisplayNameImg.ImageUrl = imgUrl;

                break;

        }



        //sortOrderbyHF.Value = " order by " + e.SortExpression + " " + SortDirection;
        sqlOrderBy = " order by " + e.SortExpression + " " + SortDirection;
        //  getResources();
    }

    public virtual string sqlOrderBy
    {
        get
        {
            if (ViewState["sqlOrderBy"] == null)
            {
                ViewState["sqlOrderBy"] = "  order by DateTimRun DESC";
            }
            return (string)ViewState["sqlOrderBy"];
        }
        set
        {
            ViewState["sqlOrderBy"] = value;
        }
    }



    public virtual string buildSearchSQL
    {
        get
        {
            if (ViewState["buildSearchSQL"] == null)
            {
                ViewState["buildSearchSQL"] = " and RunbyPIN in (" + loginSSA.myPIN + ") ";
            }
            return (string)ViewState["buildSearchSQL"];
        }
        set
        {
            ViewState["buildSearchSQL"] = value;
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
        getReports();
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