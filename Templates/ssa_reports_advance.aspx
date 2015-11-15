<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="ssa_reports_advance.aspx.cs" Inherits="Templates_Content" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>


 <asp:Content ID="Content5" ContentPlaceHolderID="cphSecondaryPageTitle" runat="Server">
 <ux:PageTitle ID="uxPageTitle"  runat="server" />
</asp:Content>

 <asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryBreadcrumb" runat="Server">
<ux:Breadcrumbs ID="uxBreadcrumb" runat="server" />
<script> $('#searchDropdownBox').val('SSA Reports');  </script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" runat="Server">
  <h3>Social Security Administration Digital Reports Archive</h3>
  <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />

  	  <form class="ssadl-int-search form" id="ssa-archive-search">
	     <label for="archive_search_field"><span>Search</span></label>
		 <input type="search" id="archive_search_field" class="int_search_field">
		 <input type="button" id="int_search_button" value="Search" alt="Social Security Administration Digital Reports Archive Search button"/>
		 <a href="ssa_reports_archive_advanced.html">Advanced Search</a>
	  </form>
	  
	  <div class="results_content">
	     <p class="float-left">Results 1-10 of 634</p>
		 <p class="float-right"><label for="show_results" class="inline-label">Show:</label>
		   <select id="show_results">
		     <option value="10">10</option>
			 <option value="20">25</option>
			 <option value="20">50</option>
			 <option value="20">100</option>
		   </select>
		 </p>
	  </div>
	  
	  <div class="clearfix"></div>

      		<p><a href="#"><span class="favorite-id">Aenean Euismod Bibendum Laoreet</span></a><a href="#" class="favorite" title="Add Aenean Euismod Bibendum Laoreet to My Resources">  <span class="favorite-id-title"></span> to My Resources</a><br/>
	  Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce iaculis, urna quis tempus lobortis, ligula lorem ultrices lorem, id tincidunt leo erat consequat ipsum. Aenean sollicitudin consequat sem eu accumsan. </p>
	  
	  <p><a href="#"><span class="favorite-id">Aenean Euismod Bibendum Laoreet</span></a><a href="#" class="favorite" title="Add Aenean Euismod Bibendum Laoreet to My Resources">  <span class="favorite-id-title"></span> to My Resources</a><br/>
	  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
	  
	  <p><a href="#"><span class="favorite-id">Aenean Euismod Bibendum Laoreet</span></a><a href="#" class="favorite" title="Add Aenean Euismod Bibendum Laoreet to My Resources">  <span class="favorite-id-title"></span> to My Resources</a><br/>
	  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
	  
	  <p><a href="#"><span class="favorite-id">Aenean Euismod Bibendum Laoreet</span></a><a href="#" class="favorite" title="Add Aenean Euismod Bibendum Laoreet to My Resources">  <span class="favorite-id-title"></span> to My Resources</a><br/>
	  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
	  
	  <p><a href="#"><span class="favorite-id">Aenean Euismod Bibendum Laoreet</span></a><a href="#" class="favorite" title="Add Aenean Euismod Bibendum Laoreet to My Resources">  <span class="favorite-id-title"></span> to My Resources</a><br/>
	  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
	  
	  <p><a href="#"><span class="favorite-id">Aenean Euismod Bibendum Laoreet</span></a><a href="#" class="favorite" title="Add Aenean Euismod Bibendum Laoreet to My Resources">  <span class="favorite-id-title"></span> to My Resources</a><br/>
	  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
	  
	  <p><a href="#"><span class="favorite-id">Aenean Euismod Bibendum Laoreet</span></a><a href="#" class="favorite" title="Add Aenean Euismod Bibendum Laoreet to My Resources">  <span class="favorite-id-title"></span> to My Resources</a><br/>
	  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
	  

      		   <div class="results_pagination">
		     <div class="float-left">
				   <span id="pager_prev" class="pager_buttons">
					 <a href="#" class="btn">Prev</a>
				   </span>
				   
				   <span id="pager_next" class="pager_buttons">
					  <a href="#" class="btn">Next</a>
					</span>
		   		</div>
		   
			   <div class="float-right">
				   <p><label for="pager_textbox" class="inline-label">Go to page</label>
				   <input type="text" placeholder="1" id="pager_textbox">
				   of
				   <span>100</span>
				   <a href="#" class="btn">Go</a>
				   </p>
				</div>
				   
			</div><!--results_pagination-->
 
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
