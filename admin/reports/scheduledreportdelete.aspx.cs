using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using TaskScheduler;

public partial class admin_reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        reportID = Request.QueryString["deleteReportID"].ToString();
        try
        {
            deleteTask(reportID);
        }
        catch { }
        string deleteSQL = "Delete from ReportSchedule where ID=" + Request.QueryString["deleteReportID"].ToString();
        DataBase.executeCommand(deleteSQL);



    }



    private void deleteTask(string reportid)
    {


        DataTableReader dtrSch = DataBase.dbDataTable("Select * from ReportSchedule  where ID=" + reportid).CreateDataReader();
        if (dtrSch.HasRows)
        {
            dtrSch.Read();
            string ReportName = dtrSch["ReportName"].ToString().Trim();
            string ReportDBID = dtrSch["ReportID"].ToString().Trim();
            ITaskService taskService = new TaskSchedulerClass();
            taskService.Connect();
            ITaskFolder containingFolder = taskService.GetFolder(@"\SSADL");
            containingFolder.DeleteTask(ReportName, 0);

            string path = commonfunctions.BaseDirectory + "/batFiles/report_" + ReportDBID + ".bat";
            // Delete the file if it exists. 

            if (File.Exists(path))
            {
                File.Delete(path);
            }





        }


    }

    public string reportID { get; set; }
   
}