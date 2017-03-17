using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace CharactersService.ConsoleApp
{
    public static class CommunicationObjectExtender
    {
        /// <summary>
        /// Method to Close an ICommunicationObject, i.e. a WCF proxy object, safely.  Based on code from the MSDN article
        /// entitled "Avoiding Problems with the Using Statement" located at http://msdn.microsoft.com/en-us/library/aa355056.aspx.
        /// </summary>
        public static void CloseConnection(this System.ServiceModel.ICommunicationObject proxy)
        {

            // Can't Close if not Open.
            if (proxy.State != CommunicationState.Opened) { return; }

            // Try to Close gracefully, failing that Abort.  Always Dispose even though at time of writing (2/2011) Dispose
            // simply calls Close for a standard client proxy.  This can be overridden or may someday change.
            try { proxy.Close(); }
            catch (CommunicationException) { proxy.Abort(); }
            catch (TimeoutException) { proxy.Abort(); }
            catch (Exception)
            {
                proxy.Abort();
                throw;
            }
            finally { ((IDisposable)proxy).Dispose(); }
        }
    }
}