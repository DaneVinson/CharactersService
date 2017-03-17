using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CharactersService.Domain
{
    [ServiceContract(Namespace = "http://charactersservice.com")]
    public interface ICharactersService
    {
        [OperationContract]
        Character CreateCharacter(Character character);

        [OperationContract]
        bool DeleteCharacter(string name);

        [OperationContract]
        Character[] GetCharacters();

        [OperationContract]
        Character GetCharacter(string name);

        [OperationContract]
        Character UpdateCharacter(Character character);
    }
}
