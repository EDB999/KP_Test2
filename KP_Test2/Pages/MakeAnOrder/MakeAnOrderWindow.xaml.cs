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
        private bool cancel = false;
        public MakeAnOrderWindow(Passenger passenger)
        {
            InitializeComponent();
            this.passenger = passenger;
            this.context = new();
            //DoubleAnimation doubleAnimation = new DoubleAnimation();
            //doubleAnimation.From = 25;
            //doubleAnimation.To = 50;
            //doubleAnimation.AutoReverse = true;
            //MakeOrderButton.BeginAnimation(Button.HeightProperty, doubleAnimation);
        }

        private async void MakeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            this.WaitOrder.Visibility = Visibility.Visible; 
            this.CancelButton.Visibility = Visibility.Visible;
            var order = this.context.Historyorders.Add(new Historyorder()
            {
                Idpassenger = this.passenger.Idpassenger,
                Routestart = this.textBoxAdress.Text,
                Routeend = this.textBoxToAdress.Text,
            }).Entity;
            this.context.SaveChanges();

            while (await context.Historyorders.Where(s => s.Price != null && s.Iddriver != null
            && s.Timeend != null && s.Timestart != null
            && order.Idorder == s.Idorder).FirstOrDefaultAsync() == null)
            {
                if (cancel == true)
                {
                    this.WaitOrder.Visibility = Visibility.Hidden;
                    this.CancelButton.Visibility = Visibility.Hidden;
                    var temp_order = this.context.Historyorders.Where(s => s.Idorder == order.Idorder).FirstOrDefault();
                    this.context.Historyorders.Remove(temp_order!); this.context.SaveChanges();
                    return;
                }
            }

            context = new();

            var item = await context.Historyorders.Where(s => order.Idorder == s.Idorder).FirstOrDefaultAsync();

            var order_claim = context.Historyorders.
                Where(o => o.Idorder == order.Idorder).
                Include(s => s.IdpassengerNavigation).
                First();


            var code = MessageBox.Show($"Цена поездки: {order_claim.Price}\nВремя прибытия: " +
                $"{order_claim.Timestart!.ToString()!.Split(" ")[1]}\nВремя прибытия в пункт назначения {order_claim!.Timeend!.ToString()!.Split(" ")[1]}",
                "Нашли!", MessageBoxButton.YesNo);

            if (code ==  MessageBoxResult.No)
            {
                this.context.Historyorders.Remove(order_claim); this.context.SaveChanges();
                this.WaitOrder.Visibility = Visibility.Hidden;
                this.CancelButton.Visibility = Visibility.Hidden;
            }
            else
            {
                this.WaitOrder.Visibility = Visibility.Hidden;
                this.CancelButton.Visibility = Visibility.Hidden;
                MessageBox.Show("Поездка успешно состоялась", "Успех");
            }
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
    }
}
