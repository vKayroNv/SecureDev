using Patterns.AbstractFactory.Interfaces;
using Patterns.AbstractFactory.Products;

namespace Patterns.AbstractFactory.Factories
{
    internal class Factory2 : IAbstractFactory
    {
        public IAbstractProduct CreateProduct()
        {
            return new Product2();
        }
    }
}
