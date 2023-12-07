# Day 7
- [AoC website](https://adventofcode.com/2023/day/7)
- Solve time: 1h25m

Really fun assigment today! Had 2 problems.  
First problem was that I compared the scores wrong, I forgot that I didn't reverse the label list, so it put them in the reverse order when comparing equal scores.  
Second problem is constructing the map of labels => number_of_labels. I tried a dictionary first, but then realised I had to sort the number_of_labels ascending which was a pain using a dictionary. Ended using a list with tuples, though updating one of the tuple values is ugly because you need to update the WHOLE tuple.  
I did try the dictionary later which resulted in cleaner code, but was also ~25% slower (120ms vs 90ms). I'll just keep the more performant one, but I would use the dictionary solution if the code needed to be maintained and the 30ms difference doesn't matter.  

And again encse (how smart is this man??) has [a brilliant solution](https://github.com/encse/adventofcode/blob/master/2023/Day07/Solution.cs). He adds the number of occurrence of each hand and the scores to eachother (using BigIntegers).  
