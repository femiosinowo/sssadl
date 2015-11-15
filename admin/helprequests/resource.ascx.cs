using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;

public partial class admin_helprequests_resource : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dtAllResources();
        if (!IsPostBack)
        {
            

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("ResourceName");

            dt.Rows.Add("", " - Select One - ");

            DataTableReader dtR = resourcesDt.CreateDataReader();
            while (dtR.Read())
            {
                dt.Rows.Add(dtR["ID"].ToString(), dtR["ResourceName"].ToString());
            }

            ResourceDD.DataSource = dt; // DataBase.dbDataTable("Select * from HelpRequestsAssignees ");
            ResourceDD.DataBind();

            try
            {
                ResourceDD.Items.FindByValue(SelectThisResource).Selected = true;
            }
            catch { }


        }
    }

    private void dtAllResources()
    {
        string sqll = "";
        if (SelectedSubjectArea == "")
        {
            sqll = "select * from  Resources  order by ResourceName";
        }
        else
        {
           // sqll = "select * from  Resources";
            sqll = "select ID, ResourceName from  Resources  where [SubjectAreasTaxonomy] like '%," + SelectedSubjectArea + ",%'    order by ResourceName";
        }
       // Response.Write(sqll);
        resourcesDt = DataBase.dbDataTable(sqll, "Admin.DbConnection");
    }


 

    private string getAssignedLicensesCount(string resourceID)
    {
        return DataBase.returnOneValue("Select count(*) as count from PasswordAssignments where ResourceID = '" + resourceID + "'", "Admin.DbConnection");
    }



    public DataTable resourcesDt { get; set; }
    

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

    public string ListUsersWithAccess { get; set; }

   // public string SelectThisResource { get; set; }

    public string SelectThisResource
    {
        get
        {
            if (ViewState["SelectThisResource"] == null)
                return string.Empty;
            return ViewState["SelectThisResource"].ToString();

        }
        set
        {
            ViewState["SelectThisResource"] = value;
        }
    }

    public string SelectedSubjectArea
    {
        get
        {
            if (ViewState["SelectedSubjectArea"] == null)
                return string.Empty;
            return ViewState["SelectedSubjectArea"].ToString();

        }
        set
        {
            ViewState["SelectedSubjectArea"] = value;
        }
    }
    public string LabelTitle
    {
        get
        {
            if (ViewState["LabelTitle"] == null)
                return string.Empty;
            return ViewState["LabelTitle"].ToString();

        }
        set
        {
            ViewState["LabelTitle"] = value;
        }
    }


}