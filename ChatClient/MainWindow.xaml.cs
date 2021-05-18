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
            try
            {
                InitializeComponent();
            }
            catch { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new BeginPage(this));
            }
            catch { }
        }
        public void LoadLoginPage()
        {
            try
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new LoginPage(this));
            }
            catch { }
        }
        public void LoadRegisterPage()
        {
            try
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new RegisterPage(this));
            }
            catch { }
        }
        public void LoadBeginPage()
        {
            try
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new BeginPage(this));
            }
            catch { }
        }
        public void LoadClientMainPage()
        {
            try
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new ClientMainPage(this));
            }
            catch { }
        }
        public void LoadClientAddOrderPage()
        {
            try
            {
                LoadMarks();
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new AddOrderPage(this));
            }
            catch { }
        }
        public void LoadClientNotificationsPage()
        {
            try
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new NotificationsPage(this));
            }
            catch { }
        }
        public void LoadAdminMainPage()
        {
            try
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new AdminMainPage(this));
            }
            catch { }
        }
        public void LoadConfirmOrderPage()
        {
            try
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(new ConfirmOrderPage(this));
            }
            catch { }
        }
        public void ConnectUser()
        {
            try
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
            catch { }
        }
        public void TryToLogin(string login, string password)
        {
            try
            {
                if (isConnected && client != null)
                {
                    if (client.TryLogin(login, GetHash(password)))
                    {
                        isLogged = true;
                    }
                    else
                        MessageBox.Show("Неверный логин или пароль", "Проверьте логин или пароль", MessageBoxButton.OK);
                }
            }
            catch { }
        }
        public void TryToRegister(string login, string password, string name, string surname)
        {
            try
            {
                if (isConnected && client != null && CheckLogin(login) && CheckPassword(password) && CheckName(name) && CheckSurname(surname))
                {
                    if (client.TryRegister(login, GetHash(password), name, surname))
                        LoadBeginPage();
                    else
                        MessageBox.Show("Возможно пользователь с таким логином уже зарегистрирован", "Не удалось зарегистрировать пользователя", MessageBoxButton.OK);
                }
            }
            catch { }
        }
        public bool CheckLogin(string login)
        {
            try
            {
                string pattern = @"[a-zA-Z0-9]{" + login.Length + "}";
                if (Regex.IsMatch(login, pattern, RegexOptions.IgnoreCase) && login.Length > 4)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Логин должен состоять от 5 до 12 символов, иметь буквы или цифры", "Неверный логин", MessageBoxButton.OK);
                    return false;
                }
            }
            catch { return false; }
        }
        public bool CheckPassword(string password)
        {
            try
            {
                string pattern = @"[a-zA-Z0-9]{" + password.Length + "}";
                if (Regex.IsMatch(password, pattern, RegexOptions.IgnoreCase) && password.Length > 7)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Пароль должен состоять от 8 до 15 символов, иметь буквы или цифры", "Неверный пароль", MessageBoxButton.OK);
                    return false;
                }
            }
            catch { return false; }
        }
        public bool CheckName(string name)
        {
            try
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
            catch { return false; }
        }
        public bool CheckSurname(string surname)
        {
            try
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
            catch { return false; }
        }
        public void DisconnectUser()
        {
            try
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
            catch { }
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
            try
            {
                freec = msg;
            }
            catch { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (isConnected)
                    DisconnectUser();
            }
            catch { }
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
            try
            {
                if (isConnected && isLogged)
                    return client.SendModels(mark);
                else
                    return "";
            }
            catch { return ""; }
        }
        public int GetID()
        {
            try
            {
                if (isConnected && isLogged)
                    return client.GetUserID(login);
                else
                    return 0;
            }
            catch { return 0; }
        }
        public int GetTransportID(string mark, string model)
        {
            try
            {
                if (isConnected && isLogged)
                    return client.GetTransport(mark, model);
                else
                    return 0;
            }
            catch { return 0; }
        }
        public void TryToOrder(int transport, string number, int creator, string creationdate, string endingdate)
        {
            try
            {
                if (isConnected && client != null)
                {
                    if (client.TryOrder(transport, number, creator, creationdate, endingdate))
                        LoadClientMainPage();
                    else
                        MessageBox.Show("Возможно автомобиль с таким номером уже на парковке", "Не удалось забронировать", MessageBoxButton.OK);
                }
            }
            catch { }
        }
        public void MarksCallback(string msg)
        {
            try
            {
                marks = null;
                marks = JsonConvert.DeserializeObject<List<string>>(msg);
            }
            catch { }
        }
        public void ModelsCallback(string msg)
        {
            try
            {
                models = null;
                models = JsonConvert.DeserializeObject<List<string>>(msg);
            }
            catch { }
        }
        public string GetHash(string input)
        {
            try
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hash);
            }
            catch { return ""; }
        }
        public int GetFreeCount()
        {
            try
            {
                if (isConnected && isLogged)
                    return client.GetCount();
                else
                    return 0;
            }
            catch { return 0; }
        }
        public string GetNotifications(string login)
        {
            try
            {
                if (isConnected && isLogged)
                    return client.SendNotifications(login);
                else
                    return "";
            }
            catch { return ""; }
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
            try
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
            catch { return false; }
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
            try
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
            catch { }
        }
        public void TryToAdmin(bool isadmin,int id)
        {
            try
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
            catch { }
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
