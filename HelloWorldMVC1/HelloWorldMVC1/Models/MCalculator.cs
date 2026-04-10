namespace HelloWorldMVC1.Models
{
    public class MCalculator
    {
        public int a { get; set; }
        public int b { get; set; }

        public int action;

        public MCalculator() { }

        public void Add()
        {
            action = a + b;
        }
        public void Min()
        {
            action = a - b;
        }

        public void Multi()
        {
            action = a * b;
        }
        public void Div()
        {
            action = a / b;
        }
    }
}
