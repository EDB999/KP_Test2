using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.TaxiUserMenuPage;
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
            });
            this.context.SaveChanges();

            while (await context.Historyorders.Where(s => order.Entity.Iddriver == null).FirstOrDefaultAsync() != null)
            {
                if (cancel == true)
                {
                    this.WaitOrder.Visibility = Visibility.Hidden;
                    this.CancelButton.Visibility = Visibility.Hidden;
                    var temp_order = this.context.Historyorders.Where(s => s.Idorder == order.Entity.Idorder).FirstOrDefault();
                    this.context.Historyorders.Remove(temp_order!); this.context.SaveChanges();
                    return;
                }
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
