using Patterns.AbstractFactory.Interfaces;

namespace Patterns.AbstractFactory.Factories
{
    internal class Client
    {
        private readonly IAbstractProduct abstractProduct;

        public Client(IAbstractFactory factory)
        {
            abstractProduct = factory.CreateProduct();
        }

        public void Run()
        {
            Console.WriteLine(abstractProduct.Name);
        }
    }
}
