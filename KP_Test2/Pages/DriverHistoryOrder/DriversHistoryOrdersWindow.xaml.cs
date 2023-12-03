using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.TaxiDriverMenu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        public DriversHistoryOrdersWindow(Driver driver)
        {
            InitializeComponent();
            this.driver = driver; this.context = new();
            var id_driver = this.context.Drivers.Where(s => s.Iddriver == this.driver.Iddriver).First().Iddriver;
            this.HistoryView.ItemsSource = GetDriverHistory().ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(this.driver);
            taxiDriverMenuWindow.Show();
            Close();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = this.SearchTextBox.Text;

            this.HistoryView.ItemsSource = GetDriverHistory().
                Where(t => Microsoft.EntityFrameworkCore.EF.Functions.Like(t.Price.ToString()!, $"%{query}%")
                || Microsoft.EntityFrameworkCore.EF.Functions.Like(t.Routestart, $"%{query}%")
                || Microsoft.EntityFrameworkCore.EF.Functions.Like(t.Routeend, $"%{query}%")
                ).ToList();

        }

        private void SearchOnDate_Click(object sender, RoutedEventArgs e)
        {
            string query = this.SearchTextBox.Text;

            var dateOne = DateOnly.FromDateTime(DateTime.Parse(this.DateOne.Text));
            var dateTwo = DateOnly.FromDateTime(DateTime.Parse(this.DateTwo.Text));


            if (query == "")
            {
                var b = GetDriverHistory().Where(id => id.Idorder == 17).First();

                this.HistoryView.ItemsSource = GetDriverHistory().
                    Where(t => dateTwo >= DateOnly.FromDateTime((DateTime)t.Timeend!)
                        && DateOnly.FromDateTime((DateTime)t.Timestart!) >= dateOne).ToList();
            }
            else 
            this.HistoryView.ItemsSource = GetDriverHistory().
                Where(t => (Microsoft.EntityFrameworkCore.EF.Functions.Like(t.Price.ToString()!, $"%{query}%")
                || Microsoft.EntityFrameworkCore.EF.Functions.Like(t.Routestart, $"%{query}%")
                || Microsoft.EntityFrameworkCore.EF.Functions.Like(t.Routeend, $"%{query}%"))
                && (dateTwo >= DateOnly.FromDateTime((DateTime)t.Timeend!)
                        && DateOnly.FromDateTime((DateTime)t.Timestart!) >= dateOne)
                ).ToList();
        }

        private IIncludableQueryable<Historyorder,Driver?> GetDriverHistory()
        {
            return this.context.Historyorders.
                Where(s => s.Iddriver == this.driver.Iddriver).
                Include(u => u.IddriverNavigation);
        }
    }
}
