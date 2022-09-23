using CardStorageService.Storage;
using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace CardStorageService.Storage.Repositories
{
    public class AccountSessionRepository : IAccountSessionRepository
    {
        private readonly DatabaseContext _context;

        public AccountSessionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> Create(AccountSession data, CancellationToken cts)
        {
            try
            {
                await _context.AccountSessions.AddAsync(data, cts);
                await _context.SaveChangesAsync(cts);

                return data.SessionId;
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
                var entity = await _context.AccountSessions.FirstOrDefaultAsync(obj => obj.SessionId == id, cts);
                if (entity == null)
                {
                    throw new Exception("Session Id not found");
                }

                _context.AccountSessions.Remove(entity);

                await _context.SaveChangesAsync(cts);

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<AccountSession>> GetAll(CancellationToken cts)
        {
            try
            {
                return await _context.AccountSessions.ToListAsync(cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<AccountSession> GetById(int id, CancellationToken cts)
        {
            try
            {
                var entity = await _context.AccountSessions.FirstOrDefaultAsync(obj => obj.SessionId == id, cts);
                if (entity == null)
                {
                    throw new Exception("Session Id not found");
                }

                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Update(AccountSession data, CancellationToken cts)
        {
            try
            {
                var entity = await _context.AccountSessions.FirstOrDefaultAsync(obj => obj.SessionId == data.SessionId, cts);
                if (entity == null)
                {
                    throw new Exception("Session Id not found");
                }

                entity.SessionToken = data.SessionToken;
                entity.AccountId = data.AccountId;
                entity.TimeCreated = data.TimeCreated;
                entity.TimeLastRequest = data.TimeLastRequest;
                entity.TimeClosed = data.TimeClosed;

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
