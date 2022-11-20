using System.Collections.Generic;
using Dapper;
using System.Configuration;
using System.Data.SQLite;
using System.Web;
using System;

namespace QPN.Database
{
    public class DatabaseService
    {
        private readonly static string _connectionString = ConfigurationManager.AppSettings["connectionString"];
        public static IEnumerable<T> Query<T>(string query, object param)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain; 
            currentDomain.SetData("DataDirectory", HttpContext.Current.Server.MapPath("~/Data"));
            
            using (var con = new SQLiteConnection(_connectionString))
            {                
                con.Open();

                return con.Query<T>(query, param);
            }
        }
    }
}