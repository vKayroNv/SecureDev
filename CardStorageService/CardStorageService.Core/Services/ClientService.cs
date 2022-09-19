using CardStorageService.Core.Interfaces;
using CardStorageService.Core.Models;
using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Models;

namespace CardStorageService.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int> Create(Client data, CancellationToken cts)
        {
            try
            {
                return await _clientRepository.Create(data, cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Delete(int id, CancellationToken cts)
        {
            try
            {
                return await _clientRepository.Delete(id, cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<ClientDto>> GetAll(CancellationToken cts)
        {
            try
            {
                var result = new List<ClientDto>();
                var clients = await _clientRepository.GetAll(cts);

                foreach(var client in clients)
                {
                    result.Add(new()
                    {
                        ClientId = client.ClientId,
                        FirstName = client.FirstName,
                        Surname = client.Surname,
                        Patronymic = client.Patronymic
                    });
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClientDto> GetById(int id, CancellationToken cts)
        {
            try
            {
                var entity = await _clientRepository.GetById(id, cts);

                return new()
                {
                    ClientId = entity.ClientId,
                    FirstName = entity.FirstName,
                    Surname = entity.Surname,
                    Patronymic = entity.Patronymic
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Update(Client data, CancellationToken cts)
        {
            try
            {
                return await _clientRepository.Update(data, cts);
            }
            catch
            {
                throw;
            }
        }
    }
}
