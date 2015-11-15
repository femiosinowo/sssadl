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

            showwhat = Request.QueryString["show"].ToString();
            string reqID = Request.QueryString["reqid"].ToString();
            dtAllResources();
            ResourceDD.DataSource = resourcesDt;
            ResourceDD.DataBind();

            DataSet dt = new DataSet();
            dt = DataBase.dbDataSet("Select * from AccessToResourceForm where ID = '" + reqID + "';", "Admin.DbConnection");
            RequestID.Value = reqID;
            // AccessToID =   dt.Tables[0].Rows[0].Field<string>("ID").Trim();
            AccesIDHF.Value = dt.Tables[0].Rows[0].Field<int>("ID").ToString();
            SubmittedByPINDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByPIN").Trim();
            SubmittedByLastNameDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByLastName").Trim();
            SubmittedByFirstNameDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByFirstName").Trim();
            SubmittedByEMailDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByEMail").Trim();
            SubmittedByOfficeDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByOffice").Trim();
            SubmittedByServerDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByServer").Trim();
            SubmittedByUserDomainDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByUserDomain").Trim();

            SubmittedForPINDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForPIN").Trim();
            SubmittedForLastNameDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForLastName").Trim();
            SubmittedForFirstNameDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForFirstName").Trim();
            SubmittedForEMailDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForEMail").Trim();
            SubmittedForOfficeDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForOffice").Trim();
            SubmittedForServerDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForServer").Trim();
            SubmittedForUserDomainDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForUserDomain").Trim();


            resourceID = dt.Tables[0].Rows[0].Field<string>("ResourceToAccess").Trim();
            string WhyDoYouNeedAccessDB = dt.Tables[0].Rows[0].Field<string>("WhyDoYouNeedAccess").Trim();
            InternalNotesDB = dt.Tables[0].Rows[0].Field<string>("InternalNotes").Trim();
            FormStatusDB = dt.Tables[0].Rows[0].Field<string>("FormStatus").Trim();

            if (FormStatusDB == "Approved-Closed") ApproveBtn.Visible = false;



            Dictionary<string, string> RequestorDetails = new Dictionary<string, string>();

            try
            {
                RequestorDetails = loginSSA.GetUsersDetails(SubmittedForPINDB);
            }
            catch
            {
                 RequestorDetails["DisplayName"] = SubmittedForPINDB;
                 RequestorDetails["Title"] = "N/A";
                 RequestorDetails["OfficeCode"] = "N/A";
                   RequestorDetails["Email"] = "N/A";
                  RequestorDetails["Telephone"] = "N/A";
                  RequestorDetails["Component"] = "N/A";
            }
            Dictionary<string, string> SubmittedByDetails = loginSSA.GetUsersDetails(SubmittedByPINDB);

            //getRequestDetails(resourceID);
            ResourceDD.Items.FindByValue(resourceID).Selected = true;
            // resourceName = ResourceDD.SelectedItem.Text;

            getResourceDetails(resourceID);


            AuditLogUX.tableName = "AccessToResourceForm";
            AuditLogUX.CHID = reqID;


            AssignedLicenses = getAssignedLicensesCount(resourceID);
            if (resourceLimitedNumberPasswordsAvailable == "Y")
            {
                AvailableLicenses = (Convert.ToInt16(resourcePasswordsAvailable) - Convert.ToInt16(AssignedLicenses)).ToString();
            }
            else
            {
                AvailableLicenses = "Unlimited";
            }
            ListUsersWithAccess = "&nbsp;&nbsp;&nbsp;&nbsp; | &nbsp;&nbsp;&nbsp;&nbsp; <a target='_blank' href='/admin/requests/usersWithAccess.aspx?rId=" + resourceID + "'>List users with access to this resource</a>";


            InternalNotes.Text = InternalNotesDB;
            RequestorName.Text = RequestorDetails["DisplayName"];
            RequestorTitle.Text = RequestorDetails["Title"];
            RequestorOfficeCode.Text = RequestorDetails["OfficeCode"];
            RequestorEmail.Text = RequestorDetails["Email"];
            RequestorPhone.Text = RequestorDetails["Telephone"];
            RequestorComponent.Text = RequestorDetails["Component"];
            try
            {
                SubmittedByName.Text = SubmittedByDetails["DisplayName"];
                SubmittedByOfficeCode.Text = SubmittedByDetails["OfficeCode"];
                SubmittedByEmail.Text = SubmittedByDetails["Email"];
                SubmittedByPhone.Text = SubmittedByDetails["Telephone"];
                SubmittedByTitle.Text = SubmittedByDetails["Title"];
                SubmittedByComponent.Text = SubmittedByDetails["Component"];
            }
            catch {

                SubmittedByName.Text = SubmittedByPINDB;
                SubmittedByOfficeCode.Text = "N/A";
                SubmittedByEmail.Text = "N/A";
                SubmittedByPhone.Text = "N/A";
                SubmittedByTitle.Text = "N/A";
                SubmittedByComponent.Text = "N/A";
            
            }
            whyNeedAccess.Text = WhyDoYouNeedAccessDB;

            //lets disable all this boxes
            SubmittedByName.Enabled = false; // = SubmittedByPINDB;
            SubmittedByOfficeCode.Enabled = false;
            SubmittedByEmail.Enabled = false; // = "N/A";
            SubmittedByPhone.Enabled = false; // = "N/A";
            SubmittedByTitle.Enabled = false; // = "N/A";
            SubmittedByComponent.Enabled = false; // = "N/A";
            RequestorName.Enabled = false; // = RequestorDetails["DisplayName"];
            RequestorTitle.Enabled = false; // = RequestorDetails["Title"];
            RequestorOfficeCode.Enabled = false; // = RequestorDetails["OfficeCode"];
            RequestorEmail.Enabled = false; // = RequestorDetails["Email"];
            RequestorPhone.Enabled = false; // = RequestorDetails["Telephone"];
            RequestorComponent.Enabled = false; // = RequestorDetails["Component"];

            ///if this record has been saved for later
            ///
            notifyStle = "style='display:none'";
            divApprovedDiv = "style='display:none'";
            divDeclinedDiv = "style='display:none'";
            divExistsApprovedDiv = "style='display:none'";
            if (FormStatusDB == "Declined-Not-Notified")
            {
                notifyStle = "";
                divDeclinedDiv = "";
                string ModifiedDateTime = dt.Tables[0].Rows[0].Field<DateTime>("ModifiedDateTime").ToShortDateString();
                string ModifiedByPIN = dt.Tables[0].Rows[0].Field<string>("ModifiedByPIN").Trim();
                notifyDate = ModifiedDateTime;
                Dictionary<string, string> allUserDetails = loginSSA.GetUsersDetails(ModifiedByPIN);
                notifyByUsername = allUserDetails["DisplayName"];
            }

            try
            {
                DataSet dtSL = DataBase.dbDataSet("Select * from PasswordAssignments where RequestID = '" + reqID + "'   ", "Admin.DbConnection");
                if (dtSL.Tables[0].Rows.Count == 1)
                {
                    notifyStle = "";
                    divApprovedDiv = "";
                    AssignmentIDHF.Value = dtSL.Tables[0].Rows[0].Field<int>("ID").ToString();
                    notifyDate = dtSL.Tables[0].Rows[0].Field<DateTime>("EPassAssignmentDate").ToString("MM/dd/yyyy");
                    Dictionary<string, string> allUserDetails = loginSSA.GetUsersDetails(dtSL.Tables[0].Rows[0].Field<string>("PasswordAssignedBy").Trim());
                    notifyByUsername = allUserDetails["DisplayName"];

                }
            }
            catch { }


            ///check if this person is already approved and has access to this rresouce
            ///
            DataTableReader dtrCheck = DataBase.dbDataSet("Select * from PasswordAssignments where UserPIN = '" + SubmittedForPINDB + "' and ResourceID='" + resourceID + "' and PasswordActive='Active'", "Admin.DbConnection").CreateDataReader();
            //Response.Write("Select * from PasswordAssignments where UserPIN = '" + SubmittedForPINDB + "' and ResourceID='" + resourceID + "'");
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

        }

    }


    private string getAssignedLicensesCount(string resourceID)
    {
        return DataBase.returnOneValue("Select count(*) as count from PasswordAssignments where ResourceID = '" + resourceID + "'", "Admin.DbConnection");
    }


    private void getResourceDetails(string resourceID)
    {
        DataRow[] foundRows = resourcesDt.Select("ID='" + resourceID + "'");

        // Use the Select method to find all rows matching the filter.
        //  foundRows = resourcesDt.Select(filter);

        // Print column 0 of each returned row. 
        for (int i = 0; i < foundRows.Length; i++)
        {
            resourceName = foundRows[i]["ResourceName"].ToString().Trim();
            resourceDescription = foundRows[i]["Description"].ToString().Trim();
            resourceAdminResourceURL = foundRows[i]["AdminResourceURL"].ToString().Trim();
            resourceAdminUsername = foundRows[i]["AdminUsername"].ToString().Trim();
            resourceAdminPassword = foundRows[i]["AdminPassword"].ToString().Trim();
            resourceLimitedNumberPasswordsAvailable = foundRows[i]["LimitedNumberPasswordsAvailable"].ToString().Trim();
            resourcePasswordsAvailable = foundRows[i]["PasswordsAvailable"].ToString().Trim();

            if (resourceAdminResourceURL != "") AdminLoginPanel.Visible = true;

        }
    }

    private void getRequestDetails(string resourceID)
    {
        throw new NotImplementedException();
    }


    private void dtAllResources()
    {
        string sqll = "select * from  Resources where AccessTypeTaxonomy='122'";
        resourcesDt = DataBase.sortDataTable(DataBase.dbDataTable(sqll, "Admin.DbConnection"), "ResourceName", "ASC");
    }


    protected void SaveForLater_Click(object sender, EventArgs e)
    {
        showwhat = Request.QueryString["show"].ToString();
        saveRecord("Open");
        Response.Redirect("/admin/requests/default.aspx?show=" + showwhat);
    }


    private void saveRecord(string FormStatus)
    {
        string sql = "Update AccessToResourceForm Set FormStatus ='" + FormStatus + "' , WhyDoYouNeedAccess=@WhyDoYouNeedAccess ,ModifiedDateTime=GETDATE(), ModifiedByPIN ='" + loginSSA.myPIN + "'  , InternalNotes=@InternalNotesTxt  where ID='" + RequestID.Value.Trim() + "'";
        DataBase.executeCommand(sql);

        SqlCommand sqlcmd = new SqlCommand(sql);
        sqlcmd.Parameters.AddWithValue("@WhyDoYouNeedAccess", whyNeedAccess.Text);
        sqlcmd.Parameters.AddWithValue("@InternalNotesTxt", InternalNotes.Text);
        DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmd);

        AuditLogs.log_Changes(AccesIDHF.Value, "AccessToResourceForm");

    }

 

    protected void Approve_Click(object sender, EventArgs e)
    {
        showwhat = Request.QueryString["show"].ToString();
        saveRecord("Approved-Not-Notified");
        Response.Redirect("/admin/requests/approvedRequest.aspx?reqid=" + RequestID.Value.Trim() + "&show=" + showwhat);
    }
    protected void DeclineBtn_Click(object sender, EventArgs e)
    {
        //delete any entry from previous approved
        showwhat = Request.QueryString["show"].ToString();
        string sqlDelete = "Delete from PasswordAssignments where RequestID = '" + RequestID.Value.Trim() + "'";
        saveRecord("Declined-Not-Notified");
        DataBase.executeCommand(sqlDelete, "Admin.DbConnection");
        Response.Redirect("/admin/requests/declinedRequest.aspx?reqid=" + RequestID.Value.Trim() + "&show=" + showwhat);
    }

    public DataTable resourcesDt { get; set; }



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



public  string divExistsApprovedDiv { get; set; }
public string approvedByDetails { get; set; }

public string approvedDate { get; set; }

public string showwhat { get; set; }
}