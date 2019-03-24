# NthRootTask (Day 2, Task 5)

## Task description

The task is to find the n-th root of a given positive (let's skip complex numbers, OK? :)) `double` using
Newton's method.

### Remarks

The precision is specified by `AbsoluteErrorGoal` and `RelativeErrorGoal`. Two `double`s `y2` and `y1` are considered equal if

    |y2 - y1| < |y2| * RelativeErrorGoal + AbsoluteErrorGoal
