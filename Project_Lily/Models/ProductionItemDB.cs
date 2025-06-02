using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace Project_Lily.Models
{
    public static class ProductionItemDB
    {
        private static readonly string ProductionItemList = "ProductionItemDB.db"; // 데이터베이스 파일

        public static void CreateDatabaseAndTable()
        {
            try
            {

                using (var connection = new SQLiteConnection($"Data Source={ProductionItemList};"))
                {
                    connection.Open();

                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS ProductionItemDB (
                        ProductionItemNum INTEGER PRIMARY KEY AUTOINCREMENT,
                        ProductionItemName TEXT NOT NULL,
                        ProducedAt TEXT NOT NULL,
                        ProductionItemCount INTEGER NOT NULL
                        );";

                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();  // SQL 실행 (테이블 생성)
                    }

                    connection.Close();  // 연결 종료
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("DB 생성 실패: " + ex.Message);
            }
        }


        public static void InsertProduction(string ProductionItemName, DateTime ProducedAt, int ProductionItemCount)
        {

            using (var conn = new SQLiteConnection($"Data Source={ProductionItemList};"))
            {
                conn.Open();

                string insertQuery = @"
                INSERT INTO ProductionItemDB (ProductionItemName, ProducedAt, ProductionItemCount)
                VALUES (@ProductionItemName, @ProducedAt, @ProductionItemCount);";

                using (var cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductionItemName", ProductionItemName);
                    cmd.Parameters.AddWithValue("@ProducedAt", ProducedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@ProductionItemCount", ProductionItemCount);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }


        public static void DeleteProduction(string ProductionItemName)
        {
            using (var conn = new SQLiteConnection($"Data Source={ProductionItemList};"))
            {
                conn.Open();

                string deleteQuery = @"
                DELETE FROM ProductionItemDB
                WHERE ProductionItemName = @ProductionItemName;";

                using (var cmd = new SQLiteCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductionItemName", ProductionItemName);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
