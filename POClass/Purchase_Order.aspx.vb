Public Class Purchase_Order
    Inherits System.Web.UI.Page
    Dim DBAccess As New DBAccess()
    Dim selectedProd As New List(Of Product)
    Dim newOrder As New Order
    Dim newOrderProdList As New List(Of Product)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cust As Customer = DirectCast(Session("User"), Customer)

        If IsNothing(Session("User")) Then
            MsgBox("You do not have access to this page. Please login to Access.", , "Authorization Error")
            Response.Redirect("~/Login.aspx", True)
        ElseIf cust.firstName.ToUpper.CompareTo("Admin".ToUpper) = 0 Then
            MsgBox("You do not have access to this page. Please login to Access.", , "Authorization Error")
            Response.Redirect("~/Login.aspx", True)
        End If
        If Not IsPostBack Then
            DBAccess.resetproductList()
            loadData()
        End If
        TextBox1.Text = DateTime.Now
    End Sub

    Private Sub loadData()
        prodGridView.DataSource = DBAccess.loadProductlist
        prodGridView.DataBind()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim totalprice As Double = 0

        If Button1.Text = "Confirm Order" Then
            DBAccess.confirmOrder(newOrder)
            Button1.Enabled = False
            Label3.Text = "Order Placed"
        Else
            newOrder.orderDate = Date.Today.ToString("d")
            newOrder.cust = DirectCast(Session("User"), Customer).customerId


            ' Iterate through the prodGridView.Rows property
            For Each row As GridViewRow In prodGridView.Rows
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("ProductSelected")
                If cb IsNot Nothing AndAlso cb.Checked Then
                    ' First, get the ProductID for the selected row
                    Dim productID As Integer =
                        Convert.ToInt32(prodGridView.DataKeys(row.RowIndex).Value)
                    Dim prodadded As Product = DBAccess.findProduct(productID)
                    Dim tb As TextBox = CType(row.FindControl("OrderQuantity"), TextBox)
                    prodadded.quantityOrdered = tb.Text
                    selectedProd.Add(prodadded)
                End If
            Next

            For Each prod In selectedProd

                totalprice += prod.price * prod.quantityOrdered
            Next
            Dim prodfailedtobeordered As List(Of Product) = DBAccess.addOrder(newOrder, selectedProd)
            Dim failure As String = ""
            If prodfailedtobeordered.Count = 0 Then
                failure = ""
            Else
                failure = "The following products where not in the inventory.Try placing the product with less quantity "
                For Each prod In prodfailedtobeordered
                    failure += prod.description + ","
                Next
            End If

            loadData()
            Label1.Text = totalprice
            Button1.Text = "Confirm Order"
            Label2.Text = failure
            For Each row As GridViewRow In prodGridView.Rows
                ' Access the CheckBox
                Dim cb As CheckBox = row.FindControl("ProductSelected")
                cb.Enabled = False
                Dim tb As TextBox = CType(row.FindControl("OrderQuantity"), TextBox)
                tb.Enabled = False
            Next
        End If

    End Sub

    Protected Sub ProductSelected_CheckedChanged(sender As Object, e As EventArgs)
        For Each row As GridViewRow In prodGridView.Rows
            ' Access the CheckBox
            Dim cb As CheckBox = row.FindControl("ProductSelected")
            If cb IsNot Nothing AndAlso cb.Checked Then
                ' First, get the ProductID for the selected row
                Dim tb As TextBox = CType(row.FindControl("OrderQuantity"), TextBox)
                tb.Enabled = True
            ElseIf cb.Checked = False Then
                Dim tb As TextBox = CType(row.FindControl("OrderQuantity"), TextBox)
                tb.Enabled = False
            End If

        Next
    End Sub

End Class