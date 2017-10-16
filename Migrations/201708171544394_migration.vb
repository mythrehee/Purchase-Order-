Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class migration
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.Products", "order_orderId", "dbo.Orders")
            DropForeignKey("dbo.Orders", "customerId", "dbo.Customers")
            DropIndex("dbo.Orders", New String() { "customerId" })
            DropIndex("dbo.Products", New String() { "order_orderId" })
            RenameColumn(table := "dbo.Orders", name := "customerId", newName := "cust_customerId")
            CreateTable(
                "dbo.OrderLines",
                Function(c) New With
                    {
                        .orderLineID = c.Int(nullable := False, identity := True),
                        .order_orderId = c.Int(),
                        .product_productId = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.orderLineID) _
                .ForeignKey("dbo.Orders", Function(t) t.order_orderId) _
                .ForeignKey("dbo.Products", Function(t) t.product_productId) _
                .Index(Function(t) t.order_orderId) _
                .Index(Function(t) t.product_productId)
            
            AddColumn("dbo.Products", "quantityOrdered", Function(c) c.Int(nullable := False))
            AlterColumn("dbo.Orders", "cust_customerId", Function(c) c.Int())
            CreateIndex("dbo.Orders", "cust_customerId")
            AddForeignKey("dbo.Orders", "cust_customerId", "dbo.Customers", "customerId")
            DropColumn("dbo.Products", "order_orderId")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.Products", "order_orderId", Function(c) c.Int())
            DropForeignKey("dbo.Orders", "cust_customerId", "dbo.Customers")
            DropForeignKey("dbo.OrderLines", "product_productId", "dbo.Products")
            DropForeignKey("dbo.OrderLines", "order_orderId", "dbo.Orders")
            DropIndex("dbo.OrderLines", New String() { "product_productId" })
            DropIndex("dbo.OrderLines", New String() { "order_orderId" })
            DropIndex("dbo.Orders", New String() { "cust_customerId" })
            AlterColumn("dbo.Orders", "cust_customerId", Function(c) c.Int(nullable := False))
            DropColumn("dbo.Products", "quantityOrdered")
            DropTable("dbo.OrderLines")
            RenameColumn(table := "dbo.Orders", name := "cust_customerId", newName := "customerId")
            CreateIndex("dbo.Products", "order_orderId")
            CreateIndex("dbo.Orders", "customerId")
            AddForeignKey("dbo.Orders", "customerId", "dbo.Customers", "customerId", cascadeDelete := True)
            AddForeignKey("dbo.Products", "order_orderId", "dbo.Orders", "orderId")
        End Sub
    End Class
End Namespace
