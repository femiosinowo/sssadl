using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSADL.CMS;
using Ektron.Cms.Instrumentation;

public partial class MasterPages_OneColumn : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();
    }
}
