using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Banks.Exceptions;
using Banks.Model.Accounts;
using Banks.Model.Clients;
using Banks.Model.Transactions;

namespace Banks.Model.Banks
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
            if (Clients.Exists(x => x.Passport == client.Passport)) throw new ClientAlreadyExistsException();
            Clients.Add(client);
        }

        public void AddAccountToClient(Client client, Account account)
        {
            if (!Clients.Exists(x => x.Id == client.Id)) throw new ClientAlreadyExistsException();
            Accounts.Add(account);
            Clients.Find(c => c == client)?.Accounts.Add(account);
        }

        public void Put(Guid accountId, decimal sum)
        {
            var account = Accounts.FirstOrDefault(x => x.Id == accountId) 
                          ?? throw new AccountNotFoundException(accountId);
            new Transaction(account).Put(sum);
        }

        public void Withdraw(Guid accountId, decimal sum)
        {
            var account = Accounts.FirstOrDefault(x => x.Id == accountId) 
                          ?? throw new AccountNotFoundException(accountId);
            if (!account.AccountOwner.Verified) throw new NotVerifiedClientException(account.AccountOwner.Id);

            new Transaction(account).Withdraw(sum);
        }

        public void Transfer(Guid sourceAccountId, Guid destinationAccountId, decimal sum)
        {
            var sourceAccount = Accounts.FirstOrDefault(account => account.Id == sourceAccountId) 
                                ?? throw new AccountNotFoundException(sourceAccountId);
            var destinationAccount = Accounts.FirstOrDefault(account => account.Id == destinationAccountId) 
                                     ?? throw new AccountNotFoundException(destinationAccountId);
            if (!sourceAccount.AccountOwner.Verified) throw new NotVerifiedClientException(sourceAccount.AccountOwner.Id);

            new Transaction(sourceAccount, destinationAccount).Transfer(sum);
        }

        public void CancelTransaction(Guid sourceAccountId, Guid destinationAccountId)
        {
            var sourceAccount = Accounts.FirstOrDefault(account => account.Id == sourceAccountId)
                                ?? throw new AccountNotFoundException(sourceAccountId);
            var destinationAccount = destinationAccountId != Guid.Empty
                ? Accounts.FirstOrDefault(account => account.Id == destinationAccountId)
                  ?? throw new AccountNotFoundException(destinationAccountId)
                : null;

            new Transaction(sourceAccount, destinationAccount).Undo();
        }


        public override string ToString() =>
            new StringBuilder()
                .AppendLine($"Name: {Name}")
                .AppendLine($"Id: {Id}")
                .ToString();
    }
}
