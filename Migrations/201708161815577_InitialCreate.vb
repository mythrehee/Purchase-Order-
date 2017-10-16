Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitialCreate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Customers",
                Function(c) New With
                    {
                        .customerId = c.Int(nullable := False, identity := True),
                        .firstName = c.String(),
                        .lastName = c.String(),
                        .address = c.String(),
                        .creditLimit = c.Int(nullable := False),
                        .telephoneNumber = c.String(),
                        .faxNumber = c.String(),
                        .email = c.String(),
                        .User_userID = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.customerId) _
                .ForeignKey("dbo.Users", Function(t) t.User_userID, cascadeDelete := True) _
                .Index(Function(t) t.User_userID)
            
            CreateTable(
                "dbo.Orders",
                Function(c) New With
                    {
                        .orderId = c.Int(nullable := False, identity := True),
                        .orderDate = c.DateTime(nullable := False),
                        .customerId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.orderId) _
                .ForeignKey("dbo.Customers", Function(t) t.customerId, cascadeDelete := True) _
                .Index(Function(t) t.customerId)
            
            CreateTable(
                "dbo.Products",
                Function(c) New With
                    {
                        .productId = c.Int(nullable := False, identity := True),
                        .description = c.String(),
                        .price = c.Double(nullable := False),
                        .quantity = c.Int(nullable := False),
                        .reorderNumber = c.Int(nullable := False),
                        .order_orderId = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.productId) _
                .ForeignKey("dbo.Orders", Function(t) t.order_orderId) _
                .Index(Function(t) t.order_orderId)
            
            CreateTable(
                "dbo.Users",
                Function(c) New With
                    {
                        .userID = c.Int(nullable := False, identity := True),
                        .userName = c.String(),
                        .password = c.String()
                    }) _
                .PrimaryKey(Function(t) t.userID)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Customers", "User_userID", "dbo.Users")
            DropForeignKey("dbo.Products", "order_orderId", "dbo.Orders")
            DropForeignKey("dbo.Orders", "customerId", "dbo.Customers")
            DropIndex("dbo.Products", New String() { "order_orderId" })
            DropIndex("dbo.Orders", New String() { "customerId" })
            DropIndex("dbo.Customers", New String() { "User_userID" })
            DropTable("dbo.Users")
            DropTable("dbo.Products")
            DropTable("dbo.Orders")
            DropTable("dbo.Customers")
        End Sub
    End Class
End Namespace
