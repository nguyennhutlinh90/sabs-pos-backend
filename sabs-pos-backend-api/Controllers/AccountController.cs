using CoreLib.PasswordHasher;

using Microsoft.AspNetCore.Mvc;

using sabs_pos_backend_api.Models;

using System;
using System.IO;
using System.Threading.Tasks;

namespace sabs_pos_backend_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/accounts")]
    public class AccountController : ActionResultBase
    {
        readonly IAppSettings _appSettings;
        readonly IPasswordHasher _passwordHasher;
        readonly IAccountService _srvAccount;
        readonly IEmployeeService _srvEmployee;
        readonly IMailService _srvMail;
        readonly IRoleService _srvRole;
        public AccountController(IAppSettings appSettings, IPasswordHasher passwordHasher, IAccountService srvAccount, IEmployeeService srvEmployee, IMailService srvMail, IRoleService srvRole)
        {
            _appSettings = appSettings;
            _passwordHasher = passwordHasher;
            _srvAccount = srvAccount;
            _srvEmployee = srvEmployee;
            _srvMail = srvMail;
            _srvRole = srvRole;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] AccountReq req)
        {
            return await ExecuteAsync(async () =>
            {
                if (req.IsNull() || req.name.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "Account name is required");

                if (req.email.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "Email is required");

                if (!req.email.IsEmail())
                    throw new ResponseException(ResponseCode.INVALID_VALUE, "Email is invalid");

                if (req.password.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "Password is required");

                var account = await _srvAccount.Get<accounts>(QF.EQ("email", req.email));
                if (!account.IsNull())
                    throw new ResponseException(ResponseCode.CONFLICTING_PARAMETERS, "Email is already in use");

                if (!req.phone_number.IsNull())
                {
                    account = await _srvAccount.Get<accounts>(QF.EQ("phone_number", req.phone_number));
                    if (!account.IsNull())
                        throw new ResponseException(ResponseCode.CONFLICTING_PARAMETERS, "Phone number is already in use");
                }

                await createOwner(req);
            });
        }

        async Task create(AccountReq req, string employee_id)
        {
            var employee = await _srvEmployee.Get<employees>(QF.EQ("uuid", employee_id));
            if (employee.IsNull())
                throw new ResponseException(ResponseCode.NOT_FOUND, $"Employee '{employee_id}' was not found");

            var newAccount = req.MapTo<accounts>();
            newAccount.uuid = Guid.NewGuid().ToString();
            newAccount.owner_id = employee.owner_id;
            newAccount.role_id = employee.role_id;
            newAccount.type = AccountType.OWNER_USER;
            newAccount.salt_key = _passwordHasher.CreateSalt();
            newAccount.password_hash = _passwordHasher.Hash(req.password, newAccount.salt_key);
            newAccount.status = GeneralStatus.ACTIVE;
            newAccount.created_at = newAccount.updated_at = DateTime.Now;

            await _srvAccount.Create(newAccount);
        }

        async Task createOwner(AccountReq req)
        {
            var role = await _srvRole.Get<roles>(QF.EQ("name", RoleName.OWNER));
            if (role.IsNull())
                throw new ResponseException(ResponseCode.NOT_FOUND, $"Role '{RoleName.OWNER}' was not found");

            var newAccount = req.MapTo<accounts>();
            newAccount.uuid = Guid.NewGuid().ToString();
            newAccount.role_id = role.id;
            newAccount.type = AccountType.OWNER;
            newAccount.salt_key = _passwordHasher.CreateSalt();
            newAccount.password_hash = _passwordHasher.Hash(req.password, newAccount.salt_key);
            newAccount.status = GeneralStatus.DEACTIVE;
            newAccount.created_at = DateTime.Now;

            await _srvAccount.CreateOwner(newAccount);

            var mailBody = MailTemplate.EmailConfirmation.Replace("[link]", Path.Combine(_appSettings.LinkSettings.EmailConfirmation, newAccount.uuid));
            await _srvMail.Send("Email Confirmation", mailBody, newAccount.email);
        }
    }
}
