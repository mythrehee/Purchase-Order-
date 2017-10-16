Imports System.IO

Public Class Customer1
    Inherits System.Web.UI.Page

    Dim DBAccess As New DBAccess()
    Dim POContext As New POContext

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
        custGridView.DataSource = POContext.customers.ToList()
        custGridView.DataBind()
    End Sub

    Private Sub custGridView_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles custGridView.RowDeleting
        Dim msg = "Are you sure you want to delete this Customer?"
        Dim title = "Confirmation Required"
        Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or
            MsgBoxStyle.Critical

        Dim response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then
            Dim custId As Integer
            custId = e.Keys("customerId")
            DBAccess.delCustomer(custId)
            loadData()
        Else
            MsgBox("Delete Canceled", style, title)
        End If
    End Sub


    Private Sub custGridView_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles custGridView.RowEditing
        custGridView.EditIndex = e.NewEditIndex
        loadData()
    End Sub


    Private Sub custGridView_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles custGridView.RowCancelingEdit
        custGridView.EditIndex = -1
        loadData()
    End Sub

    Private Sub custGridView_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles custGridView.RowUpdating
        Try
            Dim custID As Integer = custGridView.Rows(e.RowIndex).Cells(0).Text
            Dim fn As TextBox = custGridView.Rows(e.RowIndex).Cells(1).Controls(0)
            Dim ln As TextBox = custGridView.Rows(e.RowIndex).Cells(2).Controls(0)
            Dim addr As TextBox = custGridView.Rows(e.RowIndex).Cells(3).Controls(0)
            Dim cl As TextBox = custGridView.Rows(e.RowIndex).Cells(4).Controls(0)
            Dim fax As TextBox = custGridView.Rows(e.RowIndex).Cells(5).Controls(0)
            Dim tel As TextBox = custGridView.Rows(e.RowIndex).Cells(6).Controls(0)
            Dim em As TextBox = custGridView.Rows(e.RowIndex).Cells(7).Controls(0)

            custGridView.EditIndex = -1

            Dim cust As New Customer()
            cust.customerId = custID
            cust.firstName = fn.Text
            cust.lastName = ln.Text
            cust.address = addr.Text
            cust.email = em.Text
            cust.creditLimit = cl.Text
            cust.faxNumber = fax.Text
            cust.telephoneNumber = tel.Text
            DBAccess.updateCustomer(cust)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", POContext.customers.ToString, True)

            POContext.SaveChanges()
        Catch ex As Exception
            Dim strFile As String = "C:\ErrorLog_" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
            Dim sw As StreamWriter
            Try
                If (Not File.Exists(strFile)) Then
                    sw = File.CreateText(strFile)
                    sw.WriteLine("--Error Log--")
                Else
                    sw = File.AppendText(strFile)
                End If
                sw.WriteLine("Error Message in custGridView_RowUpdating Occured at-- " & DateTime.Now)
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