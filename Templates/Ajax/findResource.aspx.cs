using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms;
using SSADL.CMS;
using System.Data;
public partial class Templates_Ajax_clicktracks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string subjectID = Request.QueryString["subjectID"].ToString().Trim();
        // Response.Write(link);
        string loadResFor = Request.QueryString["loadResFor"].ToString().Trim();
        string extraSQl = string.Empty;

        string sqll = "select ID, ResourceName from  Resources order by ResourceName";

        if (loadResFor == "TrainingRequest")
        {
            extraSQl = " and ShowInTrainingRequestForm='Y' ";
            sqll = "select ID, ResourceName from  Resources where ShowInTrainingRequestForm='Y' order by ResourceName ";
        }

      
        if (subjectID != "")
        {

            sqll = "select ID, ResourceName from  Resources  where [SubjectAreasTaxonomy] like '%," + subjectID + ",%'  " + extraSQl + " and ResourceDisplayStatus in ('Enabled' , 'Disabled') order by ResourceName";
            
           
            // Use the Select method to find all rows matching the filter.

              someData.Visible = true;
        }
        else
        {

            noData.Visible = true;
        }



        DataTableReader dtr = DataBase.dbDataTable(sqll).CreateDataReader();
        if (dtr.HasRows)
        {
            while (dtr.Read())
            {
                string resourceID = dtr["ID"].ToString();
                string resourceName = dtr["ResourceName"].ToString();
                resourceDD += "<option value='" + resourceID + "'>" + resourceName + "</option>";
            }

        }



    }


   
    public string sqlInsert { get; set; }
    public string selectedText = " selected='selected' ";
    public DataTable resourcesDt { get; set; }
    public string SubJectAreaDD { get; set; }
    public string resourceDD { get; set; }
}