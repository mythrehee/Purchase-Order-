Imports System.IO

Public Class Product1
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
        prodGridView.DataSource = POContext.products.ToList()
        prodGridView.DataBind() 'bind data to grid control view
    End Sub

    Private Sub prodGridView_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles prodGridView.RowCancelingEdit
        prodGridView.EditIndex = -1
        loadData()
    End Sub

    Private Sub prodGridView_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles prodGridView.RowDeleting
        Dim msg = "Are you sure you want to delete this Product?"
        Dim title = "Confirmation Required"
        Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or
            MsgBoxStyle.Critical

        Dim response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            Dim prodId As Integer
            prodId = e.Keys("productId")
            DBAccess.delProduct(prodId)
            loadData()
        Else
            MsgBox("Delete Canceled", style, title)
        End If

    End Sub

    Private Sub prodGridView_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles prodGridView.RowEditing
        prodGridView.EditIndex = e.NewEditIndex
        loadData()
    End Sub

    Private Sub prodGridView_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles prodGridView.RowUpdating
        Try

            Dim prodID As Integer = prodGridView.Rows(e.RowIndex).Cells(0).Text
            Dim desc As TextBox = prodGridView.Rows(e.RowIndex).Cells(1).Controls(0)
            Dim price As TextBox = prodGridView.Rows(e.RowIndex).Cells(2).Controls(0)
            Dim quantity As TextBox = prodGridView.Rows(e.RowIndex).Cells(3).Controls(0)
            Dim reorder As TextBox = prodGridView.Rows(e.RowIndex).Cells(4).Controls(0)

            prodGridView.EditIndex = -1

            Dim prod As New Product()
            prod.productId = prodID
            prod.description = desc.Text
            prod.price = Double.Parse(price.Text)
            prod.quantity = Integer.Parse(quantity.Text)
            prod.reorderNumber = Integer.Parse(reorder.Text)
            DBAccess.updateProduct(prod)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", POContext.products.ToString, True)
            POContext.SaveChanges()
        Catch ex As Exception
            MsgBox("An Error Occured", , "Exception Error")
            'create or append error log if exception occurs, to ensure that errors are not shown to user
            'but can be review later in errorlog file
            Dim strFile As String = "C:\ErrorLog_" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
            Dim sw As StreamWriter
            Try
                If (Not File.Exists(strFile)) Then
                    sw = File.CreateText(strFile)
                    sw.WriteLine("--Error Log--")
                Else
                    sw = File.AppendText(strFile)
                End If
                sw.WriteLine("Error Message in  Occured at-- " & DateTime.Now)
                sw.WriteLine("Error Details: ")
                sw.WriteLine(ex.ToString)
                sw.Close()
            Catch IOex As IOException
                MsgBox("Error writing to log file.")
            End Try
        Finally
            loadData()
        End Try
    End Sub

End Class