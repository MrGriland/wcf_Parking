using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EditAdminPage.xaml
    /// </summary>
    public partial class EditAdminPage : UserControl
    {
        MainWindow mainWindow;
        private object orderInfoObject;
        OrderInfo orderInfo;
        public int Transport;
        string bdate;
        string edate;
        public EditAdminPage(object sender, MainWindow mainWindow)
        {
            InitializeComponent();
            var dataInRow = (DataGridRow)sender;
            orderInfoObject = dataInRow.Item;
            this.mainWindow = mainWindow;
            MarkComboBox.ItemsSource = JsonConvert.DeserializeObject<List<string>>(mainWindow.LoadMarks());
            LoginInfo.Content = "Вы вошли как: " + mainWindow.login;
            CountInfo.Content = "Осталось мест: " + mainWindow.GetFreeCount();
            orderInfo = (OrderInfo)orderInfoObject;
            if (orderInfo.OrderInfo_IsConfirmed)
                DeleteButton.Visibility = Visibility.Visible;
        }

        private void MarkComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainWindow.models = null;
            ModelComboBox.ItemsSource = JsonConvert.DeserializeObject<List<string>>(mainWindow.LoadModels(MarkComboBox.SelectedItem.ToString()));
            ModelComboBox.IsEnabled = true;
        }

        private void ModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MarkComboBox.SelectedItem.ToString() != null && ModelComboBox.SelectedItem != null)
                Transport = mainWindow.GetTransportID(MarkComboBox.SelectedItem.ToString(), ModelComboBox.SelectedItem.ToString());
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadAdminMainPage();
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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            MarkComboBox.Text = orderInfo.OrderInfo_TransportMark;
            ModelComboBox.ItemsSource = JsonConvert.DeserializeObject<List<string>>(mainWindow.LoadModels(MarkComboBox.SelectedItem.ToString()));
            ModelComboBox.IsEnabled = true;
            ModelComboBox.Text = orderInfo.OrderInfo_TransportModel;
            FPNumber.Text = orderInfo.OrderInfo_Number.Substring(0, 4);
            SPNumber.Text = orderInfo.OrderInfo_Number.Substring(4, 2);
            TPNumber.Text = orderInfo.OrderInfo_Number.Substring(7, 1);
            BDate.Text = orderInfo.OrderInfo_CreationDate.Substring(0, 10);
            BTime.Text = orderInfo.OrderInfo_CreationDate.Substring(11, 5);
            Date.Text = orderInfo.OrderInfo_EndingDate.Substring(0, 10);
            Time.Text = orderInfo.OrderInfo_EndingDate.Substring(11, 5);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTransport() && CheckNumber())
            {
                FPage.Visibility = Visibility.Collapsed;
                NotifyPage.Visibility = Visibility.Collapsed;
                SPage.Visibility = Visibility.Visible;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            FPage.Visibility = Visibility.Visible;
            SPage.Visibility = Visibility.Collapsed;
            NotifyPage.Visibility = Visibility.Collapsed;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDate())
            {
                string number = FPNumber.Text + SPNumber.Text + "-" + TPNumber.Text;
                bdate = BDate.Text + " " + BTime.Text;
                edate = Date.Text + " " + Time.Text;
                MessageBoxResult response = MessageBox.Show("Вы уверены?", "Изменение брони", MessageBoxButton.YesNo);
                switch (response)
                {
                    case MessageBoxResult.Yes:
                        mainWindow.TryUpdateConfirmed(Transport, number, bdate, edate, orderInfo.OrderInfo_IsConfirmed,orderInfo.OrderInfo_ID); break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult response = MessageBox.Show("Вы уверены?", "Отмена брони", MessageBoxButton.YesNo);
            switch (response)
            {
                case MessageBoxResult.Yes:
                    mainWindow.TryToDeleteUnconfirmed(orderInfo.OrderInfo_ID); break;
                case MessageBoxResult.No:
                    break;
            }
        }
        public bool CheckTransport()
        {
            if (MarkComboBox.SelectedItem != null)
            {
                if (ModelComboBox.SelectedItem != null)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Выберите модель транспорта", "Выберите транспорт", MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Выберите марку транспорта", "Выберите транспорт", MessageBoxButton.OK);
                return false;
            }
        }
        public bool CheckNumber()
        {
            string pattern1 = @"\d{4}";
            if (Regex.IsMatch(FPNumber.Text, pattern1, RegexOptions.IgnoreCase))
            {
                string pattern2 = @"^[ABEIKMHOPCTX]{2}$";
                if (Regex.IsMatch(SPNumber.Text, pattern2, RegexOptions.IgnoreCase))
                {
                    string pattern3 = @"^[0-7]{1}";
                    if (Regex.IsMatch(TPNumber.Text, pattern3, RegexOptions.IgnoreCase))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Третья часть номера должна содержать цифру", "Неверный номер", MessageBoxButton.OK);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Вторая часть номера должна содержать буквы", "Неверный номер", MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Первая часть номера должна содержать цифры", "Неверный номер", MessageBoxButton.OK);
                return false;
            }
        }
        public bool CheckDate()
        {
            if (BDate.Text != "")
            {
                if (BTime.Text != "")
                {
                    if (Date.Text != "")
                    {
                        if (Time.Text != "")
                        {
                            if ((Convert.ToDateTime(BDate.Text + " " + BTime.Text) - DateTime.Now).TotalMinutes > 0)
                            {
                                if ((Convert.ToDateTime(Date.Text + " " + Time.Text) - Convert.ToDateTime(BDate.Text + " " + BTime.Text)).TotalMinutes > 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show("Вы не можете выехать раньше чем заехать", "Неправильная дата", MessageBoxButton.OK);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Вам необходимо выбрать дату или время не из прошлого", "Неправильная дата", MessageBoxButton.OK);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выберите время выезда", "Неправильная дата", MessageBoxButton.OK);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите дату выезда", "Неправильная дата", MessageBoxButton.OK);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите время заезда", "Неправильная дата", MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Выберите дату заезда", "Неправильная дата", MessageBoxButton.OK);
                return false;
            }
        }

        private void NotifyButton_Click(object sender, RoutedEventArgs e)
        {
            FPage.Visibility = Visibility.Collapsed;
            SPage.Visibility = Visibility.Collapsed;
            NotifyPage.Visibility = Visibility.Visible;
        }

        private void SendNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.TryNotify(NotificationTB.Text, orderInfo.OrderInfo_ID);
            NotificationTB.Text = "";
            FPage.Visibility = Visibility.Visible;
            SPage.Visibility = Visibility.Collapsed;
            NotifyPage.Visibility = Visibility.Collapsed;
        }
    }
}
