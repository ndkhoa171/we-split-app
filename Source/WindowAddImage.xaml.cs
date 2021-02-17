using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for WindowAddImage.xaml
    /// </summary>
    public partial class WindowAddImage : Window  //MÀN HÌNH THÊM HÌNH ẢNH
    {
        public string pathImage;
        public WindowAddImage()
        {
            InitializeComponent();
        }
        string fileToCopy;
        string destinationDirectory;
        string tuongdoi;
        private void FindImage(object sender, RoutedEventArgs e)
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

                var exists = File.Exists(destinationDirectory + tuongdoi);
                if (exists == true)
                {
                    MessageBox.Show("Hình ảnh đã tồn tại.", "Lỗi", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
                else
                {
                    image.Source = bitmap;
                }
            }
            else
            {
                this.Close();
            }
        }
        private void OK(object sender, RoutedEventArgs e)
        {
            if (image.Source == null)
            {
                MessageBox.Show("Chưa chọn ảnh!");
            }
            else
            {
                File.Copy(fileToCopy, destinationDirectory + System.IO.Path.GetFileName(fileToCopy));
                pathImage = "img/" + tuongdoi;
                DialogResult = true;
            }
        }

        private void Cancle(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

