<%@ Control Language="C#" AutoEventWireup="true" CodeFile="subject.ascx.cs" Inherits="admin_helprequests_resource" %>

<script>


    $(document).ready(function () {

        $('#ctl00_mainContentCP_SubjectRA_subjectDD').change(function () {
            var whichOne = $('#whichResources').val();
            var url = "/admin/helprequests/findResource.aspx?subjectID=" + $(this).val() + "&loadResFor=" + whichOne;

            $(document).ajaxStart(function () {
                $("#loading").show();
                //  $("#resourceDiv").hide();
            });
            $(document).ajaxStop(function () {
                $("#loading").hide();
                //$("#ajaxContact").show();
            });

            $.get(url, function (data) {
                $("#resourceDiv").html(data);

            });


        });

    });


</script>

 <p>
          <label class="bold" for="subjectDD"><%=LabelTitle %></label>
       <asp:DropDownList runat="server" DataTextField="Name" DataValueField="TaxID" ID="subjectDD"  ></asp:DropDownList>
       </p>

      
       