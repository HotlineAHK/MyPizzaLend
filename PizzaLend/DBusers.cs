using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PizzaLend
{
    internal class DBusers
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DBusers(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public DBusers() { }
    }
}
