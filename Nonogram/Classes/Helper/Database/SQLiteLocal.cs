using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.Helper.Database
{
    public static class SQLiteLocal
    {
        static string databaseName = "nonogram.db";
        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static readonly string fullDBPath = System.IO.Path.Combine(folderPath, databaseName);
        static readonly string connStr = $@"Data Source={fullDBPath};Pooling=true;FailIfMissing=false";

        public static DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                adapter.Fill(dt);

                conn.Close();
            }

            return dt;
        }


        public static void NonQueryCommand(string cmdStr)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                SQLiteCommand sqlComm = new SQLiteCommand(cmdStr, conn);
                sqlComm.ExecuteNonQuery();
            }
        }
    }
}
