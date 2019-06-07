using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.Helper.Database
{
    public static class MariaDBServer
    {
        readonly static string connStr =
            "Server=203.254.143.200;" +
            "Port=18013;" +
            "Database=nonogram;" +
            "Uid=nonouser;" +
            "Pwd=longlivethenonodfs;";

        public static DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(dt);

                conn.Close();
            }

            return dt;
        }
    }
}
