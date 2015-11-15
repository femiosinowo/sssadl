using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using Ektron.Cms.User;
using Ektron.Cms.BusinessObjects.ContentWorkflow;

namespace Ektron.Cms.Workarea.ContentWorkflow
{

	public class ClientWorkflowUtilities
	{
		public ClientWorkflowUtilities()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public bool IsWorkflowAdmin(long userId)
		{
			EkRequestInformation requestInfo = ObjectFactory.GetRequestInfoProvider().GetRequestInformation();
			EkContent EkContentInstance = new EkContent(requestInfo);
            IUserGroup _usergroupmanager = ObjectFactory.GetUserGroup(requestInfo);
			if (EkContentInstance.IsARoleMember((long)EkEnumeration.CmsRoleIds.ContentWorkflowAdmin, requestInfo.UserId, true)
				|| _usergroupmanager.IsUserInGroup(requestInfo.UserId, 1) || requestInfo.CallerId == EkConstants.InternalAdmin)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool IsCurrentApprover(long userId, long contentId)
		{
			ContentAPI m_refContentApi = new ContentAPI();
			bool isCurrent = false;
			ApprovalData[] approvaldata = null;
			if (approvaldata == null)
			{
				approvaldata = m_refContentApi.GetCurrentApprovalInfoByID(contentId);
			}

			if ((approvaldata != null) && approvaldata.Length > 0)
			{
				bool IsLastApproval = System.Convert.ToBoolean(approvaldata[approvaldata.Length - 1].IsCurrentApprover &&
					(approvaldata[approvaldata.Length - 1].UserId == m_refContentApi.UserId ||
					new UserAPI().IsAGroupMember(m_refContentApi.UserId, approvaldata[approvaldata.Length - 1].GroupId)));
				if (IsLastApproval)
				{
					isCurrent = true;
				}
				else
				{
					for (int i = 0; i <= (approvaldata.Length - 1); i++)
					{
						if (approvaldata[i].IsCurrentApprover)
						{
							isCurrent = System.Convert.ToBoolean(approvaldata[i].UserId == m_refContentApi.UserId || new UserAPI().IsAGroupMember(m_refContentApi.UserId, approvaldata[i].GroupId));
						}
					}
				}
			}
			return isCurrent;
		}

        public static bool IsAdvancedWorkflowActive(long contentId, long folderId, int languageId)
        {
            var contentApi = new ContentAPI();
            var cwfutilties = new ContentWorkflowUtilities(contentApi.RequestInformationRef);
            var workflowdefinitionid = cwfutilties.GetInheritedWorkflowDefinitionId(contentId, folderId, languageId);
            return (workflowdefinitionid > 0);
        }
	}
}