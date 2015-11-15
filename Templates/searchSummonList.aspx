<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/OneColumn.master" AutoEventWireup="true" CodeFile="searchSummonList.aspx.cs" Inherits="Templates_Content" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
 <%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>

 
<asp:Content ID="Content7" ContentPlaceHolderID="cphSecondaryMainHead" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="cphSecondaryMainContent" Runat="Server">

 
	<div class="column-12 print-hide">
		
			  <div class="ssadl-int-search form" id="ssadl-resource-search">
				 <label for="site_search_field"><span>Search</span></label>
		 		<input type="search" id="home_search" value="<%=search %>" onkeypress="return summonSearchList(event);" class="int_search_field">
		 		<input type="button" class="submit" id="int_search_button"  onclick="summonSearchList2(event);" value="Search" />
		 		
			<span id="view_type" aria-hidden="true"></span>
				<a href="<%=ViewTypeURL %>">View as a Type</a>
			  </div>
			  
			   
		
		</div>
		<!-- END COLUMN -->
		<div class="column-12 print-full-width">
	  
	  			<!-- mmmmmmmmmmmmmm ALL RESULTS mmmmmmmmmmmmmm -->
	  			<div class="results-block" id="all-results-block">
				  
				   <h3> <%=h3Results %></h3>
 
				     

                   <asp:ListView ID="listViewArticles" OnItemDataBound="listViewDataBound"    runat="server"   >
               
 
               <ItemTemplate>
 
			  <ul class="block-content">
					     <li class="title">
                         
                          <asp:HyperLink  runat="server" CssClass="post" Target="_blank"  ID="ResourceTitle" ></asp:HyperLink>   <asp:Literal runat="server" ID="ResourceYear" > </asp:Literal>
                        <asp:Literal runat="server" ID="myfavicons" ></asp:Literal>
                         </li>
                                      <li class="issue"> <asp:Literal runat="server" ID="Description" > </asp:Literal></li>
						 <li class="author"> <asp:Literal runat="server" ID="ResourceAuthor" > </asp:Literal></li>
						 <li class="location"> <asp:Literal runat="server" ID="Location" > </asp:Literal></li>
                          <li class="type"> <asp:Literal runat="server" ID="SummonContentType" > </asp:Literal></li>
                         
<%--                         			     <li class="title"><a href="#"><span class="favorite-id">Assessing psychological trauma and PTSD</span></a> (2004) <a href="#" class="favorite" title="Add Assessing psychological trauma and PTSD to My Resource">  <span class="favorite-id-title"></span> to My Resources</a></li>
						 <li class="author">by Habka; Barjon</li>
						 <li class="location">New York, McGraw-Hill</li>
						 <li class="type">ebook</li>
						 --%>
					</ul>

               </ItemTemplate>
               </asp:ListView>

				   
			 
				  
	  
	            </div><!--results block-->
	  
	   
       
	  
	  </div>
      <div class="results_pagination">

         <div class="float-left">
				   <span id="pager_prev" class="pager_buttons">
					 
                      <asp:LinkButton runat="server" ID="PrevBtn" CssClass="btn" Text="Prev" 
                       onclick="PrevBtn_Click" ></asp:LinkButton>
				   </span>
				   
				   <span id="pager_next" class="pager_buttons">
					 
                      <asp:LinkButton runat="server" ID="NextBtn" CssClass="btn" Text="Next" 
                       onclick="NextBtn_Click"  ></asp:LinkButton>
					</span>
		   		</div>
 

                          <div class="float-right">
                            <p>
                                <label for="pager_textbox" class="inline-label">
                                    Go to page</label>
                                <asp:TextBox runat="server" ID="pager_textbox" Width="40"  ></asp:TextBox>
                                 
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                  <asp:LinkButton runat="server" ID="LinkButton1" onclick="goBtnClick" CssClass="btn" Text="Go"  ></asp:LinkButton>
                                
                            </p>
                        </div>
    </div>
		 
	  <!-- END COLUMN -->
	  
	   
	  <!-- END COLUMN -->
</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="cphSecondaryJSCode" Runat="Server">
</asp:Content>




 
 