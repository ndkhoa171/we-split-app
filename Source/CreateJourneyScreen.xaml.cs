using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    public partial class CreateJourneyScreen : Window
    {
        public Trip _New { get; set; } = new Trip()
        {
            ListExpenses = new BindingList<Expense>()
            {
                new Expense(){Name="Tiền phòng", Money=0},
                new Expense(){Name="Tiền ăn", Money=0},
                new Expense(){Name="Tiền nước", Money=0},
                new Expense(){Name="Tiền xe", Money=0},
                new Expense(){Name="Phụ phí", Money=0},
            },
            ListMembers = new BindingList<Member>(),
            ListPlaces = new BindingList<Place>()
        };

        public CreateJourneyScreen()
        {
            InitializeComponent();
        }

        private void Button_Power_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Avt_Button_Click(object sender, RoutedEventArgs e)
        {
            var _screen = new OpenFileDialog();
            if (_screen.ShowDialog() == true)
            {
                var bitmap = new BitmapImage(new Uri(_screen.FileName, UriKind.Absolute));

                string daugach = "\\";
                int dai = _screen.FileName.Length;
                int vitri = _screen.FileName.LastIndexOf(daugach);
                string tuongdoi = _screen.FileName.Substring(vitri + 1);
                string fileToCopy = _screen.FileName;
                string destinationDirectory = AppDomain.CurrentDomain.BaseDirectory + "img\\";

                var exists = File.Exists(destinationDirectory + tuongdoi);
                if (exists == true)
                {                   
                    _New.Avatar = "img/" + tuongdoi;
                }
                else
                {
                    File.Copy(fileToCopy, destinationDirectory + System.IO.Path.GetFileName(fileToCopy));
                    _New.Avatar = "img/" + tuongdoi;
                }
            }
        }

        string Avt = "";
        Member _mem = new Member();

        private void MemAvt_Click(object sender, RoutedEventArgs e)
        {
            var _screen = new OpenFileDialog();
            if (_screen.ShowDialog() == true)
            {
                var bitmap = new BitmapImage(new Uri(_screen.FileName, UriKind.Absolute));

                string daugach = "\\";
                int vitri = _screen.FileName.LastIndexOf(daugach);
                string tuongdoi = _screen.FileName.Substring(vitri + 1);
                string fileToCopy = _screen.FileName;
                string destinationDirectory = AppDomain.CurrentDomain.BaseDirectory + "img\\";

                var guid = Guid.NewGuid();
                File.Copy(fileToCopy, destinationDirectory + guid);
                Avt = "/img/" + guid;
                MemImg.Source = bitmap;
            }
        }

        static int KtHopLe(string a)
        {
            char[] b = a.ToCharArray();
            for (int i = 0; i < b.Count(); i++)
            {
                if (b[i] > '9' || b[i] < '0')
                    return 0;
            }
            return 1;
        }

        //int Duyet = 0;
        private void AddMem_Button_Click(object sender, RoutedEventArgs e)
        {

            if (Avt == "" || MemberName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin.", "Lỗi", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            else if (KtHopLe(MemPhone.Text) == 0 || MemPhone.Text.Length < 9)
            {
                MessageBox.Show("Nhập số điện thoại sai định dạng.", "Lỗi", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            else
            {
                Member Addmem = new Member();

                Addmem.Avatar = Avt;
                Addmem.Name = MemberName.Text;
                Addmem.Phone = MemPhone.Text;

                Addmem.Leader = "Black";
                if (Check_Cap.IsChecked == true)
                {
                    Addmem.Leader = "Red";
                    Check_Cap.IsEnabled = false;
                }

                _New.ListMembers.Add(Addmem);

                ComboBoxMem.ItemsSource = _New.ListMembers;

                CountMem++;

                //MemberName.Text = "";
               // MemPhone.Text = "";
                MemImg.Source = null;
                Check_Cap.IsChecked = false;
            }
        }

        int CountMem = 0;

        Expense _expense = new Expense();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _New;
            S_Member.DataContext = _mem;
            SExpense.DataContext = _expense;
            ComboBoxExpense1.ItemsSource = _New.ListExpenses;
            ComboBoxExp.ItemsSource = _New.ListExpenses;
        }

        private void AddChiTieu_click(object sender, RoutedEventArgs e)
        {
            int index = ComboBoxExpense1.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Hãy chọn mục chi tiêu.", "Lỗi", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            else if (KtHopLe(ExpPrice.Text) == 0 || ExpPrice.Text.Length == 0)
            {
                MessageBox.Show("Số tiền không hợp lệ.", "Lỗi", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            else
            {
                _New.ListExpenses[index].Money += int.Parse(ExpPrice.Text);
                _New.TotalExpense += _New.ListExpenses[index].Money;
                DataContext = _New;
                ComboBoxExp.ItemsSource = _New.ListExpenses;
                //ExpPrice.Text = "";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (PhanLoai.IsChecked == true)
            {
                _New.Status = "gone";
            }
            else
            {
                _New.Status = "going";
            }
            if (_New.ListMembers.Count == 0)
            {
                TripName.Text = "";
                MemberName.Text = "";
                MemPhone.Text = "";
                ExpPrice.Text = "";
            }
                

            if (Validation.GetHasError(TripName) || Validation.GetHasError(MemberName) ||
                Validation.GetHasError(MemPhone) || Validation.GetHasError(ExpPrice))
                return;

            else DialogResult = true;           
        }

        private void Captain_Check(object sender, RoutedEventArgs e)
        {
            if (_New.ListMembers.Count > 0)
            {
                //foreach (var member in _New.ListMembers)
                //{
                //    if (member.Leader == "Red" && Check_Cap.IsChecked == true)
                //    {
                //        MessageBox.Show("Đã có trưởng nhóm!");
                //    }
                //    else
                //    {
                //        Check_Cap.IsChecked = false;
                //    }
                //}
            }
        }

        private void ComboBoxMem_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
        private void ComboBoxExp_SelectionChanged(object sender, SelectionChangedEventArgs e) { }
    }
}
