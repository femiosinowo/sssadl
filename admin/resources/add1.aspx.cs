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

public partial class admin_Default2 : System.Web.UI.Page
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
            li.Selected = true;
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

        }
    }

    private DataTable getAllAdminUsers()
    {

        string sql = "SELECT ID, CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [users] order by FirstName ASC";
        return DataBase.dbDataTable(sql);

 

    }



    protected void createResource_Click(object sender, EventArgs e)
    {
        ///selected ResourceTypeTaxonomy      
        //  string ResourceTypeTaxonomySelect = returnSelectedItemsValue(ResourceTypeTaxonomy);
        string SubjectAreasTaxonomySelect =  returnSelectedItemsValue(SubjectAreasTaxonomy);
        string AccessTypeTaxonomySelect =   returnSelectedItemsValue(AccessTypeTaxonomy);
        string SendEpasswordToSelect =   returnSelectedItemsValue(SendEpasswordTo);

        //string path = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + ResourceName.Text + "\\";
        //createDirectory(path);

        //lets upload file
        string File1URL = uploadFiles(File1);
        string File2URL = uploadFiles(File2);
        string File3URL = uploadFiles(File3);



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


        string sql = "INSERT INTO [dbo].[Resources]([ResourceName],[Description],[ResourceTypeTaxonomy],[ResourceURLlink],[ShowInNewWindow],[AdminResourceURL],[AdminUsername],[AdminPassword],[File1],[File2],[File3],[SubjectAreasTaxonomy],[ShowInSubjectAreas],[ShowInDatabases],[ShowInTrainingRequestForm],[ShowInAudienceToolsTaxonomy],[Mandatory],[AssociatedNetwork],[AccessTypeTaxonomy],[ResourceRegistrationInstructions],[SharedUsername],[SharedPassword],[ShowLogin],[LimitedNumberPasswordsAvailable],[PasswordsAvailable],[SendEpasswordTo],[PasswordRequestsRestrictedToManagers],[TargetUsers],[BusinessPurposeOfResource],[ResourceDisplayStatus])";
        sql += " VALUES('" + ResourceName.Text + "','" + Description.Text + "','" + ResourceTypeTaxonomy.SelectedValue + "','" + ResourceURLlink.Text + "','" + ShowInNewWindow.SelectedValue + "','" + AdminResourceURL.Text + "','" + AdminUsername.Text + "','" + AdminPassword.Text + "','" + File1URL + "','" + File2URL + "','" + File3URL + "','" + SubjectAreasTaxonomySelect + "','" + ShowInSubjectAreas.SelectedValue + "','" + ShowInDatabases.SelectedValue + "','" + ShowInTrainingRequestForm.SelectedValue + "','" + ShowInAudienceToolsTaxonomy.SelectedValue + "','" + Mandatory.Text + "','" + AssociatedNetwork.Text + "','" + AccessTypeTaxonomySelect + "','" + ResourceRegistrationInstructions.Text + "','" + SharedUsername.Text + "','" + SharedPassword.Text + "','" + ShowLogin.SelectedValue + "','" + LimitedNumberPasswordsAvailable.SelectedValue + "','" + PasswordsAvailable.Text + "','" + SendEpasswordToSelect + "','" + PasswordRequestsRestrictedToManagers.SelectedValue + "','" + TargetUsers.Text + "','" + BusinessPurposeOfResource.Text + "','" + ResourceDisplayStatus.SelectedValue + "')";
       // Response.Write(sql);
        int rr = DataBase.executeCommand(sql);

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

    //protected bool uploadFiles(FileUpload fiU, string pathURL)
    //{
    //    if (fiU.HasFile)
    //    {
    //        fiU.SaveAs(pathURL);
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
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
}