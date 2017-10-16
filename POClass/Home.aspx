<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Home.aspx.vb" Inherits="VBPOAssignment.Home" %>

<!DOCTYPE html>


<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - A2 Purchase Order</title>

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
                    <a class="navbar-brand" runat="server" href="~/Home">A2 Purchase Order</a>
                </div>
              
        </div>
            </div>
        <div class="container body-content">
            <form runat="server">
                <div style="text-align: center;">
 
   <div margin-left: auto; margin-right:auto;">
 <br/>


    <div class="jumbotron">
        <h1>Welcome to A2 Purchase Order</h1>
    To access the features of this application please login by <a runat="server" href="Login.aspx">clicking here.</a> 

    </div>
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