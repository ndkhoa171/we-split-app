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
    public class Member :INotifyPropertyChanged
    {
        /// <summary>
        /// Tên thành viên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Tổng chi phí cho chuyến đi thành viên phải trả
        /// </summary>
        public double TotalCost { get; set; }

        /// <summary>
        /// Trưởng nhóm -> Red != Black
        /// </summary>
        public string Leader { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
