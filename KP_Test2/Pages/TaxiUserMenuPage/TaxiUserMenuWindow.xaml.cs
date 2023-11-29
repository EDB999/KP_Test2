using KP_Test2.Pages.HistoryOrdersPage;
using KP_Test2.Pages.MakeAnOrder;
using KP_Test2.Pages.PersonalAccount;
using KP_Test2.Pages.RegDriver;
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
        public TaxiUserMenuWindow()
        {
            InitializeComponent();
        }

        private void HistoryOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            Content = new HistoryOredersPage();
        }

        private void PersonalAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Content = new PersonalAccountPage();
        }

        private void MakeAnOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Content = new MakeAnOrderPage();

        }

        private void DriverLink_Click(object sender, RoutedEventArgs e)
        {
            RegDriverWindow regDriverWindow = new RegDriverWindow();
            regDriverWindow.Show();
            Hide();
        }
    }
}
