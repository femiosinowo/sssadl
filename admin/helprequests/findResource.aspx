<%@ Page Language="C#" AutoEventWireup="true" CodeFile="findResource.aspx.cs" Inherits="Templates_Ajax_clicktracks" %>


 <script>

  
function changeInfo(id){
if(id != "undefined"){
    var url = "/admin/controls/ajaxResourceDD.aspx?rid=" + id;
 
    $.get(url, function (data) {
        $("#results").html(data);

    });
    }
}
 
 </script>
 
  
     <select name="Resource"  size="1"  id="Resource" onchange="changeInfo(this.value);" 
         title="Resource" >
        <option selected="selected" value="">- Select a Resource - </option>
        <%=resourceDD%>
    </select>
     
        <span id="results"></span> <p></p>