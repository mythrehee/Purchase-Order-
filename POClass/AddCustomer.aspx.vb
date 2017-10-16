Public Class AddCustomer
    Inherits System.Web.UI.Page

    Dim DBAccess As New DBAccess()
    Dim POContext As New POContext
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cust As Customer = DirectCast(Session("User"), Customer)
        If cust.firstName.ToUpper.CompareTo("Admin".ToUpper) = 0 Then
        Else
            MsgBox("You do not have access to this page. Please login to Access.", , "Authorization Error")
            Response.Redirect("~/Login.aspx", True)
        End If
    End Sub

    'clears fields when user clicks button
    Protected Sub clearButton_Click(sender As Object, e As EventArgs) Handles clearButton.Click
        CType(custFnBox, TextBox).Text = String.Empty
        ' custFnBox.Text = ""
        custLnBox.Text = String.Empty
        custAddr.Text = String.Empty
        emailBox.Text = String.Empty
        ccBox.Text = String.Empty
        faxBox.Text = String.Empty
        telBox.Text = String.Empty
        passBox.Text = String.Empty
        userBox.Text = String.Empty
    End Sub

    Function validUser(vUser As String) As Boolean
        Dim check As Boolean = True

        For Each u As User In POContext.Users
            If vUser.CompareTo(u.userName) = 0 Then
                check = False
                MsgBox("Username already taken. Please try again.", , "Error")
                Exit For
            Else
            End If
        Next
        Return check
    End Function

    Protected Sub addButton_Click(sender As Object, e As EventArgs) Handles addButton.Click
        If Page.IsValid Then
            If validUser(userBox.Text) Then
                'MsgBox("Page is Valid", , "Success")
                Dim custTemp As New Customer
                Dim userTemp As New User
                userTemp.userID = 1  'id incremenets to the next available id value
                userTemp.password = passBox.Text
                userTemp.userName = userBox.Text
                DBAccess.addUser(userTemp)
                custTemp.customerId = 1 'id increments to next available id value
                custTemp.firstName = custFnBox.Text
                custTemp.lastName = custLnBox.Text
                custTemp.address = custAddr.Text
                custTemp.email = emailBox.Text
                custTemp.creditLimit = ccBox.Text
                custTemp.faxNumber = faxBox.Text
                custTemp.telephoneNumber = telBox.Text
                custTemp.User = userTemp
                DBAccess.addCustomer(custTemp)
                POContext.SaveChanges()
                MsgBox("Registration completed. Please login now!", , "Success")
                Response.Redirect("~/Login.aspx", True)
            End If
        End If
    End Sub


End Class