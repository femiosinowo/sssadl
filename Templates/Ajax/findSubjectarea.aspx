<%@ Page Language="C#" AutoEventWireup="true" CodeFile="findSubjectarea.aspx.cs" Inherits="Templates_Ajax_clicktracks" %>

<asp:Panel runat="server" Visible="false" ID="noData" >
 <select name="SubjectArea" ektdesignns_minoccurs="1" ektdesignns_validation="select-req"
        onblur="design_validate_select(1, this, 'First item is not a valid selection.')"
        ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="SubjectArea"
        ektdesignns_caption="SubjectArea" ektdesignns_name="SubjectArea" title="SubjectArea" >
        <option selected="selected" value="">- Select a SubjectArea - </option>
        <%=SubjectAreaDD%>
    </select>
    </asp:Panel>
    <asp:Panel runat="server" Visible="false" ID="someData" >

     <select name="SubjectArea" ektdesignns_minoccurs="1"  
        ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="SubjectArea"
        ektdesignns_caption="SubjectArea" ektdesignns_name="SubjectArea" title="SubjectArea" >
        <option selected="selected" value="">- Select a SubjectArea - </option>
        <%=SubjectAreaDD%>
    </select>
    </asp:Panel>