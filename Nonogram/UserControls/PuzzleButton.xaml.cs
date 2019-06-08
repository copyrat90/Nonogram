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
            DependencyProperty.Register("LastPuzzleData", typeof(PuzzleData), typeof(PuzzleButton), new PropertyMetadata(/*new PuzzleButtonData(), */FillPuzzleButtonGrid));

        private static void FillPuzzleButtonGrid(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();

            PuzzleButton puzzleButtonData = (PuzzleButton)source;
        }

        public PuzzleButton()
        {
            InitializeComponent();
        }
    }
}
