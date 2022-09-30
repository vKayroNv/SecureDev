using Patterns.ChainOfResponsobility.Objects;

namespace Patterns.ChainOfResponsobility.Handlers
{
    class FirstHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.GetType() == typeof(FirstObject))
            {
                return $"Execution time: {(request as FirstObject)!.DateTime}";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
