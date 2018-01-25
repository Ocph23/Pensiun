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
using PengusulanPensiun.ViewModels;
using DAL;

namespace PengusulanPensiun.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        private UserCreate userNew;
        private LoginViewModel loginvm;


        public UserView(UserCreate userNew, LoginViewModel loginvm)
        {
            InitializeComponent();
            this.userNew = userNew;
            this.loginvm = loginvm;
            if (userNew == UserCreate.UserNew)
            {
                this.stackOld.Visibility = Visibility.Collapsed;
                this.Title = "Tambah User Baru";
            }else
            {
                this.Title = "Ubah Password";
                Nama.Text = loginvm.Nama;
                userName.Text = loginvm.UserName;
            }
        }

        private void ubah_Click(object sender, RoutedEventArgs e)
        {
            if(this.userNew== UserCreate.ChangePassword)
            {
                if(loginvm.Password== oldpassword.Password)
                {
                    if (IsValidChangePassword()) {
                        using (var db = new OcphDbContext())
                        {
                        
                            var model = new Models.user { Nama = Nama.Text, Password = password.Password, UserName = userName.Text };
                            if (db.Users.Update(O => new { O.Password,O.Nama }, model, O => O.Id == loginvm.Id))
                            {
                                loginvm.Password = password.Password;
                                loginvm.Nama = Nama.Text;
                                loginvm.UserName = userName.Text;
                                MessageBox.Show("Password Berhasil Diubah", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Password Gagal Diubah", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                   
                }else
                {
                    MessageBox.Show("Password Lama Anda Salah", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }else
            {
                if (IsValidAddPassword())
                {
                    using (var db = new OcphDbContext())
                    {
                        var user = new Models.user { Nama = Nama.Text, Password = password.Password, UserName = userName.Text };
                        user.Id = db.Users.InsertAndGetLastID(user);
                        if (user.Id > 0)
                        {
                            MessageBox.Show("Password Berhasil Ditambah", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Password Gagal Ditambah", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                }
            }
        }

        private bool IsValidChangePassword()
        {
            var res = true;
            if(string.IsNullOrEmpty(Nama.Text) || string.IsNullOrEmpty(userName.Text) || string.IsNullOrEmpty(oldpassword.Password) || string.IsNullOrEmpty(password.Password)|| string.IsNullOrEmpty(confirmpassword.Password))
            {
                MessageBox.Show("Lengkapi Data Anda", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                res = false;
            }else if(password.Password!=confirmpassword.Password)
            {
                MessageBox.Show("Password Baru Anda tidak Cocok", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                res = false;
            }
            return res;
        }

        private bool IsValidAddPassword()
        {
            var res = true;
            if (string.IsNullOrEmpty(Nama.Text) || string.IsNullOrEmpty(userName.Text)|| string.IsNullOrEmpty(password.Password) || string.IsNullOrEmpty(confirmpassword.Password))
            {
                MessageBox.Show("Lengkapi Data Anda", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                res = false;
            }
            else if (password.Password != confirmpassword.Password)
            {
                MessageBox.Show("Password Baru Anda tidak Cocok", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                res = false;
            }
            return res;
        }

        private void batal_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
