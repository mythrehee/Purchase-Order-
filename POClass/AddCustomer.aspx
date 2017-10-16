<%@ Page Title="Add New Customer" Language="vb" AutoEventWireup="false"  CodeBehind="AddCustomer.aspx.vb" Inherits="VBPOAssignment.AddCustomer" %>


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
                          <li><a runat="server" href="~/Purchase_Order">Purchase Order</a></li>          
                       <li><a runat="server" href="~/Login">Login</a></li>
                       
                    </ul>
                
        </div>
            </div></div>
        <div class="container body-content">
            &nbsp;<form id="custForm" runat="server">
                
          <h1>Customer Registration</h1>
        <asp:ValidationSummary runat=server headertext="There were errors on the page:" />

        <h2>Account Details:</h2>
           <asp:Label ID="Label8" runat="server" AssociatedControlID="userBox">Username:</asp:Label>
        <asp:TextBox ID="userBox" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=userBox
            errormessage="UserName is required.*"> </asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator CssClass="rfv" runat="server"     
               ErrorMessage="UserName should be letters and between 4-12 letters." 
              ControlToValidate="userBox" Display="Dynamic"     
          ValidationExpression="^[a-zA-Z]{4,12}$" />

        <br/>   
         <asp:Label ID="Label9" runat="server" AssociatedControlID="passBox">Password:</asp:Label>
        <asp:TextBox ID="passBox" type=password runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=passBox
            errormessage="Password is required.*"> </asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator CssClass="rfv" runat="server"     
                                    ErrorMessage="Not a valid password. Minimum eight characters, at least one uppercase letter, one lowercase letter and one number" 
                                    ControlToValidate="passBox"
                                    Display="Dynamic" ValidationExpression= "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$" />
        <br />
          <asp:Label ID="Label7" runat="server" AssociatedControlID="emailBox">Email:</asp:Label>
        <asp:TextBox ID="emailBox" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator runat=server CssClass="rfv"
            controltovalidate=emailBox
            errormessage="Email is required.*"> </asp:RequiredFieldValidator>
        <%--Validationexpression taken from the following website: --%>
       <%--https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format --%>
          <asp:RegularExpressionValidator CssClass="rfv" runat="server"     
                                    ErrorMessage="Not a valid Email. " 
                                    ControlToValidate=emailBox
          Display="Dynamic" ValidationExpression="^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$" />
            <h2>Customer Information:</h2>

      <asp:Label ID="Label1" runat="server" AssociatedControlID="custFnBox">First Name:</asp:Label>
        <asp:TextBox ID="custFnBox" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=custFnBox
            errormessage="First Name is required.*"> </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator CssClass="rfv" ID="regexpFName" runat="server"     
                                    ErrorMessage="First name should be letters and between 2-12 letters." 
                                    ControlToValidate="custFnBox"
                                    Display="Dynamic"     
                                    ValidationExpression="^[a-zA-Z'.\s]{2,12}$" />
        <br />
   <asp:Label ID="Label2" runat="server" AssociatedControlID="custLnBox">Last Name:</asp:Label>
        <asp:TextBox ID="custLnBox" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat=server  CssClass="rfv" Display="Dynamic"
            controltovalidate=custLnBox
            errormessage="Last Name is required.*"> </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="rfv" runat="server"     
                                    ErrorMessage="Last Name should be letters and between 2-12 letters." 
                                    ControlToValidate="custLnBox"
                                    Display="Dynamic"     
                                    ValidationExpression="^[a-zA-Z'.\s]{2,12}$" />
        <br />
  
   <asp:Label ID="Label3" runat="server" AssociatedControlID="custFnBox">Address:</asp:Label>
        <asp:TextBox ID="custAddr" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=custAddr
            errormessage="Address is required.*"> </asp:RequiredFieldValidator>
     <br />
       <asp:Label ID="Label4" runat="server" AssociatedControlID="ccBox">Credit Limit:</asp:Label>
        <asp:TextBox type="Integer" MinimumValue="0" ID="ccBox" runat="server" class="form-control"></asp:TextBox>
          <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=ccBox
            errormessage="Credit Limit is required.*"> </asp:RequiredFieldValidator>
        <asp:RangeValidator id="Range1" CssClass="rfv"
           ControlToValidate=ccBox MinimumValue="100" MaximumValue="100000"
           Type="Integer" errormessage="Credit limit must be from 100 to 100000!"
           runat="server"/>
        <br />
   <asp:Label ID="Label5" runat="server" AssociatedControlID="telBox">Telephone:</asp:Label>
        <asp:TextBox ID="telBox" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=telBox
            errormessage="Telephone is required.*"> </asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator CssClass="rfv" runat="server"     
                                    ErrorMessage="Not a valid Telphone#.  4166666666 (May include - or space just not in the beginning or end)" 
                                    ControlToValidate="telBox"
                                    Display="Dynamic" ValidationExpression="^[0-9-+ ]+$" />
          <br />
    <asp:Label ID="Label6" runat="server" AssociatedControlID="faxBox">Fax:</asp:Label>
        <asp:TextBox ID="faxBox" runat="server" class="form-control"></asp:TextBox>
          <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=faxBox
            errormessage="Fax# is required.*"> </asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator CssClass="rfv" runat="server"     
                                    ErrorMessage="Not a valid Fax. 4166666666 (May include - or space just not in the beginning or end)" 
                                    ControlToValidate=faxBox
                                    Display="Dynamic" ValidationExpression="^[0-9-+ ]+$" />
        <br />
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="addButton" runat="server" Text="Register"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="clearButton" runat="server" Text="Clear"  CausesValidation="false" />
    </p>
          </form>
            <hr />
            <footer>
                <p>&copy; <%: DateTime.Now.Year %> - A2 Purchase Order</p>
            </footer>
        </div>

</body>
</html>
