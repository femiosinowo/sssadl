<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterNav.ascx.cs" Inherits="UserControls_FooterNav" %>

<footer class="footer" id="footer" role="contentinfo">
    <div class="grid">
        <div class="show-phone align-center">
            <p><a href="#banner" class="btn-top" title="Back to Top">Back to Top</a></p>
            <p>
                <a href="/agency/privacy.html" title="Privacy">Privacy</a> &#183;			
                            <a href="/agency/websitepolicies.html" title="Website Policies">Website Policies</a>
            </p>
            <p>
                <a class="language" href="/espanol/" title="Espa&#241;ol">Espa&#241;ol</a> &#183;			
                            <a class="icon icon-globe" href="/multilanguage/" title="Other Languages">
                                <svg>
                                    <use xlink:href="#icon-globe" />
                                </svg>
                                Other Languages</a>
            </p>
        </div>
        <div class="row-12 hide-phone">
            <div class="column-3"></div>
            <div class="column-7 hide-phone hide-print">                
                <div class="footer-links">
                    <asp:ListView ID="lvFooterMenu" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <a target="_blank" href="<%# Eval("Href") %>" title="<%# Eval("Text") %>"><%# Eval("Text") %></a>
                        </ItemTemplate>
                        <ItemSeparatorTemplate>
                             &nbsp;·&nbsp;
                        </ItemSeparatorTemplate>
                    </asp:ListView>
                </div>
            </div>
            <div class="column-4"></div>
        </div>
    </div>
</footer>
