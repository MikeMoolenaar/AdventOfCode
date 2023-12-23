# Day 15
- [AoC website](https://adventofcode.com/2023/day/15)
- Solve time: 0h30m

Easy peasy today! Took me longer to read than to code lol.

grigoresc [split the input](https://github.com/grigoresc/adventofcode.2023/blob/master/day15/Solve.cs#L35) by using using `str.Split(new char[] { '-', '=' })` and the second value is empty when it's a remove operation. Didn't know you could use Split like that!