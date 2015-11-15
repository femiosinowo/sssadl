<%@ Control Language="C#" AutoEventWireup="true" CodeFile="externalLinkDisclaimer.ascx.cs" Inherits="Controls_externalLinkDisclaimer" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
    <aside class="dialog" id="disclaimer-dialog" role="dialog">
<div>
<header><h4 tabindex="-1">External Link Disclaimer</h4></header>
<section>
<div class="bold">
<p>You are exiting the Social Security Administration's website.</p>
<p>Select OK to proceed.</p>
<p class="underline">Disclaimer</p>
</div>
<cms:ContentBlock runat="server" DefaultContentID="152" />   <hr />
<div class="align-right">
<a class="btn" href="#" id="btn-cancel">Cancel</a>
<a class="btn" href="#" id="btn-ok">OK</a> 
</div>
</section>
</div>
</aside>