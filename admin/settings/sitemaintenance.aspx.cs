using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSADL.CMS;
using System.Net;

public partial class admin_users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {

            WhereFrom.Value = AdminFunc.getUrlReferrer();
            
  

            DataTableReader dtR = DataBase.dbDataTable("Select * from SiteMaintenance where ID='1'").CreateDataReader();

            while (dtR.Read())
            {
                ///Alert
                ///
                string StartTime = dtR["StartTime"].ToString().Trim();
                string EndTime = dtR["EndTime"].ToString().Trim();
                string MessageToDisplay = dtR["MessageToDisplay"].ToString().Trim();
                string DaysOfWeek = dtR["DaysOfWeek"].ToString().Trim();
                string MessageTitle = dtR["MessageTitle"].ToString().Trim();
                string Enable = dtR["Enable"].ToString().Trim();


                SMTitle.Text = WebUtility.HtmlDecode(MessageTitle);
                SMMessage.Text = WebUtility.HtmlDecode(MessageToDisplay);
                AlertStartOn.Text = StartTime;
                AlertEndOn.Text = EndTime;

                AlertActive.Items.FindByValue(Enable).Selected = true;


                try
                {
                    string[] daysIDs = DaysOfWeek.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string dayID in daysIDs)
                    {
                        daysOfWeeksCB.Items.FindByValue(dayID).Selected = true;
                    }
                }
                catch { }

                //switch (key)
                //{

                //    case "OutageTitle":
                //        OutageTitle.Text = value;
                //        break;

                //    case "OutageDescription":
                //        OutageDescription.Text = value;
                //        break;


                //    //case "OutageActive":
                //    //    OutageActive.Text = value;
                //    //    break;



            }

        }
    }






    protected void Button1_Click(object sender, EventArgs e)
    {

        string daysSelected = returnSelectedItemsValue(daysOfWeeksCB);

        string updatesql = "";

        //updatesql += "   UPDATE [dbo].[SiteMaintenance] ";
        //updatesql += " SET [StartTime] = '" + AlertStartOn.Text + "' ";
        //updatesql += "  ,[EndTime] = '" + AlertEndOn.Text + "' ";
        //updatesql += "  ,[MessageTitle] ='" + AdminFunc.replaceHypen(SMTitle.Text) + "' ";
        //updatesql += "  ,[MessageToDisplay] = '" + AdminFunc.replaceHypen(SMMessage.Text) + "' ";
        //updatesql += "  ,[DaysOfWeek] = '" + daysSelected + "' ";
        //updatesql += "  ,[Enable] = '" + AlertActive.SelectedValue + "' where ID='1' ;";


        updatesql += "   UPDATE [dbo].[SiteMaintenance] ";
        updatesql += " SET [StartTime] = '" + AlertStartOn.Text + "' ";
        updatesql += "  ,[EndTime] = '" + AlertEndOn.Text + "' ";
        updatesql += "  ,[MessageTitle] =@MessageTitle ";
        updatesql += "  ,[MessageToDisplay] = @MessageToDisplay ";
        updatesql += "  ,[DaysOfWeek] = '" + daysSelected + "' ";
        updatesql += "  ,[Enable] = '" + AlertActive.SelectedValue + "' where ID='1' ;";


        SqlCommand cmd;
        cmd = new SqlCommand(updatesql);
        cmd.Parameters.AddWithValue("@MessageTitle", WebUtility.HtmlEncode(SMTitle.Text));
        cmd.Parameters.AddWithValue("@MessageToDisplay", WebUtility.HtmlEncode(SMMessage.Text));

        DataBase.executeCommandWithParameters(cmd);

        SaveConfirm.Visible = true;

    }


    public string returnSelectedItemsValue(CheckBoxList lsBox)
    {
        string seletectedItemsValue = ","; // string.Empty;

        foreach (ListItem li in lsBox.Items)
        {
            if (li.Selected == true)
            {
                seletectedItemsValue += li.Value + ",";
            }
        }

        return seletectedItemsValue;
    }


    public string h3Output { get; set; }


    public string searchString { get; set; }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/");
    }
}