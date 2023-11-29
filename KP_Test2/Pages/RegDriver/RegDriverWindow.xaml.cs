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
using KP_Test2.EF;
using KP_Test2.Entities;
using KP_Test2.Pages.TaxiDriverMenu;

namespace KP_Test2.Pages.RegDriver
{
    /// <summary>
    /// Логика взаимодействия для RegDriverWindow.xaml
    /// </summary>
    public partial class RegDriverWindow : Window
    {
        private Usertaxi user;
        private TaxiKpContext context;
        public RegDriverWindow(Usertaxi user)
        {
            InitializeComponent();
            this.user = user; this.context = new TaxiKpContext();
        }

        private void RegDriverButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Регистрация прошла успешно");

            var driver = this.context.Drivers.Add(new Driver()
            {
                Description = this.textBoxDescryption.Text,
                License = int.Parse(this.textBoxLicense.Text),
                Plane = this.textBoxPlane.Text,
                Iduser = user.Iduser,Iswork = false, Rating = 0
            }); this.context.SaveChanges();

            this.user.Roletype = "all"; this.context.Usertaxis.Update(user); this.context.SaveChanges();

            TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(driver.Entity);
            taxiDriverMenuWindow.Show();
            Hide();
        }
    }
}


