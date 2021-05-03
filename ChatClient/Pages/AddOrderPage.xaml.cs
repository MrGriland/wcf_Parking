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
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : UserControl
    {
        MainWindow mainWindow;
        public List<string> models = null;
        public int Transport;
        string edate;
        public AddOrderPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            MarkComboBox.ItemsSource = JsonConvert.DeserializeObject<List<string>>(mainWindow.LoadMarks());
            //tb.Text = creationdate;
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadClientMainPage();
            Transport = 0;
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.DisconnectUser();
            mainWindow.LoadBeginPage();
        }

        private void MarkComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainWindow.models = null;
            ModelComboBox.ItemsSource = JsonConvert.DeserializeObject<List<string>>(mainWindow.LoadModels(MarkComboBox.SelectedItem.ToString()));
            ModelComboBox.IsEnabled = true;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            string number = FPNumber.Text + SPNumber.Text + "-" + TPNumber.Text;
            edate = Date.Text + " " + Time.Text;
            tb.Text = edate;
            mainWindow.TryToOrder(Transport, number, mainWindow.UserID, DateTime.Now.ToString(), edate);
        }

        private void ModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Transport = mainWindow.GetTransportID(MarkComboBox.SelectedItem.ToString(), ModelComboBox.SelectedItem.ToString());
        }
    }
}
