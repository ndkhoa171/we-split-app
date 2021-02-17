using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PropertyChanged;
namespace WeSplitApp
{
    public class Expense :INotifyPropertyChanged
    {
        /// <summary>
        /// Tên chi tiêu
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Số tiền
        /// </summary>
        public float Money { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
