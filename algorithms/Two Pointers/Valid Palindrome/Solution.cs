public class Solution {
    public bool IsPalindrome(string s) {
        var text = new StringBuilder();
        foreach(char c in s) {
            if (char.IsLetterOrDigit(c)) text.Append(char.ToLower(c));
        }
        int left = 0;
        int right = text.Length - 1;
        while (left < right) {
            if (text[left] != text[right]) return false;
            left++;
            right--;
        }
        return true;
    }
}
