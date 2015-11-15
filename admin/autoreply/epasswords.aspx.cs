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
using System.Configuration;

public partial class admin_users_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DataSet dt = new DataSet();
            dt = DataBase.dbDataSet("Select * from  AutoReplies where Form = 'ePass';");
            
             Form = dt.Tables[0].Rows[0].Field<string>("Form").Trim();
            //New Request Messages
            SendRequestReceivedEmailMessage = dt.Tables[0].Rows[0].Field<string>("SendRequestReceivedEmailMessage").Trim();
            RequestReceivedEmailMessageSubject = dt.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageSubject").Trim();
            RequestReceivedEmailMessageText = dt.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageText").Trim();
            RequestReceivedEmailMessageFromAddress = dt.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageFromAddress").Trim();

            //CLose Messages Messages
            SendRequestClosedEmailMessage = dt.Tables[0].Rows[0].Field<string>("SendRequestClosedEmailMessage").Trim();
            RequestClosedEmailMessageSubject = dt.Tables[0].Rows[0].Field<string>("RequestClosedEmailMessageSubject").Trim();
            RequestClosedEmailMessageText = dt.Tables[0].Rows[0].Field<string>("RequestClosedEmailMessageText").Trim();
            RequestClosedEmailMessageFromAddress =(string)dt.Tables[0].Rows[0].Field<string>("RequestClosedEmailMessageFromAddress").Trim();
            //Aproved email details
            SendRequestApprovedEmailMessage = dt.Tables[0].Rows[0].Field<string>("SendRequestApprovedEmailMessage").Trim();
            RequestApprovedEmailMessageSubject = dt.Tables[0].Rows[0].Field<string>("RequestApprovedEmailMessageSubject").Trim();
            RequestApprovedEmailMessageText = dt.Tables[0].Rows[0].Field<string>("RequestApprovedEmailMessageText").Trim();
            RequestApprovedEmailMessageFromAddress = dt.Tables[0].Rows[0].Field<string>("RequestApprovedEmailMessageFromAddress").Trim();
            //Declined email details
            SendRequestDeclinedEmailMessage = dt.Tables[0].Rows[0].Field<string>("SendRequestDeclinedEmailMessage").Trim();
            RequestDeclinedEmailMessageSubject = dt.Tables[0].Rows[0].Field<string>("RequestDeclinedEmailMessageSubject").Trim();
            RequestDeclinedEmailMessageText = dt.Tables[0].Rows[0].Field<string>("RequestDeclinedEmailMessageText").Trim();
            RequestDeclinedEmailMessageFromAddress = dt.Tables[0].Rows[0].Field<string>("RequestDeclinedEmailMessageFromAddress").Trim();


            SubjectRR.Text = RequestApprovedEmailMessageSubject;
            BodyTextRR.Text = WebUtility.HtmlDecode(RequestApprovedEmailMessageText);
            fromEmailRR.Text = RequestApprovedEmailMessageFromAddress;
            if (SendRequestApprovedEmailMessage == "true")
            {
                TurnOnRRMessages.Checked = true;
            }
            else
            {
                TurnOnRRMessages.Checked = false;
            }

            SubjectRC.Text = RequestDeclinedEmailMessageSubject;
            BodyTextRC.Text = WebUtility.HtmlDecode(RequestDeclinedEmailMessageText);
            fromEmailRC.Text = RequestDeclinedEmailMessageFromAddress;
            if (SendRequestDeclinedEmailMessage == "true")
            {
                TurnOnRCMessages.Checked = true;
            }
            else
            {
                TurnOnRCMessages.Checked = false;
            }



            fromEmailNR.Text = RequestReceivedEmailMessageFromAddress;
            SubjectNR.Text = RequestReceivedEmailMessageSubject;
            BodyTextNR.Text = WebUtility.HtmlDecode(RequestReceivedEmailMessageText);
            if (SendRequestReceivedEmailMessage == "true")
            {
                TurnOnNR.Checked = true;
            }
            else
            {
                TurnOnNR.Checked = false;
            }

            fromEmailCRR.Text = RequestClosedEmailMessageFromAddress;
            SubjectCRR.Text = RequestClosedEmailMessageSubject;
            BodyTextCRR.Text = WebUtility.HtmlDecode(RequestClosedEmailMessageText);
            if (SendRequestClosedEmailMessage == "true")
            {
                TurnOnCRR.Checked = true;
            }
            else
            {
                TurnOnCRR.Checked = false;
            }

            AuditLogUX.tableName = "AutoReplies";
            AuditLogUX.CHID = "8";
            AuditLogUX.tableName2 = "";

        }

    }


    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        SendRequestApprovedEmailMessage = SendRequestDeclinedEmailMessage = "false";
        if (TurnOnRRMessages.Checked == true) SendRequestApprovedEmailMessage = "true";
        if (TurnOnRCMessages.Checked == true) SendRequestDeclinedEmailMessage = "true";
        if (TurnOnCRR.Checked == true) SendRequestClosedEmailMessage = "true";
        if (TurnOnNR.Checked == true) SendRequestReceivedEmailMessage = "true";

        RequestApprovedEmailMessageSubject = SubjectRR.Text;
        RequestApprovedEmailMessageText = WebUtility.HtmlEncode(BodyTextRR.Text);
        RequestApprovedEmailMessageFromAddress = fromEmailRR.Text;

        RequestDeclinedEmailMessageSubject = SubjectRC.Text;
        RequestDeclinedEmailMessageText = WebUtility.HtmlEncode(BodyTextRC.Text);
        RequestDeclinedEmailMessageFromAddress = fromEmailRC.Text;

        RequestReceivedEmailMessageFromAddress = fromEmailNR.Text;
        RequestReceivedEmailMessageSubject = SubjectNR.Text;
        RequestReceivedEmailMessageText = WebUtility.HtmlEncode(BodyTextNR.Text);  

        RequestClosedEmailMessageFromAddress = fromEmailCRR.Text;
        RequestClosedEmailMessageSubject = SubjectCRR.Text;
        RequestClosedEmailMessageText = WebUtility.HtmlEncode(BodyTextCRR.Text);   


        //sqlUpdate = "  UPDATE [dbo].[AutoReplies] ";
        //sqlUpdate += " SET [SendRequestApprovedEmailMessage] = '" + SendRequestApprovedEmailMessage + "',[RequestApprovedEmailMessageSubject] = '" + RequestApprovedEmailMessageSubject + "',[RequestApprovedEmailMessageText] = '" + RequestApprovedEmailMessageText + "' ";
        //sqlUpdate += ",[SendRequestDeclinedEmailMessage] = '" + SendRequestDeclinedEmailMessage + "',[RequestApprovedEmailMessageFromAddress] = '" + RequestApprovedEmailMessageFromAddress + "',[RequestDeclinedEmailMessageSubject] = '" + RequestDeclinedEmailMessageSubject + "',";
        //sqlUpdate += "  [RequestDeclinedEmailMessageText] = '" + RequestDeclinedEmailMessageText + "',[RequestDeclinedEmailMessageFromAddress] = '" + RequestDeclinedEmailMessageFromAddress + "' where Form='ePass' ; ";

        string sqlUPdate = "UPDATE [dbo].[AutoReplies]  SET ";
        sqlUPdate += " [SendRequestReceivedEmailMessage] = @SendRequestReceivedEmailMessage ";  //char(10),>
        sqlUPdate += " ,[RequestReceivedEmailMessageSubject] = @RequestReceivedEmailMessageSubject ";  //char(150),>
        sqlUPdate += " ,[RequestReceivedEmailMessageText] = @RequestReceivedEmailMessageText ";  //ext,>
        sqlUPdate += " ,[RequestReceivedEmailMessageFromAddress] = @RequestReceivedEmailMessageFromAddress ";  //char(100),>
        sqlUPdate += " ,[SendRequestClosedEmailMessage] = @SendRequestClosedEmailMessage ";  //ext,>
        sqlUPdate += " ,[RequestClosedEmailMessageSubject] = @RequestClosedEmailMessageSubject ";  //char(150),>
        sqlUPdate += " ,[RequestClosedEmailMessageText] = @RequestClosedEmailMessageText ";  //ext,>
        sqlUPdate += " ,[RequestClosedEmailMessageFromAddress] = @RequestClosedEmailMessageFromAddress ";  //ext,>
        sqlUPdate += " ,[SendRequestApprovedEmailMessage] = @SendRequestApprovedEmailMessage ";  //ext,>
        sqlUPdate += " ,[RequestApprovedEmailMessageSubject] = @RequestApprovedEmailMessageSubject ";  //char(150),>
        sqlUPdate += " ,[RequestApprovedEmailMessageText] = @RequestApprovedEmailMessageText ";  //ext,>
        sqlUPdate += " ,[RequestApprovedEmailMessageFromAddress] = @RequestApprovedEmailMessageFromAddress ";  //ext,>
        sqlUPdate += " ,[SendRequestDeclinedEmailMessage] = @SendRequestDeclinedEmailMessage ";  //char(10),>
        sqlUPdate += " ,[RequestDeclinedEmailMessageSubject] = @RequestDeclinedEmailMessageSubject ";  //char(150),>
        sqlUPdate += " ,[RequestDeclinedEmailMessageText] = @RequestDeclinedEmailMessageText ";  //ext,>
        sqlUPdate += " ,[RequestDeclinedEmailMessageFromAddress] = @RequestDeclinedEmailMessageFromAddress ";  //ext,>
        sqlUPdate += " where ID='8' ; ";
        SqlCommand cmd = new SqlCommand(sqlUPdate);
        cmd.Parameters.AddWithValue("@SendRequestReceivedEmailMessage", SendRequestReceivedEmailMessage);  //char(10),>
        cmd.Parameters.AddWithValue("@RequestReceivedEmailMessageSubject", RequestReceivedEmailMessageSubject);  //char(150),>
        cmd.Parameters.AddWithValue("@RequestReceivedEmailMessageText", RequestReceivedEmailMessageText);  //ext,>
        cmd.Parameters.AddWithValue("@RequestReceivedEmailMessageFromAddress", RequestReceivedEmailMessageFromAddress);  //char(100),>
        cmd.Parameters.AddWithValue("@SendRequestClosedEmailMessage", SendRequestClosedEmailMessage);  //ext,>
        cmd.Parameters.AddWithValue("@RequestClosedEmailMessageSubject", RequestClosedEmailMessageSubject);  //char(150),>
        cmd.Parameters.AddWithValue("@RequestClosedEmailMessageText", RequestClosedEmailMessageText);  //ext,>
        cmd.Parameters.AddWithValue("@RequestClosedEmailMessageFromAddress", RequestClosedEmailMessageFromAddress);  //ext,>
        cmd.Parameters.AddWithValue("@SendRequestApprovedEmailMessage", SendRequestApprovedEmailMessage);  //ext,>
        cmd.Parameters.AddWithValue("@RequestApprovedEmailMessageSubject", RequestApprovedEmailMessageSubject);  //char(150),>
        cmd.Parameters.AddWithValue("@RequestApprovedEmailMessageText", RequestApprovedEmailMessageText);  //ext,>
        cmd.Parameters.AddWithValue("@RequestApprovedEmailMessageFromAddress", RequestApprovedEmailMessageFromAddress);  //ext,>
        cmd.Parameters.AddWithValue("@SendRequestDeclinedEmailMessage", SendRequestDeclinedEmailMessage);  //char(10),>
        cmd.Parameters.AddWithValue("@RequestDeclinedEmailMessageSubject", RequestDeclinedEmailMessageSubject);  //char(150),>
        cmd.Parameters.AddWithValue("@RequestDeclinedEmailMessageText", RequestDeclinedEmailMessageText);  //ext,>
        cmd.Parameters.AddWithValue("@RequestDeclinedEmailMessageFromAddress", RequestDeclinedEmailMessageFromAddress);  //ext,>

        DataBase.executeCommandWithParameters(cmd);
        AuditLogs.log_Changes("8", "AutoReplies");

    }


    
    protected void CancelBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/autoreply/");
    }
    public string Form { get; set; }
    public string SendRequestReceivedEmailMessage { get; set; }
    public string RequestReceivedEmailMessageSubject { get; set; }
    public string RequestReceivedEmailMessageText { get; set; }
    public string RequestReceivedEmailMessageFromAddress { get; set; }
    public string SendRequestClosedEmailMessage { get; set; }
    public string RequestClosedEmailMessageSubject { get; set; }
    public string RequestClosedEmailMessageText { get; set; }
    public string RequestClosedEmailMessageFromAddress { get; set; }
    public string SendRequestApprovedEmailMessage { get; set; }
    public string RequestApprovedEmailMessageSubject { get; set; }
    public string RequestApprovedEmailMessageText { get; set; }
    public string RequestApprovedEmailMessageFromAddress { get; set; }
    public string SendRequestDeclinedEmailMessage { get; set; }
    public string RequestDeclinedEmailMessageSubject { get; set; }
    public string RequestDeclinedEmailMessageText { get; set; }
    public string RequestDeclinedEmailMessageFromAddress { get; set; }



    public string sqlUpdate { get; set; }
}