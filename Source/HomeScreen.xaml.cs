using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    public partial class HomeScreen : Window
    {
        //KHAI BÁO BIẾN TOÀN CỤC
        public UserControlHome_TripIsGoing Go ;
        public UserControlHome_TripHasGone Gone = new UserControlHome_TripHasGone();
        public int countOpenMenu = 0; //Đếm số lần mở menu chức năng (Search + Add)
        UserControlHome_OpenMenu UContro_openMenu;
        public BindingList<Trip> _listTrip;
        BindingList<Trip> _trips = new BindingList<Trip>(); //Danh sách các chuyến đang đi
        BindingList<Trip> _triped = new BindingList<Trip>(); //Danh sách các đã đi

        public HomeScreen()
        {
            InitializeComponent();
        }
      
        private void Button_Power_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        int _Count = 0;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var config = ConfigurationManager.AppSettings["ShowSplash"];
            if (config.ToLower() == "true")
            {
                var screen = new SplashScreen();
                screen.ShowDialog();
            }

            if (_Count == 0)
            {
                _listTrip = TripDao.GetAll();
                _Count++;
                foreach (var trip in _listTrip)
                {
                    if (trip.Status.ToLower() == "going") 
                        _trips.Add(trip);
                    if (trip.Status.ToLower() == "gone")
                        _triped.Add(trip);
                }
            }
            //Ban đầu load danh sách chuyến đang đi
            Grid_List.Children.Clear();
            Grid_List.Children.Add(Go = new UserControlHome_TripIsGoing(_trips,Gone));
            Go.SET_data(_trips);
            this.Show();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        //TAB PHÂN LOẠI CHUYẾN ĐI
        int _count2 = 0;
        int _count3 = 0;
        private void Button_Tab_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness((250 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    Grid_List.Children.Clear();
                    Grid_List.Children.Add( Go = new UserControlHome_TripIsGoing(_trips,Gone));
                    if (_count2 == 0)
                    {
                        Go.SET_data(_trips);
                        _count2++;
                    }
                    break;
                case 1:
                    Grid_List.Children.Clear();
                    Grid_List.Children.Add(Gone);
                    if (_count2 == 0)
                    {
                        Gone.SET_data(_triped);
                        _count3++;
                    }
                    break;
            }
        }

        //MỞ MENU CHỨC NĂNG (SEARCH + ADD)
        private void Button_Menu_Click(object sender, RoutedEventArgs e)
        {
            if (countOpenMenu == 0)
            {
                Grid_Menu.Children.Add(UContro_openMenu = new UserControlHome_OpenMenu(Go));
                if(UContro_openMenu.checkNewTrip==1)
                {
                    Grid_List.Children.Clear();
                    _trips.Add(UContro_openMenu.newTrip);
                    _listTrip.Add(UContro_openMenu.newTrip);
                    Go.SET_data(_trips);
                    Grid_List.Children.Add(Go);
                }    
                //UContro_openMenu.SET_trip()
                countOpenMenu++;
            }
            else
            {
                Grid_Menu.Children.Clear();
                countOpenMenu--;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            
            BindingList<Trip> _ListTripSave = new BindingList<Trip>();
            foreach (var i in Go._trips)
            {
                _ListTripSave.Add(i);
            }
            foreach (var i in _triped)
            {
                _ListTripSave.Add(i);
            }


            TripDao.GhiFile(_ListTripSave, "Database_ListTrip", 1);
            MessageBox.Show("Saving..");
            //TripDao.GhiFile(Go.GET_trip(), "Database_ListTrip2", 1);
        }
    }
}
