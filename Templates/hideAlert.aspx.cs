using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;

public partial class Templates_hideAlert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string sql = "INSERT INTO [dbo].[UserNotificationDismissal]( [UserPIN],[SiteWideNotificationLastDismissedDateTime], NewMessage) ";
        sql += " VALUES( '" + loginSSA.myPIN + "', GETDATE() , 'N')";
        DataBase.executeCommand(sql);
       // Response.Write(sql);
    }
}