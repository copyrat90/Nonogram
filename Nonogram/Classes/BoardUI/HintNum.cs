using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Classes.BoardUI
{
    public class HintNum : INotifyPropertyChanged
    {
        private bool isUsed;
        public bool IsUsed
        {
            get { return isUsed; }
            set
            {
                if (isUsed != value)
                {
                    isUsed = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsUsed"));
                }
            }
        }
        private int number;
        public int Num
        {
            get { return number; }
            set
            {
                if (number != value)
                {
                    number = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Num"));
                }
            }
        }

        public HintNum(int num)
        {
            Num = num;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
