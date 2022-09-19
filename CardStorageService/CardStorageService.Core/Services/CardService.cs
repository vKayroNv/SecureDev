using CardStorageService.Core.Interfaces;
using CardStorageService.Core.Models;
using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Models;

namespace CardStorageService.Core.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
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
                var result = new List<CardDto>();
                var cards = await _cardRepository.GetAll(cts);

                foreach (var card in cards)
                {
                    result.Add(new()
                    {
                        CardId = card.CardId,
                        CardNo = card.CardNo,
                        CVV2 = card.CVV2,
                        ExpDate = card.ExpDate,
                        Name = card.Name
                    });
                }
                return result;
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
                var result = new List<CardDto>();
                var cards = await _cardRepository.GetByClientId(id, cts);

                foreach (var card in cards)
                {
                    result.Add(new()
                    {
                        CardId = card.CardId,
                        CardNo = card.CardNo,
                        CVV2 = card.CVV2,
                        ExpDate = card.ExpDate,
                        Name = card.Name
                    });
                }
                return result;
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

                return new()
                {
                    CardId = entity.CardId,
                    CardNo = entity.CardNo,
                    CVV2 = entity.CVV2,
                    ExpDate = entity.ExpDate,
                    Name = entity.Name
                };
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
