using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharactersService.Domain
{
    public class ProxyOfService : ICharactersService
    {
        public ProxyOfService()
        {
            Service = new SqlService();
        }
        public Character CreateCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCharacter(string name)
        {
            throw new NotImplementedException();
        }

        public Character GetCharacter(string name)
        {
            throw new NotImplementedException();
        }

        public Character[] GetCharacters()
        {
            return Service.GetCharacters();
        }

        public Character UpdateCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        private readonly ICharactersService Service;
    }
}
