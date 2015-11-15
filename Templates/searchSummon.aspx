<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true" CodeFile="searchSummon.aspx.cs" Inherits="Templates_Content" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
 <%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>

 
<asp:Content ID="Content7" ContentPlaceHolderID="cphSecondaryMainHead" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="cphSecondaryMainContent" Runat="Server">

 
	<div class="column-12 print-hide">
		
			  <div class="ssadl-int-search form" id="ssadl-resource-search">
				 <label for="site_search_field"><span>Search</span></label>
		 		<input type="search" id="home_search" value="<%=search %>"  onkeypress="return summonSearch(event);" class="int_search_field">
		 		<input type="button" class="submit" id="int_search_button"  onclick="summonSearch2(event);" value="Search" />
		 		
				<span id="view_list" aria-hidden="true"></span>
				<a href="<%=viewListURL %>">View as a List</a>
			  </div>
			  
			  <div id="jump-links">
			  	<ul>
				  <li>Jump to:</li>
				  <li><a aria-labelledby="Jump to Articles" title="Jump to Articles" href="#articles">Articles</a></li>
				  <li><a aria-labelledby="Jump to eBooks" title="Jump to eBooks" href="#ebooks">eBooks</a></li>
				  <li><a aria-labelledby="Jump to Databases" title="Jump to Databases" href="#databases">Databases</a></li>
				  <li><a aria-labelledby="Jump to Journals" title="Jump to Journals" href="#journals">Journals</a></li>
				  <li><a aria-labelledby="Jump to Other Resources" title="Jump to Other Resources" href="#other_resources">Other Resources</a></li>
				  
				</ul>
				
				
			  
			  </div>
		
		</div>
		<!-- END COLUMN -->
		
		<div class="column-6 print-full-width">
	  
	  			<!-- mmmmmmmmmmmmmm ARTICLES mmmmmmmmmmmmmm -->
	  			<div class="results-block" id="articles-block">
				   <a id="articles"></a>
				   <h3>Articles</h3> 			   
				 
			 <asp:ListView ID="listViewArticles" OnItemDataBound="listViewDataBound"    runat="server"   >
               
 
               <ItemTemplate>
 
			  <ul class="block-content">
					     <li class="title">
                         
                          <asp:HyperLink  runat="server" CssClass="post"  ID="ResourceTitle" ></asp:HyperLink>   <asp:Literal runat="server" ID="ResourceYear" > </asp:Literal>
                        <asp:Literal runat="server" ID="myfavicons" ></asp:Literal>
                         </li>
                                      <li class="issue"> <asp:Literal runat="server" ID="Description" > </asp:Literal></li>
						 <li class="author"> <asp:Literal runat="server" ID="ResourceAuthor" > </asp:Literal></li>
						 <li class="issue"> <asp:Literal runat="server" ID="ResourceIssue" > </asp:Literal></li>
						 
					</ul>

               </ItemTemplate>
               </asp:ListView>

				   
				   <p class="more_results">  <asp:HyperLink runat="server" ID="ArticlesHyperLink"  ></asp:HyperLink> </p>
	  
	            </div><!--results block-->
				
				<!-- mmmmmmmmmmmmmm eBOOKS mmmmmmmmmmmmmm -->
				<div class="results-block" id="ebooks-block">
				   <a id="ebooks"></a>
				   <h3>eBooks</h3>
				   
<asp:ListView ID="listVieweBooks" OnItemDataBound="listViewDataBound"    runat="server"   >
               
 
               <ItemTemplate>
 
			  <ul class="block-content">
					     <li class="title">
                         
                          <asp:HyperLink  runat="server" CssClass="post"  Target="_blank" ID="ResourceTitle" ></asp:HyperLink>   <asp:Literal runat="server" ID="ResourceYear" > </asp:Literal>
                        <asp:Literal runat="server" ID="myfavicons" ></asp:Literal>
                         </li>
                                      <li class="issue"> <asp:Literal runat="server" ID="Description" > </asp:Literal></li>
						 <li class="author"> <asp:Literal runat="server" ID="ResourceAuthor" > </asp:Literal></li>
						 <li class="issue"> <asp:Literal runat="server" ID="ResourceIssue" > </asp:Literal></li>
						 
					</ul>

               </ItemTemplate>
               </asp:ListView>
				   
				   <p class="more_results"><asp:HyperLink runat="server" ID="ebooksHyperLink" ></asp:HyperLink></p>
	  
	            </div><!--results block-->
	  
	  
	  
	  </div>
	  <!-- END COLUMN -->
	  
	  <div class="column-6 print-full-width">
	  
	  <!-- mmmmmmmmmmmmmm DATABASES mmmmmmmmmmmmmm -->
	 			<div class="results-block" id="databases-block">
				   <a id="databases"></a>
				   <h3>Databases</h3>

					 <asp:ListView ID="listViewDatabases" OnItemDataBound="listViewDataBound"    runat="server"   >
               
 
               <ItemTemplate>
 
			  <ul class="block-content">
					     <li class="title">
                         
                          <asp:HyperLink  runat="server" CssClass="post"  ID="ResourceTitle"  ></asp:HyperLink>   <asp:Literal runat="server" ID="ResourceYear" > </asp:Literal>
                        <asp:Literal runat="server" ID="myfavicons" ></asp:Literal>
                         </li>
                          <li class="issue"> <asp:Literal runat="server" ID="Description" > </asp:Literal></li>
						 <li class="author"> <asp:Literal runat="server" ID="ResourceAuthor" > </asp:Literal></li>
						 <li class="issue"> <asp:Literal runat="server" ID="ResourceIssue" > </asp:Literal></li>
						 
					</ul>

               </ItemTemplate>
               </asp:ListView>
					
				   		<p>Browse databases by subject</p>
                        <ul>
                        <asp:ListView ID="listSubJectArea" OnItemDataBound="listSubJectAreaDataBound"    runat="server"   >
               
 
               <ItemTemplate>
 
			   <li><asp:HyperLink runat="server" ID="saLinkTitle" ></asp:HyperLink></li>

               </ItemTemplate>
               </asp:ListView>

						</ul>
					 
				   
				   
				   <p class="more_results"><asp:HyperLink runat="server" ID="DataBasesHyperLink" ></asp:HyperLink> </p>
	  
	            </div><!--results block-->
				
				
				<!-- mmmmmmmmmmmmmm Journals mmmmmmmmmmmmmm -->
				<div class="results-block" id="journals-block">
				   <a id="journals"></a>
				   <h3>Journals</h3>
				   
<asp:ListView ID="listViewJournals" OnItemDataBound="listViewDataBound"    runat="server"   >
               
 
               <ItemTemplate>
 
			  <ul class="block-content">
					     <li class="title">
                         
                          <asp:HyperLink Target="_blank" runat="server" CssClass="post"  ID="ResourceTitle" ></asp:HyperLink>   <asp:Literal runat="server" ID="ResourceYear" > </asp:Literal>
                        <asp:Literal runat="server" ID="myfavicons" ></asp:Literal>
                         </li>
                                      <li class="issue"> <asp:Literal runat="server" ID="Description" > </asp:Literal></li>
						 <li class="author"> <asp:Literal runat="server" ID="ResourceAuthor" > </asp:Literal></li>
						 <li class="issue"> <asp:Literal runat="server" ID="ResourceIssue" > </asp:Literal></li>
						 
					</ul>

               </ItemTemplate>
               </asp:ListView>
				   
				   <p class="more_results"><asp:HyperLink runat="server" ID="JournalsHyperLink" ></asp:HyperLink></p>
	  
	            </div><!--results block-->
				
				
				<!-- mmmmmmmmmmmmmm OTHER RESOURCES mmmmmmmmmmmmmm -->
				<div class="results-block" id="other-resources-block">
				   <a id="other_resources"></a>
				   <h3>Other Resources</h3>
				   
				  <p><%=ContentDMSearchResults %></p>
				  <p> <asp:HyperLink Target="_blank" runat="server" CssClass="post"  ID="AudioHyperLink" ></asp:HyperLink> </p>
				  <p> <asp:HyperLink Target="_blank" runat="server" CssClass="post"  ID="VideoHyperLink" ></asp:HyperLink> </p>				  
	    <p> <asp:HyperLink Target="_blank" runat="server" CssClass="post"  ID="PhotographHyperLink" ></asp:HyperLink> </p>
	            </div><!--results block-->
	  
	  
	   </div>
	  <!-- END COLUMN -->
</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="cphSecondaryJSCode" Runat="Server">
</asp:Content>




 
 