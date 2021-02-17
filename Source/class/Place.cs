using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
namespace WeSplitApp
{
    public class Place : INotifyPropertyChanged
    {
        /// <summary>
        /// Tên địa điểm
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Danh sách hình ảnh của địa điểm
        /// </summary>
        public BindingList<string> ListImage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
