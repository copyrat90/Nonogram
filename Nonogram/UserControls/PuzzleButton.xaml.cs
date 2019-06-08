using Nonogram.Classes.BoardVM;
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

namespace Nonogram.UserControls
{
    /// <summary>
    /// PuzzleButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PuzzleButton : UserControl
    {


        public PuzzleData LastPuzzleData
        {
            get { return (PuzzleData)GetValue(LastPuzzleDataProperty); }
            set { SetValue(LastPuzzleDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastPuzzleData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastPuzzleDataProperty =
            DependencyProperty.Register("LastPuzzleData", typeof(PuzzleData), typeof(PuzzleButton), new PropertyMetadata(FillPuzzleButtonGrid));

        private static void FillPuzzleButtonGrid(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PuzzleButton puzzleButtonData = (PuzzleButton)source;

            PuzzleData data = (PuzzleData)e.NewValue;

            //Canvas에 풀던 퍼즐 보여주기

            //Canvas 격자 생성
            puzzleButtonData.PuzzleName.Height = data.Puzzle.Height;
            puzzleButtonData.PuzzleName.Width = data.Puzzle.Width;

            Rectangle rectangle = new Rectangle();
            rectangle.Height = puzzleButtonData.PuzzleName.Height;

            //Canvas 사각형으로 채우기


            //퍼즐 이름 보여주기
            puzzleButtonData.PuzzleName.Text = data.Puzzle.Name;
        }

        private void PuzzleGridload()
        {
            int height = LastPuzzleData.Puzzle.Height;
            int width = LastPuzzleData.Puzzle.Width;

            // height, width 개수로 Grid.ColumnDefinition, Grid.RowDefinition 개수 추가

            int lastTab = LastPuzzleData.PuzzleSave.LastModifiedBoard;
            CellFill[,] arr = LastPuzzleData.PuzzleSave.CurBoard[lastTab];

            // arr 바탕으로 Grid 에 사각형을 추가

        }

        public PuzzleButton()
        {
            InitializeComponent();
        }
    }
}
