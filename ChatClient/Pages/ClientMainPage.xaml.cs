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
    public partial class ClientMainPage : UserControl
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.DisconnectUser();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
        void Update()
        {
            MainGrid.ItemsSource = JsonConvert.DeserializeObject<List<OrderInfo>>(mainWindow.UpdateData());
        }

        private void ClientMainContol_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadClientAddOrderPage();
        }
    }
}
