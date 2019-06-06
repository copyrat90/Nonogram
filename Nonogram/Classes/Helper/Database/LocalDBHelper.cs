using Nonogram.Classes.FileData;
using Nonogram.Classes.PuzzleSelectUI;
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

        private List<PuzzleButton> GetPuzzleButtonList()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM " +
                    "(SELECT * FROM PuzzleAnswer INNER JOIN PausedPuzzleSave ON PuzzleAnswer.ID=PausedPuzzleSave.PuzzleID) " +
                    "LEFT JOIN PuzzleClear ON ID=PuzzleClear.PuzzleID;";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dataTable);

                conn.Close();
            }

            List<PuzzleButton> puzzleButtons = new List<PuzzleButton>();
            foreach (DataRow data in dataTable.Rows)
            {
                string[] curBoardStrings = new string[5] { (string)data["CurBoard0"], (string)data["CurBoard1"], (string)data["CurBoard2"], (string)data["CurBoard3"], (string)data["CurBoard4"] };
                PuzzleButton puzzleButton
                    = new PuzzleButton((string)data["Name"], (int)data["Height"], (int)data["Width"], (string)data["PuzzleRawString"], (bool)data["IsCleared"], (int)data["LastModifiedBoard"], curBoardStrings);
                puzzleButtons.Add(puzzleButton);
            }
            return puzzleButtons;
        }

        /*
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
        */
    }
}
