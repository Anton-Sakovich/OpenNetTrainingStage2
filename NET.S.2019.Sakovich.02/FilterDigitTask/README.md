# FilterDigitTask (Day 2, Task 4)

## Task description

The task is to implement a method which for a given array of integers and a decimal digit returns an array with
those numbers from the input array which contain the specified digit.

The implementation presented here takes a flag `deleteDuplicates`, which determines whether repeated numbers
must be deleted from the resulting array.

### Example

For the array `{1, 2, 24, 15, 87, 17, 1}` and the digit `1` the result is `{1, 15, 17, 1}` for
`deleteDuplicates = false` and `{1, 15, 17}` for `deleteDuplicates = true`.
