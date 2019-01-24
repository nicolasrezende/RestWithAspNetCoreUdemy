using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using RestWithAspNetCoreUdemy.Data.VO;
using RestWithAspNetCoreUdemy.Repository;
using RestWithAspNetCoreUdemy.Security.Configuration;

namespace RestWithAspNetCoreUdemy.Bussines.Implementattions
{
    public class LoginBussinesImp : ILoginBussines
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly SigningConfiguration _signingConfiguration;

        public LoginBussinesImp(IUserRepository userRepository, 
            TokenConfiguration tokenConfiguration, 
            SigningConfiguration signingConfiguration)
        {
            _userRepository = userRepository;
            _tokenConfiguration = tokenConfiguration;
            _signingConfiguration = signingConfiguration;
        }

        public object FindByLogin(UserVO user)
        {
            var credendialsIsValid = false;
            if (user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var userBase = _userRepository.FindByLogin(user.Login);
                credendialsIsValid = (userBase != null && user.Login.Equals(userBase.Login)
                    && user.AccessKey.Equals(userBase.AccessKey));
            }

            if (!credendialsIsValid)
                return ExceptionObject();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                new GenericIdentity(user.Login, "Login"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                }
            );

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

            //JwtSecurityTokenHandler = classe responsavel por criar o Token
            var handler = new JwtSecurityTokenHandler();
            string token = CreateToken(claimsIdentity, createDate, expirationDate, handler);

            return SuccessObject(createDate, expirationDate, token);
        }

        private string CreateToken(ClaimsIdentity claimsIdentity, DateTime createDate, DateTime expirationDate, 
            JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = claimsIdentity,
                NotBefore = createDate, //nbf = define a data e hora em que o Token não será aceito, ou seja, o token só será aceito se a data e hora atual for posterior ao nbf informado
                Expires = expirationDate //exp = data de expiração do Token
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to authenticate"
            };
        }
    }
}
