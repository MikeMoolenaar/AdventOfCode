# Day 19
- [AoC website](https://adventofcode.com/2023/day/19)
- Solve time: 3h0m

start: 19:40

A fun and challenging solution that didn't take me 6+ hours??? Honestly I'm proud of how quick I received this solution, partly due to writing it down on paper. I did get help from Reddit, mainly [this comment](https://www.reddit.com/r/adventofcode/comments/18mdnfj/comment/ke3k5yy/?utm_source=share&utm_medium=web2x&context=3) suggesting to use ranges to calculate the possibilities, similar to day 5 part 2 (though I brute forced that one....). Apart from this comment, I didn't look at any solutions. 
The code runs in ~20ms so I won't be doing any optimisations this time.  

- viceroypenguin [used a queue](https://github.com/viceroypenguin/adventofcode/blob/master/AdventOfCode.Puzzles/2023/day19.original.cs#L91) with a dictionary for the variable as key and ranges as values. I used a record of ranges, which resulted in me having to write an ugly switch case to handle all 4 variables, using a dictionary is much cleaner.  
- encse approached the solution as [a four dimensional hypercube](https://github.com/encse/adventofcode/blob/master/2023/Day19/Solution.cs) to caclulate ranges (as you have 4 dimensions of ranges).