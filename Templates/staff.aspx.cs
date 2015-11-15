using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using Ektron.Cms.Content;
using System.Collections.Generic;

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




        RightSideContent.ccontentID = mainContent.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainContent.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        uxPageTitle.ResourceTypeId = "2";

      //  List<string> listSideWidgetIDs = new List<string>();
        var contentManager2 = new Ektron.Cms.Framework.Content.ContentManager();
        var criteria2 = new ContentCollectionCriteria();
        criteria2.AddFilter(13);
        criteria2.OrderByCollectionOrder = true;
        var staffList = contentManager2.GetList(criteria2);
        
        for (int jk = 0; jk < staffList.Count; jk++)
        {
             // Response.Write(staffList[jk].Id.ToString());
          //  listSideWidgetIDs.Add(contentList2[jk].Id.ToString());

             ouptput+= getStaffList(staffList[jk].Id);
        }


        //string[] terms = listSideWidgetIDs.ToArray();


 
        
    }

    public string getStaffList(long staffId)
    {


        XmlDocument xmlStaffList = commonfunctions.getContentXML(staffId);
        string Name = commonfunctions.getFieldValue(xmlStaffList, "Name", "/Staff");
        string Title = commonfunctions.getFieldValue(xmlStaffList, "Title", "/Staff");
        string Role = commonfunctions.getFieldValue(xmlStaffList, "Role", "/Staff");
        string Location = commonfunctions.getFieldValue(xmlStaffList, "Location", "/Staff");

        string StaffPicture = commonfunctions.getFieldAttributeValue(xmlStaffList, "Location", "img", "src", "/Staff");
        string StaffPictureAlt = commonfunctions.getFieldAttributeValue(xmlStaffList, "Location", "img", "alt", "/Staff");

        string Active = commonfunctions.getFieldValue(xmlStaffList, "Active", "/Staff");

        if (Location != "")
        {
          Location=  commonfunctions.GetTaxonomyNameFromID(long.Parse(Location));
        }

        if (Role != "" && Title !="") Role = ", " + Role;

        string oouptup = "";

        if (Active == "true")
        {
            oouptup += "<tr>";
            oouptup += "<th headers=\"header1\" scope=\"row\"><img src=\"framework/images/staff_placeholder_photo.jpg\" alt=\"Staff Placeholder Photo\"></th>";
            oouptup += "<td headers=\"header2\">" + Name + "</td>";
            oouptup += "<td headers=\"header3\">" + Title + Role + "</td>";
            oouptup += "<td headers=\"header4\">" + Location + "</td>";

            oouptup += "</tr>";
        }
        return oouptup;
    }


 





    /// <summary>
    /// This method is used to get content data
    /// </summary>
    /// <param name="contentId"></param>
    //private void GetContentData(long contentId)
    //{
    //    if (contentId > 0)
    //    {
    //        var cData = ContentHelper.GetContentById(contentId);
    //        if (cData != null && cData.Id > 0)
    //        {
    //            //ltrFAQs.Text = cData.Html;
    //        }
    //    }
    //}


    public string ouptput { get; set; }
}