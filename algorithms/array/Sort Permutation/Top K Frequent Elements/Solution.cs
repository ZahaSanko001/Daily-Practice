public class Solution {
    public int[] TopKFrequent(int[] nums, int k) {
        Dictionary<int, int> count = new Dictionary<int, int>();

        foreach (int e in nums) {
            if (!count.ContainsKey(e)) {
                count[e] = 1;
            }
            else {
                count[e] += 1;
            }
        }

        List<int[]> arr = count.Select(entry => new int[] {
            entry.Value,
            entry.Key
        }).ToList();

        arr.Sort((a, b) => b[0].CompareTo(a[0]));

        int[] res = new int[k];
        for (int i = 0; i<k; i++) {
            res[i] = arr[i][1];
        }

        return res;
    }
}
