using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;

public partial class admin_reports_viewReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        reportID = Request.QueryString["reportid"].ToString();
        DataTableReader dtr = DataBase.dbDataTable("Select * from Report  where ID=" + reportID).CreateDataReader();
        if (dtr.HasRows)
        {
            dtr.Read();
          
            ReportType = dtr["ReportType"].ToString();
            ReportXMLData = dtr["ReportXMLData"].ToString();
            ReportSQLStatement = dtr["ReportSQLStatement"].ToString();
            Name = dtr["Name"].ToString();
            
        }
    }

    public string reportID { get; set; }

    public string ReportType { get; set; }

    public string ReportXMLData { get; set; }

    public string ReportSQLStatement { get; set; }

    public string Name { get; set; }
}