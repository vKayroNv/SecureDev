using Patterns.AbstractFactory.Interfaces;
using Patterns.AbstractFactory.Products;

namespace Patterns.AbstractFactory.Factories
{
    internal class Factory1 : IAbstractFactory
    {
        public IAbstractProduct CreateProduct()
        {
            return new Product1();
        }
    }
}
