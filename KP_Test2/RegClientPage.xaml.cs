using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.UserPage;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Логика взаимодействия для RegClientPage.xaml
    /// </summary>
    public partial class RegClientPage : Page
    {

        private readonly TaxiKpContext context;

        public RegClientPage()
        {
            InitializeComponent();

            context = new TaxiKpContext();
        }

        private void RegClientClick(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPass.Text.Trim();
            string pass_entry = textBoxPass2.Text.Trim();
            string name = textBoxName.Text.Trim();
            string surname = textBoxSurname.Text.Trim();
            string contact = textBoxContact.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            int card = int.Parse(textBoxCard.Text.Trim());

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
            MessageBox.Show("Регистрация успешно осуществлена");
            Content = new TaxiUserPage(user.Entity);
        }
    }
}
