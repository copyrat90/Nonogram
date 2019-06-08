﻿using System;
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
            //NavigationService.Navigate(new PuzzlePlay());
        }
    }
}
