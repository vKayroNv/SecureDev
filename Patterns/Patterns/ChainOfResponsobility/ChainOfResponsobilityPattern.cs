using Patterns.ChainOfResponsobility.Handlers;

namespace Patterns.ChainOfResponsobility
{
    public static class ChainOfResponsobilityPattern
    {
        public static void Run()
        {
            var first = new FirstHandler();
            var second = new SecondHandler();
            var third = new ThirdHandler();
            first.SetNext(second).SetNext(third);

            Client.Run(first);
            Console.WriteLine();

        }
    }
}
