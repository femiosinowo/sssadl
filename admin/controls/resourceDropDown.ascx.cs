using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;

public partial class admin_controls_resourceDropDown : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string getThisID = Request.QueryString["rid"].ToString();
            if (getThisID != "")
            {
                ResourceSummaryPanel.Visible = true;

                DataRow[] foundRows = resourcesDt.Select("ID='" + long.Parse(getThisID) + "' ");

                // Use the Select method to find all rows matching the filter.
                //  foundRows = resourcesDt.Select(filter);

                // Print column 0 of each returned row. 
                for (int i = 0; i < foundRows.Length; i++)
                {
                    resourceName = foundRows[i]["ResourceName"].ToString().Trim();
                    resourceDescription = foundRows[i]["Description"].ToString().Trim();
                    resourceAdminResourceURL = foundRows[i]["AdminResourceURL"].ToString().Trim();
                    resourceAdminUsername = foundRows[i]["AdminUsername"].ToString().Trim();
                    resourceAdminPassword = foundRows[i]["AdminPassword"].ToString().Trim();
                    resourceLimitedNumberPasswordsAvailable = foundRows[i]["LimitedNumberPasswordsAvailable"].ToString().Trim();
                    resourcePasswordsAvailable = foundRows[i]["PasswordsAvailable"].ToString().Trim();

                    if (resourceAdminResourceURL != "") AdminLoginPanel.Visible = true;

                }

                AssignedLicenses = getAssignedLicensesCount(getThisID);
                if (resourceLimitedNumberPasswordsAvailable == "Y")
                {
                    AvailableLicenses = (Convert.ToInt16(resourcePasswordsAvailable) - Convert.ToInt16(AssignedLicenses)).ToString();
                }
                else
                {
                    AvailableLicenses = "Unlimited";
                }
            }
        }
        catch { }
    }


 

    private string getAssignedLicensesCount(string resourceID)
    {
        return DataBase.returnOneValue("Select count(*) as count from PasswordAssignments where ResourceID = '" + resourceID + "'", "Admin.DbConnection");
    }



    public DataTable resourcesDt { get; set; }
    private void getResourceDetails(string getThisID)
    {

       
    }


    public string InternalNotesDB { get; set; }

    public string resourceName { get; set; }

    public string resourceDescription { get; set; }

    public string resourceAdminResourceURL { get; set; }

    public string resourceAdminUsername { get; set; }

    public string resourceAdminPassword { get; set; }

    public string resourcePasswordsAvailable { get; set; }

    public string resourceLimitedNumberPasswordsAvailable { get; set; }

    public string AssignedLicenses { get; set; }

    public string AvailableLicenses { get; set; }

    public string ListUsersWithAccess { get; set; }
}