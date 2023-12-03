using KP_Test2.EF;
using KP_Test2.Pages.RegistrationUserPage;
using KP_Test2.Pages.TaxiUserMenuPage;
using KP_Test2.Pages.UserPage;
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

namespace KP_Test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsVisibly { get; set; } = false;
        private TaxiKpContext context;
        public MainWindow()
        {
            InitializeComponent();
            context = new TaxiKpContext();
            
        }

        private void RegLink_Click(object sender, RoutedEventArgs e)
        {
            RegisterUserWindow registerUserWindow = new RegisterUserWindow();
            registerUserWindow.Show();
            Hide();
        }
        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            var user = context.Usertaxis.
                Where(n => n.Login == this.LoginBox.Text && n.Password == this.PasswordBox.Password).
                FirstOrDefault();
            if (user == null) { MessageBox.Show("Данного пользователя не существует", "Ошибка"); return; }
            else { TaxiUserMenuWindow taxiUserMenuWindow = new TaxiUserMenuWindow(user);
                taxiUserMenuWindow.Show();
                Hide();
            }
        }

        private void ShowPasswordRadio_Click(object sender, RoutedEventArgs e)
        {
            if (!IsVisibly)
            {
                this.PasswordBox.Visibility = Visibility.Collapsed;
                this.PasswordTextBox.Visibility = Visibility.Visible;
                IsVisibly = true; this.PasswordTextBox.Text = this.PasswordBox.Password;
            }
            else
            {
                this.PasswordTextBox.Visibility = Visibility.Collapsed;
                this.PasswordBox.Visibility = Visibility.Visible;
                IsVisibly = false; this.PasswordBox.Password = this.PasswordTextBox.Text;
            }
        }
    }
}
