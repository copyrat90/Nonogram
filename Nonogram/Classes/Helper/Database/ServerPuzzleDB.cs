using MySql.Data.MySqlClient;
using Nonogram.Classes.PuzzleModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.Helper.Database
{
    public static class ServerPuzzleDB
    {
        /// <summary>
        /// 퍼즐을 서버에서 다운로드해 로컬 데이터베이스에 저장하는 메서드
        /// </summary>
        public static void DownloadPuzzle()
        {
            // 모든 퍼즐을 서버에서 가져옴
            DataTable dt = MariaDBServer.GetDataTable($"SELECT * FROM PuzzleAnswer");

            // 각 행을 현재 데이터베이스에 삽입
            foreach (DataRow data in dt.Rows)
            {
                PuzzleAnswerData puzzle = new PuzzleAnswerData((int)data["ID"], (string)data["Name"], (int)data["Height"], (int)data["Width"], (string)data["PuzzleRawString"]);
                LocalPuzzleDB.InsertPuzzle(puzzle);
            }
        }
    }
}
