using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Exam_ImageGallery
{
    class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Account(string login_, string password_, string name_, string surname_)
        {
            Login = login_;
            Password = password_;
            Name = name_;
            Surname = surname_;
            
        }      
    }
}
