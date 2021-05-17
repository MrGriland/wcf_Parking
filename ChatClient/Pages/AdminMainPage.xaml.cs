using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminMainPage.xaml
    /// </summary>
    public partial class AdminMainPage : UserControl
    {
        MainWindow mainWindow;
        List<OrderInfo> orders = new List<OrderInfo>();
        List<OrderInfo> confirmedorders = new List<OrderInfo>();
        List<OrderInfo> searchconfirmedorders = new List<OrderInfo>();
        List<UserInfo> users = new List<UserInfo>();
        List<UserInfo> searchusers = new List<UserInfo>();
        public AdminMainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainWindow.Closing += Window_Closing;
            LoginInfo.Content = "Вы вошли как: " + mainWindow.login;
            CountInfo.Content = "Осталось мест: " + mainWindow.GetFreeCount();
            Payment.Content = "Доход от зарегистрированной брони: " + PaySum();
            OrderCount.Content = "Общее количество брони: " + OrderCountSum();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            mainWindow.DisconnectUser();
        }

        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadConfirmOrderPage();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.DisconnectUser();
            mainWindow.LoadBeginPage();
        }

        private void AdminMainControl_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }
        void Update()
        {
            MainGrid.ItemsSource = searchconfirmedorders;
            MainGridUsers.ItemsSource = searchusers;
            confirmedorders.Clear();
            orders = JsonConvert.DeserializeObject<List<OrderInfo>>(mainWindow.UpdateData());
            foreach (var item in orders)
            {
                if (item.OrderInfo_IsConfirmed)
                    confirmedorders.Add(item);
            }
            MainGrid.ItemsSource = confirmedorders;
            users.Clear();
            users = JsonConvert.DeserializeObject<List<UserInfo>>(mainWindow.UpdateUsersData());
            MainGridUsers.ItemsSource = users;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mainWindow.MainGrid.Children.Clear();
            mainWindow.MainGrid.Children.Add(new EditAdminPage(sender, mainWindow));
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
            Payment.Content = "Доход от зарегистрированной брони: " + PaySum();
            OrderCount.Content = "Общее количество брони: " + OrderCountSum();
        }
        private double PaySum()
        {
            Update();
            double sum = 0;
            foreach (var item in confirmedorders)
                sum += item.OrderInfo_Sum;
            return sum;
        }
        private double OrderCountSum()
        {
            Update();
            int sum = 0;
            foreach (var item in users)
                sum += item.UserInfo_Count;
            return sum;
        }
        private double PaySumSearched()
        {
            double sum = 0;
            foreach (var item in searchconfirmedorders)
                sum += item.OrderInfo_Sum;
            return sum;
        }
        private double OrderCountSumSearched()
        {
            int sum = 0;
            foreach (var item in searchusers)
                sum += item.UserInfo_Count;
            return sum;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchconfirmedorders.Clear();
            searchusers.Clear();
            Update();
            foreach (var item in confirmedorders)
            {
                if (item.OrderInfo_CreationDate == SearchTb.Text || item.OrderInfo_CreatorLogin == SearchTb.Text || item.OrderInfo_EndingDate == SearchTb.Text || item.OrderInfo_Number == SearchTb.Text || item.OrderInfo_Sum.ToString() == SearchTb.Text || item.OrderInfo_TransportMark == SearchTb.Text || item.OrderInfo_TransportModel == SearchTb.Text || item.OrderInfo_ID.ToString() == SearchTb.Text)
                    searchconfirmedorders.Add(item);
            }
            MainGrid.ItemsSource = searchconfirmedorders;
            foreach (var item in users)
            {
                if (item.UserInfo_ID.ToString() == SearchTb.Text || item.UserInfo_Login == SearchTb.Text || item.UserInfo_Name == SearchTb.Text || item.UserInfo_Surname == SearchTb.Text || item.UserInfo_IsAdmin.ToString() == SearchTb.Text || item.UserInfo_Count.ToString() == SearchTb.Text)
                    searchusers.Add(item);
            }
            MainGridUsers.ItemsSource = searchusers;
            Payment.Content = "Доход от зарегистрированной брони: " + PaySumSearched();
            OrderCount.Content = "Общее количество брони: " + OrderCountSumSearched();
        }

        private void UserCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var rowIndex = Convert.ToInt32(MainGridUsers.SelectedIndex);
            if (rowIndex >= 0)
            {
                var row = (UserInfo)MainGridUsers.Items[rowIndex];
                if (comboBox.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: Администратор")
                    mainWindow.TryToAdmin(true, row.UserInfo_ID);
                else
                    mainWindow.TryToAdmin(false, row.UserInfo_ID);
            }
        }
    }
}
