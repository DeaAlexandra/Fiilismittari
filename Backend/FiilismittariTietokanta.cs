using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Data;

namespace Backend
{
    public class FiilismittariTietokanta : DbContext
    {
        private readonly string _connectionString = "Data Source=../Backend/backend.db";
        private readonly AppDbContext _context;

        public FiilismittariTietokanta(DbContextOptions<FiilismittariTietokanta> options)
            : base(options)
        {
            _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connectionString)
                .Options);
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            CreateTables(connection);
        }

        private static void CreateTables(SqliteConnection connection)
        {
            var createTableCmd = connection.CreateCommand();
            createTableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users(
                    Id INTEGER PRIMARY KEY,
                    FirstName TEXT,
                    LastName TEXT
                )";
            createTableCmd.ExecuteNonQuery();

            var createTableCmd2 = connection.CreateCommand();
            createTableCmd2.CommandText = @"
                CREATE TABLE IF NOT EXISTS UserDatas(
                    Id INTEGER PRIMARY KEY,
                    UserId INT,
                    Date TEXT,
                    Value INT,
                    FOREIGN KEY (UserId) REFERENCES Users(Id)
                )";
            createTableCmd2.ExecuteNonQuery();
        }

        // CRUD-metodit

        // Lisää käyttäjä
        public void AddUser(string firstName, string lastName)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = "INSERT INTO Users(FirstName, LastName) VALUES($firstName, $lastName)";
            insertCmd.Parameters.AddWithValue("$firstName", firstName);
            insertCmd.Parameters.AddWithValue("$lastName", lastName);
            insertCmd.ExecuteNonQuery();
        }

        // Lisää käyttäjän data
        public void AddUserData(int userId, DateTime date, int value)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = "INSERT INTO UserDatas(UserId, Date, Value) VALUES($userId, $date, $value)";
            insertCmd.Parameters.AddWithValue("$userId", userId);
            insertCmd.Parameters.AddWithValue("$date", date.ToString("yyyy-MM-dd"));
            insertCmd.Parameters.AddWithValue("$value", value);
            insertCmd.ExecuteNonQuery();
        }

        // Päivitä käyttäjän data
        public void UpdateUserData(int id, int value)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var updateCmd = connection.CreateCommand();
            updateCmd.CommandText = "UPDATE UserDatas SET Value = $value WHERE Id = $id";
            updateCmd.Parameters.AddWithValue("$value", value);
            updateCmd.Parameters.AddWithValue("$id", id);
            updateCmd.ExecuteNonQuery();
        }

        // Hae käyttäjän data käyttäjän ID:llä
        public string? GetUserDataByUserId(int userId)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT Value FROM UserDatas WHERE UserId = $userId";
            selectCmd.Parameters.AddWithValue("$userId", userId);

            using var reader = selectCmd.ExecuteReader();
            return reader.Read() ? reader.GetString(0) : null;
        }

        // Hae käyttäjän ID nimen perusteella
        public int? GetUserIdByName(string firstName, string lastName)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT Id FROM Users WHERE FirstName = $firstName AND LastName = $lastName";
            selectCmd.Parameters.AddWithValue("$firstName", firstName);
            selectCmd.Parameters.AddWithValue("$lastName", lastName);

            using var reader = selectCmd.ExecuteReader();
            return reader.Read() ? (int?)reader.GetInt32(0) : null;
        }

        // Hae kaikki käyttäjät Entity Frameworkin avulla
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        // Hae kaikki käyttäjän datat Entity Frameworkin avulla
        public List<UserData> GetAllUserDatas()
        {
            return _context.UserDatas.ToList();
        }
    }
}