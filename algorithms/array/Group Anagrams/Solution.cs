public class Solution {
    public List<List<string>> GroupAnagrams(string[] strs) {
        Dictionary<string, List<string>> res = new Dictionary<string, List<string>>();
        foreach (var s in strs) {
            char[] charArray = s.ToCharArray();
            Array.Sort(charArray);
            string sorted = new string(charArray);
            if (!res.ContainsKey(sorted)) {
                res[sorted] = new List<string>();
            }
            res[sorted].Add(s);
        }
        return res.Values.ToList();
    }
}
