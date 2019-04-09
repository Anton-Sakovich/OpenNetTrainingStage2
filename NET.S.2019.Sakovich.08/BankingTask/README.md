# BankingTask (Day 8, Task 2)

## Task description
The task is to implement a system of types which will model a small banking system. This system must provide:

1. A bank *account*. A bank account must have:
    * Identification number;
    * Holder's name;
    * Holder's family name;
    * Balance;
    * Bonuses;
    * *Status*, which describes "how cool an account is", for example Base, Bronze, Silver, and Platinum.
    
2. Means of creating and closing accounts.

3. Means of depositing money to and withdrawing money from an account. When money is deposited or withdrawn,
the account's bonuses value changes according to the cost of the operation and the account's status.

The system of types must allow extending possible account types as well as strategies to update bonuses.
