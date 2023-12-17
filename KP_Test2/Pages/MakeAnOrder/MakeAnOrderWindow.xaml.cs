using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.TaxiUserMenuPage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KP_Test2.Pages.MakeAnOrder
{
    /// <summary>
    /// Логика взаимодействия для MakeAnOrderWindow.xaml
    /// </summary>
    public partial class MakeAnOrderWindow : Window
    {
        private Passenger passenger;
        private TaxiKpContext context;
        private Historyorder? order;
        private bool cancel = false;
        private bool success = false;
        public MakeAnOrderWindow(Passenger passenger)
        {
            InitializeComponent();
            this.passenger = passenger;
            this.context = new();
        }

        private async void MakeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            this.WaitOrder.Visibility = Visibility.Visible; 
            this.CancelButton.Visibility = Visibility.Visible;
            order = this.context.Historyorders.Add(new Historyorder()
            {
                Idpassenger = this.passenger.Idpassenger,
                Routestart = this.textBoxAdress.Text,
                Routeend = this.textBoxToAdress.Text,
            }).Entity;
            this.context.SaveChanges();
            var result = await new Service.ServiceRoute().GetRoute(this.textBoxAdress.Text, this.textBoxToAdress.Text);


            ShowRouteText.Visibility = Visibility.Visible;
            ShowRouteText.Content = $"Расстояние маршрута: {result}";

            while (await context.Historyorders.Where(s => s.Price != null && s.Iddriver != null
            && s.Timeend != null && s.Timestart != null
            && order.Idorder == s.Idorder).FirstOrDefaultAsync() == null)
            {
                if (DateTime.Now.Second % 10 == 0)
                {
                var drivers = await context.Userorders.Where(p => p.Iduser == passenger.Iduser).Include(s => s.IddriverNavigation).ToListAsync();
                this.DriverView.ItemsSource = drivers;
                }
                this.DriverView.Visibility = Visibility.Visible;
                if (cancel == true)
                {
                    this.WaitOrder.Visibility = Visibility.Hidden;
                    this.CancelButton.Visibility = Visibility.Hidden;

                    var temp_order = this.context.Historyorders.
                        Where(s => s.Idorder == order.Idorder).
                        FirstOrDefault();
                    this.context.Historyorders
                        .Remove(temp_order!); 
                    this.context.SaveChanges();

                    return;
                }
            }

            this.WaitOrder.Visibility = Visibility.Hidden;
            ShowRouteText.Visibility = Visibility.Visible;
            this.CancelButton.Visibility = Visibility.Hidden;

            //context = new();

            //var item = await context.Historyorders.
            //    Where(s => order.Idorder == s.Idorder).
            //    FirstOrDefaultAsync();

            //var order_claim = context.Historyorders.
            //    Where(o => o.Idorder == order.Idorder).
            //    Include(s => s.IdpassengerNavigation).
            //    First();

            //var code = MessageBox.Show($"Цена поездки: {order_claim.Price}\nВремя прибытия: " +
            //    $"{order_claim.Timestart!.ToString()!.Split(" ")[1]}\nВремя прибытия в пункт назначения {order_claim!.Timeend!.ToString()!.Split(" ")[1]}",
            //    "Нашли!", MessageBoxButton.YesNo);

            //if (code ==  MessageBoxResult.No)
            //{
            //    this.context.Historyorders.Remove(order_claim); this.context.SaveChanges();
            //    this.WaitOrder.Visibility = Visibility.Hidden;
            //    this.CancelButton.Visibility = Visibility.Hidden;
            //    ShowRouteText.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    this.WaitOrder.Visibility = Visibility.Hidden;
            //    ShowRouteText.Visibility = Visibility.Visible;
            //    this.CancelButton.Visibility = Visibility.Hidden;
            //    MessageBox.Show("Поездка успешно состоялась", "Успех");
            //}
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e) =>
            this.textBoxToAdress.Text = this.passenger.Addresshome;


        private void CancelButton_Click(object sender, RoutedEventArgs e) => this.cancel = true;

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiUserMenuWindow taxiUserMenuWindow = new TaxiUserMenuWindow(this.passenger.IduserNavigation);
            taxiUserMenuWindow.Show();
            Close();
        }

        private void DriverView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] is Userorder order)
            {
                MessageBox.Show("Поездка состоялась");
                this.WaitOrder.Visibility = Visibility.Hidden;
                this.ShowRouteText.Visibility = Visibility.Visible;
                this.CancelButton.Visibility = Visibility.Hidden;
                //!!!!!!!!
                this.order!.Price = (int)order.Paysize;
                this.order.Iddriver = order.Iddriver;
                this.order.Timestart = DateTime.Now.AddMinutes(double.Parse(order.Route));
                this.order.Timeend = DateTime.Now.AddMinutes(double.Parse(order.Route));
                DeleteAll();
                this.context.Historyorders.Update(this.order);
                this.context.SaveChanges();

                success = true;

                TaxiUserMenuWindow taxiUserMenuWindow = new TaxiUserMenuWindow(this.passenger.IduserNavigation);
                taxiUserMenuWindow.Show();
                Close();    
            }
        }

        private void DeleteAll()
        {
            var temp_user_order = this.context.Userorders.Where(s => s.Iduser == this.passenger.Iduser).ToList();

            this.context.RemoveRange(temp_user_order);
            this.context.SaveChanges();

            this.WaitOrder.Visibility = Visibility.Hidden;
            this.ShowRouteText.Visibility = Visibility.Visible;
            this.CancelButton.Visibility = Visibility.Hidden;
            this.DriverView.Visibility = Visibility.Hidden;
        }

        //private async void UpdateDrivers_Click(object sender, RoutedEventArgs e)
        //{
        //    var drivers = await context.Userorders.Where(p => p.Iduser == passenger.Iduser).Include(s => s.IddriverNavigation).ToListAsync();
        //    this.DriverView.ItemsSource = drivers;
        //}
    }
}
