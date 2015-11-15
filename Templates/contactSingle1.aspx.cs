using System;
using SSADL.CMS;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Ektron.Cms.Framework.Content;
using System.Collections.Generic;
using System.Xml.Linq;
public partial class Templates_Content : System.Web.UI.Page
{
    /// <summary>
    /// Page Init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {

        
        RightSideContent.ccontentID = mainForm.EkItem.Id.ToString();
        RightSideContent.cfolderID = mainForm.EkItem.FolderId.ToString();
        uxPageTitle.pgTitle = mainForm.EkItem.Title.ToString();
        uxBreadcrumb.contentID = mainForm.EkItem.Id.ToString();
        uxPageTitle.pageId = mainForm.EkItem.Id.ToString();
        uxPageTitle.ResourceTypeId = "2";
        if (!IsPostBack)
        {
            
           // 
            long contentForm = long.Parse(Request.QueryString["id"].ToString());
            mainContent.Text = "<!-- -->";
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.LoadXml(mainContent.EkItem.Html);

            long formID = long.Parse(commonfunctions.getFieldValue(XMLDoc, "ContactForm"));
            TopDescription = commonfunctions.getFieldValue(XMLDoc, "TopDescription");
            contactForm.formID = formID.ToString();
            mainForm.DefaultFormID = formID;

            switch (formID)
            {

                case 112:
                    uploadFile.Visible = true;
                    break;



            }
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
      //  this.Form.Action = "/confirmation.aspx";
    }

    private Dictionary<string, string> GetFormFieldDefaults(long formId)
    {
        var defaults = new Dictionary<string, string>();
        var formApi = new Ektron.Cms.API.Content.Form();
        var contentApi = new Ektron.Cms.API.Content.Content();

        var formFields = formApi.GetFormFieldList(formId);
        var formData = formApi.GetForm(formId); // Can't use FormData; have to use ContentAPI / ContentData to get the HTML
        //if (string.IsNullOrEmpty(formData.Html)) throw new Exception("FormData with empty HTML. Eeek!");

        var contentData = contentApi.GetContent(formId);


        var formXml = string.Concat("<ekForm>", contentData.Html, "</ekForm>");
        var ekForm = XElement.Parse(formXml);
        var inputs = ekForm.Descendants("input");

        //  xml.Text = formFields.Fields;
        foreach (var fieldDefinition in formFields.Fields)
        {
            var name = fieldDefinition.FieldName;


            //var input = inputs.FirstOrDefault(i => i.Attribute("id").Value == name);
            //if (input == null) continue;

            //var defaultValue = input.Attribute("value").Value;
            //defaults.Add(name, defaultValue);
        }
        return defaults;



        //long formID = Convert.ToInt64(105);

        //Ektron.Cms.API.Content.Form fapi = new Ektron.Cms.API.Content.Form();

        //// get the title of the form
        //ContentManager contentManager = new ContentManager();

        //var content = contentManager.GetItem(formID, true);

        //// get the list of form fields
        //var formFieldList = fapi.GetFormFieldList(formID);


        //var formCRUD = new Ektron.Cms.Framework.Content.FormManager();
        //var formSubmittedCriteria = new Ektron.Cms.Common.FormSubmittedCriteria();
        //formSubmittedCriteria.AddFilter(FormSubmittedProperty.FormId, CriteriaFilterOperator.EqualTo, 26424);
        //formSubmittedCriteria.AddFilter(FormSubmittedProperty.DateSubmitted, CriteriaFilterOperator.GreaterThan, DateTime.Now.AddMonths(-1));

        //var formData = formCRUD.GetSubmittedFormList(formSubmittedCriteria);
    }



    public string defaultFormID { get; set; }
    public string selectOutput { get; set; }

    public string TopDescription { get; set; }
}