using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;

public partial class admin_reports_resources_ajax_getDD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"].ToString();
        if (Request.QueryString["selectThis"] != null)
        {
            selectThis = Request.QueryString["selectThis"].ToString();
        }

        DataTableReader dtr;
        string Selected = string.Empty;
        string defaultResource = string.Empty;
        switch (type)
        {

            case "ResourcesbyProcurementMethod":

                PanelProcurementMethod.Visible = true;
                //ProcurementMethod.DataSource = AdminFunc.ProcumentMethodDataTable;
                //ProcurementMethod.DataBind();
                optionProcurementMethod = "";

                dtr = AdminFunc.ProcumentMethodDataTable.CreateDataReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        if (selectThis == "" && dtr["TaxName"].ToString() == "- Select One -") Selected = "Selected";
                        if (selectThis == dtr["TaxID"].ToString().Trim()) Selected = "Selected";
                        optionProcurementMethod += "<option " + Selected + " value='" + dtr["TaxID"].ToString().Trim() + "'>" + dtr["TaxName"].ToString().Trim() + "</option>";
                        Selected = "";
                    }

                    dtr.Dispose();
                }

                break;
            case "ResourcesbyType":

                //ResourceTypeTaxonomy.DataSource = AdminFunc.ResourceTypeTaxonomyDataTable;
                //ResourceTypeTaxonomy.DataBind();
                optionResourceTypeTaxonomy = "";
                PanelResourceType.Visible = true;

                dtr = AdminFunc.ResourceTypeTaxonomyDataTable.CreateDataReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        if (selectThis == "" && dtr["TaxName"].ToString() == "- Select One -") Selected = "Selected";
                        if (selectThis == dtr["TaxID"].ToString().Trim()) Selected = "Selected";
                        optionResourceTypeTaxonomy += "<option " + Selected + " value='" + dtr["TaxID"].ToString().Trim() + "'>" + dtr["TaxName"].ToString().Trim() + "</option>";
                        Selected = "";
                    }

                    dtr.Dispose();
                }

                break;
            case "Resources":

                PanelResourceType.Visible = false;
                PanelProcurementMethod.Visible = false;
                break;
            //case "ProcurementMethod":
            //   // url = "/admin/reports/resources/ajax/getDD.aspx?type=ProcurementMethod";


            //    break;

        }
    }




    public string optionProcurementMethod { get; set; }

    public string optionResourceTypeTaxonomy { get; set; }

    public string selectThis { get; set; }
}