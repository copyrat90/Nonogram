using Nonogram.Classes.PuzzleModel;
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
        /// <summary>
        /// 로컬 데이터베이스에서 모든 퍼즐에 관한 정보를 불러와 리스트로 반환한다.
        /// </summary>
        /// <returns> 모든 퍼즐 데이터 리스트 (반환형 : List<PuzzleData>)</returns>
        public static List<PuzzleData> GetPuzzleDataList()
        {
            string query = "SELECT * FROM PuzzleAnswer " +
                    "LEFT OUTER JOIN PausedPuzzleSave ON PuzzleAnswer.ID=PausedPuzzleSave.PuzzleID " +
                    "LEFT OUTER JOIN PuzzleClear ON ID=PuzzleClear.PuzzleID;";
            DataTable dataTable = SQLiteLocal.GetDataTable(query);

            List<PuzzleData> puzzleButtons = new List<PuzzleData>();
            foreach (DataRow data in dataTable.Rows)
            {
                string[] curBoardStrings = null;
                if (data["curBoard0"].ToString() != "")
                    curBoardStrings = new string[5] { data["CurBoard0"] as string, data["CurBoard1"] as string, data["CurBoard2"] as string, data["CurBoard3"] as string, data["CurBoard4"] as string };
                PuzzleData puzzleButton
                    = new PuzzleData((int)data["ID"], false, data["Name"] as string, (int)data["Height"], (int)data["Width"], data["PuzzleRawString"] as string, data["IsCleared"] as int?, data["LastModifiedBoard"] as int?, curBoardStrings);
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
            string cmdStr = $"INSERT OR REPLACE INTO PausedPuzzleSave (PuzzleID, LastModifiedBoard, CurBoard0, CurBoard1, CurBoard2, CurBoard3, CurBoard4) " +
                $"VALUES ({saveData.PuzzleID}, {saveData.LastModifiedBoard}, '{saveData.RawBoardStringArr[0]}', '{saveData.RawBoardStringArr[1]}', " +
                $"'{saveData.RawBoardStringArr[2]}', '{saveData.RawBoardStringArr[3]}', '{saveData.RawBoardStringArr[4]}');";

            SQLiteLocal.NonQueryCommand(cmdStr);
        }

        /// <summary>
        /// 퍼즐 중단 데이터를 데이터베이스에서 삭제한다.
        /// </summary>
        /// <param name="puzzleID">삭제할 퍼즐 중단 데이터의 PuzzleID</param>
        public static void ErasePausedPuzzleStatus(int puzzleID)
        {
            string cmdStr = $"DELETE FROM PausedPuzzleSave WHERE PuzzleID={puzzleID};";

            SQLiteLocal.NonQueryCommand(cmdStr);
        }

        /// <summary>
        /// 퍼즐 클리어 여부를 데이터베이스에 저장한다.
        /// </summary>
        /// <param name="puzzleID">퍼즐 고유 ID</param>
        /// <param name="isCleared">클리어 여부</param>
        public static void SavePuzzleClear(int puzzleID, bool isCleared)
        {
            string bit = (isCleared) ? "1" : "0";

            string cmdStr = $"INSERT OR REPLACE INTO PuzzleClear (PuzzleID, IsCleared) VALUES ({puzzleID}, {bit});";

            SQLiteLocal.NonQueryCommand(cmdStr);
        }

        /// <summary>
        /// 새로운 퍼즐을 데이터베이스에 저장한다.
        /// </summary>
        /// <param name="puzzle">저장할 퍼즐</param>
        public static void InsertPuzzle(PuzzleAnswerData puzzle)
        {
            string cmdStr = "INSERT OR REPLACE INTO PuzzleAnswer(ID, Name, Height, Width, PuzzleRawString) " +
                    $"VALUES ({puzzle.PuzzleID}, '{puzzle.Name}', {puzzle.Height}, {puzzle.Width}, '{puzzle.RawPuzzleString}');";

            SQLiteLocal.NonQueryCommand(cmdStr);
        }

        /// <summary>
        /// 테이블을 생성한다.
        /// </summary>
        public static void CreateTables()
        {
            string cmdStr = "CREATE TABLE IF NOT EXISTS PuzzleAnswer (ID INT PRIMARY KEY, Name NVARCHAR(255), Height INT, Width INT, PuzzleRawString NVARCHAR(10000));" +
                "CREATE TABLE IF NOT EXISTS PausedPuzzleSave (PuzzleID INT PRIMARY KEY, LastModifiedBoard INT, CurBoard0 NVARCHAR(10000), CurBoard1 NVARCHAR(10000), " +
                "CurBoard2 NVARCHAR(10000), CurBoard3 NVARCHAR(10000), CurBoard4 NVARCHAR(10000));" +
                "CREATE TABLE IF NOT EXISTS PuzzleClear (PuzzleID INT PRIMARY KEY, IsCleared INT);";

            SQLiteLocal.NonQueryCommand(cmdStr);
        }
    }
}
