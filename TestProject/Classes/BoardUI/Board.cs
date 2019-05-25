using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestProject.Classes.FileData;

namespace TestProject.Classes.BoardUI
{
    public class Board : INotifyPropertyChanged
    {
        #region 보드판 2차원 컬렉션
        bool[,] AnswerArray { get; }
        ObservableCollection<ObservableCollection<Cell>> CurrentBoard { get; set; }
        #endregion

        /// <summary>
        /// 좌측 힌트들, 상단 힌트들을 TextBlock 의 2차원 컬렉션으로 관리
        /// 1차원 : 각 행(열)의 힌트 -> 2차원 : 각 숫자 힌트 (검은색, 흐린색)
        /// </summary>
        #region 힌트 2차원 컬렉션
        ObservableCollection<ObservableCollection<TextBlock>> LeftHintRows { get; set; }
        ObservableCollection<ObservableCollection<TextBlock>> UpperHintColumns { get; set; }
        #endregion


        public Board(PuzzleAnswerData puzzleAnswer)
        {
            AnswerArray = (bool[,])puzzleAnswer.AnswerArray.Clone();
            CurrentBoard = new ObservableCollection<ObservableCollection<Cell>>();
            // 퍼즐 크기에 맞게 보드판 생성
            for (int y = 0; y < puzzleAnswer.Height; ++y)
            {
                CurrentBoard[y] = new ObservableCollection<Cell>();
                for (int x = 0; x < puzzleAnswer.Width; ++x)
                {
                    // TODO : 중단된 퍼즐 불러오기 할 때 Fill 처리 추가
                    CurrentBoard[y][x] = new Cell(y, x);
                    // Cell 이 변경되면 Callback 될 이벤트 등록
                    CurrentBoard[y][x].PropertyChanged += DoWhenPropertyChanged;
                }
            }
            // 힌트 생성
            InitializeHint();
        }


        /// <summary>
        /// 색칠이 바뀌었을 때 Cell에서 수행할 이벤트
        /// 1. 힌트 텍스트 업데이트
        /// 2. 정답 검사 후 맞으면 정답 화면으로 넘어감
        /// </summary>
        /// <param name="sender">이벤트가 발생한 Cell 인스턴스</param>
        /// <param name="e">이벤트 정보</param>
        private void DoWhenPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell;

            if (e.PropertyName == "FillValue")
            {
                UpdateHint(cell.Y, cell.X);
                if (CheckSolved())
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSolved"));
            }
        }

        #region 힌트 초기화 및 업데이트
        /// <summary>
        /// 좌측, 상단 힌트 생성
        /// </summary>
        private void InitializeHint()
        {
            LeftHintRows = new ObservableCollection<ObservableCollection<TextBlock>>();
            UpperHintColumns = new ObservableCollection<ObservableCollection<TextBlock>>();

            // 좌측 힌트 초기화
            InitializeOneSide(true);
            // 상단 힌트 초기화
            InitializeOneSide(false);

            void InitializeOneSide(bool isLeftSide)
            {
                // 좌측 or 상단 라인 선택
                ObservableCollection<ObservableCollection<TextBlock>> hintLineCollection = null;
                int outerDimension = -1;
                int innerDimension = -1;
                if (isLeftSide)
                {
                    hintLineCollection = LeftHintRows;
                    outerDimension = 0;
                    innerDimension = 1;
                }
                else
                {
                    hintLineCollection = UpperHintColumns;
                    outerDimension = 1;
                    innerDimension = 0;
                }
                // 좌측 or 상단 힌트 초기화
                for (int i = 0; i < AnswerArray.GetLength(outerDimension); ++i)
                {
                    var oneLineHint = new ObservableCollection<TextBlock>();

                    // 한 칸씩 검사하며 연속된 칸의 개수를 구해 힌트 목록에 추가
                    bool prevIsFill = false;
                    int fillCount = 0;
                    for (int j = 0; j < AnswerArray.GetLength(innerDimension); ++j)
                    {
                        bool curIsFill = false;
                        if (isLeftSide)
                            curIsFill = AnswerArray[i, j];
                        else
                            curIsFill = AnswerArray[j, i];

                        if (curIsFill)
                        {
                            ++fillCount;
                        }
                        else if (prevIsFill)
                        {
                            oneLineHint.Add(new TextBlock() { Text = fillCount.ToString(), FontWeight = System.Windows.FontWeights.Bold });
                            fillCount = 0;
                        }
                        prevIsFill = curIsFill;
                    }
                    // 마지막 칸 채워져 있으면 마지막 힌트 추가
                    if (prevIsFill)
                    {
                        oneLineHint.Add(new TextBlock() { Text = fillCount.ToString(), FontWeight = System.Windows.FontWeights.Bold });
                    }

                    // 힌트 목록에 넣음
                    hintLineCollection.Add(oneLineHint);
                }
            }
        }


        /// <summary>
        /// 특정 Y, X 칸의 좌측, 상단 힌트 업데이트 (흐리게, 진하게)
        /// </summary>
        /// <param name="changed_Y"> 변경된 Cell의 y좌표 </param>
        /// <param name="changed_X"> 변경된 Cell의 x좌표 </param>
        private void UpdateHint(int changed_Y, int changed_X)
        {
            // TODO : 힌트 업데이트 기능 구현!
            throw new NotImplementedException("UpdateHint() Is Not Implemented");

            // 힌트 색깔 초기화
            foreach (var oneLineHint in LeftHintRows)
            {
                foreach (TextBlock numberTextBlock in oneLineHint)
                {
                    numberTextBlock.Foreground = System.Windows.Media.Brushes.Black;
                    numberTextBlock.FontWeight = System.Windows.FontWeights.Bold;
                }
            }

            // 좌측 힌트 업데이트
            for (int y = 0; y < AnswerArray.GetLength(0); ++y)
            {
                
            }

            // 상단 힌트 업데이트
            for (int x = 0; x < AnswerArray.GetLength(1); ++x)
            {

            }
        }
        #endregion

        /// <summary>
        /// 현재 보드판 색칠 상태가 정답 배열과 같은지 검사하는 메소드
        /// </summary>
        /// <returns>현재 보드판이 정답이면 true, 아니면 false</returns>
        public bool CheckSolved()
        {
            // TODO : 정답 체크 기능 구현!
            throw new NotImplementedException("CheckSolved() Is Not Implemented");
        }

        #region INotifyPropertyChanged 멤버
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
