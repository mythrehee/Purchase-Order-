Public Class Login
    Inherits System.Web.UI.Page
    Dim DBAccess As New DBAccess()
    Dim POContext As New POContext

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
    End Sub
    'Login page verification
    Protected Sub validateUser(sender As Object, e As EventArgs)
        Dim u As String = Login1.UserName
        Dim p As String = Login1.Password
        If checkUser(u, p) Then
            MsgBox("Welcome " & u & "!", , "Login Sucessful")
            If u.ToUpper.CompareTo("Admin".ToUpper) = 0 Then
                Response.Redirect("~/products.aspx", True)
            Else
                Response.Redirect("~/Purchase_Order.aspx", True)
            End If
        Else
            MsgBox("Wrong password/username. Please register if you don't have an account.", , "Error")
        End If
    End Sub

    'verify if credentials passed in parameters is equal to any in POContext.users
    'create session to user's name
    Function checkUser(uname As String, pass As String) As Boolean
        Dim check As Boolean = False
        Dim name As String = ""
        Dim id As Integer = 0
        For Each u As User In POContext.Users
            'toUpper so that both are uppercase, so user can input username in any case
            If u.userName.ToUpper.CompareTo(uname.ToUpper) = 0 And u.password.CompareTo(pass) = 0 Then
                check = True
                id = u.userID
                For Each c As Customer In POContext.customers
                    If c.User.userID = id Then
                        Session("User") = c
                        Session("Name") = c.User.userName
                        Exit For
                    End If

                Next
                Exit For
            Else
                check = False
            End If
        Next

        Return check
    End Function
End Class