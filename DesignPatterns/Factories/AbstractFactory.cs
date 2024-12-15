namespace DesignPatterns.Factories;

public class AbstractFactory
{
    public interface IHotDrink
    {
        void Consume();
    }
    
    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            throw new System.NotImplementedException();
        }
    }
    
    class Program
    {
        public static void Runner()
        {
            
        }
    }
}