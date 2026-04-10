using System;
using System.Collections.Generic;

interface INotification
{
    void Send(string msg);
}

class EmailNotification : INotification
{
    public void Send(string msg)
    {
        Console.WriteLine("Sending EMAIL " + msg);
    }
}

class SmsNotification : INotification
{
    public void Send(string msg)
    {
        Console.WriteLine("Sending SMS " + msg);
    }
}

class PushNotification : INotification
{
    public void Send(string msg)
    {
        Console.WriteLine("Sending PUSH " + msg);
    }
}

class Interface1
{
    public static void Main(string[] args)
    {
        List<INotification> lst = new List<INotification>();
        lst.Add(new EmailNotification());
        lst.Add(new SmsNotification());
        lst.Add(new PushNotification());

        foreach(INotification notif in lst)
        {
            notif.Send("Msg");
        }
    }
}