using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel;

namespace PengusulanPensiun.ViewModels
{
   public class LoginViewModel:Models.user,IDataErrorInfo
    {
        private string error;

        public LoginViewModel()
        {
            LoginCommand = new CommandHandler { CanExecuteAction = LoginCommandValidate, ExecuteAction = LoginCommandAction };
        }

        private void LoginCommandAction(object obj)
        {
            Error = string.Empty;
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Users.Where(O => O.UserName == UserName).FirstOrDefault();
                    if (result != null)
                    {
                        if (Password == result.Password)
                        {
                            this.Nama = result.Nama;
                            this.Id = result.Id;
                            IsLogin = true;
                            this.WindowClose();
                        }
                        else
                            Error = "Password Anda Salah";
                    }
                    else
                        Error = "Anda Tidak Memiliki Akses";
                }
                catch (Exception ex)
                {

                    Error = ex.Message;
                }
               
            }   
        }

        public Action WindowClose { get; set; }

        public CommandHandler LoginCommand { get; }

        public string Error { get => error;
        set { error = value;
                OnPropertyChange("Error");
            }
        }

        public bool IsLogin { get; private set; }

        public string this[string columnName] {
            get
            {
                Error = null;
                switch (columnName)
                {
                    case "UserName":
                        Error = string.IsNullOrEmpty(UserName) ? "Masukkan User Name Anda !" : null;
                        break;
                    case "Password":
                        Error = string.IsNullOrEmpty(Password) ? "Massukkan Password Anda !" : null;
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }

        private bool LoginCommandValidate(object obj)
        {
            return string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) ? false : true;
        }
    }
}
