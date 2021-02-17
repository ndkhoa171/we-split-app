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
    /// <summary>
    /// Interaction logic for WindowNewMember.xaml
    /// </summary>
    public partial class WindowNewMember : Window
    {
        public Member _member = new Member();
        public WindowNewMember()
        {
            InitializeComponent();
        }
        string fileToCopy;
        string destinationDirectory;
        string tuongdoi;
        public int KiemTra(char[] chuoi)
        {
            for (int i = 0; i < chuoi.Count(); i++)
            {
                if (chuoi[i] < '0' || chuoi[i] > '9')
                {
                    return 0;
                }
            }

            return 1;
        }
        private void Ok(object sender, RoutedEventArgs e)
        {
            if (textname.Text.Trim().Length == 0 || KiemTra(textphone.Text.ToCharArray()) == 0)
            {
                textname.Text = "";
                Validation.GetHasError(textname);
                textphone.Text = "";
                Validation.GetHasError(textphone);
            }          
            if (avatarImage.Source == null)
            {
                MessageBox.Show("Chưa chọn ảnh đại diện!");
            }
            else
            {
                _member.Name = textname.Text;
                _member.Phone = textphone.Text;
                _member.Leader = "Black";
                var newguid = Guid.NewGuid();
                File.Copy(fileToCopy, destinationDirectory + newguid);
                _member.Avatar = "img/" + newguid;
                DialogResult = true;
            }

        }

        private void NewAvatar(object sender, RoutedEventArgs e)
        {
            var _screen = new OpenFileDialog();
            if (_screen.ShowDialog() == true)
            {
                var bitmap = new BitmapImage(new Uri(_screen.FileName, UriKind.Absolute));

                string daugach = "\\";
                int dai = _screen.FileName.Length;
                int vitri = _screen.FileName.LastIndexOf(daugach);
                tuongdoi = _screen.FileName.Substring(vitri + 1);
                fileToCopy = _screen.FileName;
                destinationDirectory = AppDomain.CurrentDomain.BaseDirectory + "img\\";
                avatarImage.Source = bitmap;
            }
        }

        private void Cancle(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _member;
        }
    }
}
