using System;

using System.Web.UI;

using System.Data;

using System.Xml;

using System.Linq;

using System.Text;

using System.Collections.Generic;

using System.Text.RegularExpressions;




using Ektron.Cms;

using Ektron.Cms.Organization;

using Ektron.Cms.Instrumentation;

using SSADL.CMS;

using System.Net;

using System.Collections;

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

                long contentId = long.Parse(Request.QueryString["id"]);

                //    this.GetContentData(contentId);

            }

        }










        long mainNavId = 6; // ConfigHelper.GetValueLong("MainNavId");




        //TopMenu.DefaultMenuID = mainNavId;

        //TopMenu.Fill();

        //  Response.Write(mainNavId.ToString());

        navHTML = "<ul>";

        navHTML += generateMenu2(MenuHelper.getMenuXML(mainNavId, Page));

        navHTML += "</ul>";
       // aaa.Text = MenuHelper.getMenuXML(mainNavId, Page);


        //RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();

        //RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();

        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();

         uxPageTitle.pageId = mainContent.EkItem.Id.ToString();

         uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();

        uxPageTitle.ResourceTypeId = "2";



    }

    private string generateMenu2(string menuXML)
    {

        XmlDocument menuXMLDoc = new XmlDocument();

        menuXMLDoc.LoadXml(menuXML);

        string buildUL = "";

        int divcount = 0;

        XmlNodeList xnMenuData = menuXMLDoc.SelectNodes("/MenuDataResult/Item/Item/Menu/Item");




        foreach (XmlElement xnItem in xnMenuData)
        {
            
            string ItemType = xnItem["ItemType"].InnerText;            
            if (ItemType != "Submenu")
            {
                string ItemSelected = xnItem["ItemSelected"].InnerText;

                string ItemLink = xnItem["ItemLink"].InnerText;

                string ItemQuickLink = xnItem["ItemQuickLink"].InnerText;

                string ItemTarget = getTarget(xnItem["ItemTarget"].InnerText);

                string ItemTitle = xnItem["ItemTitle"].InnerText;

                string ItemId = xnItem["ItemId"].InnerText;

                buildUL += buildULS(ItemTarget, ItemLink, ItemTitle, ItemId);

                
            
            }




            if (ItemType == "Submenu")
            {

                XmlNodeList xnSubmenuItems = xnItem.SelectNodes("Menu");







                foreach (XmlElement xnSubItem in xnSubmenuItems)
                {




                    string Title = xnSubItem["Title"].InnerText;
                    string MenuId = xnSubItem["MenuId"].InnerText;
                    string Link = xnSubItem["Link"].InnerText;

                    string MenuSelected = xnSubItem["MenuSelected"].InnerText;

                    string ChildMenuSelected = xnSubItem["ChildMenuSelected"].InnerText;

                    //Response.Write(Link + " : >> " + Request.RawUrl);




                    XmlNodeList xnSubmenuItemsChildrent = xnSubItem.SelectNodes("Item");

                    double ccnt = xnSubmenuItemsChildrent.Count;

                    mainmenudropindicator = "";

                    if (ccnt > 0) //this main menu 
                    {

                        // mainmenudropindicator = "class='has-submenu'";

                        Link = "#";



                    }



 
                    buildUL += "<li " + mainmenudropindicator + "><a href='" + Link + "' title='" + Title + "' aria-label='" + Title + "' >" + Title + "</a><br/>";

                    if (MenuId == "40")
                    {
                        buildUL += getNews();
                    }


                    if (ccnt > 0) //begin innner dropdown here
                    {

                        buildUL += getInnerDropDown(xnSubmenuItemsChildrent);




                    }







                    buildUL += "</li>";

                    divcount++;

                }

            }




        }




        buildUL += "";

        return buildUL;

    }


    private string getNews()
    {
        string returnNews = "";
        string sql = "SELECT * FROM [ViewAlllNews_SSADL] order by Date DESC";
        DataTableReader reader = DataBase.dbDataTable(sql, "Ektron.DbConnection").CreateDataReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
               
                    XmlDocument XMLDoc = new XmlDocument();
                    XMLDoc.LoadXml(reader["content_html"].ToString());

                    string HeadLine = commonfunctions.getFieldValue(XMLDoc, "Headline", "/News");

                    long newsId = long.Parse(reader["content_id"].ToString());


                    string title = HeadLine;
                    string link = commonfunctions.getQuickLink(newsId);

                    returnNews += "<li><a href='" + link + "'>" + HeadLine + "</a></li>";
               

            }

            reader.Close();
        }
        return "<ul>" +returnNews + "</ul>"; ;
    }

    private string getInnerDropDown(XmlNodeList xnSubmenuItemsChildrent)
    {

        ArrayList AllLis = new ArrayList();

        string innerdd = "";

        // innerdd += "<div class='row-12'>";




        foreach (XmlElement xnSubItemChild in xnSubmenuItemsChildrent)
        {

            string ItemType = xnSubItemChild["ItemType"].InnerText;

            if (ItemType == "Submenu")
            {

                XmlNodeList xnSubmenuItems2 = xnSubItemChild.SelectNodes("Menu");

                foreach (XmlElement xnSubItemb in xnSubmenuItems2)
                {

                    string Title = xnSubItemb["Title"].InnerText;// +xnSubItemb["ID"].InnerText;




                    //   innerdd += " <div class='column-6'>";

                    innerdd += "  <a class='nav-header' href='#' tabindex='-1' title='" + Title + "' aria-label='" + Title + "'>" + Title + "</a>";




                    //getting other children here                 

                    XmlNodeList xnSubmenuItemsChildren2 = xnSubItemb.SelectNodes("Item");

                    innerdd += getInnerSubChildren(xnSubmenuItemsChildren2);

                    // innerdd += "</div>";




                }

            }




            if (ItemType != "Submenu")
            {




                //  string Title = xnSubItemChild["Title"].InnerText;




                //    innerdd += " <div class='row-12'>";

                //  innerdd += "<ul class='column-12'>";

                string ItemSelected = xnSubItemChild["ItemSelected"].InnerText;

                string ItemLink = xnSubItemChild["ItemLink"].InnerText;

                string ItemQuickLink = xnSubItemChild["ItemQuickLink"].InnerText;

                string ItemTarget = getTarget(xnSubItemChild["ItemTarget"].InnerText);

                string ItemTitle = xnSubItemChild["ItemTitle"].InnerText;

                string ItemId = xnSubItemChild["ItemId"].InnerText;




                AllLis.Add(buildULS(ItemTarget, ItemLink, ItemTitle, ItemId));




                //  innerdd += buildULS(ItemTarget, ItemLink, ItemTitle, ItemId);




                //     innerdd += "</ul>";

                //     innerdd += "</div>";







            }







        }

        if (AllLis.Count > 0)
        {

            // innerdd += " <div class='row-12'>";

            //innerdd += "<ul >";

            innerdd += formattedAllLis(AllLis);

            // innerdd += "</ul>";

            // innerdd += "</div>";

        }




        // innerdd += "</div>";

        //innerdd += "</div>";

        return innerdd;

    }

    private string getInnerSubChildren(XmlNodeList xnSubmenuItemsSubChildren)
    {

        string innerSubs = "";

        ArrayList AllLis = new ArrayList();

        double ccntSub = xnSubmenuItemsSubChildren.Count;

        string ItemType = "";

        foreach (XmlElement xnSubItemChild in xnSubmenuItemsSubChildren)
        {

            ItemType = xnSubItemChild["ItemType"].InnerText;

            if (ItemType != "Submenu")
            {

                string ItemSelected = xnSubItemChild["ItemSelected"].InnerText;

                string ItemLink = xnSubItemChild["ItemLink"].InnerText;

                string ItemQuickLink = xnSubItemChild["ItemQuickLink"].InnerText;

                string ItemTarget = getTarget(xnSubItemChild["ItemTarget"].InnerText);

                string ItemTitle = xnSubItemChild["ItemTitle"].InnerText;

                string ItemId = xnSubItemChild["ItemId"].InnerText;




                AllLis.Add(buildULS(ItemTarget, ItemLink, ItemTitle, ItemId));







            }

        }




        innerSubs += formattedAllLis(AllLis);




        return innerSubs;

    }




    private string formattedAllLis(ArrayList AllLis)
    {

        string innerSubs = "";

        double ccntSub = AllLis.Count;

        int iCont = 0;

        // double endUL_col1 = ccntSub;




        // double endUL_col1 = Math.Round(ccntSub / 2, 0, MidpointRounding.ToEven);

        // if (endUL_col1 < 5) endUL_col1 = 5;






        innerSubs += "<ul>";

        foreach (string li in AllLis)
        {

            innerSubs += li;

        }

        innerSubs += "</ul>";



        return innerSubs;

    }

    public string buildULS(string ItemTarget, string ItemLink, string ItemTitle, string ItemId)
    {

        string buildUL = "";

        buildUL = "<li><a title='" + ItemTitle + "'  aria-label='" + ItemTitle + "' target='" + ItemTarget + "' href='" + ItemLink + "' >" + ItemTitle + "</a></li>";

        return buildUL;

    }




    public string getTarget(string no)
    {

        if (no == "1") return "_blank";

        if (no == "2") return "_self";

        if (no == "3") return "_parent";

        if (no == "4") return "_top";

        if (no == "") return "_self";

        return "_self";

    }










    public string navFooterHTML { get; set; }




    public string navHTML { get; set; }













    public string menuID
    {

        get
        {

            if (ViewState["menuID"] == null)

                return string.Empty;

            return ViewState["menuID"].ToString();




        }

        set
        {

            ViewState["menuID"] = value;

        }

    }




    public string displaySubMenu
    {

        get
        {

            if (ViewState["displaySubMenu"] == null)

                return string.Empty;

            return ViewState["displaySubMenu"].ToString();




        }

        set
        {

            ViewState["displaySubMenu"] = value;

        }

    }




    public string navHTMLHeaderMenu { get; set; }













    public string mainmenudropindicator { get; set; }

}