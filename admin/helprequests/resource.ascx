<%@ Control Language="C#" AutoEventWireup="true" CodeFile="resource.ascx.cs" Inherits="admin_helprequests_resource" %>
 <p>
 <script>

     changeInfo(<%=SelectThisResource %>);
function changeInfo(id){
if(id != "undefined"){
    var url = "/admin/controls/ajaxResourceDD.aspx?rid=" + id;
 
    $.get(url, function (data) {
        $("#results").html(data);

    });
    }
}
 
 </script>
          <label class="bold" for="ResourceName"><%=LabelTitle %></label>
       <asp:DropDownList  runat="server" DataTextField="ResourceName" DataValueField="ID" ID="ResourceDD" onchange="changeInfo(this.value);"  class="validate[required]"  ></asp:DropDownList>
<%--       <asp:DropDownList runat="server" DataTextField="ResourceName" DataValueField="ID" ID="DropDownList1"  AutoPostBack="true" onselectedindexchanged="ResourceDD_SelectedIndexChanged" ></asp:DropDownList>--%>
       </p>
   
        <span id="results"></span>
        