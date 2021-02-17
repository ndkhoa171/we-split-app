using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WeSplitApp
{
    public partial class UpdateJourneyScreen : Window
    {
        //KHAI BÁO BIẾN TOÀN CỤC
        Trip _trip = new Trip();
        UserControlUpdate_ListMember UControl_listMember;
        UserControlUpdate_ListPlace UControl_listPlace;
        UserControlUpdate_TotalExpense UControl_totalExpense;
        public UserControlHome_Introduce usercontrol_introdue;

        public UpdateJourneyScreen(Trip trip, UserControlHome_Introduce UCI)
        {
            usercontrol_introdue = UCI;
            InitializeComponent();
            this._trip = trip;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {           
            if (_trip.Status == "gone")
                Icon_foreground.Foreground = Brushes.Red;

            this.DataContext = _trip;

            //Hiển thị danh sách thành viên trước
            Grid_Principal.Children.Clear();
            Grid_Principal.Children.Add(UControl_listMember);
            UControl_listMember.SET_listMember(_trip);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        //NÚT TẮT MÀN HÌNH
        private void Button_Power_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //CHỌN MENU HIỂN THỊ
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            switch (index)
            {
                case 0:
                    Grid_Principal.Children.Clear();
                    Grid_Principal.Children.Add(UControl_listMember = new UserControlUpdate_ListMember(usercontrol_introdue));
                    UControl_listMember.SET_listMember(_trip);
                    break;
                case 1:
                    Grid_Principal.Children.Clear();
                    Grid_Principal.Children.Add(UControl_listPlace = new UserControlUpdate_ListPlace(usercontrol_introdue));
                    UControl_listPlace.SET_places(_trip.ListPlaces);
                    break;
                case 2:
                    Grid_Principal.Children.Clear();
                    Grid_Principal.Children.Add(UControl_totalExpense = new UserControlUpdate_TotalExpense());
                    UControl_totalExpense.SET_trip(_trip);
                    break;
            }
        }

        //DI CHUYỂN CHỌN MENU
        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            Grid_Menu.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }          
    }
}
