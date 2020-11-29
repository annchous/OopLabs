using System;
using System.Collections.Generic;
using System.Text;
using Banks.Model.Accounts;

namespace Banks.Model.Clients
{
    public class Client
    {
        public Guid Id { get; }
        public String Name { get; }
        public String Surname { get; }
        public String Address { get; }
        public String Passport { get; }
        public List<Account> Accounts { get; }
        public Boolean Verified => !String.IsNullOrEmpty(Address) && !String.IsNullOrEmpty(Passport);

        public Client(String name, String surname, String address, String passport)
        {
            Id = Guid.NewGuid();
            Accounts = new List<Account>();
            Name = name;
            Surname = surname;
            Address = address;
            Passport = passport;
        }

        public override String ToString() =>
            new StringBuilder()
                .AppendLine($"Name: {Name}")
                .AppendLine($"Surname: {Surname}")
                .AppendLine($"Address: {Address}")
                .AppendLine($"Passport: {Passport}")
                .ToString();
        
        public static ClientBuilder Builder(String name, String surname) => 
            new ClientBuilder().SetName(name).SetSurname(surname);
    }
}
