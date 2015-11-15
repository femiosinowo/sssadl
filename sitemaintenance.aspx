<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sitemaintenance.aspx.cs" Inherits="sitemaintenance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="/framework/css/sitemaintenance.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="" id="base">

      <!-- Alert (Dynamic Panel) -->
      <div data-label="Alert" class="ax_dynamic_panel" id="u0">
        <div data-label="State1" class="panel_state" id="u0_state0">
          <div class="panel_state_content" id="u0_state0_content">

            <!-- Unnamed (Shape) -->
            <div class="ax_shape" id="u1">
              <img src="/framework/images/u322.png" class="img " id="u1_img">
              <!-- Unnamed () -->
              <div class="text" id="u2" style="top: 70px; transform-origin: 638px 0px 0px;">
                <p><span></span></p>
              </div>
            </div>

            <!-- Unnamed (Image) -->
            <div class="ax_image" id="u3">
              <img src="/framework/images/caution.png" class="img " id="u3_img">
              <!-- Unnamed () -->
              <div class="text" id="u4" style="top: 38px; transform-origin: 36px 0px 0px;">
                <p><span></span></p>
              </div>
            </div>

            <!-- Unnamed (Shape) -->
            <div class="ax_h2" id="u5">
              <img src="/framework/images/transparent.gif" class="img " id="u5_img">
              <!-- Unnamed () -->
              <div class="text" id="u6">
                <p><span><%=MessageTitle %></span></p>
              </div>
            </div>

            <!-- Unnamed (Shape) -->
            <div class="ax_paragraph" id="u7">
              <img src="/framework/images/transparent.gif" class="img " id="u7_img">
              <!-- Unnamed () -->
              <div class="text" id="u8">
                <p><span><%=MessageToDisplay %>.</span></p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    </form>
</body>
</html>
