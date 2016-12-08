using System.Collections.Generic;
using System;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using aspQuoting.Models;

namespace DapperApp.Factory
{
    public class QuoteRepository : IFactory<Quote>
    {
        private string connectionString;
        public QuoteRepository()
        {
            connectionString = "server=54.213.234.106;userid=remote1;password=password;port=3306;database=quoteDB;SslMode=None";
        }

        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(connectionString);
            }
        }
    

        public void Add(Quote item)
        {
            using (IDbConnection dbConnection = Connection) {
                string query = $"INSERT INTO quotes (user, quote, likes, created_at, updated_at) VALUES ('{item.user}', '{item.quote}', 0, NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }
        public IEnumerable<Quote> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Quote>("SELECT * FROM quotes ORDER BY created_at DESC");
            }
        }
        public Quote FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Quote>("SELECT * FROM quotes WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void update(Quote current){
            using (IDbConnection dbConnection = Connection) {
                string query = $"UPDATE quotes SET likes={current.likes} WHERE id={current.id}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }
    }
}