﻿using CharactersService.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace CharactersService.Wcf
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerCall,
        Namespace = "http://charactersservice.com")]
    public class WcfCharactersService : ICharactersService
    {
        static WcfCharactersService()
        {
            Service = new InMemoryService();
        }

        public WcfCharactersService()
        { }


        public Character CreateCharacter(Character character)
        {
            try
            {
                var newCharacter = Service.CreateCharacter(character);
                return newCharacter;
            }
            catch (Exception exception)
            {
                Log.Error("Exception in CreateCharacter", exception);
                throw;
            }
        }

        public bool DeleteCharacter(string name)
        {
            try
            {
                return Service.DeleteCharacter(name);
            }
            catch (Exception exception)
            {
                Log.Error("Exception in DeleteCharacter", exception);
                throw;
            }
        }

        public Character GetCharacter(string name)
        {
            try
            {
                return Service.GetCharacter(name);
            }
            catch (Exception exception)
            {
                Log.Error("Exception in GetCharacter", exception);
                throw;
            }
        }

        public Character[] GetCharacters()
        {
            try
            {
                return Service.GetCharacters();
            }
            catch (Exception exception)
            {
                Log.Error("Exception in GetCharacters", exception);
                throw;
            }
        }

        public Character UpdateCharacter(Character character)
        {
            try
            {
                return Service.UpdateCharacter(character);
            }
            catch (Exception exception)
            {
                Log.Error("Exception in UpdateCharacter", exception);
                throw;
            }
        }


        private readonly ILog Log = LogManager.GetLogger(nameof(WcfCharactersService));

        private static readonly ICharactersService Service;
    }
}
