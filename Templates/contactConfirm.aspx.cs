using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
public partial class Templates_Content : System.Web.UI.Page
{
    /// <summary>
    /// Page Init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        // uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();
        // uxPageTitle.ResourceTypeId = "2";

        //SubmittedByPIN = (string)HttpContext.Current.Request.Cookies["Member"]["PIN"];
        //SubmittedByLastName = (string)HttpContext.Current.Request.Cookies["Member"]["LastName"];
        //SubmittedByFirstName = (string)HttpContext.Current.Request.Cookies["Member"]["FirstName"];
        //SubmittedByEMail = (string)HttpContext.Current.Request.Cookies["Member"]["EMail"];
        //SubmittedByOffice = (string)HttpContext.Current.Request.Cookies["Member"]["Office"];
        //SubmittedByServer = (string)HttpContext.Current.Request.Cookies["Member"]["Server"];
        //SubmittedByUserDomain = (string)HttpContext.Current.Request.Cookies["Member"]["UserDomain"];


        SubmittedByPIN = loginSSA.myPIN;
        SubmittedByLastName = loginSSA.myLastName;
        SubmittedByFirstName = loginSSA.myFirstName;
        SubmittedByEMail = loginSSA.myEmail;
        SubmittedByOffice = loginSSA.myOffice;
        SubmittedByServer = loginSSA.myServer;
        SubmittedByUserDomain = loginSSA.myUserDomain;


        //if (!Page.IsPostBack)
        //{
        //    if (!string.IsNullOrEmpty(Request.QueryString["id"]))
        //    {
        //        long contentId = long.Parse(Request.QueryString["id"]);
        //        //    this.GetContentData(contentId);
        //    }
        //}
        //        EktFormId
        //EktFormTitle
        //     string[] noshow = { "EktronClientManager", "__VIEWSTATE", "__VIEWSTATEGENERATOR", "__EVENTVALIDATION", "xml", "__ekFormId_formBlk", "__ekFormState_formBlk", "ApplicationAPI" + Request.QueryString["formID"], "EktFormPublishDate", "EktFormLang", "EktFormDescription", "submit" };
        //   string xmlData = string.Empty;
        if ((Request.Form != null))
        {
            // Response.Write(Request.Form);

            SubmittedForPIN = Request.Form["SubmittedForPIN"];
            SubmittedForLastName = Request.Form["SubmittedForLastName"];
            SubmittedForFirstName = Request.Form["SubmittedForFirstName"];
            SubmittedForEMail = Request.Form["SubmittedForEMail"];
            SubmittedForOffice = Request.Form["SubmittedForOffice"];
            SubmittedForServer = Request.Form["SubmittedForServer"];
            SubmittedForUserDomain = Request.Form["SubmittedForUserDomain"];

            FormStatus = "New";

            long formID = long.Parse(Request.Form["EktFormId"].ToString());
            string formDBID = "";
            ///select which table to insert into
            ///
            switch (formID)
            {
                case 111: //trainingrequest
                    trainingrequestSaveDB();
                    formDBID = "5";
                    break;
                case 112: //Reporting a Problem
                    ReportingaProblemSaveDB();
                    formDBID = "6";
                    break;

                case 105:  ///////Research Assistance
                    ResearchAssistanceSaveDB();
                    formDBID = "1";
                    break;


                case 106:  ///////Database Resource Access
                    ResourceAccessDatabaseSaveDB();
                    formDBID = "8";
                    break;
                case 108:  ///////Password Assistance
                    PasswordAssistanceSaveDB();
                    formDBID = "2";
                    break;

                case 110: ///////////Suggesting a New Resource
                    SuggestingaNewResource();
                    formDBID = "4";
                    break;

                case 109: ///////////Request for an Article
                    RequestforanArticle();
                    formDBID = "3";
                    break;

                default:
                    OthersSaveDB();
                    formDBID = "7";
                    break;

            }
            sendEmail(formDBID);
        }
        
           // ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MOVEHERE';", true);
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MOVEHERE';", true);
       

    }
    private void sendEmail(string formDBID)
    {    
        DataSet dt = new DataSet();
        dt = DataBase.dbDataSet("Select * from  AutoReplies where ID ='" + formDBID + "';");
 
        //New Request Messages
        string SendRequestReceivedEmailMessage = dt.Tables[0].Rows[0].Field<string>("SendRequestReceivedEmailMessage").Trim();
        string RequestReceivedEmailMessageSubject = dt.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageSubject").Trim();
        string RequestReceivedEmailMessageText = dt.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageText").Trim();
        string RequestReceivedEmailMessageFromAddress = dt.Tables[0].Rows[0].Field<string>("RequestReceivedEmailMessageFromAddress").Trim();
        if (commonfunctions.Environment == "DEV" || commonfunctions.Environment == "PROD")
        {
            if (SendRequestReceivedEmailMessage == "true")
            {
                commonfunctions.sendEmailMessage(loginSSA.myEmail, RequestReceivedEmailMessageFromAddress, RequestReceivedEmailMessageSubject, WebUtility.HtmlDecode(RequestReceivedEmailMessageText));
            }
        }
    }

    private void ResearchAssistanceSaveDB()
    {
        string Subject = Request.Form["SubjectArea"];
        string Resource = Request.Form["Resource"];
        string HowCanWeHelp = Request.Form["How_can_we_help"];


        string sql = "INSERT INTO [dbo].[ResearchAssistanceForm] ([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[Subject],[Resource],[HowCanWeHelp],[InternalNotes],[FormStatus])";
        sql += " VALUES ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),'" + Subject + "','" + Resource + "',@HowCanWeHelp,'" + InternalNotes + "','" + FormStatus + "')";
         //Response.Write(sql);
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@HowCanWeHelp", HowCanWeHelp);
        DataBase.executeCommandWithParameters(cmd);
       
    }



    private void OthersSaveDB()
    {
        string comments = Request.Form["Please_describe_your_question"];
        string sql = "INSERT INTO [dbo].[OtherForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[comments],[InternalNotes],[FormStatus]) VALUES";
        sql += "  ('" + SubmittedByPIN + "' ,'" + SubmittedByLastName + "' ,'" + SubmittedByFirstName + "' ,'" + SubmittedByEMail + "' ,'" + SubmittedByOffice + "' ,'" + SubmittedByServer + "' ,'" + SubmittedByUserDomain + "' ,'" + SubmittedForPIN + "' ,'" + SubmittedForLastName + "' ,'" + SubmittedForFirstName + "' ,'" + SubmittedForEMail + "' ,'" + SubmittedForOffice + "' ,'" + SubmittedForServer + "' ,'" + SubmittedForUserDomain + "' , GETDATE() ,@comments ,'" + InternalNotes + "' ,'" + FormStatus + "')";
        // Response.Write(sql);
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@comments", comments);
        DataBase.executeCommandWithParameters(cmd);
    }


    private void RequestforanArticle()
    {
        string ArticleTitleorKeyword = Request.Form["ArticleTitle_orKeyword"];
        string Authors = Request.Form["Authors"];
        string JournalTitle = Request.Form["JournalTitle"];
        string YearPublished = Request.Form["YearPublished"];
        string WhyDoYouNeedThisArticle = Request.Form["Why_do_you_need_this_article"];
        string IssueVolumePage = Request.Form["Issue_Volume_andPage"];

        string sql = "INSERT INTO [dbo].[RequestForAnArticle]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[ArticleTitleorKeyword],[Authors],[JournalTitle],[IssueVolumePage] ,[YearPublished],[WhyDoYouNeedThisArticle],[InternalNotes],[FormStatus]) values ";
        sql += "  ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),@ArticleTitleorKeyword,@Authors, @IssueVolumePage ,@JournalTitle ,@YearPublished ,@WhyDoYouNeedThisArticle,'" + InternalNotes + "','" + FormStatus + "') ";
        //  DataBase.executeCommand(sql);
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@ArticleTitleorKeyword", ArticleTitleorKeyword);
        cmd.Parameters.AddWithValue("@Authors", Authors);
        cmd.Parameters.AddWithValue("@JournalTitle", JournalTitle);
        cmd.Parameters.AddWithValue("@YearPublished", YearPublished);
        cmd.Parameters.AddWithValue("@WhyDoYouNeedThisArticle", WhyDoYouNeedThisArticle);
        cmd.Parameters.AddWithValue("@IssueVolumePage", IssueVolumePage);
        DataBase.executeCommandWithParameters(cmd);

        //  Response.Write(sql);
    }

    private void SuggestingaNewResource()
    {
        string NameOfResource = Request.Form["Name_of_resource"];
        string WhatIsTheBusinessNeedForThisResource = Request.Form["What_is_the_business_need_for_this_resource"];
        string AdditionalInformation = Request.Form["Additional_information"];
        string ApprovingSupervisor = Request.Form["ApprovingSupervisor"];
        string sql = "INSERT INTO [dbo].[SuggestingNewResourceForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[NameOfResource],[WhatIsTheBusinessNeedForThisResource],[AdditionalInformation],[ApprovingSupervisor],[InternalNotes],[FormStatus]) Values ";
        sql += "  ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),@NameOfResource,@WhatIsTheBusinessNeedForThisResource,@AdditionalInformation,@ApprovingSupervisor,'" + InternalNotes + "','" + FormStatus + "') ";
        //DataBase.executeCommand(sql);
        // Response.Write(sql);
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@NameOfResource", NameOfResource);
        cmd.Parameters.AddWithValue("@WhatIsTheBusinessNeedForThisResource", WhatIsTheBusinessNeedForThisResource);
        cmd.Parameters.AddWithValue("@AdditionalInformation", AdditionalInformation);
        cmd.Parameters.AddWithValue("@ApprovingSupervisor", ApprovingSupervisor);
        DataBase.executeCommandWithParameters(cmd);
    }

    private void PasswordAssistanceSaveDB()
    {
        string Resourceforwhichassistanceisneeded = Request.Form["Resource"];
        string Additionalnotesordetails = Request.Form["Additional_notes_or_details"];
        FormStatus = "New";
        string sql = "INSERT INTO [dbo].[PasswordAssistanceForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[Resourceforwhichassistanceisneeded],[Additionalnotesordetails],[InternalNotes],[FormStatus]) VALUES";
        sql += "  ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),'" + Resourceforwhichassistanceisneeded + "',@Additionalnotesordetails,'" + InternalNotes + "','" + FormStatus + "') ";


        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@Additionalnotesordetails", Additionalnotesordetails);
        DataBase.executeCommandWithParameters(cmd);

    }
    private void ResourceAccessDatabaseSaveDB()
    {
        string ResourceToAccess = Request.Form["Resource"];
        string WhyDoYouNeedAccess = Request.Form["Why_do_you_need_access"];
        FormStatus = "New";

        string sql = "INSERT INTO [dbo].[AccessToResourceForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[ResourceToAccess],[WhyDoYouNeedAccess],[InternalNotes],[FormStatus]) VALUES";
        sql += "  ('" + SubmittedByPIN + "' ,'" + SubmittedByLastName + "' ,'" + SubmittedByFirstName + "' ,'" + SubmittedByEMail + "' ,'" + SubmittedByOffice + "' ,'" + SubmittedByServer + "' ,'" + SubmittedByUserDomain + "' ,'" + SubmittedForPIN + "' ,'" + SubmittedForLastName + "' ,'" + SubmittedForFirstName + "' ,'" + SubmittedForEMail + "' ,'" + SubmittedForOffice + "' ,'" + SubmittedForServer + "' ,'" + SubmittedForUserDomain + "' , GETDATE() ,'" + ResourceToAccess + "' ,@WhyDoYouNeedAccess ,'" + InternalNotes + "' ,'" + FormStatus + "')";
        ////  Response.Write(sql);
        // DataBase.executeCommand(sql);

        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@WhyDoYouNeedAccess", WhyDoYouNeedAccess);
        DataBase.executeCommandWithParameters(cmd);

    }


    private void ReportingaProblemSaveDB()
    {
        string ProblemType = Request.Form["The_problem_is_with"];
        string Problemsource = Request.Form["pageTrouble"];
        string ProblemDescription = Request.Form["ProblemDescription"];
        if (Problemsource == "None")
        {
            Problemsource = Request.Form["ProblemResource"];
        }

        string Screenshotimage = string.Empty;
        string ScreenshotimageDB = string.Empty;
        /////////////////Upload files
        try
        {
            foreach (var a in Request.Files)
            {
                string path = commonfunctions.BaseDirectory + "\\uploadedimages\\ReportAProblemImages\\";
                //HttpPostedFile file = Request.Files[a.ToString()];
                //if (file != null && file.ContentLength > 0)
                //{
                //    Screenshotimage = DateTime.Now.ToString("ddmmyyyhhmmss") + file.FileName.ToString();
                //    file.SaveAs(path + Screenshotimage);
                //    // ScreenshotimageDB = path + Screenshotimage;
                //    //Screenshotimage
                //}



                HttpPostedFile file = Request.Files[a.ToString()];
                if (file != null && file.ContentLength > 0)
                {
                    // Response.Write(file.FileName.ToString());
                    // fileDocument = DateTime.Now.ToString("ddmmyyyhhmmss") + file.FileName.ToString();
                    Screenshotimage = DateTime.Now.ToString("ddmmyyyhhmmss") + Path.GetFileName(file.FileName); //.ToString();

                    file.SaveAs(Path.Combine(path, Screenshotimage));
                    //return path + fileDocument;
                 //   return pathURL + Screenshotimage;
                }


            }
        }
        catch { }



        string sql = "INSERT INTO [dbo].[ReportingProblemForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[ProblemType],[Problemsource],[ProblemDescription],[Screenshotimage],[InternalNotes],[FormStatus])";
        sql += " VALUES ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),'" + ProblemType + "','" + Problemsource + "',@ProblemDescription,'" + Screenshotimage + "','" + InternalNotes + "','" + FormStatus + "')";
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@ProblemDescription", ProblemDescription);
        DataBase.executeCommandWithParameters(cmd);
    }

    private void trainingrequestSaveDB()
    {
        string Subject = Request.Form["SubjectArea"];
        string Resource = Request.Form["Resource"];
        string NumberOfAttendees = Request.Form["Number_ofAttendees"];
        string RequestedDate = Request.Form["RequestedDate"];
        string RequestedTime = Request.Form["RequestedTime_ofDay"];
        string Location = Request.Form["Location"];
        string OtherInformation = Request.Form["OtherInformation"];

        string sql = "INSERT INTO [dbo].[TrainingRequestForm] ([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[Subject],[Resource],[NumberOfAttendees],[RequestedDate],[RequestedTime],[Location],[OtherInformation],[InternalNotes],[FormStatus])";
        sql += " VALUES ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),'" + Subject + "','" + Resource + "','" + NumberOfAttendees + "','" + RequestedDate + "','" + RequestedTime + "',@Location,@OtherInformation,'" + InternalNotes + "','" + FormStatus + "')";
        //  Response.Write(sql);
        SqlCommand cmd = new SqlCommand(sql);
        cmd.Parameters.AddWithValue("@Location", Location);
        cmd.Parameters.AddWithValue("@OtherInformation", OtherInformation);
        DataBase.executeCommandWithParameters(cmd);
    }




    public void saveFormintoDB(string xmlData, string formId)
    {

        xmlData = xmlData.Replace("'", "''");
        string sql = "insert into contactSubmission  ( [formID], xmlData , dateSubmitted) values ";
        sql += " ('" + formId + "' , '" + xmlData + "',  GETDATE())";

        //  try
        //      {
        //  Response.Write(sql);
        using (SqlConnection cnn = new SqlConnection(commonfunctions.ektronConnectionStr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = cnn;
                //get data

                cmd.CommandType = CommandType.Text;
                cnn.Open();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                //Response.Write("Registration Successfull");

            }
        }
    }


    public string SubmittedByPIN { get; set; }

    public string SubmittedByLastName { get; set; }

    public string SubmittedByFirstName { get; set; }

    public string SubmittedByEMail { get; set; }

    public string SubmittedByOffice { get; set; }

    public string SubmittedByUserDomain { get; set; }

    public string SubmittedByServer { get; set; }

    public string SubmittedForPIN { get; set; }

    public string SubmittedForLastName { get; set; }

    public string SubmittedForFirstName { get; set; }

    public string SubmittedForEMail { get; set; }

    public string SubmittedForOffice { get; set; }

    public string SubmittedForServer { get; set; }

    public string SubmittedForUserDomain { get; set; }

    public string InternalNotes { get; set; }

    public string FormStatus { get; set; }
}