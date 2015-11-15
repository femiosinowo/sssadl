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
            string reqID = Request.QueryString["reqid"].ToString();
            dtAllResources();
            RequestID.Value = reqID;


            AuditLogUX.tableName = "AccessToResourceForm";
            AuditLogUX.tableName2 = "PasswordAssignments";
            AuditLogUX.ForeignColumnName = "RequestID";
            AuditLogUX.CHID = reqID;

            DataSet dt = new DataSet();
            dt = DataBase.dbDataSet("Select * from AccessToResourceForm where ID = '" + reqID + "';", "Admin.DbConnection");

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

            try { ModifiedDateTime = dt.Tables[0].Rows[0].Field<DateTime>("ModifiedDateTime").ToShortDateString();  
            
            ModifiedByPIN = dt.Tables[0].Rows[0].Field<string>("ModifiedByPIN").Trim();}
            catch { }
            FormStatusDB = dt.Tables[0].Rows[0].Field<string>("FormStatus").Trim();


            Dictionary<string, string> RequestorDetails = loginSSA.GetUsersDetails(SubmittedForPINDB);
            Dictionary<string, string> SubmittedByDetails = loginSSA.GetUsersDetails(SubmittedByPINDB);

            // string SubmissionDateandTime =  dt.Tables[0].Rows[0].Field<string>("SubmissionDateandTime").Trim();
            resourceID = dt.Tables[0].Rows[0].Field<string>("ResourceToAccess").Trim();
            string WhyDoYouNeedAccessDB = dt.Tables[0].Rows[0].Field<string>("WhyDoYouNeedAccess").Trim();
            InternalNotesDB = dt.Tables[0].Rows[0].Field<string>("InternalNotes").Trim();
            

            if (FormStatusDB == "Approved-Closed")
            {
                ApproveAccessBtn.Visible = false;
                SaveForLater.Text = "Save";
            }

            //if (SubmittedForPINDB == "None" || SubmittedForPINDB == "")
            //{
            //    RequestorPIN.Value = SubmittedByPINDB;
            //    UseThisInfo_PIN = SubmittedByPINDB;
            //    UseThisInfo_Name = SubmittedByLastNameDB + " " + SubmittedByFirstNameDB;
            //    UseThisInfo_Title = "";
            //    UseThisInfo_Component = "";
            //    UseThisInfo_OfficeCode = SubmittedByOfficeDB;
            //    UseThisInfo_Email = SubmittedByEMailDB;
            //    UseThisInfo_Phone = "";
            //}
            //else
            //{
            //    RequestorPIN.Value = SubmittedForPINDB;
            //    UseThisInfo_PIN = SubmittedForPINDB;
            //    UseThisInfo_Name = SubmittedForLastNameDB + " " + SubmittedForFirstNameDB;
            //    UseThisInfo_Title = "";
            //    UseThisInfo_Component = "";
            //    UseThisInfo_OfficeCode = SubmittedForOfficeDB;
            //    UseThisInfo_Email = SubmittedForEMailDB;
            //    UseThisInfo_Phone = "";
            //}


            RequestorNameHF.Value = RequestorDetails["DisplayName"];
            RequestorPIN.Value = RequestorDetails["PIN"]; 
            getResourceDetails(resourceID);
            ResourceIDHF.Value = resourceID;

            InternalNotes.Text = InternalNotesDB;
           // RequestorName.Text = UseThisInfo_Name;
          //  RequestorTitle.Text = "";
           // RequestorOfficeCode.Text = UseThisInfo_OfficeCode;
          //  RequestorEmail.Text = UseThisInfo_Email;



            RequestorName.Text = RequestorDetails["DisplayName"];
           // requestorName = RequestorDetails["DisplayName"];
            RequestorTitle.Text = RequestorDetails["Title"];
            RequestorOfficeCode.Text = RequestorDetails["OfficeCode"];
            RequestorEmail.Text = RequestorDetails["Email"];
            RequestorPhone.Text = RequestorDetails["Telephone"];

            //SubmittedByName.Text = SubmittedByDetails["DisplayName"];
            //SubmittedByOfficeCode.Text = SubmittedByDetails["OfficeCode"];
            //SubmittedByEmail.Text = SubmittedByDetails["Email"];
            //SubmittedByPhone.Text = SubmittedByDetails["Telephone"];
            //SubmittedByTitle.Text = SubmittedByDetails["Title"];


            AssignedLicenses = getAssignedLicensesCount(resourceID);
            if (resourceLimitedNumberPasswordsAvailable == "Y")
            {
                AvailableLicenses = (Convert.ToInt16(resourcePasswordsAvailable) - Convert.ToInt16(AssignedLicenses)).ToString();
            }
            else
            {
                AvailableLicenses = "Unlimited";
            }
            ListUsersWithAccess = "&nbsp;&nbsp;&nbsp;&nbsp; | &nbsp;&nbsp;&nbsp;&nbsp; <a target='_blank' href='/admin/requests/userswithaccess.aspx?rid=" + resourceID + "'>List users with access to this resource</a>";



            ///if this record has been saved for later
            /// 
            // DataSet dtSL = DataBase.dbDataSet("Select * from PasswordAssignments where ResourceID = '" + resourceID + "' and UserPIN = '" + UseThisInfo_PIN + "' ");
            DataSet dtSL = DataBase.dbDataSet("Select * from PasswordAssignments where RequestID = '" + reqID + "'  ", "Admin.DbConnection");
            // Response.Write("Select * from PasswordAssignments where ResourceID = '" + resourceID + "' and UserPIN = '" + UseThisInfo_PIN + "' ");
            if (dtSL.Tables[0].Rows.Count == 1)
            {
                RecordExistsHF.Value = "true";
                AssignmentID.Value = dtSL.Tables[0].Rows[0].Field<int>("ID").ToString();
                Username.Text = dtSL.Tables[0].Rows[0].Field<string>("UserName").Trim();
                Password.Text = dtSL.Tables[0].Rows[0].Field<string>("Password").Trim();
                string statuss = dtSL.Tables[0].Rows[0].Field<string>("PasswordActive").Trim();
                StatusDD.Items.FindByText(statuss).Selected = true;
                appMess.Visible = true;
                try
                {
                    Dictionary<string, string> pinDetails = loginSSA.GetUsersDetails(ModifiedByPIN);
                    approvedMessage = statuss + " Status granted on " + ModifiedDateTime + " by " + pinDetails["LastName"] + " " + pinDetails["FirstName"] + ". See notes for additional details. ";
                }
                catch { }
            }



            DataSet dtAR = DataBase.dbDataSet("Select * from  AutoReplies where Form = 'ePass';", "Admin.DbConnection");
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

    private void getRequestDetails(string resourceID)
    {
        throw new NotImplementedException();
    }


    private void dtAllResources()
    {
        string sqll = "select * from  Resources where AccessTypeTaxonomy='122'";
        resourcesDt = DataBase.sortDataTable(DataBase.dbDataTable(sqll, "Admin.DbConnection"), "ResourceName", "ASC");
    }
    private void seeIfRecordExists()
    {
        
    }

    protected void ApproveAccess_Click(object sender, EventArgs e)
    {
        try { showwhat = Request.QueryString["show"].ToString(); }
        catch { }
        PasswordActiveTxt = StatusDD.SelectedItem.Text;
        saveRecord(PasswordActiveTxt, "Approved-Closed");
        commonfunctions.sendEmailMessageWithAttachment(RequestorEmail.Text, RequestApprovedEmailMessageFromAddress.Value, RequestApprovedEmailMessageSubject.Text, RequestApprovedEmailMessageText.Text, attachment);

        Response.Redirect("/admin/requests/confirm.aspx?panel=approved&reqname=" + RequestorNameHF.Value + "&rsname=" + ResourceNameHF.Value + "&show=" + showwhat);
    }


    protected void SaveForLater_Click(object sender, EventArgs e)
    {
        try { showwhat = Request.QueryString["show"].ToString(); }
        catch { }
        if (SaveForLater.Text == "Save")
        {
            PasswordActiveTxt = StatusDD.SelectedItem.Text;
            saveRecord(PasswordActiveTxt, "Approved-Closed");
            Response.Redirect("/admin/requests/confirm.aspx?panel=approved&reqname=" + RequestorNameHF.Value + "&rsname=" + ResourceNameHF.Value + "&show=" + showwhat);
        }
        else
        {
            saveRecord("Inactive", "Approved-Not-Notified");
            if (SendEMailWhenDone.Checked)
            {
                ///lets send email
                ///
                commonfunctions.sendEmailMessageWithAttachment(RequestorEmail.Text, RequestApprovedEmailMessageFromAddress.Value, RequestApprovedEmailMessageSubject.Text, RequestApprovedEmailMessageText.Text, attachment);

            }
            Response.Write("<script>alert('Please Note That the Attachment Was Not Saved!'); location.href='/admin/requests/default.aspx?show=" + showwhat + "'</script>");
          //  Response.Redirect("/admin/requests/default.aspx?show=" + showwhat);
        }

    }

    private void saveRecord(string PasswordStatus, string formStatus)
    {
        string sqlInsert = "";
        ResourceIDTxt = ResourceIDHF.Value;
        PasswordAssignedByTxt = loginSSA.myPIN;
        UserNameTxt = Username.Text.Replace("ValueToDeleteBeforeSave", "");
        PasswordTxt = Password.Text.Replace("ValueToDeleteBeforeSave", ""); 
        UserPINTxt = RequestorPIN.Value;

        Dictionary<string, string> RequestorDetails = loginSSA.GetUsersDetails(UserPINTxt);

        string UserFirstName = RequestorDetails["FirstName"];
        string UserLastName = RequestorDetails["LastName"];
        string UserEmail = RequestorDetails["Email"];
        string UserOfficeCode = RequestorDetails["OfficeCode"];

       

        string reqID = RequestID.Value;
        if (RecordExistsHF.Value == "true")
        {
            sqlInsert = "  UPDATE [dbo].[PasswordAssignments] ";
            sqlInsert += " SET [UserPIN] = '" + UserPINTxt + "',[ResourceID] = '" + ResourceIDTxt + "',[EPassAssignmentDate] = GETDATE() ";
            sqlInsert += " ,[PasswordAssignedBy] = '" + PasswordAssignedByTxt + "',[UserName] = '" + UserNameTxt + "',[Password] = '" + PasswordTxt + "',[PasswordActive] = '" + PasswordStatus + "' where RequestID='" + reqID + "' ; ";
           
        }
        else
        {
            sqlInsert = "INSERT INTO [dbo].[PasswordAssignments]([UserPIN],[ResourceID],[EPassAssignmentDate],[PasswordAssignedBy],[UserName],[Password],[PasswordActive], [RequestID] , UserFirstName , UserLastName , UserEmail , UserOfficeCode)";
            sqlInsert += " Values  ('" + UserPINTxt + "', '" + ResourceIDTxt + "', GETDATE(), '" + PasswordAssignedByTxt + "', '" + UserNameTxt + "', '" + PasswordTxt + "', '" + PasswordStatus + "' , '" + reqID + "' ,  '" + UserFirstName + "', '" + UserLastName + "', '" + UserEmail + "', '" + UserOfficeCode + "' )";
        }

       // DataBase.executeCommand("Update AccessToResourceForm Set FormStatus ='" + formStatus + "' , InternalNotes='" + InternalNotes.Text + "'  where ID='" + RequestID.Value.Trim() + "' ;" + sqlInsert, "Admin.DbConnection");
        string sqll = "Update AccessToResourceForm Set FormStatus ='" + formStatus + "' , InternalNotes=@InternalNotesTxt  where ID='" + RequestID.Value.Trim() + "' ;" + sqlInsert;

        SqlCommand sqlcmd = new SqlCommand(sqll);
        sqlcmd.Parameters.AddWithValue("@InternalNotesTxt", InternalNotes.Text);
        DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmd);

        AuditLogs.log_Changes(reqID, "AccessToResourceForm");
        AuditLogs.log_Changes(AssignmentID.Value.Trim(), "PasswordAssignments");
    }


    protected void Cancel_Click(object sender, EventArgs e)
    {
        showwhat = Request.QueryString["show"].ToString();
        Response.Redirect("/admin/requests/default.aspx?show=" + showwhat);
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


    public string PasswordActiveTxt { get; set; }

    public string PasswordTxt { get; set; }

    public string PasswordAssignedByTxt { get; set; }

    public string ResourceIDTxt { get; set; }

    public string UserPINTxt { get; set; }

    public string UsernameTxt { get; set; }

    public string UserNameTxt { get; set; }



    public string SendRequestApprovedEmailMessage { get; set; }

    public string approvedMessage { get; set; }

    public string ModifiedDateTime { get; set; }

    public string ModifiedByPIN { get; set; }

    public string showwhat { get; set; }
}