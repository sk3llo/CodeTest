using System;
using SQLite;

namespace Testproject.ViewModels
{
    public class LoginModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
