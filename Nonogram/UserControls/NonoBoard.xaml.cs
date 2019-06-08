using Nonogram.Classes.BoardVM;
using Nonogram.Classes.PuzzleModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// NonoBoard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NonoBoard : UserControl
    {
        public Board GameBoard
        {
            get { return this.DataContext as Board; }
            set { this.DataContext = value; }
        }

        public NonoBoard()
        {
            InitializeComponent();
        }

        private enum DragStatus { NONE, FILL, X, FILL_ERASE, X_ERASE }
        struct CellPos { public int y, x; }

        private DragStatus drag = DragStatus.NONE;
        private CellPos mouseDownStartCellPos;
        private bool isDraggedToNextCell;
        private bool dragX_fixY;

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = (Border)sender;
            Cell cell = (Cell)(((ContentControl)border.Child).Content);

            mouseDownStartCellPos.y = cell.Y;
            mouseDownStartCellPos.x = cell.X;

            switch (cell.FillValue)
            {
                case CellFill.BLANK:
                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        drag = DragStatus.FILL;
                        cell.FillValue = CellFill.FILL;
                    }
                    else if (Mouse.RightButton == MouseButtonState.Pressed)
                    {
                        drag = DragStatus.X;
                        cell.FillValue = CellFill.X;
                    }
                    break;

                case CellFill.FILL:
                    drag = DragStatus.FILL_ERASE;
                    cell.FillValue = CellFill.BLANK;
                    break;
                case CellFill.X:
                    drag = DragStatus.X_ERASE;
                    cell.FillValue = CellFill.BLANK;
                    break;
            }
            
            // 디버그용 콘솔 출력
            Console.WriteLine($"MouseDown on [y:{cell.Y}, x:{cell.X}]");
        }

        private void Border_MouseUp(object sender, MouseEventArgs e)
        {
            drag = DragStatus.NONE;
            isDraggedToNextCell = false;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            Cell cell = (Cell)(((ContentControl)border.Child).Content);

            // 드래그중이 아니라면 pass
            if (drag == DragStatus.NONE)
                return;

            // 만일 이미 드래그 한 적이 있다면
            // 시작 위치를 바탕으로 X축 or Y축만 변경한 셀을 재선택
            if (isDraggedToNextCell)
            {
                if (dragX_fixY)
                    cell = GameBoard.CurrentBoard[mouseDownStartCellPos.y][cell.X];
                else // dragY, fixX
                    cell = GameBoard.CurrentBoard[cell.Y][mouseDownStartCellPos.x];
            }
            // 드래그 한 적이 없으면 첫 드래그이므로
            // 시작 위치를 바탕으로 X축 이동인지 Y축 이동인지 입력
            else
            {
                isDraggedToNextCell = true;
                dragX_fixY = (cell.X != mouseDownStartCellPos.x);
            }

            switch (drag)
            {
                case DragStatus.FILL:
                    if (cell.FillValue == CellFill.BLANK)
                        cell.FillValue = CellFill.FILL;
                    break;
                case DragStatus.X:
                    if (cell.FillValue == CellFill.BLANK)
                        cell.FillValue = CellFill.X;
                    break;
                case DragStatus.FILL_ERASE:
                    if (cell.FillValue == CellFill.FILL)
                        cell.FillValue = CellFill.BLANK;
                    break;
                case DragStatus.X_ERASE:
                    if (cell.FillValue == CellFill.X)
                        cell.FillValue = CellFill.BLANK;
                    break;
            }

            // 디버그용 콘솔 출력
            Console.WriteLine($"MouseEnter in [y:{cell.Y}, x:{cell.X}] while dragging");
        }
    }
}
