using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Queries
{
    public static class SupplierQueries
    {
        public static string GetSupplierById = @"
            SELECT * FROM Suppliers WHERE Id = @Id;
        ";

        public static string GetAllSuppliers = @"
            SELECT * FROM Suppliers;
        ";

        public static string CreateSupplier = @"
            INSERT INTO Suppliers (Name, ContactInfo) 
            VALUES (@Name, @ContactInfo);
        ";

        public static string UpdateSupplier = @"
            UPDATE Suppliers 
            SET Name = @Name, ContactInfo = @ContactInfo 
            WHERE Id = @Id;
        ";

        public static string DeleteSupplier = @"
            DELETE FROM Suppliers WHERE Id = @Id;
        ";
    }
}
