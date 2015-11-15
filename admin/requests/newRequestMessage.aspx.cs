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
        string reqID = Request.QueryString["reqid"].ToString();
        AuditLogUX.tableName = "AccessToResourceForm";
        AuditLogUX.tableName2 = "";
        AuditLogUX.ForeignColumnName = "";
        AuditLogUX.CHID = reqID;


        if (!IsPostBack)
        {
           
             
            RequestID.Value = reqID;

            DataSet dt = new DataSet();
            dt = DataBase.dbDataSet("Select * from AccessToResourceForm where ID = '" + reqID + "';");

            SubmittedByPINDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByPIN").Trim();
            SubmittedByLastNameDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByLastName").Trim();
            SubmittedByFirstNameDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByFirstName").Trim();
            SubmittedByEMailDB = dt.Tables[0].Rows[0].Field<string>("SubmittedByEMail").Trim();
            

            SubmittedForPINDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForPIN").Trim();
            SubmittedForLastNameDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForLastName").Trim();
            SubmittedForFirstNameDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForFirstName").Trim();
            SubmittedForEMailDB = dt.Tables[0].Rows[0].Field<string>("SubmittedForEMail").Trim();
            
            // string SubmissionDateandTime =  dt.Tables[0].Rows[0].Field<string>("SubmissionDateandTime").Trim();
            resourceID = dt.Tables[0].Rows[0].Field<string>("ResourceToAccess").Trim();

            resourceName = DataBase.returnOneValue("Select ResourceName from Resources where ID='" + resourceID + "'", "Admin.DbConnection");
            ResourceNameHF.Value = resourceName;

            //if (SubmittedForPINDB == "None" || SubmittedForPINDB == "")
            //{
            //    RequestorPIN.Value = SubmittedByPINDB;
            //    UseThisInfo_PIN = SubmittedByPINDB;
            //    UseThisInfo_Name = SubmittedByLastNameDB + " " + SubmittedByFirstNameDB;
 
            //    UseThisInfo_Email = SubmittedByEMailDB;
                
            //}
            //else
            //{
            //    RequestorPIN.Value = SubmittedForPINDB;
            //    UseThisInfo_PIN = SubmittedForPINDB;
            //    UseThisInfo_Name = SubmittedForLastNameDB + " " + SubmittedForFirstNameDB;
            //    UseThisInfo_Email = SubmittedForEMailDB;
               
            //}
            Dictionary<string, string> RequestorDetails = loginSSA.GetUsersDetails(SubmittedForPINDB);
            RequestorEmailHF.Value = RequestorDetails["Email"];
            //RequestorPIN.Value =    RequestorDetails["PIN"];
            RequestorNameHF.Value = RequestorDetails["DisplayName"];

           

            
            ResourceIDHF.Value = resourceID;

            requestorName = RequestorDetails["DisplayName"];


            DataSet dtAR = DataBase.dbDataSet("Select * from  AutoReplies where Form = 'ePass';");
            // Response.Write("Select * from PasswordAssignments where ResourceID = '" + resourceID + "' and UserPIN = '" + UseThisInfo_PIN + "' ");
            if (dtAR.Tables[0].Rows.Count == 1)
            {


                SendRequestReceivedEmailMessage = dtAR.Tables[0].Rows[0].Field<string>("SendRequestReceivedEmailMessage").Trim();
                RequestReceivedEmailMessageSubject = dtAR.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageSubject").Trim();
                RequestReceivedEmailMessageText = dtAR.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageText").Trim();
                RequestReceivedEmailMessageFromAddress = dtAR.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageFromAddress").Trim();
                Subject.Text = RequestReceivedEmailMessageSubject;
                BodyText.Text =   WebUtility.HtmlDecode(RequestReceivedEmailMessageText);
                fromEmail.Value = RequestReceivedEmailMessageFromAddress;
            }


            if (RequestReceivedEmailMessageFromAddress == "false")
            {
                 Response.Redirect("/admin/requests/");
               // Response.Redirect("/admin/requests/declinedConfirm.aspx?reqname=" + UseThisInfo_Name + "&rsname=" + resourceName + "&reqid=" + reqID);
            }


        }




        




    }
     

    protected void ApproveAccess_Click(object sender, EventArgs e)
    {
        
        
        Response.Redirect("/admin/requests/confirm.aspx?panel=approved&reqname=" + RequestorNameHF.Value + "&rsname=" + ResourceNameHF.Value);
    }


    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/requests/default.aspx");
    }
    private void saveRecord(string FormStatus)
    {

        string sql = "Update AccessToResourceForm Set FormStatus ='" + FormStatus + "'   where ID='" + RequestID.Value.Trim() + "'";
        DataBase.executeCommand(sql);
        AuditLogs.log_Changes(RequestID.Value.Trim() , "AccessToResourceForm");
    }

    protected void SendBtn_Click(object sender, EventArgs e)
    {
        //  Declined – closed
        saveRecord("Open");
        //DataBase.executeCommand("Update AccessToResourceForm Set FormStatus ='Open'   where ID='" + RequestID.Value.Trim() + "'");
        //AuditLogs.log_AccessToResourceFormChanges(RequestID.Value.Trim());
        commonfunctions.sendEmailMessageWithAttachment(RequestorEmailHF.Value, fromEmail.Value, Subject.Text, BodyText.Text, attachment);

        Response.Redirect("/admin/requests/confirm.aspx?panel=newrequest&reqname=" + RequestorNameHF.Value + "&rsname=" + ResourceNameHF.Value);
    }
    protected void SkipMessage_Click(object sender, EventArgs e)
    {
        saveRecord("Open");
        //DataBase.executeCommand("Update AccessToResourceForm Set FormStatus ='Open'   where ID='" + RequestID.Value.Trim() + "'");
      //  Response.Redirect("/admin/requests/confirm.aspx");
        Response.Redirect("/admin/requests/confirm.aspx?panel=newrequestskipmessage&reqname=" + RequestorNameHF.Value + "&rsname=" + ResourceNameHF.Value);
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


    public string SendRequestReceivedEmailMessage { get; set; }

    public string RequestReceivedEmailMessageSubject { get; set; }

    public string RequestReceivedEmailMessageText { get; set; }

    public string RequestReceivedEmailMessageFromAddress { get; set; }
   
}