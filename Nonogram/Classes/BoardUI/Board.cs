using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Nonogram.Classes.FileData;
using TestProject.Classes.BoardUI;

namespace Nonogram.Classes.BoardUI
{
    public class Board : INotifyPropertyChanged
    {
        private bool isSolved;
        public bool IsSolved
        {
            get { return isSolved; }
            set
            {
                if (isSolved != value)
                {
                    isSolved = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSolved"));
                }
            }
        }

        #region 보드판 2차원 컬렉션
        bool[,] AnswerArray { get; }
        public ObservableCollection<ObservableCollection<Cell>> CurrentBoard { get; set; }
        #endregion

        /// <summary>
        /// 좌측 힌트들, 상단 힌트들을 2차원 컬렉션으로 관리
        /// 1차원 : 각 행(열)의 힌트 -> 2차원 : 각 숫자 힌트 (검은색, 흐린색)
        /// </summary>
        #region 힌트 2차원 컬렉션
        public ObservableCollection<ObservableCollection<HintNum>> LeftHintRows { get; set; }
        public ObservableCollection<ObservableCollection<HintNum>> UpperHintColumns { get; set; }
        #endregion


        public Board(PuzzleData puzzleAnswer)
        {
            AnswerArray = (bool[,])puzzleAnswer.AnswerArray.Clone();
            CurrentBoard = new ObservableCollection<ObservableCollection<Cell>>();
            // 퍼즐 크기에 맞게 보드판 생성
            for (int y = 0; y < puzzleAnswer.Height; ++y)
            {
                CurrentBoard.Add(new ObservableCollection<Cell>());
                for (int x = 0; x < puzzleAnswer.Width; ++x)
                {
                    // TODO : 중단된 퍼즐 불러오기 할 때 Fill 처리 추가
                    CurrentBoard[y].Add(new Cell(y, x));
                    // Cell 이 변경되면 Callback 될 이벤트 등록
                    CurrentBoard[y][x].PropertyChanged += DoWhenCellPropertyChanged;
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
        private void DoWhenCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell;

            if (e.PropertyName == "FillValue")
            {
                UpdateHint(cell.Y, cell.X);
                this.IsSolved = CheckSolved();
            }
        }

        #region 힌트 초기화 및 업데이트
        /// <summary>
        /// 좌측, 상단 힌트 생성
        /// </summary>
        private void InitializeHint()
        {
            LeftHintRows = new ObservableCollection<ObservableCollection<HintNum>>();
            UpperHintColumns = new ObservableCollection<ObservableCollection<HintNum>>();

            // 좌측 힌트 초기화
            InitializeOneSide(true);
            // 상단 힌트 초기화
            InitializeOneSide(false);

            void InitializeOneSide(bool isLeftSide)
            {
                // 좌측 or 상단 라인 선택
                ObservableCollection<ObservableCollection<HintNum>> hintLineCollection = null;
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
                    var oneLineHint = new ObservableCollection<HintNum>();

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
                            oneLineHint.Add(new HintNum(fillCount));
                            fillCount = 0;
                        }
                        prevIsFill = curIsFill;
                    }
                    // 마지막 칸 채워져 있으면 마지막 힌트 추가
                    if (prevIsFill)
                    {
                        oneLineHint.Add(new HintNum(fillCount));
                    }

                    // 만약 칠이 없는 칸이면 0을 추가
                    if (oneLineHint.Count == 0)
                        oneLineHint.Add(new HintNum(0) { IsUsed = true });

                    // 힌트 목록에 넣음
                    hintLineCollection.Add(oneLineHint);
                }
            }
        }


        /// <summary>
        /// 특정 Y, X 칸(Cell)의 좌측, 상단 힌트 업데이트 (흐리게, 진하게)
        /// </summary>
        /// <param name="changed_Y"> 변경된 Cell의 y좌표 </param>
        /// <param name="changed_X"> 변경된 Cell의 x좌표 </param>
        private void UpdateHint(int changed_Y, int changed_X)
        {
            // TODO : 힌트 업데이트 기능 구현!
            //throw new NotImplementedException("UpdateHint() Is Not Implemented");
            UpdateLeftHint();
            UpdateUpperHint();

            #region 좌측, 상단 힌트 업데이트 기능
            void UpdateLeftHint()
            {
                var oneLineHint = LeftHintRows[changed_Y];
                // 힌트가 '0' 한 개이면 아무 작업도 안 하고 리턴
                if (oneLineHint.Count == 1 && oneLineHint[0].Num == 0)
                    return;

                // 힌트 색깔 초기화
                foreach (HintNum oneHintNum in oneLineHint)
                {
                    oneHintNum.IsUsed = false;
                }

                // 모든 힌트 숫자를 순서대로 칠했으면,
                // 즉, 줄이 완성되었으면, X와 관계없이 전부 흐린색 처리
                bool lineIsCandidate = true;
                bool prevIsFill = false;
                int fillCount = 0;  // 연속된 블록 덩어리 수
                int hintIdx = 0;    // 현재 검사중인 힌트 숫자의 인덱스
                int hintNum = 0;    // 현재 검사중인 힌트 숫자
                int lastCount = 0;
                for (int x = 0; x < CurrentBoard[0].Count; ++x)
                {
                    bool curIsFill = (CurrentBoard[changed_Y][x].FillValue == CellFill.FILL);
                    hintNum = LeftHintRows[changed_Y][hintIdx].Num;
                    if (curIsFill)
                    {
                        ++fillCount;
                        if (fillCount > hintNum)
                        {
                            lineIsCandidate = false;
                            break;
                        }
                    }
                    // prev == T, cur == F
                    else if (prevIsFill)
                    {
                        if (fillCount != hintNum)
                        {
                            lineIsCandidate = false;
                            break;
                        }
                        lastCount = fillCount;
                        fillCount = 0;
                        ++hintIdx;
                        if (hintIdx >= LeftHintRows[changed_Y].Count)
                        {
                            prevIsFill = curIsFill;
                            break;
                        }
                    }

                    prevIsFill = curIsFill;
                }
                // 한 줄이 완성된 것 같을 때 마지막 힌트 처리
                if (lineIsCandidate)
                {
                    // 마지막 칸 채워져 있으면 힌트 인덱스 올림
                    if (prevIsFill)
                    {
                        ++hintIdx;
                    }
                    // 힌트 개수가 연속된 블록 덩어리 수와 같고 && 마지막 힌트가 마지막 덩어리 수와 같으면
                    // 즉, 이 줄이 정상적으로 완료된 줄이면
                    if (hintIdx == LeftHintRows[changed_Y].Count && lastCount == LeftHintRows[changed_Y][hintIdx - 1].Num)
                    {
                        foreach (HintNum oneHintNum in LeftHintRows[changed_Y])
                        {
                            oneHintNum.IsUsed = true;
                        }
                        return;
                    }
                }

                // 왼쪽부터 or 오른쪽부터 한 칸씩 보면서 X로 둘러싸인 FILL 덩어리를 검사
                // 덩어리를 찾았으면 해당하는 힌트를 흐리게 한다.

                // TODO : 정방향 및 역방향 힌트 업데이트 기능 구현!

                // 정방향 힌트 업데이트 TODO
                CellFill prevFill = CellFill.X;
                CellFill curFill = CellFill.BLANK;
                fillCount = 0;
                hintIdx = 0;
                int lastHintCellIdx = -1;
                for (int x = 0; x < CurrentBoard[0].Count; ++x)
                {
                    curFill = CurrentBoard[changed_Y][x].FillValue;
                    // 빈 칸이 나오면 그 뒤는 완성 못 한 것이므로 힌트 흐리게 하지 않고 종료
                    if (curFill == CellFill.BLANK)
                        break;
                    // 칠해져있으면 계속 카운팅
                    if (curFill == CellFill.FILL)
                        ++fillCount;
                    // X 가 나오면 그 동안 셌던 블록 수와 힌트의 숫자 비교 후
                    // 틀리면 종료, 맞으면 힌트를 흐리게 하고 카운터 초기화 후 끝났으면 종료, 맞으면 계속 반복
                    else if (prevFill == CellFill.FILL && curFill == CellFill.X)
                    {
                        HintNum curHint = LeftHintRows[changed_Y][hintIdx];
                        if (fillCount != curHint.Num)
                            break;

                        curHint.IsUsed = true;
                        ++hintIdx;
                        if (hintIdx >= LeftHintRows[changed_Y].Count)
                            break;
                        fillCount = 0;
                        lastHintCellIdx = x - 1;
                    }
                    // 끝 부분은 고려하지 않아도 됨.
                    // 끝까지 채워졌다면 이미 줄이 완성된 것이므로 앞에서 걸려졌을 것.

                    prevFill = curFill;
                }


                // 역방향 힌트 업데이트 TODO
                prevFill = CellFill.X;
                curFill = CellFill.BLANK;
                fillCount = 0;
                hintIdx = LeftHintRows[changed_Y].Count - 1;
                for (int x = CurrentBoard[0].Count - 1; x >= lastHintCellIdx + 1; --x)
                {
                    curFill = CurrentBoard[changed_Y][x].FillValue;
                    // 빈 칸이 나오면 그 뒤는 완성 못 한 것이므로 힌트 흐리게 하지 않고 종료
                    if (curFill == CellFill.BLANK)
                        break;
                    // 칠해져있으면 계속 카운팅
                    if (curFill == CellFill.FILL)
                        ++fillCount;
                    // X 가 나오면 그 동안 셌던 블록 수와 힌트의 숫자 비교 후
                    // 틀리면 종료, 맞으면 힌트를 흐리게 하고 카운터 초기화 후 끝났으면 종료, 맞으면 계속 반복
                    else if (prevFill == CellFill.FILL && curFill == CellFill.X)
                    {
                        HintNum curHint = LeftHintRows[changed_Y][hintIdx];
                        if (fillCount != curHint.Num)
                            break;

                        curHint.IsUsed = true;
                        --hintIdx;
                        if (hintIdx < 0)
                            break;
                        fillCount = 0;
                    }
                    // 끝 부분은 고려하지 않아도 됨.
                    // 끝까지 채워졌다면 이미 줄이 완성된 것이므로 앞에서 걸려졌을 것.

                    prevFill = curFill;
                }

            }

            // 상단 힌트 업데이트
            void UpdateUpperHint()
            {
                var oneLineHint = UpperHintColumns[changed_X];
                // 힌트가 '0' 한 개이면 아무 작업도 안 하고 리턴
                if (oneLineHint.Count == 1 && oneLineHint[0].Num == 0)
                    return;

                // 힌트 색깔 초기화
                foreach (HintNum oneHintNum in oneLineHint)
                {
                    oneHintNum.IsUsed = false;
                }

                // 모든 힌트 숫자를 순서대로 칠했으면,
                // 즉, 줄이 완성되었으면, X와 관계없이 전부 흐린색 처리
                bool lineIsCandidate = true;
                bool prevIsFill = false;
                int fillCount = 0;  // 연속된 블록 덩어리 수
                int hintIdx = 0;    // 현재 검사중인 힌트 숫자의 인덱스
                int hintNum = 0;    // 현재 검사중인 힌트 숫자
                int lastCount = 0;
                for (int y = 0; y < CurrentBoard.Count; ++y)
                {
                    bool curIsFill = (CurrentBoard[y][changed_X].FillValue == CellFill.FILL);
                    hintNum = UpperHintColumns[changed_X][hintIdx].Num;
                    if (curIsFill)
                    {
                        ++fillCount;
                        if (fillCount > hintNum)
                        {
                            lineIsCandidate = false;
                            break;
                        }
                    }
                    // prev == T, cur == F
                    else if (prevIsFill)
                    {
                        if (fillCount != hintNum)
                        {
                            lineIsCandidate = false;
                            break;
                        }
                        lastCount = fillCount;
                        fillCount = 0;
                        ++hintIdx;
                        if (hintIdx >= UpperHintColumns[changed_X].Count)
                        {
                            prevIsFill = curIsFill;
                            break;
                        }
                    }

                    prevIsFill = curIsFill;
                }
                // 한 줄이 완성된 것 같을 때 마지막 힌트 처리
                if (lineIsCandidate)
                {
                    // 마지막 칸 채워져 있으면 힌트 인덱스 올림
                    if (prevIsFill)
                    {
                        ++hintIdx;
                    }
                    // 힌트 개수가 연속된 블록 덩어리 수와 같고 && 마지막 힌트가 마지막 덩어리 수와 같으면
                    // 즉, 이 줄이 정상적으로 완료된 줄이면
                    if (hintIdx == UpperHintColumns[changed_X].Count && lastCount == UpperHintColumns[changed_X][hintIdx - 1].Num)
                    {
                        foreach (HintNum oneHintNum in UpperHintColumns[changed_X])
                        {
                            oneHintNum.IsUsed = true;
                        }
                        return;
                    }
                }

                // 위쪽부터 or 아래쪽부터 한 칸씩 보면서 X로 둘러싸인 FILL 덩어리를 검사
                // 덩어리를 찾았으면 해당하는 힌트를 흐리게 한다.

                // TODO : 정방향 및 역방향 힌트 업데이트 기능 구현!

                // 정방향 힌트 업데이트 TODO
                CellFill prevFill = CellFill.X;
                CellFill curFill = CellFill.BLANK;
                fillCount = 0;
                hintIdx = 0;
                int lastHintCellIdx = -1;
                for (int y = 0; y < CurrentBoard.Count; ++y)
                {
                    curFill = CurrentBoard[y][changed_X].FillValue;
                    // 빈 칸이 나오면 그 뒤는 완성 못 한 것이므로 힌트 흐리게 하지 않고 종료
                    if (curFill == CellFill.BLANK)
                        break;
                    // 칠해져있으면 계속 카운팅
                    if (curFill == CellFill.FILL)
                        ++fillCount;
                    // X 가 나오면 그 동안 셌던 블록 수와 힌트의 숫자 비교 후
                    // 틀리면 종료, 맞으면 힌트를 흐리게 하고 카운터 초기화 후 끝났으면 종료, 맞으면 계속 반복
                    else if (prevFill == CellFill.FILL && curFill == CellFill.X)
                    {
                        HintNum curHint = UpperHintColumns[changed_X][hintIdx];
                        if (fillCount != curHint.Num)
                            break;

                        curHint.IsUsed = true;
                        ++hintIdx;
                        if (hintIdx >= UpperHintColumns[changed_X].Count)
                            break;
                        fillCount = 0;
                        lastHintCellIdx = y - 1;
                    }
                    // 끝 부분은 고려하지 않아도 됨.
                    // 끝까지 채워졌다면 이미 줄이 완성된 것이므로 앞에서 걸려졌을 것.

                    prevFill = curFill;
                }


                // 역방향 힌트 업데이트 TODO
                prevFill = CellFill.X;
                curFill = CellFill.BLANK;
                fillCount = 0;
                hintIdx = UpperHintColumns[changed_X].Count - 1;
                for (int y = CurrentBoard.Count - 1; y >= lastHintCellIdx + 1; --y)
                {
                    curFill = CurrentBoard[y][changed_X].FillValue;
                    // 빈 칸이 나오면 그 뒤는 완성 못 한 것이므로 힌트 흐리게 하지 않고 종료
                    if (curFill == CellFill.BLANK)
                        break;
                    // 칠해져있으면 계속 카운팅
                    if (curFill == CellFill.FILL)
                        ++fillCount;
                    // X 가 나오면 그 동안 셌던 블록 수와 힌트의 숫자 비교 후
                    // 틀리면 종료, 맞으면 힌트를 흐리게 하고 카운터 초기화 후 끝났으면 종료, 맞으면 계속 반복
                    else if (prevFill == CellFill.FILL && curFill == CellFill.X)
                    {
                        HintNum curHint = UpperHintColumns[changed_X][hintIdx];
                        if (fillCount != curHint.Num)
                            break;

                        curHint.IsUsed = true;
                        --hintIdx;
                        if (hintIdx < 0)
                            break;
                        fillCount = 0;
                    }
                    // 끝 부분은 고려하지 않아도 됨.
                    // 끝까지 채워졌다면 이미 줄이 완성된 것이므로 앞에서 걸려졌을 것.

                    prevFill = curFill;
                }

            }
            #endregion
        }
        #endregion

        /// <summary>
        /// 현재 보드판 색칠 상태가 정답 배열과 같은지 검사하는 메소드
        /// </summary>
        /// <returns>현재 보드판이 정답이면 true, 아니면 false</returns>
        public bool CheckSolved()
        {
            for (int y = 0; y < AnswerArray.GetLength(0); ++y)
            {
                for (int x = 0; x < AnswerArray.GetLength(1); ++x)
                {
                    bool currentIsFill = CurrentBoard[y][x].FillValue == CellFill.FILL;
                    bool answerIsFill = AnswerArray[y, x];

                    if (currentIsFill != answerIsFill)
                        return false;
                }
            }
            return true;
        }

        #region INotifyPropertyChanged 멤버
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}