Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class User
    Public Property userID As Integer
    Public Property userName As String
    Public Property password As String

End Class
Public Class Customer
    <Key()>
    Public Property customerId As Integer
    Public Property firstName As String
    Public Property lastName As String
    Public Property address As String
    Public Property creditLimit As Integer
    Public Property telephoneNumber As String
    Public Property faxNumber As String
    Public Property email As String
    Public Property orders As ICollection(Of Order)
    <Required>
    Public Property User As User
End Class

Public Class Product
    <Key()>
    Public Property productId As Integer
    Public Property description As String
    Public Property price As Double
    Public Property quantity As Integer
    Public Property quantityOrdered As Integer
    Public Property reorderNumber As Integer
    Public Property orderLines As ICollection(Of OrderLine)

End Class

Public Class Order
    <Key()>
    Public Property orderId As Integer
    Public Property orderDate As Date
    Public Property cust As Integer
    Public Property orderLines As ICollection(Of OrderLine)

End Class

Public Class OrderLine
    <Key()>
    Public Property orderLineID As Integer
    Public Property order As Order
    Public Property product As Product
End Class
Public Class POContext
    Inherits DbContext
    Public Property orders As DbSet(Of Order)
    Public Property products As DbSet(Of Product)
    Public Property customers As DbSet(Of Customer)
    Public Property orderLines As DbSet(Of OrderLine)
    Public Property Users As DbSet(Of User)

End Class



