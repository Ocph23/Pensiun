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
using PengusulanPensiun.ViewModels;

namespace PengusulanPensiun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LoginViewModel Loginvm { get; }

        public MainWindow()
        {
           var form = new Views.LoginView();
           this.Loginvm = new ViewModels.LoginViewModel() { WindowClose=form.Close};
          form.DataContext = Loginvm;
          form.ShowDialog();
            if (Loginvm.IsLogin)
            {
                InitializeComponent();
                SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            }
            else
                this.Close();
        }

        private void instansimenu_Click(object sender, RoutedEventArgs e)
        {
            var form = new Views.InstansiView();
            form.DataContext = new ViewModels.InstansiViewModel() { WindowClose=form.Close};
            form.ShowDialog();
        }

        private void periodemenu_Click(object sender, RoutedEventArgs e)
        {
            var form = new Views.PeriodeView();
            form.DataContext = new ViewModels.PeriodeViewModel() { WindowClose = form.Close };
            form.ShowDialog();
        }

        private void pegawaimenu_Click(object sender, RoutedEventArgs e)
        {
            var form = new Views.PengajuanPensiunViewm();
            form.DataContext = new ViewModels.PengajuanPensiunViewModel() { WindowClose = form.Close };
            form.ShowDialog();
        }

        private void usermenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            var form = new Views.UserView(UserCreate.ChangePassword, Loginvm);
            form.ShowDialog();
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            var form = new Views.UserView(UserCreate.UserNew,null);
            form.ShowDialog();

        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            var form = new Views.About();
            form.ShowDialog();
        }
    }


    public enum UserCreate
    {
        UserNew,ChangePassword
    }
}
