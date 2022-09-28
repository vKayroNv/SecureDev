using AutoMapper;
using CardStorageService.Core.Interfaces;
using CardStorageService.Core.Models;
using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Models;

namespace CardStorageService.Core.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<string> Create(Card data, CancellationToken cts)
        {
            try
            {
                return await _cardRepository.Create(data, cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Delete(string id, CancellationToken cts)
        {
            try
            {
                return await _cardRepository.Delete(id, cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<CardDto>> GetAll(CancellationToken cts)
        {
            try
            {
                return _mapper.Map<IReadOnlyList<CardDto>>(await _cardRepository.GetAll(cts));
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<CardDto>> GetByClientId(int id, CancellationToken cts)
        {
            try
            {
                return _mapper.Map<IReadOnlyList<CardDto>>(await _cardRepository.GetByClientId(id, cts));
            }
            catch
            {
                throw;
            }
        }

        public async Task<CardDto> GetById(string id, CancellationToken cts)
        {
            try
            {
                var entity = await _cardRepository.GetById(id, cts);

                return _mapper.Map<CardDto>(entity);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Update(Card data, CancellationToken cts)
        {
            try
            {
                return await _cardRepository.Update(data, cts);
            }
            catch
            {
                throw;
            }
        }
    }
}
