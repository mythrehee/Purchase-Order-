Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class migration1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            RenameColumn(table := "dbo.Orders", name := "cust_customerId", newName := "Customer_customerId")
            RenameIndex(table := "dbo.Orders", name := "IX_cust_customerId", newName := "IX_Customer_customerId")
            AddColumn("dbo.Orders", "cust", Function(c) c.Int(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Orders", "cust")
            RenameIndex(table := "dbo.Orders", name := "IX_Customer_customerId", newName := "IX_cust_customerId")
            RenameColumn(table := "dbo.Orders", name := "Customer_customerId", newName := "cust_customerId")
        End Sub
    End Class
End Namespace
