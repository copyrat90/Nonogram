using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.Helper.Database
{
    public static class MSSQLLocal
    {
        readonly static string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=|DataDirectory|\\Nonogram.mdf;" +
            "Integrated Security=True";

        public static DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dt);

                conn.Close();
            }

            return dt;
        }


        public static void NonQueryCommand(string cmdStr)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand sqlComm = new SqlCommand(cmdStr, conn);
                sqlComm.ExecuteNonQuery();
            }
        }
    }
}
