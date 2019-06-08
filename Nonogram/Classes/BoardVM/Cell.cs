using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonogram.Classes.BoardVM
{
    /// <summary>
    /// 한 칸이 비어있는지, 칠해져 있는지, X 표시 되어있는지 나타내는 enum
    /// </summary>
    public enum CellFill { BLANK, FILL, X }

    /// <summary>
    /// 한 칸을 표현하는 class
    /// Y좌표
    /// </summary>
    public class Cell : INotifyPropertyChanged
    {
        #region 좌표
        public int Y { get; private set; }
        public int X { get; private set; }
        #endregion

        /// <summary>
        /// 공백, 칠, X표 표현
        /// 값이 바뀌었을 때 이벤트 호출 (힌트 흐리게 변경, 클리어 체크)
        /// </summary>
        private CellFill fillValue;
        public CellFill FillValue
        {
            get { return fillValue; }
            set
            {
                if (fillValue != value)
                {
                    fillValue = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FillValue"));
                }
            }
        }

        /// <summary>
        /// 한 칸의 좌표와 색칠 상태를 매개변수로 받는 생성자
        /// </summary>
        /// <param name="y"> y좌표 (0-based index) </param>
        /// <param name="x"> x좌표 (0-based index) </param>
        /// <param name="fill"> 색칠 상태 (중단된 퍼즐을 불러오기 할 때 필요) </param>
        public Cell(int y, int x, CellFill fill)
        {
            Y = y;
            X = x;
            FillValue = fill;
        }

        #region INotifyPropertyChanged 멤버
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
