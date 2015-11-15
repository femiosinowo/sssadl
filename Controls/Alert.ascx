<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Alert.ascx.cs" Inherits="Controls_Alert" %>

 
<span runat="server" id="alertPanel" visible="false" >
<div id="alertPaneldiv" class="advisory-content container-yellow">
<div class="content-wrapper no-padding">

<div class="advisory-icon">
<img src="/framework/images/important-icon.png" alt="Advisory Icon">
</div>

<div class="advisory-message">
<h3><%=AlertTitle %></h3>
<p><%=AlertDescription%></p>
 
 <asp:LinkButton Visible="false" runat="server" ID="Oklink" CssClass="hide-alert-message" 
        Text="Okay, I've got it. Hide this alert, please." onclick="Oklink_Click" ></asp:LinkButton>
 <a href="#" onclick="javascript:hideAlert();" class="hide-alert-message" >Okay, I've got it.&nbsp; Hide this alert, please. </a>
 
</div>

</div>
</div>
</span>
