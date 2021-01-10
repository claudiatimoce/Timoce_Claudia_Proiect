using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace PasswordManager.Models
{
   public class ListPassword
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(AccountList))]
        public int AccountListID { get; set; }
        public int PasswordID { get; set; }

    }
}
