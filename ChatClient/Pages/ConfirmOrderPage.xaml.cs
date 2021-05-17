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
    /// Логика взаимодействия для ConfirmOrderPage.xaml
    /// </summary>
    public partial class ConfirmOrderPage : UserControl
    {
        MainWindow mainWindow;
        List<OrderInfo> orders = new List<OrderInfo>();
        List<OrderInfo> unconfirmedorders = new List<OrderInfo>();
        List<OrderInfo> searchunconfirmedorders = new List<OrderInfo>();
        public ConfirmOrderPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            LoginInfo.Content = "Вы вошли как: " + mainWindow.login;
            CountInfo.Content = "Осталось мест: " + mainWindow.GetFreeCount();
            Payment.Content = "Доход от незарегистрированной брони: " + PaySum();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.DisconnectUser();
            mainWindow.LoadBeginPage();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadAdminMainPage();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
            Payment.Content = "Доход от незарегистрированной брони: " + PaySum();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchunconfirmedorders.Clear();
            Update();
            foreach (var item in unconfirmedorders)
            {
                if (item.OrderInfo_CreationDate == SearchTb.Text || item.OrderInfo_CreatorLogin == SearchTb.Text || item.OrderInfo_EndingDate == SearchTb.Text || item.OrderInfo_Number == SearchTb.Text || item.OrderInfo_Sum.ToString() == SearchTb.Text || item.OrderInfo_TransportMark == SearchTb.Text || item.OrderInfo_TransportModel == SearchTb.Text || item.OrderInfo_ID.ToString() == SearchTb.Text)
                    searchunconfirmedorders.Add(item);
            }
            MainGrid.ItemsSource = searchunconfirmedorders;
            Payment.Content = "Доход от незарегистрированной брони: " + PaySumSearched();
        }

        private void UnconfirmedControl_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }
        private double PaySum()
        {
            Update();
            double sum = 0;
            foreach (var item in unconfirmedorders)
                sum += item.OrderInfo_Sum;
            return sum;
        }
        void Update()
        {
            MainGrid.ItemsSource = searchunconfirmedorders;
            unconfirmedorders.Clear();
            orders = JsonConvert.DeserializeObject<List<OrderInfo>>(mainWindow.UpdateData());
            foreach (var item in orders)
            {
                if (!item.OrderInfo_IsConfirmed)
                    unconfirmedorders.Add(item);
            }
            MainGrid.ItemsSource = unconfirmedorders;
        }
        private double PaySumSearched()
        {
            double sum = 0;
            foreach (var item in searchunconfirmedorders)
                sum += item.OrderInfo_Sum;
            return sum;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            var rowIndex = Convert.ToInt32(MainGrid.SelectedIndex); 
            var row = (OrderInfo)MainGrid.Items[rowIndex];
            mainWindow.TryToConfirmOrder(row.OrderInfo_ID);
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mainWindow.MainGrid.Children.Clear();
            mainWindow.MainGrid.Children.Add(new EditAdminPage(sender, mainWindow));
        }
    }
}
