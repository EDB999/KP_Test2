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

        TaxiKpContext db;


        public RegClientPage()
        {
            InitializeComponent();

            db = new TaxiKpContext();
            
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
            Client client = new Client(login, pass, pass_entry, name, surname, contact, email, card);
            db.Clients.Add(client);
            db.SaveChanges();
            MessageBox.Show("Регистрация успешно осуществлена");
        }
    }
}
