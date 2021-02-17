using System;
using System.Collections.Generic;
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
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WeSplitApp
{
    /// <summary>
    /// Interaction logic for UserControlTotalExpense.xaml
    /// </summary>
    /// //KHAI BÁO BIẾN TOÀN CỤC

    public partial class UserControlUpdate_TotalExpense : UserControl
    {
        BindingList<Expense> _listExpense = new BindingList<Expense>();
        public Trip _trip = new Trip();
        double _cost;

        //GET-SET
        public BindingList<Expense> GET_expenses() => _listExpense;
        public void SET_trip(Trip Trip) { this._trip = Trip; }

        public UserControlUpdate_TotalExpense()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_trip.ListExpenses.Count == 0)
            {
                _trip.ListExpenses = new BindingList<Expense>()
                {
                    new Expense(){Name="Tiền phòng", Money=0},
                    new Expense(){Name="Tiền ăn", Money=0},
                    new Expense(){Name="Tiền nước", Money=0},
                    new Expense(){Name="Tiền xe", Money=0},
                    new Expense(){Name="Phụ phí", Money=0},
                };
                MessageBox.Show("Chưa có khoản chi tiêu nào");
            }
            _listExpense = _trip.ListExpenses;
            _cost = _trip.TotalExpense / _trip.ListMembers.Count;
            textblockCost.Text = _cost.ToString("n0");
        }
        public SeriesCollection Data => new SeriesCollection() // Biến chứa dữ liệu biểu đồ
        {
            new PieSeries()
            {
                Values = new ChartValues<float> { _trip.ListExpenses[0].Money} , Title = _trip.ListExpenses[0].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { _trip.ListExpenses[1].Money } , Title = _trip.ListExpenses[1].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { _trip.ListExpenses[2].Money } , Title =_trip.ListExpenses[2].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { _trip.ListExpenses[3].Money } , Title = _trip.ListExpenses[3].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { _trip.ListExpenses[4].Money } , Title = _trip.ListExpenses[4].Name
            }
        };

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var chart = chartPoint.ChartView as PieChart;
            foreach (PieSeries pie in chart.Series)
            {
                pie.PushOut = 0;
            }

            var neo = chartPoint.SeriesView as PieSeries;
            neo.PushOut = 30;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            this.DataContext = this;
        }

    }
}
