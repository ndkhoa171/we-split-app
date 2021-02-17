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
    public partial class UserControlHome_TripHasGone : UserControl
    {
        //KHAI BÁO BIẾN TOÀN CỤC
        public BindingList<Trip> _trips = new BindingList<Trip>(); //Danh sách các chuyến đã đi
        UserControlHome_Introduce UControl_introduce=new UserControlHome_Introduce();

        //GET-SET
        public void SET_data(BindingList<Trip> data)
        {
            this._trips = data;
        }
        public BindingList<Trip> GET_trips() => _trips;
        //void SET_data(BindingList<Trip> trips) { this._trips = trips; }

        public UserControlHome_TripHasGone()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Load từ databse và lọc các chuyến đã đi
         
            dataListView.ItemsSource = _trips;

            //Load chuyến đi đầu tiên trong danh sách
            Grid_Introduce.Children.Clear();
            Grid_Introduce.Children.Add(UControl_introduce);
            UControl_introduce.SET_trip(_trips[0]);
        }

        private void dataListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Chọn một chuyến đi để hiển thị Introduce
            int index = dataListView.SelectedIndex;
            var trip = new Trip();
            for (int i = 0; i < _trips.Count; i++)
            {
                if (index == i)
                    trip = _trips[i];
            }
            Grid_Introduce.Children.Clear();
            Grid_Introduce.Children.Add(UControl_introduce);
            UControl_introduce.SET_trip(trip);
            UControl_introduce.UserControl_Loaded(sender, e);
        }
    }
}
