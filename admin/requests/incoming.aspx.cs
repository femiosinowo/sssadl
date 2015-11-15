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
            AdminUsers.DataSource = allUsers; // commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(136), "TaxName", "ASC");
            AdminUsers.DataBind();
            // Response.Write(AdminFunc.getUserDBID(loginSSA.myPIN));



            //Response.Write(AdminID);
        }
    }
    protected DataTable allUsers = AdminFunc.getAllAdminUsers();


    private void getResources()
    {
        string searchString = sqlSearchString; // searchStringHF.Value;
        // string sortOrderby = sortOrderbyHF.Value;
        //Status (options: New, Open,Approved - not notified, Declined - not notified, Approved - closed, Declined -closed)
        string sivalue = ShowInactive.Value;

        string sqlactive = " FormStatus in ('New','Open','Approved-Not-Notified','Declined-Not-Notified') ";

        if (sivalue == "True")
        {
            sqlactive = " FormStatus in ('Approved-Closed' , 'Declined-Closed') ";  //Two statuses are considered closed: "Approved - closed" and "Declined - closed";
        }

        if (searchString.Trim() == "")
        {
            searchString = " where " + sqlactive + "   ";

        }
        else
        {
            searchString = "  where " + sqlactive + "  and " + searchString;
        }

        myReqeustsSQL = "SELECT  dbo.AccessToResourceForm.*   ";
        myReqeustsSQL += " FROM dbo.AccessToResourceForm INNER JOIN ";
        myReqeustsSQL += " dbo.Resources ON LTRIM(RTRIM(dbo.AccessToResourceForm.ResourceToAccess)) = dbo.Resources.ID ";


        dt = DataBase.dbDataTable(myReqeustsSQL + searchString + sqlOrderBy, "Admin.DbConnection");
        currentSQL.Value = myReqeustsSQL + searchString + sqlOrderBy;
        passwordAssistanceLV.DataSource = dt;
        passwordAssistanceLV.DataBind();
        // Response.Write(myReqeustsSQL + searchString + sqlOrderBy);

        // Response.Write("Select * from PasswordAssistanceForm " + searchString + sqlOrderBy);
    }

    protected void UpdateListBtnForUsers_Click(object sender, EventArgs e)
    {
        string SelectedUsersID = string.Empty;
        //string ePassword = string.Empty;
        int i = 0;


        foreach (ListItem li in AdminUsers.Items)
        {
            if (li.Selected == true)
            {
                if (i == 0)
                {
                    sqlSearchString = "   ((dbo.Resources.SendEpasswordTo LIKE N'%," + li.Value + ",%') ";
                }
                else
                {
                    sqlSearchString += " or (dbo.Resources.SendEpasswordTo LIKE N'%," + li.Value + ",%') ";
                }


                i++;
            }
        }
        if (i > 0)
        {
            sqlSearchString += " )";
        }
    }

    protected void passwordAssistanceLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;

            Literal SubmittedByPINTxt = (Literal)ditem.FindControl("SubmittedByPIN");
            //Literal SubmissionDateandTimeTxt = (Literal)ditem.FindControl("SubmissionDateandTime");
            Literal AssignedTo = (Literal)ditem.FindControl("AssignedTo");
            Literal ActiveLit = (Literal)ditem.FindControl("Active");
            //HyperLink ViewEdit = (HyperLink)ditem.FindControl("ViewEdit");
            Literal rowClick = (Literal)ditem.FindControl("rowClick");
            rowClick.Text = " ondblclick='javascript:gotoEditRequest(" + item["ID"].ToString() + ")' ";
            string resourceID = item["ResourceToAccess"].ToString();
            HyperLink SubmissionDateandTimeTxt = (HyperLink)ditem.FindControl("SubmissionDateandTime");

            // ViewEdit.NavigateUrl = "/admin/requests/editrequest.aspx?reqid=" + item["ID"].ToString();
            string requestorPIN = item["SubmittedForPIN"].ToString().Trim();
            if (requestorPIN == "" || requestorPIN == "None") requestorPIN = item["SubmittedByPIN"].ToString().Trim();
            try
            {
                Dictionary<string, string> userdetails = loginSSA.GetUsersDetails(requestorPIN);

                string Active = item["FormStatus"].ToString().Trim();
                string SubmissionDateandTime = item["SubmissionDateandTime"].ToString().Trim();
                SubmissionDateandTimeTxt.Text = getDatabaseDateformat(SubmissionDateandTime);
                SubmissionDateandTimeTxt.NavigateUrl = "/admin/requests/editrequest.aspx?reqid=" + item["ID"].ToString();
                try
                {
                    SubmittedByPINTxt.Text = userdetails["LastName"] + " " + userdetails["FirstName"]; //getUserNameByPIN(item["SubmittedForPIN"].ToString().Trim());
                }
                catch { }
                AssignedTo.Text = getResourceRequestsAdminUsers(resourceID);
                ActiveLit.Text = Active;

            }
            catch { }


        }
    }

    private string getResourceRequestsAdminUsers(string resourceID)
    {
        string allRecipientsName = string.Empty;
        string requestRecipients = DataBase.returnOneValue("Select SendEpasswordTo from Resources where ID = '" + resourceID + "'").Trim();
        try
        {
            string allRecipients = requestRecipients.TrimEnd(',').TrimStart(',');
            DataTableReader dtr = DataBase.dbDataTable("select concat(RTRIM(LTRIM([FirstName])) , ' ' ,RTRIM(LTRIM([LastName])) ) as Names from [dbo].[users] where id in (" + allRecipients + ") ").CreateDataReader();
            if (dtr.HasRows)
            {
                while (dtr.Read())
                {
                    allRecipientsName += dtr["Names"].ToString() + ",";

                }

            }
        }
        catch { }
        return allRecipientsName.TrimEnd(',');

    }

    private string getUserNameByPIN(string PIN)
    {
        string name = "";
        DataRow[] result = allUsers.Select("PIN='" + PIN + "'");
        foreach (DataRow row in result)
        {
            name = row["Name"].ToString();
        }
        return name;
    }

    private string getDBIDByPIN(string PIN)
    {
        string ID = "";
        DataRow[] result = allUsers.Select("PIN='" + PIN + "'");
        foreach (DataRow row in result)
        {
            ID = row["ID"].ToString();
        }
        return ID;
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


    //protected void updateList_Click(object sender, EventArgs e)
    //{
    //    string aaa = searchWordTxt.Text;

    // //   sqlSearchString = "   ResourceName like '%" + aaa + "%' or  Description like '%" + aaa + "%'  ";
    // //   getResources();
    //}

    protected void ShowInactiveResources_Click(object sender, EventArgs e)
    {
        string sivalue = ShowInactive.Value;

        if (sivalue == "False")
        {

            ShowInactive.Value = "True";
            ShowInactiveResources.Text = "Hide Closed Requests  ";
        }
        else
        {
            ShowInactive.Value = "False";
            ShowInactiveResources.Text = "Show Closed Requests ";
        }

        // getResources();
    }
    protected void ExcelClick_Click(object sender, EventArgs e)
    {




        CsvExport myExport = new CsvExport();
        //   Then you can do any of the following three output options:

        DataTableReader reader = DataBase.dbDataTable(currentViewSQL).CreateDataReader();
        while (reader.Read())
        {
            myExport.AddRow();
            string resourceID = reader["ResourceToAccess"].ToString().Trim();
            myExport["Date"] = reader["SubmissionDateandTime"].ToString().Trim();
            myExport["Requestor"] = getResourceRequestsAdminUsers(resourceID);
            myExport["Assignted To"] = getUserNameByPIN(reader["SubmittedByPIN"].ToString().Trim());
            myExport["Status"] = reader["FormStatus"].ToString().Trim();

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
        LinkButton DateBtn = passwordAssistanceLV.FindControl("DateBtn") as LinkButton;
        ImageButton DateImg = passwordAssistanceLV.FindControl("DateImg") as ImageButton;


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
            case "SubmissionDateandTime":
                if (DateImg != null)
                    DateImg.ImageUrl = imgUrl;

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
                ViewState["sqlOrderBy"] = "  order by SubmissionDateandTime desc";
            }
            return (string)ViewState["sqlOrderBy"];
        }
        set
        {
            ViewState["sqlOrderBy"] = value;
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

    public virtual string myAdminID
    {
        get
        {
            if (ViewState["myAdminID"] == null)
            {
                ViewState["myAdminID"] = getDBIDByPIN(loginSSA.myPIN);
            }
            return (string)ViewState["myAdminID"];
        }
        set
        {
            ViewState["myAdminID"] = value;
        }
    }




    protected void AddResource_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/resources/add.aspx");
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




    public DataTable dt { get; set; }

    public string myReqeustsSQL { get; set; }
}