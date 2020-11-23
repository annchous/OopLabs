using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banks.Exceptions;
using Banks.Model.Accounts;
using Banks.Model.Transactions;

namespace Banks.Model.Bank
{
    public class Bank
    {
        public Guid Id { get; }
        public string Name { get; }
        public List<Client.Client> Clients { get; set; }
        public List<Account> Accounts { get; set; }

        public Bank(string name) : this(name, new List<Client.Client>(), new List<Account>()) {}
        public Bank(string name, List<Client.Client> clients, List<Account> accounts)
        {
            Id = Guid.NewGuid();
            Name = name;
            Clients = clients;
            Accounts = accounts;
        }

        public void AddClient(Client.Client client)
        {
            if (Clients.Exists(x => x.Passport == client.Passport)) throw new ClientAlreadyExistsException();
            Clients.Add(client);
        }

        public void AddAccountToClient(Client.Client client, Account account)
        {
            if (!Clients.Exists(x => x.Id == client.Id)) throw new ClientAlreadyExistsException();
            Accounts.Add(account);
            Clients[Clients.IndexOf(client)].Accounts.Add(account);
        }

        public void Put(Guid accountId, decimal sum)
        {
            if (!Accounts.Exists(x => x.Id == accountId))
                throw new AccountNotFoundException(accountId);
            var account = Accounts.First(x => x.Id == accountId);

            new Transaction(account).Put(sum);
        }

        public void Withdraw(Guid accountId, decimal sum)
        {
            if (!Accounts.Exists(x => x.Id == accountId))
                throw new AccountNotFoundException(accountId);
            var account = Accounts.First(x => x.Id == accountId);
            if (!account.AccountOwner.Verified) throw new NotVerifiedClientException(account.AccountOwner.Id);

            new Transaction(account).Withdraw(sum);
        }

        public void Transfer(Guid sourceAccountId, Guid destinationAccountId, decimal sum)
        {
            if (!Accounts.Exists(account => account.Id == sourceAccountId))
                throw new AccountNotFoundException(sourceAccountId);
            if (!Accounts.Exists(account => account.Id == destinationAccountId))
                throw new AccountNotFoundException(destinationAccountId);
            var sourceAccount = Accounts.First(account => account.Id == sourceAccountId);
            var destinationAccount = Accounts.First(account => account.Id == destinationAccountId);
            if (!sourceAccount.AccountOwner.Verified) throw new NotVerifiedClientException(sourceAccount.AccountOwner.Id);

            new Transaction(sourceAccount, destinationAccount).Transfer(sum);
        }

        public void CancelTransaction(Guid sourceAccountId, Guid destinationAccountId)
        {
            if (!Accounts.Exists(account => account.Id == sourceAccountId))
                throw new AccountNotFoundException(sourceAccountId);
            if (destinationAccountId != Guid.Empty)
                if (!Accounts.Exists(account => account.Id == destinationAccountId))
                    throw new AccountNotFoundException(destinationAccountId);

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
