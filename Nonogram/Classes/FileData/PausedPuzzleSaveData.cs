using Nonogram.Classes.BoardUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.FileData
{
    public class PausedPuzzleSaveData
    {
        public int PuzzleID { get; set; }
        public int LastModifiedBoard { get; set; }

        /// <summary>
        /// 1차원 : 가정 레이어 탭 0~4
        /// 2차원 : y좌표
        /// 3차원 : x좌표
        /// </summary>
        public CellFill[,,] CurBoard { get; set; }

        public string[] RawBoardStringArr
        {
            get
            {
                string[] rawArr = new string[5];
                for (int b = 0; b < 5; ++b)
                {
                    rawArr[b] = string.Empty;
                    for (int y = 0; y < CurBoard.GetLength(1); ++y)
                    {
                        for (int x = 0; x < CurBoard.GetLength(2); ++x)
                        {
                            string numStr = string.Empty;
                            switch(CurBoard[b, y, x])
                            {
                                case CellFill.BLANK: numStr = "0"; break;
                                case CellFill.FILL:  numStr = "1"; break;
                                case CellFill.X:     numStr = "2"; break;
                            }

                            rawArr[b] += numStr;
                        }
                    }
                }
                return rawArr;
            }
        }

        public PausedPuzzleSaveData(int puzzleID, int lastModifiedBoard, CellFill[,,] curBoard)
        {
            PuzzleID = puzzleID;
            LastModifiedBoard = lastModifiedBoard;
            CurBoard = curBoard;
        }
    }
}
