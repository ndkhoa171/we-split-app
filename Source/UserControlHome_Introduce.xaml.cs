using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeSplitApp
{   
    public partial class UserControlHome_Introduce : UserControl
    {
        //KHAI BÁO BIẾN TOÀN CỤC
        Trip _trip = new Trip(); //chi tiết một chuyến đi

        //GET-SET
        public Trip GET_trip() => _trip;
        public void SET_trip(Trip trip) { this._trip = trip; }

        public UserControlHome_Introduce()
        {
            InitializeComponent();
        }

        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(_trip.ListMembers!=null)
            {
                textblockNumberMember.Text = _trip.ListMembers.Count.ToString(); // Hiển thị số lượng thành viên
                textblockNumberPlace.Text = _trip.ListPlaces.Count.ToString(); // Hiện thị số địa điểm
                texblockTotalExpense.Text = _trip.TotalExpense.ToString("n0"); // Hiện thị tổng chi
                DataContext = _trip;
            }    
            
        }
        
        private void Button_Detail_Click(object sender, RoutedEventArgs e)
        {
            var screen = new UpdateJourneyScreen(_trip, this);
            screen.ShowDialog();
        }
        public double TinhLaiTongChi(BindingList<Expense> t) // Hàm tính lại tổng chi tiêu
        {
            double result = 0;
            foreach (var i in t)
            {
                result += i.Money;
            }
            return result;
        }
        private void Button_Add_Expense(object sender, RoutedEventArgs e)
        {
            var screen = new WindowAddExpense(_trip.ListExpenses);
            var result = screen.ShowDialog();
            if(result==true)
            {
                _trip.ListExpenses = screen._listExpense;
                _trip.TotalExpense = TinhLaiTongChi(_trip.ListExpenses);
                texblockTotalExpense.Text = _trip.TotalExpense.ToString("n0"); // Hiện thị lại tổng chi
            } 
            else
            {
                screen.Close();
            }    
        }
    }
}
