using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Classes.FileData
{
    public class PuzzleAnswerData
    {
        public int Height { get { return AnswerArray.GetLength(0); } }
        public int Width { get { return AnswerArray.GetLength(1); } }

        public bool[,] AnswerArray { get; }

        public PuzzleAnswerData(string rawPuzzleString)
        {
            // TODO : 파싱으로 퍼즐 정답 데이터 생성
        }

        public bool this[int y, int x]
        {
            get { return AnswerArray[y, x]; }
        }
    }
}
