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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        MainWindow mainWindow;
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ConnectUser();
            if(Tblogin!=null)
                mainWindow.TryToLogin(Tblogin.Text, Tbpassword.Text);
            mainWindow.login = Tblogin.Text;
            if(mainWindow.isLogged)
             mainWindow.LoadClientMainPage();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadBeginPage();
        }
    }
}
