﻿using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.DriverCar;
using KP_Test2.Pages.DriverHistoryOrder;
using KP_Test2.Pages.DriverPersonalAccount;
using KP_Test2.Pages.TaxiUserMenuPage;
using KP_Test2.Pages.ViewPage;
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

namespace KP_Test2.Pages.TaxiDriverMenu
{
    /// <summary>
    /// Логика взаимодействия для TaxiDriverMenuWindow.xaml
    /// </summary>
    public partial class TaxiDriverMenuWindow : Window
    {
        private Driver driver;
        private readonly TaxiKpContext context;
        private Usertaxi user;
        public TaxiDriverMenuWindow(Driver driver)
        {
            InitializeComponent(); this.context = new();
            this.driver = this.context.Drivers.Where(d => d.Iddriver == driver.Iddriver).Include(u => u.IduserNavigation).Include(c => c.IdcarNavigation).First();
            this.context = new();
        }

        private void DriverHistoryOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            DriversHistoryOrdersWindow driversHistoryOrdersWindow = new DriversHistoryOrdersWindow(this.driver);
            driversHistoryOrdersWindow.Show();
            Hide();
        }

        private void DriverCarButton_Click(object sender, RoutedEventArgs e)
        {
            DriverCarWindow driverCarWindow = new DriverCarWindow(this.driver);
            driverCarWindow.Show();
            Hide();
        }

        private void DriverActivity_Click(object sender, RoutedEventArgs e)
        {
            DriverPersonalAccountWindow personalAccountWindow = new DriverPersonalAccountWindow(this.driver);
            personalAccountWindow.Show();
            Hide();
        }


        private void ViewIncomingOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            DriverViewOrdersWindow driverViewOrdersWindow = new DriverViewOrdersWindow(this.driver);
            driverViewOrdersWindow.Show();
            Hide();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiUserMenuWindow taxiUserMenuWindow = new TaxiUserMenuWindow(this.driver.IduserNavigation);
            taxiUserMenuWindow.Show();
            Hide();
        }
    }
}
