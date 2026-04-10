using System;
using System.Reflection.Emit;

class InsufficientBalance : Exception
{
    public override string ToString()
    {
        return base.ToString()+"Negetive value not allowed";
    }
}

class BankAccount
{
    public int accountNumber { get; }
    private double balance;

    public BankAccount(int x)
    {
        if (x < 0) balance = 0;
        accountNumber = x;
        balance = 25;
    }
    public double diposit(double x)
    {

        if (x < 0) return balance;
        balance += x;
        Console.WriteLine("Sucessfully dipositec" + x + " Ammount");
        return balance;
    }

    public double withDraw(double x)
    {
        try
        {
            if(balance-x<0)
            {
                throw new InsufficientBalance();
            }
            else
            {
                balance -= x;
                Console.WriteLine("Successfully withdrawn : " + x + " Ammount");
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        
        return balance;
    }



}

class oops1
{
    public static void Main(string[] args)
    {
        BankAccount sbi = new BankAccount(12345);
        Console.WriteLine("new balance = " + sbi.diposit(100));
        Console.WriteLine("New Balance = " + sbi.withDraw(30.5));
        Console.WriteLine("New Balance = " + sbi.withDraw(100.5));

    }
}