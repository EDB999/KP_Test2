using KP_Test2.EF;
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

namespace KP_Test2.Pages.DriverPersonalAccount
{
    /// <summary>
    /// Логика взаимодействия для DriverPersonalAccountWindow.xaml
    /// </summary>
    public partial class DriverPersonalAccountWindow : Window
    {
        private Driver driver;
        private TaxiKpContext context;
        public DriverPersonalAccountWindow(Driver driver)
        {
            InitializeComponent();
            this.driver = driver;
            this.context = new(); DisplayDriverInfo();
        }

        private void DisplayDriverInfo()
        {
            this.textBoxDescryption.Text = this.driver.Description;
            this.textBoxPlane.Text = this.driver.Plane;
        }

        private void UpdateDriverInfo()
        {
            this.driver.Description = this.textBoxDescryption.Text;
            this.driver.Plane = this.textBoxPlane.Text;
            this.context.Drivers.Update(driver); this.context.SaveChanges();
            MessageBox.Show("Данные успешно изменены", "Состояние");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(this.driver);
            taxiDriverMenuWindow.Show();
            Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) => UpdateDriverInfo();
    }
}
