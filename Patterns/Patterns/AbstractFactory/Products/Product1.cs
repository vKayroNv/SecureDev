using Patterns.AbstractFactory.Interfaces;

namespace Patterns.AbstractFactory.Products
{
    internal class Product1 : IAbstractProduct
    {
        private readonly string _name;

        public string Name { get => _name; }

        public Product1()
        {
            _name = "Product 1";
        }
    }
}
