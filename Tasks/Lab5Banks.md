## Banks
### Laboratory work №5

### [Solution](https://github.com/annchous/OopLabs/tree/master/OopLabs/Banks)

### [Tests](https://github.com/annchous/OopLabs/tree/master/OopLabs/BanksTest)

There are several banks that provide financial services for transactions with
money.

The bank has **Accounts** and **Clients**. The client has a name, surname, address and passport number (name and surname are required, the rest is optional).

There are three types of accounts: **Debit account**, **Deposit** and **Credit** account. Every account belongs to a client.

A _**debit account**_ is a regular account with a fixed interest on the balance. Money can
shoot at any time, you can't go into the minus. There are no commissions.

_**Deposit**_ is an account from which you cannot withdraw and transfer money until
its term will end (you can replenish it). The percentage on the balance depends on the original
amounts, for example, if we open a deposit up to 50,000 rubles. - 3%, if from 50,000 rubles. up to 100,000 R. - 3.5%, more than 100,000 rubles. - 4%. There are no commissions.

_**Credit account**_ - has a credit limit, within which you can go into the minus (in
plus is also possible). There is no interest on the balance. There is a flat fee for
use if the client is in the red.

The **interest on the balance** is calculated daily from the current amount on that day, but
paid once a month (for both debit card and deposit). For example, 3.65%
annual. This means per day: 3.65% / 365 days = 0.01%. The client has 100,000 rubles today. on the account - remembered that he already had 10 rubles. Tomorrow he received a salary and it was 200,000 rubles. For this day he added another 20 p. The next day he bought himself a new PC and he had 50,000 left R. - added 5 p. Thus, by the end of the month, we add up everything that we remember.
Let's say it came out 300 rubles. - this amount is added to the account or deposit in the current month.Different banks offer different conditions. Interest rates are known in each bank
and commissions.

Each account must provide a mechanism for **withdrawing**, **replenishing** and **transferring** money (then there are accounts need some identifiers).

The client must be created in steps. First, he indicates the first and last name (required),
then the address (you can skip and not specify), then passport data (you can
skip and omit).

If the client does not have an address or passport number when creating an account, we will declare
such an account (of any type) is doubtful, and we prohibit withdrawal and transfer operations above
a certain amount (each bank has its own value). If in the future the client indicates all the necessary information about yourself - the account ceases to be doubtful and can be used without restrictions.

From time to time, banks carry out operations to pay interest and deduct commission. it
means that we need a mechanism to skip time to see what will happen in
day / month / year, etc.

Another mandatory mechanism that banks must have is the **cancellation of transactions**. If a
it suddenly turns out that the transaction was committed by an intruder, then such a transaction
must be canceled.


### Code description

#### [Accounts](https://github.com/annchous/OopLabs/tree/master/OopLabs/Banks/Model/Accounts)

A set of accounts of different types that inherit from the base abstract class `Account`.

There are 3 types of accounts:
* [_**Credit Account.cs**_](https://github.com/annchous/OopLabs/blob/master/OopLabs/Banks/Model/Accounts/CreditAccount.cs)
* [_**Debit Account.cs**_](https://github.com/annchous/OopLabs/blob/master/OopLabs/Banks/Model/Accounts/DebitAccount.cs)
* [_**Deposit Account.cs**_](https://github.com/annchous/OopLabs/blob/master/OopLabs/Banks/Model/Accounts/DepositAccount.cs)

The base abstract class `Account` contains a field `CareTracker` that will be responsible for creating backups using `pattern Memento`.
Of the key methods, the class `Account` contains methods responsible for calculating interest on the balance:
```
public void ChargeDailyInterest(Int32 days, DateTime updateTime)
public void UpdateBalance(Decimal sum)
```

#### [Bank.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/Banks/Model/Banks/Bank.cs)

This class represents the entity of the bank. 

It stores a list of clients and accounts and contains the methods necessary to provide the bank's functionality:
```
public void AddClient(Client client)
public void AddAccountToClient(Client client, Account account)
public void Put(Guid accountId, Decimal sum)
public void Withdraw(Guid accountId, Decimal sum)
public void Transfer(Guid sourceAccountId, Guid destinationAccountId, Decimal sum)
public void CancelTransaction(Guid sourceAccountId, Guid destinationAccountId)
```

#### [Client.cs](https://github.com/annchous/OopLabs/blob/master/OopLabs/Banks/Model/Clients/Client.cs)

Represents a client entity. The client is created using [**ClientBuilder.cs**](https://github.com/annchous/OopLabs/blob/master/OopLabs/Banks/Model/Clients/ClientBuilder.cs) - the `Builder pattern`.

#### [Memento](https://github.com/annchous/OopLabs/tree/master/OopLabs/Banks/Model/Memento)

Implementation of the `Memento pattern` for storing account states and canceling transactions on them, if necessary.

#### [Observer](https://github.com/annchous/OopLabs/tree/master/OopLabs/Banks/Model/Observer)

Implementation of the `Observer pattern` for counting the term of the deposit.

#### [Transactions](https://github.com/annchous/OopLabs/tree/master/OopLabs/Banks/Model/Transactions)

Implementation of a transaction system using the `Chain of Responsibility pattern`.
