using log4net;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens.Jwt;
using System.ServiceModel.Security;
using System.Threading;

namespace CharactersService.Wcf
{
    public class Validator : UserNamePasswordValidator
    {
        private readonly static JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly static ILog _log = LogManager.GetLogger(nameof(Validator));
        private readonly static TokenValidationParameters _tokenValidationParameters;

        static Validator()
        {
            try
            {
                var audience = ConfigurationManager.AppSettings["Audience"];
                var authority = ConfigurationManager.AppSettings["Authority"];
                var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                                                    $"{authority}.well-known/openid-configuration",
                                                    new OpenIdConnectConfigurationRetriever());
                var openIdConfig = configurationManager.GetConfigurationAsync(CancellationToken.None).GetAwaiter().GetResult();
                _tokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authority,
                    ValidAudiences = new[] { audience },
                    IssuerSigningKeys = openIdConfig.SigningKeys
                };
            }
            catch (Exception ex)
            {
                _log.Error("Exception in ctor", ex);
                throw;
            }
        }

        public override void Validate(string userName, string password)
        {
            var user = _jwtSecurityTokenHandler.ValidateToken(
                                                    password, 
                                                    _tokenValidationParameters, 
                                                    out var validatedToken);

            if (!user.Identity.IsAuthenticated)
            {
                throw new MessageSecurityException("Auth Fail!");
            }
        }
    }
}
