﻿using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.TaxiDriverMenu;
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

namespace KP_Test2.Pages.DriverCar
{
    /// <summary>
    /// Логика взаимодействия для DriverCarWindow.xaml
    /// </summary>
    public partial class DriverCarWindow : Window
    {
        private readonly TaxiKpContext context;
        private readonly Driver driver;
        public DriverCarWindow(Driver driver)
        {
            InitializeComponent();
            this.driver = driver;
            this.context = new();
        }

        private void CarView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp_driver = this.context.Drivers.Where(s => s.Iddriver == driver.Iddriver).First();
            temp_driver.Idcar = ((Car)e.AddedItems[0]!).Idcar;
        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseCarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(this.driver);
            taxiDriverMenuWindow.Show();
            Close();
        }
    }
}