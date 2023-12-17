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
        public DriverViewOrdersWindow(Driver driver)
        {
            InitializeComponent();
            this.driver = driver; this.context = new TaxiKpContext();
            LoadActivityOrder();
        }

        private void LoadActivityOrder()
        {
            var activityOrders = this.context.Historyorders.
                Where(o => o.Iddriver == null).
                Include(p => p.IdpassengerNavigation).
                ThenInclude(u => u.IduserNavigation).
                ToList();
            this.ListActivityOrders.ItemsSource = activityOrders;
        }

        private async void ListActivityOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] is Historyorder order)
            {
                var result = await new Service.ServiceRoute().GetRoute(order.Routestart, order.Routeend);

                var payment = 0f;

                if (float.TryParse(result, out var size))
                {
                    var t = ((float)size / 60f) * 60;
                    
                    if (t > 5)
                    {
                        payment = (float)150 + 11 * (t - 5);
                    }
                    else if (t < 0)
                    {
                        payment = 500;
                    }
                    else
                    {
                        payment = 100 * t;
                    }

                }
                else return;
                
                var route_to = await new Service.ServiceRoute().GetRoute(this.driver.Lastplace!, order.Routestart);

                var candidate = this.context.Userorders.Add(new Userorder()
                {
                    Iduser = order.Idpassenger,
                    Paysize = payment,
                    Route = route_to!,
                    Iddriver = this.driver.Iddriver
                });


                MessageBox.Show($"До клиента {route_to} км.\nЦена поездки: {payment}");

                this.context.SaveChanges();

                while (await context.Historyorders.Where(s => s.Price != null && s.Iddriver != null
                    && s.Timeend != null && s.Timestart != null
                    && order.Idorder == s.Idorder).FirstOrDefaultAsync() == null)
                {
                    var code = MessageBox.Show("Ожидание клиента","Отменить?",MessageBoxButton.YesNo);
                    if (code == MessageBoxResult.No)
                    {

                        if (await context.Historyorders.Where(s => s.Idorder == order.Idorder && s.Iddriver == driver.Iddriver).
                            FirstOrDefaultAsync() != null)
                        {
                            MessageBox.Show("Поездка уже осуществляется");
                            this.context.Userorders.RemoveRange(this.context.Userorders.Where(s => s.Iduser == order.Idpassenger));
                            this.context.SaveChanges();
                            LoadActivityOrder();
                            return;
                        }

                        var delete = this.context.Userorders.Where(s => s.Iddriver == this.driver.Iddriver).First();
                        this.context.Userorders.Remove(delete); this.context.SaveChanges();
                        return;
                    }
                }

                if (await context.Historyorders.Where(s => s.Idorder == order.Idorder && s.Iddriver == driver.Iddriver).
                    FirstOrDefaultAsync() != null)
                {
                    MessageBox.Show("Поездка состоялась");
                    this.context.Userorders.RemoveRange(this.context.Userorders.Where(s => s.Iduser == order.Idpassenger));
                    this.context.SaveChanges();
                    LoadActivityOrder();
                }
                else MessageBox.Show("Клиент предпочёл другого водителя");
            }
        }

        private void ChoosePrice_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(this.driver);
            taxiDriverMenuWindow.Show();
            Close();
        }

        public static class OrderInfo
        {
            public static int Price;
            public static DateTime TimeTo;
            public static DateTime TimeToEnd;

            public static void RefrashData()
            {
                Price = 0; TimeTo = DateTime.Now; TimeToEnd = DateTime.Now;
            }
        }
    }
}
