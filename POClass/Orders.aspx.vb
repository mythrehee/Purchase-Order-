Public Class Orders
    Inherits System.Web.UI.Page
    Dim DBAccess As New DBAccess()
    Dim POContext As New POContext
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim cust As Customer = DirectCast(Session("User"), Customer)
        If IsNothing(Session("User")) Then
            MsgBox("You do not have access to this page. Please login to Access.", , "Authorization Error")
            Response.Redirect("~/Login.aspx", True)
        ElseIf cust.firstName.ToUpper.CompareTo("Admin".ToUpper) <> 0 Then
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


    Private Sub ordGridView_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles ordGridView.RowDeleting
        Dim msg = "Are you sure you want to delete this Product?"
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






End Class