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

            DataSet dt = new DataSet();
            reqID = Request.QueryString["reqid"].ToString();
            FormID = Request.QueryString["formId"].ToString();
            string sql = "";



            switch (FormID)
            {
                case "1":  //Research Assistance           
                    sql = "Select * from ResearchAssistanceForm where id=" + reqID;
                    dt = DataBase.dbDataSet(sql);
                    ResearchAssistanceForm(dt);
                    requestName = "Research Assistance";
                    break;

                case "2": //Password Assistance           
                    sql = "Select * from PasswordAssistanceForm where id=" + reqID;
                    dt = DataBase.dbDataSet(sql);
                    PasswordAssistanceForm(dt);
                    requestName = "Password Assistance  ";
                    break;

                case "3": //Request for an Article        
                    sql = "Select * from RequestForAnArticle where id=" + reqID;
                    dt = DataBase.dbDataSet(sql);
                    RequestForAnArticle(dt);
                    requestName = "Request for an Article";
                    break;

                case "4": //Suggesting a New Resource     
                    sql = "Select * from SuggestingNewResourceForm where id=" + reqID;
                    dt = DataBase.dbDataSet(sql);
                    SuggestingNewResourceForm(dt);
                    requestName = "Suggesting a New Resource";
                    break;

                case "5": //Training Request              
                    sql = "Select * from TrainingRequestForm where id=" + reqID;
                    dt = DataBase.dbDataSet(sql);
                    TrainingRequestForm(dt);
                    requestName = "Training Request ";
                    break;

                case "6":  //Reporting a problem        
                    sql = "Select * from ReportingProblemForm where id=" + reqID;
                    dt = DataBase.dbDataSet(sql);
                    ReportingProblemForm(dt);
                    requestName = "Reporting a problem ";
                    //Response.Write(FormID);
                    break;

                case "7":  //Other                         

                    sql = "Select * from OtherForm where id=" + reqID;
                    dt = DataBase.dbDataSet(sql);
                    OtherForm(dt);
                    requestName = "Other Forms";
                    break;

            }



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
            FormStatusDB = dt.Tables[0].Rows[0].Field<string>("FormStatus").Trim();


            Dictionary<string, string> RequestorDetails = loginSSA.GetUsersDetails(SubmittedForPINDB);
            Dictionary<string, string> SubmittedByDetails = loginSSA.GetUsersDetails(SubmittedByPINDB);

            FormStatusDB = dt.Tables[0].Rows[0].Field<string>("FormStatus").Trim();

            if (FormStatusDB == "Closed")
            {
                SaveForLaterBtn.Visible = false;
                MarkAsClosedBtn.Visible = false;
            }

            UseThisInfo_PIN = SubmittedForPINDB;
            UseThisInfo_Name = RequestorDetails["DisplayName"]; // SubmittedForLastNameDB + " " + SubmittedForFirstNameDB;
            UseThisInfo_Title = "";
            UseThisInfo_Component = "";
            UseThisInfo_OfficeCode = SubmittedForOfficeDB;
            UseThisInfo_Email = SubmittedForEMailDB;
            UseThisInfo_Phone = "";

            InternalNotes.Text = dt.Tables[0].Rows[0].Field<string>("InternalNotes").Trim(); ;

            RequestorName.Text = RequestorDetails["DisplayName"];
            requestorName = RequestorDetails["DisplayName"];
            RequestorTitle.Text = RequestorDetails["Title"];
            RequestorOfficeCode.Text = RequestorDetails["OfficeCode"];
            RequestorEmail.Text = RequestorDetails["Email"];
            RequestorPhone.Text = RequestorDetails["Telephone"];

            SubmittedByName.Text = SubmittedByDetails["DisplayName"];
            SubmittedByOfficeCode.Text = SubmittedByDetails["OfficeCode"];
            SubmittedByEmail.Text = SubmittedByDetails["Email"];
            SubmittedByPhone.Text = SubmittedByDetails["Telephone"];
            SubmittedByTitle.Text = SubmittedByDetails["Title"];
        }

    }

    private void OtherForm(DataSet dt)
    {
        OtherFormPanel.Visible = true;
        comments.Text = dt.Tables[0].Rows[0].Field<string>("comments").Trim();


        AuditLogUX.tableName = "OtherForm";
        AuditLogUX.CHID = dt.Tables[0].Rows[0].Field<int>("ID").ToString();

    }

    private void ReportingProblemForm(DataSet dt)
    {
        ReportingProblemFormPanel.Visible = true;
        string ProblemType = dt.Tables[0].Rows[0].Field<string>("ProblemType").Trim();
        string Problemsource = dt.Tables[0].Rows[0].Field<string>("Problemsource").Trim();
        ProblemDescription.Text = dt.Tables[0].Rows[0].Field<string>("ProblemDescription").Trim();
        string ScreenshotimageDB = dt.Tables[0].Rows[0].Field<string>("Screenshotimage").Trim();
        string InternalNotes = dt.Tables[0].Rows[0].Field<string>("InternalNotes").Trim();

        if (ProblemType == "A Resource")
        {

            // ResourceRP.SelectThisResource = Problemsource;
            resourceDropDownData = resourceDropDown(Problemsource, "");
            HideMe = "$('#spanResource').show();";


        }
        else
        {
            pageTrouble.Text = Problemsource;
            HideMe = "$('#spanWebsite').show();";
        }
        ProblemWith.Items.FindByValue(ProblemType).Selected = true;
        if (ScreenshotimageDB != "")
        {
          //  Screenshotimage.ImageUrl = commonfunctions.host + "/uploadedimages/ReportAProblemImages/" + ScreenshotimageDB;
            screenShotLink.Text = ScreenshotimageDB;
            screenShotLink.NavigateUrl = commonfunctions.host + "/uploadedimages/ReportAProblemImages/" + ScreenshotimageDB;
            screenShotLink.Target = "_blank";
        }


        AuditLogUX.tableName = "ReportingProblemForm";
        AuditLogUX.CHID = dt.Tables[0].Rows[0].Field<int>("ID").ToString();

    }

    private void TrainingRequestForm(DataSet dt)
    {
        TrainingRequestFormPanel.Visible = true;
        string Subject = dt.Tables[0].Rows[0].Field<string>("Subject").Trim();
        string Resource = dt.Tables[0].Rows[0].Field<string>("Resource").Trim();
        //ResourceTR.SelectThisResource = Resource;
        //ResourceTR.SelectedSubjectArea = Subject;
        // SubjectTR.SelectThisSubject = Subject;
        subjectAreadDropDownData = subjectAreadDropDown(Subject);
        resourceDropDownData = resourceDropDown(Resource, Subject);

        NumberOfAttendees.Text = dt.Tables[0].Rows[0].Field<string>("NumberOfAttendees").Trim();
        RequestedDate.Text = dt.Tables[0].Rows[0].Field<DateTime>("RequestedDate").ToShortDateString();
        RequestedTime.Text = dt.Tables[0].Rows[0].Field<string>("RequestedTime").Trim();
        Location.Text = dt.Tables[0].Rows[0].Field<string>("Location").Trim();
        OtherInformation.Text = dt.Tables[0].Rows[0].Field<string>("OtherInformation").Trim();


        AuditLogUX.tableName = "TrainingRequestForm";
        AuditLogUX.CHID = dt.Tables[0].Rows[0].Field<int>("ID").ToString();
    }

    private void SuggestingNewResourceForm(DataSet dt)
    {
        SuggestingNewResourceFormPanel.Visible = true;
        NameOfResource.Text = dt.Tables[0].Rows[0].Field<string>("NameOfResource").Trim();
        WhatIsTheBusinessNeedForThisResource.Text = dt.Tables[0].Rows[0].Field<string>("WhatIsTheBusinessNeedForThisResource").Trim();
        AdditionalInformation.Text = dt.Tables[0].Rows[0].Field<string>("AdditionalInformation").Trim();
        ApprovingSupervisor.Text = dt.Tables[0].Rows[0].Field<string>("ApprovingSupervisor").Trim();

        AuditLogUX.tableName = "SuggestingNewResourceForm";
        AuditLogUX.CHID = dt.Tables[0].Rows[0].Field<int>("ID").ToString();
    }

    private void RequestForAnArticle(DataSet dt)
    {
        RequestForAnArticlePanel.Visible = true;

        ArticleTitleorKeyword.Text = dt.Tables[0].Rows[0].Field<string>("ArticleTitleorKeyword").Trim();
        Author.Text = dt.Tables[0].Rows[0].Field<string>("Authors").Trim();
        JournalTitle.Text = dt.Tables[0].Rows[0].Field<string>("JournalTitle").Trim();
        IssueVolumePage.Text = dt.Tables[0].Rows[0].Field<string>("IssueVolumePage").Trim();
        YearPublished.Text = dt.Tables[0].Rows[0].Field<string>("YearPublished").Trim();
        WhyDoYouNeedThisArticle.Text = dt.Tables[0].Rows[0].Field<string>("WhyDoYouNeedThisArticle").Trim();

        AuditLogUX.tableName = "RequestForAnArticle";
        AuditLogUX.CHID = dt.Tables[0].Rows[0].Field<int>("ID").ToString();


    }

    private void PasswordAssistanceForm(DataSet dt)
    {

        PasswordAssistanceFormPanel.Visible = true;
        string ResourceforwhichassistanceisneededDB = dt.Tables[0].Rows[0].Field<string>("Resourceforwhichassistanceisneeded").Trim();
        // ResourceInfo.SelectThisResource = ResourceforwhichassistanceisneededDB;
        // ResourceInfo.LabelTitle = "For which Resource do you need password assistance?";
        resourceDropDownData = resourceDropDown(ResourceforwhichassistanceisneededDB, "");
        Additionalnotesordetails.Text = dt.Tables[0].Rows[0].Field<string>("Additionalnotesordetails").Trim();

        AuditLogUX.tableName = "PasswordAssistanceForm";
        AuditLogUX.CHID = dt.Tables[0].Rows[0].Field<int>("ID").ToString();
    }

    private void ResearchAssistanceForm(DataSet dt)
    {
        ResearchAssistanceFormPanel.Visible = true;
        string Subject = dt.Tables[0].Rows[0].Field<string>("Subject").Trim();
        string Resource = dt.Tables[0].Rows[0].Field<string>("Resource").Trim();
        HowCanWeHelp.Text = dt.Tables[0].Rows[0].Field<string>("HowCanWeHelp").Trim();

        //ResourceRA.SelectThisResource = Resource;
        //SubjectRA.SelectThisSubject = Subject;
        //ResourceRA.SelectedSubjectArea = Subject;
        subjectAreadDropDownData = subjectAreadDropDown(Subject);
        resourceDropDownData = resourceDropDown(Resource, Subject);
        AuditLogUX.tableName = "ResearchAssistanceForm";
        AuditLogUX.CHID = dt.Tables[0].Rows[0].Field<int>("ID").ToString();
    }


    private string subjectAreadDropDown(string selectthissubject)
    {


        string sqll = "select * from  ViewAllSubjectArea_SSADL order by Name";

        string subjectDD = "<select id='SubjectAreaData' name='SubjectAreaData'  >";
        //  Response.Write(sqll);
        DataTableReader dtr = DataBase.dbDataTable(sqll, "Ektron.Dbconnection").CreateDataReader();
        if (dtr.HasRows)
        {
            while (dtr.Read())
            {
                string TaxID = dtr["TaxID"].ToString();
                string Name = dtr["Name"].ToString();
                if (selectthissubject == TaxID)
                {
                    subjectDD += "<option selected value='" + TaxID + "'>" + Name + "</option>";
                }
                else
                {
                    subjectDD += "<option value='" + TaxID + "'>" + Name + "</option>";
                }
            }

        }
        subjectDD += "</select>";
        return subjectDD;


    }

    private string resourceDropDown(string selectthisresource, string loadThisSubjectArea, string extraSQl = "")
    {
        string resourceDD = "<select id='resourceData' name='resourceData' onchange='changeInfo(this.value);' >";
        string sqll = "select ID, ResourceName from  Resources order by ResourceName";
        if (loadThisSubjectArea != "")
        {

            sqll = "select ID, ResourceName from  Resources  where [SubjectAreasTaxonomy] like '%," + loadThisSubjectArea + ",%'  " + extraSQl + "  order by ResourceName";
        }

        //  Response.Write(sqll);

        DataTableReader dtr = DataBase.dbDataTable(sqll).CreateDataReader();
        if (dtr.HasRows)
        {
            while (dtr.Read())
            {
                string ID = dtr["ID"].ToString();
                string ResourceName = dtr["ResourceName"].ToString();
                if (selectthisresource == ID)
                {
                    resourceDD += "<option selected value='" + ID + "'>" + ResourceName + "</option>";
                }
                else
                {
                    resourceDD += "<option value='" + ID + "'>" + ResourceName + "</option>";
                }
            }

        }

        resourceDD += "</select><p></p><span id='results'></span>";

        resourceDD += "<script>changeInfo(" + selectthisresource + ");</script>";

        return resourceDD;

    }


    //private void getResourceDetails(string resourceID)
    //{
    //    DataRow[] foundRows = resourcesDt.Select("ID='" + resourceID + "'");

    //    // Use the Select method to find all rows matching the filter.
    //    //  foundRows = resourcesDt.Select(filter);

    //    // Print column 0 of each returned row. 
    //    for (int i = 0; i < foundRows.Length; i++)
    //    {
    //        resourceName = foundRows[i]["ResourceName"].ToString().Trim();
    //        resourceDescription = foundRows[i]["Description"].ToString().Trim();
    //        resourceAdminResourceURL = foundRows[i]["AdminResourceURL"].ToString().Trim();
    //        resourceAdminUsername = foundRows[i]["AdminUsername"].ToString().Trim();
    //        resourceAdminPassword = foundRows[i]["AdminPassword"].ToString().Trim();
    //        resourceLimitedNumberPasswordsAvailable = foundRows[i]["LimitedNumberPasswordsAvailable"].ToString().Trim();
    //        resourcePasswordsAvailable = foundRows[i]["PasswordsAvailable"].ToString().Trim();

    //        if (resourceAdminResourceURL != "") AdminLoginPanel.Visible = true;

    //    }
    //}

    private void getRequestDetails(string resourceID)
    {
        throw new NotImplementedException();
    }





    protected void SaveForLater_Click(object sender, EventArgs e)
    {

        string generalSQL = "  [SubmittedByEMail] = '" + RequestorEmail.Text + "' ";
        generalSQL += " ,[SubmittedByOffice] ='" + RequestorOfficeCode.Text + "' ";
        generalSQL += "  ,[SubmittedForEMail] = '" + SubmittedByEmail.Text + "' ";
        generalSQL += "  ,[SubmittedForOffice] ='" + SubmittedByOfficeCode.Text + "' ";
        generalSQL += "    ,[InternalNotes] =@InternalNotes  ";
        generalSQL += "   ,[FormStatus] = 'Open' , ModifiedDateTime=GETDATE() ";
        someGeneralStuff(generalSQL);



        Response.Redirect("/admin/helprequests/default.aspx");

    }

    private void someGeneralStuff(string generalSQL)
    {
        reqID = Request.QueryString["reqid"].ToString();
        FormID = Request.QueryString["formId"].ToString();
        string sql = "";

        switch (FormID)
        {
            case "1":  //Research Assistance   
                //  DropDownList Subj = (DropDownList)SubjectRA.FindControl("subjectDD");
                //  DropDownList Resc = (DropDownList)ResourceRA.FindControl("ResourceDD");

                string Subject = Request.Form["SubjectAreaData"]; // Subj.SelectedValue;
                string Resource = Request.Form["resourceData"]; //Resc.SelectedValue;
                sql = "UPDATE ResearchAssistanceForm SET Subject='" + Subject + "' ,  Resource='" + Resource + "' , HowCanWeHelp=@HowCanWeHelp  , " + generalSQL + " where id=" + reqID;
                SqlCommand cmd1 = new SqlCommand(sql);
                cmd1.Parameters.AddWithValue("@HowCanWeHelp", HowCanWeHelp.Text);
                cmd1.Parameters.AddWithValue("@InternalNotes", InternalNotes.Text);
                DataBase.executeCommandWithParameters(cmd1);

                saveRecord(sql, "ResearchAssistanceForm");
                

                break;

            case "2": //Password Assistance           


                //  DropDownList Resource22 = (DropDownList)ResourceInfo.FindControl("ResourceDD");
                string Resource2 = Request.Form["resourceData"]; // Resource22.SelectedValue;

                sql = "UPDATE PasswordAssistanceForm SET   Resourceforwhichassistanceisneeded='" + Resource2 + "' , Additionalnotesordetails=@Additionalnotesordetails , " + generalSQL + " where id=" + reqID;
                SqlCommand cmd2 = new SqlCommand(sql);
                cmd2.Parameters.AddWithValue("@Additionalnotesordetails", Additionalnotesordetails.Text);
                cmd2.Parameters.AddWithValue("@InternalNotes", InternalNotes.Text);
                
                DataBase.executeCommandWithParameters(cmd2);
                saveRecord(sql, "PasswordAssistanceForm");

                break;

            case "3": //Request for an Article        

                sql = "UPDATE RequestForAnArticle SET   ArticleTitleorKeyword=@ArticleTitleorKeyword , Authors=@Authors , JournalTitle=@JournalTitle , IssueVolumePage=@IssueVolumePage  , YearPublished=@YearPublished , WhyDoYouNeedThisArticle=@WhyDoYouNeedThisArticle, " + generalSQL + " where id=" + reqID;
                SqlCommand cmd3 = new SqlCommand(sql);
                cmd3.Parameters.AddWithValue("@ArticleTitleorKeyword", ArticleTitleorKeyword.Text);
                cmd3.Parameters.AddWithValue("@Authors", Author.Text);
                cmd3.Parameters.AddWithValue("@JournalTitle", JournalTitle.Text);
                cmd3.Parameters.AddWithValue("@YearPublished", YearPublished.Text);
                cmd3.Parameters.AddWithValue("@WhyDoYouNeedThisArticle", WhyDoYouNeedThisArticle.Text);
                cmd3.Parameters.AddWithValue("@IssueVolumePage", IssueVolumePage.Text);
                cmd3.Parameters.AddWithValue("@InternalNotes", InternalNotes.Text);
                DataBase.executeCommandWithParameters(cmd3);
                saveRecord(sql, "RequestForAnArticle");
              //  string sql2 = "UPDATE RequestForAnArticle SET   ArticleTitleorKeyword='" + ArticleTitleorKeyword.Text + "' , Authors='" + ArticleTitleorKeyword.Text + "' , JournalTitle='" + JournalTitle.Text + "' , IssueVolumePage='" + IssueVolumePage.Text + "'  , YearPublished='" + YearPublished.Text + "' , WhyDoYouNeedThisArticle='" + WhyDoYouNeedThisArticle.Text + "', " + generalSQL + " where id=" + reqID;
               // Response.Write(sql2);
                break;

            case "4": //Suggesting a New Resource     

                sql = "UPDATE SuggestingNewResourceForm SET   NameOfResource=@NameOfResource , WhatIsTheBusinessNeedForThisResource=@WhatIsTheBusinessNeedForThisResource , AdditionalInformation=@AdditionalInformation , ApprovingSupervisor=@ApprovingSupervisor, " + generalSQL + " where id=" + reqID;
                SqlCommand cmd4 = new SqlCommand(sql);
                cmd4.Parameters.AddWithValue("@NameOfResource", NameOfResource.Text);
                cmd4.Parameters.AddWithValue("@WhatIsTheBusinessNeedForThisResource", WhatIsTheBusinessNeedForThisResource.Text);
                cmd4.Parameters.AddWithValue("@AdditionalInformation", AdditionalInformation.Text);
                cmd4.Parameters.AddWithValue("@ApprovingSupervisor", ApprovingSupervisor.Text);
                cmd4.Parameters.AddWithValue("@InternalNotes", InternalNotes.Text);
                DataBase.executeCommandWithParameters(cmd4);
                saveRecord(sql, "SuggestingNewResourceForm");

                break;

            case "5": //Training Request              
                //  DropDownList Subj5 = (DropDownList)SubjectTR.FindControl("subjectDD");
                //  DropDownList Resc5 = (DropDownList)ResourceTR.FindControl("ResourceDD");
                string Subject5 = Request.Form["SubjectAreaData"]; //Subj5.SelectedValue;
                string Resource5 = Request.Form["resourceData"]; //Resc5.SelectedValue;

                sql = "UPDATE TrainingRequestForm SET   Subject='" + Subject5 + "' , Resource='" + Resource5 + "' , NumberOfAttendees=@NumberOfAttendees  , RequestedDate='" + RequestedDate.Text + "' , RequestedTime='" + RequestedTime.Text + "' , Location=@Location,   OtherInformation=@OtherInformation,  " + generalSQL + " where id=" + reqID;
                SqlCommand cmd5 = new SqlCommand(sql);
                cmd5.Parameters.AddWithValue("@NumberOfAttendees", NumberOfAttendees.Text);
                cmd5.Parameters.AddWithValue("@Location", Location.Text);
                cmd5.Parameters.AddWithValue("@OtherInformation", OtherInformation.Text);
                cmd5.Parameters.AddWithValue("@InternalNotes", InternalNotes.Text);
                DataBase.executeCommandWithParameters(cmd5);

                saveRecord(sql, "TrainingRequestForm");
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

                sql = "UPDATE ReportingProblemForm SET   ProblemType='" + ProblemWith.SelectedValue + "' , Problemsource='" + pprbsource + "' , ProblemDescription=@ProblemDescription, " + generalSQL + " where id=" + reqID;


                SqlCommand cmd6 = new SqlCommand(sql);
                cmd6.Parameters.AddWithValue("@ProblemDescription", ProblemDescription.Text);
                cmd6.Parameters.AddWithValue("@InternalNotes", InternalNotes.Text);
                DataBase.executeCommandWithParameters(cmd6);
                AuditLogs.log_Changes(AccesIDHF.Value, "ReportingProblemForm");
               // saveRecord(sql, "ReportingProblemForm");

                break;

            case "7":  //Other                         

                sql = "UPDATE OtherForm SET   comments=@comments , " + generalSQL + " where id=" + reqID;
                SqlCommand cmd7 = new SqlCommand(sql);
                cmd7.Parameters.AddWithValue("@comments", comments.Text);
                cmd7.Parameters.AddWithValue("@InternalNotes", InternalNotes.Text);
                DataBase.executeCommandWithParameters(cmd7);
                saveRecord(sql, "OtherForm");
                break;

        }

     
    }

    protected void MarkAsClosedBtn_Click(object sender, EventArgs e)
    {
        string generalSQL = "  [SubmittedByEMail] = '" + RequestorEmail.Text + "' ";
        generalSQL += " ,[SubmittedByOffice] ='" + RequestorOfficeCode.Text + "' ";
        generalSQL += "  ,[SubmittedForEMail] = '" + SubmittedByEmail.Text + "' ";
        generalSQL += "  ,[SubmittedForOffice] ='" + SubmittedByOfficeCode.Text + "' ";
        generalSQL += "    ,[InternalNotes] = @InternalNotes  ";
        generalSQL += "   ,[FormStatus] = 'Resolved-Not-Notified' , ModifiedDateTime=GETDATE() ";
        someGeneralStuff(generalSQL);

        reqID = Request.QueryString["reqid"].ToString();
        FormID = Request.QueryString["formId"].ToString();
        Response.Redirect("/admin/helprequests/sendMessage.aspx?type=close&reqid=" + reqID + "&formId=" + FormID + "&requestName=" + requestName + "&requestorName=" + requestorName + "&email=" + RequestorEmail.Text);
    }

    private void saveRecord(string sql, string tableName)
    {
        //string sql = "Update AccessToResourceForm Set FormStatus ='" + FormStatus + "' , WhyDoYouNeedAccess='" + whyNeedAccess.Text + "' ,ModifiedDateTime=GETDATE(), ModifiedByPIN ='" + loginSSA.myPIN + "'  , InternalNotes='" + InternalNotes.Text + "'  where ID='" + RequestID.Value.Trim() + "'";
        // Response.Write(sql);
        // DataBase.executeCommand(sql);
        AuditLogs.log_Changes(AccesIDHF.Value, tableName);

    }



    protected void Approve_Click(object sender, EventArgs e)
    {
        //saveRecord("Approved-Not-Notified");
        //Response.Redirect("/admin/requests/approvedRequest.aspx?reqid=" + RequestID.Value.Trim());
    }
    protected void DeclineBtn_Click(object sender, EventArgs e)
    {
        //delete any entry from previous approved
        //string sqlDelete = "Delete from PasswordAssignments where RequestID = '" + RequestID.Value.Trim() + "'";
        //saveRecord("Declined-Not-Notified");
        //DataBase.executeCommand(sqlDelete, "Admin.DbConnection");
        //Response.Redirect("/admin/requests/declinedRequest.aspx?reqid=" + RequestID.Value.Trim());
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



    public string HideMe { get; set; }

    public string reqID { get; set; }

    public string FormID { get; set; }




    public string requestName
    {
        get
        {
            if (ViewState["requestName"] == null)
                return string.Empty;
            return ViewState["requestName"].ToString();

        }
        set
        {
            ViewState["requestName"] = value;
        }
    }


    public string requestorName
    {
        get
        {
            if (ViewState["requestorName"] == null)
                return string.Empty;
            return ViewState["requestorName"].ToString();

        }
        set
        {
            ViewState["requestorName"] = value;
        }
    }

    public string subjectAreadDropDownData { get; set; }

    public string resourceDropDownData { get; set; }
}