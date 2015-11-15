using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSADL.CMS;
using Ektron.Cms.Framework.User;
using Ektron.Cms;
using Ektron.Cms.User;
using Ektron.Cms.Common;
public partial class admin_users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

     



            userID = Request.QueryString["userid"].ToString();
            DataTableReader dtR = DataBase.dbDataTable("Select * from users where ID = " + userID).CreateDataReader();

            dtR.Read();

            PIN.Text = dtR["PIN"].ToString().Trim();
            EmailAddress.Text = dtR["EmailAddress"].ToString().Trim();
            FirstName.Text = dtR["FirstName"].ToString().Trim();
            LastName.Text = dtR["LastName"].ToString().Trim();
            CORExpiresOn.Text = commonfunctions.getDatabaseDateformat(dtR["CORExpireDate"].ToString().Trim());

            CORS = dtR["COR"].ToString().Trim();
            AccessLevelS = dtR["AccessLevel"].ToString().Trim();
            ActiveS = dtR["Active"].ToString().Trim();

            AccessLevel.SelectedValue = AccessLevelS;
            EmployeeActive.SelectedValue = ActiveS;
            if (CORS == "Y")
            {
                CORYes.Checked = true;
                CORNo.Checked = false;
            }
            else
            {
                CORYes.Checked = false;
                CORNo.Checked = true;
            }

            AuditLogUX.tableName = "users";
            AuditLogUX.CHID = userID;

            DataTable dtAllUsers = DataBase.dbDataTable("Select * from users where Active ='Y' ");
            if (dtAllUsers.Rows.Count >= 14)
            {
                
                if (ActiveS == "N")
                {
                    errorEdit.Visible = true;
                    ErrorHeader = "This User Status Cannot Be Active.";
                    ErrorMessage = "The database currently holds the maximum numbers of allow users for Ektron";
                   // DisableActiveButton.Visible = true;
                    EmployeeActive.Enabled = false;
                }  
               
                //EmployeeActive.Enabled = false;
                
            }
        }
    }
    protected void updateUser_Click(object sender, EventArgs e)
    {
        
        string PINi = PIN.Text;
        string EmailAddressi = EmailAddress.Text;
        string FirstNamei = FirstName.Text;
        string LastNamei = LastName.Text;
        string AccessLeveli = AccessLevel.SelectedValue;
        string CORi = "N";
        string CORExpireDatei = CORExpiresOn.Text;
        string Activei = EmployeeActive.SelectedValue;
        if (Activei.Trim() == "") Activei = ActiveS;
        if (CORYes.Checked) CORi = "Y";
        if (Activei == "Y")
        {
            //add user to ektron
            createSSAEktronMember(PINi);

        }
        else
        {
            //remove from ektron
            removeSSAEktronMember(PINi);
        }
        string sql = " UPDATE [dbo].[users] ";
        sql += " SET [PIN] = '" + PINi + "',[EmailAddress] = '" + EmailAddressi + "',[FirstName] = '" + FirstNamei + "',[LastName] = '" + LastNamei + "',[AccessLevel] = '" + AccessLeveli + "' ,[COR] = '" + CORi + "',[CORExpireDate] = '" + CORExpireDatei + "' ";
        sql += "   ,[Active] = '" + Activei + "' ";
        sql += " WHERE ID = '" + userID + "' ;";

         //Response.Write(sql);

       int iresult =  DataBase.executeCommand(sql);
       AuditLogs.log_Changes(userID, "users");
       if (iresult>1)
       {
           successedit.Visible = true;
           //  Response.Redirect("/admin/users/success.aspx");
           Response.Redirect("/admin/users/default.aspx?action=userupdated");
       }
       else
       {

           errorEdit.Visible = true;
       }

 

        


    }

    public void removeSSAEktronMember(string UserPIN)
    {
        try
        {
            UserManager Usermanager = new UserManager();
            UserCriteria criteria = new UserCriteria(UserProperty.UserName, EkEnumeration.OrderByDirection.Ascending);

            criteria.AddFilter(UserProperty.UserName, CriteriaFilterOperator.EqualTo, UserPIN);

            List<UserData> UserList = Usermanager.GetList(criteria);
            long EktronUserID = UserList[0].Id;

            if (Ektron.Cms.Framework.Context.UserContextService.Current.IsLoggedIn)
            {
                Usermanager.Delete(EktronUserID);
                // add user to group MSBA Members
                //UserGroupManager UserGroupmanager = new UserGroupManager();
                //Add a User  to a UserGroup
                // UserGroupmanager.DeleteUser(1, newUserdata.Id);
            }
        }
        catch { }


    }

    public string WorkAreadPassword = "Ektron0249"; //{ get; set; }
    public string WorkAreadUsername = "Admin"; //{ get; set; }
    public string EktronMemberDefaultPassword = "SSAMemberLog1nPassw0rd";
    public void createSSAEktronMember(string PIN)
    {
        try
        {

            Dictionary<string, string> UserDetails = loginSSA.GetUsersDetails(PIN);


            UserManager Usermanager = new UserManager();
            CustomAttributeList attrList = new CustomAttributeList();
            CustomAttribute timeZone = new CustomAttribute();
            timeZone.Name = "Time Zone";
            timeZone.Value = "Eastern Standard Time";
            attrList.Add(timeZone);

            UserData newUserdata = new UserData()
            {
                Username = PIN,
                Password = EktronMemberDefaultPassword,
                FirstName = UserDetails["FirstName"],
                LastName = UserDetails["LastName"],
                DisplayName = UserDetails["DisplayName"],
                Email = UserDetails["Email"],
                CustomProperties = attrList,
                // IsMemberShip = true
            };

            if (Ektron.Cms.Framework.Context.UserContextService.Current.IsLoggedIn)
            {
                Usermanager.Add(newUserdata);
                // add user to group MSBA Members
                UserGroupManager UserGroupmanager = new UserGroupManager();
                //Add a User  to a UserGroup
                UserGroupmanager.AddUser(1, newUserdata.Id);
            }
        }
        catch { }
    }

    public virtual string userID
    {
        get
        {
            if (ViewState["userID"] == null)
            {
                ViewState["userID"] = Request.QueryString["userid"].ToString();
            }
            return (string)ViewState["userID"];
        }
        set
        {
            ViewState["userID"] = value;
        }
    }

    public string CORS { get; set; }

    public string AccessLevelS { get; set; }

    public string ActiveS { get; set; }
    public string ErrorHeader { get; set; }

    public string ErrorMessage { get; set; }
}