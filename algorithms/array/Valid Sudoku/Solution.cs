public class Solution {
    public bool IsValidSudoku(char[][] board) {

        for (int i = 0; i < board.Length; i++) {
            HashSet<char> row = new HashSet<char>();
            HashSet<char> col = new HashSet<char>();
            foreach (var c in board[i]) {
                if (c is '.') continue;
                bool isAddedr = row.Add(c);
                if (!isAddedr) return false;
            }
            for (int j = 0; j < board[0].Length; j++) {
                if (board[j][i] == '.') continue;
                bool isAddedc = col.Add(board[j][i]);
                if (!isAddedc) return false;
            }
        }

        int[][] squares = new int[][] 
        {
            new int[] {0,0}, new int[] {0,3}, new int[] {0,6},
            new int[] {3,0}, new int[] {3,3}, new int[] {3,6},
            new int[] {6,0}, new int[] {6,3}, new int[] {6,6}
        };
        foreach (int[] s in squares) {
            HashSet<char> sqr = new HashSet<char>(); 

            for (int n = s[0]; n < s[0] + 3; n++) {
                for (int m = s[1]; m < s[1] + 3; m++) {
                    if (board[n][m] == '.') continue;
                    if (!sqr.Add(board[n][m])) return false;
                }
            }
        }
        return true;
    }
}
