using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class SearchScreen : Window
    {

        List<Trip> _dataSearch = new List<Trip>();
        public SearchScreen(BindingList<Trip> data)
        {
            InitializeComponent();

            //Load từ database - lọc các chuyến đang đi
            BindingList<Trip> temp = TripDao.GetAll();
            _dataSearch = temp.ToList();
            dataListView.ItemsSource = _dataSearch;

            for (int i = 0; i < _dataSearch.Count; i++)
            {
                for (int j = 0; j < _dataSearch[i].ListMembers.Count; j++)
                {
                    _dataSearch[i].ThanhVien += RemoveVietnameseTone(_dataSearch[i].ListMembers[j].Name).Trim();
                }

                for (int t = 0; t < _dataSearch[i].ListPlaces.Count; t++)
                {
                    _dataSearch[i].DiaDiem += RemoveVietnameseTone(_dataSearch[i].ListPlaces[t].Name).Trim();
                }
                _dataSearch[i].Ten = RemoveVietnameseTone(_dataSearch[i].Name).Trim();
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        public static string RemoveVietnameseTone(string text)
        {
            string result = text.ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //if (Choose == 0)
            //{
            //    Search_text.IsEnabled = false;
            //}
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(dataListView.ItemsSource) as CollectionView;
            view.Filter = TripFilter;
        }

        public bool TripFilter(object item)
        {
            if (String.IsNullOrEmpty(Search_text.Text))
            {
                return false;
            }
            else
            {
                if (Choose == 1)
                {
                    return ((item as Trip).DiaDiem.IndexOf(RemoveVietnameseTone(Search_text.Text.Trim()), StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (Choose == 2)
                {
                    return ((item as Trip).ThanhVien.IndexOf(RemoveVietnameseTone(Search_text.Text.Trim()), StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else
                {
                    return ((item as Trip).Ten.IndexOf(RemoveVietnameseTone(Search_text.Text.Trim()), StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
        }

        private void ButtonPower_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Search_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search_text.Text.Trim().Length == 0)
            {
                dataListView.ItemsSource = _dataSearch;
            }
            CollectionViewSource.GetDefaultView(dataListView.ItemsSource).Refresh();
        }

        int Choose = -1;
        private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Choose = Combobox_SearchBy.SelectedIndex;
            if (Search_text.Text != "")
            {
                Search_text.Text += " ";
                Search_text.Text = Search_text.Text.Substring(0, Search_text.Text.Length - 1);
            } // để sự kiện text changed đc gọi

        }

        private void Button_Detail_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Trip selectedTrip = button.DataContext as Trip;
            UserControlHome_Introduce a = new UserControlHome_Introduce(); // Đối số tạm không sử dụng.
            var Screen = new UpdateJourneyScreen(selectedTrip, a);
            Screen.Show();
        }
    }
}
