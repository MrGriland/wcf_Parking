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
using ChatClient.ServiceParking;
using Newtonsoft.Json;
using ChatClient.Resources;
using ChatClient.Pages;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceParkingCallback
    {
        public string freec;
        public string login;
        public int UserID;
        public List<OrderInfo> orderInfos = null;
        public List<string> marks = null;
        public List<string> models = null;
        public bool isConnected = false;
        public bool isLogged = false;
        ServiceParkingClient client;
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
        public void LoadRegisterPage()
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new RegisterPage(this));
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
        public void LoadClientAddOrderPage()
        {
            LoadMarks();
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new AddOrderPage(this));
        }
        public void LoadClientNotificationsPage()
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new NotificationsPage(this));
        }
        public void LoadAdminMainPage()
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new AdminMainPage(this));
        }
        public void LoadConfirmOrderPage()
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new ConfirmOrderPage(this));
        }
        public void ConnectUser()
        {
            if (!isConnected)
            {
                if (!isConnected)
                {
                    try
                    {
                        client = new ServiceParkingClient(new System.ServiceModel.InstanceContext(this));
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
                if (client.TryLogin(login, GetHash(password)))
                {
                    isLogged = true;
                }
                else
                    MessageBox.Show("Неверный логин или пароль", "Проверьте логин или пароль", MessageBoxButton.OK);
            }
        }
        public void TryToRegister(string login, string password, string name, string surname)
        {
            if (isConnected && client != null && CheckLogin(login) && CheckPassword(password) && CheckName(name) && CheckSurname(surname))
            {
                if (client.TryRegister(login, GetHash(password), name, surname))
                    LoadBeginPage();
                else
                    MessageBox.Show("Возможно пользователь с таким логином уже зарегистрирован", "Не удалось зарегистрировать пользователя", MessageBoxButton.OK);
            }
        }
        public bool CheckLogin(string login)
        {
            string pattern = @"\w{5,12}";
            if (Regex.IsMatch(login, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Логин должен состоять от 5 до 12 символов, иметь буквы или цифры", "Неверный логин", MessageBoxButton.OK);
                return false;
            }
        }
        public bool CheckPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$";
            if (Regex.IsMatch(password, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Пароль должен состоять от 8 до 15 символов, иметь буквы и цифры", "Неверный пароль", MessageBoxButton.OK);
                return false;
            }
        }
        public bool CheckName(string name)
        {
            string pattern = @"^[А-Яа-я]{2,30}$";
            if (Regex.IsMatch(name, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Имя должно состоять от 2 до 30 символов, иметь буквы", "Неверное имя", MessageBoxButton.OK);
                return false;
            }
        }
        public bool CheckSurname(string surname)
        {
            string pattern = @"^[А-Яа-я]{2,30}$";
            if (Regex.IsMatch(surname, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Фамилия должна состоять от 2 до 30 символов, иметь буквы", "Неверная фамилия", MessageBoxButton.OK);
                return false;
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
        public string UpdateData()
        {
            try
            {
                if (client != null && login != null)
                {
                    return client.SendDB(login);
                }
                else
                    return "";
            }
            catch { return ""; }
        }

        public void MsgCallback(string msg)
        {
            freec = msg;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isConnected)
                DisconnectUser();
        }
        public string LoadMarks()
        {
            try
            {
                if (isConnected && isLogged)
                    return client.SendMarks();
                else
                    return "";
            }
            catch { return ""; }
        }
        public string LoadModels(string mark)
        {
            if (isConnected && isLogged)
                return client.SendModels(mark);
            else
                return "";
        }
        public int GetID()
        {
            if (isConnected && isLogged)
                return client.GetUserID(login);
            else
                return 0;
        }
        public int GetTransportID(string mark, string model)
        {
            if (isConnected && isLogged)
                return client.GetTransport(mark, model);
            else
                return 0;
        }
        public void TryToOrder(int transport, string number, int creator, string creationdate, string endingdate)
        {
            if (isConnected && client != null)
            {
                if (client.TryOrder(transport, number, creator, creationdate, endingdate))
                    LoadClientMainPage();
                else
                    MessageBox.Show("Возможно автомобиль с таким номером уже на парковке", "Не удалось забронировать", MessageBoxButton.OK);
            }
        }
        public void MarksCallback(string msg)
        {
            marks = null;
            marks = JsonConvert.DeserializeObject<List<string>>(msg);
        }
        public void ModelsCallback(string msg)
        {
            models = null;
            models = JsonConvert.DeserializeObject<List<string>>(msg);
        }
        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
        public int GetFreeCount()
        {
            if (isConnected && isLogged)
                return client.GetCount();
            else
                return 0;
        }
        public string GetNotifications(string login)
        {
            if (isConnected && isLogged)
                return client.SendNotifications(login);
            else
                return "";
        }
        public bool ClearThisNotification(int id)
        {
            try
            {
                if (isConnected && isLogged)
                    return client.ClearNotification(id);
                else
                    return false;
            }
            catch { return false; }
        }
        public void TryUpdateConfirmed(int transport, string number, string creationdate, string endingdate, bool isConfirmed,int id)
        {
            try
            {
                if (isConnected && isLogged)
                    if (client.ChangeConfirmed(transport, number, creationdate, endingdate, isConfirmed, id))
                        if (IsAdmin())
                            LoadAdminMainPage();
                        else
                            LoadClientMainPage();
                    else
                        MessageBox.Show("Возможно автомобиль с таким номером уже на парковке", "Не удалось изменить бронь", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("Возможно автомобиль с таким номером уже на парковке", "Не удалось изменить бронь", MessageBoxButton.OK);
            }
        }
        public void TryToDeleteUnconfirmed(int id)
        {
            try
            {
                if (isConnected && isLogged)
                    if (client.DeleteUnconfirmed(id))
                        if (IsAdmin())
                            LoadAdminMainPage();
                        else
                            LoadClientMainPage();
                    else
                        MessageBox.Show("Что-то пошло не так", "Не удалось отменить бронь", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Не удалось отменить бронь", MessageBoxButton.OK);
            }
        }
        public bool IsAdmin()
        {
            if (isConnected && isLogged)
            {
                if (client.IsAdmin(login))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public string UpdateUsersData()
        {
            try
            {
                if (client != null && isConnected && isLogged)
                {
                    return client.SendUsers();
                }
                else
                    return "";
            }
            catch { return ""; }
        }
        public void TryToConfirmOrder(int id)
        {
            if (isConnected && isLogged)
            {
                if (client.TryToConfirm(id))
                    LoadAdminMainPage();
                else
                    MessageBox.Show("Что-то пошло не так", "Не удалось подтвердить бронь", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Что-то пошло не так", "Не удалось подтвердить бронь", MessageBoxButton.OK);
        }
        public void TryToAdmin(bool isadmin,int id)
        {
            if (isConnected && isLogged)
            {
                if (client.TryToAdmin(isadmin, id)) { }
                else
                    MessageBox.Show("Что-то пошло не так", "Не удалось изменить права", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Что-то пошло не так", "Не удалось изменить права", MessageBoxButton.OK);
        }
        public void TryNotify(string message, int id)
        {
            try
            {
                if (isConnected && isLogged)
                {
                    if (client.TryToNotify(message, id)) { }
                    else
                        MessageBox.Show("Что-то пошло не так", "Не удалось оповестить пользователя", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Что-то пошло не так", "Не удалось оповестить пользователя", MessageBoxButton.OK);
            }
            catch { }
        }
    }
}
