using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SQLite;


namespace MusicDataService.Model
{
    public static class Database
    {
        const string DB_NAME = "music.db";
        static string CONNECTION_STRING;

        static Database()
        {
            var db_path = HttpContext.Current.Server.MapPath($"~/App_Data/{DB_NAME}");
            CONNECTION_STRING = $"Data Source={db_path}";

        }

        public static SQLiteConnection GetSQLiteConnection()
        {
            return new SQLiteConnection(CONNECTION_STRING);
        }
    }
}