using CoreLib.PasswordHasher;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using sabs_pos_backend_api.Models;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sabs_pos_backend_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/auth")]
    public class AuthenticationController : ActionResultBase
    {
        readonly IAppSettings _appSettings;
        readonly IPasswordHasher _passwordHasher;
        readonly IAccountService _srvAccount;
        public AuthenticationController(IAppSettings appSettings, IPasswordHasher passwordHasher, IAccountService srvAccount)
        {
            _appSettings = appSettings;
            _passwordHasher = passwordHasher;
            _srvAccount = srvAccount;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginReq req)
        {
            return await ExecuteAsync(async () =>
            {
                if (req.IsNull() || req.email.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "Email is required");

                if (req.password.IsNull())
                    throw new ResponseException(ResponseCode.MISSING_REQUIRED_PARAMETER, "Password is required");

                var account = await _srvAccount.Get<AccountRes>(QF.EQ("email", req.email), include: QI.IC("owners", "uuid owner_uuid").Merge(QI.IC("roles", "uuid role_uuid")));
                if (account.IsNull())
                    throw new ResponseException(ResponseCode.NOT_FOUND, $"Email '{req.email}' was not found");

                if (!_passwordHasher.Verify(req.password, account.password_hash, account.salt_key))
                    throw new ResponseException(ResponseCode.INVALID_VALUE, "Password is invalid");

                if (account.status != GeneralStatus.ACTIVE)
                    throw new ResponseException(ResponseCode.UNAUTHORIZED, "Email is not activated");

                if (!account.owner_id.HasValue)
                {
                    account.owner_id = account.id;
                    account.owner_uuid = account.uuid;
                }

                var securityKey = new SymmetricSecurityKey(_appSettings.Configuration.JwtTokenKey.ToBytes());
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimType.LOGIN_ID, account.id?.ToString()),
                        new Claim(ClaimType.LOGIN_UUID, account.uuid),
                        new Claim(ClaimType.LOGIN_NAME, account.name),
                        new Claim(ClaimType.LOGIN_TYPE, LoginType.ACCOUNT),
                        new Claim(ClaimType.OWNER_ID, account.owner_id?.ToString()),
                        new Claim(ClaimType.OWNER_UUID, account.owner_uuid),
                        new Claim(ClaimType.ROLE_UUID, account.role_uuid),
                        new Claim(ClaimType.PASSWORD, account.password_hash)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new
                {
                    account.name,
                    account.email,
                    account.type,
                    account.status,
                    role_id = account.role_uuid,
                    token = tokenHandler.WriteToken(token),
                    expiration = token.ValidTo
                };
            });
        }
    }
}
