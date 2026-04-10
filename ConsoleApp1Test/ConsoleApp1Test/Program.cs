using System;
using System.Text;

class Reverse
{

    public static void swap(StringBuilder input, int i, int j)
    {
        char temp = input[j];
        input[j] = input[i];
        input[i] = temp;
    }


    public static void Main(String[] args)
    {
        Console.WriteLine("input a string : ");
        StringBuilder input = new StringBuilder(Console.ReadLine());
        int n = input.Length;
        int i = 0, j = n - 1;
        while(i<j)
        {
            swap(input, i, j);
            i++;
            j--;
        }
        Console.WriteLine("Here is your output: ");
        Console.WriteLine(input);
    }
}