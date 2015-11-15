using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;

public partial class Templates_resourceAjax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // string mytempPIN = "myPIN";
        try
        {
            string resourceId = Request.QueryString["rid"].ToString();
            string resourceTypeId = Request.QueryString["rstid"].ToString();
            string action = Request.QueryString["action"].ToString();
            string collection = Request.QueryString["collection"].ToString();



            if (action == "add")
            {
                string Title = Request.QueryString["Title"].ToString();
                string Url = Request.QueryString["Url"].ToString();
                string Date = Request.QueryString["Date"].ToString();
                string Author = Request.QueryString["Author"].ToString();

                sqlCount = "Select count(*) from MyResources where ( PINNumber = '" + loginSSA.myPIN + "' and FavoriteURL='" + resourceId + "' and ResourceContentType ='" + resourceTypeId + "' ) ";
                myCount = DataBase.returnOneValue(sqlCount);
                if (myCount == "0")
                {

                    string xmlData = string.Empty;
                    xmlData += "<root>";

                    xmlData += "<Title><![CDATA[";
                    xmlData += Title;
                    xmlData += "]]></Title>";

                    xmlData += "<Url><![CDATA[";
                    xmlData += Url;
                    xmlData += "]]></Url>";

                    xmlData += "<Author><![CDATA[";
                    xmlData += Author;
                    xmlData += "]]></Author>";


                    xmlData += "<Date><![CDATA[";
                    xmlData += Date;
                    xmlData += "]]></Date>";

                    xmlData += "</root>";

                    sql = "INSERT INTO [dbo].[MyResources]  ([PINNumber],[FavoriteURL],[DateAdded],[ResourceContentType],[Collection],[Data]) VALUES ('" + loginSSA.myPIN + "','" + resourceId + "',GETDATE() , '" + resourceTypeId + "' , '" + collection + "' , '" + xmlData + "')";
                }

                //sql = "INSERT INTO [dbo].[MyResources]  ([PINNumber],[FavoriteURL],[DateAdded],[ResourceContentType]) VALUES ('" + commonfunctions.myPIN + "','" + resourceId + "',GETDATE() , '" + resourceTypeId + "')";
            }
            else if (action == "delete")
            {
                sql = "Delete from MyResources where (PINNumber='" + loginSSA.myPIN + "' and FavoriteURL='" + resourceId + "' and ResourceContentType ='" + resourceTypeId + "' ) ";
                //Response.Write(sql);
            }

            DataBase.executeCommand(sql);
        }
        catch { }

        sqlCount = "Select count(*) from MyResources where PINNumber = '" + loginSSA.myPIN + "'";
        myResourcesCount = DataBase.returnOneValue(sqlCount);


    }



    public string myResourcesCount { get; set; }

    public string sql { get; set; }

    public string sqlCount { get; set; }

    public string myCount { get; set; }
}