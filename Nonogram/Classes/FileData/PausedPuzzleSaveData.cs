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
        int LastModifiedBoard { get; set; }

        /// <summary>
        /// 1차원 : 가정 레이어 탭 0~4
        /// 2차원 : y좌표
        /// 3차원 : x좌표
        /// </summary>
        CellFill[,,] CurBoard { get; set; }

        public PausedPuzzleSaveData(int lastModifiedBoard, CellFill[,,] curBoard)
        {
            LastModifiedBoard = lastModifiedBoard;
            CurBoard = curBoard;
        }
    }
}
