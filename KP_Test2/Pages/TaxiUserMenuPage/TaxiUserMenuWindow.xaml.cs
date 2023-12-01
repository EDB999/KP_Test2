using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.HistoryOrdersPage;
using KP_Test2.Pages.MakeAnOrder;
using KP_Test2.Pages.PersonalAccount;
using KP_Test2.Pages.RegDriver;
using KP_Test2.Pages.TaxiDriverMenu;
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

namespace KP_Test2.Pages.TaxiUserMenuPage
{
    /// <summary>
    /// Логика взаимодействия для TaxiUserMenuWindow.xaml
    /// </summary>
    public partial class TaxiUserMenuWindow : Window
    {
        private Usertaxi user;
        private TaxiKpContext context;
        private bool isDriver;
        public TaxiUserMenuWindow(Usertaxi user)
        {
            InitializeComponent();
            this.user = user; this.context = new();
            isDriver = this.context.Usertaxis.Where(id => id.Iduser == this.user.Iduser).
                FirstOrDefault()!.Roletype == "user" ? true : false;

            if (isDriver) this.DriverAccount.Visibility = Visibility.Visible;
            else this.DriverLink.Visibility = Visibility.Visible;

        }

        private void HistoryOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryOrdersWindow historyOrdersWindow = new HistoryOrdersWindow(this.user);
            historyOrdersWindow.Show();
            Hide();
        }

        private void PersonalAccountButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalAccountWindow personalAccountWindow = new PersonalAccountWindow(this.user);
            personalAccountWindow.Show();
            Hide();
        }

        private void MakeAnOrderButton_Click(object sender, RoutedEventArgs e)
        {
            MakeAnOrderWindow makeAnOrderWindow = new MakeAnOrderWindow(this.context.Passengers.
                Where(s => s.Iduser == user.Iduser).FirstOrDefault()!);
            makeAnOrderWindow.Show();
            Hide();
        }

        private void DriverAccount_Click(Object sender,RoutedEventArgs e)
        {
            var driver = this.context.Drivers.Where(id => id.Iduser == this.user.Iduser).FirstOrDefault();
            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(driver!);
            taxiDriverMenuWindow.Show();
            Hide();
        }

        private void DriverLink_Click(object sender, RoutedEventArgs e)
        {
            RegDriverWindow regDriverWindow = new RegDriverWindow(user);
            regDriverWindow.Show();
            Hide();
        }
    }
}
