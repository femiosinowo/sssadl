using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;

public partial class sitemaintenance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        checkSiteMaintenance();
          DataTableReader dtR = DataBase.dbDataTable("Select * from SiteMaintenance where ID='1'").CreateDataReader();

          while (dtR.Read())
          {
              ///Alert
              ///
           
                MessageToDisplay = dtR["MessageToDisplay"].ToString().Trim();
           
                MessageTitle = dtR["MessageTitle"].ToString().Trim();
          

 

          }
    }

    public void checkSiteMaintenance()
    {
        DataTableReader dtR_SiteMT = DataBase.dbDataTable("Select * from SiteMaintenance where ID='1'").CreateDataReader();
        while (dtR_SiteMT.Read())
        {
            string Enable = dtR_SiteMT["Enable"].ToString().Trim();
            //  Response.Write( convertTimeToDateTime(EndTime).ToLongDateString());
            if (Enable == "N")
            {
                Response.Redirect("/default.aspx");
            }
        }
    }

    public string MessageTitle { get; set; }

    public string MessageToDisplay { get; set; }
}