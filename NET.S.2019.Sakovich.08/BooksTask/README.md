# BooksTask (Day 8, Task 1)

## Task description

The task is to implement a type `Book` encapsulating a book with the following fields: *ISBN*, *Author*, *Title*,
*Publisher*, *Publication year*, and *Price*. There should be also developed a mechanism for reading and writing
a *collection* of `Book`s from and to a binary file, using only `BinaryReader` and `BinaryWriter` for IO operations.
For the aforementioned collection, the following operations must be implemented:

1. `AddBook`, which adds a `Book` to the collection if the `Book` is not already there and throws an exception otherwise;

2. `RemoveBook`, which removes a present `Book` from the collection and throws an exception if the `Book` is not present
in the collection;

3. `SortBooksByTag`, which sorts the collection in the order of a specified tag;

4. `FindBookByTag`, which returns the first `Book` with a specified value of a specified tag; if no such
book exists, an exception is thrown.

A tag must be specified without resorting to `delegate`s.
