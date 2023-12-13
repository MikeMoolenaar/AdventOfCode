# Day 12
- [AoC website](https://adventofcode.com/2023/day/12)
- Solve time: 3h0m

Solved the first part with brute force (will I ever learn?) which was quite fun as I learned about converting integers to binary values to generate all possibilities of hash or pound.  But that didn't work for part 2 and I didn't solve it myself, it was too hard.  
Thanks to reddit user StaticMoose, he made an excellent [tutorial how he solved it in python](https://www.reddit.com/r/adventofcode/comments/18hbbxe/2023_day_12python_stepbystep_tutorial_with_bonus/). Using recursion and memoization (caching of the recursion function), this solution calculates parts of the groups of the record.  

Probably could do this by myself; next time I should read the tutorial, but just to get a hint. In this example that was: chop the record up in groups and use recursion + caching to calculate the value.
