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

            myAdminID = getDBIDByPIN(loginSSA.myPIN);
           
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
            searchString = " where " + sqlactive + "  ";

        }
        else
        {
            searchString = "  where " + sqlactive + " and " + searchString;
        }

        string myReqeustsSQL = "SELECT  * FROM dbo.AccessToResourceForm  ";
       


        passwordAssistanceLV.DataSource = DataBase.dbDataTable(myReqeustsSQL + searchString + sqlOrderBy);
        passwordAssistanceLV.DataBind();

        // Response.Write("Select * from PasswordAssistanceForm " + searchString + sqlOrderBy);
    }
    protected void passwordAssistanceLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;

            Literal SubmittedByPINTxt = (Literal)ditem.FindControl("SubmittedByPIN");
            Literal SubmissionDateandTimeTxt = (Literal)ditem.FindControl("SubmissionDateandTime");
            Literal AssignedTo = (Literal)ditem.FindControl("AssignedTo");
            Literal ActiveLit = (Literal)ditem.FindControl("Active");
            HyperLink ViewEdit = (HyperLink)ditem.FindControl("ViewEdit");

            string resourceID = item["ResourceToAccess"].ToString();

             
                ViewEdit.NavigateUrl = "/admin/requests/editrequest.aspx?reqid=" + item["ID"].ToString();
             

                string Active = item["FormStatus"].ToString().Trim();
                string SubmissionDateandTime = item["SubmissionDateandTime"].ToString().Trim();
                SubmissionDateandTimeTxt.Text = getDatabaseDateformat(SubmissionDateandTime);
                SubmittedByPINTxt.Text = getUserNameByPIN(item["SubmittedByPIN"].ToString().Trim());
                AssignedTo.Text = getResourceRequestsAdminUsers(resourceID);
                ActiveLit.Text = Active;
             

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
            name =  row["Name"].ToString();
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

    
    protected void updateList_Click(object sender, EventArgs e)
    {
        string aaa = searchWordTxt.Text;

     //   sqlSearchString = "   ResourceName like '%" + aaa + "%' or  Description like '%" + aaa + "%'  ";
     //   getResources();
    }
    
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



    protected void passwordAssistanceLV_OnSorting(object sender, ListViewSortEventArgs e)
    {
        LinkButton ResourceNameBtn = passwordAssistanceLV.FindControl("ResourceNameBtn") as LinkButton;
        ImageButton ResourceNameImg = passwordAssistanceLV.FindControl("ResourceNameImg") as ImageButton;
        

        string DefaultSortIMG = "~/admin/img/asc.png";
        string imgUrl = "~/admin/img/desc.png";


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
            case "Date":
                if (ResourceNameImg != null)
                    ResourceNameImg.ImageUrl = DefaultSortIMG;
                
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
                ViewState["sqlOrderBy"] = "  order by SubmissionDateandTime asc";
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

        foreach (ListItem li in AdminUsers.Items)
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
        //string epassSelect = ePassDD.SelectedValue;
        //string epassS = string.Empty;
        //switch (epassSelect)
        //{
        //    case "All":
        //        epassS = " LimitedNumberPasswordsAvailable in ('Y','N') ";
        //        break;

        //    case "ePassword":
        //        epassS = " LimitedNumberPasswordsAvailable = 'Y' ";
        //        break;

        //    case "noneePassword":
        //        epassS = " LimitedNumberPasswordsAvailable = 'N' ";
        //        break;
        //}


        //if (subjectAreaIDs == "")
        //{
        //    epassS = "    " + epassS;
        //}
        //else
        //{
        //    epassS = " and  " + epassS;
        //}
        //Response.Write(subjectAreaIDs + epassS);
      // sqlSearchString = subjectAreaIDs + epassS;
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


    public string AdminID { get; set; }

    public string myAdminID { get; set; }
}