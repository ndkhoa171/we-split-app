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
    public partial class UserControlUpdate_ListPlace : UserControl
    {
        //KHAI BÁO BIẾN TOÀN CỤC
        BindingList<Place> _listPlaces = new BindingList<Place>();

        //GET-SET
        public BindingList<Place> GET_places() => _listPlaces;
        public void SET_places(BindingList<Place> listPlaces) { this._listPlaces = listPlaces; }
        public UserControlHome_Introduce usercontrol_introdue; // Con trỏ UserControl_Introduce
        public UserControlUpdate_ListPlace(UserControlHome_Introduce UCI)
        {
            usercontrol_introdue = UCI;
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_listPlaces.Count != 0)
            {
                listViewPlace.ItemsSource = _listPlaces; // Hiển thị các địa điểm trên lộ trình
                listViewPlace.SelectedIndex = 0;
                listViewImage.ItemsSource = _listPlaces[0].ListImage; // Hiển thị các hình ảnh của lộ trình đầu tiên
                listViewPlace.SelectionChanged += ListViewPlace_SelectionChanged; ;  //Mỗi khi thay đổi địa điểm khác
            }
        }

        private void ListViewPlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = listViewPlace.SelectedIndex;
            listViewImage.ItemsSource = _listPlaces[index].ListImage; // Thay đổi danh sách hiển thị ảnh
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            if (_listPlaces.Count == 0)
            {
                MessageBox.Show("Chưa có lộ trình nào");
            }
            else
            {
                var screen = new WindowAddImage();
                var result = screen.ShowDialog();
                if (result == true)
                {
                    var index = listViewPlace.SelectedIndex;
                    _listPlaces[index].ListImage.Add(screen.pathImage);
                }
            }
        }

        private void AddPlace(object sender, RoutedEventArgs e)
        {
            var screen = new WindowAddPlace();
            var result = screen.ShowDialog();
            if (result == true)
            {
                _listPlaces.Add(screen.place);
                if (_listPlaces.Count - 1 == 0)
                {
                    this.UserControl_Loaded(sender, e);
                }
                listViewPlace.SelectedIndex = _listPlaces.Count - 1;
                usercontrol_introdue.UserControl_Loaded(sender,e);
            }
        }
    }
}
