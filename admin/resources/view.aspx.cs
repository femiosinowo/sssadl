using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSADL.CMS;
using System.IO;
using System.Collections;

public partial class admin_users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Resource Type Taxonomy
            ResourceTypeTaxonomy.DataSource = AdminFunc.ResourceTypeTaxonomyDataTable;
            ResourceTypeTaxonomy.DataBind();

            //Subject Areas Taxonomy
            SubjectAreasTaxonomy.DataSource = AdminFunc.SubjectAreasTaxonomyDataTable;
            SubjectAreasTaxonomy.DataBind();

            //Access Type Taxonomy
            AccessTypeTaxonomy.DataSource = AdminFunc.AccessTypeTaxonomyDataTable;
            AccessTypeTaxonomy.DataBind();

            //Audience

            ShowInAudienceToolsTaxonomy.DataSource = AdminFunc.ShowInAudienceToolsTaxonomyDataTable;
            ShowInAudienceToolsTaxonomy.DataBind();
            //SendEpasswordTo
            SendEpasswordTo.DataSource = getAllAdminUsers();
            SendEpasswordTo.DataBind();
            // CreateAccessTypeOptions();
            LibraryCOR.DataSource = getAllAdminUsers(" and COR='Y' ");
            LibraryCOR.DataBind();



            resourceid = Request.QueryString["resourceid"].ToString();
            getItem(resourceid);
            ResourceIDHF.Value = resourceid;

            AuditLogUX.tableName = "Resources";
            AuditLogUX.tableName2 = "ResourcesContract";
            AuditLogUX.ForeignColumnName = "ResourceID";
            AuditLogUX.CHID = resourceid;

        }


    }


    private DataSet dt = new DataSet();

    private void getItem(string resourceid)
    {

        dt = DataBase.dbDataSet("Select * from Resources where ID = '" + resourceid + "';");
        //  getInitialValues(dt);



        //  ResourceID.Value = dt.Tables[0].Rows[0].Field<int>("ID").ToString();
        ResourceNameDB = dt.Tables[0].Rows[0].Field<string>("ResourceName").Trim();
        DescriptionDB = dt.Tables[0].Rows[0].Field<string>("Description").Trim();
        ResourceTypeTaxonomyDB = dt.Tables[0].Rows[0].Field<string>("ResourceTypeTaxonomy").Trim();
        ResourceURLlinkDB = dt.Tables[0].Rows[0].Field<string>("ResourceURLlink").Trim();
        ShowInNewWindowDB = dt.Tables[0].Rows[0].Field<string>("ShowInNewWindow").Trim();
        AdminResourceURLDB = dt.Tables[0].Rows[0].Field<string>("AdminResourceURL").Trim();
        AdminUsernameDB = dt.Tables[0].Rows[0].Field<string>("AdminUsername").Trim();
        AdminPasswordDB = dt.Tables[0].Rows[0].Field<string>("AdminPassword").Trim();
        File1DB = dt.Tables[0].Rows[0].Field<string>("File1").Trim();
        File2DB = dt.Tables[0].Rows[0].Field<string>("File2").Trim();
        File3DB = dt.Tables[0].Rows[0].Field<string>("File3").Trim();
        SubjectAreasTaxonomyDB = dt.Tables[0].Rows[0].Field<string>("SubjectAreasTaxonomy").Trim();
        ShowInSubjectAreasDB = dt.Tables[0].Rows[0].Field<string>("ShowInSubjectAreas").Trim();
        ShowInDatabasesDB = dt.Tables[0].Rows[0].Field<string>("ShowInDatabases").Trim();
        ShowInTrainingRequestFormDB = dt.Tables[0].Rows[0].Field<string>("ShowInTrainingRequestForm").Trim();
        ShowInAudienceToolsTaxonomyDB = dt.Tables[0].Rows[0].Field<string>("ShowInAudienceToolsTaxonomy").Trim();
        MandatoryDB = dt.Tables[0].Rows[0].Field<string>("Mandatory").Trim();
        AssociatedNetworkDB = dt.Tables[0].Rows[0].Field<string>("AssociatedNetwork").Trim();
        AccessTypeTaxonomyDB = dt.Tables[0].Rows[0].Field<string>("AccessTypeTaxonomy").Trim();
        ResourceRegistrationInstructionsDB = dt.Tables[0].Rows[0].Field<string>("ResourceRegistrationInstructions").Trim();
        SharedUsernameDB = dt.Tables[0].Rows[0].Field<string>("SharedUsername").Trim();
        SharedPasswordDB = dt.Tables[0].Rows[0].Field<string>("SharedPassword").Trim();
        ShowLoginDB = dt.Tables[0].Rows[0].Field<string>("ShowLogin").Trim();
        LimitedNumberPasswordsAvailableDB = dt.Tables[0].Rows[0].Field<string>("LimitedNumberPasswordsAvailable").Trim();
        PasswordsAvailableDB = dt.Tables[0].Rows[0].Field<string>("PasswordsAvailable").Trim();
        SendEpasswordToDB = dt.Tables[0].Rows[0].Field<string>("SendEpasswordTo").Trim();
        PasswordRequestsRestrictedToManagersDB = dt.Tables[0].Rows[0].Field<string>("PasswordRequestsRestrictedToManagers").Trim();
        TargetUsersDB = dt.Tables[0].Rows[0].Field<string>("TargetUsers").Trim();
        BusinessPurposeOfResourceDB = dt.Tables[0].Rows[0].Field<string>("BusinessPurposeOfResource").Trim();
        ResourceDisplayStatusDB = dt.Tables[0].Rows[0].Field<string>("ResourceDisplayStatus").Trim();
        ResourceFileTypeDB = dt.Tables[0].Rows[0].Field<string>("ResourceFileType").Trim();

        ListInResearchGuideFile1DB = dt.Tables[0].Rows[0].Field<string>("ListInResearchGuideFile1").Trim();
        ListInResearchGuideFile2DB = dt.Tables[0].Rows[0].Field<string>("ListInResearchGuideFile2").Trim();
        ListInResearchGuideFile3DB = dt.Tables[0].Rows[0].Field<string>("ListInResearchGuideFile3").Trim();

        LinktotheResourceDivStyle = " style='display:none;' ";
        uploadafileDivStyle = " style='display:none;' ";
        ResourceNameS = ResourceNameDB;
        ResourceName.Text = ResourceNameDB;

        Description.Text = DescriptionDB;
        string ResourceFileType = dt.Tables[0].Rows[0].Field<string>("ResourceFileType").Trim();
        if (ResourceFileType == "File")
        {
            File11DB = dt.Tables[0].Rows[0].Field<string>("ResourceURLlink").Trim();
        }
        try
        {
            ResourceTypeTaxonomy.Items.FindByValue(ResourceTypeTaxonomyDB).Selected = true;
        }
        catch { }

        file11Label = getFileUPloads2("", "11", true);
        switch (ResourceTypeTaxonomyDB)
        {
            case "124":
            case "125":
                ResourceURLlink.Text = ResourceURLlinkDB;
                LinktotheResourceDivStyle = "";

                break;

            case "126":
            case "127":
            case "128":

                uploadafileDivStyle = "";

                if (ResourceFileTypeDB == "File")
                {
                    //upload files
                    uploadFileRd.Checked = true;
                    // ResourceFileUploadTxt = uploadFiles(ResourceFileUpload);

                    if (File11DB != "")
                    {
                        file11Label = getFileUPloads2(File11DB, "11", true);
                    }

                }
                if (ResourceFileTypeDB == "Link")
                {
                    ResourceLinkRd.Checked = true;
                    ResourceURLlink2.Text = ResourceURLlinkDB;

                }


                break;
        }



        //ResourceURLlink.Text = ResourceURLlinkDB;

        //ShowInNewWindow.Items.FindByValue(ShowInNewWindowDB).Selected = true;
        if (ShowInNewWindowDB == "Y") ShowResourceinPopup.Checked = true;

        AdminResourceURL.Text = AdminResourceURLDB;
        AdminUsername.Text = AdminUsernameDB;
        AdminPassword.Text = AdminPasswordDB;


        taxIds = SubjectAreasTaxonomyDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string taxId in taxIds)
        {
            try
            {
                SubjectAreasTaxonomy.Items.FindByValue(taxId).Selected = true;
            }
            catch { }
        }

        file1Label = getFileUPloads(File1DB, "1", ListInResearchGuideFile1DB);
        file2Label = getFileUPloads(File2DB, "2", ListInResearchGuideFile2DB);
        file3Label = getFileUPloads(File3DB, "3", ListInResearchGuideFile3DB);

        if (ShowInSubjectAreasDB == "Y") ShowInSubjectAreas.Checked = true;
        if (ShowInDatabasesDB == "Y") ShowInDatabases.Checked = true;
        if (ShowInTrainingRequestFormDB == "Y") ShowInTrainingRequestForm.Checked = true;



        taxIds = ShowInAudienceToolsTaxonomyDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string taxId in taxIds)
        {
             try
            {
            ShowInAudienceToolsTaxonomy.Items.FindByValue(taxId).Selected = true;
            }
             catch { }
        }



        try { ApplicationIs.Items.FindByValue(MandatoryDB).Selected = true; }
        catch { }

        try { AssociatedNetwork.Items.FindByValue(AssociatedNetworkDB).Selected = true; }
        catch { }

        try { AccessTypeTaxonomy.Items.FindByValue(AccessTypeTaxonomyDB).Selected = true; }
        catch { }

        selfRegisterDivStyle = "style='display:none;'";
        SharedLoginDivStyle = "style='display:none;'";
        ePassDivStyle = "style='display:none;'";

        switch (AccessTypeTaxonomyDB)
        {

            case "122":  //122 - Epassword
                ePassDivStyle = "";
                if (Convert.ToInt16(PasswordsAvailableDB) > 0)
                {
                    limitedTo.Checked = true;
                    LimitedNumberPasswordsAvailable.Text = PasswordsAvailableDB;
                }
                else
                {
                    Unlimited.Checked = true;
                }

                taxIds = SendEpasswordToDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string taxId in taxIds)
                {
                    try{
                    SendEpasswordTo.Items.FindByValue(taxId).Selected = true;
                    }catch{}
                }


                if (PasswordRequestsRestrictedToManagersDB == "Y")
                {
                    PasswordRequestsRestrictedToManagers.Checked = true;
                }
                break;

            case "120":  //120 - Self Registered  
                selfRegisterDivStyle = "";
                ResourceRegistrationInstructions.Text = ResourceRegistrationInstructionsDB;
                break;

            case "121": //121 - Shared Login
                SharedLoginDivStyle = "";
                ShareUsername.Text = SharedUsernameDB;
                SharedPassword.Text = SharedPasswordDB;
                try
                {
                    ShowLogin.Items.FindByValue(ShowLoginDB).Selected = true;
                }
                catch { }
                break;

        }



        TargetUsers.Text = TargetUsersDB;
        BusinessPurposeOfResource.Text = BusinessPurposeOfResourceDB;

        ResourceDisplayStatus.Items.FindByValue(ResourceDisplayStatusDB).Selected = true;

        /////////////////Contracts
        // DataTable dtContract = new DataTable();
        string cIDs = string.Empty;
        // DataTableReader dtContractReader = DataBase.dbDataTable("Select ID from ResourcesContract where ResourceID = '" + resourceid + "';").CreateDataReader();

        DataTable dtContract = DataBase.dbDataTable("Select * from ResourcesContract where ResourceID = '" + resourceid + "' order by FiscalYear asc;");
        ContractTabsLV.DataSource = dtContract;
        ContractTabsLV.DataBind();
        ContractContentLV.DataSource = dtContract;
        ContractContentLV.DataBind();

        DataTableReader dtContractReader = DataBase.dbDataTable("Select Top 1 * from ResourcesContract where ResourceID = '" + resourceid + "';").CreateDataReader();
        dtContractReader.Read();
        NotifyOfExpirationThisManyDaysInAdvance.Text = dtContractReader["NotifyOfExpirationThisManyDaysInAdvance"].ToString().Trim();

        string LibraryCORDB = dtContractReader["LibraryContractingOfficersRepresentative"].ToString().Trim();

        taxIds = LibraryCORDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string taxId in taxIds)
        {
            try
            {
                LibraryCOR.Items.FindByValue(taxId).Selected = true;
            }
            catch { }
        }


        CriticalNotes.Text = dtContractReader["CriticalNotes"].ToString().Trim();
        VendorName.Text = dtContractReader["VendorName"].ToString().Trim();
        VendorEmail.Text = dtContractReader["RepresentativeEmail"].ToString().Trim();
        RepresentativeName.Text = dtContractReader["RepresentativeName"].ToString().Trim();
        VendorPhone.Text = dtContractReader["RepresentativePhone"].ToString().Trim();
        VendorTechnicalAssistanceName.Text = dtContractReader["TechnicalContactName"].ToString().Trim();
        VendorTechnicalAssistanceEmail.Text = dtContractReader["TechnicalContactEmail"].ToString().Trim();
        VendorTechnicalAssistancePhone.Text = dtContractReader["TechnicalContactPhone"].ToString().Trim();
        NewFeaturesDescription.Text = dtContractReader["NewFeatures"].ToString().Trim();
        NewFeatureStartDate.Text = dtContractReader["NotificationActiveStartDate"].ToString().Trim();
        NewFeatureEndDate.Text = dtContractReader["NotificationActiveEndDate"].ToString().Trim();

    }

    public string getFileUPloads(string path, string which, string ListInResearchGuideFile)
    {
        if (path != "")
        {
            //Response.Write(path);
            string fullPath = path;
            string filename = Path.GetFileName(path);
            string filext = Path.GetExtension(path);
            string htmlString = string.Empty;
            string lsSelected = string.Empty;
            if (ListInResearchGuideFile == "Y") { lsSelected = "checked"; } else { lsSelected = ""; }
            string researchGuideOption = "<option selected='true' >Research Guide</option >";
            htmlString = " <div id='fileResults" + which + "'>";
            htmlString += "<img src='/admin/framework/images/close.jpg' onclick='showUpload(" + which + ")' style='cursor:pointer' /><a href='" + fullPath + "' target='_blank'>" + filename + "</a> &nbsp;&nbsp;&nbsp;&nbsp;     File type: <select disabled='true' style='width:70px'><option selected='true' > " + filext + "</option></select > ";
            htmlString += "&nbsp;&nbsp;&nbsp;&nbsp; Description : <select  style='width:170px'>" + researchGuideOption + "</select >";
            htmlString += "&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' " + lsSelected + " name='listInRG" + which + "' id='listInRG" + which + "' /> <label for='listInRG" + which + "'>List in Research Guide</label>";
            htmlString += "<input type='hidden' name='uploadURL" + which + "' id='uploadURL" + which + "' value='" + fullPath + "' />";
            htmlString += "<input type='hidden' name='fileModified" + which + "' id='fileModified" + which + "' value='false' />";
            htmlString += " </div><div id='fileDiv" + which + "' style='display:none'> <input type='file' id='fileUpload" + which + "' name='fileUpload" + which + "'  onchange='uploadMe(this , " + which + ")' /> </div>";
            return htmlString;
        }
        else
        {
            return " <div id='fileDiv" + which + "'><input type='file' id='fileUpload" + which + "' name='fileUpload" + which + "'  onchange='uploadMe(this , " + which + ")' /></div> <div id='fileResults" + which + "'></div> ";
        }
    }


    public string getFileUPloads2(string path, string which, bool showValues)
    {
        if (path != "")
        {
            //Response.Write(path);
            string fullPath = path;
            string filename = Path.GetFileName(path);
            string filext = Path.GetExtension(path);
            string htmlString = string.Empty;
            string lsSelected = string.Empty;

            htmlString = " <div id='fileResults" + which + "'>";
            htmlString += "<img src='/admin/framework/images/close.jpg' onclick='showUpload(" + which + ")' style='cursor:pointer' /><a href='" + fullPath + "' target='_blank'>" + filename + "</a>  &nbsp;&nbsp;&nbsp;&nbsp;     File type: <select disabled='true' style='width:70px'><option selected='true' > " + filext + "</option></select > ";


            htmlString += "<input type='hidden' name='uploadURL" + which + "' id='uploadURL" + which + "' value='" + fullPath + "' />";
            htmlString += "<input type='hidden' name='fileModified" + which + "' id='fileModified" + which + "' value='false' />";
            htmlString += " </div><div id='fileDiv" + which + "' style='display:none'> <input type='file' id='fileUpload" + which + "' name='fileUpload" + which + "'  onchange='uploadMe(this , " + which + ")' /> </div>";
            return htmlString;
        }
        else
        {
            return " <div id='fileDiv" + which + "'><input type='file' id='fileUpload" + which + "' name='fileUpload" + which + "'  onchange='uploadMe2(this , " + which + ")' class='validate[required]' /></div> <div id='fileResults" + which + "'></div> ";
        }
    }


    protected string ListFinResearchGuide(string chkboxID)
    {
        try
        {
            if (Request.Form[chkboxID].ToString() == "on")
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }
        catch
        {
            return "N";
        }
    }

    protected void AddResource_Click(object sender, EventArgs e)
    {

        string ResourceIDTxt = ResourceIDHF.Value;
        string ResourceNameTxt = ResourceName.Text;
        string DescriptionTxt = Description.Text;
        string ResourceTypeTaxonomySelected = ResourceTypeTaxonomy.SelectedValue;
        string ResourceFileTypeTxt = "";
        string ResourceURLlinkTxt = "";
        string ResourceFileUploadTxt = "";
        string ShowResourceinPopupTxt = "N";
        string taxId = ResourceTypeTaxonomySelected;


        switch (taxId)
        {
            case "124":
            case "125":
                ResourceURLlinkTxt = ResourceURLlink.Text;

                break;

            case "126":
            case "127":
            case "128":

                if (uploadFileRd.Checked)
                {
                    //upload files
                    ResourceFileTypeTxt = "File";
                    ResourceURLlinkTxt = uploadResourceFiles("fileUpload11"); //uploadFiles(ResourceFileUpload);
                }
                if (ResourceLinkRd.Checked)
                {
                    ResourceURLlinkTxt = ResourceURLlink2.Text;
                    ResourceFileTypeTxt = "Link";

                }
                break;

                ////ResourceFileType
                //ResourceFileTypeTxt = ResourceLinkRd.Value;
                //if (uploadFileRd.Checked)
                //{
                //    //upload files
                //    ResourceFileUploadTxt = uploadResourceFiles("fileUpload11"); //uploadFiles(ResourceFileUpload); //uploadResourceFiles

                //}
                //if (ResourceLinkRd.Checked)
                //{
                //    ResourceURLlinkTxt = ResouceLink.Text;

                //}
                break;
        }

        if (ShowResourceinPopup.Checked) ShowResourceinPopupTxt = "Y";

        string AdminResourceURLTxt = AdminResourceURL.Text;
        string AdminUsernameTxt = AdminUsername.Text;
        string AdminPasswordTxt = AdminPassword.Text;





        //string File1_OldURL = TestNull(OldDBValues["File1"].ToString());
        //string File2_OldURL = TestNull(OldDBValues["File2"].ToString());
        //string File3_OldURL = TestNull(OldDBValues["File3"].ToString());



        //bool File1_NewURL = TestifNew("fileUpload1");
        //bool File2_NewURL = TestifNew("fileUpload2");
        //bool File3_NewURL = TestifNew("fileUpload3");


        bool File1_ModifiedURL = TestifModified("1");
        bool File2_ModifiedURL = TestifModified("2");
        bool File3_ModifiedURL = TestifModified("3");


        string File1URL = TestNull(OldDBValues["File1"].ToString());
        string File2URL = TestNull(OldDBValues["File2"].ToString());
        string File3URL = TestNull(OldDBValues["File3"].ToString());


        string ListInResearchGuideFile1 = ListFinResearchGuide("listInRG1"); // uploadResourceFiles("fileUpload1"); // uploadFiles(File1);
        string ListInResearchGuideFile2 = ListFinResearchGuide("listInRG2");  //  uploadResourceFiles("fileUpload2"); //uploadFiles(File2);
        string ListInResearchGuideFile3 = ListFinResearchGuide("listInRG3");  // uploadResourceFiles("fileUpload3"); // uploadFiles(File3);


        if (File1_ModifiedURL) File1URL = uploadResourceFiles("fileUpload1"); // uploadFiles(File1);
        if (File2_ModifiedURL) File2URL = uploadResourceFiles("fileUpload2"); //uploadFiles(File2);
        if (File3_ModifiedURL) File3URL = uploadResourceFiles("fileUpload3"); // uploadFiles(File3);

        string SubjectAreasTaxonomySelect = returnSelectedItemsValue(SubjectAreasTaxonomy);
        string ShowInSubjectAreasTxt = "N";
        if (ShowInSubjectAreas.Checked) ShowInSubjectAreasTxt = "Y";
        string ShowInDatabasesTxt = "N";
        if (ShowInDatabases.Checked) ShowInDatabasesTxt = "Y";
        string ShowInTrainingRequestFormTxt = "N";
        if (ShowInTrainingRequestForm.Checked) ShowInTrainingRequestFormTxt = "Y";


        string ShowInAudienceToolsTaxonomySelect = returnSelectedItemsValue(ShowInAudienceToolsTaxonomy);

        string ApplicationIsTxt = "";
        ApplicationIsTxt = ApplicationIs.SelectedValue;

        string AssociatedNetworkTxt = AssociatedNetwork.SelectedValue;

        string AccessTypeTaxonomyTxt = AccessTypeTaxonomy.SelectedValue;

        string LimitedNumberPasswordsAvailableTxt = "N";
        string UnlimitedTxt = "N";
        string PasswordsAvailableTxt = "0";
        string SendEpasswordToSelect = "";
        string PasswordRequestsRestrictedToManagersTxt = "N";

        //    if (AssociatedNetworkTxt == 119) return; //public
        string ResourceRegistrationInstructionsTxt = "";

        string ShareUsernameTxt = "";
        string SharedPasswordTxt = "";
        string ShowLoginTxt = "";

        switch (AccessTypeTaxonomyTxt)
        {

            case "122":  //122 - Epassword
                if (limitedTo.Checked)
                {
                    PasswordsAvailableTxt = LimitedNumberPasswordsAvailable.Text;
                    LimitedNumberPasswordsAvailableTxt = "Y";
                }

                if (Unlimited.Checked) UnlimitedTxt = "Y";

                SendEpasswordToSelect = returnSelectedItemsValue(SendEpasswordTo);
                if (PasswordRequestsRestrictedToManagers.Checked) PasswordRequestsRestrictedToManagersTxt = "Y";

                break;

            case "120":  //120 - Self Registered  
                ResourceRegistrationInstructionsTxt = ResourceRegistrationInstructions.Text;
                break;

            case "121": //121 - Shared Login
                ShareUsernameTxt = ShareUsername.Text;
                SharedPasswordTxt = SharedPassword.Text;
                ShowLoginTxt = ShowLogin.SelectedValue;
                break;

        }

        string TargetUsersTxt = TargetUsers.Text;
        string BusinessPurposeOfResourceTxt = BusinessPurposeOfResource.Text;

        /////////////////////////////////////////////////////////////////////////////////Contract Stuff


        string CriticalNotesTxt = CriticalNotes.Text;
        //  string HowManyDaysTxt = HowManyDays.Text;

        string LibraryCORSelect = returnSelectedItemsValue(LibraryCOR);

        string VendorNameTxt = VendorName.Text;
        string RepresentativeNameTxt = RepresentativeName.Text;
        string VendorEmailTxt = VendorEmail.Text;
        string VendorPhoneTxt = VendorPhone.Text;
        string VendorTechnicalAssistanceNameTxt = VendorTechnicalAssistanceName.Text;
        string VendorTechnicalAssistanceEmailTxt = VendorTechnicalAssistanceEmail.Text;
        string VendorTechnicalAssistancePhoneTxt = VendorTechnicalAssistancePhone.Text;
        string NewFeaturesDescriptionTxt = NewFeaturesDescription.Text;
        string NewFeatureStartDateTxt = NewFeatureStartDate.Text;
        string NewFeatureEndDateTxt = NewFeatureEndDate.Text;


        ///////////////////////////////////////////////////////////////////////////////////End Contract


        string ResourceDisplayStatusTxt = ResourceDisplayStatus.SelectedValue;

        // string sqlUpdate = string.Empty;

        //sqlUpdate += " UPDATE [dbo].[Resources] ";
        //sqlUpdate += "  SET [ResourceName] = '" + ResourceNameTxt + "'";
        //sqlUpdate += ",[Description] = '" + DescriptionTxt + "'";
        //sqlUpdate += ",[ResourceTypeTaxonomy] = '" + ResourceTypeTaxonomy.SelectedValue + "'";
        //sqlUpdate += ",[ResourceURLlink] = '" + ResourceURLlinkTxt + "'";
        //sqlUpdate += ",[ShowInNewWindow] = '" + ShowResourceinPopupTxt + "'";
        //sqlUpdate += ",[AdminResourceURL] = '" + AdminResourceURL.Text + "'";
        //sqlUpdate += ",[AdminUsername] = '" + AdminUsername.Text + "'";
        //sqlUpdate += ",[AdminPassword] = '" + AdminPassword.Text + "'";
        //sqlUpdate += ",[File1] = '" + File1URL + "'";
        //sqlUpdate += ",[File2] = '" + File2URL + "'";
        //sqlUpdate += ",[File3] = '" + File3URL + "'";
        //sqlUpdate += ",[SubjectAreasTaxonomy] = '" + SubjectAreasTaxonomySelect + "'";
        //sqlUpdate += ",[ShowInSubjectAreas] = '" + ShowInSubjectAreasTxt + "'";
        //sqlUpdate += ",[ShowInDatabases] = '" + ShowInDatabasesTxt + "'";
        //sqlUpdate += ",[ShowInTrainingRequestForm] = '" + ShowInTrainingRequestFormTxt + "'";
        //sqlUpdate += ",[ShowInAudienceToolsTaxonomy] = '" + ShowInAudienceToolsTaxonomySelect + "'";
        //sqlUpdate += ",[Mandatory] = '" + ApplicationIsTxt + "'";
        //sqlUpdate += ",[AssociatedNetwork] = '" + AssociatedNetworkTxt + "'";
        //sqlUpdate += ",[AccessTypeTaxonomy] = '" + AccessTypeTaxonomyTxt + "'";
        //sqlUpdate += ",[ResourceRegistrationInstructions] = '" + ResourceRegistrationInstructionsTxt + "'";
        //sqlUpdate += ",[SharedUsername] = '" + ShareUsernameTxt + "'";
        //sqlUpdate += ",[SharedPassword] = '" + SharedPasswordTxt + "'";
        //sqlUpdate += ",[ShowLogin] = '" + ShowLoginTxt + "'";
        //sqlUpdate += ",[LimitedNumberPasswordsAvailable] = '" + LimitedNumberPasswordsAvailableTxt + "'";
        //sqlUpdate += ",[PasswordsAvailable] = '" + PasswordsAvailableTxt + "'";
        //sqlUpdate += ",[SendEpasswordTo] = '" + SendEpasswordToSelect + "'";
        //sqlUpdate += ",[PasswordRequestsRestrictedToManagers] = '" + PasswordRequestsRestrictedToManagersTxt + "'";
        //sqlUpdate += ",[TargetUsers] = '" + TargetUsersTxt + "'";
        //sqlUpdate += ",[BusinessPurposeOfResource] = '" + BusinessPurposeOfResourceTxt + "'";
        //sqlUpdate += ",[ResourceDisplayStatus] = '" + ResourceDisplayStatusTxt + "'";
        //sqlUpdate += ",[ListInResearchGuideFile1] = '" + ListInResearchGuideFile1 + "'";
        //sqlUpdate += ",[ListInResearchGuideFile2] = '" + ListInResearchGuideFile2 + "'";
        //sqlUpdate += ",[ListInResearchGuideFile3] = '" + ListInResearchGuideFile3 + "'";
        //sqlUpdate += ",[ResourceFileType] = '" + ResourceFileTypeTxt + "'";
        //sqlUpdate += " WHERE ID='" + ResourceIDTxt + "'";

        string sqlUpdate = " UPDATE [dbo].[Resources]SET [ResourceName] = @ResourceName   ,[Description] = @Description   ,[ResourceTypeTaxonomy] = @ResourceTypeTaxonomy   ,[ResourceURLlink] = @ResourceURLlink   ,[ResourceFileType] = @ResourceFileType   ,[ShowInNewWindow] = @ShowInNewWindow   ,[AdminResourceURL] = @AdminResourceURL   ,[AdminUsername] = @AdminUsername   ,[AdminPassword] = @AdminPassword   ,[File1] = @File1   ,[File2] = @File2   ,[File3] = @File3   ,[ListInResearchGuideFile1] = @ListInResearchGuideFile1   ,[ListInResearchGuideFile2] = @ListInResearchGuideFile2   ,[ListInResearchGuideFile3] = @ListInResearchGuideFile3   ,[SubjectAreasTaxonomy] = @SubjectAreasTaxonomy   ,[ShowInSubjectAreas] = @ShowInSubjectAreas   ,[ShowInDatabases] = @ShowInDatabases   ,[ShowInTrainingRequestForm] = @ShowInTrainingRequestForm   ,[ShowInAudienceToolsTaxonomy] = @ShowInAudienceToolsTaxonomy   ,[Mandatory] = @Mandatory   ,[AssociatedNetwork] = @AssociatedNetwork   ,[AccessTypeTaxonomy] = @AccessTypeTaxonomy   ,[ResourceRegistrationInstructions] = @ResourceRegistrationInstructions   ,[SharedUsername] = @SharedUsername   ,[SharedPassword] = @SharedPassword   ,[ShowLogin] = @ShowLogin   ,[LimitedNumberPasswordsAvailable] = @LimitedNumberPasswordsAvailable   ,[PasswordsAvailable] = @PasswordsAvailable   ,[SendEpasswordTo] = @SendEpasswordTo   ,[PasswordRequestsRestrictedToManagers] = @PasswordRequestsRestrictedToManagers   ,[TargetUsers] = @TargetUsers   ,[BusinessPurposeOfResource] = @BusinessPurposeOfResource   ,[ResourceDisplayStatus] = @ResourceDisplayStatus ";
        sqlUpdate += " WHERE ID='" + ResourceIDTxt + "'";
        SqlCommand sqlcmd = new SqlCommand(sqlUpdate);
        sqlcmd.Parameters.AddWithValue("@ResourceName", ResourceNameTxt);
        sqlcmd.Parameters.AddWithValue("@Description", DescriptionTxt);
        sqlcmd.Parameters.AddWithValue("@ResourceTypeTaxonomy", ResourceTypeTaxonomy.SelectedValue);
        sqlcmd.Parameters.AddWithValue("@ResourceURLlink", ResourceURLlinkTxt);
        sqlcmd.Parameters.AddWithValue("@ResourceFileType", ResourceFileTypeTxt);
        sqlcmd.Parameters.AddWithValue("@ShowInNewWindow", ShowResourceinPopupTxt);
        sqlcmd.Parameters.AddWithValue("@AdminResourceURL", AdminResourceURL.Text);
        sqlcmd.Parameters.AddWithValue("@AdminUsername", AdminUsername.Text);
        sqlcmd.Parameters.AddWithValue("@AdminPassword", AdminPassword.Text);
        sqlcmd.Parameters.AddWithValue("@File1", File1URL);
        sqlcmd.Parameters.AddWithValue("@File2", File2URL);
        sqlcmd.Parameters.AddWithValue("@File3", File3URL);
        sqlcmd.Parameters.AddWithValue("@ListInResearchGuideFile1", ListInResearchGuideFile1);
        sqlcmd.Parameters.AddWithValue("@ListInResearchGuideFile2", ListInResearchGuideFile2);
        sqlcmd.Parameters.AddWithValue("@ListInResearchGuideFile3", ListInResearchGuideFile3);
        sqlcmd.Parameters.AddWithValue("@SubjectAreasTaxonomy", SubjectAreasTaxonomySelect);
        sqlcmd.Parameters.AddWithValue("@ShowInSubjectAreas", ShowInSubjectAreasTxt);
        sqlcmd.Parameters.AddWithValue("@ShowInDatabases", ShowInDatabasesTxt);
        sqlcmd.Parameters.AddWithValue("@ShowInTrainingRequestForm", ShowInTrainingRequestFormTxt);
        sqlcmd.Parameters.AddWithValue("@ShowInAudienceToolsTaxonomy", ShowInAudienceToolsTaxonomySelect);
        sqlcmd.Parameters.AddWithValue("@Mandatory", ApplicationIsTxt);
        sqlcmd.Parameters.AddWithValue("@AssociatedNetwork", AssociatedNetworkTxt);
        sqlcmd.Parameters.AddWithValue("@AccessTypeTaxonomy", AccessTypeTaxonomyTxt);
        sqlcmd.Parameters.AddWithValue("@ResourceRegistrationInstructions", ResourceRegistrationInstructionsTxt);
        sqlcmd.Parameters.AddWithValue("@SharedUsername", ShareUsernameTxt);
        sqlcmd.Parameters.AddWithValue("@SharedPassword", SharedPasswordTxt);
        sqlcmd.Parameters.AddWithValue("@ShowLogin", ShowLoginTxt);
        sqlcmd.Parameters.AddWithValue("@LimitedNumberPasswordsAvailable", LimitedNumberPasswordsAvailableTxt);
        sqlcmd.Parameters.AddWithValue("@PasswordsAvailable", PasswordsAvailableTxt);
        sqlcmd.Parameters.AddWithValue("@SendEpasswordTo", SendEpasswordToSelect);
        sqlcmd.Parameters.AddWithValue("@PasswordRequestsRestrictedToManagers", PasswordRequestsRestrictedToManagersTxt);
        sqlcmd.Parameters.AddWithValue("@TargetUsers", TargetUsersTxt);
        sqlcmd.Parameters.AddWithValue("@BusinessPurposeOfResource", BusinessPurposeOfResourceTxt);
        sqlcmd.Parameters.AddWithValue("@ResourceDisplayStatus", ResourceDisplayStatusTxt);

        DataBase.executeCommandWithParameters(sqlcmd);
        // Response.Write(sqlUpdate);
        //  DataBase.executeCommand(sqlUpdate);
        AuditLogs.log_Changes(ResourceIDTxt, "Resources");
        // Response.Write(Request.Form);


        if ((Request.Form != null))
        {
            // Response.Write(Request.Form);
            // try
            //  {

            // Response.Write(ResourceIDTxt);

            string sqlContract = string.Empty;

            string[] ContractIDs = this.Request.Form.GetValues("contractCounts");
            foreach (string contractID in ContractIDs)
            {
                //string RID =  TestNull(Request.Form["ResourceID"].ToString()); 
                string AddContract = TestNull(Request.Form["AddContract" + contractID].ToString());
                string FiscalYearSQL = TestNull(Request.Form["FiscalYear" + contractID].ToString());  //
                string PeriodofPerformanceStartSQL = TestNull(Request.Form["PeriodofPerformanceStart" + contractID].ToString());  //TestNull(PeriodofPerformanceStart[iC].ToString());
                string PeriodofPerformanceEndSQL = TestNull(Request.Form["PeriodofPerformanceEnd" + contractID].ToString());  //TestNull(PeriodofPerformanceEnd[iC].ToString());
                string RequisitionNumberSQL = TestNull(Request.Form["RequisitionNumber" + contractID].ToString());  // TestNull(RequisitionNumber[iC].ToString());
                string ContractNumberSQL = TestNull(Request.Form["ContractNumber" + contractID].ToString());  //TestNull(ContractNumber[iC].ToString());
                string NumberOfLicensesOwnedSQL = TestNull(Request.Form["HowManyLicenses" + contractID].ToString());  // TestNull(NumberOfLicensesOwned[iC].ToString());
                string AnnualContractCostSQL = TestNull(Request.Form["AnnualContractCost" + contractID].ToString());  //TestNull(AnnualContractCost[iC].ToString());
                string LicensesCountSQL = TestNull(Request.Form["limitedToLicensesCount" + contractID].ToString());
                string ProcurementMethodOtherSQL = ""; // TestNull(Request.Form["ProcurementMethodOther" + contractID].ToString()); 
                string ContractFileNameSQL = ""; // TestNull(Request.Files["ContractFileName" + contractID].ToString());

                string OldPDFUrl = "";
                try
                {
                    OldPDFUrl = TestNull(Request.Form["oldFileURL" + contractID].ToString());
                }
                catch { }

                string deleteContractFile = TestNull(Request.Form["deleteContractFile" + contractID].ToString());
                if (deleteContractFile == "true")
                {
                    ContractFileNameSQL = uploadResourceFiles("ContractFileName" + contractID, true);
                }
                else
                {
                    ContractFileNameSQL = OldPDFUrl;
                }
                string ProcurementMethodSQL = TestNull(Request.Form["ProcurementMethod" + contractID].ToString());
                if (ProcurementMethodSQL == "112")
                {
                    ProcurementMethodOtherSQL = TestNull(Request.Form["ProcurementMethodOther" + contractID].ToString());
                }


                string limitedToLicensesCountSQL = TestNull(Request.Form["limitedToLicensesCount" + contractID].ToString());

                if (AddContract == "False")
                {
                    //sqlContract += "UPDATE [dbo].[ResourcesContract] ";
                    //sqlContract += "SET ";
                    //sqlContract += "[FiscalYear] = '" + FiscalYearSQL + "'";
                    //sqlContract += ",[PeriodofPerformanceStart] = '" + PeriodofPerformanceStartSQL + "'";
                    //sqlContract += ",[PeriodofPerformanceEnd] = '" + PeriodofPerformanceEndSQL + "'";
                    //sqlContract += ",[RequisitionNumber] = '" + RequisitionNumberSQL + "'";
                    //sqlContract += ",[ContractNumber] = '" + ContractNumberSQL + "'";
                    //sqlContract += ",[NumberOfLicensesOwned] = '" + NumberOfLicensesOwnedSQL + "'";
                    //sqlContract += ",[LicensesCount] = '" + LicensesCountSQL + "'";
                    //sqlContract += ",[AnnualContractCost] = '" + AnnualContractCostSQL + "'";
                    //sqlContract += ",[ProcurementMethod] = '" + ProcurementMethodSQL + "'";
                    //if (deleteContractFile == "true")
                    //{
                    //    sqlContract += ",[ContractFileName] = '" + ContractFileNameSQL + "'";
                    //}
                    //sqlContract += ",[CriticalNotes] = '" + CriticalNotesTxt + "'";
                    //sqlContract += ",[NotifyOfExpirationThisManyDaysInAdvance] = '" + NotifyOfExpirationThisManyDaysInAdvance.Text + "'";
                    //sqlContract += ",[LibraryContractingOfficersRepresentative] = '" + LibraryCORSelect + "'";
                    //sqlContract += ",[VendorName] = '" + VendorNameTxt + "'";
                    //sqlContract += ",[RepresentativeName] = '" + RepresentativeNameTxt + "'";
                    //sqlContract += ",[RepresentativeEmail] = '" + VendorEmailTxt + "'";
                    //sqlContract += ",[RepresentativePhone] = '" + VendorPhoneTxt + "'";
                    //sqlContract += ",[TechnicalContactName] = '" + VendorTechnicalAssistanceNameTxt + "'";
                    //sqlContract += ",[TechnicalContactEmail] = '" + VendorTechnicalAssistanceEmailTxt + "'";
                    //sqlContract += ",[TechnicalContactPhone] = '" + VendorTechnicalAssistancePhoneTxt + "'";
                    //sqlContract += ",[NewFeatures] = '" + NewFeaturesDescriptionTxt + "'";
                    //sqlContract += ",[NotificationActiveStartDate] = '" + NewFeatureStartDateTxt + "'";
                    //sqlContract += ",[NotificationActiveEndDate] = '" + NewFeatureEndDateTxt + "'";
                    //sqlContract += " WHERE ID='" + contractID + "';";




                    sqlContract = "   UPDATE [dbo].[ResourcesContract]    SET  [FiscalYear] = @FiscalYear,[PeriodofPerformanceStart] = @PeriodofPerformanceStart,[PeriodofPerformanceEnd] = @PeriodofPerformanceEnd,[RequisitionNumber] = @RequisitionNumber,[ContractNumber] = @ContractNumber,[NumberOfLicensesOwned] = @NumberOfLicensesOwned,[LicensesCount] = @LicensesCount,[AnnualContractCost] = @AnnualContractCost,[ProcurementMethod] = @ProcurementMethod,[ProcurementMethodOther] = @ProcurementMethodOther,[ContractFileName] = @ContractFileName,[CriticalNotes] = @CriticalNotes,[NotifyOfExpirationThisManyDaysInAdvance] = @NotifyOfExpirationThisManyDaysInAdvance,[LibraryContractingOfficersRepresentative] = @LibraryContractingOfficersRepresentative,[VendorName] = @VendorName,[RepresentativeName] = @RepresentativeName,[RepresentativeEmail] = @RepresentativeEmail,[RepresentativePhone] = @RepresentativePhone,[TechnicalContactName] = @TechnicalContactName,[TechnicalContactEmail] = @TechnicalContactEmail,[TechnicalContactPhone] = @TechnicalContactPhone,[NewFeatures] = @NewFeatures,[NotificationActiveStartDate] = @NotificationActiveStartDate,[NotificationActiveEndDate] = @NotificationActiveEndDate ";
                    sqlContract += " WHERE ID='" + contractID + "';";
                    SqlCommand sqlcmdContract = new SqlCommand(sqlContract);
                    sqlcmdContract.Parameters.AddWithValue("@FiscalYear", FiscalYearSQL);
                    sqlcmdContract.Parameters.AddWithValue("@PeriodofPerformanceStart", PeriodofPerformanceStartSQL);
                    sqlcmdContract.Parameters.AddWithValue("@PeriodofPerformanceEnd", PeriodofPerformanceEndSQL);
                    sqlcmdContract.Parameters.AddWithValue("@RequisitionNumber", RequisitionNumberSQL);
                    sqlcmdContract.Parameters.AddWithValue("@ContractNumber", ContractNumberSQL);
                    sqlcmdContract.Parameters.AddWithValue("@NumberOfLicensesOwned", NumberOfLicensesOwnedSQL);
                    sqlcmdContract.Parameters.AddWithValue("@LicensesCount", LicensesCountSQL);
                    sqlcmdContract.Parameters.AddWithValue("@AnnualContractCost", AnnualContractCostSQL);
                    sqlcmdContract.Parameters.AddWithValue("@ProcurementMethod", ProcurementMethodSQL);
                    sqlcmdContract.Parameters.AddWithValue("@ProcurementMethodOther", ProcurementMethodOtherSQL);
                    sqlcmdContract.Parameters.AddWithValue("@ContractFileName", ContractFileNameSQL);
                    sqlcmdContract.Parameters.AddWithValue("@CriticalNotes", CriticalNotesTxt);
                    sqlcmdContract.Parameters.AddWithValue("@NotifyOfExpirationThisManyDaysInAdvance", NotifyOfExpirationThisManyDaysInAdvance.Text);
                    sqlcmdContract.Parameters.AddWithValue("@LibraryContractingOfficersRepresentative", LibraryCORSelect);
                    sqlcmdContract.Parameters.AddWithValue("@VendorName", VendorNameTxt);
                    sqlcmdContract.Parameters.AddWithValue("@RepresentativeName", RepresentativeNameTxt);
                    sqlcmdContract.Parameters.AddWithValue("@RepresentativeEmail", VendorEmailTxt);
                    sqlcmdContract.Parameters.AddWithValue("@RepresentativePhone", VendorPhoneTxt);
                    sqlcmdContract.Parameters.AddWithValue("@TechnicalContactName", VendorTechnicalAssistanceNameTxt);
                    sqlcmdContract.Parameters.AddWithValue("@TechnicalContactEmail", VendorTechnicalAssistanceEmailTxt);
                    sqlcmdContract.Parameters.AddWithValue("@TechnicalContactPhone", VendorTechnicalAssistancePhoneTxt);
                    sqlcmdContract.Parameters.AddWithValue("@NewFeatures", NewFeaturesDescriptionTxt);
                    sqlcmdContract.Parameters.AddWithValue("@NotificationActiveStartDate", NewFeatureStartDateTxt);
                    sqlcmdContract.Parameters.AddWithValue("@NotificationActiveEndDate", NewFeatureEndDateTxt);
                    DataBase.executeCommandWithParameters(sqlcmdContract);
                }
                if (AddContract == "True")
                {
                    //sqlContract += "INSERT INTO [dbo].[ResourcesContract]([ResourceID],[FiscalYear],[PeriodofPerformanceStart],[PeriodofPerformanceEnd],[RequisitionNumber],[ContractNumber],[NumberOfLicensesOwned],[AnnualContractCost],[ProcurementMethod],[ProcurementMethodOther],[ContractFileName],[CriticalNotes],[NotifyOfExpirationThisManyDaysInAdvance],[LibraryContractingOfficersRepresentative],[VendorName],[RepresentativeName],[RepresentativeEmail],[RepresentativePhone],[TechnicalContactName],[TechnicalContactEmail],[TechnicalContactPhone],[NewFeatures],[NotificationActiveStartDate],[NotificationActiveEndDate] , LicensesCount)";
                    //sqlContract += " VALUES ( ";
                    //sqlContract += "  '" + ResourceIDTxt + "','" + FiscalYearSQL + "','" + PeriodofPerformanceStartSQL + "','" + PeriodofPerformanceEndSQL + "','" + RequisitionNumberSQL + "','" + ContractNumberSQL + "','";
                    //sqlContract += NumberOfLicensesOwnedSQL + "','" + AnnualContractCostSQL + "','" + ProcurementMethodSQL + "','" + ProcurementMethodOtherSQL + "','";
                    //sqlContract += ContractFileNameSQL + "','" + CriticalNotes.Text + "','" + NotifyOfExpirationThisManyDaysInAdvance.Text + "','" + LibraryCORSelect + "','";
                    //sqlContract += VendorNameTxt + "','" + RepresentativeNameTxt + "','" + VendorEmailTxt + "','" + VendorPhoneTxt + "','" + VendorTechnicalAssistanceNameTxt + "','";
                    //sqlContract += VendorTechnicalAssistanceEmailTxt + "','" + VendorTechnicalAssistancePhoneTxt + "','" + NewFeaturesDescriptionTxt + "','" + NewFeatureStartDateTxt + "','" + NewFeatureEndDateTxt + "','" + limitedToLicensesCountSQL + "') ; ";

                    sqlContract = "INSERT INTO [dbo].[ResourcesContract]([ResourceID],[FiscalYear],[PeriodofPerformanceStart],[PeriodofPerformanceEnd],[RequisitionNumber],[ContractNumber],[NumberOfLicensesOwned],[LicensesCount],[AnnualContractCost],[ProcurementMethod],[ProcurementMethodOther],[ContractFileName],[CriticalNotes],[NotifyOfExpirationThisManyDaysInAdvance],[LibraryContractingOfficersRepresentative],[VendorName],[RepresentativeName],[RepresentativeEmail],[RepresentativePhone],[TechnicalContactName],[TechnicalContactEmail],[TechnicalContactPhone],[NewFeatures],[NotificationActiveStartDate],[NotificationActiveEndDate])";
                    sqlContract += "VALUES(@ResourceID,@FiscalYear,@PeriodofPerformanceStart,@PeriodofPerformanceEnd,@RequisitionNumber,@ContractNumber,@NumberOfLicensesOwned,@LicensesCount,@AnnualContractCost,@ProcurementMethod,@ProcurementMethodOther,@ContractFileName,@CriticalNotes,@NotifyOfExpirationThisManyDaysInAdvance,@LibraryContractingOfficersRepresentative,@VendorName,@RepresentativeName,@RepresentativeEmail,@RepresentativePhone,@TechnicalContactName,@TechnicalContactEmail,@TechnicalContactPhone,@NewFeatures,@NotificationActiveStartDate,@NotificationActiveEndDate)";

                    SqlCommand sqlcmdContract = new SqlCommand(sqlContract);
                    sqlcmdContract.Parameters.AddWithValue("@ResourceID", ResourceIDTxt);
                    sqlcmdContract.Parameters.AddWithValue("@FiscalYear", FiscalYearSQL);
                    sqlcmdContract.Parameters.AddWithValue("@PeriodofPerformanceStart", PeriodofPerformanceStartSQL);
                    sqlcmdContract.Parameters.AddWithValue("@PeriodofPerformanceEnd", PeriodofPerformanceEndSQL);
                    sqlcmdContract.Parameters.AddWithValue("@RequisitionNumber", RequisitionNumberSQL);
                    sqlcmdContract.Parameters.AddWithValue("@ContractNumber", ContractNumberSQL);
                    sqlcmdContract.Parameters.AddWithValue("@NumberOfLicensesOwned", NumberOfLicensesOwnedSQL);
                    sqlcmdContract.Parameters.AddWithValue("@LicensesCount", limitedToLicensesCountSQL);
                    sqlcmdContract.Parameters.AddWithValue("@AnnualContractCost", AnnualContractCostSQL);
                    sqlcmdContract.Parameters.AddWithValue("@ProcurementMethod", ProcurementMethodSQL);
                    sqlcmdContract.Parameters.AddWithValue("@ProcurementMethodOther", ProcurementMethodOtherSQL);
                    sqlcmdContract.Parameters.AddWithValue("@ContractFileName", ContractFileNameSQL);
                    sqlcmdContract.Parameters.AddWithValue("@CriticalNotes", CriticalNotesTxt);
                    sqlcmdContract.Parameters.AddWithValue("@NotifyOfExpirationThisManyDaysInAdvance", NotifyOfExpirationThisManyDaysInAdvance.Text);
                    sqlcmdContract.Parameters.AddWithValue("@LibraryContractingOfficersRepresentative", LibraryCORSelect);
                    sqlcmdContract.Parameters.AddWithValue("@VendorName", VendorNameTxt);
                    sqlcmdContract.Parameters.AddWithValue("@RepresentativeName", RepresentativeNameTxt);
                    sqlcmdContract.Parameters.AddWithValue("@RepresentativeEmail", VendorEmailTxt);
                    sqlcmdContract.Parameters.AddWithValue("@RepresentativePhone", VendorPhoneTxt);
                    sqlcmdContract.Parameters.AddWithValue("@TechnicalContactName", VendorTechnicalAssistanceNameTxt);
                    sqlcmdContract.Parameters.AddWithValue("@TechnicalContactEmail", VendorTechnicalAssistanceEmailTxt);
                    sqlcmdContract.Parameters.AddWithValue("@TechnicalContactPhone", VendorTechnicalAssistancePhoneTxt);
                    sqlcmdContract.Parameters.AddWithValue("@NewFeatures", NewFeaturesDescriptionTxt);
                    sqlcmdContract.Parameters.AddWithValue("@NotificationActiveStartDate", NewFeatureStartDateTxt);
                    sqlcmdContract.Parameters.AddWithValue("@NotificationActiveEndDate", NewFeatureEndDateTxt);
                    DataBase.executeCommandWithParameters(sqlcmdContract);
                }


            }
            // Response.Write(sqlContract);
            //  DataBase.executeCommand(sqlContract);
            foreach (string contractID in ContractIDs)
            {
                AuditLogs.log_Changes(contractID, "ResourcesContract");
            }
            Response.Redirect("/admin/resources/confirm.aspx?panel=edit&rn=" + ResourceNameTxt);
        }
    }

    private bool TestifModified(string which)
    {
        try
        {
            string flag = Request.Form["fileModified" + which].ToString();
            if (flag == "true")
            {


                return true;

            }
            else
            {
                return false;
            }
            //HttpPostedFile file = Request.Files[uploadfileID];
            //if (file != null && file.ContentLength > 0)
            //{
            //    if (file.FileName != )
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}
        }
        catch { return false; }
    }

    private bool TestifNew(string uploadfileID)
    {
        try
        {
            HttpPostedFile file = Request.Files[uploadfileID];
            if (file != null && file.ContentLength > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        catch { return false; }
    }

    protected void ContractTabsLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {

            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            //   System.Data.Common.DbDataRecord item = (System.Data.Common.DbDataRecord)ditem.DataItem;
            DataRowView item = (DataRowView)ditem.DataItem;

            string id = item["ID"].ToString().Trim();
            HyperLink tabViewLinkTitle = (HyperLink)ditem.FindControl("tabViewLinkTitle");

            tabViewLinkTitle.NavigateUrl = "#tabView" + id;
            tabViewLinkTitle.Text = "FY " + item["FiscalYear"].ToString().Trim();

            int ind = e.Item.DisplayIndex + 1;
            if (ind == 1)
            {
                tabViewLinkTitle.CssClass = "on";
            }

        }

    }





    // public string itemCount { get; set; }
    protected void ContractContentLV_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            DataRowView item = (DataRowView)ditem.DataItem;

            bool thisYearPresent = false;
            string optionsFYYear = "";
            string id = item["ID"].ToString().Trim();
            string displayYearDB = item["FiscalYear"].ToString().Trim();

            Literal fiscalYearList = (Literal)ditem.FindControl("fiscalYearList");
            // Response.Write(ProcumentMethodList.Count.ToString());



            int currentYear = DateTime.Now.Year;
            int startYear = DateTime.Now.Year - 1;
            int endYear = DateTime.Now.Year + 5;
            string selectedFYear = "";
            for (int i = startYear; i <= endYear; i++)
            {
                if (i.ToString() == displayYearDB)
                {
                    thisYearPresent = true;
                    selectedFYear = "selected='true'";
                }
                else
                {
                    selectedFYear = "";
                }
                optionsFYYear += "<option " + selectedFYear + ">" + i.ToString() + "</option>";
            }
            if (!thisYearPresent) optionsFYYear += "<option selected='true'>" + displayYearDB + "</option>";

            fiscalYearList.Text = optionsFYYear;




            Literal idview = (Literal)ditem.FindControl("idview");
            int ind = e.Item.DisplayIndex + 1;
            if (ind == 1)
            {
                idview.Text = "id='tabView" + id + "' ";
            }

            idview.Text = "id='tabView" + id + "' style='display:block;' ";


            Literal ProcurementMethodListOptions = (Literal)ditem.FindControl("ProcurementMethodListOptions");
            Literal ProcurementMethodOtherDivLit = (Literal)ditem.FindControl("ProcurementMethodOtherDivLit");
            string ProcurementMethod = item["ProcurementMethod"].ToString().Trim();

            ProcurementMethodListOptions.Text += "<option   value='' >Select One</option>";
            foreach (var pmLO in ProcumentMethodList)
            {
                // Response.Write(pmLO.Value);
                string selected = string.Empty; ;
                if (ProcurementMethod == pmLO.Key)
                {
                    selected = "selected = 'true'";
                }
                else { selected = ""; }
                ProcurementMethodListOptions.Text += "<option " + selected + " value='" + pmLO.Key + "' >" + pmLO.Value + "</option>";
            }



            if (ProcurementMethod == "112")
            {
                ProcurementMethodOtherDivLit.Text = "  style='display:block;' ";
            }
            else
            {
                ProcurementMethodOtherDivLit.Text = " style='display:none;' ";
            }


            Literal noneLicenseChecked = (Literal)ditem.FindControl("noneLicenseChecked");
            Literal limitedToLicensesChecked = (Literal)ditem.FindControl("limitedToLicensesChecked");
            Literal UnlimitedLicensesChecked = (Literal)ditem.FindControl("UnlimitedLicensesChecked");
            string NumberOfLicensesOwned = item["NumberOfLicensesOwned"].ToString().Trim();

            switch (NumberOfLicensesOwned)
            {
                case "None": ;
                    noneLicenseChecked.Text = "checked='true'";
                    break;

                case "limitedTo":
                    limitedToLicensesChecked.Text = "checked='true'";
                    break;

                case "Unlimited":
                    UnlimitedLicensesChecked.Text = "checked='true'";
                    break;


            }

            string ContractFileName = item["ContractFileName"].ToString().Trim();
            Literal fileUploadDetails = (Literal)ditem.FindControl("fileUploadDetails");
            if (ContractFileName != "")
            {
                string fullPath = ContractFileName;
                string filename = Path.GetFileName(ContractFileName);
                string filext = Path.GetExtension(ContractFileName);

                fileUploadDetails.Text = "<div id='contractFile" + id + "'><a href='" + ContractFileName + "' target='_blank'>" + filename + "</a><img src='/admin/framework/images/close.jpg' onclick='deleteUpload(" + id + ")' style='cursor:pointer' /><input type='hidden' name='oldFileURL" + id + "'   id='oldFileURL" + id + "' value='" + ContractFileName + "'/></div>";
            }

        }
    }

    protected string uploadResourceFiles(string uploadfileID, bool isContract = false)
    {
        try
        {
            string thisPathToUse = ResourceName.Text.Replace("'", "").Replace(",", ""); 
            string fileDocument = string.Empty;
            string path = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + thisPathToUse + "\\";
            string pathURL = commonfunctions.host + "/uploadedfiles/Resources/" + thisPathToUse + "/";
            string contractpathURL = commonfunctions.host + "/uploadedfiles/Resources/" + thisPathToUse + "/Contracts/";
            string contractpath = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + thisPathToUse + "\\Contracts\\";
            try { createDirectory(path); }
            catch { }
            try { createDirectory(contractpath); }
            catch { }

            if (isContract) path = contractpath;
            if (isContract) pathURL = contractpathURL;

            HttpPostedFile file = Request.Files[uploadfileID];
            if (file != null && file.ContentLength > 0)
            {
                fileDocument = Path.GetFileName(file.FileName);
                file.SaveAs(path + fileDocument);
                //return path + fileDocument;
                return pathURL + fileDocument;
            }
            else
            {
                return "";
            }
        }
        catch { return ""; }

    }
    //protected string uploadResourceFiles(string uploadfileID, bool isContract = false)
    //{
    //    try
    //    {
    //        string fileDocument = string.Empty;
    //        string path = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + ResourceName.Text + "\\";
    //        string contractpath = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + ResourceName.Text + "\\Contracts\\";
    //        try { createDirectory(path); }
    //        catch { }
    //        try { createDirectory(contractpath); }
    //        catch { }

    //        if (isContract) path = contractpath;

    //        HttpPostedFile file = Request.Files[uploadfileID];
    //        if (file != null && file.ContentLength > 0)
    //        {
    //            fileDocument = DateTime.Now.ToString("ddmmyyyhhmmss") + file.FileName.ToString();
    //            file.SaveAs(path + fileDocument);
    //            return path + fileDocument;
    //        }
    //        else
    //        {
    //            return "";
    //        }
    //    }
    //    catch { return ""; }

    //}

    public static String TestNull(string s)
    {
        if (String.IsNullOrEmpty(s))
            return "";
        else
            return s;
    }


    private ArrayList retArraySpecial(string Key)
    {
        int HowManyContracts = this.Request.Form.GetValues("contractCounts").Length;
        string[] ContractIDs = this.Request.Form.GetValues("contractCounts");
        ArrayList arL = new ArrayList();
        string useThis = Key;

        try
        {
            //  for (int i = 1; i <= HowManyContracts; i++)
            foreach (string i in ContractIDs)
            {

                string[] values = this.Request.Form.GetValues(Key + i);


                foreach (string value in values)
                {
                    string res = value;
                    if (res == "Select Year" || res == "") res = "Empty";
                    arL.Add(res);
                }
            }

            return arL;


        }
        catch { return arL; }
    }
    //private string HowManyContracts()
    //{
    //    string HowManyContractsSubmitted = "";
    //    if ((Request.Form != null))
    //    {
    //        Request.Form.c
    //    }
    //    return HowManyContractsSubmitted;

    //}
    private ArrayList retArray(string[] values)
    {
        int FYCounts = this.Request.Form.GetValues("contractCounts").Length;

        ArrayList arL = new ArrayList();
        try
        {

            foreach (string value in values)
            {
                string res = value;
                if (res == "Select Year" || res == "") res = "Empty";
                arL.Add(res);
            }



            return arL;
        }
        catch { return arL; }

    }
    protected string uploadFiles(FileUpload fiU)
    {

        string thisPathToUse = ResourceName.Text.Replace("'", "").Replace(",", ""); 
        string pathURL = "";
        if (fiU.HasFile)
        {
            string path = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + thisPathToUse + "\\";

            try
            {
                createDirectory(path);
            }
            catch { }
            string URL_PATHDB = commonfunctions.host + "/uploadedfiles/Resources/" + thisPathToUse + "/" + fiU.FileName;
            pathURL = path + fiU.FileName;
            fiU.SaveAs(pathURL);
            return URL_PATHDB;
        }
        else
        {
            return "";
        }

    }
    //protected string uploadFiles(FileUpload fiU)
    //{
    //    string pathURL = "";
    //    if (fiU.HasFile)
    //    {
    //        string path = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + ResourceName.Text + "\\";
    //        try
    //        {
    //            createDirectory(path);
    //        }
    //        catch { }

    //        pathURL = path + fiU.FileName;
    //        fiU.SaveAs(pathURL);
    //        return pathURL;
    //    }
    //    else
    //    {
    //        return "";
    //    }

    //}



    public string returnSelectedItemsValue(ListBox lsBox)
    {
        string seletectedItemsValue = ","; // string.Empty;

        foreach (ListItem li in lsBox.Items)
        {
            if (li.Selected == true)
            {
                seletectedItemsValue += li.Value + ",";
            }
        }

        return seletectedItemsValue;
    }



    public static void createDirectory(string path)
    {
        // Specify the directory you want to manipulate. 
        // string path = @"c:\MyDir";

        try
        {
            // Determine whether the directory exists. 
            if (Directory.Exists(path))
            {

                return;
            }

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(path);
            // Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

            // Delete the directory.
            //    di.Delete();
            //  Console.WriteLine("The directory was deleted successfully.");
        }
        catch (Exception e)
        {
            //  Console.WriteLine("The process failed: {0}", e.ToString());
        }
        finally { }
    }



    private DataTable getAllAdminUsers(string addSQL = "")
    {

        string sql = "SELECT ID, CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [users]  where Active='Y' " + addSQL + " order by FirstName ASC";
        return DataBase.dbDataTable(sql);



    }


    public virtual string userID
    {
        get
        {
            if (ViewState["userID"] == null)
            {
                ViewState["userID"] = Request.QueryString["userid"].ToString();
            }
            return (string)ViewState["userID"];
        }
        set
        {
            ViewState["userID"] = value;
        }
    }

    public string CORS { get; set; }
    public string AccessTypeOptions { get; set; }

    public string AccessLevelS { get; set; }
    // public string optionsFYYear { get; set; }

    public string ActiveS { get; set; }
    protected void AccessTypeTaxonomy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    public string resourceid { get; set; }


    public string ResourceNameS { get; set; }


    public string[] taxIds { get; set; }

    public string ResourceNameDB { get; set; }

    public string DescriptionDB { get; set; }

    public string ResourceTypeTaxonomyDB { get; set; }

    public string ShowInNewWindowDB { get; set; }

    public string AdminUsernameDB { get; set; }

    public string AdminResourceURLDB { get; set; }

    public string ResourceURLlinkDB { get; set; }

    public string AdminPasswordDB { get; set; }

    public string File1DB { get; set; }

    public string File2DB { get; set; }

    public string File3DB { get; set; }

    public string SubjectAreasTaxonomyDB { get; set; }

    public string ShowInSubjectAreasDB { get; set; }

    public string ShowInDatabasesDB { get; set; }

    public string ShowInTrainingRequestFormDB { get; set; }

    public string ShowInAudienceToolsTaxonomyDB { get; set; }

    public string MandatoryDB { get; set; }

    public string AssociatedNetworkDB { get; set; }

    public string AccessTypeTaxonomyDB { get; set; }

    public string ResourceRegistrationInstructionsDB { get; set; }

    public string SharedUsernameDB { get; set; }

    public string SharedPasswordDB { get; set; }

    public string ShowLoginDB { get; set; }

    public string LimitedNumberPasswordsAvailableDB { get; set; }

    public string PasswordsAvailableDB { get; set; }

    public string SendEpasswordToDB { get; set; }

    public string PasswordRequestsRestrictedToManagersDB { get; set; }

    public string TargetUsersDB { get; set; }

    public string BusinessPurposeOfResourceDB { get; set; }

    public string ResourceDisplayStatusDB { get; set; }

    public string LinktotheResourceDivStyle { get; set; }
    public string uploadafileDivStyle { get; set; }

    public string selfRegisterDivStyle { get; set; }

    public string SharedLoginDivStyle { get; set; }

    public string ePassDivStyle { get; set; }

    public virtual Dictionary<string, string> OldDBValues
    {
        get
        {
            if (ViewState["OldDBValues"] == null)
            {
                ViewState["OldDBValues"] = AdminFunc.GetIntialDBValuesSQL("Select * from Resources where ID = '" + Request.QueryString["resourceID"] + "';");
            }
            return (Dictionary<string, string>)ViewState["OldDBValues"];
        }
        set
        {
            ViewState["OldDBValues"] = value;
        }
    }

    public virtual Dictionary<string, string> ProcumentMethodList
    {
        get
        {
            if (ViewState["ProcumentMethodList"] == null)
            {
                ViewState["ProcumentMethodList"] = AdminFunc.GetProcumentMethodList();
            }
            return (Dictionary<string, string>)ViewState["ProcumentMethodList"];
        }
        set
        {
            ViewState["ProcumentMethodList"] = value;
        }
    }




    public string ResourceFileTypeDB { get; set; }
    public string file11Label { get; set; }
    public string file1Label { get; set; }
    public string file2Label { get; set; }
    public string file3Label { get; set; }

    public string ListInResearchGuideFile1DB { get; set; }

    public string ListInResearchGuideFile2DB { get; set; }

    public string ListInResearchGuideFile3DB { get; set; }

    public string File11DB { get; set; }
}