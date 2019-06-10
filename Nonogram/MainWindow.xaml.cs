using Nonogram.Classes.Helper.Database;
using Nonogram.Classes.PuzzleModel;
using Nonogram.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nonogram
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LocalPuzzleDB.CreateTables();
            LocalPuzzleDB.InsertPuzzle(new PuzzleAnswerData(1, "음표", 5, 5, "0010000110001011110011100"));
            frame.NavigationService.Navigate(new StartupPage());
        }
    }
}
