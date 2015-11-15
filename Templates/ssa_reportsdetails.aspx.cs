using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
public partial class Templates_Content : System.Web.UI.Page
{
    /// <summary>
    /// Page Init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        string ptr = "";
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                long contentId = long.Parse(Request.QueryString["id"]);
                //    this.GetContentData(contentId);
            }

          
            if (!string.IsNullOrEmpty(Request.QueryString["pointer"]))
            {
                getDetails(Request.QueryString["pointer"].ToString(), Request.QueryString["collection"].ToString());
                ptr = Request.QueryString["pointer"].ToString();
               // getImages(Request.QueryString["pointer"].ToString(), Request.QueryString["collection"].ToString());
            }
        }




        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = title; // mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = ptr; // mainContent.EkItem.Id.ToString();
        uxPageTitle.ResourceTypeId = "3";
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();


    }

    private void getDetails(string pointer, string collection)
    {

        string format = "xml"; //either "xml" or "json".
        string url = commonfunctions.contentDMServer + "dmwebservices/index.php?q=dmGetItemInfo" + collection + "/" + pointer + "/" + format;
         Response.Write(url);


        WebRequest request = WebRequest.Create(url);
        WebResponse response = request.GetResponse();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(response.GetResponseStream());


        XmlNodeList xmlDocuments = xmlDoc.SelectNodes("/xml");

        // Response.Write(xmlDocuments.Count);
        foreach (XmlNode node in xmlDocuments)
        {

            // string id = node["pointer"].InnerText;
            title = node["title"].InnerText;
            descri = node["descri"].InnerText;
            creato = node["creato"].InnerText;
            publis = node["publis"].InnerText;
            date = node["date"].InnerText;
            // Response.Write(title);



        }





    }

    private void getImages(string pointer, string collection)
    {


        DataTable dtResult = new DataTable();
        dtResult.Columns.Add("thumbnailsSrc");
        dtResult.Columns.Add("ImageSrc");




        string format = "xml"; //either "xml" or "json".
        string url = commonfunctions.contentDMServer + "dmwebservices/index.php?q=dmGetCompoundObjectInfo" + collection + "/" + pointer + "/" + format;


        WebRequest request = WebRequest.Create(url);
        WebResponse response = request.GetResponse();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(response.GetResponseStream());


        XmlNodeList xmlDocuments = xmlDoc.SelectNodes("/cpd/page");

        foreach (XmlNode node in xmlDocuments)
        {
            string pagetitle = node["pagetitle"].InnerText;
            string pagefile = node["pagefile"].InnerText;
            string pageptr = node["pageptr"].InnerText;
            string width = "4413";
            string height = "3277";
            // string imgsrc = commonfunctions.contentDMServerUtil + "utils/ajaxhelper/?CISOROOT=" + collection.Replace("/", "") + "&CISOPTR=" + pageptr + "&action=2&DMSCALE=100&DMWIDTH=" + width + "&DMHEIGHT=" + height + "";

            string thumnailSrc = commonfunctions.contentDMServerUtil + "/utils/getthumbnail/collection" + collection + "/id/" + pageptr;
            string imgSrc = commonfunctions.contentDMServerUtil + "/utils/ajaxhelper/?CISOROOT=" + collection.Replace("/", "") + "&CISOPTR=" + pageptr + "&action=2&DMSCALE=100&DMWIDTH=" + width + "&DMHEIGHT=" + height;
            //  Response.Write("<img src='" + imgsrc + "' />");

            dtResult.Rows.Add(thumnailSrc, imgSrc);



        }
        imagesLV.DataSource = dtResult;
        imagesLV.DataBind();
        // <img id="selectedThumb-889" class="co-selected-thumb" src="/ui/cdm/default/collection/default/images/cdm_overlay.png" 
        //data-original="/utils/getthumbnail/collection/p16760coll2/id/889" alt="318930013061Z_Page_004" exifid="-914799834"
        //oldsrc="http://cdm16760.contentdm.oclc.org/ui/cdm/default/collection/default/images/cdm_overlay.png" style="width: 83px; height: 110px; background: 
        // url(http://cdm16760.contentdm.oclc.org/utils/getthumbnail/collection/p16760coll2/id/889) ;">
        //http://cdm16760.contentdm.oclc.org/utils/ajaxhelper/?CISOROOT=p16760coll2&CISOPTR=887&action=2&DMSCALE=100&DMWIDTH=4413&DMHEIGHT=3277


    }

    public string title { get; set; }

    public string descri { get; set; }

    public string creato { get; set; }

    public string publis { get; set; }

    public string date { get; set; }
}