<%@ Page Language="vb" Title="Customer"  MasterPageFile="~/Site1.Master" AutoEventWireup="false" CodeBehind="Customer.aspx.vb" Inherits="VBPOAssignment.Customer1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <form runat="server">

        &nbsp;&nbsp;
    <br />

    <br />
        <asp:GridView ID="custGridView" runat="server" DataKeyNames="customerId" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
       <Columns>
           <asp:BoundField DataField="customerId" HeaderText="Customer ID" ReadOnly="true" />
           <asp:BoundField DataField="firstName" HeaderText="First Name" />
           <asp:BoundField DataField="lastName" HeaderText="Last Name" />
           <asp:BoundField DataField="address" HeaderText="Address" />
           <asp:BoundField DataField="creditLimit" HeaderText="Credit Limit" />
           <asp:BoundField DataField="telephoneNumber" HeaderText="Telephone Number" />
           <asp:BoundField DataField="faxNumber" HeaderText="Fax Number" />
           <asp:BoundField DataField="email" HeaderText="Email" />
           <asp:CommandField ShowDeleteButton="true" ShowEditButton="true" />
       </Columns>
        </asp:GridView>

         </form>
    </asp:Content>
