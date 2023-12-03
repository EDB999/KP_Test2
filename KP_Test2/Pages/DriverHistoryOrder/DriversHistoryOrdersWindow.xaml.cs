using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.TaxiDriverMenu;
using Microsoft.EntityFrameworkCore;
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

namespace KP_Test2.Pages.DriverHistoryOrder
{
    /// <summary>
    /// Логика взаимодействия для DriversHistoryOrdersWindow.xaml
    /// </summary>
    public partial class DriversHistoryOrdersWindow : Window
    {
        private Driver driver;
        private TaxiKpContext context;
        private Usertaxi user;
        public DriversHistoryOrdersWindow(Driver driver)
        {
            InitializeComponent();
            this.driver = driver; this.context = new();
            var id_driver = this.context.Drivers.Where(s => s.Iduser == user.Iduser).First().Iduser;
            this.HistoryView.ItemsSource = this.context.Historyorders.
                Where(s => s.Iddriver == id_driver).
                Include(f => f.IddriverNavigation).
                ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(this.driver);
            taxiDriverMenuWindow.Show();
            Close();
        }
    }
}
