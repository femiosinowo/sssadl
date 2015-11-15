<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="Templates_NewsDetail" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>

 
 
 <asp:Content ID="Content5" ContentPlaceHolderID="cphSecondaryPageTitle" runat="Server">
 <ux:PageTitle ID="uxPageTitle"  runat="server" />
</asp:Content>


 <asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryBreadcrumb" runat="Server">
<ux:Breadcrumbs ID="uxBreadcrumb" runat="server" />
</asp:Content>


 

<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" runat="Server">
   <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />

    <div class="img-polaroid margin-bottom" runat="server" id="imgDiv" visible="false">
        <!--temporary-->
        <div>
            <asp:Literal ID="ltrNewsImg" runat="server"></asp:Literal>
        </div>
    </div>
    <asp:Literal ID="ltrNewsBody" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>


 
