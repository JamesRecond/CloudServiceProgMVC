using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace SignupsWorker1
{
    class Person : TableEntity
    {
        public Person(string email, string password)
        {
            this.PartitionKey = "signups";
            this.RowKey = email;
            Password = password;
        }

        public Person() { }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
