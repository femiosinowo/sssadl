<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeleteRightColumnContent.ascx.cs" 
    Inherits="Controls_RightColumnContent" %>

<asp:Panel ID="pnlRelatedNews" runat="server" Visible="false">
    <div class="pad-left">
        <h3>Related Information</h3>
        <asp:ListView ID="lvRelatedInfo" runat="server">
            <LayoutTemplate>
                <ul class="divider">
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </ul>                
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <a aria-label="<%# Eval("SmartForm.Headline") %>" title="<%# Eval("SmartForm.Headline") %>" href="<%# Eval("Content.Quicklink") %>"><%# Eval("SmartForm.Headline") %></a>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Panel>
<asp:Panel ID="pnlRelatedFAQ" runat="server" Visible="false">
    <div class="pad-left">
        <h3>Related FAQs</h3>
        <asp:ListView ID="lvRelatedFAQs" runat="server">
        <LayoutTemplate>
                <ul class="divider">
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </ul>                
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <a aria-label="<%# Eval("SmartForm.Question") %>" title="<%# Eval("SmartForm.Question") %>" href="<%# Eval("Content.Quicklink") %>"><%# Eval("SmartForm.Question") %></a>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Panel>
<asp:Panel ID="pnlTopFAQ" runat="server" Visible="false">
    <div class="pad-left">
        <h3>FAQs</h3>
        <asp:ListView ID="lvTopFAQs" runat="server">
        <LayoutTemplate>
                <ul class="divider">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </ul>                
            </LayoutTemplate>
            <ItemTemplate>
                <li>
                    <a aria-label="<%# Eval("SmartForm.Headline") %>" title="<%# Eval("SmartForm.Headline") %>" href="<%# Eval("Content.Quicklink") %>"><%# Eval("SmartForm.Headline") %></a>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Panel>
<asp:Panel ID="pnlGenericContent" runat="server" Visible="false">
    <div class="pad-left">
        <h3><asp:Label ID="lblGenericTitle" runat="server"></asp:Label></h3>
        <p><asp:Literal ID="ltrGenericTxtBody" runat="server"></asp:Literal></p>
    </div>
</asp:Panel>
<asp:Panel ID="pnlNeedHelp" runat="server" Visible="false">
    <div class="pad-left">
        <h3><asp:Label ID="lblNeedHelpTitle" runat="server"></asp:Label></h3>
        <p><asp:Literal ID="ltrNeedHelpTxtBody" runat="server"></asp:Literal></p>
    </div>
</asp:Panel>