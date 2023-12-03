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

namespace KP_Test2.Pages.PersonalAccount
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountWindow.xaml
    /// </summary>
    public partial class PersonalAccountWindow : Window
    {
        private Usertaxi user;
        private Passenger passenger;
        private readonly TaxiKpContext context;
        public PersonalAccountWindow(Usertaxi user)
        {
            InitializeComponent();
            
            this.context = new();

            this.user = DisplayPassengerInfo(user.Iduser);
        }

        private Usertaxi DisplayPassengerInfo(int id_user)
        {
            this.passenger = context.Passengers.
                Where(id => id.Iduser == id_user).
                Include(u => u.IduserNavigation).
                First();

            this.textBoxName.Text = passenger.IduserNavigation.Name;
            this.textBoxPass.Text = passenger.IduserNavigation.Password;
            this.textBoxSurname.Text = passenger.IduserNavigation.Surname;
            this.textBoxLogin.Text = passenger.IduserNavigation.Login;
            this.textBoxCard.Text = passenger.IduserNavigation.Card.ToString();
            this.textBoxContact.Text = passenger.IduserNavigation.Contact;
            this.textBoxEmail.Text = passenger.IduserNavigation.Email;
            this.textBoxDescription.Text = passenger.Description;
            this.textBoxHome.Text = passenger.Addresshome;
            return this.passenger.IduserNavigation;
        }

        private void UpdatePassengerInfo()
        {
            try
            {
                this.user.Name = this.textBoxName.Text;
                this.user.Password = this.textBoxPass.Text;
                this.user.Surname = this.textBoxSurname.Text;
                this.user.Card = int.Parse(this.textBoxCard.Text);
                this.user.Email = this.textBoxEmail.Text;
                this.user.Login = this.textBoxLogin.Text;
                this.user.Contact = this.textBoxContact.Text;

                this.passenger.Description = this.textBoxDescription.Text;
                this.passenger.Addresshome = this.textBoxHome.Text;
                this.passenger.IduserNavigation = user;
                this.context.Passengers.Update(passenger); this.context.SaveChanges();


                MessageBox.Show("Данные успешно изменены", "Состояние");
            }
            catch { MessageBox.Show("Данные были заполнены не правильно","Ошибка"); }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TaxiUserMenuWindow taxiUserMenuWindow = new TaxiUserMenuWindow(user);
            taxiUserMenuWindow.Show();
            Hide();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) => UpdatePassengerInfo();
    }
}
