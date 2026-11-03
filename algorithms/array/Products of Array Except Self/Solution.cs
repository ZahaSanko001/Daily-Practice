public class Solution {
    public int[] ProductExceptSelf(int[] nums) {
        int[] output = new int[nums.Length];
        int[] suffix = new int[nums.Length]; 
        int[] prefix = new int[nums.Length];

        int multiplier = 1;
        for (int i = 0; i < nums.Length; i++) {
            if (i == 0) prefix[i] = 1;
            else {
                multiplier *= nums[i-1];
                prefix[i] = multiplier;
            }
        } 

        multiplier = 1;
        for (int i = nums.Length - 1; i >= 0; i--) {
            if (i == nums.Length - 1) suffix[i] = 1;
            else {
                multiplier *= nums[i+1];
                suffix[i] = multiplier;
            }
        }

        for (int i = 0; i < nums.Length; i++) {
            output[i] = suffix[i] * prefix[i];
        } 

        return output;
    }
}

// O(n)