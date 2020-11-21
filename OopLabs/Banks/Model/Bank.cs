using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banks.Model.Accounts;
using Banks.Model.Transactions;

namespace Banks.Model
{
    public class Bank
    {
        public Guid Id { get; }
        public string Name { get; }
        public List<Client> Clients { get; set; }
        public List<Account> Accounts { get; set; }

        public Bank(string name) : this(name, new List<Client>(), new List<Account>()) {}
        public Bank(string name, List<Client> clients, List<Account> accounts)
        {
            Id = Guid.NewGuid();
            Name = name;
            Clients = clients;
            Accounts = accounts;
        }

        public void AddClient(Client client)
        {
            if (Clients.Exists(x => x.Passport == client.Passport)) throw new Exception("Клиент с такими данными уже существует");
            Clients.Add(client);
        }

        public void AddAccountToClient(Client client, Account account)
        {
            if (!Clients.Exists(x => x.Id == client.Id)) throw new Exception("Клиента с такими данными не существует");
            Accounts.Add(account);
            Clients[Clients.IndexOf(client)].Accounts.Add(account);
        }

        public void Put(Guid accountId, decimal sum)
        {
            if (!Accounts.Exists(x => x.Id == accountId))
                throw new Exception("Счёт не существует");
            var account = Accounts.First(x => x.Id == accountId);

            new Transaction(account).Put(sum);
        }

        public void Withdraw(Guid accountId, decimal sum)
        {
            if (!Accounts.Exists(x => x.Id == accountId))
                throw new Exception("Счёт не существует");
            var account = Accounts.First(x => x.Id == accountId);
            if (!account.AccountOwner.Verified) throw new Exception("Аккаунт не подтверждён");

            new Transaction(account).Withdraw(sum);
        }

        public void Transfer(Guid sourceAccountId, Guid destinationAccountId, decimal sum)
        {
            if (!Accounts.Exists(account => account.Id == sourceAccountId))
                throw new Exception("Счёт не существует");
            if (!Accounts.Exists(account => account.Id == destinationAccountId))
                throw new Exception("Счёт не существует");
            var sourceAccount = Accounts.First(account => account.Id == sourceAccountId);
            var destinationAccount = Accounts.First(account => account.Id == destinationAccountId);
            if (!sourceAccount.AccountOwner.Verified) throw new Exception("Аккаунт не подтверждён");

            new Transaction(sourceAccount, destinationAccount).Execute(sum);
        }

        public void CancelTransaction(Guid sourceAccountId, Guid destinationAccountId)
        {
            if (!Accounts.Exists(account => account.Id == sourceAccountId))
                throw new Exception("Счёт не существует");
            if (destinationAccountId != Guid.Empty)
                if (!Accounts.Exists(account => account.Id == destinationAccountId))
                    throw new Exception("Счёт не существует");

            new Transaction(Accounts.FirstOrDefault(account => account.Id == sourceAccountId),
                    Accounts.FirstOrDefault(account => account.Id == destinationAccountId))
                .Undo();
        }


        public override string ToString() =>
            new StringBuilder()
                .AppendLine($"Name: {Name}")
                .AppendLine($"Id: {Id}")
                .ToString();
    }
}
