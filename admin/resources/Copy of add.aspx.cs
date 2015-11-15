using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSADL.CMS;

public partial class admin_users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //Resource Type Taxonomy
            ResourceTypeTaxonomy.DataSource = CreateAccessTypeOptions(123, true); // commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(123), "TaxName", "ASC");
            ResourceTypeTaxonomy.DataBind();
            
            //Subject Areas Taxonomy
            SubjectAreasTaxonomy.DataSource = CreateAccessTypeOptions(136); // commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(136), "TaxName", "ASC");
            SubjectAreasTaxonomy.DataBind();

            //Access Type Taxonomy
            AccessTypeTaxonomy.DataSource = CreateAccessTypeOptions(118, true);// commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(118), "TaxName", "ASC");
            AccessTypeTaxonomy.DataBind();

            //Audience

            ShowInAudienceToolsTaxonomy.DataSource = CreateAccessTypeOptions(96);// commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(118), "TaxName", "ASC");
            ShowInAudienceToolsTaxonomy.DataBind();
            //SendEpasswordTo
            SendEpasswordTo.DataSource = getAllAdminUsers();
            SendEpasswordTo.DataBind();
           // CreateAccessTypeOptions();

 
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
    protected void createUser_Click(object sender, EventArgs e)
    {

        //string PINi = PIN.Text;
        //string EmailAddressi = EmailAddressHF.Value;
        //string FirstNamei = FirstNameHF.Value;
        //string LastNamei = LastNameHF.Value;
        //string AccessLeveli = AccessLevel.SelectedValue;
        //string CORi = "N";
        //string CORExpireDatei = CORExpiresOn.Text;
        //string Activei = EmployeeActive.SelectedValue;

        //if (CORYes.Checked) CORi = "Y";


        //string sql = "INSERT INTO [dbo].[users]([PIN],[EmailAddress],[FirstName],[LastName],[AccessLevel],[COR],[CORExpireDate],[Active],[CreateDate])";
        //sql += " VALUES('" + PINi + "','" + EmailAddressi + "','" + FirstNamei + "','" + LastNamei + "','" + AccessLeveli + "','" + CORi + "','" + CORExpireDatei + "','" + Activei + "',GETDATE())";

        ////    Response.Write(sql);

        //int iresult = DataBase.executeCommand(sql);
        //if (iresult == 1)
        //{
        //    successedit.Visible = true;
        //    //  Response.Redirect("/admin/users/success.aspx");
        //}
        //else
        //{

        //    errorEdit.Visible = true;
        //}


    }

    private DataTable getAllAdminUsers()
    {

        string sql = "SELECT ID, CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [users] order by FirstName ASC";
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

    public string ActiveS { get; set; }
    protected void AccessTypeTaxonomy_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}