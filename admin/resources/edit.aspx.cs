using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms;
using Ektron.Cms.BusinessObjects.Core.Content;
using System.IO;


public partial class admin_resources_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {


            //Resource Type Taxonomy
            ResourceTypeTaxonomy.DataSource = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(123), "TaxName", "ASC");
            ResourceTypeTaxonomy.DataBind();
            ListItem li = new ListItem();
            li.Text = "- Select Resource Type -";
            li.Value = "";
           // li.Selected = true;
            ResourceTypeTaxonomy.Items.Add(li);
            //Subject Areas Taxonomy
            SubjectAreasTaxonomy.DataSource = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(136), "TaxName", "ASC");
            SubjectAreasTaxonomy.DataBind();

            //Access Type Taxonomy
            AccessTypeTaxonomy.DataSource = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(118), "TaxName", "ASC");
            AccessTypeTaxonomy.DataBind();

            //SendEpasswordTo
            SendEpasswordTo.DataSource = getAllAdminUsers();
            SendEpasswordTo.DataBind();



            resourceid = Request.QueryString["resourceid"].ToString();

            getItem(resourceid);

 
        }
    }


    private void getItem(string resourceid)
    {
        DataSet dt = new DataSet();
        dt = DataBase.dbDataSet("Select * from Resources where ID = '" + resourceid + "';");

        ResourceID.Value = dt.Tables[0].Rows[0].Field<int>("ID").ToString();
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


        ResourceNameS = ResourceNameDB;
        ResourceName.Text = ResourceNameHF.Value= ResourceNameDB;

        Description.Text = DescriptionHF.Value = DescriptionDB;

        ResourceTypeTaxonomyHF.Value = ResourceTypeTaxonomyDB;
        ResourceTypeTaxonomy.Items.FindByValue(ResourceTypeTaxonomyDB).Selected = true;


        File1HF.Value = File1DB;
        File2HF.Value = File2DB;
        File3HF.Value = File3DB;


        ResourceURLlink.Text =ResourceURLlinkHF.Value =  ResourceURLlinkDB;

        ShowInNewWindow.Items.FindByValue(ShowInNewWindowDB).Selected = true;
        ShowInNewWindowHF.Value = ShowInNewWindowDB;

        AdminResourceURL.Text = AdminResourceURLHF.Value = AdminResourceURLDB;
        AdminUsername.Text = AdminUsernameHF.Value = AdminUsernameDB;
        AdminPassword.Text = AdminPasswordHF.Value = AdminPasswordDB;

        SubjectAreasTaxonomyHF.Value = SubjectAreasTaxonomyDB;
        try
        {
            taxIds = SubjectAreasTaxonomyDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string taxId in taxIds)
            {
                SubjectAreasTaxonomy.Items.FindByValue(taxId).Selected = true;
            }
        }
        catch { }

        ShowInSubjectAreas.Items.FindByValue(ShowInSubjectAreasDB).Selected = true;
        ShowInSubjectAreasHF.Value = ShowInSubjectAreasDB;

        ShowInDatabases.Items.FindByValue(ShowInDatabasesDB).Selected = true;
        ShowInDatabasesHF.Value = ShowInDatabasesDB;

        ShowInTrainingRequestForm.Items.FindByValue(ShowInTrainingRequestFormDB).Selected = true;
        ShowInTrainingRequestFormHF.Value = ShowInTrainingRequestFormDB;

        ShowInAudienceToolsTaxonomy.Items.FindByValue(ShowInAudienceToolsTaxonomyDB).Selected = true;
        ShowInAudienceToolsTaxonomyHF.Value = ShowInAudienceToolsTaxonomyDB;

        Mandatory.Items.FindByValue(MandatoryDB).Selected = true;
        MandatoryHF.Value = MandatoryDB;

        AssociatedNetwork.Text = AssociatedNetworkHF.Value = AssociatedNetworkDB;
        


        AccessTypeTaxonomyHF.Value = AccessTypeTaxonomyDB;
        try
        {
            //AccessTypeTaxonomyDB
            taxIds = AccessTypeTaxonomyDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string taxId in taxIds)
            {
                AccessTypeTaxonomy.Items.FindByValue(taxId).Selected = true;
            }
        }
        catch { }

        ResourceRegistrationInstructions.Text = ResourceRegistrationInstructionsHF.Value = ResourceRegistrationInstructionsDB;
        SharedUsername.Text = SharedUsernameHF.Value = SharedUsernameDB;
        SharedPassword.Text = SharedPasswordHF.Value = SharedPasswordDB;

        ShowLoginHF.Value = ShowLoginDB;
        ShowLogin.Items.FindByValue(ShowLoginDB).Selected = true;

        LimitedNumberPasswordsAvailableHF.Value = LimitedNumberPasswordsAvailableDB;
        LimitedNumberPasswordsAvailable.Items.FindByValue(LimitedNumberPasswordsAvailableDB).Selected = true;
        PasswordsAvailable.Text = PasswordsAvailableHF.Value = PasswordsAvailableDB;


        SendEpasswordToHF.Value = SendEpasswordToDB;
        try
        {
            //SendEpasswordToDB
            taxIds = SendEpasswordToDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string taxId in taxIds)
            {
                 SendEpasswordTo.Items.FindByValue(taxId).Selected = true;
            }
        }
        catch { }

        PasswordRequestsRestrictedToManagersHF.Value = PasswordRequestsRestrictedToManagersDB;
        PasswordRequestsRestrictedToManagers.Items.FindByValue(PasswordRequestsRestrictedToManagersDB).Selected = true;

        TargetUsers.Text = TargetUsersHF.Value = TargetUsersDB;
        BusinessPurposeOfResource.Text = BusinessPurposeOfResourceHF.Value = BusinessPurposeOfResourceDB;

        ResourceDisplayStatus.Items.FindByValue(ResourceDisplayStatusDB).Selected = true;
        ResourceDisplayStatusHF.Value = ResourceDisplayStatusDB;
    }





    protected void TrackChanges(object sender, EventArgs e)
    {
        string ColumnName = string.Empty;
        string oldValue = string.Empty;
        string newValue = string.Empty;
        TextBox textBox = sender as TextBox;

        string fieldName = textBox.ID + "HF";
        HiddenField hf = (HiddenField)FindControl(fieldName);
 
        ColumnName = textBox.ID;
        oldValue = hf.Value;
        newValue = textBox.Text;
        saveTracks(ColumnName, oldValue, newValue);
 


    }

    protected void TrackChanges2(object sender, EventArgs e)
    {
        string ColumnName = string.Empty;
        string oldValue = string.Empty;
        string newValue = string.Empty;
        DropDownList DDL = sender as DropDownList;

        string fieldName = DDL.ID + "HF";
        HiddenField hf = (HiddenField)FindControl(fieldName);

        ColumnName = DDL.ID;
        oldValue = hf.Value;
        newValue = DDL.SelectedValue;
        saveTracks(ColumnName, oldValue, newValue);
        
    }

    protected void TrackChanges3(object sender, EventArgs e)
    {
        string ColumnName = string.Empty;
        string oldValue = string.Empty;
        string newValue = string.Empty;
        ListBox lsBox = sender as ListBox;

        string seletectedItemsValue = ",";
        foreach (ListItem li in lsBox.Items)
        {
            if (li.Selected == true)
            {
                seletectedItemsValue += li.Value + ",";
            }
        }

        string fieldName = lsBox.ID + "HF";
        HiddenField hf = (HiddenField)FindControl(fieldName);

        ColumnName = lsBox.ID;
        oldValue = hf.Value;
        newValue = seletectedItemsValue; // lsBox.SelectedValue;
        saveTracks(ColumnName, oldValue, newValue);



    }

    private void saveTracks(string ColumnName , string oldValue , string newValue)
    {
        string sql = "INSERT INTO [dbo].[ResourcesChangeHistory]([ResourceID],[NameOfFieldChanged],[BeforeValue],[AfterValue],[ChangesByPIN],[ChangeDate]) ";
        sql += " VALUES('" + ResourceID.Value + "','" + ColumnName + "','" + oldValue + "','" + newValue + "','" + commonfunctions.myPIN + "',GETDATE())";
        DataBase.executeCommand(sql);
    }

    public string resourceid { get; set; }

    public string ResourceNameS { get; set; }
    private DataTable getAllAdminUsers()
    {
        string sql = "SELECT ID, CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [users] order by FirstName ASC";
        return DataBase.dbDataTable(sql); 
    }

    protected void createResource_Click(object sender, EventArgs e)
    {
        ///selected ResourceTypeTaxonomy      
        //  string ResourceTypeTaxonomySelect = returnSelectedItemsValue(ResourceTypeTaxonomy);
        string SubjectAreasTaxonomySelect = returnSelectedItemsValue(SubjectAreasTaxonomy);
        string AccessTypeTaxonomySelect = returnSelectedItemsValue(AccessTypeTaxonomy);
        string SendEpasswordToSelect = returnSelectedItemsValue(SendEpasswordTo);







        //lets upload file
        string File1URL = uploadFiles(File1);
        string File2URL = uploadFiles(File2);
        string File3URL = uploadFiles(File3);


        if (File1URL != "") saveTracks("File1URL", File1HF.Value, File1URL);
        if (File2URL != "") saveTracks("File2URL", File2HF.Value, File2URL);
        if (File3URL != "") saveTracks("File3URL", File3HF.Value, File3URL);

        if (File1URL == "") File1URL = File1HF.Value;
        if (File2URL == "") File2URL = File2HF.Value;
        if (File3URL == "") File3URL = File3HF.Value;






        //   string sql = "INSERT INTO [dbo].[Resources]([ResourceName],[Description],[ResourceTypeTaxonomy],[ResourceURLlink],[ShowInNewWindow],[AdminResourceURL],[AdminUsername],[AdminPassword],[File1],[File2],[File3],[SubjectAreasTaxonomy],[ShowInSubjectAreas],[ShowInDatabases],[ShowInTrainingRequestForm],[ShowInAudienceToolsTaxonomy],[Mandatory],[AssociatedNetwork],[AccessTypeTaxonomy],[ResourceRegistrationInstructions],[SharedUsername],[SharedPassword],[ShowLogin],[LimitedNumberPasswordsAvailable],[PasswordsAvailable],[SendEpasswordTo],[PasswordRequestsRestrictedToManagers],[TargetUsers],[BusinessPurposeOfResource],[ResourceDisplayStatus])";
        //  sql += " VALUES('" + ResourceName.Text + "','" + Description.Text + "','" + ResourceTypeTaxonomy.SelectedValue + "','" + ResourceURLlink.Text + "','" + ShowInNewWindow.SelectedValue + "','" 
        //+ AdminResourceURL.Text + "','" + AdminUsername.Text + "','" + AdminPassword.Text + "','" + File1URL + "','" + File2URL + "','" + File3URL + "','" + 
        //    SubjectAreasTaxonomySelect + "','" + ShowInSubjectAreas.SelectedValue + "','" + ShowInDatabases.SelectedValue + "','" + ShowInTrainingRequestForm.SelectedValue + "'
        //,'" + ShowInAudienceToolsTaxonomy.SelectedValue + "','" + Mandatory.Text + "','" + AssociatedNetwork.Text + "','" + AccessTypeTaxonomySelect + "','
        //" + ResourceRegistrationInstructions.Text + "','" + SharedUsername.Text + "','" + SharedPassword.Text + "','" + ShowLogin.SelectedValue + "','" 
        //    + LimitedNumberPasswordsAvailable.SelectedValue + "','" + PasswordsAvailable.Text + "','" + SendEpasswordToSelect + "','" +
        //    PasswordRequestsRestrictedToManagers.SelectedValue + "','" + TargetUsers.Text + "','" + BusinessPurposeOfResource.Text + "','" + ResourceDisplayStatus.SelectedValue + "')";
        // 


        string updateSQL = " UPDATE [dbo].[Resources] ";
        updateSQL += " SET [ResourceName] ='" + ResourceName.Text + "' "; //char(50),>
        updateSQL += " ,[Description] ='" + Description.Text + "' "; //text,>
        updateSQL += " ,[ResourceTypeTaxonomy] ='" + ResourceTypeTaxonomy.SelectedValue + "' "; //char(50),>
        updateSQL += " ,[ResourceURLlink] ='" + ResourceURLlink.Text + "' "; //char(50),>
        updateSQL += " ,[ShowInNewWindow] ='" + ShowInNewWindow.SelectedValue + "' "; //char(50),>
        updateSQL += " ,[AdminResourceURL] ='" + AdminResourceURL.Text + "' "; //char(50),>
        updateSQL += " ,[AdminUsername] ='" + AdminUsername.Text + "' "; //char(50),>
        updateSQL += " ,[AdminPassword] ='" + AdminPassword.Text + "' "; //char(50),>
        updateSQL += " ,[File1] ='" + File1URL + "' "; //text,>
        updateSQL += " ,[File2] ='" + File2URL + "' "; //text,>
        updateSQL += " ,[File3] ='" + File3URL + "' "; //text,>
        updateSQL += " ,[SubjectAreasTaxonomy] ='" + SubjectAreasTaxonomySelect + "' "; //text,>
        updateSQL += " ,[ShowInSubjectAreas] ='" + ShowInSubjectAreas.SelectedValue + "' "; //char(10),>
        updateSQL += " ,[ShowInDatabases] ='" + ShowInDatabases.SelectedValue + "' "; //char(10),>
        updateSQL += " ,[ShowInTrainingRequestForm] ='" + ShowInTrainingRequestForm.SelectedValue + "' "; //char(10),>
        updateSQL += " ,[ShowInAudienceToolsTaxonomy] ='" + ShowInAudienceToolsTaxonomy.SelectedValue + "' "; //text,>
        updateSQL += " ,[Mandatory] ='" + Mandatory.Text + "' "; //char(10),>
        updateSQL += " ,[AssociatedNetwork] ='" + AssociatedNetwork.Text + "' "; //char(50),>
        updateSQL += " ,[AccessTypeTaxonomy] ='" + AccessTypeTaxonomySelect + "' "; //text,>
        updateSQL += " ,[ResourceRegistrationInstructions] ='" + ResourceRegistrationInstructions.Text + "' "; //char(50),>
        updateSQL += " ,[SharedUsername] ='" + SharedUsername.Text + "' "; //char(50),>
        updateSQL += " ,[SharedPassword] ='" + SharedPassword.Text + "' "; //char(50),>
        updateSQL += " ,[ShowLogin] ='" + ShowLogin.SelectedValue + "' "; //char(50),>
        updateSQL += " ,[LimitedNumberPasswordsAvailable] ='" + LimitedNumberPasswordsAvailable.SelectedValue + "' "; //char(10),>
        updateSQL += " ,[PasswordsAvailable] ='" + PasswordsAvailable.Text + "' "; //char(10),>
        updateSQL += " ,[SendEpasswordTo] ='" + SendEpasswordToSelect + "' "; //char(50),>
        updateSQL += " ,[PasswordRequestsRestrictedToManagers] ='" + PasswordRequestsRestrictedToManagers.SelectedValue + "' "; //char(10),>
        updateSQL += " ,[TargetUsers] ='" + TargetUsers.Text + "' "; //char(10),>
        updateSQL += " ,[BusinessPurposeOfResource] ='" + BusinessPurposeOfResource.Text + "' "; //char(10),>
        updateSQL += " ,[ResourceDisplayStatus] ='" + ResourceDisplayStatus.SelectedValue + "' "; //char(10),>
        updateSQL += " WHERE ID='" + ResourceID.Value + "'";


        // Response.Write(updateSQL);

        int rr = DataBase.executeCommand(updateSQL);

        if (rr == 1)
        {

        }
        else
        {
            //error inserting
        }
    }

    protected string uploadFiles(FileUpload fiU)
    {
        string pathURL = "";
        if (fiU.HasFile)
        {
            string path = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + ResourceName.Text + "\\";
            try
            {               
                createDirectory(path);
            }
            catch { }

            pathURL = path + fiU.FileName;
            fiU.SaveAs(pathURL);
            return pathURL;
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


    protected void uploadFilesToEktron(FileUpload fiU)
    {

        // folder ID where I want the files to go

        long folderId = 103;

        // create the content item title in the form "YYYY T - CCCC" where Y is year, T is term and C is program code.

        string content_title = "aasdf" + DateTime.Now.ToString();

        // initialize framework.

        Ektron.Cms.API.Library libraryAPI = new Ektron.Cms.API.Library();

        LibraryManager lmgr = new LibraryManager();

        // determine if the item already exists.

        Ektron.Cms.Content.LibraryCriteria criteria = new Ektron.Cms.Content.LibraryCriteria();

        // upload the file to Ektron

        Ektron.Cms.LibraryConfigData lib_setting_data = libraryAPI.GetLibrarySettings(folderId);

        string filename = Server.MapPath(lib_setting_data.FileDirectory) + Path.GetFileName(fiU.FileName);

        Ektron.Cms.LibraryData item = new Ektron.Cms.LibraryData()

        {

            Title = content_title,

            ParentId = folderId,

            FileName = filename,

            File = File1.FileBytes

        };



        if (fiU.PostedFile.ContentType == "application/pdf")
        {

            lmgr.Add(item);

            ltlMessage.Text = "Library item added with ID = " + item.Id.ToString();

        }

        else

            ltlMessage.Text = "Not added. File must be a PDF.";

    }

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
}