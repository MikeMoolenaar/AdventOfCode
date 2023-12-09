# Day 9
- [AoC website](https://adventofcode.com/2023/day/9)
- Solve time: 0h30m

Easy peasy! Had some trouble with the loop finding the next history value, but overall nothing special.

- CameronAavik [has a fast solution](https://github.com/CameronAavik/AdventOfCode/blob/master/csharp/2023/Solvers/Day09.cs) using spans and `Vector256` (I don't understand it...).
- encse and some others [used zip+select](https://github.com/encse/adventofcode/blob/master/2023/Day09/Solution.cs#L20) to cleverly get the array of diffs He used 2 lists, where the second one is just the history without the first value. Apparently it's a common trick, neat!