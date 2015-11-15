<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Breadcrumbs.ascx.cs" Inherits="Controls_Breadcrumbs" %>
<%@ Register TagPrefix="CMS" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<!-- BREADCRUMBS-->
<div class="ssadl-breadcrumbs margin-bottom">
     <ul>
         <%=bCOutput %>
    </ul> 

   
    
      <CMS:FolderBreadcrumb runat="server" ID="pagebreadCrumb2" Visible="false" AddContentTitleToBreadcrumb="true"
                    DynamicParameter="id" />
</div>
