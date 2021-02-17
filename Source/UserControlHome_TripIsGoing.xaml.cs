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
    public partial class UserControlHome_TripIsGoing : UserControl
    {
        //KHAI BÁO BIẾN TOÀN CỤC
        public BindingList<Trip> _trips = new BindingList<Trip>(); //Danh sách các chuyến đang đi
        BindingList<Trip> _triped = new BindingList<Trip>(); //Danh sách các đã đi

        UserControlHome_TripHasGone triphasgone; // Con trỏ màn hình TripHasGone
        UserControlHome_Introduce UControl_introduce = new UserControlHome_Introduce();

        //GET-SET
        public BindingList<Trip> GET_triped() => _triped;
        public BindingList<Trip> GET_trip() => _trips;
        
        public void SET_data(BindingList<Trip> data)
        {
            this._trips = data;
        }

        //void SET_data(BindingList<Trip> trips) { this._trips = trips; }
        //int _Count = 0;
        public UserControlHome_TripIsGoing(BindingList<Trip> tr, UserControlHome_TripHasGone THG)
        {
            triphasgone = THG;
            _trips = tr;
            InitializeComponent();       
        }

        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Load từ database - lọc các chuyến đang đi
            if (_trips.Count > 0)
            {
                dataListView.ItemsSource = _trips;
                dataListView.SelectedIndex = 0;

                //Load chuyến đi đầu tiên trong danh sách
                Grid_Introduce.Children.Clear();
                Grid_Introduce.Children.Add(UControl_introduce = new UserControlHome_Introduce());
                UControl_introduce.SET_trip(_trips[0]);
            }
        }

        private void dataListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Chọn một chuyến đi để hiển thị trên Introduce
            int index = dataListView.SelectedIndex;
            var trip = new Trip();
            for (int i = 0; i < _trips.Count; i++)
            {
                if (index == i)
                {
                    trip = _trips[i];
                    break;
                }
            }
            Grid_Introduce.Children.Clear();
            Grid_Introduce.Children.Add(UControl_introduce = new UserControlHome_Introduce());
            UControl_introduce.SET_trip(trip);
        }

        private void Click_Has_Gone(object sender, RoutedEventArgs e)
        {
            var index = dataListView.SelectedIndex;
            _trips[index].Status = "gone";
            _triped.Add(_trips[index]); // Thêm vào danh sách từng đi
            triphasgone._trips.Add(_trips[index]);
            _trips.RemoveAt(index); //Xóa bỏ tại dang sách đang đi
            this.UserControl_Loaded(sender, e);

        }
    }
}
