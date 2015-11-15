<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"
    CodeFile="searchsummondetails.aspx.cs" Inherits="Templates_Content" %>

<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux" TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSecondaryPageTitle" runat="Server">
    <ux:PageTitle ID="uxPageTitle" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryBreadcrumb" runat="Server">
    <ux:Breadcrumbs ID="uxBreadcrumb" runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" runat="Server">
    
  <h3>Search results for "<%=search%>"  in category <%=field %></h3>
    <div class="results_content">
        <p class="float-left">
            Results <asp:Label runat="server" ID="idResultsLabel" ></asp:Label></p>
        <p class="float-right">
           <%--  <label for="show_results" class="inline-label">
                Show:</label>
   
            <asp:DropDownList runat="server" AutoPostBack="true" ID="show_results" OnSelectedIndexChanged="Index_Changed" >
           <asp:ListItem Value="10">10</asp:ListItem>
             <asp:ListItem Value="25">25</asp:ListItem>
              <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="100">100</asp:ListItem>
                
            </asp:DropDownList>--%>
        </p>
    </div>
   
        <asp:ListView ID="listView" OnItemDataBound="listViewDataBound"    runat="server"   >
               
 
               <ItemTemplate>
 
			  <ul class="block-content">
					     <li class="title">
                         
                          <asp:HyperLink  runat="server" CssClass="post"  Target="_blank" ID="ResourceTitle" ></asp:HyperLink>   <asp:Literal runat="server" ID="ResourceYear" > </asp:Literal>
                        <asp:Literal runat="server" ID="myfavicons" ></asp:Literal>
                         </li>
                                      <li class="issue"> <asp:Literal runat="server" ID="Description" > </asp:Literal></li>
						 <li class="author"> <asp:Literal runat="server" ID="ResourceAuthor" > </asp:Literal></li>
						 <li class="issue"> <asp:Literal runat="server" ID="ResourceIssue" > </asp:Literal></li>
						 
					</ul>

               </ItemTemplate>
               </asp:ListView>
    <div class="results_pagination">

         <div class="float-left">
				   <span id="pager_prev" class="pager_buttons">
					 
                      <asp:LinkButton runat="server" ID="PrevBtn" CssClass="btn" Text="Prev" 
                       onclick="PrevBtn_Click" ></asp:LinkButton>
				   </span>
				   
				   <span id="pager_next" class="pager_buttons">
					 
                      <asp:LinkButton runat="server" ID="NextBtn" CssClass="btn" Text="Next" 
                       onclick="NextBtn_Click"  ></asp:LinkButton>
					</span>
		   		</div>
 

                          <div class="float-right" id="pagerDown" runat="server"  >
                            <p>
                                <label for="pager_textbox" class="inline-label">
                                    Go to page</label>
                                <asp:TextBox runat="server" ID="pager_textbox" Width="40"  Visible="false" ></asp:TextBox>
                                  <asp:DropDownList runat="server" ID="pager_dropdown" AutoPostBack="true"  Width="55" OnSelectedIndexChanged="pager_dropdown_SelectedIndexChanged" ></asp:DropDownList>
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                  <asp:LinkButton runat="server" ID="LinkButton1" onclick="goBtnClick" CssClass="btn" Text="Go" Visible="false"  ></asp:LinkButton>
                                
                            </p>
                        </div>
    </div>
    <!--results_pagination-->
    <div class="clearfix">
    </div>
<%--    <div class="container-blue search-message">
        <img src="framework/images/important-icon.png" alt="Important message">
       <cms:ContentBlock runat="server" DefaultContentID="151" />
    </div>--%>
    <!-- END COLUMN --><cms:ContentBlock ID="mainContent" DynamicParameter="id" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
