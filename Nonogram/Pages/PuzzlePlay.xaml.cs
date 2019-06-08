using Nonogram.Classes.BoardVM;
using Nonogram.Classes.Helper;
using Nonogram.Classes.PuzzleModel;
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

namespace Nonogram.Pages
{
    /// <summary>
    /// PuzzlePlay.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PuzzlePlay : Page
    {
        PuzzleData puzzleData;
        Board[] BoardArray { get; set; }

        /// <summary>
        /// 퍼즐 테스트용 기본 생성자(제거 요망)
        /// </summary>
        public PuzzlePlay() : this(new PuzzleData(-1, true, "test", 5, 5, "1111100000010100000010101",
            null, 3, new string[5] { "1000002222000000000000000", "0000000022222000000000001", "2222222000011000000000000", "1000011000001110000011100", "0000000000000000000000000" } )) { }

        /// <summary>
        /// 퍼즐 데이터를 인자로 받아 5탭 퍼즐을 불러오는 생성자
        /// </summary>
        /// <param name="data">퍼즐 정답, 중단 데이터</param>
        public PuzzlePlay(PuzzleData data)
        {
            InitializeComponent();

            this.puzzleData = data;
            BoardArray = PuzzleDataHelper.PuzzleDataToBoardArray(data);

            nonoBoard0.GameBoard = BoardArray[0];
            nonoBoard1.GameBoard = BoardArray[1];
            nonoBoard2.GameBoard = BoardArray[2];
            nonoBoard3.GameBoard = BoardArray[3];
            nonoBoard4.GameBoard = BoardArray[4];
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Zeroize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartingGamePage());
        }
    }
}
