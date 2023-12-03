using KP_Test2.EF;
using KP_Test2.Entities;
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
using System.Windows.Shapes;

namespace KP_Test2.Pages.RegistrationUserPage
{
    /// <summary>
    /// Логика взаимодействия для RegisterUserWindow.xaml
    /// </summary>
    public partial class RegisterUserWindow : Window
    {
        private readonly TaxiKpContext context;
        public RegisterUserWindow()
        {
            InitializeComponent();

            context = new TaxiKpContext();
        }


        private void RegUser_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPass.Text.Trim();
            string pass_entry = textBoxPass2.Text.Trim();
            string name = textBoxName.Text.Trim();
            string surname = textBoxSurname.Text.Trim();
            string contact = textBoxContact.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            int card = int.Parse(textBoxCard.Text.Trim());

            //if (login.Length < 4)
            //{
            //    textBoxLogin.ToolTip = "Логин должен содержать не менее 4 символов";
            //    textBoxLogin.Background = Brushes.Orange;
            //}
            //else if (pass.Length < 6)
            //{
            //    textBoxPass.ToolTip = "Пароль должен содержать не менее 6 символов";
            //    textBoxPass.Background = Brushes.Orange;
            //}
            //else if (pass_entry.Length < 6)
            //{
            //    textBoxPass2.ToolTip = "Пароль должен содержать не менее 6 символов";
            //    textBoxPass2.Background = Brushes.Orange;
            //}
            //else if (!email.Contains("@") && !email.Contains("."))
            //{
            //    textBoxEmail.ToolTip = "Электронная почта должна содержать @ и .";
            //    textBoxEmail.Background = Brushes.Orange;
            //}
            //else if (!card.Equals(4))
            //{
            //    textBoxCard.ToolTip = "Номер карты должен содержать не менее 4 символов";
            //    textBoxCard.Background = Brushes.Orange;
            //}
            //else {
            //    textBoxLogin.Background = Brushes.White;
            //    textBoxPass.Background = Brushes.White;
            //    textBoxPass2.Background = Brushes.White;
            //    textBoxEmail.Background = Brushes.White;
            //    textBoxCard.Background = Brushes.White;
            //}

            if (!pass.Equals(pass_entry))
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка");
                return;
            }

            var user = context.Usertaxis.Add(new Usertaxi()
            {
                Login = login,
                Password = pass,
                Name = name,
                Surname = surname,
                Contact = contact,
                Email = email,
                Card = card,
                Dateregistration = DateOnly.FromDateTime(DateTime.Now),
                Roletype = "user"
            });
            context.SaveChanges();

            var passengers = context.Passengers.Add(new Passenger()
            {
                Addresshome = "",
                Description = "",
                Iduser = user.Entity.Iduser,
                Rating = 5,
            });

            context.SaveChanges();
            MessageBox.Show("Регистрация успешно осуществлена");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }
    }
}
