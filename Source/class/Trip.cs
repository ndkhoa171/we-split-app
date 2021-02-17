using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
namespace WeSplitApp
{
    public class Trip :INotifyPropertyChanged
    {
        /// <summary>
        /// Tên chuyến đi
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Danh sách thành viên
        /// </summary>
        public BindingList<Member> ListMembers { get; set; }

        /// <summary>
        /// Danh sách địa điểm
        /// </summary>
        public BindingList<Place> ListPlaces { get; set; }

        /// <summary>
        /// Danh sách chi tiêu chung
        /// </summary>
        public BindingList<Expense> ListExpenses { get; set; }

        /// <summary>
        /// Tổng chi phí chung
        /// </summary>
        public double TotalExpense { get; set; }

        /// <summary>
        /// Trạng thái: Đang đi "going" / đã đi "gone"
        /// </summary>
        public string Status { get; set; }

        // DÙNG CHO TÌM KIẾM
        public string ThanhVien { get; set; }
        public string DiaDiem { get; set; }
        public string Ten { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
