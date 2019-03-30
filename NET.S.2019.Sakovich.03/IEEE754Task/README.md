# IEEE754Task (Day 3, Task 2)

## Task description

The task is to implement an extension method on `double` which would return a string representation of its 64 bits.

### Example
For the following `double`

    4.235732815836542e307
    
the returned string must be

    0111111111001110001010001100111110101010110110000001111010001111
    
### P.S.
I used the following expression to generate test cases:

    IntegerString[FromDigits[Reverse @ ImportString[ExportString[n, "Real64"], "Byte"], 256], 2, 64]
