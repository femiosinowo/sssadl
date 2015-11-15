using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSADL.CMS;

public partial class admin_users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            WhereFrom.Value = Request.UrlReferrer.ToString();
            DataTableReader dtR = DataBase.dbDataTable("Select * from SystemSettings").CreateDataReader();

            while (dtR.Read())
            {
                ///Alert
                ///
                string value = dtR["SettingValue"].ToString().Trim();
                string key = dtR["SettingName"].ToString().Trim();
                switch (key)
                {
                    case "AlertTitle":
                        AlertTitle.Text = value;
                        break;
                    case "AlertDescription":
                        AlertDescription.Text = value;
                        break;

                    case "AlertStartOn":
                        AlertStartOn.Text = value;
                        break;
                    case "AlertEndOn":
                        AlertEndOn.Text = value;
                        break;

                    case "AlertActive":
                        AlertActive.Text = value;
                        break;




                }

            }
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string updatesql = "";
        updatesql += "Update SystemSettings SET SettingValue = @AlertTitle where SettingName = 'AlertTitle' ;";
        updatesql += "Update SystemSettings SET SettingValue = @AlertDescription where SettingName = 'AlertDescription' ;";
        updatesql += "Update SystemSettings SET SettingValue = @AlertStartOn where SettingName = 'AlertStartOn' ;";
        updatesql += "Update SystemSettings SET SettingValue = @AlertEndOn where SettingName = 'AlertEndOn' ;";
        updatesql += "Update SystemSettings SET SettingValue = @AlertActive where SettingName = 'AlertActive'; ";
        updatesql += " Update UserNotificationDismissal Set NewMessage='Y'; ";





        //updatesql += "Update SystemSettings SET SettingValue = '" + replaceHypen(OutageTitle.Text) + "' where SettingName = 'OutageTitle' ;";
        //updatesql += "Update SystemSettings SET SettingValue = '" + replaceHypen(OutageDescription.Text) + "' where SettingName = 'OutageDescription' ;";
        //updatesql += "Update SystemSettings SET SettingValue = '" + replaceHypen(OutageActive.Text) + "' where SettingName = 'OutageActive' ;";


        //Response.Write(updatesql);
        //  DataBase.executeCommand(updatesql);


        SqlCommand cmd;
        cmd = new SqlCommand(updatesql);
        cmd.Parameters.AddWithValue("@AlertDescription", AlertDescription.Text);
        cmd.Parameters.AddWithValue("@AlertTitle", AlertTitle.Text);
        cmd.Parameters.AddWithValue("@AlertStartOn", AlertStartOn.Text);
        cmd.Parameters.AddWithValue("@AlertEndOn", AlertEndOn.Text);
        cmd.Parameters.AddWithValue("@AlertActive", AlertActive.Text);

        DataBase.executeCommandWithParameters(cmd);

        SaveConfirm.Visible = true;
    }


    public string replaceHypen(string word)
    {

        return word.Replace("'", "''");
    }
    public string h3Output { get; set; }


    public string searchString { get; set; }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/");
    }
}