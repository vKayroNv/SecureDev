using Patterns.ChainOfResponsobility.Objects;

namespace Patterns.ChainOfResponsobility.Handlers
{
    class ThirdHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.GetType() == typeof(ThirdObject)) 
            {
                return $"Name: {(request as ThirdObject)!.Name}";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
