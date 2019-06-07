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
    public static class LocalPuzzleDB
    {
        static string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=|DataDirectory|\\Nonogram.mdf;" +
            "Integrated Security=True";

        /// <summary>
        /// 로컬 데이터베이스에서 모든 퍼즐에 관한 정보를 불러와 리스트로 반환한다.
        /// </summary>
        /// <returns> 모든 퍼즐 버튼 데이터 리스트 (반환형 : List<PuzzleButtonData>)</returns>
        public static List<PuzzleButtonData> GetPuzzleButtonList()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM " +
                    "(SELECT * FROM PuzzleAnswer LEFT JOIN PausedPuzzleSave ON PuzzleAnswer.ID=PausedPuzzleSave.PuzzleID) " +
                    "LEFT JOIN PuzzleClear ON ID=PuzzleClear.PuzzleID;";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dataTable);

                conn.Close();
            }

            List<PuzzleButtonData> puzzleButtons = new List<PuzzleButtonData>();
            foreach (DataRow data in dataTable.Rows)
            {
                string[] curBoardStrings = null;
                if (data["curBoard0"] != null)
                    curBoardStrings = new string[5] { (string)data["CurBoard0"], (string)data["CurBoard1"], (string)data["CurBoard2"], (string)data["CurBoard3"], (string)data["CurBoard4"] };
                PuzzleButtonData puzzleButton
                    = new PuzzleButtonData((int)data["ID"], (string)data["Name"], (int)data["Height"], (int)data["Width"], (string)data["PuzzleRawString"], (bool?)data["IsCleared"], (int?)data["LastModifiedBoard"], curBoardStrings);
                puzzleButtons.Add(puzzleButton);
            }
            return puzzleButtons;
        }

        /// <summary>
        /// 퍼즐 중단 데이터를 데이터베이스에 저장한다.
        /// </summary>
        /// <param name="saveData">저장할 퍼즐 중단 데이터</param>
        public static void SavePausedPuzzleStatus(PausedPuzzleSaveData saveData)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                
                string boardDataStr = $" LastModifiedBoard={saveData.LastModifiedBoard}, " +
                    $"CurBoard0='{saveData.RawBoardStringArr[0]}', CurBoard1='{saveData.RawBoardStringArr[1]}', CurBoard2='{saveData.RawBoardStringArr[2]}', " +
                    $"CurBoard3='{saveData.RawBoardStringArr[3]}', CurBoard4='{saveData.RawBoardStringArr[4]}' ";

                string cmdStr = $"UPDATE PausedPuzzleSave SET {boardDataStr} " +
                    $"WHERE PuzzleID={saveData.PuzzleID}; " +
                    $"INSERT INTO PausedPuzzleSave (PuzzleID, LastModifiedBoard, CurBoard0, CurBoard1, CurBoard2, CurBoard3, CurBoard4) " +
                    $"SELECT PuzzleID={saveData.PuzzleID}, {boardDataStr} WHERE NOT EXISTS (SELECT PuzzleID FROM PausedPuzzleSave WHERE PuzzleID={saveData.PuzzleID});";
                // TSQL문장과 Connection 객체를 지정   
                SqlCommand sqlComm = new SqlCommand(cmdStr, conn);

                // 데이타는 서버에서 가져오도록 실행
                sqlComm.ExecuteNonQuery();
            }

        }

        /// <summary>
        /// 퍼즐 클리어 여부를 데이터베이스에 저장한다.
        /// </summary>
        /// <param name="puzzleID">퍼즐 고유 ID</param>
        /// <param name="isCleared">클리어 여부</param>
        public static void SavePuzzleClear(int puzzleID, bool isCleared)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string bit = (isCleared) ? "1" : "0";

                string cmdStr = $"UPDATE PuzzleClear SET IsCleared={bit} " +
                    $"WHERE PuzzleID={puzzleID}; " +
                    $"INSERT INTO PuzzleClear (PuzzleID, IsCleared) " +
                    $"SELECT PuzzleID={puzzleID}, IsCleared={bit} WHERE NOT EXISTS (SELECT PuzzleID FROM PuzzleClear WHERE PuzzleID={puzzleID});";
                // TSQL문장과 Connection 객체를 지정   
                SqlCommand sqlComm = new SqlCommand(cmdStr, conn);

                // 데이타는 서버에서 가져오도록 실행
                sqlComm.ExecuteNonQuery();
            }
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
