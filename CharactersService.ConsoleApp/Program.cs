using CharactersService.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
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
                channelFactory = new ChannelFactory<ICharactersService>(ConfigurationManager.AppSettings["EndpointName"]);
                channelFactory.Credentials.UserName.UserName = "dane";
                channelFactory.Credentials.UserName.Password = "dane";
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
