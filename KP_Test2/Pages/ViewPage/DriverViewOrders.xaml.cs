using KP_Test2.EF;
using KP_Test2.Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KP_Test2.Pages.ViewPage
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class DriverViewOrders : Page
    {
        private Driver driver;
        private TaxiKpContext context;
        public DriverViewOrders(Driver driver)
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
                orderTemp!.Iddriver = this.driver.Iddriver; orderTemp.Price = new Random().Next(100,1000);
                this.context.Historyorders.Update(orderTemp); this.context.SaveChanges();
            }

        }
    }
}
