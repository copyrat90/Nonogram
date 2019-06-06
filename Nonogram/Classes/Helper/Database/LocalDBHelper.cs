using Nonogram.Classes.FileData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.Helper.Database
{
    public class LocalDBHelper
    {
        static string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=D:\\Project\\C#\\Nonogram\\Nonogram\\LocalDB\\Nonogram.mdf;" +
            "Integrated Security=True";

        private PuzzleAnswerData[] GetPuzzleButtonList()
        {
            DataSet dataSet = new DataSet();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "select * from PuzzleAnswer;";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dataSet);

                conn.Close();
            }

            foreach (var data in dataSet.Tables[0].Rows)
            {
                
            }
            return dataTable;
        }

        private void SaveDataToDB()
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "insert into PuzzleAnswer(Name, Height, Width, PuzzleRawString)" +
                    $"values (N'{name}', {height}, {width}, '{puzzleRawString}');";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }

            UpdateDBView();
        }
    }
}
