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
using KP_Test2.Pages.TaxiDriverMenu;

namespace KP_Test2.Pages.RegDriver
{
    /// <summary>
    /// Логика взаимодействия для RegDriverWindow.xaml
    /// </summary>
    public partial class RegDriverWindow : Window
    {
        public RegDriverWindow()
        {
            InitializeComponent();
        }

        private void RegDriverButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Регистрация прошла успешно");
            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow();
            taxiDriverMenuWindow.Show();
            Hide();
        }
    }
}


