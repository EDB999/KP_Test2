using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.TaxiUserMenuPage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        private readonly Passenger passenger;
        public HistoryOrdersWindow(Usertaxi user)
        {
            InitializeComponent();
            this.user = user; this.context = new();
            passenger = this.context.Passengers.Where(s => s.Iduser == user.Iduser).First();
            this.HistoryView.ItemsSource = this.context.Historyorders.
                Where(s => s.Idpassenger == passenger.Idpassenger).
                Include(f => f.IddriverNavigation).
                ToList();
        }

        private IIncludableQueryable<Historyorder, Driver?> GetDriverHistory()
        {
            return this.context.Historyorders.
                Where(s => s.Idpassenger == this.passenger.Idpassenger).
                Include(u => u.IddriverNavigation);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiUserMenuWindow taxiUserMenuWindow = new TaxiUserMenuWindow(this.user);
            taxiUserMenuWindow.Show(); 
            Close(); 
        }

        private void SearchOnDate_Click(object sender, RoutedEventArgs e)
        {
            string query = this.SearchTextBox.Text;

            try
            {
                var dateOne = DateOnly.FromDateTime(DateTime.Parse(this.DateOne.Text));
                var dateTwo = DateOnly.FromDateTime(DateTime.Parse(this.DateTwo.Text));

                if (query == "")
                {
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
            catch { MessageBox.Show("Ошибка"); return; }
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
    }
}
