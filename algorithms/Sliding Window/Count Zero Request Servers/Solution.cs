using System.Collections.Generic;

public class Solution {
    public int[] CountServers(int n, int[][] logs, int x, int[] queries) {
        Array.Sort(logs, (a, b) => a[1].CompareTo(b[1]));

        int q = queries.Length;
        int[] order = new int[q];
        for (int i = 0; i < q; i++) order[i] = i;
        Array.Sort(order, (a, b) => queries[a].CompareTo(queries[b]));

        int[] count = new int[n + 1];
        int active = 0;
        int left = 0, right = 0;
        int[] result = new int[q];

        foreach (int idx in order) {
            int end = queries[idx];
            int start = end - x;

            while (right < logs.Length && logs[right][1] <= end) {
                if (count[logs[right][0]]++ == 0) active++;
                right++;
            }
            
            while (left < right && logs[left][1] < start) {
                if (--count[logs[left][0]] == 0) active--;
                left++;
            }

            result[idx] = n - active;
        }

        return result;
    }
}