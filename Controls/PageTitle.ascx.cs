using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Collections;

public partial class Controls_PageTitle : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            string pgID = Request.QueryString["id"].ToString();
            
        }
        catch { }

        string collection = "0";
        try
        {
              collection = Request.QueryString["collection"].ToString();

        }
        catch { }
        //myFavCollection = commonfunctions.getAllmyFavorites();

        //if (myFavCollection.Contains(pageId))
        //{
        //    myFav = " style=\"display:none\" ";
        //}
        //else
        //{
        //   notFav  = " style=\"display:none\" ";
        //}

        myFavIcons = commonfunctions.getMyFavIcons(pageId, ResourceTypeId, pgTitle,   collection  );
       
    }



    
    public ArrayList myFavCollection = new ArrayList();

    public string pgTitle
    {
        get
        {
            if (ViewState["pgTitle"] == null)
                return string.Empty;
            return ViewState["pgTitle"].ToString();

        }
        set
        {
            ViewState["pgTitle"] = value;
        }
    }


    public string pageId
    {
        get
        {
            if (ViewState["pageId"] == null)
                return string.Empty;
            return ViewState["pageId"].ToString();

        }
        set
        {
            ViewState["pageId"] = value;
        }
    }

    public string ResourceTypeId
    {
        get
        {
            if (ViewState["ResourceTypeId"] == null)
                return string.Empty;
            return ViewState["ResourceTypeId"].ToString();

        }
        set
        {
            ViewState["ResourceTypeId"] = value;
        }
    }


    public string favDisplay { get; set; }

    public string myFav { get; set; }

    public string notFav { get; set; }

    public string myFavIcons { get; set; }
}