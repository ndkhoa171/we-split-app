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
    public partial class UserControlHome_OpenMenu : UserControl
    {

        //KHAI BÁO BIẾN TOÀN CỤC
        BindingList<Trip> _trip = new BindingList<Trip>();
        public Trip newTrip = new Trip();
        public int checkNewTrip = 0;
        UserControlHome_TripIsGoing tripgoing; //Con trỏ trỏ đến màn hình TripIsGoing
        //GET-SET
        public BindingList<Trip> GET_trip() => _trip;
        public void SET_trip(BindingList<Trip> trip) { this._trip = trip; }

        public UserControlHome_OpenMenu(UserControlHome_TripIsGoing tripgo)
        {
            tripgoing = tripgo; //Con trỏ màn hình
            InitializeComponent();
        }

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            var screen = new SearchScreen(_trip);
            screen.ShowDialog();           
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            checkNewTrip = 1;

            var screen = new CreateJourneyScreen();
            var result = screen.ShowDialog(); 
            if(result==true)
            {
                newTrip = screen._New;
                tripgoing._trips.Add(newTrip);
                MessageBox.Show("Tạo mới chuyến đi thành công!");
            } 
            else
            {
                screen.Close();
            }    
        }
    }
}
