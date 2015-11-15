using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;
using System.Net;
using System.Data.SqlClient;

public partial class admin_users_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string PIN = Request.QueryString["PIN"].ToString();
            Dictionary<string, string> UsersDetails = loginSSA.GetUsersDetails(PIN);
            FullName = UsersDetails["LastName"] + " " + UsersDetails["FirstName"];
            RequestorNameHF.Value = FullName;
            RequestorName.Text = FullName;
            RequestorTitle.Text = UsersDetails["Title"];
            RequestorComponent.Text = UsersDetails["Component"];
            RequestorOfficeCode.Text = UsersDetails["OfficeCode"];
            RequestorEmail.Text = UsersDetails["Email"];
            RequestorPhone.Text = UsersDetails["Telephone"];


            RequestorFirstName.Value = UsersDetails["LastName"];
            RequestorLastName.Value = UsersDetails["FirstName"];
            RequestorServer.Value = UsersDetails["Server"]; // UsersDetails["LastName"];
            RequestorUserDomain.Value = UsersDetails["Domain"];  //UsersDetails["LastName"];

 


            getDropDownValues();




            DataSet dtAR = DataBase.dbDataSet("Select * from  AutoReplies where Form = 'ePass';");
            // Response.Write("Select * from PasswordAssignments where ResourceID = '" + resourceID + "' and UserPIN = '" + UseThisInfo_PIN + "' ");
            if (dtAR.Tables[0].Rows.Count == 1)
            {

                SendRequestApprovedEmailMessage = dtAR.Tables[0].Rows[0].Field<string>("SendRequestApprovedEmailMessage").Trim();
                RequestApprovedEmailMessageSubject.Text = dtAR.Tables[0].Rows[0].Field<string>("RequestApprovedEmailMessageSubject").Trim();
                RequestApprovedEmailMessageText.Text = WebUtility.HtmlDecode(dtAR.Tables[0].Rows[0].Field<string>("RequestApprovedEmailMessageText").Trim());
                RequestApprovedEmailMessageFromAddress.Value = dtAR.Tables[0].Rows[0].Field<string>("RequestApprovedEmailMessageFromAddress").Trim();

            }


            if (SendRequestApprovedEmailMessage == "false")
            {
                MessagingPanel.Visible = false;
                ApproveAccessBtn.Text = "Approve Access";
            }
            if (SendRequestApprovedEmailMessage == "true")
            {
                MessagingPanel.Visible = true;
                ApproveAccessBtn.Text = "Approve Access & Notify User";
            }

        }
    }

    private string getAssignedLicensesCount(string resourceID)
    {
        return DataBase.returnOneValue("Select count(*) as count from PasswordAssignments where ResourceID = '" + resourceID + "'");
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
            ResourceNameHF.Value = resourceName;
            resourceDescription = foundRows[i]["Description"].ToString().Trim();
            resourceAdminResourceURL = foundRows[i]["AdminResourceURL"].ToString().Trim();
            resourceAdminUsername = foundRows[i]["AdminUsername"].ToString().Trim();
            resourceAdminPassword = foundRows[i]["AdminPassword"].ToString().Trim();
            resourceLimitedNumberPasswordsAvailable = foundRows[i]["LimitedNumberPasswordsAvailable"].ToString().Trim();
            resourcePasswordsAvailable = foundRows[i]["PasswordsAvailable"].ToString().Trim();
           // if (resourceLimitedNumberPasswordsAvailable == "Y")   LicensePanel.Visible = true;
            if (resourceAdminResourceURL != "") AdminLoginPanel.Visible = true;
          
        } 
    }



    protected void ResourceDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        string getThisID = ResourceDD.SelectedValue;

        if (getThisID != "")
        {
            ResourceSummaryPanel.Visible = true;
            DataRow[] foundRows = resourcesDt.Select("ID='" + long.Parse(getThisID) + "' ");

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


            AssignedLicenses = getAssignedLicensesCount(getThisID);
            if (resourceLimitedNumberPasswordsAvailable == "Y")
            {
                AvailableLicenses = (Convert.ToInt16(resourcePasswordsAvailable) - Convert.ToInt16(AssignedLicenses)).ToString();
            }
            else
            {
                AvailableLicenses = "Unlimited";
            }
        }

    }



    private void getRequestDetails(string resourceID)
    {
        throw new NotImplementedException();
    }


 
    private void seeIfRecordExists()
    {
        
    }

    protected void ApproveAccess_Click(object sender, EventArgs e)
    {
        PasswordActiveTxt = StatusDD.SelectedItem.Text;
        saveRecord(PasswordActiveTxt, "Approved-Closed");
        commonfunctions.sendEmailMessageWithAttachment(RequestorEmail.Text, RequestApprovedEmailMessageFromAddress.Value, RequestApprovedEmailMessageSubject.Text, RequestApprovedEmailMessageText.Text, attachment);

        Response.Redirect("/admin/requests/confirm.aspx?panel=approved&reqname=" + RequestorNameHF.Value + "&rsname=" + ResourceDD.SelectedItem.Text);
    }


    protected void SaveForLater_Click(object sender, EventArgs e)
    {
        saveRecord("Inactive", "Approved-Not-Notified");      
            ///lets send email
            ///       
        Response.Redirect("/admin/requests/usersProfileAccess.aspx?PIN=" + Request.QueryString["PIN"].ToString());
    }

    private void saveRecord(string PasswordStatus, string FormStatus)
    {
        string sqlInsert = "";
        ResourceIDTxt = ResourceDD.SelectedValue;
        PasswordAssignedByTxt = loginSSA.myPIN;
        UserNameTxt = Username.Text;
        PasswordTxt = Password.Text;
        UserPINTxt = Request.QueryString["PIN"].ToString(); // RequestorPIN.Value;
        //string reqID = RequestID.Value;
        //if (RecordExistsHF.Value == "true")
        //{
        //    sqlInsert = "  UPDATE [dbo].[PasswordAssignments] ";
        //    sqlInsert += " SET [UserPIN] = '" + UserPINTxt + "',[ResourceID] = '" + ResourceIDTxt + "',[EPassAssignmentDate] = GETDATE() ";
        //    sqlInsert += " ,[PasswordAssignedBy] = '" + PasswordAssignedByTxt + "',[UserName] = '" + UserNameTxt + "',[Password] = '" + PasswordTxt + "',[PasswordActive] = '" + PasswordStatus + "' where RequestID='" + reqID + "' ; ";

        //}
        //else
        //{


        string ResourceToAccess = ResourceDD.SelectedValue;
        
        string InternalNotesTxt = InternalNotes.Text;

        string SubmittedByPIN = loginSSA.myPIN;
        string SubmittedByLastName = loginSSA.myLastName;
        string SubmittedByFirstName = loginSSA.myFirstName;
        string SubmittedByEMail = loginSSA.myEmail;
        string SubmittedByOffice = loginSSA.myOffice;
        string SubmittedByServer = loginSSA.myServer;
        string SubmittedByUserDomain = loginSSA.myUserDomain;

        string SubmittedForPIN = Request.QueryString["PIN"].ToString();
        //string SubmittedForLastName = RequestorLastName.Value;
        //string SubmittedForFirstName = RequestorFirstName.Value;
        //string SubmittedForEMail = RequestorEmail.Text;
        //string SubmittedForOffice = RequestorOfficeCode.Text;
        //string SubmittedForServer = RequestorServer.Value;
        //string SubmittedForUserDomain = RequestorUserDomain.Value;
        string WhyDoYouNeedAccess = "";
        Dictionary<string, string> GetUsersDetails = loginSSA.GetUsersDetails(SubmittedForPIN);

        string SubmittedForLastName = GetUsersDetails["LastName"]; // RequestorLastName.Value;
        string SubmittedForFirstName = GetUsersDetails["FirstName"]; // RequestorFirstName.Value;
        string SubmittedForEMail = GetUsersDetails["Email"]; //RequestorEmail.Text;
        string SubmittedForOffice = GetUsersDetails["OfficeCode"]; //RequestorOfficeCode.Text;
        string SubmittedForServer = GetUsersDetails["Server"];// RequestorServer.Value;
        string SubmittedForUserDomain = GetUsersDetails["Domain"]; //RequestorUserDomain.Value;


        InternalNotesTxt = InternalNotes.Text;

       // Dictionary<string, string> GetUsersDetails = loginSSA.GetUsersDetails(SubmittedForPIN);


        string sql = "INSERT INTO [dbo].[AccessToResourceForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],";
        sql += " [SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer], ";
        sql += "[SubmittedForUserDomain],[SubmissionDateandTime],[ResourceToAccess],[WhyDoYouNeedAccess],[InternalNotes],[FormStatus] , ModifiedDateTime , ModifiedByPIN ) VALUES";
        sql += "  ('" + SubmittedByPIN + "' ,'" + SubmittedByLastName + "' ,'" + SubmittedByFirstName + "' ,'" + SubmittedByEMail + "' ,'" + SubmittedByOffice + "' ,'" + SubmittedByServer + "' ,'";
        sql += SubmittedByUserDomain + "' ,'" + SubmittedForPIN + "' ,'" + SubmittedForLastName + "' ,'" + SubmittedForFirstName + "' ,'" + SubmittedForEMail + "' ,'" + SubmittedForOffice + "' ,'";
        sql += SubmittedForServer + "' ,'" + SubmittedForUserDomain + "' , GETDATE() ,'" + ResourceToAccess + "' ,'" + WhyDoYouNeedAccess + "' ,@InternalNotesTxt ,'" + FormStatus + "', GETDATE() ,'" + loginSSA.myPIN + "')";
        //  Response.Write(sql);
        //string reqID = DataBase.executeCommanAndReturnSCOPE_IDENTITY(sql);
        SqlCommand sqlcmda = new SqlCommand(sql);
        sqlcmda.Parameters.AddWithValue("@InternalNotesTxt", InternalNotes.Text);
        string reqID = DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmda);


        sqlInsert = "INSERT INTO [dbo].[PasswordAssignments]([UserPIN],[ResourceID],[EPassAssignmentDate],[PasswordAssignedBy],[UserName],[Password],[PasswordActive], [RequestID], UserFirstName,UserLastName,UserEmail,UserOfficeCode )";
        sqlInsert += " Values  ('" + SubmittedForPIN + "', '" + ResourceIDTxt + "', GETDATE(), '" + PasswordAssignedByTxt + "', '" + UserNameTxt + "', '" + PasswordTxt + "', '" + PasswordStatus + "' , '" + reqID + "','" + SubmittedForFirstName + "' ,'" + SubmittedForLastName + "' ,'" + SubmittedForEMail + "' ,'" + SubmittedForOffice + "')";
        //   }

       // DataBase.executeCommand("Update AccessToResourceForm Set FormStatus ='" + FormStatus + "' , InternalNotes='" + InternalNotes.Text + "'  where ID='" + RequestID.Value.Trim() + "' ;" + sqlInsert);
        string sqll = "Update AccessToResourceForm Set FormStatus ='" + FormStatus + "' , InternalNotes=@InternalNotesTxt where ID='" + RequestID.Value.Trim() + "' ;" + sqlInsert;
        SqlCommand sqlcmd = new SqlCommand(sqll);
        sqlcmd.Parameters.AddWithValue("@InternalNotesTxt", InternalNotes.Text);
        DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmd);


        AuditLogs.log_Changes(RequestID.Value.Trim() , "AccessToResourceForm");
    }


    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/requests/default.aspx");
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

    private DataTable dtAllResources()
    {
        string sqll = "select * from  Resources where AccessTypeTaxonomy='122'";
        return resourcesDt = DataBase.sortDataTable(DataBase.dbDataTable(sqll), "ResourceName", "ASC");

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


    public string PasswordActiveTxt { get; set; }

    public string PasswordTxt { get; set; }

    public string PasswordAssignedByTxt { get; set; }

    public string ResourceIDTxt { get; set; }

    public string UserPINTxt { get; set; }

    public string UsernameTxt { get; set; }

    public string UserNameTxt { get; set; }



    public string SendRequestApprovedEmailMessage { get; set; }

    public string FullName { get; set; }
}