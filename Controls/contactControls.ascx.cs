using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;

public partial class Controls_contactControls : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //formID = long.Parse(Request.QueryString["ekfrm"].ToString());
            formID =  Request.QueryString["formID"].ToString();
        }
        catch
        {
          //  formID = "113";
        }

        populateSubjectAreas();
        // populateResources();

        dtAllResources();

        switch (long.Parse(formID))
        {
            case 111: //TrainingRequest
                TrainingRequest.Visible = true;
                ResearchAssistancePanel.Visible = true;
                IntroPanel.Visible = false;
                populateResources("ShowInTrainingRequestForm='Y'");
                break;

            case 112:
                ReportingAProblem.Visible = true;
                populateResources();
                break;

            case 108:
                PasswordAssistancePanel.Visible = true;
                populateResources("AccessTypeTaxonomy='122'");
               // populateResources("PasswordRequestsRestrictedToManagers='Y'");
                break;

            case 105:
                ResearchAssistancePanel.Visible = true;
                populateResources();
                break;

            case 106:
                RequestAccessPanel.Visible = true;
               // populateResources("PasswordRequestsRestrictedToManagers='Y' and ResourceTypeTaxonomy = '122' ");
                populateResources("ShowInDatabases='Y' and AccessTypeTaxonomy = '122' ");
                break;

        }


    }


    private void populateSubjectAreas()
    {
        SubJectAreaDD = "";
        string sqll = "select * from  ViewAllSubjectArea_SSADL";
        //Response.Write(sqll);
        DataTable dt = DataBase.sortDataTable(DataBase.dbDataTable(sqll, "Ektron.Dbconnection"), "Name", "ASC");

        DataTableReader rs = dt.CreateDataReader();
        if (rs.HasRows)
        {

            while (rs.Read())
            {
                SubJectAreaDD += "<option value='" + rs["TaxID"] + "'>" + rs["Name"] + "</option>";

            }
        }
    }
 

    private void populateResources(string filter = "")
    {
        resourceDD = "";
        string rId = "";
        try
        {
            rId = Request.QueryString["rid"].ToString();

        }
        catch { }

        //  string expression;
        //  expression = "ShowInTrainingRequestForm='Y'";
        DataRow[] foundRows;

        // Use the Select method to find all rows matching the filter.
        foundRows = resourcesDt.Select(filter);

        // Print column 0 of each returned row. 
        for (int i = 0; i < foundRows.Length; i++)
        {
            string resourceID = foundRows[i]["ID"].ToString();
            string resourceName = foundRows[i]["ResourceName"].ToString();
            if (rId == resourceID)
            {
                selectedText = "";
                resourceDD += "<option  selected='selected' value='" + resourceID + "'>" + resourceName + "</option>";
            }
            else
            {
                resourceDD += "<option value='" + resourceID + "'>" + resourceName + "</option>";
            }
        }


    }


    public string formID
    {
        get
        {
            if (ViewState["formID"] == null)
                return string.Empty;
            return ViewState["formID"].ToString();

        }
        set
        {
            ViewState["formID"] = value;
        }
    }



    private void dtAllResources()
    {
        string sqll = "select * from  Resources ";
        resourcesDt = DataBase.sortDataTable(DataBase.dbDataTable(sqll), "ResourceName", "ASC");
    }
    public string selectedText = " selected='selected' ";
    public DataTable resourcesDt { get; set; }
    public string SubJectAreaDD { get; set; }
    public string resourceDD { get; set; }

    //public long formID { get; set; }
}