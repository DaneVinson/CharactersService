using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace CharactersService.Wcf
{
    public class Validator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if ("dane".Equals(userName, StringComparison.OrdinalIgnoreCase) &&"dane".Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            else if ("1".Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("My Exception");
            }
            else if ("2".Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                throw new MessageSecurityException("My MessageSecurityException with InnerException", new Exception("My inner Exception"));
            }
            // Cases "3" and "4" yield identical results at client, i.e. MessageSecurityException with the FaultException as the InnerException.
            else if ("3".Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                throw new FaultException("My FaultException");
            }
            else if ("4".Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                throw new MessageSecurityException("My MessageSecurityException with InnerException", new FaultException("My FaultException", new FaultCode("MyFaultCode")));
            }
            else
            {
                throw new MessageSecurityException("My MessageSecurityException");
            }
        }
    }
}
