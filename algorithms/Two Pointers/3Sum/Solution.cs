public class Solution {
    public List<List<int>> ThreeSum(int[] nums) {
        Array.Sort(nums);
        int left = 1;
        int right = nums.Length - 1;
        List<List<int>> res = new List<List<int>>();

        for (int i = 0; i < nums.Length - 2; i++) {
            if (nums[i] > 0) break;
            if (i > 0 && nums[i] == nums[i-1]) continue;
            left = i + 1;
            right = nums.Length - 1;

            while(left < right && i < right) {
                if (nums[i] + nums[left] + nums[right] == 0) {
                    res.Add(new List<int> {nums[i], nums[left], nums[right]});
                    while (left < right && nums[left + 1] == nums[left]) left++;
                    while (right > left && nums[right - 1] == nums[right]) right--;
                    left++;
                    right--;
                } else if(nums[i] + nums[left] + nums[right] > 0) {
                    right--;
                } else left++;
            }
        }

        return res;
    }
}
