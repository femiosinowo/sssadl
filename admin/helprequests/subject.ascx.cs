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

        if (!IsPostBack)
        {

            string sqll = "select * from  ViewAllSubjectArea_SSADL";


            Response.Write(sqll);
            DataTable dt = DataBase.sortDataTable(DataBase.dbDataTable(sqll, "Ektron.Dbconnection"), "Name", "ASC");

 

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TaxID");
            dt2.Columns.Add("Name");

            dt2.Rows.Add("", " - Select One - ");

            DataTableReader dtR = dt.CreateDataReader();
            while (dtR.Read())
            {
                dt2.Rows.Add(dtR["TaxID"].ToString(), dtR["Name"].ToString());
            }

            subjectDD.DataSource = dt2; // DataBase.dbDataTable("Select * from HelpRequestsAssignees ");
            subjectDD.DataBind();


            try
            {
                subjectDD.Items.FindByValue(SelectThisSubject).Selected = true;
            }
            catch { }

        }
    }

 


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

   // public string SelectThisSubject { get; set; }

    public string SelectThisSubject
    {
        get
        {
            if (ViewState["SelectThisSubject"] == null)
                return string.Empty;
            return ViewState["SelectThisSubject"].ToString();

        }
        set
        {
            ViewState["SelectThisSubject"] = value;
        }
    }


    public string SQLFilter
    {
        get
        {
            if (ViewState["SQLFilter"] == null)
                return string.Empty;
            return ViewState["SQLFilter"].ToString();

        }
        set
        {
            ViewState["SQLFilter"] = value;
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