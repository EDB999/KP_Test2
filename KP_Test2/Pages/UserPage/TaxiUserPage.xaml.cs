using KP_Test2.Entities;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KP_Test2.Pages.UserPage
{
    public partial class TaxiUserPage : Page
    {
        private Usertaxi userTaxi;
        public TaxiUserPage(Usertaxi user)
        {
            InitializeComponent();
            this.userTaxi = user;
            MessageBox.Show($"Привет, {user.Name}");
        }
    }
}
