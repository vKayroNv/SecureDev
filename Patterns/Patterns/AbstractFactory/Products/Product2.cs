using Patterns.AbstractFactory.Interfaces;

namespace Patterns.AbstractFactory.Products
{
    internal class Product2 : IAbstractProduct
    {
        private readonly string _name;

        public string Name { get => _name; }

        public Product2()
        {
            _name = "Product 2";
        }
    }
}
