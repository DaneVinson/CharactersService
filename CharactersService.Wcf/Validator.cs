using System;
using System.IdentityModel.Selectors;
using System.ServiceModel.Security;

namespace CharactersService.Wcf
{
    public class Validator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            bool flag = !"dane".Equals(userName, StringComparison.OrdinalIgnoreCase) || !"dane".Equals(password, StringComparison.OrdinalIgnoreCase);
            if (flag)
            {
                throw new MessageSecurityException("Auth Fail!");
            }
        }
    }
}
