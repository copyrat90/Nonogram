using Nonogram.Classes.BoardVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.PuzzleModel
{
    public class PuzzleData
    {
        public PuzzleAnswerData Puzzle { get; set; }
        public PausedPuzzleSaveData PuzzleSave { get; set; }
        public bool IsCleared { get; set; }
        public bool IsRandomPuzzle { get; set; }

        public PuzzleData(int puzzleID, bool isRandomPuzzle, string name, int height, int width, string rawPuzzleString, bool? isCleared,
            int? lastModifiedBoard, string[] curBoardStrings)
        {
            Puzzle = new PuzzleAnswerData(puzzleID, name, height, width, rawPuzzleString);
            IsRandomPuzzle = isRandomPuzzle;
            IsCleared = isCleared == true;

            if (curBoardStrings != null)
            {
                CellFill[][,] savedBoard = new CellFill[5][,];
                for (int b=0;b<5;++b)
                {
                    savedBoard[b] = new CellFill[height,width];

                    for (int y=0;y<height;++y)
                    {
                        for (int x=0;x<width;++x)
                        {
                            switch(curBoardStrings[b][y * width + x])
                            {
                                case '0':
                                    savedBoard[b][y, x] = CellFill.BLANK;
                                    break;
                                case '1':
                                    savedBoard[b][y, x] = CellFill.FILL;
                                    break;
                                case '2':
                                    savedBoard[b][y, x] = CellFill.X;
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
