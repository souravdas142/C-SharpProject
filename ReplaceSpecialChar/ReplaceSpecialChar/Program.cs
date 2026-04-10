using System;
using System.Text;

public class ReplaceSpecialChar
{
    public static void Solve(StringBuilder sb)
    {
        int n = sb.Length;
        for(int i = 0;i<n;i++)
        {
            if (!((sb[i]>='a' && sb[i]<='z') || (sb[i]>='A' && sb[i]<='Z') || (sb[i]>='0' && sb[i]<='9') || sb[i]==' '))
            {
                sb[i] = '#';
            }
        }
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Input a string; ");
        StringBuilder sb = new StringBuilder(Console.ReadLine());
        Solve(sb);
        Console.WriteLine(sb);
    }
}