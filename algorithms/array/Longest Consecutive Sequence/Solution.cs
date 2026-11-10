public class Solution {
    public int LongestConsecutive(int[] nums) {
        HashSet<int> numbers = new HashSet<int>(nums);
        int i = 0;
        int len = 0;
        int longest = 0;
        foreach (int n in nums) {
            if (!numbers.Contains(n-1)) {
                i = n;
                len = 1;
                while (numbers.Contains(i+1)) {
                    len++;
                    i++;
                }
                longest = Math.Max(longest, len);
            }
        }

        return longest;
    }
}
