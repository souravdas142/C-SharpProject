using System.Runtime.InteropServices;

namespace HelloWorldMVC1.Models
{
    public class Students
    {
        public int id { get; set; }
        public string name { get; set; }
        public string course { get; set; }
        public Students(int id, string name, string course)
        {
            this.id = id;
            this.name = name;
            this.course = course;
        }
    }
}
