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
            this.GameBoard = new Board(new PuzzleAnswerData(6,8,
                "11101110" +
                "11011001" +
                "10101101" + 
                "11101111" +
                "00110101" +
                "11010010"));
        }
    }
}
