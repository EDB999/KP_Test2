using System.Windows;
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
            try
            {
                var driver = this.context.Drivers.Add(new Driver()
                {
                    Description = this.textBoxDescryption.Text,
                    License = int.Parse(this.textBoxLicense.Text),
                    Plane = this.textBoxPlane.Text,
                    Iduser = user.Iduser,
                    Iswork = false,
                    Rating = 0
                }); this.context.SaveChanges();

                this.user.Roletype = "all"; this.context.Usertaxis.Update(user); this.context.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно");
                TaxiDriverMenuWindow taxiDriverMenuWindow = new TaxiDriverMenuWindow(driver.Entity);
                taxiDriverMenuWindow.Show();
                Hide();
            }
            catch
            {
                MessageBox.Show("Лицензия не корректна", "Ошибка");
                return;
            }
        }
    }
}


