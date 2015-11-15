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
        string whatType = Request.QueryString["type"].ToString();
        listOptions = string.Empty;


        sql = "Select * from Resources  order by ResourceName";

        switch (whatType)
        {

            case "ResourcesbyType":
                string typeid = Request.QueryString["typeid"].ToString();
                //ListResourcesDD.DataSource = DataBase.dbDataTable("Select * from Resources where ResourceTypeTaxonomy ='" + typeid + "' order by ResourceName ");
                //ListResourcesDD.DataBind();
                sql = "Select * from Resources where ResourceTypeTaxonomy ='" + typeid + "' order by ResourceName ";
               // Response.Write(sql);
                break;
            case "ResourcesbyProcurementMethod":

                string pmid = Request.QueryString["pmid"].ToString();
                sql = "select a.[ResourceID] as ID , b.ResourceName ,a.[FiscalYear] from [dbo].[ResourcesContract] a left join [Resources] b on a.resourceid=b.id ";
                sql += " where ProcurementMethod = '" + pmid + "' order by  b.ResourceName ";
                //ListResourcesDD.DataSource = DataBase.RemoveDuplicateRows(DataBase.dbDataTable(sql), "ID");
                //ListResourcesDD.DataBind();
                 
                break;


            case "showall":
                sql = "Select * from Resources  order by ResourceName";
                //ListResourcesDD.DataSource = DataBase.dbDataTable("Select * from Resources  order by ResourceName ");
                //ListResourcesDD.DataBind();
                break;

            case "AssoicatedNetwork":
                 string value = Request.QueryString["value"].ToString();
                 sql = "Select * from Resources where AssociatedNetwork= '" + value + "'  order by ResourceName ";
                //ListResourcesDD.DataSource = DataBase.dbDataTable("Select * from Resources  order by ResourceName ");
                //ListResourcesDD.DataBind();
                break;

        }
        string Selected = "";

        string[] getAllSelected = { };

        if (Request.QueryString["rid"] != null)
        {
            defaultResource = Request.QueryString["rid"].ToString();
            getAllSelected = defaultResource.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        }
        //Response.Write(sql);
        DataTableReader dtr = DataBase.RemoveDuplicateRows(DataBase.dbDataTable(sql), "ID").CreateDataReader();
        if (dtr.HasRows)
        {
            while (dtr.Read())
            {
                if (getAllSelected.Contains(dtr["ID"].ToString().Trim())) Selected = "Selected";
                listOptions += "<option " + Selected + " value='" + dtr["ID"].ToString().Trim() + "'>" + dtr["ResourceName"].ToString().Trim() + "</option>";
                Selected = "";
            }

            dtr.Dispose();
        }

    }




    public string listOptions { get; set; }

    public string sql { get; set; }

    public string defaultResource { get; set; }
}