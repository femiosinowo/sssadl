<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="dynamicdb.aspx.cs" Inherits="Templates_NewsDetail" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>

 
 
 <asp:Content ID="Content5" ContentPlaceHolderID="cphSecondaryPageTitle" runat="Server">
 <ux:PageTitle ID="uxPageTitle"  runat="server" />
</asp:Content>


 <asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryBreadcrumb" runat="Server">
<ux:Breadcrumbs ID="uxBreadcrumb" runat="server" Visible="false" />
<div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/">Home</a></li> <li><a href="#"><%=Title %></a></li>
    </ul> 

   
    
      
</div>
	
</asp:Content>


 

<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" runat="Server">
   <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />
    
    

    <asp:ListView     ID="DL_Resourceslist"   runat="server" OnItemDataBound="DL_Resourceslist_ItemDatabound" >
               
 <EmptyDataTemplate>
 <table class="table table-bordered table-striped database-table">
				    
					<thead tabindex="0" role="button">
						<tr>
						   <th>No Resource found</th>
						</tr>	
					 </thead>
                     </table>
 </EmptyDataTemplate>
               <ItemTemplate>
  


          <p>
           <asp:HyperLink runat="server" CssClass="post"  ID="ResourceTitle" ></asp:HyperLink>
           <asp:Literal runat="server" ID="myfavicons" ></asp:Literal>
          <%--<a href="#" class="icon icon-book ga-event"><span class="favorite-id">AMA ePhysician Profile</span>
	  <svg>
		<use xlink:href="#icon-book">
	  </svg></a>--%>
      
<%--      <span id="addFavDiv<%# Eval("ID").ToString().Trim()%>"   <asp:Literal runat="server" ID="notFav" ></asp:Literal> >
      <a style="cursor:pointer"  onclick="addToFavorite(<%# Eval("ID").ToString().Trim()%>,1)" class="favorite" title="Add <%# Eval("ResourceName").ToString().Trim()%> to My Resource"> Add <span class="favorite-id-title"><%# Eval("ResourceName").ToString().Trim()%></span> to My Resources</a>
      </span>
       
       
    <span id="removeFavDiv<%# Eval("ID").ToString().Trim()%>"  <asp:Literal runat="server" ID="myFav" ></asp:Literal>       > 
    <a style="cursor:pointer"  title="Remove <%# Eval("ResourceName").ToString().Trim()%> from My Resources"   onclick="deleteFavorite(<%# Eval("ID").ToString().Trim()%>,1)" class="favorite favorite_on"   > Remove  <span class="favorite-id-title"><%# Eval("ResourceName").ToString().Trim()%></span> to My Resources</a></span>
--%>
	  <br/>
	  <asp:Literal runat="server" ID="Description" ></asp:Literal>
      <asp:Literal runat="server" ID="LoginInfomation" ></asp:Literal>
      
      <asp:Panel runat="server" ID="sharedPasswordPanel" Visible="false" >
      
      
      </asp:Panel>
      <asp:Literal runat="server" ID="bottomLinks" ></asp:Literal>
      </p> 

   
                  <!--end multimedia_block -->


 


               </ItemTemplate>
               </asp:ListView>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
 
	  
	  <h3>Related Information</h3>
	 	<ul>
		 <%=subjectArea%>
		</ul>
	  
	 
	  
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>


 
