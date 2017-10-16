<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AddProduct.aspx.vb" Inherits="VBPOAssignment.AddProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form id="prodForm" runat="server">
            <asp:ValidationSummary runat=server headertext="There were errors on the page:" />
  <h2>Add New Product</h2>
    <p>
      <asp:Label ID="Label1" runat="server" AssociatedControlID="pDescBox">Description:</asp:Label>
        <asp:TextBox ID="pDescBox" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=pDescBox
            errormessage="Description is required.*"> </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator CssClass="rfv" runat="server"     
                                    ErrorMessage="Description should be letters and between 2-12 letters." 
                                    ControlToValidate="pDescBox"
                                    Display="Dynamic"     
                                    ValidationExpression="^[a-zA-Z'.\s]{2,12}$" />
        <br />

       <asp:Label ID="Label2" runat="server" AssociatedControlID="pPriceBox">Price:</asp:Label>
        <asp:TextBox  ID="pPriceBox" runat="server" class="form-control"></asp:TextBox>
          <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=pPriceBox
            errormessage="Price is required.*"> </asp:RequiredFieldValidator>
         <asp:RegularExpressionValidator CssClass="rfv" runat="server"     
                                    ErrorMessage="Not a valid price." 
                                    ControlToValidate="pPriceBox"
                                    Display="Dynamic"     
                                    ValidationExpression="^\$?\d+(\.(\d{2}))?$" />
       
        <br />
     <asp:Label ID="Label3" runat="server" AssociatedControlID="pQtyBox">Quantity:</asp:Label>
        <asp:TextBox ID="pQtyBox" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=pQtyBox
            errormessage="Quantity is required.*"> </asp:RequiredFieldValidator>
        <asp:RangeValidator id="Range1" CssClass="rfv"
           ControlToValidate=pQtyBox MinimumValue="0" MaximumValue="100"
           Type="Integer" errormessage="Quantity must be from 0 to 100!"
           runat="server"/>
          <br />
    <asp:Label ID="Label4" runat="server" AssociatedControlID="pReordBox">Reorder#::</asp:Label>
        <asp:TextBox ID="pReordBox" runat="server" class="form-control"></asp:TextBox>
          <asp:RequiredFieldValidator runat=server CssClass="rfv" Display="Dynamic"
            controltovalidate=pReordBox
            errormessage="Reorder# is required.*"> </asp:RequiredFieldValidator>
        <asp:RangeValidator id="RangeValidator2" CssClass="rfv"
           ControlToValidate=pReordBox MinimumValue="0" MaximumValue="100"
           Type="Integer" errormessage="Reorder# must be from 0 to 100!"
           runat="server"/>
        <br />
  
    </p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="addButton" runat="server" Text="Add"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="clearButton" runat="server" Text="Clear"  CausesValidation="false"/>
    </p>
    </form>
</asp:Content>
