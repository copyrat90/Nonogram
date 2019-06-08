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

            //StackPanal에 풀던 퍼즐 보여주기
            if (data.PuzzleSave == null)
            {
                TextBlock nothingBlock = new TextBlock();
                nothingBlock.HorizontalAlignment = HorizontalAlignment.Center;
                nothingBlock.Text = "?";
                nothingBlock.FontSize = 150;
                nothingBlock.FontWeight = FontWeights.Bold;
                puzzleButtonData.PuzzledataLoadGrid.Children.Add(nothingBlock);
            }
            else
            {
                CellFill[,] userInputBoard = data.PuzzleSave.CurBoard[data.PuzzleSave.LastModifiedBoard];

                int height = data.Puzzle.Height;
                int width = data.Puzzle.Width;

                for (int i = 0; i < height; i++)
                {
                    StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                    puzzleButtonData.PuzzledataLoadGrid.Children.Add(stackPanel);

                    for (int j = 0; j < width; j++)
                    {
                        if (userInputBoard[i, j] == CellFill.FILL)
                        {
                            Rectangle rectangle = new Rectangle();
                            rectangle.Height = 220 / height;
                            rectangle.Width = 220 / width;
                            rectangle.Fill = new SolidColorBrush(Colors.Black);
                            stackPanel.Children.Add(rectangle);
                        }
                        else
                        {
                            Rectangle rectangle = new Rectangle();
                            rectangle.Height = 220 / height;
                            rectangle.Width = 220 / width;
                            rectangle.Fill = new SolidColorBrush(Colors.White);
                            stackPanel.Children.Add(rectangle);
                        }
                    }
                }
            }

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
