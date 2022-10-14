using CoreLib.PasswordHasher;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using sabs_pos_backend_api.Models;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace sabs_pos_backend_api
{
    public class DatabaseHostedService : IHostedService
    {
        private readonly ILogger<DatabaseHostedService> _logger;
        private readonly IAppSettings _appSettings;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountService _srvAccount;
        private readonly IRoleService _srvRole;
        public DatabaseHostedService(ILogger<DatabaseHostedService> logger, IAppSettings appSettings, IPasswordHasher passwordHasher, IAccountService srvAccount, IRoleService srvRole)
            => (_logger, _appSettings, _passwordHasher, _srvAccount, _srvRole) = (logger, appSettings, passwordHasher, srvAccount, srvRole);

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Seed data started on {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            try
            {
                //await initRole();
                //await initAccount();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Seed data failure: " + ex.Message);
            }

            _logger.LogInformation($"Seed data finished on {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        async Task initAccount()
        {
            var account = await _srvAccount.Get<accounts>(QF.EQ("email", _appSettings.SeedData.AccountEmail));
            if (account.IsNull())
            {
                var role = await _srvRole.Get<roles>(QF.EQ("name", RoleName.OWNER));
                if (role.IsNull())
                    throw new ResponseException(ResponseCode.NOT_FOUND, $"Role '{RoleName.OWNER}' was not found");

                var saltKey = _passwordHasher.CreateSalt();
                var newAccount = new accounts
                {
                    uuid = Guid.NewGuid().ToString(),
                    name = _appSettings.SeedData.AccountName,
                    email = _appSettings.SeedData.AccountEmail,
                    role_id = role.id,
                    type = AccountType.OWNER,
                    salt_key = saltKey,
                    password_hash = _passwordHasher.Hash(_appSettings.SeedData.AccountPass, saltKey),
                    status = GeneralStatus.ACTIVE,
                    created_at = DateTime.Now
                };

                await _srvAccount.CreateOwner(newAccount);
            }
        }
        async Task initRole()
        {
            var newRoles = new List<roles> {
                new roles { uuid = Guid.NewGuid().ToString(), name = RoleName.OWNER, type = RoleName.OWNER, allow_pos = true, allow_back_office = true },
                new roles { uuid = Guid.NewGuid().ToString(), name = RoleName.ADMIN, type = RoleName.ADMIN, allow_pos = false, allow_back_office = false },
                new roles { uuid = Guid.NewGuid().ToString(), name = RoleName.MANAGER, type = RoleName.MANAGER, allow_pos = false, allow_back_office = false },
                new roles { uuid = Guid.NewGuid().ToString(), name = RoleName.CASHIER, type = RoleName.CASHIER, allow_pos = false, allow_back_office = false },
                new roles { uuid = Guid.NewGuid().ToString(), name = RoleName.OTHER, type = RoleName.OTHER, allow_pos = false, allow_back_office = false }
            };
            foreach (var newRole in newRoles)
            {
                var role = await _srvRole.Get<roles>(QF.EQ("name", newRole.name));
                if (role.IsNull())
                    await _srvRole.Create(newRole);
            }
        }
    }
}
