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
using System.Windows.Shapes;

namespace WeSplitApp
{
    /// <summary>
    /// Interaction logic for WindowAddPlace.xaml
    /// </summary>
    public partial class WindowAddPlace : Window
    {
        public Place place = new Place();
        public WindowAddPlace()
        {
            InitializeComponent();
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            if (nameplace.Text.Trim().Length == 0)
            {
                nameplace.Text = "";
                Validation.GetHasError(nameplace);
            }
            else
            {
                place.Name = nameplace.Text;
                place.ListImage = new BindingList<string>();
                DialogResult = true;
            }
        }

        private void Cancle(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = place;
        }
    }
}
