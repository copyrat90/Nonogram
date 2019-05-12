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

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OptionPage());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO 나가기 구현
        }
    }
}
