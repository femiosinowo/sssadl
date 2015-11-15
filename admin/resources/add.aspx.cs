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


            ProcurementMethodListOptions += "<option selected = 'true'  value='' >Select One</option>";
            foreach (var pmLO in ProcumentMethodList)
            {


                ProcurementMethodListOptions += "<option   value='" + pmLO.Key + "' >" + pmLO.Value + "</option>";
            }


            ///FYYear
            ///

            string selectedFYear = "";
            int currentYear = DateTime.Now.Year;
            int startYear = DateTime.Now.Year - 1;
            int endYear = DateTime.Now.Year + 5;
            
            for (int i = startYear; i <= endYear; i++)
            {
                if (i.ToString() == currentYear.ToString())
                {
                    selectedFYear = "selected='true'";
                }
                else
                {
                    selectedFYear = "";
                }
                optionsFYYear += "<option " + selectedFYear + ">" + i.ToString() + "</option>";

            }

        }


    }





    

    protected DataTable CreateAccessTypeOptions(long taxID , bool showSelect = false)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("TaxID");
        dt.Columns.Add("TaxName");
        if (showSelect)
        {          
            dt.Rows.Add("", " - Select One - ");
        }
        DataTableReader dtR = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(taxID), "TaxName", "ASC").CreateDataReader();
        while (dtR.Read())
        {
            dt.Rows.Add(dtR["TaxID"].ToString(), dtR["TaxName"].ToString());
        }


        return dt;


    }
    protected void AddResource_Click(object sender, EventArgs e)
    {

        string ResourceNameTxt = ResourceName.Text;
        string DescriptionTxt = Description.Text;
        string ResourceTypeTaxonomySelected = ResourceTypeTaxonomy.SelectedValue;
        //124 Databases
        //125 Journals
        //126 Articles
        //127 eBooks
        //128 Other Resources

        string ResourceURLlinkTxt = "";
        string ResourceFileUploadTxt = "";
        string ShowResourceinPopupTxt = "N";
        string taxId = ResourceTypeTaxonomySelected;
        string ResourceFileTypeTxt = "";

        switch (taxId)
        {
            case "124":
            case "125":
                ResourceURLlinkTxt = ResourceURLlink.Text;

                break;

            case "126":
            case "127":
            case "128":
               // ResourceFileTypeTxt = ResourceLinkRd.Value;
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
        }

        if (ShowResourceinPopup.Checked) ShowResourceinPopupTxt = "Y";

        string AdminResourceURLTxt = AdminResourceURL.Text;
        string AdminUsernameTxt = AdminUsername.Text;
        string AdminPasswordTxt = AdminPassword.Text;


        string File1URL = uploadResourceFiles("fileUpload1"); // uploadFiles(File1);
        string File2URL = uploadResourceFiles("fileUpload2"); //uploadFiles(File2);
        string File3URL = uploadResourceFiles("fileUpload3"); // uploadFiles(File3);


        string ListInResearchGuideFile1 = ListFinResearchGuide("listInRG1"); // uploadResourceFiles("fileUpload1"); // uploadFiles(File1);
        string ListInResearchGuideFile2 = ListFinResearchGuide("listInRG2");  //  uploadResourceFiles("fileUpload2"); //uploadFiles(File2);
        string ListInResearchGuideFile3 = ListFinResearchGuide("listInRG3");  // uploadResourceFiles("fileUpload3"); // uploadFiles(File3);

      
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

        //lets upload file
        //if (uploadFiles(File1, File1URL))
        //{
        //    File1URL = path + File1.FileName;
        //}
        //if (uploadFiles(File2, File2URL))
        //{
        //    File2URL = path + File2.FileName;
        //}
        //if (uploadFiles(File3, File3URL))
        //{
        //    File3URL = path + File3.FileName;
        //}


        //string sql = "INSERT INTO [dbo].[Resources]([ResourceName],[Description],[ResourceTypeTaxonomy],[ResourceURLlink],[ShowInNewWindow],[AdminResourceURL],[AdminUsername],[AdminPassword],[File1],[File2],[File3],[SubjectAreasTaxonomy],";
        //sql += " [ShowInSubjectAreas],[ShowInDatabases],[ShowInTrainingRequestForm],[ShowInAudienceToolsTaxonomy],[Mandatory],[AssociatedNetwork],[AccessTypeTaxonomy],[ResourceRegistrationInstructions],[SharedUsername],";
        //sql += " [SharedPassword],[ShowLogin],[LimitedNumberPasswordsAvailable],[PasswordsAvailable],[SendEpasswordTo],[PasswordRequestsRestrictedToManagers],[TargetUsers],[BusinessPurposeOfResource],[ResourceDisplayStatus] ";
        //sql += " , [ResourceFileType] , [ListInResearchGuideFile1] , [ListInResearchGuideFile2] , [ListInResearchGuideFile3] )";
        //sql += " VALUES('" + ResourceNameTxt + "','" + DescriptionTxt + "','" + ResourceTypeTaxonomy.SelectedValue + "','" + ResourceURLlinkTxt + "','" + ShowResourceinPopupTxt + "','";
        //sql += AdminResourceURL.Text + "','" + AdminUsername.Text + "','" + AdminPassword.Text + "','" + File1URL + "','" + File2URL + "','" + File3URL + "','";
        //sql += SubjectAreasTaxonomySelect + "','" + ShowInSubjectAreasTxt + "','" + ShowInDatabasesTxt + "','" + ShowInTrainingRequestFormTxt + "','";
        //sql += ShowInAudienceToolsTaxonomySelect + "','" + ApplicationIsTxt + "','" + AssociatedNetworkTxt + "','" + AccessTypeTaxonomyTxt + "','" + ResourceRegistrationInstructionsTxt + "','";
        //sql += ShareUsernameTxt + "','" + SharedPasswordTxt + "','" + ShowLoginTxt + "','" + LimitedNumberPasswordsAvailableTxt + "','" + PasswordsAvailableTxt + "','";
        //sql += SendEpasswordToSelect + "','" + PasswordRequestsRestrictedToManagersTxt + "','" + TargetUsersTxt + "','" + BusinessPurposeOfResourceTxt + "','" + ResourceDisplayStatusTxt + "','" + ResourceFileTypeTxt + "','";
        //sql += ListInResearchGuideFile1 + "','" + ListInResearchGuideFile2 + "','" + ListInResearchGuideFile3 + "')";
         // Response.Write(sql);
      //  string ResourceID = DataBase.executeCommanAndReturnSCOPE_IDENTITY(sql);
        //Response.Write(Request.Files);
 
       
        string sql2 = " INSERT INTO [dbo].[Resources]([ResourceName],[Description],[ResourceTypeTaxonomy],[ResourceURLlink],[ResourceFileType],[ShowInNewWindow],[AdminResourceURL],[AdminUsername],[AdminPassword],[File1],[File2],[File3],[ListInResearchGuideFile1],[ListInResearchGuideFile2],[ListInResearchGuideFile3],[SubjectAreasTaxonomy],[ShowInSubjectAreas],[ShowInDatabases],[ShowInTrainingRequestForm],[ShowInAudienceToolsTaxonomy],[Mandatory],[AssociatedNetwork],[AccessTypeTaxonomy],[ResourceRegistrationInstructions],[SharedUsername],[SharedPassword],[ShowLogin],[LimitedNumberPasswordsAvailable],[PasswordsAvailable],[SendEpasswordTo],[PasswordRequestsRestrictedToManagers],[TargetUsers],[BusinessPurposeOfResource],[ResourceDisplayStatus]) ";
     sql2 += " VALUES(@ResourceName,@Description,@ResourceTypeTaxonomy,@ResourceURLlink,@ResourceFileType,@ShowInNewWindow,@AdminResourceURL,@AdminUsername,@AdminPassword,@File1,@File2,@File3,@ListInResearchGuideFile1,@ListInResearchGuideFile2,@ListInResearchGuideFile3,@SubjectAreasTaxonomy,@ShowInSubjectAreas,@ShowInDatabases,@ShowInTrainingRequestForm,@ShowInAudienceToolsTaxonomy,@Mandatory,@AssociatedNetwork,@AccessTypeTaxonomy,@ResourceRegistrationInstructions,@SharedUsername,@SharedPassword,@ShowLogin,@LimitedNumberPasswordsAvailable,@PasswordsAvailable,@SendEpasswordTo ,@PasswordRequestsRestrictedToManagers,@TargetUsers,@BusinessPurposeOfResource,@ResourceDisplayStatus) ";
     SqlCommand sqlcmd = new SqlCommand(sql2);
     sqlcmd.Parameters.AddWithValue("@ResourceName", ResourceNameTxt); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@Description", DescriptionTxt); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@ResourceTypeTaxonomy", ResourceTypeTaxonomy.SelectedValue); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@ResourceURLlink", ResourceURLlinkTxt); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@ResourceFileType", ResourceFileTypeTxt); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@ShowInNewWindow", ShowResourceinPopupTxt); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@AdminResourceURL", AdminResourceURL.Text); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@AdminUsername", AdminUsername.Text); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@AdminPassword", AdminPassword.Text); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@File1", File1URL); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@File2", File2URL); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@File3", File3URL); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@ListInResearchGuideFile1", ListInResearchGuideFile1); //nchar(1),>
     sqlcmd.Parameters.AddWithValue("@ListInResearchGuideFile2", ListInResearchGuideFile2); //nchar(1),>
     sqlcmd.Parameters.AddWithValue("@ListInResearchGuideFile3", ListInResearchGuideFile3); //nchar(1),>
     sqlcmd.Parameters.AddWithValue("@SubjectAreasTaxonomy", SubjectAreasTaxonomySelect); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@ShowInSubjectAreas", ShowInSubjectAreasTxt); //nchar(10),>
     sqlcmd.Parameters.AddWithValue("@ShowInDatabases", ShowInDatabasesTxt); //nchar(10),>
     sqlcmd.Parameters.AddWithValue("@ShowInTrainingRequestForm", ShowInTrainingRequestFormTxt); //nchar(10),>
     sqlcmd.Parameters.AddWithValue("@ShowInAudienceToolsTaxonomy", ShowInAudienceToolsTaxonomySelect); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@Mandatory", ApplicationIsTxt); //nchar(10),>
     sqlcmd.Parameters.AddWithValue("@AssociatedNetwork", AssociatedNetworkTxt); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@AccessTypeTaxonomy", AccessTypeTaxonomyTxt); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@ResourceRegistrationInstructions", ResourceRegistrationInstructionsTxt); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@SharedUsername", ShareUsernameTxt); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@SharedPassword", SharedPasswordTxt); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@ShowLogin", ShowLoginTxt); //nchar(50),>
     sqlcmd.Parameters.AddWithValue("@LimitedNumberPasswordsAvailable", LimitedNumberPasswordsAvailableTxt); //nchar(10),>
     sqlcmd.Parameters.AddWithValue("@PasswordsAvailable", PasswordsAvailableTxt); //nchar(10),>
     sqlcmd.Parameters.AddWithValue("@SendEpasswordTo", SendEpasswordToSelect); //nchar(250),>
     sqlcmd.Parameters.AddWithValue("@PasswordRequestsRestrictedToManagers", PasswordRequestsRestrictedToManagersTxt); //nchar(10),>
     sqlcmd.Parameters.AddWithValue("@TargetUsers", TargetUsersTxt); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@BusinessPurposeOfResource", BusinessPurposeOfResourceTxt); //varchar(max),>
     sqlcmd.Parameters.AddWithValue("@ResourceDisplayStatus", ResourceDisplayStatusTxt); //nchar(10),>)

     string ResourceID = DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmd);


        if (Convert.ToInt16(ResourceID) >0)
        {


            if ((Request.Form != null))
            {
                  // Response.Write(Request.Form);
                string sqlContract = string.Empty;
                string[] ContractIDs = this.Request.Form.GetValues("contractCounts");
                foreach (string contractID in ContractIDs)
                {
                    //string RID =  TestNull(Request.Form["ResourceID"].ToString()); 
                   // string AddContract = TestNull(Request.Form["AddContract" + contractID].ToString());
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
                    string path = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\Contract\\";
                    //string fileDocument = string.Empty;
                    //HttpPostedFile file = Request.Files["ContractFileName" + contractID];
                    //if (file != null && file.ContentLength > 0)
                    //{
                    //    fileDocument = DateTime.Now.ToString("ddmmyyyhhmmss") + file.FileName.ToString();
                    //    file.SaveAs(path + fileDocument);
                    //    ContractFileNameSQL = path + fileDocument;
                    //}

                    ContractFileNameSQL = uploadResourceFiles("ContractFileName" + contractID, true);
                    string ProcurementMethodSQL = TestNull(Request.Form["ProcurementMethod" + contractID].ToString());
                    if (ProcurementMethodSQL == "112")
                    {
                        ProcurementMethodOtherSQL = TestNull(Request.Form["ProcurementMethodOther" + contractID].ToString());
                    }


                    string limitedToLicensesCountSQL = TestNull(Request.Form["limitedToLicensesCount" + contractID].ToString());


                    sqlContract += "INSERT INTO [dbo].[ResourcesContract]([ResourceID],[FiscalYear],[PeriodofPerformanceStart],[PeriodofPerformanceEnd],[RequisitionNumber],[ContractNumber],[NumberOfLicensesOwned],[AnnualContractCost],[ProcurementMethod],[ProcurementMethodOther],[ContractFileName],[CriticalNotes],[NotifyOfExpirationThisManyDaysInAdvance],[LibraryContractingOfficersRepresentative],[VendorName],[RepresentativeName],[RepresentativeEmail],[RepresentativePhone],[TechnicalContactName],[TechnicalContactEmail],[TechnicalContactPhone],[NewFeatures],[NotificationActiveStartDate],[NotificationActiveEndDate] , LicensesCount)";
                    sqlContract += " VALUES ( ";
                    sqlContract += "  '" + ResourceID + "','" + FiscalYearSQL + "','" + PeriodofPerformanceStartSQL + "','" + PeriodofPerformanceEndSQL + "','" + RequisitionNumberSQL + "','" + ContractNumberSQL + "','";
                    sqlContract += NumberOfLicensesOwnedSQL + "','" + AnnualContractCostSQL + "','" + ProcurementMethodSQL + "','" + ProcurementMethodOtherSQL + "','";
                    sqlContract += ContractFileNameSQL + "','" + CriticalNotes.Text + "','" + NotifyOfExpirationThisManyDaysInAdvance.Text + "','" + LibraryCORSelect + "','";
                    sqlContract += VendorNameTxt + "','" + RepresentativeNameTxt + "','" + VendorEmailTxt + "','" + VendorPhoneTxt + "','" + VendorTechnicalAssistanceNameTxt + "','";
                    sqlContract += VendorTechnicalAssistanceEmailTxt + "','" + VendorTechnicalAssistancePhoneTxt + "','" + NewFeaturesDescriptionTxt + "','" + NewFeatureStartDateTxt + "','" + NewFeatureEndDateTxt + "','" + limitedToLicensesCountSQL + "') ; ";


                    sqlContract = "INSERT INTO [dbo].[ResourcesContract]([ResourceID],[FiscalYear],[PeriodofPerformanceStart],[PeriodofPerformanceEnd],[RequisitionNumber],[ContractNumber],[NumberOfLicensesOwned],[LicensesCount],[AnnualContractCost],[ProcurementMethod],[ProcurementMethodOther],[ContractFileName],[CriticalNotes],[NotifyOfExpirationThisManyDaysInAdvance],[LibraryContractingOfficersRepresentative],[VendorName],[RepresentativeName],[RepresentativeEmail],[RepresentativePhone],[TechnicalContactName],[TechnicalContactEmail],[TechnicalContactPhone],[NewFeatures],[NotificationActiveStartDate],[NotificationActiveEndDate])";
                    sqlContract += "VALUES(@ResourceID,@FiscalYear,@PeriodofPerformanceStart,@PeriodofPerformanceEnd,@RequisitionNumber,@ContractNumber,@NumberOfLicensesOwned,@LicensesCount,@AnnualContractCost,@ProcurementMethod,@ProcurementMethodOther,@ContractFileName,@CriticalNotes,@NotifyOfExpirationThisManyDaysInAdvance,@LibraryContractingOfficersRepresentative,@VendorName,@RepresentativeName,@RepresentativeEmail,@RepresentativePhone,@TechnicalContactName,@TechnicalContactEmail,@TechnicalContactPhone,@NewFeatures,@NotificationActiveStartDate,@NotificationActiveEndDate)";

                    SqlCommand sqlcmdContract = new SqlCommand(sqlContract);
                    sqlcmdContract.Parameters.AddWithValue("@ResourceID", ResourceID);
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
               // DataBase.executeCommand(sqlContract);
                Response.Redirect("/admin/resources/confirm.aspx?panel=new&rn=" + ResourceNameTxt);

            }
        }
    }


    public static String TestNull(string s)
    {
        if (String.IsNullOrEmpty(s))
            return "";
        else
            return s;
    }


    private ArrayList retArraySpecial(string Key)
    {
        int FYCounts = this.Request.Form.GetValues("FYYear").Length;
        ArrayList arL = new ArrayList();
        string useThis = Key;
        try
        {
            for (int i = 1; i <= FYCounts; i++)
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
        int FYCounts = this.Request.Form.GetValues("FYYear").Length;

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
        catch {
            return "N";
        }
    }

    protected string uploadResourceFiles(string uploadfileID , bool isContract=false)
    {
       // try
      //  {
            string fileDocument = string.Empty;
            string thisPathToUse = ResourceName.Text.Replace("'", "").Replace(",", ""); 
            string path = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + thisPathToUse + "\\";
            string pathURL = commonfunctions.host + "/uploadedfiles/Resources/" + thisPathToUse + "/";
            string contractpathURL = commonfunctions.host + "/uploadedfiles/Resources/" + thisPathToUse + "/Contracts/";
            string contractpath = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + thisPathToUse + "\\Contracts\\";
            try { createDirectory(path); } catch { }
            try { createDirectory(contractpath); }
            catch { }

            if (isContract) path = contractpath;
            if (isContract) pathURL = contractpathURL;
      

            HttpPostedFile file = Request.Files[uploadfileID];
            if (file != null && file.ContentLength > 0)
            {
               // Response.Write(file.FileName.ToString());
               // fileDocument = DateTime.Now.ToString("ddmmyyyhhmmss") + file.FileName.ToString();
                fileDocument =  Path.GetFileName(file.FileName); //.ToString();

                file.SaveAs(Path.Combine(path , fileDocument));
                //return path + fileDocument;
                return pathURL + fileDocument;
            }
            else
            {
                return "";
            }
      //  }
      //  catch { return ""; }
         
    }

    protected string uploadFiles(FileUpload fiU)
    {
 

        string pathURL = "";
        string thisPathToUse = ResourceName.Text.Replace("'", "").Replace(",", ""); 
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
            fiU.SaveAs(Path.Combine(path ,  fiU.FileName));
            return URL_PATHDB;
        }
        else
        {
            return "";
        }

    }



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
        path = path.Replace("'", "");

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



    private DataTable getAllAdminUsers(string addSQL="")
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
    public string optionsFYYear { get; set; }
    
    public string ActiveS { get; set; }
    protected void AccessTypeTaxonomy_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    public string ProcurementMethodListOptions { get; set; }
   
}