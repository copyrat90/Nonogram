using Nonogram.Classes.BoardUI;
using Nonogram.Classes.FileData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nonogram.UserControls
{
    /// <summary>
    /// NonoBoard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NonoBoard : UserControl
    {
        public Board GameBoard
        {
            get { return this.DataContext as Board; }
            set { this.DataContext = value; }
        }

        public NonoBoard()
        {
            InitializeComponent();
            //this.GameBoard = new Board(puzzleAnswer);

            // 테스트용 퍼즐
            this.GameBoard = new Board(new PuzzleAnswerData(15,15,
                "000000100011110" +
                "001111100110011" +
                "011111001110111" +
                "111011001101001" +
                "111111001101011" +
                "011111101110111" +
                "000111101110111" +
                "001101111111101" +
                "011011111111101" +
                "000111111111010" +
                "001111111111010" +
                "001111111110100" +
                "000101111001000" +
                "000010111110000" +
                "000111111000000"));
        }

        private bool isLeftDragging;
        private bool isRightDragging;

        private void Border_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // TODO
        }

        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            Cell cell = (Cell)(((ContentControl)border.Child).Content);

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                cell.FillValue = CellFill.FILL;
            }
            else if (Mouse.RightButton == MouseButtonState.Pressed)
            {
                cell.FillValue = CellFill.X;
            }
        }

        private void Border_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // TODO
        }
    }
}
