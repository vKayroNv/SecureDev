using Patterns.ChainOfResponsobility.Objects;

namespace Patterns.ChainOfResponsobility.Handlers
{
    class SecondHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if (request.GetType() == typeof(SecondObject))
            {
                return $"Process Id: {(request as SecondObject)!.Id}";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
