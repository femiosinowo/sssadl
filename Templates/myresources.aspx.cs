using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using Ektron.Cms.Framework;
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
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                contentId = long.Parse(Request.QueryString["id"]);

            }

        }




        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();
        uxPageTitle.ResourceTypeId = "2";


       // allmyfavorites = commonfunctions.AllmyFavorites(); //AllmyFavoritesDT
        allFavorites();
        //    Response.Write(allmyfavorites.Count);
    }
    public ArrayList allmyfavIDs = new ArrayList();
    public void allFavorites()
    {
        DataTable dtresults = new DataTable();

        dtresults.Columns.Add("ID");
        dtresults.Columns.Add("ResourceName");
        dtresults.Columns.Add("Description");
        dtresults.Columns.Add("QuickLink");
        dtresults.Columns.Add("ShowInNewWindow");
        dtresults.Columns.Add("ShowLogin");
        dtresults.Columns.Add("SharedPassword");
        dtresults.Columns.Add("SharedUsername");
        dtresults.Columns.Add("contentType");
        dtresults.Columns.Add("collection");
        commonfunctions cf = new commonfunctions();

        DataTableReader dtr = commonfunctions.AllMyFavsDTP().CreateDataReader();
        while (dtr.Read())
        {

            string ResourceContentType = dtr["ResourceContentType"].ToString().Trim();
            string FavoriteURL = dtr["FavoriteURL"].ToString().Trim();
            string collection = dtr["collection"].ToString().Trim();
            string Data = dtr["Data"].ToString().Trim();
            switch (ResourceContentType)
            {
                case "1": //this is a resource
                    dtresults.Rows.Add(getResourceitem(FavoriteURL, ResourceContentType));
                    break;

                case "2": //this is a ektron content
                    dtresults.Rows.Add(getEktronContenItem(FavoriteURL, ResourceContentType));
                    //  Response.Write(resourceID + "<br/>");
                    break;

                case "3": //SSA Report - ContentDM
                    dtresults.Rows.Add(getContentDMItem(FavoriteURL, ResourceContentType, collection));

                    break;

                case "4": //Summon Search
                    dtresults.Rows.Add(getSummonItem(FavoriteURL, ResourceContentType, Data));
                    break;
                 case "6": //Tools For
                     dtresults.Rows.Add(getToolsForContent(FavoriteURL, ResourceContentType));

                    break;
            }

        }
       // Response.Write(dtresults.Rows.Count);

        DL_Resourceslist.DataSource = commonfunctions.sortDataTable(dtresults, "ResourceName", "ASC");
        DL_Resourceslist.DataBind();

    }

    private object[] getSummonItem(string FavoriteURL, string ResourceContentType, string Data)
    {
        object[] array = new object[10];
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(Data);
     //   Response.Write(xmlDoc.InnerXml);
        string Title = commonfunctions.getFieldValue(xmlDoc, "Title");
        string Url = commonfunctions.getFieldValue(xmlDoc, "Url");
        string Author = commonfunctions.getFieldValue(xmlDoc, "Author");
        string Date = commonfunctions.getFieldValue(xmlDoc, "Date");
      
        if (Author != "") Author = "By " + Author;
        if (Date != "") Title = Title + " (" + Date + ")";
      //  Response.Write(Title);
        array[0] = FavoriteURL;
        array[1] =  Title;
        array[2] = Author;
        array[3] = Url;
        array[4] = "Y";
        array[5] = ""; // rdr["ShowLogin"].ToString();
        array[6] = ""; //rdr["SharedUsername"].ToString();
        array[7] = ""; // rdr["SharedPassword"].ToString();
        array[8] = ResourceContentType;
        array[9] = "0"; // "/" + collection;

    

        return array;
    }

    private object[] getContentDMItem(string pointer, string resourceType, string collection)
    {
        object[] array = new object[10];


        string format = "xml"; //either "xml" or "json".
        string url = commonfunctions.contentDMServer + "dmwebservices/index.php?q=dmGetItemInfo" + collection + "/" + pointer + "/" + format;
        //  Response.Write(url);

     
        WebRequest request = WebRequest.Create(url);
        
        if (commonfunctions.Environment == "PROD" || commonfunctions.Environment == "DEV")
        {
            WebProxy wp = new WebProxy("http://access.lb.ssa.gov:80/", true);
            request.Proxy = wp;
        }
        WebResponse response = request.GetResponse();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(response.GetResponseStream());


        XmlNodeList xmlDocuments = xmlDoc.SelectNodes("/xml");

        // Response.Write(xmlDocuments.Count);
        foreach (XmlNode node in xmlDocuments)
        {

            // string id = node["pointer"].InnerText;
            //title = node["title"].InnerText;
            //descri = node["descri"].InnerText;
            //creato = node["creato"].InnerText;
            //publis = node["publis"].InnerText;
            //date = node["date"].InnerText;
            // Response.Write(title);

            array[0] = pointer;
            array[1] = node["title"].InnerText;
            array[2] = node["descri"].InnerText;
            array[3] = "https://cdm16760.contentdm.oclc.org/cdm/compoundobject/collection" + collection + "/id/" + pointer; // "/templates/ssa_reportsdetails.aspx?collection=" + collection + "&pointer=" + pointer;
            array[4] = "Y";
            array[5] = ""; // rdr["ShowLogin"].ToString();
            array[6] = ""; //rdr["SharedUsername"].ToString();
            array[7] = ""; // rdr["SharedPassword"].ToString();
            array[8] = resourceType;
            array[9] = "/" + collection;


        }


 



        return array;
    }


    private object[] getToolsForContent(string p, string resourceType)
    {
        object[] array = new object[10];

        XmlDocument xmlDoc = commonfunctions.getContentXML(long.Parse(p));

        string Title = commonfunctions.getFieldValue(xmlDoc, "Name", "/Tools"); // item["toolName"].ToString();
      string  Description = commonfunctions.getFieldValue(xmlDoc, "Description", "/Tools");

        string toolUrl = commonfunctions.getFieldAttributeValue(xmlDoc, "url", "a", "href", "/Tools");
        string toolUrlTarget = commonfunctions.getFieldAttributeValue(xmlDoc, "url", "a", "target", "/Tools");

       

        array[0] = p;
        array[1] = Title;
        array[2] = Description;
        array[3] = toolUrl;
        array[4] = "Y";
        array[5] = "N";
        array[6] = "";
        array[7] = "";
        array[8] = resourceType;
        array[9] = "0";


        return array;
    }


    private object[] getEktronContenItem(string p, string resourceType)
    {
        object[] array = new object[10];

       

        Ektron.Cms.Framework.Core.Content.Content contentManager1 = new Ektron.Cms.Framework.Core.Content.Content(ApiAccessMode.LoggedInUser);
        ContentData contentData = contentManager1.GetItem(long.Parse(p));

       

        array[0] = contentData.Id;
        array[1] = contentData.Title;
        array[2] = ""; // contentData.Teaser;
        array[3] = contentData.Quicklink;
        array[4] = "N";
        array[5] = "N";
        array[6] = "";
        array[7] = "";
        array[8] = resourceType;
        array[9] = "0";


        return array;
    }

    private object[] getResourceitem(string resourceID, string resourceType)
    {
        object[] array = new object[10];
        string sql = "Select * from Resources where ID='" + resourceID + "' ";
        DataTableReader rdr = DataBase.dbDataTable(sql).CreateDataReader();
        if (rdr.HasRows)
        {
            while (rdr.Read())
            {
                array[0] = rdr["ID"].ToString();
                array[1] = rdr["ResourceName"].ToString();
                array[2] = rdr["Description"].ToString();
                array[3] = rdr["ResourceURLlink"].ToString();
                array[4] = "Y"; // rdr["ShowInNewWindow"].ToString();
                array[5] = rdr["ShowLogin"].ToString();
                array[6] = rdr["SharedUsername"].ToString();
                array[7] = rdr["SharedPassword"].ToString();
                array[8] = resourceType;
                array[9] = "0";
            }
        }



        return array;
    }

 

    protected void DL_Resourceslist_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;


            HyperLink ResourceTitle = (HyperLink)ditem.FindControl("ResourceTitle");
            Literal Description = (Literal)ditem.FindControl("Description");
            Literal LoginInfomation = (Literal)ditem.FindControl("LoginInfomation");
            Literal myfavicons = (Literal)ditem.FindControl("myfavicons");

            //Literal notFav = (Literal)ditem.FindControl("notFav");
            //Literal myFav = (Literal)ditem.FindControl("myFav");

       //     try
     //       {
                string rId =item["ID"].ToString().Trim();

                string contentType = item["contentType"].ToString().Trim();
                string collection = item["collection"].ToString().Trim();
                //bool amIfav = amIfavorite(rId.ToString());
                //if (amIfav)
                //{
                //    notFav.Text = " style=\"display:none\"";
                //}
                //else
                //{

                //    myFav.Text = " style=\"display:none\"";
                //}



                string ResourceName = item["ResourceName"].ToString();

                myfavicons.Text = commonfunctions.getMyFavIcons(rId.ToString(), contentType, ResourceName, collection);


                Description.Text = item["Description"].ToString();
                string QuickLink = item["QuickLink"].ToString();
                ResourceTitle.Text = ResourceName;
                ResourceTitle.NavigateUrl = QuickLink;
                if (item["ShowInNewWindow"].ToString() == "Y")
                {
                    ResourceTitle.Target = "_blank";
                }

                //login sharing info 
                Panel sharedPasswordPanel = (Panel)ditem.FindControl("sharedPasswordPanel");
                string ShowLogin = item["ShowLogin"].ToString();

                if (deterMineShowLogin(ShowLogin.Trim()))
                {
                    sharedPasswordPanel.Visible = true;
                }
            //}
          //  catch { }

        }
    }

 

    private bool deterMineShowLogin(string ShowLogin)
    {
        //check for DDS employees - if you are DDS and also OnlyDDSEmployees is chosen - then only show resource password to DDS Employees
        if (ShowLogin == "All")
        {
            return false;
        }
        else if (ShowLogin == "OnlyDDSEmployees")
        {
            return true;
        }
        return false;

    }


    protected void Datapager_prender(object sender, EventArgs e)
    {


        // Response.Write(sql);






        // Response.Write(sql);
        //DL_Resourceslist.DataSource = commonfunctions.sortDataTable(DataBase.dbDataTable(sql), "ResourceName", "ASC");
        //DL_Resourceslist.DataBind();
        //base.OnPreRender(e);

    }





    public string SATitle { get; set; }

    public string SADescription { get; set; }

    public long contentId { get; set; }

    public string SubjectAreaTaxID { get; set; }

    protected void BtnShowSelectedResourceType_Click(object sender, EventArgs e)
    {


    }

    public string sql { get; set; }
 
}