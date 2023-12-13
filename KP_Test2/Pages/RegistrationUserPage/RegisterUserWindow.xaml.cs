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
using System.Windows.Media.Animation;

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

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 450;
            doubleAnimation.Duration = TimeSpan.FromSeconds(3);
            RegUser.BeginAnimation(Button.WidthProperty, doubleAnimation);
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

            bool isValidated = true;

            if (login.Length < 3)
            {
                textBoxLogin.ToolTip = "Логин должен содержать не менее 3 символов";
                textBoxLogin.Background = Brushes.Orange;
                MessageBox.Show("Логин введён неверно", "Ошибка");
                isValidated = false;
            }
            else textBoxLogin.Background = Brushes.White;

            if (pass.Length < 6)
            {
                textBoxPass.ToolTip = "Пароль должен содержать не менее 6 символов";
                textBoxPass.Background = Brushes.Orange;
                MessageBox.Show("Пароль введён неверно", "Ошибка");
                isValidated = false;
            }
            else textBoxPass.Background = Brushes.White;

            if (pass_entry.Length < 6)
            {
                textBoxPass2.ToolTip = "Подтверждение пароля должно содержать не менее 6 символов";
                textBoxPass2.Background = Brushes.Orange;
                MessageBox.Show("Подтверждение пароля введено неверно", "Ошибка");
                isValidated = false;
            }
            else textBoxPass2.Background = Brushes.White;

            if (!contact.Contains("+"))
            {
                textBoxContact.ToolTip = "Пароль должен содержать не менее 6 символов";
                textBoxContact.Background = Brushes.Orange;
                MessageBox.Show("Телефоный номер введён неверно", "Ошибка");
                isValidated = false;
            }
            else textBoxContact.Background = Brushes.White;

            if (!email.Contains("@") && !email.Contains("."))
            {
                textBoxEmail.ToolTip = "Электронная почта должна содержать @ и .";
                textBoxEmail.Background = Brushes.Orange;
                MessageBox.Show("Электронная почта введена неверно", "Ошибка");
                isValidated = false;

            } else textBoxEmail.Background = Brushes.White;


            if (!pass.Equals(pass_entry))
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка");
                return;
                isValidated = false;
            }

            if (isValidated)
            {
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
}
