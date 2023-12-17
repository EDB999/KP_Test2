using KP_Test2.EF;
using KP_Test2.Entities;
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

namespace KP_Test2.Pages.DriverCar
{
    /// <summary>
    /// Логика взаимодействия для DriverCarWindow.xaml
    /// </summary>
    public partial class DriverCarWindow : Window
    {
        private readonly TaxiKpContext context;
        private Driver driver;

        private string isPark;

        public DriverCarWindow(Driver driver)
        {
            InitializeComponent();
            this.driver = driver;
            this.context = new();

            if (driver.IdcarNavigation != null)
            {
                if (driver.IdcarNavigation!.Isautopark == true) ShowAutoParkCarRadio();
                else ShowHaveCarRadio();

                this.CarName.Content = $"Ваша машина\nМодель: {driver.IdcarNavigation.Model}| Номер: {driver.IdcarNavigation.Numbers}";
            }
        }

        private void CarView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = 0;
            try
            {
                selected = ((Car)e.AddedItems[0]!).Idcar;
            }
            catch { return; }

            var temp_driver = this.context.Drivers.Where(s => s.Iddriver == driver.Iddriver).First();
            if (temp_driver.Idcar != null)
            {
                if (driver!.IdcarNavigation!.Isautopark == true)
                {
                    var old_car = this.context.Cars.Where(id => id.Idcar == temp_driver.Idcar).First();
                    old_car.Isfree = true; this.context.Cars.Update(old_car);
                }
                else
                {
                    var delete_car = this.context.Cars.Where(s => s.Idcar == this.driver.Idcar).First();
                    this.driver.Idcar = null; this.context.Cars.Remove(delete_car);
                    this.context.SaveChanges();
                }
            }

            var car = this.context.Cars.Where(s => s.Idcar == selected).First();

            car.Isfree = false; temp_driver.Idcar = selected;

            temp_driver.IdcarNavigation = car;
            this.driver = temp_driver;
            this.context.Cars.Update(car); this.context.Drivers.Update(temp_driver);
            this.context.SaveChanges();
            ShowAutoParkCarRadio();

        }

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxModerCar.Text == "" && textBoxNumberCar.Text == "")
            {
                MessageBox.Show("Невозможно привязать машину");
                return;
            }
            else
            {
                if (driver.IdcarNavigation == null || driver.IdcarNavigation!.Isautopark == true)
                {
                    var new_car = this.context.Cars.Add(new Car()
                    {
                        Isautopark = false,
                        Isfree = false,
                        Model = textBoxModerCar.Text,
                        Numbers = textBoxNumberCar.Text,
                    });
                    this.context.SaveChanges();
                    this.driver.Idcar = new_car.Entity.Idcar;
                    this.context.Drivers.Update(driver);
                    this.context.SaveChanges();
                }
                else //(driver.IdcarNavigation!.Isautopark == false)
                {
                    var old_car = this.context.Cars.Where(s => s.Idcar == this.driver.Idcar).First();
                    old_car.Isfree = true; this.context.Update(old_car);
                    this.context.SaveChanges();

                    var update_car = this.driver.IdcarNavigation;
                    update_car.Model = textBoxModerCar.Text;
                    update_car.Numbers = textBoxNumberCar.Text;
                    this.context.Cars.Update(update_car);
                    this.context.SaveChanges();
                }
                ShowHaveCarRadio();
                MessageBox.Show("Ваша машина добавлена", "Успешно");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(this.driver);
            taxiDriverMenuWindow.Show();
            Close();
        }

        private void ShowHaveCarRadio()
        {
            this.textBoxModerCar.Text = driver.IdcarNavigation!.Model;
            this.textBoxNumberCar.Text = driver.IdcarNavigation.Numbers;
            this.HaveCarRadio.IsChecked = true;
            this.CarView.Visibility = Visibility.Collapsed;
            this.SearchAuto.Visibility = Visibility.Collapsed;
            this.textBoxModerCar.Visibility = Visibility.Visible;
            this.textBoxNumberCar.Visibility = Visibility.Visible;
        }

        private void HaveCarRadio_Click(object sender, RoutedEventArgs e) => ShowHaveCarRadio();

        private void ShowAutoParkCarRadio()
        {
            this.AutoParkCarRadio.IsChecked = true;
            this.textBoxModerCar.Visibility = Visibility.Collapsed;
            this.textBoxNumberCar.Visibility = Visibility.Collapsed;
            this.CarView.Visibility = Visibility.Visible;
            this.SearchAuto.Visibility = Visibility.Visible;
            this.CarView.ItemsSource = context.Cars.Where(s => s.Isfree == true && s.Isautopark == true).ToList();
            if (driver.IdcarNavigation!.Isautopark == true)
                this.CarName.Content = $"Ваша машина\nМодель: {driver.IdcarNavigation.Model}| Номер: {driver.IdcarNavigation.Numbers}";
        }

        private void AutoParkCarRadio_Click(object sender, RoutedEventArgs e) => ShowAutoParkCarRadio();

        private void UpdateCarButton_Click(object sender, RoutedEventArgs e)
        {


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var data = context.Cars.Where(s => s.Isfree == true && s.Isautopark == true);


            if (this.SearchAuto.Text == "") this.CarView.ItemsSource = data.ToList();
            else if (int.TryParse(this.SearchAuto.Text,out var id))
            {
                this.CarView.ItemsSource = data.Where(s => s.Idcar == id).ToList();
            }
            else
            {
                var find_data = data.Where(s =>
                Microsoft.EntityFrameworkCore.EF.Functions.Like(s.Model, $"%{this.SearchAuto.Text}%") ||
                Microsoft.EntityFrameworkCore.EF.Functions.Like(s.Numbers, $"%{this.SearchAuto.Text}%") 
                
                ).
                ToList();
                this.CarView.ItemsSource = find_data;
            }
        }
    }
}
