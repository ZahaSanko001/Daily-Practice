public class Solution {

    public string Encode(IList<string> strs) {
        var sb = new StringBuilder();
        foreach (var s in strs) {
            sb.Append(s.Length).Append('#').Append(s);
        }
        return sb.ToString();
    }

    public List<string> Decode(string s) {
        var result = new List<string>();
        int i = 0;
        while (i < s.Length) {
            int j = s.IndexOf('#', i);            
            int len = int.Parse(s.AsSpan(i, j - i));
            result.Add(s.Substring(j + 1, len));  
            i = j + 1 + len;                      
        }
        return result;
    }
}
