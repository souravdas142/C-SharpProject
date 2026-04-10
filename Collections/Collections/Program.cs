using System;
using System.Collections.Generic;


class Collections
{
    public static void Main(string[] args)
    {
        List<string> list1 = new List<string>()
        {
            "Sourav",
            "shilpa"

        };

        list1.Add("Sipra");
        list1.Add("Pritam");

        foreach (string s in list1)
        {
            Console.WriteLine(s);
        }

        list1.ForEach(s => Console.WriteLine(s));

        list1.Remove("Sipra");
        int n = list1.Count;

        Console.WriteLine(list1.Contains("Pritam")); //searching

        Dictionary<string, int> dict = new Dictionary<string, int>()
        {
            {"key1", 1 },
            {"key2", 2 }
        };
        Console.WriteLine(dict.ContainsKey("Pritam")); //searching




    }
}