using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ektron.Cms.Content;
using System.Xml;
using Ektron.Cms.Framework.Content;
using Ektron.Cms;

public partial class Controls_RightSideColumn : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        
        try
        {
            contentId = long.Parse(Request.QueryString["id"]);
            getSideWidget(contentId);
            //getAssignedCollection();
        }
        catch
        {
            try
            {
                contentId = long.Parse(Request.QueryString["ekfrm"]);
                getSideWidget(contentId);
                //getAssignedCollection();
            }
            catch
            {

            }
        }
    }


    public void getSideWidget(long MaincontentID)
    {
        string resultView = string.Empty;
        XmlDocument contentXML = new XmlDocument();
        string mySidebar = string.Empty;

        ContentManager contentManager = new ContentManager();
        ContentData cData = new ContentData();
        Boolean returnMetadata = true;
        string result = string.Empty;

        cData = contentManager.GetItem(MaincontentID, returnMetadata);
        foreach (ContentMetaData cmd in cData.MetaData)
        {

            if (cmd.Name == "Side Widget Content")
            {
                mySidebar = cmd.Text;

            }
        }

        if (mySidebar == "")
        {
            getAssignedCollection();
        }
        else
        {

            string[] swIds = mySidebar.Split(new string[] { ";" }, StringSplitOptions.None);

            foreach (string swId in swIds)
            {
                sideWidgetContent += getSideWidgetDetails(long.Parse(swId));
            }
        }
        // resultView += getSideWidgetDetails(mySidebar);

        //  return resultView;

    }

    /// <summary>
    /// This method is used to dislay selected news items as metadata
    /// </summary>
    /// <param name="cData"></param>
    /// 
    public void getAssignedCollection(){

        //let check if it has its own sidewidget
       // sideWidget = cf.getSideWidget(contentId);
        string sideWidget = "";
      //  long contentId = long.Parse(ccontentID); // cData.Id;

         
        long sideNavBarMenuID = 0;
      // Response.Write(cfolderID.ToString());

        SqlDataReader rdr = null;

        string str = ConfigurationManager.ConnectionStrings["Ektron.DbConnection"].ConnectionString;

        List<string> allParentFolder = new List<string>();
        using (SqlConnection sqlConnection1 = new SqlConnection(str))
        {
            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.CommandText = "getParentFolder";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@folderID", cfolderID));
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        allParentFolder.Add(rdr["parent_folder_id"].ToString());
                    }
                }

                rdr.Close();


            }
        }

        //Collection check
        Dictionary<string, long> menuCol = new Dictionary<string, long>();
        foreach (var FolderId in allParentFolder)
        {
            menuCol = commonfunctions.getAssignedMenuNCollectionId(long.Parse(FolderId.ToString()));
            foreach (var f in menuCol)
            {
                if (f.Key == "CollectionID")
                {

                    if (f.Value > 0)
                    {
                        CollectionID = f.Value;

                    }
                }

            }


            if (CollectionID > 0)
            {
                break;

            }

        }



        ///lets continue the journey
        ///

        if (sideWidget == "" && CollectionID > 0)
        {

            var contentManager2 = new Ektron.Cms.Framework.Content.ContentManager();
            var criteria2 = new ContentCollectionCriteria();
            criteria2.AddFilter(CollectionID);
            criteria2.OrderByCollectionOrder = true;
            var contentList2 = contentManager2.GetList(criteria2);

            for (int jk = 0; jk < contentList2.Count; jk++)
            { 
                sideWidgetContent += getSideWidgetDetails(contentList2[jk].Id);
            }
             
        }
        else
        {
           // sideWidgetContent = sideWidget;
        }
         

    }

    private string getSideWidgetDetails(long sideWidgetID)
    {
        string returnedResults = "";
        try
        {

           
            XmlDocument contentXML = commonfunctions.getContentXML(sideWidgetID);

            string WidgetTitle = commonfunctions.getFieldValue(contentXML, "WidgetTitle");
            string WidgetContent = commonfunctions.getFieldValue(contentXML, "WidgetContent");

            string SideWidgetCollection = commonfunctions.getFieldValue(contentXML, "SideWidgetCollection");
            string display_count = commonfunctions.getFieldValue(contentXML, "display_count");

            returnedResults += "  <div class=\"pad-left\">";
            returnedResults += "  <h3>" + WidgetTitle + "</h3>";
            returnedResults += " <div class=\"divider\">";
            returnedResults += "  <p>" + WidgetContent + "</p>";
            returnedResults += " </div>";
            returnedResults += "</div>";

        }
        catch
        {
          //  return "";
        }

        return returnedResults;

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


    public long CollectionID { get; set; }

    public string ccontentID
    {
        get
        {
            if (ViewState["ccontentID"] == null)
                return string.Empty;
            return ViewState["ccontentID"].ToString();

        }
        set
        {
            ViewState["ccontentID"] = value;
        }
    }



    public string cfolderID
    {
        get
        {
            if (ViewState["cfolderID"] == null)
                return string.Empty;
            return ViewState["cfolderID"].ToString();

        }
        set
        {
            ViewState["cfolderID"] = value;
        }
    }


    public string sideWidgetContent { get; set; }

    public long contentId { get; set; }
}