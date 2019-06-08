using Nonogram.Classes.Helper.Database;
using Nonogram.UserControls;
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
    /// PuzzleSelectPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PuzzleSelectPage : Page
    {
        public PuzzleSelectPage()
        {
            InitializeComponent();

            ServerPuzzleDB.DownloadPuzzle();
            PuzzleListBox.ItemsSource = LocalPuzzleDB.GetPuzzleDataList();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartingGamePage());
        }

        private void PuzzleButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PuzzleButton puzzleButton = sender as PuzzleButton;
            NavigationService.Navigate(new PuzzlePlay(puzzleButton.LastPuzzleData)); 
        }
    }
}
