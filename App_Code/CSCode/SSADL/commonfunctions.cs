using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms;
using System.Xml;
using Ektron.Cms.Framework;
using Ektron.Cms.Framework.Content;
using Microsoft.VisualBasic;
using Ektron.Cms.UI.CommonUI;
using System.Data.SqlClient;
using System.Data;
using Ektron.Cms.API.Content;
using Ektron.Cms.Common;
using Ektron.Cms.Framework.Organization;
using System.Configuration;
using Ektron.Cms.Framework.Settings.UrlAliasing;
using System.Security.Cryptography;
using System.Text;
using Ektron.Cms.Framework.User;
using Ektron.Cms.Organization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Ektron.Cms.Content;
using System.Collections;
using System.Collections;
using System.Net.Mail;
using System.Web.Mail;
using System.Net.Mime;

namespace SSADL.CMS
{
    /// <summary>
    /// Summary description for commonfunctions
    /// </summary>
    public class commonfunctions
    {
        public static string ektronConnectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["Ektron.DbConnection"].ConnectionString.ToString();
        public static string adminConnectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["Admin.DbConnection"].ConnectionString.ToString();
        public static string contentDMServer = System.Configuration.ConfigurationManager.AppSettings["ContentDMServer"].ToString();
        public static string contentDMServerUtil = System.Configuration.ConfigurationManager.AppSettings["ContentDMServerUtils"].ToString();
        public static string Environment = System.Configuration.ConfigurationManager.AppSettings["Environment"].ToString();
        public static string devEnvironmentEmails = System.Configuration.ConfigurationManager.AppSettings["devEnvironmentEmails"].ToString();
        public static string SendEmailsToRealPeople = System.Configuration.ConfigurationManager.AppSettings["SendEmailsToRealPeople"].ToString();
        public static string mailfromAddress = System.Configuration.ConfigurationManager.AppSettings["mailfromAddress"].ToString();
        public static string mailBccEmail = System.Configuration.ConfigurationManager.AppSettings["mailBccEmail"].ToString();
         
        public static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public commonfunctions()
        {
            //
            // TODO: Add constructor logic here
            //

        }



        public static string getFieldValue(XmlDocument xmlDoc, string FieldName, string NodeName = "/root")
        {
            try
            {
                XmlNodeList xList = xmlDoc.SelectNodes(NodeName);
                string FieldValue = xList[0][FieldName].InnerXml;
                return FieldValue;
            }
            catch
            {
                return "";
            }
        }

        public static string getFieldAttributeValue(XmlDocument xmlDoc, string FieldName, string tagName, string attributeName, string NodeName = "/root")
        {
            try
            {
                XmlNodeList xList = xmlDoc.SelectNodes(NodeName);
                string FieldValue = xList[0][FieldName].SelectSingleNode(tagName).Attributes[attributeName].Value;

                return FieldValue;
            }
            catch
            {
                return "";
            }
        }

        public static XmlDocument getContentXML(long contentID)
        {

            Ektron.Cms.Framework.Core.Content.Content contentManager1 = new Ektron.Cms.Framework.Core.Content.Content(ApiAccessMode.LoggedInUser);
            ContentData contentData = contentManager1.GetItem(contentID);

            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.LoadXml(contentData.Html);

            return XMLDoc;

        }



        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true; // addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected string bracketizeMe(string ids)
        {
            int p = 0;
            string results = "(";
            string[] taxIds = ids.Split(new string[] { ";" }, StringSplitOptions.None);
            foreach (string taxId in taxIds)
            {
                results += taxId;
                if (p > 0)
                {

                    results += taxId + ",";
                }

                p++;
            }
            results += ")";
            return results;
        }

        public static String host = HttpContext.Current.Request.Url.Scheme + Uri.SchemeDelimiter + HttpContext.Current.Request.Url.Host;


        /// <summary>
        /// Get the  menu assigned to the folder the current item is in.
        /// If no such menu exists,  return 0.
        /// </summary>
        public static long getAssignedMenuId(long folderId)
        {
            ApplicationAPI AppUI = new ApplicationAPI();
            AppUI.RequestInformationRef.CallerId = (int)Ektron.Cms.Common.EkEnumeration.UserGroups.InternalAdmin;
            Microsoft.VisualBasic.Collection gtNavs = new Microsoft.VisualBasic.Collection();


            gtNavs = AppUI.EkContentRef.GetAllMenusInfo(folderId, "title");
            string mainMenuId = string.Empty;
            if (gtNavs.Count > 0)
            {
                foreach (Microsoft.VisualBasic.Collection gtNav in gtNavs)
                {
                    // if (gtNav["MenuTitle"].ToString() == menuTitle)
                    // {
                    mainMenuId = mainMenuId + gtNav["MenuID"];
                    return long.Parse(mainMenuId);
                    //  }
                }
            }
            return 0;
        }


        /// <summary>
        /// Get the  menu collection to the folder the current item is in.
        /// If no such menu exists,  return 0.
        /// </summary>
        public static long getAssignedCollectionId(long folderId)
        {
            ApplicationAPI AppUI = new ApplicationAPI();
            AppUI.RequestInformationRef.CallerId = (int)Ektron.Cms.Common.EkEnumeration.UserGroups.InternalAdmin;
            Microsoft.VisualBasic.Collection gtNavs = new Microsoft.VisualBasic.Collection();

            gtNavs = AppUI.EkContentRef.GetAllCollectionsInfo(folderId, "title");
            string mainCollectionId = string.Empty;
            if (gtNavs.Count > 0)
            {
                foreach (Microsoft.VisualBasic.Collection gtNav in gtNavs)
                {
                    // if (gtNav["MenuTitle"].ToString() == menuTitle)
                    // {
                    mainCollectionId = mainCollectionId + gtNav["CollectionID"];
                    return long.Parse(mainCollectionId);
                    //  }
                }
            }
            return 0;
        }

        /// <summary>
        /// Get the  both collection and menu collection to the folder the current item is in.
        /// If no such menu exists,  return 0.
        /// </summary>
        public static Dictionary<string, long> getAssignedMenuNCollectionId(long folderId)
        {
            Dictionary<string, long> menuCollection = new Dictionary<string, long>();

            ApplicationAPI AppUI = new ApplicationAPI();
            AppUI.RequestInformationRef.CallerId = (int)Ektron.Cms.Common.EkEnumeration.UserGroups.InternalAdmin;
            Microsoft.VisualBasic.Collection gtNavs = new Microsoft.VisualBasic.Collection();
            gtNavs = AppUI.EkContentRef.GetAllCollectionsInfo(folderId, "title");
            string mainCollectionId = string.Empty;
            if (gtNavs.Count > 0)
            {
                // gtNavs["CollectionID"].ToString();
                foreach (Microsoft.VisualBasic.Collection gtNav in gtNavs)
                {
                    // if (gtNav["MenuTitle"].ToString() == menuTitle)
                    // {
                    mainCollectionId = mainCollectionId + gtNav["CollectionID"];
                    // return long.Parse(mainCollectionId);
                    //  }
                    try
                    {
                        menuCollection.Add("CollectionID", long.Parse(mainCollectionId));
                    }
                    catch { }
                }
            }



            return menuCollection;
        }


        /// <summary>
        /// Get the  collection ids
        /// </summary>  
        /// 
        public static Dictionary<long, string> getCollectionContentIds(long collectionID)
        {
            Dictionary<long, string> contenIdTitles = new Dictionary<long, string>();
            var contentManager2 = new Ektron.Cms.Framework.Content.ContentManager();
            var criteria2 = new ContentCollectionCriteria();
            criteria2.AddFilter(collectionID);
            criteria2.OrderByCollectionOrder = true;
            var collectionList = contentManager2.GetList(criteria2);

            for (int i = 0; i < collectionList.Count; i++)
            {
                contenIdTitles.Add(collectionList[i].Id, collectionList[i].Title);
            }
            return contenIdTitles;
        }

        public static string getQuickLink(long contentID)
        {
            var _refUrlCommonApi = new Ektron.Cms.UrlAliasing.UrlAliasCommonApi();

            string quickLink = _refUrlCommonApi.GetAliasForContent(contentID);
            if (string.IsNullOrEmpty(quickLink))
            {
                var m_refContentApi = new Ektron.Cms.ContentAPI();
                var templateData = m_refContentApi.GetTemplatesByFolderId(m_refContentApi.GetJustFolderIdByContentId(contentID));
                if (templateData.FileName.IndexOf('?') >= 0)
                    quickLink = string.Format("{0}{1}&id={2}", m_refContentApi.SitePath, templateData.FileName, contentID);
                else
                    quickLink = string.Format("{0}{1}?id={2}", m_refContentApi.SitePath, templateData.FileName, contentID);
            }
            if (string.IsNullOrEmpty(quickLink))
                quickLink = string.Format("/Workarea/linkit.aspx?id={0}", contentID.ToString());
            if (quickLink[0] != '/') quickLink = "/" + quickLink;

            return quickLink;
        }
        public static string getAlias(long contentId, bool autoAlias = false)
        {
            Ektron.Cms.API.UrlAliasing.UrlAliasCommon urlCommon = new Ektron.Cms.API.UrlAliasing.UrlAliasCommon();

            Ektron.Cms.API.UrlAliasing.UrlAliasAuto urlAuto = new Ektron.Cms.API.UrlAliasing.UrlAliasAuto();
            String urlAlias = string.Empty;
            //Get Manual Alias for Content
            if (autoAlias)
            {

                AutoAliasManager autoAliasManager = new AutoAliasManager();
                UrlAliasAutoData urlAliasAutoData = autoAliasManager.GetItem(contentId);

                if (urlAliasAutoData != null)
                {
                }

            }
            else
            {
                try
                {
                    //Get Auto Alais for Content
                    ManualAliasManager manualAliasManager = new ManualAliasManager();
                    UrlAliasManualData urlAliasManualData = manualAliasManager.GetDefaultAlias(contentId);
                    if (urlAliasManualData != null)
                    {
                        urlAlias = urlAliasManualData.DisplayAlias;
                    }
                    // urlAlias = urlManual.GetDefaultAlias(contentId).AliasPageName;

                }
                catch
                {
                    return getQuickLink(contentId);
                }
            }

            //Get Any Alias for Content
            if (string.IsNullOrEmpty(urlAlias))
            {

                urlAlias = urlCommon.GetAliasForContent(contentId);

            }



            return urlAlias;

        }



        public static long getFolderID(long contentID)
        {
            if (contentID == 0) return 0;
            Ektron.Cms.API.Content.Content cAPI = new Ektron.Cms.API.Content.Content();
            Ektron.Cms.ContentData item;
            item = cAPI.GetContent(contentID, Ektron.Cms.Content.EkContent.ContentResultType.Published);

            return item.FolderId;
        }


        ///<summary> 
        /// This class returns taxonomy childeren
        /// 
        /// Fields: TaxID , TaxName , TaxDescription , TaxCategoryURL
        ///  
        /// </summary> 

        public static DataTable getTaxonomyTree(long ParentTaxID)
        {
            // Dictionary<long, string> TaxonomyList = new Dictionary<long, string>();

            DataTable taxDataTable = new DataTable();
            taxDataTable.Columns.Add("TaxID", typeof(long));
            taxDataTable.Columns.Add("TaxName", typeof(string));
            taxDataTable.Columns.Add("TaxDescription", typeof(string));
            taxDataTable.Columns.Add("TaxCategoryURL", typeof(string));

            taxData = createTaxonomyTree(ParentTaxID);

            if (taxData.Taxonomy != null)
            {
                try
                {
                    for (int i = 0; i < taxData.Taxonomy.Length; i++)
                    {

                        taxDataTable.Rows.Add(taxData.Taxonomy[i].TaxonomyId, taxData.Taxonomy[i].Name, taxData.Taxonomy[i].Description, taxData.Taxonomy[i].CategoryUrl);


                    }
                }
                catch { }

            }

            return taxDataTable;


        }
        public static Ektron.Cms.TaxonomyData taxData = new Ektron.Cms.TaxonomyData();

        public static TaxonomyData createTaxonomyTree(long TaxonomyId)
        {
            TaxonomyRequest taxonomyRequest = new TaxonomyRequest();
            Ektron.Cms.API.Content.Taxonomy tax1 = new Taxonomy();
            //  Ektron.Cms.TaxonomyData taxData = new TaxonomyData();
            try
            {
                taxonomyRequest.TaxonomyId = TaxonomyId;
                //   taxonomyRequest.Page = Page;
                taxonomyRequest.TaxonomyLanguage = 1033;
                // taxonomyRequest.PageSize = contentApi.RequestInformationRef.PagingSize;
                taxonomyRequest.Depth = -1;
                taxonomyRequest.ReadCount = true;
                taxonomyRequest.TaxonomyType = 0; //0 = content; 1 = user; 2 = group;
                taxonomyRequest.IncludeItems = false;
                //    taxonomyRequest.SortOrder = "last_edit_date";
                //   taxonomyRequest.SortDirection = "desc";
                taxData = tax1.LoadTaxonomy(ref taxonomyRequest);
                TaxonomyBaseData[] taxonomyDataArray = new CommonApi().EkContentRef.ReadAllSubCategories(taxonomyRequest);

            }
            catch (Exception)
            {
            }
            return taxData;
        }


        public static Ektron.Cms.Framework.Core.CustomProperty.CustomPropertyObject customObject = new Ektron.Cms.Framework.Core.CustomProperty.CustomPropertyObject();

        public static string GetCustomPropertyValue(long taxid, long properytype)
        {
            CustomPropertyObjectData customData = customObject.GetItem(taxid, 1033, EkEnumeration.CustomPropertyObjectType.TaxonomyNode, properytype);

            if (customData.Items.Count > 0)
            {
                return customData.Items[0].PropertyValue.ToString();
            }
            else
            {
                return GetTaxonomyNameFromID(taxid); // string.Empty;
            }



        }



        public static string GetTaxonomyNameFromID(long taxID)
        {
            //CustomPropertyObjectData customData = customObject.GetItem(taxID, 1033, EkEnumeration.CustomPropertyObjectType.TaxonomyNode, 1);
            //if (customData.Items.Count > 0)
            //{
            //    return customData.Items[0].PropertyValue.ToString();
            //}

            //else
            //{
            string taxName = string.Empty;
            TaxonomyManager taxonomyManager = new TaxonomyManager();
            TaxonomyData taxonomyData = taxonomyManager.GetItem(taxID);
            if (taxonomyData != null)
            {
                taxName = taxonomyData.Name;
            }
            return taxName;
            // }
        }
        public static string GetAssignedTaxonomyList(long contentId)
        {
            string AssignedTaxonomyList = "";
            Taxonomy taxonomyApi = new Taxonomy();
            TaxonomyBaseData[] taxBaseData = taxonomyApi.ReadAllAssignedCategory(contentId);


            if (taxBaseData.Length > 0)
            {
                int p = 0;
                foreach (TaxonomyBaseData txbd in taxBaseData)
                {
                    if (p >= 1)
                    {
                        AssignedTaxonomyList += ",";
                    }

                    AssignedTaxonomyList += txbd.Id.ToString();
                    p++;

                }
                //long taxonomyid = taxBaseData[0].TaxonomyId;
                return AssignedTaxonomyList;
            }
            else
            {
                return "";
            }
        }


        public static ArrayList GetAssignedTaxonomyArray(long contentId)
        {
            ArrayList AssignedTaxonomyList = new ArrayList();

            Taxonomy taxonomyApi = new Taxonomy();
            TaxonomyBaseData[] taxBaseData = taxonomyApi.ReadAllAssignedCategory(contentId);


            if (taxBaseData.Length > 0)
            {

                foreach (TaxonomyBaseData txbd in taxBaseData)
                {

                    AssignedTaxonomyList.Add(txbd.Id.ToString());

                }
                //long taxonomyid = taxBaseData[0].TaxonomyId;

            }
            return AssignedTaxonomyList;
        }



        public static DataTable sortDataTable(DataTable dt, string columnToSort, string direction)
        {

            DataView dv = dt.DefaultView;

            dv.Sort = columnToSort + " " + direction;
            return dv.ToTable();

        }






        protected DataTable AllmyFavoritesDT()
        {

            string sqlCount = "Select * from MyResources where PINNumber = '" + loginSSA.myPIN + "'";

            return DataBase.dbDataTable(sqlCount);

        }


        public static DataTable AllMyFavsDTP()
        {
            commonfunctions cf = new commonfunctions();
            return cf.AllmyFavoritesDT();

        }
        public static string getMyFavIcons(string contentId, string ResourceContentType, string title, string collection = "0", string Url = "", string Date = "", string Author = "")
        {
            commonfunctions cf = new commonfunctions();
            DataTable AllMyFavsDT = cf.AllmyFavoritesDT();


            string ouptput = "";
            string notFav = string.Empty;
            string myFav = string.Empty;
            title = new string(title.Where(c => !char.IsPunctuation(c)).ToArray()); // ; title.Replace("'", "").Replace(".", "");
            DataRow[] foundRows;

            // Use the Select method to find all rows matching the filter.
            // foundRows = resourcesDt.Select(filter);
            string filter = " FavoriteURL='" + contentId + "' and ResourceContentType = '" + ResourceContentType + "' ";
            foundRows = AllMyFavsDT.Select(filter);
            string coo = AllMyFavsDT.Rows.Count.ToString();
            if (foundRows.Length > 0)
            {

                notFav = " style=\"display:none\"";

            }
            else
            {

                myFav = " style=\"display:none\"";
            }

            string ResourceContentTypeU = "\"" + ResourceContentType + "\"";
            collection = "\"" + collection + "\"";
            ouptput += " <span id='removeFavDiv" + contentId + "'  " + myFav + "   > ";
            string acontentId = "\"" + contentId + "\""; // @""" + contentId + """; 
            ouptput += @" <a style='cursor:pointer'  title='Remove " + title + " from My Resources' href='#'  onclick='javascript:deleteFavorite(" + acontentId + "," + ResourceContentTypeU + "," + collection + ");return false;' onkeypress='javascript:deleteFavoriteKP(event," + acontentId + "," + ResourceContentTypeU + "," + collection + ");return false;' class='favorite favorite_on'   > Remove  ";
            ouptput += "<span class='favorite-id-title'>" + title + "</span> to My Resources</a></span>";

            string Url1 = "\"" + Url + "\"";
            string Date1 = "\"" + Date + "\"";
            string Author1 = "\"" + Author + "\"";
            string title1 = "\"" + title + "\"";

            ouptput += "<span id='addFavDiv" + contentId + "'    " + notFav + " >";
            ouptput += @" <a style='cursor:pointer'  href='#' onclick='javascript:addToFavorite(" + acontentId + "," + ResourceContentTypeU + "," + collection + " , " + title1 + " , " + Url1 + " , " + Date1 + " , " + Author1 + ");return false;' onkeypress='javascript:addToFavoriteKP(event," + acontentId + "," + ResourceContentTypeU + "," + collection + " , " + title1 + " , " + Url1 + " , " + Date1 + " , " + Author1 + ");return false;' class='favorite' title='Add " + title + " to My Resources'> Add ";
            ouptput += " <span class='favorite-id-title'>" + title + "</span> to My Resources</a>";
            ouptput += "  </span>";

            return ouptput;
        }

        public static void sendEmailMessageWithAttachment(string mailTo, string mailFrom, string mailSubject, string mailBody, System.Web.UI.WebControls.FileUpload FileUpload1)
        {
            try
            {

                // string SendEmailsToRealPeople = System.Configuration.ConfigurationManager.AppSettings["SendEmailsToRealPeople"].ToString();
                if (SendEmailsToRealPeople == "false")
                {
                    mailTo = devEnvironmentEmails; // "DCBFQM.OMM.975-S@ssa.gov,ssasdl@figleaf.com";
                }

                if (mailFrom == "") mailFrom = mailfromAddress;
                string mailServerRelay = System.Configuration.ConfigurationManager.AppSettings["ek_SMTPServer"].ToString(); ;
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(mailTo);
                message.Bcc.Add(mailBccEmail);
                message.Subject = mailSubject;
                message.From = new System.Net.Mail.MailAddress(mailFrom);
                message.IsBodyHtml = true;
                message.Body = mailBody;



                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                if (fileName != "")
                {
                    Attachment myAttachment =
                                   new Attachment(FileUpload1.FileContent, fileName);
                    message.Attachments.Add(myAttachment);
                }


                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServerRelay);
                smtp.Send(message);
            }
            catch { }



        }

        public static void sendEmailMessage(string mailTo, string mailFrom, string mailSubject, string mailBody)
        {
            try
            {
                if (SendEmailsToRealPeople == "false")
                {
                    mailTo = devEnvironmentEmails; // "DCBFQM.OMM.975-S@ssa.gov,ssasdl@figleaf.com";
                }

                if (mailFrom == "") mailFrom = mailfromAddress;
                string mailServerRelay = System.Configuration.ConfigurationManager.AppSettings["ek_SMTPServer"].ToString();
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(mailTo);
                message.Bcc.Add(mailBccEmail);
                message.Subject = mailSubject;
                message.From = new System.Net.Mail.MailAddress(mailFrom);
                message.IsBodyHtml = true;
                message.Body = mailBody;



                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(mailServerRelay);
                smtp.Send(message);
            }
            catch { }



        }



        public static string getResourceBottomLinks(DataRowView item)
        {
            string outputR = "<br />";

            long rId = long.Parse(item["ID"].ToString().Trim());

            string ShowLogin = item["ShowLogin"].ToString().Trim();
            bool iamDDSEmployee = false;
            if (loginSSA.myDDS == "DDS" || loginSSA.isAdminUser())
            {
                iamDDSEmployee = true;
            }

            
            bool addSlash = false;
            if (ShowLogin == "AllEmployees" || (ShowLogin == "OnlyDDSEmployees" && iamDDSEmployee))
            {
                //  sharedPasswordPanel.Visible = true;
                addSlash = true;
                outputR += " <a href='#' class='fixed_inline login_text'>Login Information</a>";
                outputR += "  <span class='container-blue login_info'>user ID - " + item["SharedUsername"].ToString().Trim() + "    / password - " + item["SharedPassword"].ToString().Trim() + "</span>";
            }

            if (item["AccessTypeTaxonomy"].ToString().Trim() == "122")
            {

                //if (item["LimitedNumberPasswordsAvailable"].ToString().Trim() == "N") ///////////// Request Access
                // {
                if (addSlash) outputR += " | ";

                outputR += "<a href='" + commonfunctions.getQuickLink(108) + "?rid=" + rId.ToString() + "'>Password Assistance</a>";
                outputR += "  |   <a href='" + commonfunctions.getQuickLink(106) + "?rid=" + rId.ToString() + "'>Request Access</a>";


                // }
            }


            return outputR;
        }


        private static bool deterMineShowLogin(string ShowLogin)
        {
            //check for DDS employees - if you are DDS and also OnlyDDSEmployees is chosen - then only show resource password to DDS Employees
            bool iamDDSEmployee = false;
            if (loginSSA.myDDS == "DDS" || loginSSA.isAdminUser())
            {
                iamDDSEmployee = true;
            }
            if (ShowLogin == "AllEmployees")
            {
                return true;
            }
            else if (ShowLogin == "OnlyDDSEmployees" && iamDDSEmployee)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// Methods to remove HTML from strings.
        /// </summary>

        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }


        public static string getResourceLink(string resourceID)
        {
            return "/dynamicdb.aspx?rid=" + resourceID;
        }

        public static string getDatabaseDateformat(string datetimeString)
        {

            try
            {
                DateTime dt = new DateTime();
                dt = DateTime.Parse(datetimeString);
                return dt.ToShortDateString();
            }
            catch
            {

                return "";
            }
        }
    }

}