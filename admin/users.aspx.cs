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

public partial class admin_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        //Resource Type Taxonomy
        ResourceTypeTaxonomy.DataSource = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(123) , "TaxName", "ASC");
        ResourceTypeTaxonomy.DataBind();

        //Subject Areas Taxonomy
        SubjectAreasTaxonomy.DataSource = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(136), "TaxName", "ASC");
        SubjectAreasTaxonomy.DataBind();

        //Access Type Taxonomy
        AccessTypeTaxonomy.DataSource = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(148), "TaxName", "ASC");
        AccessTypeTaxonomy.DataBind();

        //SendEpasswordTo
        SendEpasswordTo.DataSource = getAllAdminUsers();
        SendEpasswordTo.DataBind();
        
    }

    private DataTable getAllAdminUsers()
    {
         
        string sql = "SELECT * FROM [users] order by FirstName ASC";
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        using (SqlConnection cnn = new SqlConnection(commonfunctions.ektronConnectionStr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = cnn;
                cmd.CommandType = CommandType.Text;

                cnn.Open();



                cmd.CommandText = sql;
                adapter.SelectCommand = cmd;
                adapter.Fill(dt);


            }
        }
        return dt;

    }




    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    if (this.File1.HasFile)
    //    {
    //        this.FileUpload1.SaveAs("c:\\" + this.FileUpload1.FileName);
    //    }
    //}
    protected void createResource_Click(object sender, EventArgs e)
    {


        string sql = "INSERT INTO [dbo].[Resources]([ResourceName],[Description],[ResourceTypeTaxonomy],[ResourceURLlink],[ShowInNewWindow],[AdminResourceURL],[AdminUsername],[AdminPassword],[File1],[File2],[File3],[SubjectAreasTaxonomy],[ShowInSubjectAreas],[ShowInDatabases],[ShowInTrainingRequestForm],[ShowInAudienceToolsTaxonomy],[Mandatory],[AssociatedNetwork],[AccessTypeTaxonomy],[ResourceRegistrationInstructions],[SharedUsername],[SharedPassword],[ShowLogin],[LimitedNumberPasswordsAvailable],[PasswordsAvailable],[SendEpasswordTo],[PasswordRequestsRestrictedToManagers],[TargetUsers],[BusinessPurposeOfResource],[ResourceDisplayStatus])";
        sql += " VALUES(" + ResourceName + "," + Description + "," + ResourceTypeTaxonomy + "," + ResourceURLlink + "," + ShowInNewWindow + "," + AdminResourceURL + "," + AdminUsername + "," + AdminPassword + "," + File1 + "," + File2 + "," + File3 + "," + SubjectAreasTaxonomy + "," + ShowInSubjectAreas + "," + ShowInDatabases + "," + ShowInTrainingRequestForm + "," + ShowInAudienceToolsTaxonomy + "," + Mandatory + "," + AssociatedNetwork + "," + AccessTypeTaxonomy + "," + ResourceRegistrationInstructions + "," + SharedUsername + "," + SharedPassword + "," + ShowLogin + "," + LimitedNumberPasswordsAvailable + "," + PasswordsAvailable + "," + SendEpasswordTo + "," + PasswordRequestsRestrictedToManagers + "," + TargetUsers + "," + BusinessPurposeOfResource + "," + ResourceDisplayStatus + ")";


        using (SqlConnection cnn = new SqlConnection(commonfunctions.ektronConnectionStr))
        {
            using (SqlCommand cmd = new SqlCommand())
            { 

                cmd.CommandType = CommandType.Text;
                cnn.Open();
                cmd.Connection = cnn;      



                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

            }

        }
    }
}