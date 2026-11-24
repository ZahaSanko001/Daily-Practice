public class Solution {
    public int MaxArea(int[] heights) {
        int left = 0;
        int right = heights.Length - 1;
        int area = 0;

        while (left < right) {
            int temp = Math.Min(heights[left], heights[right]) * (right - left);
            if (temp > area) area = temp;
            if (heights[left] < heights[right]) left++;
            else right--;
        }

        return area;
    }
}
