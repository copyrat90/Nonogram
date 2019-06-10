﻿using Nonogram.Classes.MyException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.PuzzleModel
{
    public class PuzzleAnswerData
    {
        public int PuzzleID { get; set; }
        public string Name { get; set; }
        public int Height { get { return AnswerArray.GetLength(0); } }
        public int Width { get { return AnswerArray.GetLength(1); } }

        public bool[,] AnswerArray { get; }


        /// <summary>
        /// 퍼즐 높이, 너비, '0'과 '1'로 이루어진 퍼즐 데이터를 입력받아
        /// PuzzleAnswerData 객체를 생성하는 생성자
        /// </summary>
        /// <param name="height">퍼즐의 높이</param>
        /// <param name="width">퍼즐의 너비</param>
        /// <param name="rawPuzzleString">'0'과 '1'로 이루어진 퍼즐 데이터 문자열</param>
        /// <exception cref="PuzzleStringLengthMismatchException">
        /// 퍼즐 문자열 길이와 (높이x너비)가 일치하지 않을 때 발생하는 예외</exception>
        public PuzzleAnswerData(int puzzleID, string name, int height, int width, string rawPuzzleString)
        {
            PuzzleID = puzzleID;
            Name = name;

            rawPuzzleString.Replace("\n", String.Empty);
            rawPuzzleString.Replace("\t", String.Empty);
            rawPuzzleString.Replace(" ", String.Empty);

            if (rawPuzzleString.Length != (height * width))
                throw new PuzzleStringLengthMismatchException(height, width, rawPuzzleString);
            if (height > 50 || width > 50 || height <= 0 || width <= 0)
                throw new PuzzleSizeTooBigOrSmallException(height, width);

            AnswerArray = new bool[height, width];

            for (int y = 0; y < height; ++y)
                for (int x = 0; x < width; ++x)
                    AnswerArray[y, x] = (rawPuzzleString[y * width + x] != '0');
        }

        /// <summary>
        /// '0'과 '1'로 이루어진 퍼즐 데이터 문자열을 반환하는 get-only 속성
        /// </summary>
        public string RawPuzzleString
        {
            get
            {
                string raw = String.Empty;
                for (int y = 0; y < Height; ++y)
                    for (int x = 0; x < Width; ++x)
                        raw += (AnswerArray[y, x]) ? "1" : "0";

                return raw;
            }
        }

        public bool this[int y, int x]
        {
            get { return AnswerArray[y, x]; }
        }
    }
}
