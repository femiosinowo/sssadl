using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;
using System.Net;
using System.Data.SqlClient;

public partial class admin_users_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        Requestor = Request.QueryString["Requestor"];
        RequestName = Request.QueryString["reqname"];
        string panel = Request.QueryString["panel"];
        string type = Request.QueryString["type"];
        switch (type)
        {
            case "open":
                successpanel.Visible = true;
                whatInfo = "Opened";
                break;

            case "close":
                closepanel.Visible = true;
                whatInfo = "Closed";
                break;


        }

       
    }



    public string whatInfo { get; set; }
    public string Requestor { get; set; }

    public string RequestName { get; set; }
}