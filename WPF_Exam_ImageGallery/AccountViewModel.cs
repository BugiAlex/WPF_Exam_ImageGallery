using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Exam_ImageGallery
{
    [Serializable]
    class AccountViewModel : ViewModelBase
    {
        private Account account;

        public AccountViewModel(Account account_)
        {
            account = account_;
        }

        public string Surname
        {
            get { return account.Surname; }
            set
            {
                account.Surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string Name
        {
            get { return account.Name; }
            set
            {
                account.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Login
        {
            get { return account.Login; }
            set
            {
                account.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Password
        {
            get { return account.Password; }
            set
            {
                account.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}
