<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Templates_Content" %>
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
 <div class="grid">
  <p>
  <INPUT   type="submit"
value="Submit">
<asp:Label id="Label6"   runat="server">Example: 000020,
000021, 000022</asp:Label>
<asp:Label id="Label4"  runat="server">Example:
Adrian.Weiss@ssa.gov </asp:Label>
<asp:Label id="Label3"  runat="server">Valid Values:
GETEMAIL, GETPIN, VERIFYEMAIL</asp:Label>
<asp:Label id="Label2"   runat="server">PIN</asp:Label>
<asp:TextBox id="PIN"   tabIndex="1"
runat="server" Width="120px" MaxLength="6"></asp:TextBox>
<asp:Label id="Label1"  runat="server">EMAIL</asp:Label>
<asp:TextBox id="EMAIL"   tabIndex="1"
runat="server" Width="120px" MaxLength="40"></asp:TextBox>
<asp:Label id="Label5"  
runat="server">REQTYPE</asp:Label>
<asp:TextBox id="REQTYPE"   runat="server"
MaxLength="15" Width="120px" tabIndex="1"></asp:TextBox>
<asp:Label id="Label7"   ></asp:Label>
 </p>
 </div>
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
