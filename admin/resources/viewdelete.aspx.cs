using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;
public partial class admin_resources_view : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


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


    private DataTable getAllAdminUsers()
    {
        string sql = "SELECT ID, CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [users] order by FirstName ASC";
        return DataBase.dbDataTable(sql);
    }
    private void getItem(string resourceid)
    {
        DataSet dt = new DataSet();
        dt = DataBase.dbDataSet("Select * from Resources where ID = '" + resourceid + "';");

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


        ResourceNameS = ResourceNameDB;
        ResourceName.Text =  ResourceNameDB;

        Description.Text =  DescriptionDB;

       
        ResourceTypeTaxonomy.Items.FindByValue(ResourceTypeTaxonomyDB).Selected = true;

        ResourceURLlink.Text =   ResourceURLlinkDB;

        ShowInNewWindow.Items.FindByValue(ShowInNewWindowDB).Selected = true;
 

        AdminResourceURL.Text =   AdminResourceURLDB;
        AdminUsername.Text =   AdminUsernameDB;
        AdminPassword.Text =   AdminPasswordDB;

        
            taxIds = SubjectAreasTaxonomyDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string taxId in taxIds)
            {
                
                SubjectAreasTaxonomyLabel.Text += SubjectAreasTaxonomy.Items.FindByValue(taxId).Text + " <br/>";
            }


            file1Label.Text = File1DB;
            file2Label.Text = File2DB;
            file3Label.Text = File3DB;

        ShowInSubjectAreas.Items.FindByValue(ShowInSubjectAreasDB).Selected = true;
       

        ShowInDatabases.Items.FindByValue(ShowInDatabasesDB).Selected = true;
     

        ShowInTrainingRequestForm.Items.FindByValue(ShowInTrainingRequestFormDB).Selected = true;
     

        ShowInAudienceToolsTaxonomy.Items.FindByValue(ShowInAudienceToolsTaxonomyDB).Selected = true;
  

        Mandatory.Items.FindByValue(MandatoryDB).Selected = true;
 

        AssociatedNetwork.Text =  AssociatedNetworkDB;

 
            //AccessTypeTaxonomyDB
            taxIds = AccessTypeTaxonomyDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string taxId in taxIds)
            {
                
                AccessTypeTaxonomyLabel.Text += AccessTypeTaxonomy.Items.FindByValue(taxId).Text + "<br/>";
            }
    

        ResourceRegistrationInstructions.Text =   ResourceRegistrationInstructionsDB;
        SharedUsername.Text =  SharedUsernameDB;
        SharedPassword.Text =   SharedPasswordDB;

        
        ShowLogin.Items.FindByValue(ShowLoginDB).Selected = true;

       
        LimitedNumberPasswordsAvailable.Items.FindByValue(LimitedNumberPasswordsAvailableDB).Selected = true;
        PasswordsAvailable.Text =   PasswordsAvailableDB;

 
            taxIds = SendEpasswordToDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string taxId in taxIds)
            {
               // SendEpasswordTo.Items.FindByValue(taxId).Selected = true;
                SendEpasswordToLabel.Text += SendEpasswordTo.Items.FindByValue(taxId).Text + "<br/>";
            }
   
        PasswordRequestsRestrictedToManagers.Items.FindByValue(PasswordRequestsRestrictedToManagersDB).Selected = true;

        TargetUsers.Text =   TargetUsersDB;
        BusinessPurposeOfResource.Text = BusinessPurposeOfResourceDB;

        ResourceDisplayStatus.Items.FindByValue(ResourceDisplayStatusDB).Selected = true;
     
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
}