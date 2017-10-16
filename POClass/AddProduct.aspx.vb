Imports System.IO

Public Class AddProduct
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
    End Sub

    Protected Sub clearButton_Click(sender As Object, e As EventArgs) Handles clearButton.Click
        pDescBox.Text = ""
        pPriceBox.Text = ""
        pQtyBox.Text = ""
        pReordBox.Text = ""
    End Sub

    Protected Sub addButton_Click(sender As Object, e As EventArgs) Handles addButton.Click
        Try
            If Page.IsValid Then
                MsgBox("Page is Valid", , "Success")
                Dim prodTemp As New Product
                prodTemp.productId = 1 'id increments to next available id value
                prodTemp.description = pDescBox.Text
                prodTemp.reorderNumber = pReordBox.Text
                prodTemp.quantity = pQtyBox.Text
                prodTemp.price = pPriceBox.Text

                POContext.products.Add(prodTemp)
                POContext.SaveChanges()
                Response.Redirect("~/Product.aspx", True)
            End If
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
                sw.WriteLine("Error Message in AddButton - Product.aspx.vb Occured at-- " & DateTime.Now)
                sw.WriteLine("Error Details: ")
                sw.WriteLine(ex.ToString)
                sw.Close()
            Catch IOex As IOException
                MsgBox("Error writing to log file.")
            End Try
        End Try
    End Sub
End Class