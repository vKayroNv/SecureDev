using CardStorageService.Grpc.Protos;
using CardStorageService.Storage.Interfaces;
using Grpc.Core;
using static CardStorageService.Grpc.Protos.CardService;

namespace CardStorageService.Grpc.Services
{
    public class CardService : CardServiceBase
    {
        private readonly ICardRepository _repository;

        public CardService(ICardRepository repository)
        {
            _repository = repository;
        }

        public override async Task<CardCreateResponse> Create(CardCreateRequest request, ServerCallContext context)
        {
            var result = await _repository.Create(new()
            {
                CardNo = request.CardNo,
                ClientId = request.ClientId,
                CVV2 = request.CVV2,
                ExpDate = request.ExpDate.ToDateTime(),
                Name = request.Name
            }, context.CancellationToken);

            return new() { CardId = result };
        }

        public override async Task<CardDeleteResponse> Delete(CardDeleteRequest request, ServerCallContext context)
        {
            var result = await _repository.Delete(request.CardId, context.CancellationToken);

            return new() { Count = result };
        }

        public override async Task<CardGetAllResponse> GetAll(CardGetAllRequest request, ServerCallContext context)
        {
            var result = await _repository.GetAll(context.CancellationToken);

            CardGetAllResponse response = new();

            foreach (var card in result)
            {
                response.Cards.Add(new Card()
                {
                    CardId = card.CardId.ToString(),
                    CardNo = card.CardNo,
                    CVV2 = card.CVV2,
                    ExpDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(card.ExpDate),
                    Name = card.Name
                });
            }

            return response;
        }

        public override async Task<CardGetByClientIdResponse> GetByClientId(CardGetByClientIdRequest request, ServerCallContext context)
        {
            var result = await _repository.GetByClientId(request.ClientId, context.CancellationToken);

            CardGetByClientIdResponse response = new();

            foreach (var card in result)
            {
                response.Cards.Add(new Card()
                {
                    CardId = card.CardId.ToString(),
                    CardNo = card.CardNo,
                    CVV2 = card.CVV2,
                    ExpDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(card.ExpDate),
                    Name = card.Name
                });
            }

            return response;
        }

        public override async Task<CardGetByIdResponse> GetById(CardGetByIdRequest request, ServerCallContext context)
        {
            var result = await _repository.GetById(request.CardId, context.CancellationToken);

            return new()
            {
                Card = new()
                {
                    CardId = result.CardId.ToString(),
                    CardNo = result.CardNo,
                    CVV2 = result.CVV2,
                    ExpDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(result.ExpDate),
                    Name = result.Name
                }
            };
        }

        public override async Task<CardUpdateResponse> Update(CardUpdateRequest request, ServerCallContext context)
        {
            var result = await _repository.Update(new()
            {
                CardId = Guid.Parse(request.CardId),
                ClientId = request.ClientId,
                CardNo = request.CardNo,
                CVV2 = request.CVV2,
                ExpDate = request.ExpDate.ToDateTime(),
                Name = request.Name
            }, context.CancellationToken);

            return new() { Count = result };
        }
    }
}
