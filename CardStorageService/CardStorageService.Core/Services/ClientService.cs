using AutoMapper;
using CardStorageService.Core.Interfaces;
using CardStorageService.Core.Models;
using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Models;

namespace CardStorageService.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
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
                return _mapper.Map<IReadOnlyList<ClientDto>>(await _clientRepository.GetAll(cts));
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
                return _mapper.Map<ClientDto>(await _clientRepository.GetById(id, cts));
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
