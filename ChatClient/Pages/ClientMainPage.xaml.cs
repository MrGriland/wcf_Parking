using ChatClient.ServiceChat;
using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace ChatClient.Resources
{
    /// <summary>
    /// Логика взаимодействия для ClientMainPage.xaml
    /// </summary>
    public partial class ClientMainPage : UserControl, IServiceChatCallback
    {
        MainWindow mainWindow;
        public ClientMainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainWindow.Closing += Window_Closing;
        }
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.DisconnectUser();
            mainWindow.LoadBeginPage();
        }

        public void MsgCallback(string msg)
        {
            OrderInfo orderInfo = JsonConvert.DeserializeObject<OrderInfo>(msg);
            MainGrid.Items.Add(orderInfo);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.DisconnectUser();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.UpdateData();
            MainGrid.Items.Clear();
            MainGrid.Items.Add(mainWindow.orderInfo);
        }
    }
}
