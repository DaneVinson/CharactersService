using CharactersService.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                //List<Task<Character>> tasks = new List<Task<Character>>();
                //for (int i = 0; i < 20; i++)
                //{
                //    tasks.Add(Task.Run(GetCharacter));
                //}
                //var results = Task.WhenAll(tasks).GetAwaiter().GetResult();

                var results = new List<Character>();
                for (int i = 0; i < 20; i++)
                {
                    results.Add(GetCharacter());
                }

                stopWatch.Stop();

                var messages = new List<string>();
                foreach(var machine in results.Select(c => c.Source).Distinct())
                {
                    messages.Add($"{machine}: {results.Count(c => c.Source == machine)}");
                }

                Console.WriteLine($"Elapsed time for {results.Count()} calls: {stopWatch.ElapsedMilliseconds} ms");
                messages.ForEach(m => Console.WriteLine(m));
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

                var response = proxy.GetCharacter("Bilbo");
                Console.WriteLine(response);
            }
            finally { if (channelFactory != null) { channelFactory.CloseConnection(); } }
        }

        private static Character GetCharacter()
        {
            ChannelFactory<ICharactersService> channelFactory = null;
            try
            {
                channelFactory = new ChannelFactory<ICharactersService>(ConfigurationManager.AppSettings["EndpointName"]);
                channelFactory.Credentials.UserName.UserName = "dane";
                channelFactory.Credentials.UserName.Password = "dane";
                var proxy = channelFactory.CreateChannel();

                var response = proxy.GetCharacter("Bilbo");
                return response;
            }
            finally { if (channelFactory != null) { channelFactory.CloseConnection(); } }
        }
    }
}
