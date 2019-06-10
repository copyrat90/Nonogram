using MySql.Data.MySqlClient;
using Nonogram.Classes.Helper.Database;
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
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void StartingGameButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartingGamePage());
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
                downloadingTextBox.Visibility = Visibility.Visible;
                ServerPuzzleDB.DownloadPuzzle();
                MessageBox.Show("퍼즐 다운로드가 완료되었습니다.");
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show($"퍼즐 다운로드에 실패했습니다.\n\n{ex.GetType()}\n{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                downloadingTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
