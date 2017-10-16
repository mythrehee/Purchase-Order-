<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="VBPOAssignment.Login" %>

<!DOCTYPE html>


<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - My ASP.NET Application</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" runat="server"  href="~/css/site.css" />
</head>
<body>
   

        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand" runat="server">A2 Purchase Order</a>
                </div>
               <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                       <li><a runat="server" href="~/Home">Home</a></li>
                       <li><a runat="server" href="~/Contact">Contact</a></li>

                    </ul>
                
        </div>
        </div>
            </div>
        <div class="container body-content">
            <form runat="server">
                <div style="text-align: center;">
 
   <div style="width: 400px; margin-left: auto; margin-right:auto;">
 <br/>
 <asp:Login id="Login1" runat="server" 
                BorderStyle="Solid" 
                BackColor="#FFFFFF" 
                BorderWidth="1px"
                BorderColor="#CCCC99" 
                Font-Size="10pt" 
                Font-Names="Verdana" 
                CreateUserText="New User..?"
                CreateUserUrl="AddCustomer.aspx" 
                DisplayRememberMe="False" 
                OnLoggingIn="validateUser"
                     Width="500px" >
                <TitleTextStyle Font-Bold="True" 
                    ForeColor="#FFFFFF" 
                    BackColor="#6B696B">

                </TitleTextStyle>
            </asp:Login>
 
   </div>
 
</div>
            </form>
            <hr />
            <footer>
                <p>&copy; <%: DateTime.Now.Year %> - A2 Purchase Order</p>
            </footer>
        </div>

</body>
</html>
