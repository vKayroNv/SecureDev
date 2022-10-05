using Grpc.Net.Client;
using static CardStorageService.Grpc.Protos.CardService;
using static CardStorageService.Grpc.Protos.ClientService;

namespace CardStorageServiceClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.Http2UnencryptedSupport", true);

            using var channel = GrpcChannel.ForAddress("http://localhost:5001");

            ClientServiceClient clientService = new(channel);

            var createClientResponse = clientService.Create(new()
            {
                FirstName = "Kirill",
                Surname = "Goncharov",
                Patronymic = "Alekseevich"
            });

            Console.WriteLine($"Client {createClientResponse.ClientId} created successfully.");

            CardServiceClient cardService = new(channel);

            var createCardResponse = cardService.Create(new()
            {
                ClientId = createClientResponse.ClientId,
                CardNo = "2202202212345678",
                CVV2 = "123",
                Name = "KIRILL G.",
                ExpDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(new(2025, 12, 01, 0, 0, 0, DateTimeKind.Utc))
            });

            Console.WriteLine($"Card {createCardResponse.CardId} created successfully.");

            var getByClientIdResponse = cardService.GetByClientId(new()
            {
                ClientId = createClientResponse.ClientId
            });

            Console.WriteLine("Cards:");
            Console.WriteLine("======");

            foreach (var card in getByClientIdResponse.Cards)
            {
                Console.WriteLine($"{card.CardNo}; {card.Name}; {card.CVV2}; {card.ExpDate}");
            }

            Console.ReadKey();
        }
    }
}