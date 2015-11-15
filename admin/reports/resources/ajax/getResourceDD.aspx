<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getResourceDD.aspx.cs" Inherits="admin_reports_resources_ajax_getDD" %>

 
 
     
 

       <select id="ListResourcesDD" multiple="true" name="ListResourcesDD" class="validate[required]" ToolTip="Select One or more Resource(s)" >
       <%=listOptions %>
       </select><br />
     <a href="javascript:selectAll('ListResourcesDD')"   >Select All</a> |  <a href="javascript:selectNone('ListResourcesDD')">Select None</a>
    
 