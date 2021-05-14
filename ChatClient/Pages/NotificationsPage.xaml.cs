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

namespace ChatClient.Pages
{
    /// <summary>
    /// Логика взаимодействия для NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : UserControl
    {
        MainWindow mainWindow;
        private OrderInfo selectedOrderInfo;
        public NotificationsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            LoginInfo.Content = "Вы вошли как: " + mainWindow.login;
            CountInfo.Content = "Осталось мест: " + mainWindow.GetFreeCount();
            LBNotifications.ItemsSource = JsonConvert.DeserializeObject<List<OrderInfo>>(mainWindow.GetNotifications(mainWindow.login));
        }
        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadClientMainPage();
        }
        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadClientAddOrderPage();
        }
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.DisconnectUser();
            mainWindow.LoadBeginPage();
        }

        private void LBNotifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedOrderInfo = (OrderInfo)LBNotifications.SelectedItem;
            if (selectedOrderInfo != null)
            {
                NotificationL.Content = "Сообщение от администратора";
                NotificationTB.Text = selectedOrderInfo.OrderInfo_Notification;
                RemoveNotificationButton.Visibility = Visibility.Visible;
            }
        }

        private void RemoveNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ClearThisNotification(selectedOrderInfo.OrderInfo_ID);
            LBNotifications.ItemsSource = JsonConvert.DeserializeObject<List<OrderInfo>>(mainWindow.GetNotifications(mainWindow.login));
            NotificationL.Content = "";
            NotificationTB.Text = "";
            RemoveNotificationButton.Visibility = Visibility.Collapsed;
        }
    }
}
