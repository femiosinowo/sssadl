<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="faqs.aspx.cs" Inherits="Templates_Content" %>
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

	   
	  
	  <div class="all_answers">
	  	<a href="#" class="show_all_faq">Show All Answers</a> | <a href="#" class="hide_all_faq">Hide All Answers</a>
	  </div>

       <div class="accordion" style="visibility: visible;">
                                 <asp:ListView ID="level1" runat="server" ItemPlaceholderID="lvPlaceholder" OnItemDataBound="FAQLV_ItemDatabound"   >
            <LayoutTemplate>
                 
                    <asp:PlaceHolder ID="lvPlaceholder" runat="server"></asp:PlaceHolder>
                 
            </LayoutTemplate>
            <ItemTemplate>
                 
                 <asp:Literal runat="server" ID="headerH3"  ></asp:Literal>
                   <%-- <ul>--%>
                        <asp:Repeater ID="level2" runat="server" DataSource='<%#Eval("TaxonomyItems") %>' OnItemDataBound='faqsItem_ItemDatabound'  >
                        <HeaderTemplate>
                     
                        
                        </HeaderTemplate>
                        
                            <ItemTemplate>
                                
                                
                                  

                            
<asp:Literal runat="server" ID="outputText"  ></asp:Literal>


                            </ItemTemplate>
                        </asp:Repeater>
         <%--              </ul>
                </li>
             <ul>
                    <asp:Repeater ID="submenus" runat="server" DataSource='<%#Eval("Taxonomy") %>'>
                        <ItemTemplate>
                            <li>Sub Taxonomy Node<%#Eval("Name") %></li>
                            <ul>
                                <asp:Repeater ID="submenus" runat="server" DataSource='<%#Eval("TaxonomyItems") %>'>
                                    <ItemTemplate>
                                        <li>Sub Node Content Title: <%#Eval("Title") %>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>--%>
            </ItemTemplate>
        </asp:ListView>

 
	  
	  </div>
	  
	  
	  
 
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
