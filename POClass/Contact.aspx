<%@ Page Title="Contact" Language="VB"  AutoEventWireup="true" CodeBehind="Contact.aspx.vb" Inherits="VBPOAssignment.Contact" %>

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
                    <a class="navbar-brand" runat="server" href="~/Home">A2 Purchase Order</a>
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
    <h2><%: Title %></h2>
    <p>If you questions or concerns about this website please contact us at:</p>
    <address>
        1750 Finch Ave E<br />
        Toronto, ON M2J 2X5<br />
        <abbr title="Phone">P:</abbr>
        425.491.5050    
    </address>

    <address>
        <strong>Support:</strong>Mythrehee Himachalapathy - <a href="mailto:mhimachalapathy@myseneca.ca">mhimachalapathy@myseneca.ca</a><br />
        <strong>Support:</strong>Jocelyne Sinogo - <a href="mailto:jsinogo@myseneca.ca">jsinogo@myseneca.ca</a>
    </address>
      
 
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
