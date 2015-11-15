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

            //Subject Areas Taxonomy
            AdminUsers.DataSource = allUsers; // commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(136), "TaxName", "ASC");
            AdminUsers.DataBind();



            try
            {
                AdminUsers.Items.FindByValue(myAdminID).Selected = true;
            }
            catch { }


            try
            {

                showWhat = Request.QueryString["show"].ToString();
            }
            catch { }

            if (showWhat == "onlyme")
            {
                pageTitle = "My e-Passwords Requests";
                try
                {
                    AdminUsers.Items.FindByValue(myAdminID).Selected = true;
                }
                catch { }
            }
            else
            {
                pageTitle = "All Incoming e-Password Requests";
                foreach (ListItem item in AdminUsers.Items)
                {
                    item.Selected = true;
                }

            }
            getResources();

        }

        if (showWhat == "onlyme")
        {
            pageTitle = "My e-Passwords Requests";

        }
        else
        {
            pageTitle = "All Incoming e-Password Requests";


        }
    }
    protected DataTable allUsers = AdminFunc.getAllAdminUsers();

    protected void ListView1_DataBound(object sender, EventArgs e)
    {
        //if (IsPostBack)
        //{
        //    DataPager1.Visible = (DataPager1.PageSize < DataPager1.TotalRowCount);
        //}
    }

    private void getResources()
    {
        string searchString = sqlSearchString; // searchStringHF.Value;
        // string sortOrderby = sortOrderbyHF.Value;
        //Status (options: New, Open,Approved - not notified, Declined - not notified, Approved - closed, Declined -closed)
        try
        {

            showWhat = Request.QueryString["show"].ToString();
        }
        catch { }

        string sivalue = ShowInactive.Value;
        string onlymeFilter = string.Empty;


        string SelectedUsersID = string.Empty;
        int i = 0;
        foreach (ListItem li in AdminUsers.Items)
        {
            if (li.Selected == true)
            {
                if (i == 0)
                {
                    SelectedUsersID = "  and ((dbo.Resources.SendEpasswordTo LIKE N'%," + li.Value + ",%') ";
                }
                else
                {
                    SelectedUsersID += " or (dbo.Resources.SendEpasswordTo LIKE N'%," + li.Value + ",%') ";
                }


                i++;
            }
        }
        if (i > 0)
        {
            SelectedUsersID += " )";
        }
        //if (showWhat == "onlyme")
        //{
        //    onlymeFilter = " where (dbo.Resources.SendEpasswordTo LIKE N'%," + myAdminID + ",%') ";
        //}
        //else
        //{
        //    onlymeFilter = " where 1=1 ";
        //}
        string searchWrd = string.Empty;
        string searchWordTxtTxt = string.Empty;
        if (searchWordTxt.Text != "")
        {
            searchWordTxtTxt = searchWordTxt.Text;
            //  searchWrd = "   and (SubmittedByPIN like '%" + searchWordTxtTxt + "%'  or SubmittedByLastName like '%" + searchWordTxtTxt + "%'   ";
            // searchWrd += " or SubmittedByFirstName like '%" + searchWordTxtTxt + "%' ";
            //  searchWrd += " or SubmittedByEMail like '%" + searchWordTxtTxt + "%'  ";
            // searchWrd += " or SubmittedByOffice like '%" + searchWordTxtTxt + "%'  ";
            // searchWrd += " or SubmittedByServer like '%" + searchWordTxtTxt + "%' ";
            // searchWrd += " or SubmittedByUserDomain like '%" + searchWordTxtTxt + "%' ";
            // searchWrd += " or SubmittedForPIN like '%" + searchWordTxtTxt + "%' ";
            searchWrd += " and ( SubmittedForLastName like '%" + searchWordTxtTxt + "%' ";
            searchWrd += " or SubmittedForFirstName like '%" + searchWordTxtTxt + "%' ";
            // searchWrd += " or SubmittedForEMail like '%" + searchWordTxtTxt + "%' ";
            // searchWrd += " or SubmittedForOffice like '%" + searchWordTxtTxt + "%' ";
            // searchWrd += " or SubmittedForServer like '%" + searchWordTxtTxt + "%' ";
            // searchWrd += " or SubmittedForUserDomain like '%" + searchWordTxtTxt + "%' ";
            searchWrd += " or FormStatus like '%" + searchWordTxtTxt + "%' ";
            //  searchWrd += " or WhyDoYouNeedAccess like '%" + searchWordTxtTxt + "%' ";
            // searchWrd += " or InternalNotes like '%" + searchWordTxtTxt + "%' "; 
            searchWrd += " or  CONVERT(VARCHAR(10),SubmissionDateandTime, 101) = '" + searchWordTxtTxt + "' ";
            searchWrd += "  ) ";
        }

        string sqlactive = " and FormStatus in ('New','Open','Approved-Not-Notified','Declined-Not-Notified') ";

        if (sivalue == "True")
        {
            sqlactive = "  and FormStatus in ('Approved-Closed' , 'Declined-Closed') ";  //Two statuses are considered closed: "Approved - closed" and "Declined - closed";
        }
        if (sqlSearchString.Trim() != "")
        {
            searchString = " and " + sqlSearchString;
        }


        myReqeustsSQL = "SELECT  dbo.AccessToResourceForm.*   ";
        myReqeustsSQL += " FROM dbo.AccessToResourceForm INNER JOIN ";
        myReqeustsSQL += " dbo.Resources ON LTRIM(RTRIM(dbo.AccessToResourceForm.ResourceToAccess)) = dbo.Resources.ID  where 1=1  " + SelectedUsersID + sqlactive + "  " + searchString + " " + searchWrd;


        //Response.Write(myReqeustsSQL + sqlOrderBy);

        dt = DataBase.dbDataTable(myReqeustsSQL + sqlOrderBy, "Admin.DbConnection");
        currentSQL.Value = myReqeustsSQL + sqlOrderBy;
        passwordAssistanceLV.DataSource = dt;
        passwordAssistanceLV.DataBind();
        // Response.Write(myReqeustsSQL  + sqlOrderBy);

        // Response.Write("Select * from PasswordAssistanceForm " + searchString + sqlOrderBy);
    }

    protected void UpdateListBtnForUsers_Click(object sender, EventArgs e)
    {
        string SelectedUsersID = string.Empty;
        //string ePassword = string.Empty;
        //int i = 0;


        //foreach (ListItem li in AdminUsers.Items)
        //{
        //    if (li.Selected == true)
        //    {
        //        if (i == 0)
        //        {
        //            sqlSearchString = "   ((dbo.Resources.SendEpasswordTo LIKE N'%," + li.Value + ",%') ";
        //        }
        //        else
        //        {
        //            sqlSearchString += " or (dbo.Resources.SendEpasswordTo LIKE N'%," + li.Value + ",%') ";
        //        }


        //        i++;
        //    }
        //}
        //if (i > 0)
        //{
        //    sqlSearchString += " )";
        //}
        //getResources();
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
            HyperLink SubmissionDateandTimeTxt = (HyperLink)ditem.FindControl("SubmissionDateandTime");

            Literal rowClick = (Literal)ditem.FindControl("rowClick");
            string fas = "\"" + showWhat + "\"";
            rowClick.Text = " ondblclick='javascript:gotoEditRequest(" + item["ID"].ToString() + " , " + fas + ")' ";

            string resourceID = item["ResourceToAccess"].ToString();
            //  ViewEdit.NavigateUrl = "/admin/requests/editrequest.aspx?reqid=" + item["ID"].ToString();
            string requestorPIN = item["SubmittedForPIN"].ToString().Trim();




            string Active = item["FormStatus"].ToString().Trim();
            string SubmissionDateandTime = item["SubmissionDateandTime"].ToString().Trim();
            SubmissionDateandTimeTxt.Text = getDatabaseDateformat(SubmissionDateandTime);
            SubmissionDateandTimeTxt.NavigateUrl = "/admin/requests/editrequest.aspx?reqid=" + item["ID"].ToString() + "&show=" + showWhat;
            try
            {
                Dictionary<string, string> userdetails = loginSSA.GetUsersDetails(requestorPIN);
                SubmittedByPINTxt.Text = userdetails["DisplayName"]; // +" " + userdetails["FirstName"]; //getUserNameByPIN(item["SubmittedForPIN"].ToString().Trim());
            }
            catch { }
            AssignedTo.Text = getResourceRequestsAdminUsers(resourceID);
            ActiveLit.Text = Active;



        }
    }

    private string getResourceRequestsAdminUsers(string resourceID)
    {
        string allRecipientsName = string.Empty;
        string requestRecipients = DataBase.returnOneValue("Select SendEpasswordTo from Resources where ID = '" + resourceID + "'", "Admin.DbConnection").Trim();
        try
        {
            string allRecipients = requestRecipients.TrimEnd(',').TrimStart(',');
            DataTableReader dtr = DataBase.dbDataTable("select concat(RTRIM(LTRIM([FirstName])) , ' ' ,RTRIM(LTRIM([LastName])) ) as Names from [dbo].[users] where id in (" + allRecipients + ") ", "Admin.DbConnection").CreateDataReader();
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
            // return dt.ToShortDateString();
            return dt.ToString("MM/dd/yyyy");
        }
        catch
        {

            return "";
        }
    }

    protected void Datapager_prender(object sender, EventArgs e)
    {


        if (IsPostBack)
        {
            getResources();
        }
        //  DataPager1.Visible = (DataPager1.PageSize < DataPager1.TotalRowCount);
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
        //Response.Write(toCountNumber.ToString());


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

        // 
    }


    protected void Index_Changed(Object sender, EventArgs e)
    {
        // getResources();
        DataPager1.PageSize = Convert.ToInt16(show_results.SelectedValue);

        //  getResources();

    }

    protected void pager_dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataPager1.SetPageProperties((int.Parse(pager_dropdown.SelectedValue) - 1) * DataPager1.PageSize, DataPager1.PageSize, true);
        // pager_dropdown.Items.FindByValue(pager_dropdown.SelectedValue).Selected = true;
        //  getResources();
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

        //getResources();
    }
    protected void ExcelClick_Click(object sender, EventArgs e)
    {

        CsvExport myExport = new CsvExport();
        //   Then you can do any of the following three output options:      


        string RequestorDisplayName = string.Empty;

        DataTableReader reader = DataBase.dbDataTable(currentViewSQL, "Admin.DbConnection").CreateDataReader();
        while (reader.Read())
        {
            string resourceID = reader["ResourceToAccess"].ToString();
            //  ViewEdit.NavigateUrl = "/admin/requests/editrequest.aspx?reqid=" + item["ID"].ToString();
            string requestorPIN = reader["SubmittedForPIN"].ToString().Trim();

            Dictionary<string, string> userdetails = loginSSA.GetUsersDetails(requestorPIN);

            try
            {
                RequestorDisplayName = userdetails["DisplayName"];
            }
            catch { }


            myExport.AddRow();
            // string resourceID = reader["ResourceToAccess"].ToString().Trim();
            myExport["Date"] = reader["SubmissionDateandTime"].ToString().Trim();
            myExport["Requestor"] = RequestorDisplayName; // getResourceRequestsAdminUsers(resourceID);
            myExport["Assignted To"] = getResourceRequestsAdminUsers(resourceID);
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

        LinkButton StatusBtn = passwordAssistanceLV.FindControl("StatusBtn") as LinkButton;
        ImageButton StatusImg = passwordAssistanceLV.FindControl("StatusImg") as ImageButton;

        LinkButton SubmittedForFirstNameBtn = passwordAssistanceLV.FindControl("SubmittedForFirstNameBtn") as LinkButton;
        ImageButton SubmittedForFirstNameImg = passwordAssistanceLV.FindControl("SubmittedForFirstNameImg") as ImageButton;


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
                if (StatusImg != null)
                    StatusImg.ImageUrl = DefaultSortIMG;
                break;

            case "FormStatus":
                if (StatusImg != null)
                    StatusImg.ImageUrl = imgUrl;
                if (DateImg != null)
                    DateImg.ImageUrl = DefaultSortIMG;
                break;

            case "SubmittedForFirstName":
                if (SubmittedForFirstNameImg != null)
                    SubmittedForFirstNameImg.ImageUrl = imgUrl;
                if (StatusImg != null)
                    StatusImg.ImageUrl = DefaultSortIMG;
                if (DateImg != null)
                    DateImg.ImageUrl = DefaultSortIMG;
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

    public string showWhat { get; set; }

    public string pageTitle { get; set; }
}