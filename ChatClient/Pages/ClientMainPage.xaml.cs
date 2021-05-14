using ChatClient.ServiceParking;
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
        List<OrderInfo> orders = new List<OrderInfo>();
        List<OrderInfo> confirmedorders = new List<OrderInfo>();
        List<OrderInfo> notconfirmedorders = new List<OrderInfo>();
        List<OrderInfo> searchconfirmedorders = new List<OrderInfo>();
        List<OrderInfo> searchnotconfirmedorders = new List<OrderInfo>();
        MainWindow mainWindow;
        public ClientMainPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainWindow.Closing += Window_Closing;
            LoginInfo.Content = "Вы вошли как: " + mainWindow.login;
            CountInfo.Content = "Осталось мест: " + mainWindow.GetFreeCount();
            Payment.Content = "Итого к оплате: " + PaySum();
        }
        private double PaySum()
        {
            Update();
            double sum = 0;
            foreach (var item in confirmedorders)
                sum += item.OrderInfo_Sum;
            return sum;
        }
        private double PaySumSearched()
        {
            double sum = 0;
            foreach (var item in searchconfirmedorders)
                sum += item.OrderInfo_Sum;
            return sum;
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
            Payment.Content = "Итого к оплате: " + PaySum();
        }
        void Update()
        {
            confirmedorders.Clear();
            notconfirmedorders.Clear();
            orders = JsonConvert.DeserializeObject<List<OrderInfo>>(mainWindow.UpdateData());
            foreach (var item in orders)
            {
                if (item.OrderInfo_IsConfirmed)
                    confirmedorders.Add(item);
                else
                    notconfirmedorders.Add(item);
            }
            MainGrid.ItemsSource = confirmedorders;
            MainGridNotConfirmed.ItemsSource = notconfirmedorders;
        }

        private void ClientMainContol_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadClientAddOrderPage();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchconfirmedorders.Clear();
            searchnotconfirmedorders.Clear();
            Update();
            foreach(var item in confirmedorders)
            {
                if (item.OrderInfo_CreationDate == SearchTb.Text || item.OrderInfo_Creator.ToString() == SearchTb.Text || item.OrderInfo_EndingDate == SearchTb.Text || item.OrderInfo_Number == SearchTb.Text || item.OrderInfo_Sum.ToString() == SearchTb.Text || item.OrderInfo_TransportMark == SearchTb.Text || item.OrderInfo_TransportModel == SearchTb.Text)
                    searchconfirmedorders.Add(item);
            }
            MainGrid.ItemsSource = searchconfirmedorders;
            foreach (var item in notconfirmedorders)
            {
                if (item.OrderInfo_CreationDate == SearchTb.Text || item.OrderInfo_Creator.ToString() == SearchTb.Text || item.OrderInfo_EndingDate == SearchTb.Text || item.OrderInfo_Number == SearchTb.Text || item.OrderInfo_Sum.ToString() == SearchTb.Text || item.OrderInfo_TransportMark == SearchTb.Text || item.OrderInfo_TransportModel == SearchTb.Text)
                    searchnotconfirmedorders.Add(item);
            }
            MainGridNotConfirmed.ItemsSource = searchnotconfirmedorders;
            Payment.Content = "Итого к оплате: " + PaySumSearched();
        }
    }
}
