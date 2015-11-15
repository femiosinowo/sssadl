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

public partial class admin_users_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            FirstName.Text = FirstNameHF.Value;
            LastName.Text = LastNameHF.Value;
            EmailAddress.Text = EmailAddressHF.Value;
            
        
        }
        DataTable dtAllUsers = DataBase.dbDataTable("Select * from users where Active ='Y' ");
        if (dtAllUsers.Rows.Count >= 14)
        {
            errorEdit.Visible = true;
            ErrorHeader = "Admin users cannot be added now.";
            ErrorMessage = "The database currently holds the maximum numbers of allow users for Ektron";
            updateUser.Enabled = false;

        }
    }
    protected void createUser_Click(object sender, EventArgs e)
    {
        DataTable dtAllUsers = DataBase.dbDataTable("Select * from users ");
        DataRow[] ActiveUsers = dtAllUsers.Select("Active='Y'"); ;
        int activeUsersCount = ActiveUsers.Length;

        if (activeUsersCount <= 14)
        {

            string expression;
            expression = "PIN=" + PIN.Text;
            DataRow[] foundRows;

            // Use the Select method to find all rows matching the filter.
            foundRows = dtAllUsers.Select(expression);

            if (foundRows.Length > 0)
            {
                errorEdit.Visible = true;
                ErrorHeader = "There was an error submitting your request";
                ErrorMessage = "This user already exists in the Database";
                return;
            }

        }
        if (activeUsersCount >= 14)
        {
            errorEdit.Visible = true;
            ErrorHeader = "There was an error submitting your request";
            ErrorMessage = "The database currently holds the maximum numbers of allow users for Ektron";
            return;
        }




        string PINi = PIN.Text;
        string EmailAddressi = EmailAddressHF.Value;
        string FirstNamei = FirstNameHF.Value;
        string LastNamei = LastNameHF.Value;
        string AccessLeveli = AccessLevel.SelectedValue;
        string CORi = "N";
        string CORExpireDatei = CORExpiresOn.Text;
        string Activei = EmployeeActive.SelectedValue;

        if (CORYes.Checked) CORi = "Y";


        string sql = "INSERT INTO [dbo].[users]([PIN],[EmailAddress],[FirstName],[LastName],[AccessLevel],[COR],[CORExpireDate],[Active],[CreateDate])";
        sql += " VALUES('" + PINi + "','" + EmailAddressi + "','" + FirstNamei + "','" + LastNamei + "','" + AccessLeveli + "','" + CORi + "','" + CORExpireDatei + "','" + Activei + "',GETDATE())";
 

        int iresult = DataBase.executeCommand(sql);

        //create ektron contact
         loginSSA.createSSAEktronMember(PINi);

        // Response.Write(iresult);
        if (iresult >= 1)
        {
            entryPanel.Visible = false;
            successedit.Visible = true;
            Response.Redirect("/admin/users/default.aspx?action=successadduser");
        }
        else
        {

            errorEdit.Visible = true;
        }


    }
    public   string WorkAreadPassword = "Ektron0249"; //{ get; set; }
    public   string WorkAreadUsername = "Admin"; //{ get; set; }
    public   string EktronMemberDefaultPassword = "SSAMemberLog1nPassw0rd";

    public void createSSAEktronMember(string PIN)
    {
        //try
        //{

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