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

            string type = Request.QueryString["type"].ToString();
            reqID = Request.QueryString["reqid"].ToString();
            FormID = Request.QueryString["formId"].ToString();
            requestName = Request.QueryString["requestName"].ToString();
            requestorName = Request.QueryString["requestorName"].ToString();
            RequestID.Value = reqID;
            try
            {
                RequestorEmailHF.Value = Request.QueryString["email"].ToString();
            }
            catch { }

            DataSet dtAR = DataBase.dbDataSet("Select * from  AutoReplies where ID = '" + FormID + "';", "Admin.DbConnection");
            // Response.Write("Select * from PasswordAssignments where ResourceID = '" + resourceID + "' and UserPIN = '" + UseThisInfo_PIN + "' ");
            if (dtAR.Tables[0].Rows.Count == 1)
            {
                switch (type)
                {

                    case "open":
                        WhatMessage = "Opened";
                        SendRequestReceivedEmailMessage = dtAR.Tables[0].Rows[0].Field<string>("SendRequestReceivedEmailMessage").Trim();

                        if (SendRequestReceivedEmailMessage == "false")
                        {

                            string generalSQL = " FormStatus='Open' ";
                            saveRecord(generalSQL);
                            Response.Redirect("/admin/helprequests/confirm.aspx?panel=declined&reqname=" + requestName + "&Requestor=" + requestorName);

                        }



                        RequestReceivedEmailMessageSubject = dtAR.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageSubject").Trim();
                        RequestReceivedEmailMessageText = WebUtility.HtmlDecode(dtAR.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageText").Trim());
                        RequestReceivedEmailMessageFromAddress = dtAR.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageFromAddress").Trim();
                        Subject.Text = RequestReceivedEmailMessageSubject;
                        BodyText.Text = RequestReceivedEmailMessageText;
                        SaveForLater.Visible = false;
                        SkipMessage.Visible = true;


                        break;

                    case "close":
                        WhatMessage = "Closed";
                        SendRequestClosedEmailMessage = dtAR.Tables[0].Rows[0].Field<string>("SendRequestClosedEmailMessage").Trim();

                        if (SendRequestClosedEmailMessage == "false")
                        {

                            string generalSQL = " FormStatus='Closed' ";
                            saveRecord(generalSQL);
                            Response.Redirect("/admin/helprequests/confirm.aspx?panel=declined&reqname=" + requestName + "&Requestor=" + requestorName);

                        }



                        RequestClosedEmailMessageSubject = dtAR.Tables[0].Rows[0].Field<string>("RequestClosedEmailMessageSubject").Trim();
                        RequestClosedEmailMessageText = WebUtility.HtmlDecode(dtAR.Tables[0].Rows[0].Field<string>("RequestClosedEmailMessageText").Trim());
                        RequestClosedEmailMessageFromAddress = dtAR.Tables[0].Rows[0].Field<string>("RequestClosedEmailMessageFromAddress").Trim();
                        fromEmail.Value = RequestClosedEmailMessageFromAddress;
                        Subject.Text = RequestClosedEmailMessageSubject;
                        BodyText.Text = RequestClosedEmailMessageText;
                        break;



                }
            }




        }

    }



    private void saveRecord(string generalSQL)
    {
        reqID = Request.QueryString["reqid"].ToString();
        FormID = Request.QueryString["formId"].ToString();
        requestName = Request.QueryString["requestName"].ToString();
        requestorName = Request.QueryString["requestorName"].ToString();
        string sql = string.Empty;
        string tableName = string.Empty;
        switch (FormID)
        {
            case "1":  //Research Assistance   
                sql = "UPDATE ResearchAssistanceForm SET  " + generalSQL + " where id=" + reqID;
                tableName = "ResearchAssistanceForm";


                break;

            case "2": //Password Assistance           

                sql = "UPDATE PasswordAssistanceForm SET   " + generalSQL + " where id=" + reqID;
                tableName = "PasswordAssistanceForm";

                break;

            case "3": //Request for an Article        

                sql = "UPDATE RequestForAnArticle SET  " + generalSQL + " where id=" + reqID;
                tableName = "RequestForAnArticle";
                break;

            case "4": //Suggesting a New Resource     

                sql = "UPDATE SuggestingNewResourceForm SET  " + generalSQL + " where id=" + reqID;
                tableName = "SuggestingNewResourceForm";

                break;

            case "5": //Training Request              

                sql = "UPDATE TrainingRequestForm SET " + generalSQL + " where id=" + reqID;
                tableName = "TrainingRequestForm";
                break;

            case "6":  //Reporting a problem        

                sql = "UPDATE ReportingProblemForm SET  " + generalSQL + " where id=" + reqID;
                tableName = "ReportingProblemForm";
                break;

            case "7":  //Other                         

                sql = "UPDATE OtherForm SET  " + generalSQL + " where id=" + reqID;
                tableName = "OtherForm";
                break;

        }
        // Response.Write(sql);

        DataBase.executeCommand(sql);
        AuditLogs.log_Changes(RequestID.Value, tableName);
        //   Response.Redirect("/admin/requests/confirm.aspx?panel=declined&reqname=" + UseThisInfo_Name + "&rsname=" + resourceName + "&reqid=" + reqID);

    }




    protected void SkipMessage_Click(object sender, EventArgs e)
    {
        string generalSQL = " FormStatus='Open' ";
        saveRecord(generalSQL);
        Response.Redirect("/admin/helprequests/default.aspx");
    }


    protected void SaveForLater_Click1(object sender, EventArgs e)
    {
        string generalSQL = " FormStatus='Resolved-Not-Notified' ";
        saveRecord(generalSQL);
        Response.Write("<script>alert('Please Note That the Attachment Was Not Saved!'); location.href='/admin/helprequests/default.aspx'</script>");
        // Response.Redirect("/admin/helprequests/default.aspx");
    }


    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/helprequests/default.aspx");
    }

    protected void SendBtn_Click(object sender, EventArgs e)
    {
        requestName = Request.QueryString["requestName"].ToString();
        requestorName = Request.QueryString["requestorName"].ToString();
        //  Declined – closed
        string generalSQL =   " FormStatus='Closed' ";
        saveRecord(generalSQL);
        //   DataBase.executeCommand("Update AccessToResourceForm Set FormStatus ='Declined-Closed'   where ID='" + RequestID.Value.Trim() + "'", "Admin.DbConnection");
        commonfunctions.sendEmailMessageWithAttachment(RequestorEmailHF.Value, fromEmail.Value, Subject.Text, BodyText.Text, attachment);

        Response.Redirect("/admin/helprequests/confirm.aspx?type=" + Request.QueryString["type"].ToString() + "&panel=success&reqname=" + requestName + "&Requestor=" + requestorName);
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


    public string reqID { get; set; }

    public string FormID { get; set; }

    public string SendRequestClosedEmailMessage { get; set; }

    public string RequestClosedEmailMessageSubject { get; set; }

    public string RequestClosedEmailMessageText { get; set; }

    public string RequestClosedEmailMessageFromAddress { get; set; }

    public string requestName { get; set; }


    public string SendRequestReceivedEmailMessage { get; set; }

    public string RequestReceivedEmailMessageText { get; set; }

    public string RequestReceivedEmailMessageSubject { get; set; }

    public string RequestReceivedEmailMessageFromAddress { get; set; }

    public string WhatMessage { get; set; }

}