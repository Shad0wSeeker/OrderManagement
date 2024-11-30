using OrderManagement.Data.Queries;
using System;
using System.Data.SQLite;

namespace OrderManagement.Data.Data
{
    public class DatabaseInitializer
    {
        private readonly SQLiteConnection _connection;

        public DatabaseInitializer(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Initialize()
        {
            Console.WriteLine("Initializing database...");
            try
            {
                // Открываем соединение с базой данных
                _connection.Open();
                Console.WriteLine("conn open");

                using var command = _connection.CreateCommand();

                // Собираем все SQL-запросы для создания таблиц
                command.CommandText = string.Join("\n",
                    TableCreationQueries.CreateUserTable,
                    TableCreationQueries.CreateOrderTable,
                    TableCreationQueries.CreateOrderItemTable,
                    TableCreationQueries.CreateProductTable,
                    TableCreationQueries.CreateCategoryTable,
                    TableCreationQueries.CreateRoleTable,
                    TableCreationQueries.CreateAuditLogTable,
                    TableCreationQueries.CreatePaymentTable,
                    TableCreationQueries.CreateDeliveryTable,
                    TableCreationQueries.CreateWishlistTable,
                    TableCreationQueries.CreateReviewTable,
                    TableCreationQueries.CreateDiscountTable,
                    TableCreationQueries.CreateSupplierTable);

                // Выполняем команду для создания таблиц
                command.ExecuteNonQuery();
                Console.WriteLine("Tables created successfully.");
            }
            catch (Exception ex)
            {
                // Логирование ошибки в консоль
                Console.WriteLine($"Error creating tables: {ex.Message}");
            }
            finally
            {
                // Закрытие соединения
                _connection.Close();
            }
        }
    }
}
