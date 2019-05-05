# MonkeyBanker (Day 15, Task 1)

## Task description

_MonkeyBanker_ is a remake of
[BankingTask](https://github.com/Anton-Sakovich/OpenNetTrainingStage2/tree/master/NET.S.2019.Sakovich.08/BankingTask)
rewritten following the Stairway pattern.

MonkeyBanker is a console application which implements a simple system of bank accounts. Major changes compared to
[BankingTask](https://github.com/Anton-Sakovich/OpenNetTrainingStage2/tree/master/NET.S.2019.Sakovich.08/BankingTask)
are:

1. Projects and resulting assemblies are organized according to the Stairway pattern:
    * `MonkeyBanker.Entities` contains types which describe MonkeyBanker's data models;
    * `MonkeyBanker.Services` provides interface to MonkeyBanker's business logic layer;
    * `MonkeyBanker.Services.FairTrade` implements `MonkeyBanker.Services`;
    * `MonkeyBanker.Data` provides interface to MonkeyBanker's data access layer;
    * `MonkeyBanker.Data.AdoNet` implements `MonkeyBanker.Data` using ADO.NET;
    
2. `Deposit` class was removed. Account balance is now integrated into `Account` model.

3. Deposit and withdrawal operations are derived from a common base class responsible for operations in general.
