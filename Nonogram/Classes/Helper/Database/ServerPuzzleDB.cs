using MySql.Data.MySqlClient;
using Nonogram.Classes.PuzzleModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                int id = (int)data["ID"];
                string name = (string)data["Name"];
                int height = (int)data["Height"];
                int width = (int)data["Width"];
                string raw = (string)data["PuzzleRawString"];

                raw.Replace("\n", String.Empty);
                raw.Replace("\t", String.Empty);
                raw.Replace(" ", String.Empty);

                if (raw.Length != height * width)
                {
                    MessageBox.Show($"서버 퍼즐에 오류가 있어 다운로드를 스킵하였습니다.\n해당 퍼즐 = {id} : {name}\nstrLen == {raw.Length} != {width * height} == w({width}) * h({height})");
                    continue;
                }

                PuzzleAnswerData puzzle = new PuzzleAnswerData(id, name, height, width, raw);
                LocalPuzzleDB.InsertPuzzle(puzzle);
            }
        }
    }
}
