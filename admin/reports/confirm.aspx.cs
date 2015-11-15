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
using TaskScheduler;
using System.IO;
using System.Text;

public partial class admin_reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SchedCreatePanel.Visible = true;
        UpdSaveMessage = "Report Schedule Has Been Updated";

    }






    public string UpdSaveMessage { get; set; }
}