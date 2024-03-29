# Day 5
- [AoC website](https://adventofcode.com/2023/day/5)
- Solve time: 1h45m

Ok this was fun. My first try ran out of RAM because I created every possible seed in a dictionary (which resulted in 20GB of allocations). Second try worked with checking the seed ranges, which took me WAY too long because I got confused on the range checks (less-than and greater-than are confusing for me...). Next time I should take a look at the input and just do the range-check solution from the get-go if I notice the numbers are large to save time.   
Part 2 was solved quite quickly, though it takes about 6 minutes to run for me.  

This [reddit user](https://www.reddit.com/r/adventofcode/comments/18b4b0r/comment/kc4dmtd/?utm_source=share&utm_medium=web2x&context=3) mentioned he also tried the brute force way and the code took 40 mins to run, I'm glad I "only" have 16gb of RAM so it forced me to code a better solution.

- CameronAavik [used a binary search](https://github.com/CameronAavik/AdventOfCode/blob/master/csharp/2023/Solvers/Day05.cs) and ReadOnlySpan's which must be faster, I don't get the solution though, might be worth a look later.  
- viceroypenguin used [span slices](https://github.com/viceroypenguin/adventofcode/blob/master/AdventOfCode.Puzzles/2023/day05.fastest.cs) to make it fast.  
- Reddit user Salad-Extension [used a range split algorithm](https://topaz.github.io/paste/#XQAAAQCuCQAAAAAAAAA6nMlWi076alCx9N1TsRv/nE/5+HEJqrfgdiKbnpiCU30HOqctQj7Jp3T1Xt0zuyBzJjB+ise8j8R3bmPwIDTyo8c++CYGfLfshnb0jcBhyL/6GbQ1a8Qtge1cQaPvZR3+xe3RZDpCKCOWiFPJPImoJaIv3pnwNtjg2obpk/dPK02M9A2hxkZd/N5weImKnXk5rvmjdEgOMfaRpJas/b+kM41Xpp7e/43hP/fTVinr/M3K2Kq8Gz0k43OrCba4eaSxjWl4lqI9Z58GkRGp+zttskYw5R707lpxp1bpHIKRYc5WoPlZp6pzW5zfyjTuDZ5oDMyvcve1HW2mTqtjVbh49A0soaVCTXNB343GcmTi4R81Zs09idT7fxw5EpuVztX7rgEUJK8z172X4cMvaNfoDmoADuXK0qHUFpxP8lmZjL/JRY+zyPto6KsCnOvRvuC8N7C4YjJDW3EfWy44sIEcbIuD1C1tDGXCHL+Z6xIoedxLAZ9BaEYTUHhgyoQBvfJtNt7N2VbhmIpFoMxe3Ir+YHPwWaLT/YpJHsJz2/B3/+OcwJ9eUF/8qXWIjRM8abM3HjM5Rx/vDalZoJQ0zRBu4vRYwhWIwUIH5mgRHVrVsMWambDbsmKfhFeJ+TUsMqY6AI2p4l6OsRG+4om2ccJ7V4SJXykFXOOfB4iazn9U02VWUiV1Ac+mnovRunLCMuN6KHITD0mH18+x7Le5r9Wv7kBO+KDGMIxowVAinxfdfeNgamFzGOFR/BEd89OmXr6ftGokpHfg7YiCaC/ISuidD+eBrdRs+db4xu2d/9sIlWR1jDcygVHCauYDFKo8lGgbYjOTVqaFG7XSylOc3UaZi6DUWB4OcUyEC59Eraugm5tYK2ih2FqaS9s38BI7vbQmJkWJ6qOR1yGVKkQolTUfRGcqA+Ps+O9b6HwTW/6yIRfvw4CLqS8CDUiTFqYLQ2EDRqpqz2DsYfg6MraPqaHwQYrdqi6CKB/nD7op7fqvuRC7aMiMR7fWtLBP7AumvNyFBsxUhLE7cZBxJ1Jt0UL7zq9TY4psV0FZpfbIbl87vMSL348OEhIstut7z6opBRdeh//ZPcoH) which runs in 8ms.

Only the last one works for me btw, not sure why.