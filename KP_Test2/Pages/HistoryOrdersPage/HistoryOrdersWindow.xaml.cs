using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.TaxiUserMenuPage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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

namespace KP_Test2.Pages.HistoryOrdersPage
{
    /// <summary>
    /// Логика взаимодействия для HistoryOrdersWindow.xaml
    /// </summary>
    public partial class HistoryOrdersWindow : Window
    {
        private readonly Usertaxi user;
        private readonly TaxiKpContext context;
        public HistoryOrdersWindow(Usertaxi user)
        {
            InitializeComponent();
            this.user = user; this.context = new();
            var id_pass = this.context.Passengers.Where(s => s.Iduser == user.Iduser).First().Iduser;
            this.HistoryView.ItemsSource = this.context.Historyorders.
                Where(s => s.Idpassenger == id_pass).
                Include(f => f.IddriverNavigation).
                ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiUserMenuWindow taxiUserMenuWindow = new TaxiUserMenuWindow(this.user);
            taxiUserMenuWindow.Show(); 
            Close(); 
        }
    }
}
