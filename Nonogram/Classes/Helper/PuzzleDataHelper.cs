﻿using Nonogram.Classes.BoardVM;
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
                boardArray[i] = new Board(data.Puzzle, data.PuzzleSave?.CurBoard[i]);
            }

            return boardArray;
        }

        /// <summary>
        /// 5개짜리 보드 배열을 받아 퍼즐 세이브 데이터를 만든다.
        /// </summary>
        /// <param name="boardArr"></param>
        /// <returns>저장할 퍼즐 세이브 데이터</returns>
        public static PausedPuzzleSaveData BoardArrayToPuzzleSaveData(int puzzleID, int lastModifiedBoard, Board[] boardArr)
        {
            int height = boardArr[0].CurrentBoard.Count;
            int width = boardArr[0].CurrentBoard[0].Count;

            CellFill[][,] curBoard = new CellFill[5][,];
            for (int b = 0; b < 5; ++b)
            {
                curBoard[b] = new CellFill[height, width];
                for (int y = 0; y < height; ++y)
                {
                    for (int x=0; x < width; ++x)
                    {
                        curBoard[b][y,x] = boardArr[b].CurrentBoard[y][x].FillValue;
                    }
                }
            }

            return new PausedPuzzleSaveData(puzzleID, lastModifiedBoard, curBoard);
        }
    }
}
