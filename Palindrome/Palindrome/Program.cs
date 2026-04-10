using System;
using System.Text;

public class Palindrome
{
    public static bool Solve(StringBuilder sb)
    {
        int n = sb.Length;
        int i = 0;
        int j = n - 1;
        while(i<j) 
        {
            if (sb[i] != sb[j])
            {
                return false;
            }
            i++;
            j--;

        }
        return true;
    }
    public static void Main(String[] args)
    {
        Console.WriteLine("Input a string; ");
        StringBuilder sb = new StringBuilder(Console.ReadLine().ToLower());
        if(Solve(sb))
        {
            Console.WriteLine("Its a Palindrome");

        }
        else
        {
            Console.WriteLine("Its not a palindome");
        }
    }
}