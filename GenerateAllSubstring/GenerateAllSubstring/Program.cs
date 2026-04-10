using System;
using System.Data.SqlTypes;
using System.Text;
public class GenerateAllSubstring
{

    public static StringBuilder Solve(StringBuilder sb) {
        int n = sb.Length;
        Console.WriteLine(n);
        StringBuilder sbans = new StringBuilder();
        
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                sbans.Append(sb.ToString(i, j-i+1));
                sbans.Append(" ");
            }
        }
        

        return sbans;
    }
    public static void Main(String[] args)
    {
        Console.WriteLine("input a string");
        StringBuilder sb = new StringBuilder(Console.ReadLine());
        Console.WriteLine(Solve(sb));
        
    }
}