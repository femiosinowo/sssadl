<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainNav.ascx.cs" Inherits="UserControls_MainNav" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%-- <CMS:FlexMenu  SuppressAddEdit="true" ID="TopMenu" SuppressWrapperTags="true" runat="server" Visible="false"
                            WrapTag="nav"  EnableSmartOpen="true" AutoCollapseBranches="False"
                            EnableMouseOverPopUp="false"  StartLevel="0" MenuDepth="0"
                            EnableAjax="False" MasterControlId=""   CacheInterval="0" /> --%>

                          <%=navHTML%>
<asp:Literal ID="ltrMenuData" runat="server"  ></asp:Literal>
 
 <a class="btn-top-menu show-phone hide-print" id="btn-top-menu" href="#nav-top-menu" aria-label="Menu">MENU</a>
 