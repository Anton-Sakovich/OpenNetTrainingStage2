# InsertNumberTask (Day 2, Task 1)

## Task description

The task is to insert the first `k - j + 1` bits of one number into a sequence of bits from `j` to `k` of another number.
Numbers are given as two four byte signed integers.

Unit tests are to be written using both `MSTest` and `NUnit` test frameworks.

### Example

For `j = 4` and `k = 7`, we take the first 4 bits (`0011`) from one number

    0000 0000 0000 0000 0000 0000 0000 0011
    
and insert them into another number

    1010 1010 1010 1010 1010 1010 1010 1010
    
to obtain

    1010 1010 1010 1010 1010 1010 0011 1010
