using System;
using System.Text;

public class ReverseWord
{
    public static void reverse(StringBuilder sb, int i, int j)
    {
        while(i<j)
        {
            char c = sb[i];
            sb[i] = sb[j];
            sb[j] = c;
            i++;
            j--;
        }
    }
    public static void Solve(StringBuilder sb)
    {
        int n = sb.Length;
        int i = 0;
        int j = 0;
        while(j<n)
        {
            if (sb[j]==' ')
            {
                reverse(sb, i, j - 1);
                i = j + 1;
            }
            j++;
        }
    }
    public static void Main(String[] args)
    {
        Console.WriteLine("Write a sentence : ");
        StringBuilder sb = new StringBuilder(Console.ReadLine());
        Solve(sb);
        Console.WriteLine(sb);
    }
}