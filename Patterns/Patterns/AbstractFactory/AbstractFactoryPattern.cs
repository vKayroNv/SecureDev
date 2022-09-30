using Patterns.AbstractFactory.Factories;

namespace Patterns.AbstractFactory
{
    public static class AbstractFactoryPattern
    {
        public static void Run()
        {
            Client client1 = new(new Factory1());
            Client client2 = new(new Factory2());

            client1.Run();
            client2.Run();
        }
    }
}
