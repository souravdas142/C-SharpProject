using System;
using System.Text;

class  Employee
{
    private int id { get; }
    private string name { get; }

    public int getId()
    {
        return id;
    }

    public string getName()
    {
        return name;
    }

    public  Employee(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public virtual double calcSalary()
    {
        return 0.0f;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Employee Name: ");
        sb.Append(getName());
        sb.Append("\nEmployee ID: ");
        sb.Append(getId());

        return sb.ToString();


    }

}

class PermanentEmployee : Employee
{
    private double basicSalary;
    public PermanentEmployee(int id, string name, double bsc) : base(id,name)
    {
        
        basicSalary = bsc;
    }

    public override double calcSalary()
    {
        basicSalary = basicSalary + (basicSalary * (20 / 100));
        return basicSalary;
    }


}

class ContractEmployee : Employee
{
    private double hourlyRate;
    private double hoursWorked;
    public ContractEmployee(int  id, string name, double hr, double hoursWorked) : base (id,name)
    {
        hourlyRate = hr;
        this.hoursWorked = hoursWorked;
    }

    public override double calcSalary()
    {
        double salary = hoursWorked * hourlyRate;
        return salary;
    }
}

class Inharitance1
{
    public static void Main(string[] args)
    {
        Employee emp = new PermanentEmployee(101, "shilpa", 100);
        Console.WriteLine(emp);
        Console.WriteLine("Salary of permanent employee : " + emp.calcSalary());

        emp = new ContractEmployee(102, "sourav", 10, 8);
        Console.WriteLine(emp);
        Console.WriteLine("Salary of permanent employee : " + emp.calcSalary());

    }
}