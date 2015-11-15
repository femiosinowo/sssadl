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

        string sqll = "select ID, SubjectAreaName from  SubjectAreas order by SubjectAreaName";

        if (loadResFor == "TrainingRequest")
        {
            extraSQl = " and ShowInTrainingRequestForm='Y' ";
            sqll = "select ID, SubjectAreaName from  SubjectAreas where ShowInTrainingRequestForm='Y' order by SubjectAreaName ";
        }

      
        if (subjectID != "")
        {

            sqll = "select ID, SubjectAreaName from  SubjectAreas  where [SubjectAreasTaxonomy] like '%," + subjectID + ",%'  " + extraSQl + " and SubjectAreaDisplayStatus in ('Enabled' , 'Disabled') order by SubjectAreaName";
            
           
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
                string SubjectAreaID = dtr["ID"].ToString();
                string SubjectAreaName = dtr["SubjectAreaName"].ToString();
                SubjectAreaDD += "<option value='" + SubjectAreaID + "'>" + SubjectAreaName + "</option>";
            }

        }



    }


   
    public string sqlInsert { get; set; }
    public string selectedText = " selected='selected' ";
    public DataTable SubjectAreasDt { get; set; }
 
    public string SubjectAreaDD { get; set; }
}