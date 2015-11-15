using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSADL.CMS;
using Ektron.Cms;
using Ektron.Cms.Instrumentation;

public partial class Controls_RightColumnContent : System.Web.UI.UserControl
{
    /// <summary>
    /// Page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            if (Request.QueryString.HasKeys())
            {
                long contentId;
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    contentId = EktUtility.ParseLong(Request.QueryString["id"]);
                    var contentData = ContentHelper.GetContentById(contentId, true);
                    if (contentData != null && contentData.MetaData != null)
                    {
                        this.RelatedNewsItems(contentData);
                        this.RelatedFAQItems(contentData);
                        this.TopFAQItems(contentData);
                        this.GenericContent(contentData);
                        this.NeedHelp(contentData);
                    }
                }
            }
        }
    }

    /// <summary>
    /// This method is used to dislay selected news items as metadata
    /// </summary>
    /// <param name="cData"></param>
    private void RelatedNewsItems(ContentData cData)
    {
        try
        {
            long relatedNewsMetaId = ConfigHelper.GetValueLong("MetaRelatedInformationId");
            var relatedNewsMeta = cData.MetaData.Where(x => x.Id == relatedNewsMetaId).FirstOrDefault();
            if(relatedNewsMeta != null && relatedNewsMeta.Text != string.Empty)
            {
                var ids = relatedNewsMeta.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if(ids != null && ids.Length > 0)
                {
                    List<long> contentIds = new List<long>();
                    foreach (var id in ids)
                        contentIds.Add(EktUtility.ParseLong(id));

                    var newsContent = SiteDataManager.GetNewsbyIds(contentIds);
                    if(newsContent != null && newsContent.Any())
                    {
                        pnlRelatedNews.Visible = true;
                        lvRelatedInfo.DataSource = newsContent;
                        lvRelatedInfo.DataBind();
                    }
                }
            }            
        }
        catch(Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.StackTrace);
        }
    }

    /// <summary>
    /// This method is used to dislay selected FAQs items as metadata
    /// </summary>
    /// <param name="cData"></param>
    private void RelatedFAQItems(ContentData cData)
    {
        try
        {
            long relatedFAQsMetaId = ConfigHelper.GetValueLong("MetaFAQSelectorId");
            var relatedFAQsMeta = cData.MetaData.Where(x => x.Id == relatedFAQsMetaId).FirstOrDefault();
            if (relatedFAQsMeta != null && relatedFAQsMeta.Text != string.Empty)
            {
                var ids = relatedFAQsMeta.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (ids != null && ids.Length > 0)
                {
                    List<long> contentIds = new List<long>();
                    foreach (var id in ids)
                        contentIds.Add(EktUtility.ParseLong(id));

                    var faqsContent = SiteDataManager.GetFAQsByIds(contentIds);
                    if (faqsContent != null && faqsContent.Any())
                    {
                        pnlRelatedFAQ.Visible = true;
                        lvRelatedFAQs.DataSource = faqsContent;
                        lvRelatedFAQs.DataBind();
                    }
                }
            } 
        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.StackTrace);
        }
    }

    /// <summary>
    /// This method is used to dislay selected FAQs driven by collection
    /// </summary>
    /// <param name="cData"></param>
    private void TopFAQItems(ContentData cData)
    {
        try
        {
            long topFAQsMetaId = ConfigHelper.GetValueLong("MetaTopFAQsId");
            var topFAQsMeta = cData.MetaData.Where(x => x.Id == topFAQsMetaId).FirstOrDefault();
            if (topFAQsMeta != null && topFAQsMeta.Text != string.Empty)
            {
                var topFAQCollectionId = EktUtility.ParseLong(topFAQsMeta.Text);
                if (topFAQCollectionId > 0)
                {
                    var faqsContent = SiteDataManager.GetFAQsbyCollectionId(topFAQCollectionId);
                    if (faqsContent != null && faqsContent.Any())
                    {
                        pnlTopFAQ.Visible = true;
                        lvTopFAQs.DataSource = faqsContent;
                        lvTopFAQs.DataBind();
                    }
                }
            } 
        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.StackTrace);
        }
    }

    /// <summary>
    /// This method is used to dislay generic content
    /// </summary>
    /// <param name="cData"></param>
    private void GenericContent(ContentData cData)
    {
        try
        {
            long genericContentMetaId = ConfigHelper.GetValueLong("MetaGenericContentSelectorId");
            var genericContentMeta = cData.MetaData.Where(x => x.Id == genericContentMetaId).FirstOrDefault();
            if (genericContentMeta != null && genericContentMeta.Text != string.Empty)
            {
                var genericContentId = EktUtility.ParseLong(genericContentMeta.Text);
                if (genericContentId > 0)
                {
                    var contentData = SiteDataManager.GetRightColumnContentById(genericContentId);
                    if (contentData != null && contentData.SmartForm != null)
                    {
                        pnlGenericContent.Visible = true;
                        lblGenericTitle.Text = contentData.SmartForm.Title;
                        ltrGenericTxtBody.Text = EktUtility.GetHTMLNodesFromEktRich(contentData.SmartForm.Body);
                    }
                }
            } 
        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.StackTrace);
        }
    }

    /// <summary>
    /// This method is used to dislay Need Help
    /// </summary>
    /// <param name="cData"></param>
    private void NeedHelp(ContentData cData)
    {
        try
        {
            long genericContentMetaId = ConfigHelper.GetValueLong("MetaNeedHelpId");
            var genericContentMeta = cData.MetaData.Where(x => x.Id == genericContentMetaId).FirstOrDefault();
            if (genericContentMeta != null && genericContentMeta.Text != string.Empty)
            {
                var genericContentId = EktUtility.ParseLong(genericContentMeta.Text);
                if (genericContentId > 0)
                {
                    var contentData = SiteDataManager.GetRightColumnContentById(genericContentId);
                    if (contentData != null && contentData.SmartForm != null)
                    {
                        pnlNeedHelp.Visible = true;
                        lblNeedHelpTitle.Text = contentData.SmartForm.Title;
                        ltrNeedHelpTxtBody.Text = EktUtility.GetHTMLNodesFromEktRich(contentData.SmartForm.Body);
                    }
                }
            } 
        }
        catch (Exception ex)
        {
            Log.WriteError(ex.Message + " : " + ex.StackTrace);
        }
    }

}