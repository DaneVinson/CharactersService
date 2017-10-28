using CharactersService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace CharactersService.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //log4net.Config.XmlConfigurator.Configure();

            try
            {
                CallWcfService();
            }
            catch (MessageSecurityException ex)
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} - {1}", ex.GetType(), ex.Message);
                Console.WriteLine(ex.StackTrace ?? String.Empty);
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("...");
                Console.ReadKey();
            }
        }

        private static void CallWcfService()
        {
            ChannelFactory<ICharactersService> channelFactory = null;
            try
            {
                //channelFactory = new ChannelFactory<IProcessorService>(ProcessorServiceConfigurationName, new EndpointAddress(CurrentServicePackJob.Endpoint));
                string endpoint = "http://charactersservice.azurewebsites.net/wcfcharactersservice.svc";

                channelFactory = new ChannelFactory<ICharactersService>("CharactersServiceEndpoint2", new EndpointAddress(endpoint));
                //channelFactory = new ChannelFactory<ICharactersService>("CharactersServiceEndpoint");
                channelFactory.Credentials.UserName.UserName = "dane";
                channelFactory.Credentials.UserName.Password = "4";
                var proxy = channelFactory.CreateChannel();

                // GetCharacters
                var response = proxy.GetCharacters();
                foreach (var character in response)
                {
                    Console.WriteLine(character);
                }
            }
            finally { if (channelFactory != null) { channelFactory.CloseConnection(); } }
        }
    }
}
