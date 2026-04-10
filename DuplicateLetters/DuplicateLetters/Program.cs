using System;
using System.Text;

public class DuplicateLetters
{
    public static int Solve(StringBuilder sb)
    {
        int n = sb.Length;
        int cnt = 0;
        int[] letters = new int[26];
        for(int i  = 0;i<n;i++) 
        {
            letters[sb[i] - 'a']++;
            if (letters[sb[i] - 'a'] > 1) cnt++;
        }



        return cnt;
    }
    public static void Main(String[] args)
    {
        Console.WriteLine("Input a duplicated letters string: ");
        StringBuilder sb = new StringBuilder(Console.ReadLine());
        int ans = Solve(sb);
        Console.WriteLine("Total duplicated letters in {0} = {1}", sb, ans);
    }
}
