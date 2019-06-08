using Nonogram.Classes.BoardVM;
using Nonogram.Classes.PuzzleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.Helper
{
    public class PuzzleDataHelper
    {
        /// <summary>
        /// 퍼즐 데이터를 받아 5개짜리 보드 배열을 만든다.
        /// </summary>
        /// <param name="data">퍼즐 정답 및 중단 데이터</param>
        /// <returns>5개짜리 보드 배열</returns>
        public static Board[] PuzzleDataToBoardArray(PuzzleData data)
        {
            Board[] boardArray = new Board[5];
            for (int i = 0; i < 5; ++i)
            {
                boardArray[i] = new Board(data.Puzzle, data.PuzzleSave.CurBoard[i]);
            }

            return boardArray;
        }

        /// <summary>
        /// 5개짜리 보드 배열을 받아 퍼즐 데이터를 만든다.
        /// </summary>
        /// <param name="boardArr"></param>
        /// <returns>저장할 퍼즐 데이터</returns>
        public static PuzzleData BoardArrayToPuzzleData(Board[] boardArr)
        {
            throw new NotImplementedException("BoardArrayToPuzzleData() Is Not Implemented");


        }
    }
}
