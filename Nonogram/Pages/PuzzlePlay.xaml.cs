using Nonogram.Classes.BoardVM;
using Nonogram.Classes.Helper;
using Nonogram.Classes.Helper.Database;
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
    /// PuzzlePlay.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PuzzlePlay : Page
    {
        int puzzleID;
        bool isRandomPuzzle;
        Board[] BoardArray { get; set; }
        public Board board_ClipBoard { get; set; } = null;

        /// <summary>
        /// 퍼즐 테스트용 기본 생성자(제거 요망)
        /// </summary>
        //public PuzzlePlay() : this(new PuzzleData(-1, true, "test", 5, 5, "1111100000010100000010101",
        //    null, 3, new string[5] { "1000002222000000000000000", "0000000022222000000000001", "2222222000011000000000000", "1000011000001110000011100", "0000000000000000000000000" } )) { }

        /// <summary>
        /// 퍼즐 데이터를 인자로 받아 5탭 퍼즐을 불러오는 생성자
        /// </summary>
        /// <param name="data">퍼즐 정답, 중단 데이터</param>
        public PuzzlePlay(PuzzleData data)
        {
            InitializeComponent();
            this.DataContext = this;

            puzzleID = data.Puzzle.PuzzleID;
            isRandomPuzzle = data.IsRandomPuzzle;

            boardTabControl.SelectedIndex = (data.PuzzleSave == null) ? 0 : data.PuzzleSave.LastModifiedBoard;

            BoardArray = PuzzleDataHelper.PuzzleDataToBoardArray(data);

            foreach (Board board in BoardArray)
            {
                board.PropertyChanged += Board_PropertyChanged;
            }

            nonoBoard0.GameBoard = BoardArray[0];
            nonoBoard1.GameBoard = BoardArray[1];
            nonoBoard2.GameBoard = BoardArray[2];
            nonoBoard3.GameBoard = BoardArray[3];
            nonoBoard4.GameBoard = BoardArray[4];

            pasteMenuItems = new MenuItem[5];
            pasteMenuItems[0] = pasteMenuItem0;
            pasteMenuItems[1] = pasteMenuItem1;
            pasteMenuItems[2] = pasteMenuItem2;
            pasteMenuItems[3] = pasteMenuItem3;
            pasteMenuItems[4] = pasteMenuItem4;

            mergeMenuItems = new MenuItem[5];
            mergeMenuItems[0] = mergeMenuItem0;
            mergeMenuItems[1] = mergeMenuItem1;
            mergeMenuItems[2] = mergeMenuItem2;
            mergeMenuItems[3] = mergeMenuItem3;
            mergeMenuItems[4] = mergeMenuItem4;

            //퍼즐 이름 받아오기
            PuzzleName.Text = data.Puzzle.Name;
        }

        /// <summary>
        /// 퍼즐을 풀었을 때 발동할 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Board_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSolved")
            {
                if (!isRandomPuzzle)
                {
                    LocalPuzzleDB.ErasePausedPuzzleStatus(this.puzzleID);
                    LocalPuzzleDB.SavePuzzleClear(this.puzzleID, true);
                }

                clearBoarder.Visibility = Visibility.Visible;
                clearTextBlock.Visibility = Visibility.Visible;
            }
        }

        public void SaveCurrentBoard()
        {
            PausedPuzzleSaveData saveData = PuzzleDataHelper.BoardArrayToPuzzleSaveData(puzzleID, boardTabControl.SelectedIndex, BoardArray);

            LocalPuzzleDB.SavePausedPuzzleStatus(saveData);
        }

        public void ResetCurrentBoard()
        {
            foreach (Board board in BoardArray)
            {
                board.ResetBoard();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!isRandomPuzzle)
            {
                SaveCurrentBoard();
                MessageBox.Show("저장이 완료되었습니다.");
            }
            else
            {
                MessageBox.Show("랜덤 퍼즐은 저장할 수 없습니다.");
            }
        }

        private void Zeroize_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("정말 모든 보드를 초기화하시겠습니까?\n저장되지 않은 데이터는 사라집니다!", "모든 퍼즐 초기화", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ResetCurrentBoard();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("정말 나가시겠습니까?\n저장되지 않은 데이터는 사라집니다!", "나가기", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new StartingGamePage());
            }
        }

        private void ClearBoarder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new StartingGamePage());
        }

        MenuItem[] pasteMenuItems;
        MenuItem[] mergeMenuItems;

        private void TabCopy_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = (MenuItem)sender;
            ContextMenu cmenu = (ContextMenu)(menu.Parent);
            int tabIdx = int.Parse((string)(cmenu.Tag));

            board_ClipBoard = BoardArray[tabIdx].Clone();

            // 붙여넣기, 병합 표시
            foreach (MenuItem p in pasteMenuItems)
            {
                p.Visibility = Visibility.Visible;
            }
            foreach (MenuItem m in mergeMenuItems)
            {
                m.Visibility = Visibility.Visible;
            }

            MessageBox.Show($"{tabIdx}번 탭이 복사되었습니다.");
        }

        private void TabPaste_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = (MenuItem)sender;
            ContextMenu cmenu = (ContextMenu)(menu.Parent);
            int tabIdx = int.Parse((string)(cmenu.Tag));

            if (MessageBox.Show($"정말 {tabIdx}번 탭에 붙여넣으시겠습니까?\n모든 칸이 덮어씌워집니다!","붙여넣기",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Board.CurrentBoardCopy(BoardArray[tabIdx], board_ClipBoard);
                MessageBox.Show($"{tabIdx}번 탭에 붙여넣었습니다.");
            }
        }

        private void TabMerge_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mmenu = (MenuItem)sender;
            MenuItem menu = (MenuItem)(mmenu.Parent);
            ContextMenu cmenu = (ContextMenu)(menu.Parent);
            int tabIdx = int.Parse((string)(cmenu.Tag));

            string modeStr = (string)mmenu.Tag;
            string modeText = (string)mmenu.Header;

            if (MessageBox.Show($"{tabIdx}번 탭에 [{modeText}]모드로 병합합니다.\n계속하시겠습니까?","병합",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                switch (modeStr)
                {
                    case "BLANK":
                        Board.CurrentBoardMerge(BoardArray[tabIdx], board_ClipBoard, Board.MergeMode.BLANK);
                        break;
                    case "KEEP_FILL":
                        Board.CurrentBoardMerge(BoardArray[tabIdx], board_ClipBoard, Board.MergeMode.KEEP_FILL);
                        break;
                    case "KEEP_ORIGINAL":
                        Board.CurrentBoardMerge(BoardArray[tabIdx], board_ClipBoard, Board.MergeMode.KEEP_ORIGINAL);
                        break;
                    case "OVERRIDE_WITH_NEW":
                        Board.CurrentBoardMerge(BoardArray[tabIdx], board_ClipBoard, Board.MergeMode.OVERRIDE_WITH_NEW);
                        break;
                    default:
                        throw new Exception("병합 모드 Tag 오류");
                }
                MessageBox.Show("병합이 완료되었습니다.");
            }
        }

        private void TabReset_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = (MenuItem)sender;
            ContextMenu cmenu = (ContextMenu)(menu.Parent);
            int tabIdx = int.Parse((string)(cmenu.Tag));

            if (MessageBox.Show($"정말 {tabIdx}번 보드를 초기화하시겠습니까?","보드 초기화", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BoardArray[tabIdx].ResetBoard();
            }
        }
    }
}
