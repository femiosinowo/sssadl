<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Default1.aspx.cs" Inherits="Templates_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
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
                        <h2 id="home_search_box_header">Search<span><a href="research_guides.html" title="Search Tips">Search Tips</a></span></h2>
                        <div class="form">
                            <input type="text" placeholder="Articles, books, journals and more" name="home_search" aria-label="home_search">
                            <input type="submit" value="Search">
                        </div>
                        <asp:Literal ID="ltrContentBlockText" runat="server"></asp:Literal>
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
                <asp:ListView ID="lvFooterNav" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div class="column-3">
                            <h3>
                                <%# FormatLink(Eval("Href").ToString(), Eval("Text").ToString(), Eval("Description").ToString()) %></h3>
                            <asp:ListView ID="listViewSecondLevel" DataSource='<%# Eval("Items")%>' runat="server">
                                <LayoutTemplate>
                                    <ul class="divider no-margin">
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                    </ul>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li><a href="<%#Eval("Href") %>" title="<%#Eval("Text") %>" aria-label="<%#Eval("Text") %>"><%#Eval("Text") %></a></li>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </ItemTemplate>
                </asp:ListView>

                <!-- column 3-->
                <div class="column-3">
                    <h3>Library News</h3>
                     <asp:ListView ID="lvLatestNews" runat="server">
                         <LayoutTemplate>
                             <ul>
                                 <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                             </ul>                             
                         </LayoutTemplate>
                         <ItemTemplate>
                             <li>
                                <div class="byline ng-binding"><span><%# DataBinder.Eval(Container.DataItem, "SmartForm.Date", "{0:MMM dd, yyyy}") %></span></div>
                                <div class="ng-binding"><a href="<%# Eval("Content.Quicklink") %>" title="<%# Eval("SmartForm.Headline") %>" aria-label="<%# Eval("SmartForm.Headline") %>"><%# Eval("SmartForm.Headline") %></a></div>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphJSCode" runat="Server">
</asp:Content>

