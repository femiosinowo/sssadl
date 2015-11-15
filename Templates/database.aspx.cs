using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Web;
public partial class Templates_Content : System.Web.UI.Page
{
    /// <summary>
    /// Page Init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                long contentId = long.Parse(Request.QueryString["id"]);
                //    this.GetContentData(contentId);
            }


        }


        //if ((Request.Form != null))
        //{

        //    foreach (var a in Request.Form)
        //    {
        //        Response.Write(a.ToString() + "<br/>");
        //    }

        //}

        if (IsPostBack)
        {
            //Response.Write(Request.Form["ctl00$ctl00$cphMainContent$cphSecondaryMainContent$databaseKeyword"].ToString());
            try
            {
                string searchTxt = Request.Form["ctl00$ctl00$cphMainContent$cphSecondaryMainContent$databaseKeyword"].ToString();
                if (searchTxt != "")
                {
                    sqlAddition = " and (ResourceName like '%" + searchTxt + "%'  or Description like '%" + searchTxt + "%' ) ";

                }
            }
            catch { }

        }


        uxPageTitle.pgTitle = mainContent.EkItem.Title.ToString();
        uxPageTitle.pageId = mainContent.EkItem.Id.ToString();
        uxBreadcrumb.contentID = mainContent.EkItem.Id.ToString();
        uxPageTitle.ResourceTypeId = "1";


     //   myFavCollection = commonfunctions.getAllmyFavorites();

        createDivs();


    }


    private void createDivs()
    {
        DataTable dtAlpha = new DataTable();
        dtAlpha.Columns.Add("alpha");
        dtAlpha.Columns.Add("alphaRange");
        dtAlpha.Columns.Add("alphaTitle");
        //Dictionary<string, string> alphaThings = new Dictionary<string, string>();
        //alphaThings.Add("a", "ab");
        //alphaThings.Add("b", "cd");
        //alphaThings.Add("c", "eh");
        //alphaThings.Add("d", "im");
        //alphaThings.Add("e", "no");
        //alphaThings.Add("f", "pr");
        //alphaThings.Add("g", "st");
        //alphaThings.Add("h", "uz");



        for (int i = 1; i < 26; i++)
        {
            string alp = Number2String(i);
            string alpR = AlphaRange(alp);
            string alpR2 = alpR.Replace("-", "").ToLower();

            string sqll = "select count(*) from  Resources where ResourceTypeTaxonomy = '124' and SUBSTRING(ResourceName, 1, 1) = '" + alp + "' and ResourceDisplayStatus='Enabled' " + sqlAddition;
            //    Response.Write(sqll + "<br/>");
            string count = DataBase.returnOneValue(sqll);
           
            if (long.Parse(count) > 0)
            {
               // Response.Write(alpR + "<br/>");
                dtAlpha.Rows.Add(alp, alpR2, alpR);
            }
        }

        DL_Databaselist.DataSource = dtAlpha;
        DL_Databaselist.DataBind();

    }


    protected void DL_Databaselist_ItemDatabound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem ditem = (ListViewDataItem)e.Item;
            //data reader
            Repeater rpt = (Repeater)ditem.FindControl("alphaRepeater");
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;


            rpt.DataSource = getAlphaResources(item["alpha"].ToString());
            rpt.DataBind();


        }
    }

    private DataTable getAlphaResources(string alphabet)
    {


        string sqll = "select * from  Resources where ResourceTypeTaxonomy = '124' and SUBSTRING(ResourceName, 1, 1) = '" + alphabet + "' and ResourceDisplayStatus='Enabled' " + sqlAddition;
          //Response.Write(sqll);
        return DataBase.sortDataTable(DataBase.dbDataTable(sqll), "ResourceName", "ASC");
    }

    protected void alphaRepeaterItem_ItemDatabound(Object Sender, RepeaterItemEventArgs e)
    {



        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RepeaterItem ditem = (RepeaterItem)e.Item;
            //data reader
            System.Data.DataRowView item = (System.Data.DataRowView)ditem.DataItem;

            HyperLink ResourceTitle = (HyperLink)ditem.FindControl("ResourceTitle");
            // Literal Description = (Literal)ditem.FindControl("Description");
            // Literal LoginInfomation = (Literal)ditem.FindControl("LoginInfomation");

            Literal notFav = (Literal)ditem.FindControl("notFav");
            Literal myFav = (Literal)ditem.FindControl("myFav");



            long rId = long.Parse(item["ID"].ToString().Trim());

            //bool amIfav = amIfavorite(rId.ToString());
            //if (amIfav)
            //{
            //    notFav.Text = " style=\"display:none\"";
            //}
            //else
            //{

            //    myFav.Text = " style=\"display:none\"";
            //}


           

            string Title = item["ResourceName"].ToString();
            // Description.Text = item["Description"].ToString();
            string ResourceURLlink = item["ResourceURLlink"].ToString();
            ResourceTitle.Text = Title;
            ResourceTitle.NavigateUrl = ResourceURLlink;

            ResourceTitle.CssClass = "outboundResource";
            ResourceTitle.Attributes.Add("resourceid", rId.ToString());

            if (item["ShowInNewWindow"].ToString() == "Y")
            {
                ResourceTitle.Target = "_blank";
            }

            myFav.Text = commonfunctions.getMyFavIcons(rId.ToString(), "1", Title);

            Literal bottomLinks = (Literal)ditem.FindControl("bottomLinks");
            bottomLinks.Text = commonfunctions.getResourceBottomLinks(item);


        }
    }
    public ArrayList myFavCollection = new ArrayList();
    private bool amIfavorite(string rId)
    {

        if (myFavCollection.Count > 0)
        {

            if (myFavCollection.Contains(rId))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
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
    private string AlphaRange(string value)
    {
        string returnValue = string.Empty;
        switch (value.ToString().ToLower())
        {
            case "a":
            case "b":
                returnValue = "A-B";
                break;
            case "c":
            case "d":
                returnValue = "C-D";
                break;

            case "e":
            case "f":
            case "g":
            case "h":
                returnValue = "E-H";
                break;

            case "i":
            case "j":
            case "k":
            case "l":
            case "m":
                returnValue = "I-M";
                break;

            case "n":
            case "o":
                returnValue = "N-O";
                break;

            case "p":
            case "q":
            case "r":
                returnValue = "P-R";
                break;

            case "s":
            case "t":
                returnValue = "S-T";
                break;

            case "u":
            case "v":
            case "w":
            case "y":
            case "z":
                returnValue = "U-Z";
                break;

        }
        return returnValue;

    }
    private String Number2String(int number, bool isCaps = true)
    {
        Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
        return c.ToString();
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
 


    }

    public string sqlAddition { get; set; }


}