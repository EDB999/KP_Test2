using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.PriceAndPlace;
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

namespace KP_Test2.Pages.ViewPage
{
    /// <summary>
    /// Логика взаимодействия для DriverViewOrdersWindow.xaml
    /// </summary>
    public partial class DriverViewOrdersWindow : Window
    {
        private Driver driver;
        private TaxiKpContext context;
        private readonly Usertaxi user;
        public DriverViewOrdersWindow(Driver driver)
        {
            InitializeComponent();
            this.driver = driver; this.context = new TaxiKpContext();
            var activityOrders = this.context.Historyorders.
                Where(o => o.Iddriver == null).
                Include(p => p.IdpassengerNavigation).
                ThenInclude(u => u.IduserNavigation).
                ToList();
            this.ListActivityOrders.ItemsSource = activityOrders;
        }

        private void ListActivityOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] is Historyorder order)
            {
                var orderTemp = this.context.Historyorders.Where(o => o.Idorder == order.Idorder).FirstOrDefault();
                orderTemp!.Iddriver = this.driver.Iddriver; orderTemp.Price = new Random().Next(100, 1000);
                this.context.Historyorders.Update(orderTemp); this.context.SaveChanges();
            }
        }

        private void ChoosePrice_Click(object sender, RoutedEventArgs e)
        {
            PriceAndPlaceWindow priceAndPlaceWindow = new PriceAndPlaceWindow();
            priceAndPlaceWindow.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(this.driver);
            taxiDriverMenuWindow.Show();
            Close();
        }
    }
}
