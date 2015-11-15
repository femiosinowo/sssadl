<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="confirm.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server"><div id="title-bar"><h2>Scheduled Report</h2></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->


	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	      <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li>Reports</li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	  
	  
<ux:SideNav ID="SideNav1" runat="server" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	   
      <asp:Panel runat="server" ID="SchedCreatePanel" Visible="false" >
      
           <div class="container-blue">
	     <h4><%=UpdSaveMessage %></h4>
		
	 
	  
	  </div>
      </asp:Panel>

	   
	  
	 
	    

	     
	  

         
	   
	    <p></p>

        

      <p>
      

      </p><p>
      

      </p>
        
	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
	  <!-- END GRID -->
	  
	  </div>
	  <!-- END PAGE CONTENT -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

