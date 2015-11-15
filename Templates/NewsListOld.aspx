<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="NewsListOld.aspx.cs" Inherits="Templates_NewsList" %>
<%@ Register Src="~/Controls/RightColumnContent.ascx" TagPrefix="ux" TagName="RightSideContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" Runat="Server">
    <h3>News Archives for <asp:Label ID="lblYearSelected" runat="server"></asp:Label></h3>
    <p>
        <label for="news_archive_year" class="inline-label">View Archives for:</label>
        <asp:DropDownList ID="ddlArchiveYear" runat="server">            
        </asp:DropDownList>
        <asp:Button ID="btnSubmit" CssClass="btn" runat="server" Text="Go" OnClick="btnSubmit_Click" /> 
    </p>
    <asp:ListView ID="lvNewsList" runat="server">
        <LayoutTemplate>
            <ul class="divider no-margin">
                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
            <li>
                <div class="byline"><%# DataBinder.Eval(Container.DataItem, "SmartForm.Date", "{0:MMM dd, yyyy}") %></div>
                <a class="post" href="<%# Eval("Content.Quicklink") %>" title="<%# Eval("SmartForm.Headline") %>" aria-label="<%# Eval("SmartForm.Headline") %>"><%# Eval("SmartForm.Headline") %></a>
            </li>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" Runat="Server">
    <!--Home page news resource spot light -->
    <div class="pad-left resource_spotlight">
        <h3>Resource Spotlight</h3>
        <svg id="spotlight">
            <use xlink:href="#icon-tv">
            </use></svg>
        <asp:Literal ID="ltrNewsSpotlight" runat="server"></asp:Literal>
    </div>
    <ux:RightSideContent ID="rightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" Runat="Server">
</asp:Content>

