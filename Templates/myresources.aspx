<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="myresources.aspx.cs" Inherits="Templates_Content" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>


 <asp:Content ID="Content5" ContentPlaceHolderID="cphSecondaryPageTitle" runat="Server">
 <ux:PageTitle ID="uxPageTitle"  runat="server" />
</asp:Content>

 <asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryBreadcrumb" runat="Server">
<ux:Breadcrumbs ID="uxBreadcrumb" runat="server" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" runat="Server">
 <h3><%=SATitle %></h3>
  <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />


 
  <hr />

 


      <asp:HiddenField runat="server" ID="SubjectAreaTaxIDHF" /> 
      

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
          
          <%--<a href="#" class="icon icon-book ga-event"><span class="favorite-id">AMA ePhysician Profile</span>
	  <svg>
		<use xlink:href="#icon-book">
	  </svg></a>--%>
      <asp:Literal runat="server" ID="myfavicons" ></asp:Literal>
<%--      <span id="addFavDiv<%# Eval("ID").ToString().Trim()%>"   <asp:Literal runat="server" ID="notFav" ></asp:Literal> >
      <a style="cursor:pointer"  onclick="addToFavorite(<%# Eval("ID").ToString().Trim()%>,<%# Eval("contentType").ToString().Trim()%>)" class="favorite" title="Add <%# Eval("ResourceName").ToString().Trim()%> to My Resource"> Add <span class="favorite-id-title"><%# Eval("ResourceName").ToString().Trim()%></span> to My Resources</a>
      </span>
       
       
    <span id="removeFavDiv<%# Eval("ID").ToString().Trim()%>"  <asp:Literal runat="server" ID="myFav" ></asp:Literal>       > 
    <a style="cursor:pointer"  title="Remove <%# Eval("ResourceName").ToString().Trim()%> from My Resources"   onclick="deleteFavorite(<%# Eval("ID").ToString().Trim()%>,<%# Eval("contentType").ToString().Trim()%>)" class="favorite favorite_on"   > Remove  <span class="favorite-id-title"><%# Eval("ResourceName").ToString().Trim()%></span> to My Resources</a></span>
--%>
	  <br/>
	  <asp:Literal runat="server" ID="Description" ></asp:Literal>
     <asp:Literal runat="server" ID="LoginInfomation" ></asp:Literal>
      
      <asp:Panel runat="server" ID="sharedPasswordPanel" Visible="false" >
      <a href="#" class="fixed_inline login_text">Login Information</a>
	  <span class="container-blue login_info">user ID -  <%# Eval("SharedUsername").ToString().Trim()%> / password - <%# Eval("SharedPassword").ToString().Trim()%></span>
      
      </asp:Panel> 
      </p> 

   
                  <!--end multimedia_block -->


 


               </ItemTemplate>
               </asp:ListView>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Ektron.DbConnection %>"
                                        >
                                    </asp:SqlDataSource>
                                    <hr />

               <asp:DataPager ID="DataPager1" Visible="false"  PageSize="125" runat="server" PagedControlID="DL_Resourceslist" OnPreRender="Datapager_prender">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="btn" ShowLastPageButton="false"  ShowNextPageButton="false" 
                                      />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowNextPageButton="true"
                                    ButtonCssClass="btn" ShowPreviousPageButton="false" />
                            </Fields>
                        </asp:DataPager>
 
    
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
