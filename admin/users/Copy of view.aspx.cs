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

    }
    protected void createUser_Click(object sender, EventArgs e)
    {

        string PINi = PIN.Text;
        string EmailAddressi = EmailAddress.Text;
        string FirstNamei = FirstName.Text;
        string LastNamei = LastName.Text;
        string AccessLeveli = AccessLevel.SelectedValue;
        //string CORi = COR.SelectedValue;
        //string CORExpireDatei = CORExpireDate.Text;
        string Activei = Active.SelectedValue;


        //string sql = "INSERT INTO [dbo].[users]([PIN],[EmailAddress],[FirstName],[LastName],[AccessLevel],[COR],[CORExpireDate],[Active],[CreateDate])";
        //sql += " VALUES('" + PINi + "','" + EmailAddressi + "','" + FirstNamei + "','" + LastNamei + "','" + AccessLeveli + "','" + CORi + "','" + CORExpireDatei + "','" + Activei + "',GETDATE())";

       // Response.Write(sql);

   //   Response.Write(  DataBase.executeCommand(sql).ToString());

      //  using (SqlConnection cnn = new SqlConnection(commonfunctions.ektronConnectionStr))
      //  {
      //      using (SqlCommand cmd = new SqlCommand())
      //      {

      //          cmd.CommandType = CommandType.Text;
      //          cnn.Open();
      //          cmd.Connection = cnn;



      //         cmd.CommandText = sql;
      //          cmd.ExecuteNonQuery();

      //      }

      //  }

      //Response.Redirect("/admin/users/success.aspx");


    }
}