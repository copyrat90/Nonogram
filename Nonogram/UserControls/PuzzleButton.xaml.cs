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

            // 중단 데이터가 없을 때
            if (data.PuzzleSave == null)
            {
                // 퍼즐 클리어했으면 클리어 된 화면을 보여줌
                if (data.IsCleared)
                {
                    bool[,] userInputBoard = data.Puzzle.AnswerArray;

                    int height = data.Puzzle.Height;
                    int width = data.Puzzle.Width;

                    for (int i = 0; i < height; i++)
                    {
                        StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                        puzzleButtonData.PuzzledataLoadGrid.Children.Add(stackPanel);

                        for (int j = 0; j < width; j++)
                        {
                            if (userInputBoard[i, j])
                            {
                                Rectangle rectangle = new Rectangle();
                                rectangle.Height = 220 / height;
                                rectangle.Width = 220 / width;
                                rectangle.Fill = new SolidColorBrush(Colors.Purple);
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
                    //퍼즐 클리어했으면 퍼즐 크기 대신 클리어 축하
                    puzzleButtonData.PuzzleSize.Text = "Clear";
                    puzzleButtonData.PuzzleSize.Foreground = new SolidColorBrush(Colors.Red);
                }
                // 퍼즐 클리어 못 했으면 ? 를 보여줌
                else
                {
                    //퍼즐 크기 보여주기
                    int height = data.Puzzle.Height;
                    int width = data.Puzzle.Width;
                    puzzleButtonData.PuzzleSize.Text = height + " x " + width;

                    TextBlock nothingBlock = new TextBlock();
                    nothingBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    nothingBlock.Text = "?";
                    nothingBlock.FontSize = 150;
                    nothingBlock.FontWeight = FontWeights.Bold;
                    puzzleButtonData.PuzzledataLoadGrid.Children.Add(nothingBlock);
                }
            }
            // 중단 데이터가 있으면 마지막 화면을 보여줌
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
                            rectangle.Fill = new SolidColorBrush(Colors.CornflowerBlue);
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

                //퍼즐 사이즈 보여주기
                puzzleButtonData.PuzzleSize.Text = height + " x " + width;


                //퍼즐 클리어했으면 퍼즐 크기 대신 클리어 축하
                if (data.IsCleared)
                {
                    puzzleButtonData.PuzzleSize.Text = "Clear";
                    puzzleButtonData.PuzzleSize.Foreground = new SolidColorBrush(Colors.Red);
                }

            }

            //퍼즐 이름 보여주기
            puzzleButtonData.PuzzleName.Text = data.Puzzle.Name;
        }

        public PuzzleButton()
        {
            InitializeComponent();
        }
    }
}
