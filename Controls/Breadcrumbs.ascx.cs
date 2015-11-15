using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Xml;
public partial class Controls_Breadcrumbs : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pagebreadCrumb2.DefaultContentID = long.Parse(contentID);
            pagebreadCrumb2.Fill();
            XmlNodeList xList = pagebreadCrumb2.XmlDoc.SelectNodes("/FolderBreadcrumb/entry");
            foreach (XmlElement xn in xList)
            {
                bCTitle = xn["title"].InnerXml;
                bCURL = commonfunctions.host + "/" + xn["url"].InnerXml;

                bCOutput += " <li><a href='" + bCURL + "'>" + bCTitle + "</a></li>";


            }
        }
        catch { }
    }


    public string bCTitle { get; set; }

    public string bCURL { get; set; }

    public string bCOutput { get; set; }
   // public string contentId { get; set; }

    public string contentID
    {
        get
        {
            if (ViewState["contentID"] == null)
                return string.Empty;
            return ViewState["contentID"].ToString();

        }
        set
        {
            ViewState["contentID"] = value;
        }
    }

}