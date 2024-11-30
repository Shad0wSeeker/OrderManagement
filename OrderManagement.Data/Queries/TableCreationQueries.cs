using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class TableCreationQueries
    {
        public static readonly string CreateUserTable = @"
        CREATE TABLE IF NOT EXISTS Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Username TEXT NOT NULL,
            Password TEXT NOT NULL,
            RoleId INTEGER NOT NULL,
            CreatedAt TEXT NOT NULL,
            LastLoginAt TEXT,
            FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE
        );";

        public static readonly string CreateOrderTable = @"
        CREATE TABLE IF NOT EXISTS Orders (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            UserId INTEGER NOT NULL,
            OrderStatus TEXT NOT NULL,
            TotalAmount REAL NOT NULL,
            CreatedAt TEXT NOT NULL,
            FOREIGN KEY (UserId) REFERENCES Users(Id)
        );";

        public static readonly string CreateOrderItemTable = @"
        CREATE TABLE IF NOT EXISTS OrderItems (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            OrderId INTEGER NOT NULL,
            ProductId INTEGER NOT NULL,
            Quantity INTEGER NOT NULL,
            Price REAL NOT NULL,
            FOREIGN KEY (OrderId) REFERENCES Orders(Id),
            FOREIGN KEY (ProductId) REFERENCES Products(Id)
        );";

        public static readonly string CreateProductTable = @"
        CREATE TABLE IF NOT EXISTS Products (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            Description TEXT,
            Price REAL NOT NULL,
            Stock INTEGER NOT NULL,
            CategoryId INTEGER NOT NULL, -- Связь с Categories
            FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
        );";

        public static readonly string CreateCategoryTable = @"
        CREATE TABLE IF NOT EXISTS Categories (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            ParentCategoryId INTEGER,
            FOREIGN KEY (ParentCategoryId) REFERENCES Categories(Id)
        );";

        public static readonly string CreateRoleTable = @"
        CREATE TABLE IF NOT EXISTS Roles (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL
        );";

        public static readonly string CreateAuditLogTable = @"
        CREATE TABLE IF NOT EXISTS AuditLogs (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            UserId INTEGER NOT NULL,
            Action TEXT NOT NULL,
            CreatedAt TEXT NOT NULL,
            FOREIGN KEY (UserId) REFERENCES Users(Id)
        );";

        public static readonly string CreatePaymentTable = @"
        CREATE TABLE IF NOT EXISTS Payments (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            OrderId INTEGER NOT NULL,
            PaymentStatus TEXT NOT NULL,
            PaymentMethod TEXT NOT NULL,
            CreatedAt TEXT NOT NULL,
            FOREIGN KEY (OrderId) REFERENCES Orders(Id)
        );";

        public static readonly string CreateDeliveryTable = @"
        CREATE TABLE IF NOT EXISTS Deliveries (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            OrderId INTEGER NOT NULL,
            DeliveryStatus TEXT NOT NULL,
            Address TEXT NOT NULL,
            DeliveryDate TEXT NOT NULL,
            FOREIGN KEY (OrderId) REFERENCES Orders(Id)
        );";

        public static readonly string CreateWishlistTable = @"
        CREATE TABLE IF NOT EXISTS Wishlists (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            UserId INTEGER NOT NULL,
            ProductId INTEGER NOT NULL,
            FOREIGN KEY (UserId) REFERENCES Users(Id),
            FOREIGN KEY (ProductId) REFERENCES Products(Id)
        );";

        public static readonly string CreateReviewTable = @"
        CREATE TABLE IF NOT EXISTS Reviews (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            UserId INTEGER NOT NULL,
            ProductId INTEGER NOT NULL,
            Rating INTEGER NOT NULL,
            Comment TEXT,
            CreatedAt TEXT NOT NULL,
            FOREIGN KEY (UserId) REFERENCES Users(Id),
            FOREIGN KEY (ProductId) REFERENCES Products(Id)
        );";

        public static readonly string CreateDiscountTable = @"
        CREATE TABLE IF NOT EXISTS Discounts (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            ProductId INTEGER NOT NULL,
            DiscountPercentage REAL NOT NULL,
            StartDate TEXT NOT NULL,
            EndDate TEXT NOT NULL,
            FOREIGN KEY (ProductId) REFERENCES Products(Id)
        );";

        public static readonly string CreateSupplierTable = @"
        CREATE TABLE IF NOT EXISTS Suppliers (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            ContactInfo TEXT NOT NULL
        );";
    }

}
