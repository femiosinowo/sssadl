<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="Research.aspx.cs" Inherits="Templates_Content" %>
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

 
	  
	  <div class="img-polaroid margin-bottom">
		  <!--temporary-->
		  <div>
		  <%=bannerImage %>
		  </div>
	  </div>
	  <p>
	<%=displayContent %></p>
	  
	 
	  
	  

                                 <asp:ListView ID="RGTLV" runat="server" ItemPlaceholderID="lvPlaceholder" OnItemDataBound="RGTLV_ItemDatabound"   >
            <LayoutTemplate>
                 
                    <asp:PlaceHolder ID="lvPlaceholder" runat="server"></asp:PlaceHolder>
                 
            </LayoutTemplate>
            <ItemTemplate>
                 <p></p>
                 <asp:Literal runat="server" ID="headerH4"  ></asp:Literal>
                  
                   
                        <asp:Repeater ID="RGsRepeater" runat="server"  OnItemDataBound='RGsItem_ItemDatabound'  >
                        <HeaderTemplate>
                     
                        
                        </HeaderTemplate>
                        
                            <ItemTemplate>
                                
                                
                                  <p>
                                  <asp:HyperLink runat="server" ID="pdfurl" ></asp:HyperLink><br />
                            
<asp:Literal runat="server" ID="description"  ></asp:Literal>

</p>
                            </ItemTemplate>
                        </asp:Repeater> 

            </ItemTemplate>
        </asp:ListView>

	  
	 
       <asp:Literal runat="server" ID="headerH4"  ></asp:Literal>

      <asp:ListView ID="ListViewResourceSearchGuide" runat="server" ItemPlaceholderID="lvPlaceholder" OnItemDataBound="ListViewResourceSearchGuide_ItemDatabound"   >
            <LayoutTemplate>
                 
                    <asp:PlaceHolder ID="lvPlaceholder" runat="server"></asp:PlaceHolder>
                 
            </LayoutTemplate>
            <ItemTemplate>
                 <ItemTemplate>
                                
                                
                                  <p>
                                  <asp:HyperLink runat="server" ID="pdfurl" ></asp:HyperLink><br />
                            
<asp:Literal runat="server" ID="description"  ></asp:Literal>

</p>
                            </ItemTemplate>
                  
                    

            </ItemTemplate>
        </asp:ListView>


	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
