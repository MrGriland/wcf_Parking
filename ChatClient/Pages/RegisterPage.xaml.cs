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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : UserControl
    {
        MainWindow mainWindow;
        public RegisterPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.LoadBeginPage();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ConnectUser();
            mainWindow.TryToRegister(Tblogin.Text, Tbpassword.Text, TbName.Text, TbSurname.Text);
        }
    }
}
