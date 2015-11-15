using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSADL.CMS;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;

public partial class admin_reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        reportID = Request.QueryString["reportid"].ToString();

        DataTableReader dtr = DataBase.dbDataTable("Select * from Report  where ID=" + reportID).CreateDataReader();
        if (dtr.HasRows)
        {
            dtr.Read();          
            ReportName = dtr["Name"].ToString().Trim();   


        }


    }





    public string reportID { get; set; }
    protected void Send_Click(object sender, EventArgs e)
    {
        invalidEmailsFound = "";
        string[] allEmails = emails.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string validEmails = "";
        foreach (string enteredemail in allEmails)
        {
            string vem = checkValidEmailWithCore(enteredemail);
            if (vem != "")
            {
                validEmails += vem + ",";
            }
            else
            {
                // Response.Write(enteredemail + " is invalid");
                //  break; 
                invalidEmailsFound += enteredemail + "<br/>";
            }
            //  ArrayList validEmails = new ArrayList();

        }

        if (invalidEmailsFound != "")
        {
            InvalidEmailPanel.Visible = true;
            return;
        }
        else
        {
            InvalidEmailPanel.Visible = false;
        }

 
    }

     

    
    private string checkValidEmailWithCore(string email)
    {
        Dictionary<string, string> UsersDetails = loginSSA.GetUsersDetails("", email);
        if (UsersDetails.ContainsKey("PIN"))
        {
            return email;
        }
        else
        {
            return "";
        }

        // throw new NotImplementedException();
    }

    public string invalidEmailsFound { get; set; }

    public string ReportName { get; set; }
}