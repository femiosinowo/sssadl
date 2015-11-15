using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace SSADL.CMS
{
    /// <summary>
    /// Summary description for AuditLogs
    /// </summary>
    public class AuditLogs
    {
        public AuditLogs()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void log_Changes(string ID, string tableName)
        {
            string NameofFieldChanged = ""; string BeforeValue = ""; string AfterValue = ""; string UpdateDate = ""; string auditInsertSQL = "";

            string auditSQL = "Select * from Audit where TableName = '" + tableName + "' and PrimaryKeyValue = '" + ID + "' ";
           
            DataTableReader dtr = DataBase.dbDataTable(auditSQL).CreateDataReader();
            while (dtr.Read())
            {
                NameofFieldChanged = dtr["FieldName"].ToString();
                BeforeValue = dtr["OldValue"].ToString();
                AfterValue = dtr["NewValue"].ToString();
                UpdateDate = dtr["UpdateDate"].ToString();
                auditInsertSQL = "INSERT INTO [dbo].[" + tableName + "ChangeHistory]([CHID],[NameofFieldChanged],[BeforeValue],[AfterValue],[ChangemadebyPIN],[ChangeDateTime]) ";
                auditInsertSQL += " VALUES ('" + ID + "' , '" + NameofFieldChanged + "' ,@BeforeValue ,@AfterValue  ,'" + loginSSA.myPIN + "'   , '" + UpdateDate + "') ;";
                auditInsertSQL += "Delete from Audit where TableName = '" + tableName + "' and PrimaryKeyValue = '" + ID + "' ";

                SqlCommand cmd = new SqlCommand(auditInsertSQL);
                cmd.Parameters.AddWithValue("@BeforeValue", BeforeValue);
                cmd.Parameters.AddWithValue("@AfterValue", AfterValue);
               
                DataBase.executeCommandWithParameters(cmd);
            }
           
         //   HttpContext.Current.Response.Write(auditInsertSQL);
          //  DataBase.executeCommand(auditInsertSQL);

           
        }

        public static void log_AccessToResourceFormChanges(string ID)
        {
            string NameofFieldChanged = ""; string BeforeValue = ""; string AfterValue = ""; string UpdateDate = ""; string auditInsertSQL = "";

            string auditSQL = "Select * from Audit where TableName = 'AccessToResourceForm' and PrimaryKeyValue = '" + ID + "' ";
            DataTableReader dtr = DataBase.dbDataTable(auditSQL).CreateDataReader();
            while (dtr.Read())
            {
                NameofFieldChanged = dtr["FieldName"].ToString();
                BeforeValue = dtr["OldValue"].ToString();
                AfterValue = dtr["NewValue"].ToString();
                UpdateDate = dtr["UpdateDate"].ToString();
                auditInsertSQL = "INSERT INTO [dbo].[AccessToResourceFormChangeHistory]([FormSubmissionID],[NameofFieldChanged],[BeforeValue],[AfterValue],[ChangemadebyPIN],[ChangeDateTime]) ";
                auditInsertSQL += " VALUES ('" + ID + "' , '" + NameofFieldChanged + "' ,@BeforeValue  ,@AfterValue  ,'" + loginSSA.myPIN + "'   , '" + UpdateDate + "') ;";
                auditInsertSQL += "Delete from Audit where TableName = 'AccessToResourceForm' and PrimaryKeyValue = '" + ID + "' ";


                SqlCommand cmd = new SqlCommand(auditInsertSQL);
                cmd.Parameters.AddWithValue("@BeforeValue", BeforeValue);
                cmd.Parameters.AddWithValue("@AfterValue", AfterValue);
                DataBase.executeCommandWithParameters(cmd);
            }
           
         //   DataBase.executeCommand(auditInsertSQL);
        }



        public static void log_PasswordAssignmentsChanges(string ID)
        {
            string NameofFieldChanged = ""; string BeforeValue = ""; string AfterValue = ""; string UpdateDate = ""; string auditInsertSQL = "";

            string auditSQL = "Select * from Audit where TableName = 'PasswordAssignments' and PrimaryKeyValue = '" + ID + "' ";
            DataTableReader dtr = DataBase.dbDataTable(auditSQL).CreateDataReader();
            while (dtr.Read())
            {
                NameofFieldChanged = dtr["FieldName"].ToString();
                BeforeValue = dtr["OldValue"].ToString();
                AfterValue = dtr["NewValue"].ToString();
                UpdateDate = dtr["UpdateDate"].ToString();
                auditInsertSQL += "INSERT INTO [dbo].[PasswordAssignmentsChangeHistory]([EPasswordAssignmentsID],[NameofFieldChanged],[BeforeValue],[AfterValue],[ChangemadebyPIN],[ChangeDateTime]) ";
                auditInsertSQL += " VALUES ('" + ID + "' , '" + NameofFieldChanged + "' ,@BeforeValue ,@AfterValue  ,'" + loginSSA.myPIN + "'   , '" + UpdateDate + "') ;";
                auditInsertSQL += "Delete from Audit where TableName = 'PasswordAssignments' and PrimaryKeyValue = '" + ID + "' ";

                SqlCommand cmd = new SqlCommand(auditInsertSQL);
                cmd.Parameters.AddWithValue("@BeforeValue", BeforeValue);
                cmd.Parameters.AddWithValue("@AfterValue", AfterValue);
                DataBase.executeCommandWithParameters(cmd);
            }
           
           // DataBase.executeCommand(auditInsertSQL);
        }
    }
}