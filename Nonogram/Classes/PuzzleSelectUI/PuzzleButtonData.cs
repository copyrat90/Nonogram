﻿using Nonogram.Classes.BoardUI;
using Nonogram.Classes.FileData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.PuzzleSelectUI
{
    public class PuzzleButtonData
    {
        public PuzzleData Puzzle { get; set; }
        public PausedPuzzleSaveData PuzzleSave { get; set; }
        public bool IsCleared { get; set; }

        public PuzzleButtonData(int puzzleID, string name, int height, int width, string rawPuzzleString, bool? isCleared,
            int? lastModifiedBoard, string[] curBoardStrings)
        {
            Puzzle = new PuzzleData(puzzleID, name, height, width, rawPuzzleString);

            IsCleared = isCleared == true;

            if (curBoardStrings != null)
            {
                CellFill[,,] savedBoard = new CellFill[5, height, width];
                for (int b=0;b<5;++b)
                {
                    for (int y=0;y<height;++y)
                    {
                        for (int x=0;x<width;++x)
                        {
                            switch(curBoardStrings[b][y * width + x])
                            {
                                case '0':
                                    savedBoard[b, y, x] = CellFill.BLANK;
                                    break;
                                case '1':
                                    savedBoard[b, y, x] = CellFill.FILL;
                                    break;
                                case '2':
                                    savedBoard[b, y, x] = CellFill.X;
                                    break;
                            }
                        }
                    }
                }

                PuzzleSave = new PausedPuzzleSaveData(puzzleID, (int)lastModifiedBoard, savedBoard);
            }
        }
    }
}
