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
    public partial class WindowAddExpense : Window
    {
        public BindingList<Expense> _listExpense = new BindingList<Expense>();
        Expense _expense = new Expense();

        public WindowAddExpense(BindingList<Expense> _listexpense)
        {
            InitializeComponent();
            _listExpense = _listexpense;
        }

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxExpense.ItemsSource = _listExpense;
            DataContext = _expense;
        }

        private void Ok(object sender, RoutedEventArgs e)
        {
            if (money.Text.Trim().Length == 0 || ComboBoxExpense.SelectedItem == null)
            {
                money.Text = "";
                Validation.GetHasError(money);
                //money.Visibility = Visibility.Visible;
            }
            else
            {
                if (KiemTra(money.Text.ToCharArray()) != 0)
                
                {
                    var index = ComboBoxExpense.SelectedIndex;
                    _listExpense[index].Money += float.Parse(money.Text);
                    DialogResult = true;
                }
            }
        }

        private void Cancle(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
