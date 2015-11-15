using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Templates_Default6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        String command = @"C:\openSite.bat";

      //  ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + command);

      //  System.Diagnostics.Process.Start("cmd.exe", "/c " + command);
        System.Diagnostics.Process.Start(command);

    }
}