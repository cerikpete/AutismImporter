using System;
using System.IO;
using Importer.DTOs;

namespace SQLite
{
    public class DatabaseFactory
    {
        public SQLiteConnection InitDb()
        {
            var pathToDb = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "sessions.db");
            if (!File.Exists(pathToDb))
            {
                File.Copy("sessions.db", pathToDb);
            }

            var conn = new SQLiteConnection(pathToDb);

            conn.CreateTable<Task>();
            return conn;
        }
    }
}