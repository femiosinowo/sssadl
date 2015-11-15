<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="database.aspx.cs" Inherits="Templates_Content" %>
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

  <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />
  <!-- GRID -->
<div class="grid">
 
			
			<div class="row-12">
			
			<div class="column-12 print-full-width">
			
			<div id="pager-alpha">
			  <div class="nav">
			      <span>Show:</span>
				  <a href="#" id="all" class="alpha-id active">ALL</a>
				  <a href="#" id="ab" class="alpha-id">A-B</a>
				  <a href="#" id="cd" class="alpha-id">C-D</a>
				  <a href="#" id="eh" class="alpha-id">E-H</a>
				  <a href="#" id="im" class="alpha-id">I-M</a>
				  <a href="#" id="no" class="alpha-id">N-O</a>
				  <a href="#" id="pr" class="alpha-id">P-R</a>
				  <a href="#" id="st" class="alpha-id">S-T</a>
				  <a href="#" id="uz" class="alpha-id">U-Z</a>
			 </div><!-- nav -->
			 
			</div><!-- pager alpha -->
			
			<hr>
			
			<div class="filter">
			   <label for="database_keyword">Filter by keyword:</label>
			  
               <asp:TextBox runat="server" ID="databaseKeyword" ></asp:TextBox>
	 <asp:HiddenField runat="server" ID="hiddenKeyword"  />
               <asp:Button runat="server" CssClass="btn" Text="Go" ID="btnsubmit" 
                    onclick="btnsubmit_Click" />
			</div><!-- filter -->
			
			<div class="show_hide_desc_wrapper">
				<span><a href="#" class="show_all_desc">Show all descriptions</a></span>
				<span><a href="#" class="hide_all_desc">Hide all descriptions</a></span>
		   </div><!-- show_hide_desc_wrapper -->
			
			<div class="clear"></div>
			
			<div class="database">
			
			 
				<asp:ListView     ID="DL_Databaselist"   runat="server" OnItemDataBound="DL_Databaselist_ItemDatabound" >
               
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
  


           <div class="page database-<%# Eval("alphaRange")%>" id="database-<%# Eval("alpha")%>">
           
           <table class="table table-bordered table-striped database-table">
				    <caption>Database <%# Eval("alpha")%>
					</caption>
					<thead tabindex="0" role="button">
						<tr>
						   <th><%# Eval("alpha")%></th>
						</tr>	
					 </thead>
                     
                     <tbody>
                     


                           <asp:Repeater ID="alphaRepeater" runat="server"  OnItemDataBound='alphaRepeaterItem_ItemDatabound'  >
                        <HeaderTemplate>
                     
                        
                        </HeaderTemplate>
                        
                            <ItemTemplate>
                                

                                <tr>
						   <td>
                                     <asp:HyperLink runat="server" CssClass="post"  ID="ResourceTitle" ></asp:HyperLink> 
<asp:Literal runat="server" ID="myFav" ></asp:Literal> 



						   <a href="#" class="show_desc_info">show information ></a>
						   <div class="show_desc_info_content">
                           <%# Eval("Description").ToString().Trim()%>                         
                                
      
<asp:Literal runat="server" ID="bottomLinks" ></asp:Literal>
                           
                           </div>
	  						
	  						</td>
						</tr>	

                                
                                   
                            </ItemTemplate>
                        </asp:Repeater> 





                     
                     </tbody>
                     
                     
                     
                     
                     
                     </table>
           
           
           </div> 

   
                  <!--end multimedia_block -->


 


               </ItemTemplate>
               </asp:ListView>
				 
			
			</div><!-- database-->
			
			</div>
			<!-- END COLUMN -->

</div>
<!-- END ROW -->

</div>
<!-- END GRID -->
 
	  <!-- END COLUMN -->
	  
 
</asp:Content>
 
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
