using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;
using System.Net;

public partial class admin_users_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string reqID = Request.QueryString["reqid"].ToString();

            RequestID.Value = reqID;

            AuditLogUX.tableName = "AccessToResourceForm";
            AuditLogUX.tableName2 = "";
            AuditLogUX.ForeignColumnName = "";
            AuditLogUX.CHID = reqID;

            DataSet dt = new DataSet();
            dt = DataBase.dbDataSet("Select * from AccessToResourceForm where ID = '" + reqID + "';", "Admin.DbConnection");

          

            SubmittedForPINDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForPIN").Trim();
        

            // string SubmissionDateandTime =  dt.Tables[0].Rows[0].Field<string>("SubmissionDateandTime").Trim();
            resourceID = dt.Tables[0].Rows[0].Field<string>("ResourceToAccess").Trim();
       



         

            Dictionary<string, string> RequestorDetails = loginSSA.GetUsersDetails(SubmittedForPINDB);
            RequestorEmailHF.Value = RequestorDetails["Email"];
            //RequestorPIN.Value =    RequestorDetails["PIN"];
            RequestorNameHF.Value = RequestorDetails["DisplayName"];
            ResourceIDHF.Value = resourceID;

            requestorName = RequestorDetails["DisplayName"];

            resourceName = DataBase.returnOneValue("Select ResourceName from Resources where ID='" + resourceID + "'", "Admin.DbConnection");
            ResourceNameHF.Value = resourceName;
            //  Response.Write(resourceID);
            DataSet dtAR = DataBase.dbDataSet("Select * from  AutoReplies where Form = 'ePass';", "Admin.DbConnection");
            // Response.Write("Select * from PasswordAssignments where ResourceID = '" + resourceID + "' and UserPIN = '" + UseThisInfo_PIN + "' ");
            if (dtAR.Tables[0].Rows.Count == 1)
            {



                SendRequestDeclinedEmailMessage = dtAR.Tables[0].Rows[0].Field<string>("SendRequestDeclinedEmailMessage").Trim();
                RequestDeclinedEmailMessageSubject = dtAR.Tables[0].Rows[0].Field<string>("RequestDeclinedEmailMessageSubject").Trim();
                RequestDeclinedEmailMessageText = WebUtility.HtmlDecode(dtAR.Tables[0].Rows[0].Field<string>("RequestDeclinedEmailMessageText").Trim());
                RequestDeclinedEmailMessageFromAddress = dtAR.Tables[0].Rows[0].Field<string>("RequestDeclinedEmailMessageFromAddress").Trim();
                Subject.Text = RequestDeclinedEmailMessageSubject;
                BodyText.Text = RequestDeclinedEmailMessageText;
                fromEmail.Value = RequestDeclinedEmailMessageFromAddress;
            }


            if (SendRequestDeclinedEmailMessage == "false")
            {
                // Response.Redirect("/admin/requests/confirm.aspx?action=decline");
                DataBase.executeCommand("Update AccessToResourceForm Set FormStatus ='Declined-Closed'   where ID='" + reqID + "'", "Admin.DbConnection");
                AuditLogs.log_Changes(reqID.Trim(), "AccessToResourceForm");
                Response.Redirect("/admin/requests/confirm.aspx?panel=declined&reqname=" + RequestorDetails["DisplayName"] + "&rsname=" + resourceName + "&reqid=" + reqID);
            }


        }

    }

 
 
 

    

    protected void ApproveAccess_Click(object sender, EventArgs e)
    {
        
        
        Response.Redirect("/admin/requests/confirm.aspx?panel=approved&reqname=" + RequestorNameHF.Value + "&rsname=" + ResourceNameHF.Value);
    }


    protected void SaveForLater_Click(object sender, EventArgs e)
    {
         
     
        Response.Redirect("/admin/requests/declinedConfirm.aspx");
    }

     


    protected void Cancel_Click(object sender, EventArgs e)
    {
        showwhat = Request.QueryString["show"].ToString();
        Response.Redirect("/admin/requests/default.aspx?show=" + showwhat);
    }

    protected void SendBtn_Click(object sender, EventArgs e)
    {
        //  Declined – closed
        DataBase.executeCommand("Update AccessToResourceForm Set FormStatus ='Declined-Closed'   where ID='" + RequestID.Value.Trim() + "'", "Admin.DbConnection");
        commonfunctions.sendEmailMessageWithAttachment(RequestorEmailHF.Value, fromEmail.Value, Subject.Text, BodyText.Text, attachment);

         
        Response.Redirect("/admin/requests/confirm.aspx?panel=declined&reqname=" + RequestorNameHF.Value + "&rsname=" + ResourceNameHF.Value);
    }

   
    public DataTable resourcesDt { get; set; }

    public string SendRequestDeclinedEmailMessage { get; set; }
    public string RequestDeclinedEmailMessageSubject { get; set; }
    public string RequestDeclinedEmailMessageText { get; set; }
    public string RequestDeclinedEmailMessageFromAddress { get; set; }

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

    public string requestorName { get; set; }


    public string showwhat { get; set; }
}