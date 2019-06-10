using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.MyException
{
    /// <summary>
    /// 퍼즐 사이즈 관련 기본 예외
    /// </summary>
    public class PuzzleSizeException : Exception
    {
        public PuzzleSizeException()
        {
            Height = 0;
            Width = 0;
        }
        public PuzzleSizeException(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public int Height { get; }
        public int Width { get; }
        public override string Message
        {
            get
            {
                return $"Height : {Height}\nWidth : {Width}";
            }
        }
    }

    /// <summary>
    /// 퍼즐 문자열 길이와 (높이x너비)가 일치하지 않을 때 발생하는 예외
    /// </summary>
    public class PuzzleStringLengthMismatchException : PuzzleSizeException
    {
        public PuzzleStringLengthMismatchException() : base()
        {
            RawPuzzleString = String.Empty;
        }
        public PuzzleStringLengthMismatchException(int height, int width, string rawPuzzleString) : base(height, width)
        {
            RawPuzzleString = rawPuzzleString;
        }

        public string RawPuzzleString { get; }
        public override string Message
        {
            get
            {
                return $"Length of RawPuzzleString : {RawPuzzleString.Length}\n" +
                  $"Expected Length : {Height * Width} (h:{Height}, w:{Width})";
            }
        }
    }

    /// <summary>
    /// 퍼즐 크기가 너무 크거나 작을 때 발생하는 예외
    /// (한 쪽 라인이 100 초과 or 0 이하일 때)
    /// </summary>
    public class PuzzleSizeTooBigOrSmallException : PuzzleSizeException
    {
        public PuzzleSizeTooBigOrSmallException() : base() { }
        public PuzzleSizeTooBigOrSmallException(int height, int width) : base(height, width) { }
    }

    /// <summary>
    /// 깊은 복사를 하려는 퍼즐의 크기가 다를 때 발생하는 예외
    /// </summary>
    public class PuzzleSizeMismatchException : PuzzleSizeException
    {
        public int SourceHeight { get; set; }
        public int SourceWidth { get; set; }
        public PuzzleSizeMismatchException() : base()
        {
            SourceHeight = 0;
            SourceWidth = 0;
        }
        public PuzzleSizeMismatchException(int destHeight, int destWidth, int srcHeight, int srcWidth) : base(destHeight, destWidth)
        {
            SourceHeight = srcHeight;
            SourceWidth = srcWidth;
        }

        public override string Message
        {
            get
            {
                return $"DestHeight : {Height}\nDestWidth : {Width}\nSourceHeight : {SourceHeight}\nSourceWidth : {SourceWidth}";
            }
        }
    }
}
