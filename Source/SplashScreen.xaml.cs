using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        //KHAI BÁO BIẾN TOÀN CỤC
        public BindingList<string> data;
        public Random random = new Random();
        public SplashScreen()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            data = new BindingList<string>()
            {
                "Cuộc hành trình ngàn dặm bắt đầu từ một bước chân nhỏ bé",
                "Một cuộc hành trình thực sự được tính không phải bằng dặm, mà bằng những người bạn",
                "Đừng nói với tôi bạn học hành thế nào, hãy nói với tôi bạn đi bao nhiêu",
                "Lợi ích của việc đi là để lấy thực tế điều chỉnh trí tưởng tượng, và thay vì ngồi hình dung ra mọi chuyện, cứ đi để xem nó thực sự như thế nào",
                "Không gì giúp phát triển trí thông minh như là đi du lịch",
                "Ai cũng phải đu di lịch để học hỏi",
                "Đầu tư vào du lịch là một khoản đầu tư cho bản thân",
                "Du lịch – ban đầu nó khiến bạn không thốt nên lời, và sau đó biến bạn trở thành một người kể chuyện",
                "Tôi thích cảm giác được là kẻ vô danh trong một thành phố xa lạ",
                "Hãy chỉ nhớ những kỷ niệm, và để lại những dấu chân"
            };

            int index = random.Next(0, data.Count);
            Quote.Text = data[index];
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplash"].Value = "False";
            config.Save(ConfigurationSaveMode.Modified);

            //Clean solution để khởi tạo lại màn hinh Splash
        }
    }
}
