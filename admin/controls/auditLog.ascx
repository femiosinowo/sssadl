<%@ Control Language="C#" AutoEventWireup="true" CodeFile="auditLog.ascx.cs" Inherits="admin_controls_auditLog" %>
 <style>
 .toggle-block > a.on + div {
 
    font-size: small;
}
 </style>

<div class="toggle-block">
    <a href="#">Audit Log</a>
    <div>
     <ul>
    <%=message %>
   </ul>
   </div>
</div><!-- end toggle block -->