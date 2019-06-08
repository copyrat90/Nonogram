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
        

        /// <summary>
        /// 로컬 데이터베이스에서 모든 퍼즐에 관한 정보를 불러와 리스트로 반환한다.
        /// </summary>
        /// <returns> 모든 퍼즐 버튼 데이터 리스트 (반환형 : List<PuzzleButtonData>)</returns>
        public static List<PuzzleButtonData> GetPuzzleButtonList()
        {
            string query = "SELECT * FROM PuzzleAnswer " +
                    "LEFT OUTER JOIN PausedPuzzleSave ON PuzzleAnswer.ID=PausedPuzzleSave.PuzzleID " +
                    "LEFT OUTER JOIN PuzzleClear ON ID=PuzzleClear.PuzzleID;";
            DataTable dataTable = MSSQLLocal.GetDataTable(query);

            List<PuzzleButtonData> puzzleButtons = new List<PuzzleButtonData>();
            foreach (DataRow data in dataTable.Rows)
            {
                string[] curBoardStrings = null;
                if (data["curBoard0"].ToString() != "")
                    curBoardStrings = new string[5] { data["CurBoard0"] as string, data["CurBoard1"] as string, data["CurBoard2"] as string, data["CurBoard3"] as string, data["CurBoard4"] as string };
                PuzzleButtonData puzzleButton
                    = new PuzzleButtonData((int)data["ID"], data["Name"] as string, (int)data["Height"], (int)data["Width"], data["PuzzleRawString"] as string, data["IsCleared"] as bool?, data["LastModifiedBoard"] as int?, curBoardStrings);
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
            string boardDataStr = $" LastModifiedBoard={saveData.LastModifiedBoard}, " +
                    $"CurBoard0='{saveData.RawBoardStringArr[0]}', CurBoard1='{saveData.RawBoardStringArr[1]}', CurBoard2='{saveData.RawBoardStringArr[2]}', " +
                    $"CurBoard3='{saveData.RawBoardStringArr[3]}', CurBoard4='{saveData.RawBoardStringArr[4]}' ";

            string cmdStr = $"UPDATE PausedPuzzleSave SET {boardDataStr} " +
                $"WHERE PuzzleID={saveData.PuzzleID}; " +
                $"INSERT INTO PausedPuzzleSave (PuzzleID, LastModifiedBoard, CurBoard0, CurBoard1, CurBoard2, CurBoard3, CurBoard4) " +
                $"SELECT PuzzleID={saveData.PuzzleID}, {boardDataStr} WHERE NOT EXISTS (SELECT PuzzleID FROM PausedPuzzleSave WHERE PuzzleID={saveData.PuzzleID});";

            MSSQLLocal.NonQueryCommand(cmdStr);
        }

        /// <summary>
        /// 퍼즐 클리어 여부를 데이터베이스에 저장한다.
        /// </summary>
        /// <param name="puzzleID">퍼즐 고유 ID</param>
        /// <param name="isCleared">클리어 여부</param>
        public static void SavePuzzleClear(int puzzleID, bool isCleared)
        {
            string bit = (isCleared) ? "1" : "0";

            string cmdStr = $"UPDATE PuzzleClear SET IsCleared={bit} " +
                $"WHERE PuzzleID={puzzleID}; " +
                $"INSERT INTO PuzzleClear (PuzzleID, IsCleared) " +
                $"SELECT PuzzleID={puzzleID}, IsCleared={bit} WHERE NOT EXISTS (SELECT PuzzleID FROM PuzzleClear WHERE PuzzleID={puzzleID});";

            MSSQLLocal.NonQueryCommand(cmdStr);
        }

        /// <summary>
        /// 새로운 퍼즐을 데이터베이스에 저장한다.
        /// </summary>
        /// <param name="puzzle">저장할 퍼즐</param>
        public static void InsertPuzzle(PuzzleData puzzle)
        {
            string cmdStr = "INSERT INTO PuzzleAnswer(ID, Name, Height, Width, PuzzleRawString) " +
                    $"values ({puzzle.PuzzleID}, N'{puzzle.Name}', {puzzle.Height}, {puzzle.Width}, N'{puzzle.RawPuzzleString}');";

            MSSQLLocal.NonQueryCommand(cmdStr);
        }
    }
}
