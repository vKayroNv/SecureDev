using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace CardStorageService.Storage.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly DatabaseContext _context;

        public CardRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<string> Create(Card data, CancellationToken cts)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(client => client.ClientId == data.ClientId, cts);
                if (client == null)
                {
                    throw new Exception("Client not found");
                }

                await _context.Cards.AddAsync(data, cts);
                await _context.SaveChangesAsync(cts);

                return data.CardId.ToString();
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
                var entity = await _context.Cards.FirstOrDefaultAsync(obj => obj.CardId.ToString() == id, cts);
                if (entity == null)
                {
                    throw new Exception("Card Id not found");
                }

                _context.Cards.Remove(entity);

                await _context.SaveChangesAsync(cts);

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<Card>> GetAll(CancellationToken cts)
        {
            try
            {
                return await _context.Cards.ToListAsync(cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<Card>> GetByClientId(int id, CancellationToken cts)
        {
            try
            {
                return await _context.Cards.Where(obj => obj.ClientId == id).ToListAsync(cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Card> GetById(string id, CancellationToken cts)
        {
            try
            {
                var entity = await _context.Cards.FirstOrDefaultAsync(obj => obj.CardId.ToString() == id, cts);
                if (entity == null)
                {
                    throw new Exception("Card Id not found");
                }

                return entity;
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
                var entity = await _context.Cards.FirstOrDefaultAsync(obj => obj.CardId.ToString() == data.CardId.ToString(), cts);
                if (entity == null)
                {
                    throw new Exception("Card Id not found");
                }

                entity.CardNo = data.CardNo;
                entity.ClientId = data.ClientId;
                entity.CVV2 = data.CVV2;
                entity.ExpDate = data.ExpDate;
                entity.Name = data.Name;

                await _context.SaveChangesAsync(cts);

                return 1;
            }
            catch
            {
                throw;
            }
        }
    }
}
