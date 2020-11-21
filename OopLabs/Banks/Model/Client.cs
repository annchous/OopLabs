using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using Banks.Model.Accounts;

namespace Banks.Model
{
    public class Client
    {
        public readonly Guid Id;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }
        public List<Account> Accounts { get; }
        public bool Verified => !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Passport);

        public Client()
        {
            Id = Guid.NewGuid();
            Accounts = new List<Account>();
        }

        public override string ToString() =>
            new StringBuilder()
                .AppendLine($"Name: {Name}")
                .AppendLine($"Surname: {Surname}")
                .AppendLine($"Address: {Address}")
                .AppendLine($"Passport: {Passport}")
                .ToString();
        
        public static ClientBuilder Builder(string name, string surname) => new ClientBuilder().SetName(name).SetSurname(surname);
    }
}
