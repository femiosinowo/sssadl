<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Templates_Default" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainPBPageHost" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" Runat="Server">
 <div id="page" class="home">
 <!-- PAGE TITLE -->
    <div id="title-bar">
        <h2>Homepage</h2>
    </div>
    <div id="homeContent" runat="server" class="home-content" role="main" title="Main Image">
        <!-- GRID -->
        <div class="grid">
            <div class="row-12">
                <div class="column-12 print-full-width" id="home_main_img">
                    <div class="home_search_box">
                        <h2 id="home_search_box_header">Search<span>
                        <asp:HyperLink runat="server" ID="SearchTip" ToolTip="Search Tips" ></asp:HyperLink>
                        
                        
                        </span></h2>
                        <div class="form">
                          <label class="hide" for="home_search" title="Search"  >Search</label>   <input type="text"   placeholder="Articles, books, journals and more" name="home_search" id="home_search" onkeypress="return summonSearch(event);" >
                          <label class="hide" for="homesearchtxtBtn" title="Search"  ></label>  <input id="homesearchtxtBtn" type="button" onclick="summonSearch2(event);" value="Search">
                        </div>
                        <p>
                        <%=navSearchHTML %></p>
                         
                        <cms:FlexMenu runat="server" ID="searchMenu" Visible="false" />
                          <cms:ContentBlock runat="server" ID="mainContent"   />
                    
                         
                    </div>
                    <div class="clear"></div>
                    <div class="resource_spotlight_box">                        
                        <h4>Resource Spotlight</h4>
                        <asp:Literal ID="ltrNewsSpotlight" runat="server"></asp:Literal>
                    </div>
                </div>
                <!-- END COLUMN -->
            </div>
            <!-- END ROW -->
            <div class="row-12">
                 <%=footerNavlinks %>

                <!-- column 3-->
                <div class="column-3">
                    <h3>Library News</h3>
                     <asp:ListView ID="lvLatestNews" runat="server" OnItemDataBound="latestNewsBinding">
                         <LayoutTemplate>
                             <ul  class="divider no-margin">
                                 <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                             </ul>                             
                         </LayoutTemplate>
                         <ItemTemplate>
                             <li>
                                <div class="byline ng-binding"><span><%# DataBinder.Eval(Container.DataItem, "Date", "{0:MMM dd, yyyy}") %></span></div>
                                <div class="ng-binding"><a href="<asp:Literal ID="link" runat="server"></asp:Literal>" title="<%# Eval("Headline") %>" aria-label="<%# Eval("Headline") %>"><%# Eval("Headline") %></a></div>
                             </li>
                         </ItemTemplate>
                     </asp:ListView>      
                    
                           
                </div>
                <!-- column 3-->
            </div>
            <!--end row-->
        </div>
        <!-- END GRID -->
    </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphJSCode" Runat="Server">
</asp:Content>

