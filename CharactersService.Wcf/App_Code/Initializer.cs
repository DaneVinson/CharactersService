using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharactersService.Wcf.App_Code
{
    public class Initializer
    {
        public static void AppInitialize()
        {
            XmlConfigurator.Configure();
        }
    }
}
