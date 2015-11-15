using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SSADL.CMS;
using System.Net;
using System.Data.SqlClient;

public partial class admin_users_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["whatform"]))
        {
            whatform = Request.QueryString["whatform"];
            AreaShow.Visible = true;
            DataTableReader dtr = DataBase.dbDataTable("Select * from HelpRequestsAssignees where id=" + whatform).CreateDataReader();

            dtr.Read();

            FormTitle = dtr["FormName"].ToString();

            if (!IsPostBack)
            {
                SendReqeustsTo.DataSource = getAllAdminUsers();
                SendReqeustsTo.DataBind();



                string SendRequestsToDB = dtr["SendRequestsTo"].ToString();

                string[] userIds = SendRequestsToDB.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string userId in userIds)
                {
                    if (userId != "")
                    {
                        try
                        {
                            SendReqeustsTo.Items.FindByValue(userId).Selected = true;
                        }
                        catch { }
                    }
                }







            }
            AuditLogUX.tableName = "HelpRequestsAssignees";
            AuditLogUX.CHID = whatform;
            AuditLogUX.tableName2 = "";
        }
       
            
    }

 
   

  

    public string returnSelectedItemsValue(ListBox lsBox)
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

    private DataTable getAllAdminUsers(string addSQL = "")
    {
        string sql = "SELECT ID, CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [users] " + addSQL + " where Active='Y' order by FirstName ASC";
        return DataBase.dbDataTable(sql);
    }
    
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        whatform = Request.QueryString["whatform"];
        string sendRequeststoo = returnSelectedItemsValue(SendReqeustsTo);
        string sqlUPdate = "UPDATE [dbo].[HelpRequestsAssignees]  SET ";
        sqlUPdate += " [SendRequestsTo] ='" + sendRequeststoo + "'  where ID='" + whatform + "'";  //char(150),>
        DataBase.executeCommand(sqlUPdate);
        //  Response.Write(sqlUPdate);
            AuditLogs.log_Changes(whatform, "HelpRequestsAssignees");

    }
    protected void CancelBtn_Click(object sender, EventArgs e)
    {

    }
    public string Form { get; set; }
 



 

    public string FormTitle { get; set; }

    public string whatform { get; set; }
}