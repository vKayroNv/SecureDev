using CardStorageService.Core.Interfaces;
using CardStorageService.Core.Models;
using CardStorageService.Core.Utils;
using CardStorageService.Storage.Interfaces;

namespace CardStorageService.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountSessionRepository _sessionRepository;

        public AuthService(IAccountRepository accountRepository, IAccountSessionRepository sessionRepository)
        {
            _accountRepository = accountRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<string> Register(AccountDto account, CancellationToken cts)
        {
            try
            {
                var entity = await _accountRepository.GetByEmail(account.EMail, cts);
                if (entity != null)
                {
                    throw new Exception("Email already exist");
                }

                var passwordHash = PasswordUtils.CreatePasswordHash(account.Password);

                var accountId = await _accountRepository.Create(new()
                {
                    EMail = account.EMail,
                    FirstName = account.FirstName,
                    Surname = account.Surname,
                    Patronymic = account.Patronymic,
                    PasswordSalt = passwordHash.passwordSalt,
                    PasswordHash = passwordHash.passwordHash,
                    Locked = false
                }, cts);

                return await CreateSession(accountId, cts);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> Login(string email, string password, CancellationToken cts)
        {
            try
            {
                var account = await _accountRepository.GetByEmail(email, cts);

                if (!PasswordUtils.VerifyPassword(password, account.PasswordSalt, account.PasswordHash))
                {
                    throw new Exception("Wrong password");
                }

                var session = account.Sessions.FirstOrDefault(obj => !obj.IsClosed);

                if (session == null)
                {
                    return await CreateSession(account.AccountId, cts);
                }

                session.TimeLastRequest = DateTime.Now;
                await _sessionRepository.Update(session, cts);

                return session.SessionToken;
            }
            catch
            {
                throw;
            }
        }

        private async Task<string> CreateSession(int accountId, CancellationToken cts)
        {
            var token = TokenUtils.GenerateJwtToken(accountId);

            await _sessionRepository.Create(new()
            {
                AccountId = accountId,
                SessionToken = token.token,
                TimeCreated = token.created,
                TimeLastRequest = token.created,
                TimeClosed = token.closed
            }, cts);

            return token.token;
        }
    }
}
