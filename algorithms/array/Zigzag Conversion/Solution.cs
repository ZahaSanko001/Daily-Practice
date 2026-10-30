public class Solution {
    public string Convert(string s, int numRows) {
        if (numRows == 1) return s;

        int idx = 0;
        int dir = 1;
        var res = new StringBuilder();

        var rows = new StringBuilder[numRows];
        for (int i = 0; i < numRows; i++) rows[i] = new StringBuilder();

        foreach (char c in s) {
            rows[idx].Append(c);
            if (idx == 0) dir = 1;
            else if (idx == numRows - 1) dir = -1;
            idx += dir;
        }
        foreach (var row in rows) res.Append(row);

        return res.ToString();
    }
}

// O(n)