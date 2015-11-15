using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;

public partial class admin_users_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        requestorName = Request.QueryString["reqname"].ToString();
        resourceName = Request.QueryString["rsname"].ToString();
        reqID = Request.QueryString["reqid"].ToString();
        DataBase.executeCommand("Update AccessToResourceForm Set FormStatus ='Declined-Closed'   where ID='" + reqID + "'", "Admin.DbConnection");
        AuditLogs.log_Changes(reqID.Trim(), "AccessToResourceForm");
    }





    public string requestorName { get; set; }

    public string resourceName { get; set; }





    public string reqID { get; set; }
}