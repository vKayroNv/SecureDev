using CardStorageService.Storage.Interfaces;
using CardStorageService.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace CardStorageService.Storage.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DatabaseContext _context;

        public ClientRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Client data, CancellationToken cts)
        {
            try
            {
                await _context.Clients.AddAsync(data, cts);
                await _context.SaveChangesAsync(cts);

                return data.ClientId;
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
                var entity = await _context.Clients.FirstOrDefaultAsync(obj => obj.ClientId == id, cts);
                if (entity == null)
                {
                    throw new Exception("Client Id not found");
                }

                _context.Clients.Remove(entity);

                await _context.SaveChangesAsync(cts);

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IReadOnlyList<Client>> GetAll(CancellationToken cts)
        {
            try
            {
                return await _context.Clients.ToListAsync(cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Client> GetById(int id, CancellationToken cts)
        {
            try
            {
                var entity = await _context.Clients.FirstOrDefaultAsync(obj => obj.ClientId == id, cts);
                if (entity == null)
                {
                    throw new Exception("Client Id not found");
                }

                return entity;
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
                var entity = await _context.Clients.FirstOrDefaultAsync(obj => obj.ClientId == data.ClientId, cts);
                if (entity == null)
                {
                    throw new Exception("Client Id not found");
                }

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
