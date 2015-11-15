using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using SSADL.CMS;
using System.IO;
using System.Configuration;

public partial class admin_resources_Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTableReader dtr = DataBase.dbDataTable("Select * from [dbo].[Sheet1$] ").CreateDataReader();

        while (dtr.Read())
        {
        string sql2 = " INSERT INTO [dbo].[Resources]([ResourceName],[Description],[ResourceTypeTaxonomy],[ResourceURLlink],[ResourceFileType],[ShowInNewWindow],[AdminResourceURL],[AdminUsername],[AdminPassword],[File1],[File2],[File3],[ListInResearchGuideFile1],[ListInResearchGuideFile2],[ListInResearchGuideFile3],[SubjectAreasTaxonomy],[ShowInSubjectAreas],[ShowInDatabases],[ShowInTrainingRequestForm],[ShowInAudienceToolsTaxonomy],[Mandatory],[AssociatedNetwork],[AccessTypeTaxonomy],[PasswordRequestsRestrictedToManagers],[TargetUsers],[BusinessPurposeOfResource],[ResourceDisplayStatus]) ";
        sql2 += " VALUES(@ResourceName,@Description,@ResourceTypeTaxonomy,@ResourceURLlink,@ResourceFileType,@ShowInNewWindow,@AdminResourceURL,@AdminUsername,@AdminPassword,@File1,@File2,@File3,@ListInResearchGuideFile1,@ListInResearchGuideFile2,@ListInResearchGuideFile3,@SubjectAreasTaxonomy,@ShowInSubjectAreas,@ShowInDatabases,@ShowInTrainingRequestForm,@ShowInAudienceToolsTaxonomy,@Mandatory,@AssociatedNetwork,@AccessTypeTaxonomy,@PasswordRequestsRestrictedToManagers,@TargetUsers,@BusinessPurposeOfResource,@ResourceDisplayStatus) ";
           
        SqlCommand sqlcmd = new SqlCommand(sql2);
        sqlcmd.Parameters.Add("@ResourceName", SqlDbType.VarChar); // dtr["Resource Name"]); //nchar(50),>
        sqlcmd.Parameters.Add("@Description", SqlDbType.VarChar); // dtr["Description"]); //varchar(max),>
        sqlcmd.Parameters.Add("@ResourceTypeTaxonomy", SqlDbType.VarChar); // dtr["Resource Type"]); //nchar(50),>
        sqlcmd.Parameters.Add("@ResourceURLlink", SqlDbType.VarChar); // dtr["Link to Resource"]); //varchar(max),>
        sqlcmd.Parameters.Add("@ResourceFileType", SqlDbType.VarChar); // dtr["Resource File"]); //varchar(max),>
        sqlcmd.Parameters.Add("@ShowInNewWindow", SqlDbType.VarChar); // dtr["Show In Popup?"]); //nchar(50),>
        sqlcmd.Parameters.Add("@AdminResourceURL", SqlDbType.VarChar); // dtr["Admin Link"]); //nchar(50),>
        sqlcmd.Parameters.Add("@AdminUsername", SqlDbType.VarChar); // dtr["Admin User Name"]); //nchar(50),>
        sqlcmd.Parameters.Add("@AdminPassword", SqlDbType.VarChar); // dtr["Admin Password"]); //nchar(50),>
        sqlcmd.Parameters.Add("@File1", SqlDbType.VarChar); // ""); //varchar(max),>
        sqlcmd.Parameters.Add("@File2", SqlDbType.VarChar); // ""); //varchar(max),>
        sqlcmd.Parameters.Add("@File3", SqlDbType.VarChar); // ""); //varchar(max),>
        sqlcmd.Parameters.Add("@ListInResearchGuideFile1", SqlDbType.VarChar); // 'N'); //nchar(1),>
        sqlcmd.Parameters.Add("@ListInResearchGuideFile2", SqlDbType.VarChar); // 'N'); //nchar(1),>
        sqlcmd.Parameters.Add("@ListInResearchGuideFile3", SqlDbType.VarChar); // 'N'); //nchar(1),>
        sqlcmd.Parameters.Add("@SubjectAreasTaxonomy", SqlDbType.VarChar); // dtr["Subject Areas"]); //varchar(max),>
        sqlcmd.Parameters.Add("@ShowInSubjectAreas", SqlDbType.VarChar); // dtr["Show Resources In Subject Areas?"]); //nchar(10),>
        sqlcmd.Parameters.Add("@ShowInDatabases", SqlDbType.VarChar); // dtr["Show Resources in Databases A-Z?"]); //nchar(10),>
        sqlcmd.Parameters.Add("@ShowInTrainingRequestForm", SqlDbType.VarChar); // dtr["Show in Training Request Form?"]); //nchar(10),>
        sqlcmd.Parameters.Add("@ShowInAudienceToolsTaxonomy", SqlDbType.VarChar); // dtr["Audience Tool"]); //varchar(max),>
        sqlcmd.Parameters.Add("@Mandatory", SqlDbType.VarChar); // dtr["Mandatory/ Discretionary"]); //nchar(10),>
        sqlcmd.Parameters.Add("@AssociatedNetwork", SqlDbType.VarChar); // dtr["Associated Network"]); //nchar(50),>
        sqlcmd.Parameters.Add("@AccessTypeTaxonomy", SqlDbType.VarChar); // dtr["Access Type"]); //nchar(50),>

        sqlcmd.Parameters.Add("@PasswordRequestsRestrictedToManagers", SqlDbType.VarChar); // dtr["Password requests restricted to managers?"]); //nchar(10),>
        sqlcmd.Parameters.Add("@TargetUsers", SqlDbType.VarChar); // dtr["Users of resource"]); //varchar(max),>
        sqlcmd.Parameters.Add("@BusinessPurposeOfResource", SqlDbType.VarChar); // dtr["How does the agency use this resource"]); //varchar(max),>
        sqlcmd.Parameters.Add("@ResourceDisplayStatus", SqlDbType.VarChar); // dtr["Resource Display Status"]); //nchar(10),>)


        string sqlContract = " INSERT INTO [dbo].[ResourcesContract]([ResourceID],[FiscalYear],[PeriodofPerformanceStart],[PeriodofPerformanceEnd],[RequisitionNumber],[ContractNumber],[NumberOfLicensesOwned],[LicensesCount],[AnnualContractCost],[ProcurementMethod],[ProcurementMethodOther],[ContractFileName],[CriticalNotes],[NotifyOfExpirationThisManyDaysInAdvance],[VendorName],[RepresentativeName],[RepresentativeEmail],[RepresentativePhone],[TechnicalContactName],[TechnicalContactEmail],[TechnicalContactPhone],[NewFeatures],[NotificationActiveStartDate],[NotificationActiveEndDate]) ";
        sqlContract += " VALUES(@ResourceID,@FiscalYear,@PeriodofPerformanceStart,@PeriodofPerformanceEnd,@RequisitionNumber,@ContractNumber,@NumberOfLicensesOwned,@LicensesCount,@AnnualContractCost,@ProcurementMethod,@ProcurementMethodOther,@ContractFileName,@CriticalNotes,@NotifyOfExpirationThisManyDaysInAdvance,@VendorName,@RepresentativeName,@RepresentativeEmail,@RepresentativePhone,@TechnicalContactName,@TechnicalContactEmail,@TechnicalContactPhone,@NewFeatures,@NotificationActiveStartDate,@NotificationActiveEndDate) ";
            

        SqlCommand sqlcmdContract = new SqlCommand(sqlContract);
        sqlcmdContract.Parameters.Add("@ResourceID", SqlDbType.VarChar); // ResourceID); // nchar(10)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@FiscalYear", SqlDbType.VarChar); // dtr["FY 20XX"]); // nchar(10)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@PeriodofPerformanceStart", SqlDbType.VarChar); // start); // nchar(10)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@PeriodofPerformanceEnd", SqlDbType.VarChar); // end); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@RequisitionNumber", SqlDbType.VarChar); // dtr["Req #"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@ContractNumber", SqlDbType.VarChar); // dtr["Contract #"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@NumberOfLicensesOwned", SqlDbType.VarChar); // ""); // nchar(10)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@LicensesCount", SqlDbType.VarChar); // dtr["How Many licenses?"]); // nchar(10)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@AnnualContractCost", SqlDbType.VarChar); // dtr["Annual Contract Cost"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@ProcurementMethod", SqlDbType.VarChar); // dtr["Procurement Method"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@ProcurementMethodOther", SqlDbType.VarChar); //""); // varchar(max)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@ContractFileName", SqlDbType.VarChar); // contractpathURL + filename); // varchar(max)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@CriticalNotes", SqlDbType.VarChar); // dtr["Critical Notes"]); // varchar(max)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@NotifyOfExpirationThisManyDaysInAdvance", SqlDbType.VarChar); // dtr["How many days in advance for alert?"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        // sqlcmdContract.Parameters.Add("@LibraryContractingOfficersRepresentative",   SqlDbType.VarChar); // dtr["Description"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@VendorName", SqlDbType.VarChar); // dtr["Vendor Name"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@RepresentativeName", SqlDbType.VarChar); // dtr["Account Rep Name"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@RepresentativeEmail", SqlDbType.VarChar); // dtr["Account Rep email"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@RepresentativePhone", SqlDbType.VarChar); // dtr["Account Rep Phone"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@TechnicalContactName", SqlDbType.VarChar); // dtr["Technical Assistance Contact Name"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@TechnicalContactEmail", SqlDbType.VarChar); // dtr["Tech Assist email"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@TechnicalContactPhone", SqlDbType.VarChar); // dtr["Tech Assist Phone"]); // nchar(50)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@NewFeatures", SqlDbType.VarChar); // dtr["Description of New Features"]); // varchar(max)",   SqlDbType.VarChar); // dtr["Description"]); //>
        sqlcmdContract.Parameters.Add("@NotificationActiveStartDate", SqlDbType.VarChar); //"");
        sqlcmdContract.Parameters.Add("@NotificationActiveEndDate", SqlDbType.VarChar); // "");

   
          //  Response.Write(dtr["Resource Name"].ToString() + "<br/>");

            

        
            
            sqlcmd.Parameters["@ResourceName"].Value = dtr["Resource Name"].ToString(); //nchar(50),>
            sqlcmd.Parameters["@Description"].Value = dtr["Description"].ToString(); //varchar(max),>
            sqlcmd.Parameters["@ResourceTypeTaxonomy"].Value = dtr["Resource Type"].ToString(); //nchar(50),>
            sqlcmd.Parameters["@ResourceURLlink"].Value = dtr["Link to Resource"].ToString(); //varchar(max),>
            sqlcmd.Parameters["@ResourceFileType"].Value = dtr["Resource File"].ToString(); //varchar(max),>
            sqlcmd.Parameters["@ShowInNewWindow"].Value = dtr["Show In Popup?"].ToString(); //nchar(50),>
            sqlcmd.Parameters["@AdminResourceURL"].Value = dtr["Admin Link"].ToString(); //nchar(50),>
            sqlcmd.Parameters["@AdminUsername"].Value = dtr["Admin User Name"].ToString(); //nchar(50),>
            sqlcmd.Parameters["@AdminPassword"].Value = dtr["Admin Password"].ToString(); //nchar(50),>
            sqlcmd.Parameters["@File1"].Value = ""; //varchar(max),>
            sqlcmd.Parameters["@File2"].Value = ""; //varchar(max),>
            sqlcmd.Parameters["@File3"].Value = ""; //varchar(max),>
            sqlcmd.Parameters["@ListInResearchGuideFile1"].Value = 'N'; //nchar(1),>
            sqlcmd.Parameters["@ListInResearchGuideFile2"].Value = 'N'; //nchar(1),>
            sqlcmd.Parameters["@ListInResearchGuideFile3"].Value = 'N'; //nchar(1),>
            sqlcmd.Parameters["@SubjectAreasTaxonomy"].Value = dtr["Subject Areas"].ToString(); //varchar(max),>
            sqlcmd.Parameters["@ShowInSubjectAreas"].Value = dtr["Show Resources In Subject Areas?"].ToString(); //nchar(10),>
            sqlcmd.Parameters["@ShowInDatabases"].Value = dtr["Show Resources in Databases A-Z?"].ToString(); //nchar(10),>
            sqlcmd.Parameters["@ShowInTrainingRequestForm"].Value = dtr["Show in Training Request Form?"].ToString(); //nchar(10),>
            sqlcmd.Parameters["@ShowInAudienceToolsTaxonomy"].Value = dtr["Audience Tool"].ToString(); //varchar(max),>
            sqlcmd.Parameters["@Mandatory"].Value = dtr["Mandatory/ Discretionary"].ToString(); //nchar(10),>
            sqlcmd.Parameters["@AssociatedNetwork"].Value = dtr["Associated Network"].ToString(); //nchar(50),>
            sqlcmd.Parameters["@AccessTypeTaxonomy"].Value = dtr["Access Type"].ToString(); //nchar(50),>

            sqlcmd.Parameters["@PasswordRequestsRestrictedToManagers"].Value = dtr["Password requests restricted to managers?"].ToString(); //nchar(10),>
            sqlcmd.Parameters["@TargetUsers"].Value = dtr["Users of resource"].ToString(); //varchar(max),>
            sqlcmd.Parameters["@BusinessPurposeOfResource"].Value = dtr["How does the agency use this resource"].ToString(); //varchar(max),>
            sqlcmd.Parameters["@ResourceDisplayStatus"].Value = dtr["Resource Display Status"].ToString(); //nchar(10),>)

            string ResourceID = DataBase.executeCommandWithParametersReturnIDENTITY(sqlcmd);

           // 

            string ResourceName = dtr["Resource Name"].ToString().Replace("'", "").Replace(",", "");
             string filename = dtr["Contract PDF"].ToString();
             string fileURL = "";
            string path = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + ResourceName + "\\";
            string pathURL = commonfunctions.host + "/uploadedfiles/Resources/" + ResourceName + "/";
            string contractpathURL = commonfunctions.host + "/uploadedfiles/Resources/" + ResourceName + "/Contracts/";
            string contractpath = commonfunctions.BaseDirectory + "\\uploadedfiles\\Resources\\" + ResourceName + "\\Contracts\\";
            try { createDirectory(path); }
            catch { }
            try { createDirectory(contractpath); }
            catch { }

            if (filename != "")
            {
                File.Copy(@"c:\ssadlfiles\" + filename, contractpath + filename, true);
               fileURL = contractpathURL + filename;
            }
           
            string year = dtr["Per of Perf"].ToString();
            string start = "";
            string end = "";
            if(year != "")
            {
                string[] yyyr = year.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                start = yyyr[0];
                end = yyyr[1];
            }
            string licc = dtr["How Many licenses?"].ToString().Trim();
            string NumberOfLicensesOwned = "";
            string LicensesCount = "";
            if (licc == "unlimited")
            {
                NumberOfLicensesOwned = "Unlimited";

            }
            else
            {
                LicensesCount = dtr["How Many licenses?"].ToString();
            }

            sqlcmdContract.Parameters["@ResourceID"].Value = ResourceID; // nchar(10)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@FiscalYear"].Value = dtr["FY 20XX"]; // nchar(10)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@PeriodofPerformanceStart"].Value = start; // nchar(10)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@PeriodofPerformanceEnd"].Value = end; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@RequisitionNumber"].Value = dtr["Req #"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@ContractNumber"].Value = dtr["Contract #"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@NumberOfLicensesOwned"].Value = NumberOfLicensesOwned; // nchar(10)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@LicensesCount"].Value = LicensesCount; // nchar(10)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@AnnualContractCost"].Value = dtr["Annual Contract Cost"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@ProcurementMethod"].Value = dtr["Procurement Method"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@ProcurementMethodOther"].Value = ""; // varchar(max)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@ContractFileName"].Value = fileURL; // varchar(max)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@CriticalNotes"].Value = dtr["Critical Notes"]; // varchar(max)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@NotifyOfExpirationThisManyDaysInAdvance"].Value = dtr["How many days in advance for alert?"]; // nchar(50)"].Value =  dtr["Description"]; //>
            // sqlcmdContract.Parameters["@LibraryContractingOfficersRepresentative"].Value =  dtr["Description"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@VendorName"].Value = dtr["Vendor Name"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@RepresentativeName"].Value = dtr["Account Rep Name"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@RepresentativeEmail"].Value = dtr["Account Rep email"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@RepresentativePhone"].Value = dtr["Account Rep Phone"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@TechnicalContactName"].Value = dtr["Technical Assistance Contact Name"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@TechnicalContactEmail"].Value = dtr["Tech Assist email"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@TechnicalContactPhone"].Value = dtr["Tech Assist Phone"]; // nchar(50)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@NewFeatures"].Value = dtr["Description of New Features"]; // varchar(max)"].Value =  dtr["Description"]; //>
            sqlcmdContract.Parameters["@NotificationActiveStartDate"].Value = "";
            sqlcmdContract.Parameters["@NotificationActiveEndDate"].Value = "";

            DataBase.executeCommandWithParameters(sqlcmdContract);
           // sqlcmd.Parameters.Clear();
           // sqlcmdContract.Parameters.Clear();
           

        }
    }


    public   string executeCommandWithParametersReturnIDENTITY(SqlCommand cmd, string ConnectionString = "Admin.DbConnection")
    {

        string results = "0";
       string  ConnectionStr = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString; ;
        using (SqlConnection cnn = new SqlConnection(ConnectionStr))
        {
           // try
           // {
                cmd.CommandType = CommandType.Text;
                cnn.Open();
                cmd.Connection = cnn;




                cmd.ExecuteNonQuery();


                SqlDataReader readers;
                cmd.CommandText = "SELECT @@IDENTITY";
                readers = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (readers.Read())
                {
                    results = readers[0].ToString();
                    readers.Close();
                }

          //  }
          //  catch { results = "0"; }
          //  finally
          //  {

             //   cnn.Close();
            //    cmd.Dispose();

           // }




        }

        return results;


    }
    public static void createDirectory(string path)
    {
        // Specify the directory you want to manipulate. 
        // string path = @"c:\MyDir";

        try
        {
            // Determine whether the directory exists. 
            if (Directory.Exists(path))
            {

                return;
            }

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(path);
            // Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

            // Delete the directory.
            //    di.Delete();
            //  Console.WriteLine("The directory was deleted successfully.");
        }
        catch (Exception e)
        {
            //  Console.WriteLine("The process failed: {0}", e.ToString());
        }
        finally { }
    }
}