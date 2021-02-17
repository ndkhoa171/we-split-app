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
    public partial class UserControlUpdate_ListMember : UserControl
    {
        //KHAI BÁO BIẾN TOÀN CỤC
        BindingList<Member> _listMembers = new BindingList<Member>(); //Danh sách thành viên
        public Trip _trip = new Trip();
        public UserControlHome_Introduce usercontrol_introduce; // Con trỏ màn hình UserControl_Introduce
        //GET-SET
        public BindingList<Member> GET_listMember() => _listMembers;
        public void SET_listMember(Trip trip) { this._trip = trip; }
      
        public UserControlUpdate_ListMember(UserControlHome_Introduce UCI)
        {
            usercontrol_introduce = UCI;
            InitializeComponent();           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {          
            dataListView.ItemsSource = _trip.ListMembers;
        }

        private void ButtonAddNewMember(object sender, RoutedEventArgs e)
        {
            var screen = new WindowNewMember();
            var result = screen.ShowDialog();
            if (result == true)
            {
                _trip.ListMembers.Add(screen._member);
                usercontrol_introduce.UserControl_Loaded(sender, e);
                this.UserControl_Loaded(sender, e);
            }
            else
            {
                screen.Close();
            }    
        }
    }
}
