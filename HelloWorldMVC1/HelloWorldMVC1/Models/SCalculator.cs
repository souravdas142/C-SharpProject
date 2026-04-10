using HelloWorldMVC1.Models;


namespace HelloWorldMVC1.Models
{
    public class SCalculator : MCalculator
    {
        public void pow()
        {
            action = (int)Math.Pow(a, b);
        }
    }
}
