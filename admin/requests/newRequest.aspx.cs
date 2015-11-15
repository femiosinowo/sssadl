using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;
using System.Data.SqlClient;

public partial class admin_users_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            getDropDownValues();
            SubmittedByPIN.Text = loginSSA.myPIN;
            SubmittedByName.Text = loginSSA.myFirstName + " " + loginSSA.myLastName;
            SubmittedByTitle.Text = loginSSA.myTitle;
            SubmittedByComponent.Text = loginSSA.myComponent;
            SubmittedByOfficeCode.Text = loginSSA.myOffice;

            SubmittedByEmail.Text = loginSSA.myEmail;
            SubmittedByPhone.Text = loginSSA.myPhone;

            notifyStle = "style='display:none'";

        }
        else
        {

        }

    }


    private string getAssignedLicensesCount(string resourceID)
    {
        return DataBase.returnOneValue("Select count(*) as count from PasswordAssignments where ResourceID = '" + resourceID + "'");
    }


    private void getResourceDetails(string resourceID)
    {

    }

    private void getRequestDetails(string resourceID)
    {
        throw new NotImplementedException();
    }
    private void getDropDownValues()
    {


        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("ResourceName");

        dt.Rows.Add("", " - Select One - ");

        DataTableReader dtR = resourcesDt.CreateDataReader();
        while (dtR.Read())
        {
            dt.Rows.Add(dtR["ID"].ToString(), dtR["ResourceName"].ToString());
        }

        ResourceDD.DataSource = dt;
        ResourceDD.DataBind();

    }

    private DataTable dtAllResources()
    {
        string sqll = "select * from  Resources where AccessTypeTaxonomy='122'";
        return resourcesDt = DataBase.sortDataTable(DataBase.dbDataTable(sqll), "ResourceName", "ASC");

    }


    protected void SaveForLater_Click(object sender, EventArgs e)
    {
        //DataBase.executeCommand("Update AccessToResourceForm Set FormStatus ='Open' , InternalNotes='" + InternalNotes.Text + "'    where ID='" + RequestID.Value.Trim() + "'");
        try { showwhat = Request.QueryString["show"].ToString(); }
        catch { }
        string reqID = saveRecord("Open");
        Response.Redirect("/admin/requests/newRequestMessage.aspx?reqid=" + reqID + "&show=" + showwhat);
    }

    protected void Approve_Click(object sender, EventArgs e)
    {
        ///check if this person is already approved and has access to this rresouce
        ///
        try { showwhat = Request.QueryString["show"].ToString(); }
        catch { }
        DataTableReader dtrCheck = DataBase.dbDataSet("Select * from PasswordAssignments where UserPIN = '" + RequestorPIN.Text + "' and ResourceID='" + ResourceDD.SelectedValue + "' and PasswordActive='Active'", "Admin.DbConnection").CreateDataReader();
        if (dtrCheck.HasRows)
        {
            dtrCheck.Read();
            ApproveBtn.Enabled = false;
            divExistsApprovedDiv = "style='display:block'";
            Dictionary<string, string> GetUsersDetails = loginSSA.GetUsersDetails(dtrCheck["PasswordAssignedBy"].ToString().Trim());
            notifyStle = "style='display:block'";
            approvedDate = Convert.ToDateTime(dtrCheck["EPassAssignmentDate"].ToString().Trim()).ToShortDateString();
            approvedByDetails = GetUsersDetails["DisplayName"].ToString();

        }
        else
        {

            string reqID = saveRecord("Approved-Not-Notified");


            Response.Redirect("/admin/requests/approvedRequest.aspx?reqid=" + reqID + "&show=" + showwhat);
        }
    }
    protected void DeclineBtn_Click(object sender, EventArgs e)
    {
        //delete any entry from previous approved
        //string sqlDelete = "Delete from PasswordAssignments where RequestID = '" + RequestID.Value.Trim() + "'";
        //DataBase.executeCommand("Update AccessToResourceForm Set    ModifiedDateTime=GETDATE() , ModifiedByPIN ='" + loginSSA.myPIN + "' , FormStatus ='Declined-Not-Notified' , InternalNotes='" + InternalNotes.Text + "' where ID='" + RequestID.Value.Trim() + "'; " + sqlDelete);
        //Response.Redirect("/admin/requests/declinedRequest.aspx?reqid=" + RequestID.Value.Trim());
        try { showwhat = Request.QueryString["show"].ToString(); }
        catch { }
        string reqID = saveRecord("Declined-Not-Notified");
        Response.Redirect("/admin/requests/declinedRequest.aspx?reqid=" + reqID + "&show=" + showwhat);
    }

    private string saveRecord(string status)
    {

        string ResourceToAccess = ResourceDD.SelectedValue;
        //string WhyDoYouNeedAccess = AdminFunc.replaceHypen(whyNeedAccess.Text);
        // string InternalNotesTxt = AdminFunc.replaceHypen(InternalNotes.Text);
        string FormStatus = status;

        string SubmittedByLastName = loginSSA.myLastName;
        string SubmittedByFirstName = loginSSA.myFirstName;
        string SubmittedByEMail = loginSSA.myEmail;
        string SubmittedByOffice = loginSSA.myOffice;
        string SubmittedByServer = loginSSA.myServer;
        string SubmittedByUserDomain = loginSSA.myUserDomain;


        string SubmittedForPIN = RequestorPIN.Text;
        Dictionary<string, string> GetUsersDetails = loginSSA.GetUsersDetails(SubmittedForPIN);

        string SubmittedForLastName = GetUsersDetails["LastName"]; // RequestorLastName.Value;
        string SubmittedForFirstName = GetUsersDetails["FirstName"]; // RequestorFirstName.Value;
        string SubmittedForEMail = GetUsersDetails["Email"]; //RequestorEmail.Text;
        string SubmittedForOffice = GetUsersDetails["OfficeCode"]; //RequestorOfficeCode.Text;
        string SubmittedForServer = GetUsersDetails["Server"];// RequestorServer.Value;
        string SubmittedForUserDomain = GetUsersDetails["Domain"]; //RequestorUserDomain.Value;




        string sql = "INSERT INTO [dbo].[AccessToResourceForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],";
        sql += " [SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer], ";
        sql += "[SubmittedForUserDomain],[SubmissionDateandTime],[ResourceToAccess],[WhyDoYouNeedAccess],[InternalNotes],[FormStatus] , ModifiedDateTime , ModifiedByPIN ) VALUES";
        sql += "  ('" + SubmittedByPIN.Text + "' ,'" + SubmittedByLastName + "' ,'" + SubmittedByFirstName + "' ,'" + SubmittedByEMail + "' ,'" + SubmittedByOffice + "' ,'" + SubmittedByServer + "' ,'";
        sql += SubmittedByUserDomain + "' ,'" + SubmittedForPIN + "' ,'" + SubmittedForLastName + "' ,'" + SubmittedForFirstName + "' ,'" + SubmittedForEMail + "' ,'" + SubmittedForOffice + "' ,'";
        sql += SubmittedForServer + "' ,'" + SubmittedForUserDomain + "' , GETDATE() ,'" + ResourceToAccess + "' ,@WhyDoYouNeedAccess ,@InternalNotesTxt ,'" + FormStatus + "', GETDATE() ,'" + loginSSA.myPIN + "')";
        //  Response.Write(sql);

        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@WhyDoYouNeedAccess", whyNeedAccess.Text);
        sqlcmd.Parameters.AddWithValue("@InternalNotesTxt", InternalNotes.Text);
        return DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmd);

        // return DataBase.executeCommanAndReturnSCOPE_IDENTITY(sql);
    }



    //protected void ResourceDD_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string getThisID = ResourceDD.SelectedValue;

    //    if (getThisID != "")
    //    {
    //        ResourceSummaryPanel.Visible = true;

    //        DataRow[] foundRows = resourcesDt.Select("ID='" + long.Parse(getThisID) + "' ");

    //        // Use the Select method to find all rows matching the filter.
    //        //  foundRows = resourcesDt.Select(filter);

    //        // Print column 0 of each returned row. 
    //        for (int i = 0; i < foundRows.Length; i++)
    //        {
    //            resourceName = foundRows[i]["ResourceName"].ToString().Trim();
    //            resourceDescription = foundRows[i]["Description"].ToString().Trim();
    //            resourceAdminResourceURL = foundRows[i]["AdminResourceURL"].ToString().Trim();
    //            resourceAdminUsername = foundRows[i]["AdminUsername"].ToString().Trim();
    //            resourceAdminPassword = foundRows[i]["AdminPassword"].ToString().Trim();
    //            resourceLimitedNumberPasswordsAvailable = foundRows[i]["LimitedNumberPasswordsAvailable"].ToString().Trim();
    //            resourcePasswordsAvailable = foundRows[i]["PasswordsAvailable"].ToString().Trim();

    //            if (resourceAdminResourceURL != "") AdminLoginPanel.Visible = true;

    //        }

    //        AssignedLicenses = getAssignedLicensesCount(getThisID);
    //        if (resourceLimitedNumberPasswordsAvailable == "Y")
    //        {
    //            AvailableLicenses = (Convert.ToInt16(resourcePasswordsAvailable) - Convert.ToInt16(AssignedLicenses)).ToString();
    //        }
    //        else
    //        {
    //            AvailableLicenses = "Unlimited";
    //        }
    //    }
    //}

    // public DataTable resourcesDt { get; set; }
    public virtual DataTable resourcesDt
    {
        get
        {
            if (ViewState["resourcesDt"] == null)
            {
                ViewState["resourcesDt"] = dtAllResources();
            }
            return (DataTable)ViewState["resourcesDt"];
        }
        set
        {
            ViewState["resourcesDt"] = value;
        }
    }


    public string SubmittedByPINDB { get; set; }

    public string SubmittedByLastNameDB { get; set; }

    public string SubmittedByFirstNameDB { get; set; }

    public string SubmittedByOfficeDB { get; set; }

    public string SubmittedByEMailDB { get; set; }

    public string SubmittedByServerDB { get; set; }

    public string SubmittedByUserDomainDB { get; set; }

    public string SubmittedForPINDB { get; set; }

    public string SubmittedForLastNameDB { get; set; }

    public string SubmittedForFirstNameDB { get; set; }

    public string SubmittedForEMailDB { get; set; }

    public string SubmittedForOfficeDB { get; set; }

    public string SubmittedForServerDB { get; set; }

    public string SubmittedForUserDomainDB { get; set; }

    public string resourceID { get; set; }

    public string AdditionalnotesordetailsDB { get; set; }

    public string FormStatusDB { get; set; }

    public string InternalNotesDB { get; set; }

    public string resourceName { get; set; }

    public string resourceDescription { get; set; }

    public string resourceAdminResourceURL { get; set; }

    public string resourceAdminUsername { get; set; }

    public string resourceAdminPassword { get; set; }

    public string resourcePasswordsAvailable { get; set; }

    public string resourceLimitedNumberPasswordsAvailable { get; set; }

    public string AssignedLicenses { get; set; }

    public string AvailableLicenses { get; set; }


    public string UseThisInfo_Name { get; set; }

    public string UseThisInfo_Title { get; set; }

    public string UseThisInfo_Component { get; set; }

    public string UseThisInfo_OfficeCode { get; set; }

    public string UseThisInfo_Email { get; set; }

    public string UseThisInfo_Phone { get; set; }

    public string UseThisInfo_PIN { get; set; }

    public string ListUsersWithAccess { get; set; }



    public string notifyStle { get; set; }

    public string notifyDate { get; set; }

    public string notifyByUsername { get; set; }

    public string divApprovedDiv { get; set; }

    public string divDeclinedDiv { get; set; }


    public string divExistsApprovedDiv { get; set; }

    public string approvedDate { get; set; }

    public string approvedByDetails { get; set; }

    public string showwhat { get; set; }
}