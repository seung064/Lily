using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

namespace Project_Lily.LogInUI
{
    public static class DatabaseInitializer
    {
        private static readonly string DbFile = "UserDatabase.db";

        // DB 및 테이블 생성
        public static void CreateDatabaseAndTable()
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={DbFile};"))
                {
                    connection.Open();

                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        UserId TEXT NOT NULL UNIQUE,
                        Password TEXT NOT NULL,
                        Birth TEXT NOT NULL,
                        Username TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        UserType TEXT NOT NULL
                        );";

                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("DB 생성 실패: " + ex.Message);
            }
        }

        // 관리자 계정 추가
        public static void InsertAdminAccount()
        {
            try
            {
                using (var conn = new SQLiteConnection($"Data Source={DbFile};"))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE UserId = 'admin123'";
                    using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
                    {
                        long count = (long)checkCmd.ExecuteScalar();
                        if (count > 0) return; // 이미 있으면 추가 안 함
                    }

                    string insertAdmin = @"
                    INSERT INTO Users (Name, UserId, Password, Birth, Username, Email, UserType)
                    VALUES (@Name, @UserId, @Password, @Birth, @Username, @Email, 'admin');
                    ";

                    using (SQLiteCommand cmd = new SQLiteCommand(insertAdmin, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", "관리자");
                        cmd.Parameters.AddWithValue("@UserId", "admin123");
                        cmd.Parameters.AddWithValue("@Password", "admin123"); // 실제 배포 시 암호화 필요
                        cmd.Parameters.AddWithValue("@Birth", "1980-01-01");
                        cmd.Parameters.AddWithValue("@Username", "AdminUser");
                        cmd.Parameters.AddWithValue("@Email", "admin@example.com");

                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("관리자 계정 추가 실패: " + ex.Message);
            }
        }
    }
}



