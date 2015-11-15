using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
public partial class Controls_conctactOnBehalfOfForm : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        myLastName = loginSSA.myLastName;
        myTitle = loginSSA.myTitle;
        myComponent = loginSSA.myComponent;
        myPhone = loginSSA.myPhone;
        myPin = loginSSA.myPIN;
        myFirstName = loginSSA.myFirstName;
        myEmail = loginSSA.myEmail;
        myOffice = loginSSA.myOffice;
        myServer = loginSSA.myServer;
        myUserDomain = loginSSA.myUserDomain;

    }

    public string myLastName { get; set; }
    public string myTitle { get; set; }
    public string myComponent { get; set; }
    public string myOffice { get; set; }
    public string myPhone { get; set; }
    public string myPin { get; set; }
    public string myFirstName { get; set; }
    public string myEmail { get; set; }
    
    public string myServer { get; set; }
    public string myUserDomain { get; set; }
}