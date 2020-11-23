using System;
using System.Collections.Generic;
using System.Text;
using Banks.Model.Accounts;

namespace Banks.Model.Clients
{
    public class Client
    {
        public readonly Guid Id;
        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string Passport { get; }
        public List<Account> Accounts { get; }
        public bool Verified => !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Passport);

        public Client(string name, string surname, string address, string passport)
        {
            Id = Guid.NewGuid();
            Accounts = new List<Account>();
            Name = name;
            Surname = surname;
            Address = address;
            Passport = passport;
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
