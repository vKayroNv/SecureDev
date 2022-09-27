using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace CardStorageService.Storage.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _context;

        public AccountRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Account data, CancellationToken cts)
        {
            try
            {
                await _context.Accounts.AddAsync(data, cts);
                await _context.SaveChangesAsync(cts);

                return data.AccountId;
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
                var entity = await _context.Accounts.FirstOrDefaultAsync(obj => obj.AccountId == id, cts);
                if (entity == null)
                {
                    throw new Exception("Account Id not found");
                }

                _context.Accounts.Remove(entity);

                await _context.SaveChangesAsync(cts);

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<Account>> GetAll(CancellationToken cts)
        {
            try
            {
                return await _context.Accounts.ToListAsync(cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Account> GetByEmail(string email, CancellationToken cts)
        {
            try
            {
                var entity = await _context.Accounts.FirstOrDefaultAsync(obj => obj.EMail == email, cts);

                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Account> GetById(int id, CancellationToken cts)
        {
            try
            {
                var entity = await _context.Accounts.FirstOrDefaultAsync(obj => obj.AccountId == id, cts);
                if (entity == null)
                {
                    throw new Exception("Account Id not found");
                }

                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> Update(Account data, CancellationToken cts)
        {
            try
            {
                var entity = await _context.Accounts.FirstOrDefaultAsync(obj => obj.AccountId == data.AccountId, cts);
                if (entity == null)
                {
                    throw new Exception("Account Id not found");
                }

                entity.EMail = data.EMail;
                entity.PasswordSalt = data.PasswordSalt;
                entity.PasswordHash = data.PasswordHash;
                entity.Locked = data.Locked;
                entity.FirstName = data.FirstName;
                entity.Surname = data.Surname;
                entity.Patronymic = data.Patronymic;

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
