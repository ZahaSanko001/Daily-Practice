# Problem
```
The problem felt a bit confusing, what it wants is essentially a value which we can get doing AND operation for all pair of missplaced number positions. Each number should be nums[n] = n to be sorted. 

```
# Solution 
```
So the solution is essentially we go through the array and every time we find a missplaced number where nums[n] != n, we AND it with it's position: nums[n] & n. We keep a variable that updates the AND result each time we come across a missplaced number, this makes it so all the missplaced numbers have 1 bit in common which will let them all swap. And to initialize the variable we put -1 which is just a series of 1s in binary, if it stays that way in the end then that means the array is already sorted and k = 0.

```
# Time & Space complexity
```
Time: O(n) > goes through the array once
Space: O(1) > only the int variable stored in memory

