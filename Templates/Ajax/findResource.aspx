<%@ Page Language="C#" AutoEventWireup="true" CodeFile="findResource.aspx.cs" Inherits="Templates_Ajax_clicktracks" %>

<asp:Panel runat="server" Visible="false" ID="noData" >
 <select name="Resource" ektdesignns_minoccurs="1" ektdesignns_validation="select-req"
        onblur="design_validate_select(1, this, 'First item is not a valid selection.')"
        ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="Resource"
        ektdesignns_caption="Resource" ektdesignns_name="Resource" title="Resource" >
        <option selected="selected" value="">- Select a Resource - </option>
        <%=resourceDD%>
    </select>
    </asp:Panel>
    <asp:Panel runat="server" Visible="false" ID="someData" >

     <select name="Resource" ektdesignns_minoccurs="1"  
        ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="Resource"
        ektdesignns_caption="Resource" ektdesignns_name="Resource" title="Resource" >
        <option selected="selected" value="">- Select a Resource - </option>
        <%=resourceDD%>
    </select>
    </asp:Panel>