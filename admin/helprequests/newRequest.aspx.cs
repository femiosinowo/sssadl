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
        // dtAllResources();

        if (!IsPostBack)
        {

            

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("FormName");

            dt.Rows.Add("", " - Select One - ");

            DataTableReader dtR = DataBase.dbDataTable("Select * from HelpRequestsAssignees ").CreateDataReader();
            while (dtR.Read())
            {
                dt.Rows.Add(dtR["ID"].ToString(), dtR["FormName"].ToString());
            }

            RequestTypeDD.DataSource = dt; // DataBase.dbDataTable("Select * from HelpRequestsAssignees ");
            RequestTypeDD.DataBind();




        }

    }

    protected void RequestTypeDD_SelectedIndexChanged(object sender, EventArgs e)
    {
        subjectAreadDropDownData = subjectAreadDropDown();
        resourceDropDownData = resourceDropDown();

        string FormID = RequestTypeDD.SelectedValue;
        switch (FormID)
        {
            case "1":  //Research Assistance           

                disableAllPanels();
                ResearchAssistanceFormPanel.Visible = true;

                break;

            case "2": //Password Assistance           

                disableAllPanels();
                PasswordAssistanceFormPanel.Visible = true;
                break;

            case "3": //Request for an Article        

                disableAllPanels();
                RequestForAnArticlePanel.Visible = true;
                break;

            case "4": //Suggesting a New Resource     

                disableAllPanels();
                SuggestingNewResourceFormPanel.Visible = true;
                break;

            case "5": //Training Request              

                disableAllPanels();
                TrainingRequestFormPanel.Visible = true;
                break;

            case "6":  //Reporting a problem        

                disableAllPanels();
                ReportingProblemFormPanel.Visible = true;
                break;

            case "7":  //Other                         

                disableAllPanels();
                OtherFormPanel.Visible = true;
                break;

        }
    }

    private void disableAllPanels()
    {
        RequestForAnArticlePanel.Visible = false;
        OtherFormPanel.Visible = false;
        ReportingProblemFormPanel.Visible = false;
        TrainingRequestFormPanel.Visible = false;
        PasswordAssistanceFormPanel.Visible = false;
        SuggestingNewResourceFormPanel.Visible = false;
        ResearchAssistanceFormPanel.Visible = false;
    }

    private void OtherForm()
    {
        OtherFormPanel.Visible = true;
    }

    private void ReportingProblemForm()
    {
        ReportingProblemFormPanel.Visible = true;





    }

    private void TrainingRequestForm()
    {
        TrainingRequestFormPanel.Visible = true;


    }

    private void SuggestingNewResourceForm()
    {
        SuggestingNewResourceFormPanel.Visible = true;



    }

    private void RequestForAnArticle()
    {
        RequestForAnArticlePanel.Visible = true;



    }

    private void PasswordAssistanceForm()
    {

        PasswordAssistanceFormPanel.Visible = true;
        //string ResourceforwhichassistanceisneededDB = dt.Tables[0].Rows[0].Field<string>("Resourceforwhichassistanceisneeded").Trim();
        //ResourceInfo.SelectThisResource = ResourceforwhichassistanceisneededDB;
        //ResourceInfo.LabelTitle = "For which Resource do you need password assistance?";
        //Additionalnotesordetails.Text = dt.Tables[0].Rows[0].Field<string>("Additionalnotesordetails").Trim();

        //AuditLogUX.tableName = "PasswordAssistanceForm";
        //AuditLogUX.CHID = dt.Tables[0].Rows[0].Field<int>("ID").ToString();
    }

    private void ResearchAssistanceForm()
    {
        ResearchAssistanceFormPanel.Visible = true;
        //string Subject = dt.Tables[0].Rows[0].Field<string>("Subject").Trim();
        //string Resource = dt.Tables[0].Rows[0].Field<string>("Resource").Trim();
        //HowCanWeHelp.Text = dt.Tables[0].Rows[0].Field<string>("HowCanWeHelp").Trim();

        //ResourceRA.SelectThisResource = Resource;
        //SubjectRA.SelectThisSubject = Subject;

    }




    protected void SaveForLater_Click(object sender, EventArgs e)
    {
        FormID = RequestTypeDD.SelectedValue;
        string requestName = RequestTypeDD.SelectedItem.Text;
        string requestorName = loginSSA.myLastName + " " + loginSSA.myFirstName;
        reqID = saveRecord("Open");

        // Response.Redirect("/admin/helprequests/default.aspx");
        Response.Redirect("/admin/helprequests/SendMessage.aspx?type=open&reqid=" + reqID + "&formId=" + FormID + "&requestName=" + requestName + "&requestorName=" + requestorName + "&email=" + loginSSA.myEmail);
    }



    protected void MarkAsClosedBtn_Click(object sender, EventArgs e)
    {
        FormID = RequestTypeDD.SelectedValue;
        string requestName = RequestTypeDD.SelectedItem.Text;
        string requestorName = loginSSA.myLastName + " " + loginSSA.myFirstName;
        reqID = saveRecord("Resolve-Not-Notified");
        Response.Redirect("/admin/helprequests/SendMessage.aspx?type=close&reqid=" + reqID + "&formId=" + FormID + "&requestName=" + requestName + "&requestorName=" + requestorName + "&email=" + loginSSA.myEmail);
    }

    private string saveRecord(string FormStatus)
    {
        SubmittedByPIN = loginSSA.myPIN;
        SubmittedByLastName = loginSSA.myLastName;
        SubmittedByFirstName = loginSSA.myFirstName;
        SubmittedByEMail = loginSSA.myEmail;
        SubmittedByOffice = loginSSA.myOffice;
        SubmittedByServer = loginSSA.myServer;
        SubmittedByUserDomain = loginSSA.myUserDomain;

        SubmittedForPIN = loginSSA.myPIN;
        SubmittedForLastName = loginSSA.myLastName;
        SubmittedForFirstName = loginSSA.myFirstName;
        SubmittedForEMail = loginSSA.myEmail;
        SubmittedForOffice = loginSSA.myOffice;
        SubmittedForServer = loginSSA.myServer;
        SubmittedForUserDomain = loginSSA.myUserDomain;

        string FormID = RequestTypeDD.SelectedValue;
        string sql = string.Empty;

        InternalNotesdb = InternalNotes.Text;
        string insertedID = "";
        switch (FormID)
        {
            case "1":  //Research Assistance           

                // DropDownList Subj = (DropDownList)SubjectRA.FindControl("subjectDD");
                // DropDownList Resc = (DropDownList)ResourceRA.FindControl("ResourceDD");
                Subjectdb = Request.Form["SubjectAreaData"]; //Subj.SelectedValue;
                Resourcedb = Request.Form["resourceData"]; // Resc.SelectedValue;

                HowCanWeHelpdb = HowCanWeHelp.Text;


                sql = "INSERT INTO [dbo].[ResearchAssistanceForm] ([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[Subject],[Resource],[HowCanWeHelp],[InternalNotes],[FormStatus],ModifiedDateTime)";
                sql += " VALUES ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),'" + Subjectdb + "','" + Resourcedb + "',@HowCanWeHelpdb,@InternalNotesdb,'" + FormStatus + "',GETDATE())";

                SqlCommand cmd1 = new SqlCommand(sql);
                cmd1.Parameters.AddWithValue("@HowCanWeHelpdb", HowCanWeHelpdb); //nchar(50),>
                cmd1.Parameters.AddWithValue("@InternalNotesdb", InternalNotesdb); //varchar(max),>
                insertedID = DataBase.executeCommandWithParametersReturnIDENTITY(cmd1);

                break;

            case "2": //Password Assistance           
                // DropDownList Resource22 = (DropDownList)ResourceInfo.FindControl("ResourceDD");
                string Resourceforwhichassistanceisneeded = Request.Form["resourceData"]; //Resource22.SelectedValue;

                // string Resourceforwhichassistanceisneeded = Request.Form["Resource"];
                string Additionalnotesordetailsdb = Additionalnotesordetails.Text;

                sql = "INSERT INTO [dbo].[PasswordAssistanceForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[Resourceforwhichassistanceisneeded],[Additionalnotesordetails],[InternalNotes],[FormStatus],ModifiedDateTime) VALUES";
                sql += "  ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),'" + Resourceforwhichassistanceisneeded + "',@Additionalnotesordetailsdb ,@InternalNotesdb ,'" + FormStatus + "',GETDATE()) ";

                SqlCommand cmd2 = new SqlCommand(sql);
                cmd2.Parameters.AddWithValue("@Additionalnotesordetailsdb", Additionalnotesordetailsdb); //nchar(50),>
                cmd2.Parameters.AddWithValue("@InternalNotesdb", InternalNotesdb); //varchar(max),>
                insertedID = DataBase.executeCommandWithParametersReturnIDENTITY(cmd2);

                break;

            case "3": //Request for an Article        

                sql = "INSERT INTO [dbo].[RequestForAnArticle]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[ArticleTitleorKeyword],[Authors],[JournalTitle],[IssueVolumePage] ,[YearPublished],[WhyDoYouNeedThisArticle],[InternalNotes],[FormStatus],ModifiedDateTime) values ";
                sql += "  ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),@ArticleTitleorKeyword,@Author, @IssueVolumePage ,@JournalTitle ,@YearPublished ,@WhyDoYouNeedThisArticle,@InternalNotesdb,'" + FormStatus + "',GETDATE()) ";

                SqlCommand cmd3 = new SqlCommand(sql);

                cmd3.Parameters.AddWithValue("@ArticleTitleorKeyword", ArticleTitleorKeyword.Text);
                cmd3.Parameters.AddWithValue("@Author", Author.Text);
                cmd3.Parameters.AddWithValue("@JournalTitle", JournalTitle.Text);
                cmd3.Parameters.AddWithValue("@YearPublished", YearPublished.Text);
                cmd3.Parameters.AddWithValue("@WhyDoYouNeedThisArticle", WhyDoYouNeedThisArticle.Text);
                cmd3.Parameters.AddWithValue("@IssueVolumePage", IssueVolumePage.Text);
                cmd3.Parameters.AddWithValue("@InternalNotesdb", InternalNotesdb); //varchar(max),>
                insertedID = DataBase.executeCommandWithParametersReturnIDENTITY(cmd3);
                break;

            case "4": //Suggesting a New Resource     
                sql = "INSERT INTO [dbo].[SuggestingNewResourceForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[NameOfResource],[WhatIsTheBusinessNeedForThisResource],[AdditionalInformation],[ApprovingSupervisor],[InternalNotes],[FormStatus],ModifiedDateTime) Values ";
                sql += "  ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),@NameOfResource ,@WhatIsTheBusinessNeedForThisResource ,@AdditionalInformation ,@ApprovingSupervisor ,@InternalNotesdb  ,'" + FormStatus + "',GETDATE()) ";

                SqlCommand cmd4 = new SqlCommand(sql);
                cmd4.Parameters.AddWithValue("@NameOfResource", NameOfResource.Text);
                cmd4.Parameters.AddWithValue("@WhatIsTheBusinessNeedForThisResource", WhatIsTheBusinessNeedForThisResource.Text);
                cmd4.Parameters.AddWithValue("@AdditionalInformation", AdditionalInformation.Text);
                cmd4.Parameters.AddWithValue("@ApprovingSupervisor", ApprovingSupervisor.Text);
                cmd4.Parameters.AddWithValue("@InternalNotesdb", InternalNotesdb);
                insertedID = DataBase.executeCommandWithParametersReturnIDENTITY(cmd4);
                break;

            case "5": //Training Request              

                //  DropDownList Subj5 = (DropDownList)SubjectTR.FindControl("subjectDD");
                // DropDownList Resc5 = (DropDownList)ResourceTR.FindControl("ResourceDD");
                string Subject5 = Request.Form["SubjectAreaData"]; //Subj5.SelectedValue;
                string Resource5 = Request.Form["resourceData"]; //Resc5.SelectedValue;

                sql = "INSERT INTO [dbo].[TrainingRequestForm] ([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[Subject],[Resource],[NumberOfAttendees],[RequestedDate],[RequestedTime],[Location],[OtherInformation],[InternalNotes],[FormStatus],ModifiedDateTime)";
                sql += " VALUES ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),'" + Subject5 + "','" + Resource5 + "',@NumberOfAttendees ,'" + RequestedDate.Text + "','" + RequestedTime.SelectedValue + "',@Location ,@OtherInformation ,@InternalNotesdb ,'" + FormStatus + "',GETDATE())";


                SqlCommand cmd5 = new SqlCommand(sql);
                cmd5.Parameters.AddWithValue("@NumberOfAttendees", NumberOfAttendees.Text);
                cmd5.Parameters.AddWithValue("@Location", Location.Text);
                cmd5.Parameters.AddWithValue("@OtherInformation", OtherInformation.Text);
                cmd5.Parameters.AddWithValue("@InternalNotesdb", InternalNotesdb);
                insertedID = DataBase.executeCommandWithParametersReturnIDENTITY(cmd5);
                break;


            case "6":  //Reporting a problem        



                string ppt = ProblemWith.SelectedValue;

                string pprbsource = "";
                if (ppt == "A Resource")
                {
                    //DropDownList Resc6 = (DropDownList)ResourceRP.FindControl("ResourceDD");
                    pprbsource = Request.Form["resourceData"]; //Resc6.SelectedValue;
                }
                else
                {
                    pprbsource = pageTrouble.Text;
                }


                string Screenshotimage = uploadResourceFiles();



                sql = "INSERT INTO [dbo].[ReportingProblemForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[ProblemType],[Problemsource],[ProblemDescription],[Screenshotimage],[InternalNotes],[FormStatus],ModifiedDateTime)";
                sql += " VALUES ('" + SubmittedByPIN + "','" + SubmittedByLastName + "','" + SubmittedByFirstName + "','" + SubmittedByEMail + "','" + SubmittedByOffice + "','" + SubmittedByServer + "','" + SubmittedByUserDomain + "','" + SubmittedForPIN + "','" + SubmittedForLastName + "','" + SubmittedForFirstName + "','" + SubmittedForEMail + "','" + SubmittedForOffice + "','" + SubmittedForServer + "','" + SubmittedForUserDomain + "',GETDATE(),'" + ProblemWith.SelectedValue + "','" + pprbsource + "',@ProblemDescription ,'" + Screenshotimage + "',@InternalNotesdb ,'" + FormStatus + "',GETDATE())";


                SqlCommand cmd6 = new SqlCommand(sql);
                cmd6.Parameters.AddWithValue("@ProblemDescription", ProblemDescription.Text);
                cmd6.Parameters.AddWithValue("@InternalNotesdb", InternalNotes.Text);
                insertedID = DataBase.executeCommandWithParametersReturnIDENTITY(cmd6);
                break;

            case "7":  //Other                         

                sql = "INSERT INTO [dbo].[OtherForm]([SubmittedByPIN],[SubmittedByLastName],[SubmittedByFirstName],[SubmittedByEMail],[SubmittedByOffice],[SubmittedByServer],[SubmittedByUserDomain],[SubmittedForPIN],[SubmittedForLastName],[SubmittedForFirstName],[SubmittedForEMail],[SubmittedForOffice],[SubmittedForServer],[SubmittedForUserDomain],[SubmissionDateandTime],[comments],[InternalNotes],[FormStatus],ModifiedDateTime) VALUES";
                sql += "  ('" + SubmittedByPIN + "' ,'" + SubmittedByLastName + "' ,'" + SubmittedByFirstName + "' ,'" + SubmittedByEMail + "' ,'" + SubmittedByOffice + "' ,'" + SubmittedByServer + "' ,'" + SubmittedByUserDomain + "' ,'" + SubmittedForPIN + "' ,'" + SubmittedForLastName + "' ,'" + SubmittedForFirstName + "' ,'" + SubmittedForEMail + "' ,'" + SubmittedForOffice + "' ,'" + SubmittedForServer + "' ,'" + SubmittedForUserDomain + "' , GETDATE() ,@comments ,@InternalNotesdb  ,'" + FormStatus + "',GETDATE())";

                SqlCommand cmd7 = new SqlCommand(sql);
                cmd7.Parameters.AddWithValue("@comments", comments.Text);
                cmd7.Parameters.AddWithValue("@InternalNotesdb", InternalNotes.Text);
                insertedID = DataBase.executeCommandWithParametersReturnIDENTITY(cmd7);

                break;

        }

        //  DataBase.executeCommand(sql);
        return insertedID; // DataBase.executeCommanAndReturnSCOPE_IDENTITY(sql).ToString();
    }



    public DataTable resourcesDt { get; set; }

    protected string uploadResourceFiles()
    {
        //try
        //{
        //    string path = commonfunctions.BaseDirectory + "\\uploadedimages\\ReportAProblemImages\\";
        //    string fileDocument = string.Empty;


        //    HttpPostedFile file = Request.Files[uploadfileID];
        //    if (file != null && file.ContentLength > 0)
        //    {
        //        fileDocument = DateTime.Now.ToString("ddmmyyyhhmmss") + file.FileName.ToString();
        //        file.SaveAs(path + fileDocument);
        //        return fileDocument;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        //catch { return ""; }


        Boolean fileOK = false;
        string path = commonfunctions.BaseDirectory + "\\uploadedimages\\ReportAProblemImages\\";
        if (ScreenShotImageFile.HasFile)
        {
            String fileExtension =
                System.IO.Path.GetExtension(ScreenShotImageFile.FileName).ToLower();
            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
        }

        if (fileOK)
        {
            try
            {
                string fileDocument = DateTime.Now.ToString("ddmmyyyhhmmss") + ScreenShotImageFile.FileName.ToString();
                ScreenShotImageFile.PostedFile.SaveAs(path
                    + fileDocument);
                return fileDocument;
               // Label1.Text = "File uploaded!";
            }
            catch (Exception ex)
            {
               // Label1.Text = "File could not be uploaded.";
                return "";
            }
        }
        else
        {
            return "";
           // Label1.Text = "Cannot accept files of this type.";
        }

    }


    private string subjectAreadDropDown()
    {


        string sqll = "select * from  ViewAllSubjectArea_SSADL order by Name";

        string subjectDD = "<select id='SubjectAreaData' name='SubjectAreaData' class='validate[required]'  >";
        //  Response.Write(sqll);
        subjectDD += "<option value=''>- Select Subject Area -</option>";
        DataTableReader dtr = DataBase.dbDataTable(sqll, "Ektron.Dbconnection").CreateDataReader();
        if (dtr.HasRows)
        {
            while (dtr.Read())
            {
                string TaxID = dtr["TaxID"].ToString();
                string Name = dtr["Name"].ToString();
                 
                    subjectDD += "<option value='" + TaxID + "'>" + Name + "</option>";
              
            }

        }
        subjectDD += "</select>";
        return subjectDD;


    }

    private string resourceDropDown( string extraSQl = "")
    {
        string resourceDD = "<select id='resourceData' name='resourceData' onchange='changeInfo(this.value);' class='validate[required]' >";
        string sqll = "select ID, ResourceName from  Resources  " + extraSQl + " order by ResourceName";
        
        //  Response.Write(sqll);
        resourceDD += "<option value=''>- Select Subject Area -</option>";
        DataTableReader dtr = DataBase.dbDataTable(sqll).CreateDataReader();
        if (dtr.HasRows)
        {
            while (dtr.Read())
            {
                string ID = dtr["ID"].ToString();
                string ResourceName = dtr["ResourceName"].ToString();
               
                    resourceDD += "<option value='" + ID + "'>" + ResourceName + "</option>";
                
            }

        }

        resourceDD += "</select><p></p><span id='results'></span>";

       

        return resourceDD;

    }



    public string notifyStle { get; set; }

    public string notifyDate { get; set; }

    public string notifyByUsername { get; set; }

    public string divApprovedDiv { get; set; }

    public string divDeclinedDiv { get; set; }



    public string HideMe { get; set; }

    public string reqID { get; set; }

    public string FormID { get; set; }







    public string SubmittedByPIN { get; set; }

    public string SubmittedByLastName { get; set; }

    public string SubmittedByFirstName { get; set; }

    public string SubmittedByEMail { get; set; }

    public string SubmittedByOffice { get; set; }

    public string SubmittedByServer { get; set; }

    public string SubmittedByUserDomain { get; set; }

    public string SubmittedForPIN { get; set; }

    public string SubmittedForLastName { get; set; }

    public string SubmittedForFirstName { get; set; }

    public string SubmittedForEMail { get; set; }

    public string SubmittedForOffice { get; set; }

    public string SubmittedForServer { get; set; }

    public string SubmittedForUserDomain { get; set; }

    public string FormStatus { get; set; }

    public string Subject { get; set; }

    public string Resource { get; set; }

    public object HowCanWeHelpdb { get; set; }

    public object InternalNotesdb { get; set; }

    public object Resourcedb { get; set; }

    public string Subjectdb { get; set; }

    public string subjectAreadDropDownData { get; set; }

    public string resourceDropDownData { get; set; }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/helprequests/");
    }
}