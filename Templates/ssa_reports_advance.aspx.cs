using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
public partial class Templates_Content : System.Web.UI.Page
{
    /// <summary>
    /// Page Init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                long contentId = EktUtility.ParseLong(Request.QueryString["id"]);
                //    this.GetContentData(contentId);
            }
        }




        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();

        ///https://server16760.contentdm.oclc.org/dmwebservices/index.php?q=dmGetCollectionList/param1/param2/xml
        ///http://CdmServer.com:port/dmwebservices/index.php?q=dmQuery/alias/searchstrings/fields/sortby/maxrecs/start/suppress/docptr/suggest/facets/showunpub/denormalizeFacets/format     
        //////https://server16760.contentdm.oclc.org/dmwebservices/index.php?q=dmQuery/all/dollars/title!subjec!creato!publis!date/title/250/1/0/0/0/0/0/0/xml 
        ///Tutorials from : http://www.contentdm.org/help6/custom/customize2h.asp
        ///


    }


}