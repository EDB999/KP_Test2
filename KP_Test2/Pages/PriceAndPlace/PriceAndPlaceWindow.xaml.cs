using System;
using System.ComponentModel;
using System.Windows;
using static KP_Test2.Pages.ViewPage.DriverViewOrdersWindow;

namespace KP_Test2.Pages.PriceAndPlace
{
    /// <summary>
    /// Логика взаимодействия для PriceAndPlaceWindow.xaml
    /// </summary>
    public partial class PriceAndPlaceWindow : Window
    {
        public PriceAndPlaceWindow()
        {
            InitializeComponent();
            OrderInfo.RefrashData();
        }

        private void ConfirmPrice_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(this.textBoxPrice.Text, out var price))
                MessageBox.Show("Цена некорректна");

            if (price > 0)
            {
                OrderInfo.TimeTo = (DateTime)this.textBoxRouteToPassenger!.SelectedTime!;
                OrderInfo.TimeToEnd = (DateTime)this.textBoxRouteToPlace!.SelectedTime!;
                OrderInfo.Price = price;
                this.Hide();
            }
        }
    }
}
