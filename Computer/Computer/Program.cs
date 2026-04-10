using System;

class Computer
{
    private int cpu;
    private string cpuBrand;
    private int ram;
    private int disk;
    public Computer(int cpu, int ram, int disk,string brand)
    {

        /*
         * A computer Has-A cpu,ram,disk
         * 
         * */

        this.cpu = cpu;
        this.ram = ram;
        this.disk = disk;
        this.cpuBrand = brand;

    }

    public bool is_computer_Work()
    {
        if (cpu > 0 && ram>0 && disk>0) return true;
        return false;
    }
    public void setBrand(string brand)
    {
        cpuBrand = brand;
    }
    
}

class World
{
    public static void Main(string[] args)
    {
        Computer laptop = new Computer(4,4,1000,"hp");
        laptop.setBrand("dell");
        // Even after changing the brand still is working...
        Console.WriteLine(laptop.is_computer_Work());
    }
}