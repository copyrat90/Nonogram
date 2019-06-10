using Nonogram.Classes.MyException;
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
    /// StartingGamePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StartingGamePage : Page
    {
        public StartingGamePage()
        {
            InitializeComponent();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void PuzzleSelectButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PuzzleSelectPage());
        }

        private void RandomPuzzleButton_Click(object sender, RoutedEventArgs e)
        {
            // 크기 지정 StackPanel 이 안 뜬 상태로 누르면 그게 뜬다.
            if (randomPuzzleSizeStackPanel.Visibility == Visibility.Collapsed)
            {
                randomPuzzleSizeStackPanel.Visibility = Visibility.Visible;
            }
            // 크기 지정 StackPanel 이 뜬 상태로 누르면 랜덤 퍼즐이 생성되어 넘어간다.
            else
            {
                try
                {
                    int width = int.Parse(randomWidthTextBox.Text);
                    int height = int.Parse(randomHeightTextBox.Text);

                    if (height > 50 || width > 50 || height < 0 || width < 0)
                        throw new PuzzleSizeTooBigOrSmallException(height, width);

                    string randomStr = string.Empty;
                    Random random = new Random();
                    for (int i = 0; i < height * width; ++i)
                    {
                        randomStr += (random.NextDouble() < 0.6) ? "1" : "0";
                    }

                    PuzzleData randomPuzzle = new PuzzleData(-1, true, "랜덤퍼즐", height, width, randomStr, null, null, null);
                    NavigationService.Navigate(new PuzzlePlay(randomPuzzle));
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("퍼즐 크기 문자열 형식이 잘못되었습니다.");
                }
                catch(PuzzleSizeTooBigOrSmallException ex)
                {
                    MessageBox.Show("행 또는 열의 길이는 1~50 사이여야 합니다.\n" + ex.Message);
                }
            }
        }
    }
}
