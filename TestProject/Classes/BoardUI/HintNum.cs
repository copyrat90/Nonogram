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
        public int Num { get; set; }
        public string NumString { get { return Num.ToString(); } }

        public HintNum(int num)
        {
            Num = num;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
