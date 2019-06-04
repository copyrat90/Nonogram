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
        public string NumString
        {
            get { return number.ToString(); }
            set
            {
                int numVal = int.Parse(value);
                if (number != numVal)
                {
                    number = numVal;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumString"));
                }
            }
        }

        public HintNum(int num)
        {
            NumString = num.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
