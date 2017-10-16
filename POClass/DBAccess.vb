
Imports System.Data.Entity
Imports VBPOAssignment

Public Class DBAccess

    Dim POContext As New POContext

    Friend Sub addUser(userTemp As User)
        POContext.Users.Add(userTemp)
    End Sub
    Sub addCustomer(Custr As Customer)
        POContext.customers.Add(Custr)
        POContext.SaveChanges()
    End Sub

    Sub delCustomer(Custr As Integer)
        POContext.customers.Remove(POContext.customers.Find(Custr))
        POContext.SaveChanges()
    End Sub

    Sub updateCustomer(Custr As Customer)
        Dim updatecust As Customer = POContext.customers.Find(Custr.customerId)
        updatecust.firstName = Custr.firstName
        updatecust.lastName = Custr.lastName
        updatecust.address = Custr.address
        updatecust.creditLimit = Custr.creditLimit
        updatecust.faxNumber = Custr.faxNumber
        updatecust.telephoneNumber = Custr.telephoneNumber
        updatecust.email = Custr.email
        POContext.SaveChanges()
    End Sub

    Public Function findProduct(prodId As Integer) As Product
        Dim prodSelected As Product
        prodSelected = POContext.products.Find(prodId)
        Return prodSelected
    End Function

    Sub addProduct(product As Product)
        POContext.products.Add(product)
        POContext.SaveChanges()
    End Sub

    Sub delProduct(prod As Integer)
        POContext.products.Remove(POContext.products.Find(prod))
        POContext.SaveChanges()
    End Sub

    Sub updateProduct(product As Product)
        Dim updateprod As Product = POContext.products.Find(product.productId)
        updateprod.description = product.description
        updateprod.price = product.price
        updateprod.quantity = product.quantity
        updateprod.reorderNumber = product.reorderNumber
        POContext.SaveChanges()
    End Sub

    Sub updateOrderedProduct(Ord As Integer, prod As Product)
        Dim ordlines = From ordDS In POContext.orderLines.Include("Order").Include("Product")
                       Where ordDS.order.orderId = Ord
                       Select ordDS
        Dim change As Integer = 0
        For Each orline In ordlines.ToList
            If orline.product.productId = prod.productId Then
                Dim inventory As Product = POContext.products.Find(orline.product.productId)
                If orline.product.quantityOrdered > prod.quantityOrdered Then
                    change = orline.product.quantityOrdered - prod.quantityOrdered
                    inventory.quantity += change
                ElseIf prod.quantityOrdered > orline.product.quantityOrdered Then
                    change = prod.quantityOrdered - orline.product.quantityOrdered
                    inventory.quantity -= change
                End If
                orline.product.quantityOrdered = prod.quantityOrdered

            End If
        Next

        POContext.SaveChanges()
    End Sub
    Public Function loadProductlist() As List(Of Product)
        Return POContext.products.ToList
    End Function
    Public Function prodChangeWhenOrderPlaced(Prod As Product) As Boolean
        Dim quantityUpdated As Boolean
        Dim updateprod As Product = POContext.products.Find(Prod.productId)
        If (updateprod.quantity > Prod.quantityOrdered) Then
            Dim quantity As Integer = updateprod.quantity - Prod.quantityOrdered
            updateprod.quantity = quantity
            updateprod.quantityOrdered = Prod.quantityOrdered
            POContext.SaveChanges()
            quantityUpdated = True
        Else
            quantityUpdated = False
        End If
        Return quantityUpdated
    End Function

    Sub resetproductList()
        For Each prod In POContext.products.ToList
            prod.quantityOrdered = 0
        Next
    End Sub

    Sub confirmOrder(Order As Order)
        POContext.orders.Add(Order)
        'POContext.SaveChanges()
    End Sub
    Sub delOrder(Order As Integer)

        Dim ordlines = From ordDS In POContext.orderLines.Include("Order").Include("Product")
                       Where ordDS.order.orderId = Order
                       Select ordDS
        For Each ordline In ordlines.ToList
            prodChangeWhenOrderDeleted(ordline.product)
            POContext.orderLines.Remove(POContext.orderLines.Find(ordline.orderLineID))

        Next
        POContext.orders.Remove(POContext.orders.Find(Order))
        POContext.SaveChanges()
    End Sub

    Public Function addOrder(ord As Order, prodList As List(Of Product)) As List(Of Product)
        Dim checkupdate As Boolean
        Dim productswithquantityLessthanOrderedQuantity As New List(Of Product)
        POContext.orders.Add(ord)
        For Each prod In prodList
            checkupdate = prodChangeWhenOrderPlaced(prod)
            If checkupdate = False Then
                productswithquantityLessthanOrderedQuantity.Add(prod)
            Else
                Dim ordline As New OrderLine
                ordline.order = ord
                ordline.product = prod
                POContext.orderLines.Add(ordline)
            End If

        Next

        Return productswithquantityLessthanOrderedQuantity
    End Function

    Sub prodChangeWhenOrderDeleted(Prod As Product)
        Dim updateprod As Product = POContext.products.Find(Prod.productId)
        Dim quantity As Integer = updateprod.quantity + Prod.quantityOrdered
        updateprod.quantity = quantity
        POContext.SaveChanges()

    End Sub



    Public Function findProdListforOrder(ord As Integer) As List(Of Product)
        Dim ordline As New List(Of OrderLine)
        Dim prodordered As New List(Of Product)
        ordline = POContext.orderLines.Include("Order").Include("Product").ToList
        Dim ordlines = From ProdDS In ordline
                       Where ProdDS.order.orderId = ord
                       Select ProdDS

        For Each ords In ordlines.ToList
            prodordered.Add(ords.product)
        Next
        Return prodordered.ToList
    End Function

    Sub updateOrder(ord As Order, productschanged As List(Of Product))
        Dim prodorders As List(Of Product) = findProdListforOrder(ord.orderId)
        For Each prodchange In productschanged
            For Each prodordered In prodorders
                If prodchange.productId = prodordered.productId Then
                    If prodordered.quantityOrdered > prodchange.quantityOrdered Then
                        Dim change As Integer = prodordered.quantityOrdered - prodchange.quantityOrdered
                        Dim updateprod As Product = POContext.products.Find(prodordered.productId)
                        Dim quantity As Integer = updateprod.quantity + change
                        updateprod.quantity = quantity
                    ElseIf prodchange.quantityOrdered > prodordered.quantityOrdered Then
                        Dim change As Integer = prodchange.quantityOrdered - prodordered.quantityOrdered
                        Dim updateprod As Product = POContext.products.Find(prodordered.productId)
                        Dim quantity As Integer = updateprod.quantity - change
                        updateprod.quantity = quantity
                    End If

                    prodordered.quantityOrdered = prodchange.quantityOrdered

                End If
            Next
        Next

        POContext.SaveChanges()
    End Sub
    Public Function delProductfromorder(Ord As Integer, prodId As Integer) As Boolean
        Dim deleted As Boolean
        Dim ordlines = From ordDS In POContext.orderLines.Include("Order").Include("Product")
                       Where ordDS.order.orderId = Ord
                       Select ordDS
        Dim change As Integer = 0
        For Each orline In ordlines.ToList
            If orline.product.productId = prodId Then
                Dim inventory As Product = POContext.products.Find(orline.product.productId)
                inventory.quantity += orline.product.quantityOrdered
                POContext.orderLines.Remove(orline)
                deleted = True
            End If
        Next

        POContext.SaveChanges()



        Return deleted
    End Function
End Class
