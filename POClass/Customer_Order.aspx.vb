Public Class Customer_Order
    Inherits System.Web.UI.Page
    Dim DBAccess As New DBAccess()
    Dim POContext As New POContext
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
            loadData()
        End If
    End Sub


    Private Sub loadData()
        ordGridView.DataSource = POContext.orders.ToList()
        ordGridView.DataBind()
    End Sub

    Private Sub prodGridView_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles prodGridView.RowCancelingEdit
        prodGridView.EditIndex = -1
        loadData()
    End Sub

    Private Sub ordGridView_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles ordGridView.RowDeleting
        Dim msg = "Are you sure you want to delete this Order?"
        Dim title = "Confirmation Required"
        Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or
            MsgBoxStyle.Critical

        Dim response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            Dim ordId As Integer
            ordId = e.Keys("orderId")
            DBAccess.delOrder(ordId)
            loadData()
        Else
            MsgBox("Delete Canceled", style, title)
        End If

    End Sub


    Private Sub ordGridView_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles ordGridView.RowCommand
        If e.CommandName = "OrderSelected_click" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim ordGridView As GridView = CType(e.CommandSource, GridView)
            Dim orderSelected = ordGridView.DataKeys(index).Value
            Cache("OrderSelected") = orderSelected
            loadproductData(orderSelected)
        End If
    End Sub

    Private Sub loadproductData(ord As Integer)
        prodGridView.DataSource = DBAccess.findProdListforOrder(ord)
        prodGridView.DataBind()
        Dim totalprice As Double
        For Each prod In DBAccess.findProdListforOrder(ord)
            totalprice += prod.price * prod.quantityOrdered
        Next
        Label1.Text = "Total Price :" & totalprice
    End Sub

    Private Sub prodGridView_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles prodGridView.RowDeleting
        Dim prodId As Integer
        prodId = e.Keys("productId")
        Dim editorderId As Integer = DirectCast(Cache("OrderSelected"), Integer)

        If DBAccess.delProductfromorder(editorderId, prodId) = True Then
            Dim msg = "Product Quantity Updated"
            MsgBox(msg, , "Confirmation")
        Else
            Dim msg = "Product could not be deleted"
            MsgBox(msg, , "Confirmation")
        End If

        loadproductData(editorderId)
    End Sub

    Private Sub prodGridView_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles prodGridView.RowEditing
        prodGridView.EditIndex = e.NewEditIndex
        'loadData()
    End Sub

    Private Sub prodGridView_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles prodGridView.RowUpdating
        Dim prodID As Integer = prodGridView.Rows(e.RowIndex).Cells(0).Text
        Dim quantityOrdered As TextBox = prodGridView.Rows(e.RowIndex).Cells(3).Controls(0)
        prodGridView.EditIndex = -1
        Dim prod As New Product()
        prod.productId = prodID
        prod.quantityOrdered = Integer.Parse(quantityOrdered.Text)
        Dim editorderId As Integer = DirectCast(Cache("OrderSelected"), Integer)
        DBAccess.updateOrderedProduct(editorderId, prod)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", POContext.products.ToString, True)
        POContext.SaveChanges()
        loadproductData(editorderId)
        Dim msg = "Product Quantity Updated"
        MsgBox(msg, , "Confirmation")
    End Sub




End Class