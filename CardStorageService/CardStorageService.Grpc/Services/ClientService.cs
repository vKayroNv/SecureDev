using CardStorageService.Grpc.Protos;
using CardStorageService.Storage.Interfaces;
using Grpc.Core;
using static CardStorageService.Grpc.Protos.ClientService;

namespace CardStorageService.Grpc.Services
{
    public class ClientService : ClientServiceBase
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public override async Task<ClientCreateResponse> Create(ClientCreateRequest request, ServerCallContext context)
        {
            var result = await _repository.Create(new()
            {
                FirstName = request.FirstName,
                Surname = request.Surname,
                Patronymic = request.Patronymic
            }, context.CancellationToken);

            return new() { ClientId = result };
        }

        public override async Task<ClientDeleteResponse> Delete(ClientDeleteRequest request, ServerCallContext context)
        {
            var result = await _repository.Delete(request.ClientId, context.CancellationToken);

            return new() { Count = result };
        }

        public override async Task<ClientGetAllResponse> GetAll(ClientGetAllRequest request, ServerCallContext context)
        {
            var result = await _repository.GetAll(context.CancellationToken);

            ClientGetAllResponse response = new();

            foreach (var client in result)
            {
                response.Clients.Add(new Client()
                {
                    ClientId = client.ClientId,
                    FirstName = client.FirstName,
                    Surname = client.Surname,
                    Patronymic = client.Patronymic
                });
            }

            return response;
        }

        public override async Task<ClientGetByIdResponse> GetById(ClientGetByIdRequest request, ServerCallContext context)
        {
            var result = await _repository.GetById(request.ClientId, context.CancellationToken);

            return new()
            {
                Client = new()
                {
                    ClientId = result.ClientId,
                    FirstName = result.FirstName,
                    Surname = result.Surname,
                    Patronymic = result.Patronymic
                }
            };
        }

        public override async Task<ClientUpdateResponse> Update(ClientUpdateRequest request, ServerCallContext context)
        {
            var result = await _repository.Update(new()
            {
                ClientId = request.ClientId,
                FirstName = request.FirstName,
                Surname = request.Surname,
                Patronymic = request.Patronymic
            }, context.CancellationToken);

            return new() { Count = result };
        }
    }
}
