using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Mail;

namespace SSADL.CMS
{
    /// <summary>
    /// Summary description for AdminFunc
    /// </summary>
    public class AdminFunc
    {
        public AdminFunc()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataTable ResourceTypeTaxonomyDataTable = CreateAccessTypeOptions(123, true);
        public static DataTable SubjectAreasTaxonomyDataTable = CreateAccessTypeOptions(136);
        public static DataTable AccessTypeTaxonomyDataTable = CreateAccessTypeOptions(118, true);
        public static DataTable ShowInAudienceToolsTaxonomyDataTable = CreateAccessTypeOptions(96);
        public static DataTable ProcumentMethodDataTable = CreateAccessTypeOptions(107, true);
        //public  string AdminID = getMyDBID(loginSSA.myPIN);

        public static Dictionary<string, string> returnUserDetailsByPIN(string userPIN)
        {
            Dictionary<string, string> allUserDetails = new Dictionary<string, string>();

            DataTableReader reader = DataBase.dbDataTable("Select * from AccurintUsers where PIN ='" + userPIN + "'").CreateDataReader();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                allUserDetails.Add(reader.GetName(i), reader[i].ToString().Trim());

            }
            reader.Close();

            return allUserDetails;


        }

        public static string getUrlReferrer()
        {

            try
            {
                return HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch
            {
                return "";

            }
        }

        public static Dictionary<string, string> getUserDetailsByPIN(string userPIN)
        {
            Dictionary<string, string> allUserDetails = new Dictionary<string, string>();

            //DataTableReader reader = DataBase.dbDataTable("Select * from users where PIN ='" + userPIN + "'").CreateDataReader();
            DataTableReader reader = DataBase.dbDataTable("Select * from AccurintUsers where PIN ='" + userPIN + "'").CreateDataReader();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                allUserDetails.Add(reader.GetName(i), reader[i].ToString().Trim());

            }
            reader.Close();

            return allUserDetails;


        }

        public static string getUserDBID(string myPIN)
        {
            return DataBase.returnOneValue("Select ID from users where PIN ='" + myPIN + "' and Active='Y'");

        }
        public static string replaceHypen(string word)
        {
            return word.Replace("'", "\"");
            // return word.Replace("'", "''");


        }
        public static DataTable getAllAdminUsers(string addSQL = "")
        {

            string sql = "SELECT * , CONCAT(LTRIM(RTRIM(FirstName)) , ' ', LTRIM(RTRIM(LastName))) as Name FROM [users] where Active='Y' " + addSQL + " order by FirstName ASC";
            return DataBase.dbDataTable(sql);



        }

        public static Dictionary<string, string> GetProcumentMethodList()
        {
            Dictionary<string, string> ProcumentMethodList2 = new Dictionary<string, string>();
            DataTableReader dtR = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(107), "TaxName", "ASC").CreateDataReader();
            while (dtR.Read())
            {
                //  Response.Write(dtR["TaxName"].ToString());
                ProcumentMethodList2.Add(dtR["TaxID"].ToString(), dtR["TaxName"].ToString());
            }
            return ProcumentMethodList2;

        }

        public static Dictionary<string, string> GetIntialDBValuesSQL(string sql)
        {
            Dictionary<string, string> AuditInitalValues = new Dictionary<string, string>();
            DataTableReader reader = DataBase.dbDataTable(sql).CreateDataReader();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                AuditInitalValues.Add(reader.GetName(i), reader[i].ToString().Trim());

            }
            reader.Close();

            return AuditInitalValues;

        }


        public static Dictionary<string, string> GetIntialDBValuesDT(DataTable dT)
        {
            Dictionary<string, string> AuditInitalValues = new Dictionary<string, string>();
            DataTableReader reader = dT.CreateDataReader();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                AuditInitalValues.Add(reader.GetName(i), reader[i].ToString().Trim());

            }
            reader.Close();

            return AuditInitalValues;

        }

        public static Dictionary<string, string> GetIntialDBValuesDTs(DataSet dT)
        {
            Dictionary<string, string> AuditInitalValues = new Dictionary<string, string>();
            DataTableReader reader = dT.CreateDataReader();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                AuditInitalValues.Add(reader.GetName(i), reader[i].ToString().Trim());

            }
            reader.Close();

            return AuditInitalValues;

        }


        public static DataTable CreateAccessTypeOptions(long taxID, bool showSelect = false)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TaxID");
            dt.Columns.Add("TaxName");
            if (showSelect)
            {
                dt.Rows.Add("", " - Select One - ");
            }
            DataTableReader dtR = commonfunctions.sortDataTable(commonfunctions.getTaxonomyTree(taxID), "TaxName", "ASC").CreateDataReader();
            while (dtR.Read())
            {
                dt.Rows.Add(dtR["TaxID"].ToString(), dtR["TaxName"].ToString());
            }


            return dt;


        }


        public static void sendEmailMessage(string mailTo,  string mailFrom , string mailSubject, string mailBody)
        {
            try
            {
                string mailServerRelay = "";
                MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(mailTo);


                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment("your attachment file");
                message.Attachments.Add(attachment);

                //  message.To.Add("figleaf@msba.org");
                // message.CC.Add("msba@figleaf.com");
                message.Subject = mailSubject;
                message.From = new System.Net.Mail.MailAddress(mailFrom);
                message.IsBodyHtml = true;
                message.Body = mailBody;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServerRelay);
                smtp.Send(message);
            }
            catch { }
        }



        public static string getResourceName(string resourceID)
        {
            return DataBase.returnOneValue("Select ResourceName from Resources where ID='" + resourceID + "'");
        }


        public static int CurrentFiscalYear(DateTime dateTime)
        {
            return (dateTime.Month >= 10 ? dateTime.Year + 1 : dateTime.Year);
        }

        public static string ToFinancialYearShort(DateTime dateTime)
        {
            return "FY" + (dateTime.Month >= 10 ? dateTime.AddYears(1).ToString("yy") : dateTime.ToString("yy"));
        }

        public static string CalculatePercentage(Double previous, Double current)
        {
            string output = "";
            if (previous ==0) previous = 1;
             if (current ==0) current = 1;
            double result = ((current - previous) / (double)previous);
            if (result < 0)
            {
                output = ((-1) * result).ToString("p") + " decrease ";
            }
            else
            {
                output = result.ToString("p") + " increase ";
            }
            return output;
        }
    }
}