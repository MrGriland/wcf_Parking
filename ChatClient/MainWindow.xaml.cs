using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceChat;
using Newtonsoft.Json;
using ChatClient.Resources;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        public string login;
        public List<OrderInfo> orderInfos;
        public bool isConnected = false;
        public bool isLogged = false;
        ServiceChatClient client;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new BeginPage(this));
        }
        public void LoadLoginPage()
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new LoginPage(this));
        }
        public void LoadBeginPage()
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new BeginPage(this));
        }
        public void LoadClientMainPage()
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new ClientMainPage(this));
        }
        public void ConnectUser()
        {
            if (!isConnected)
            {
                if (!isConnected)
                {
                    try
                    {
                        client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                        client.Connect();
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось подключиться к серверу", "Что-то пошло не так", MessageBoxButton.OK);
                        return;
                    }
                }
                UpdateData();
                isConnected = true;
            }
        }
        public void TryToLogin(string login, string password)
        {
            if (isConnected && client!=null)
            {
                if (client.TryLogin(login, password))
                    isLogged = true;
                else
                    MessageBox.Show("Неверный логин или пароль", "Проверьте логин или пароль", MessageBoxButton.OK);
            }
        }
        public void DisconnectUser()
        {
            isLogged = false;
            login = null;
            orderInfos = null;
            if (isConnected)
            {
                try
                {
                    client.Disconnect();
                }
                catch { }
                client = null;
                isConnected = false;
            }
        }
        public void UpdateData()
        {
            try
            {
                if (client != null && login != null)
                {
                    client.SendMsg(login);
                }
            }
            catch { }
        }

        public void MsgCallback(string msg)
        {
            orderInfos = null;
            orderInfos = JsonConvert.DeserializeObject<List<OrderInfo>>(msg);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isConnected)
                DisconnectUser();
        }
    }
}
