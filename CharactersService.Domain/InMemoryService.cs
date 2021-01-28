using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharactersService.Domain
{
    /// <summary>
    /// Simple implementation of <see cref="ICharactersService"/> which utiizes instanced methods
    /// to perform CRUD against that instance's list of characters.
    /// </summary>
    public class InMemoryService : ICharactersService
    {
        public InMemoryService()
        {
            var machine = Environment.MachineName;
            Characters = new List<Character>()
            {
                new Character() { Name = "Bilbo", Source = machine },
                new Character() { Name = "Sam", Source = machine },
                new Character() { Name = "Froto", Source = machine },
                new Character() { Name = "Gandalf", Source = machine },
                new Character() { Name = "Conan", Source = machine },
                new Character() { Name = "Hurcules", Source = machine },
                new Character() { Name = "Darth Vader", Source = machine }
            };
        }

        #region ICharactersService

        public Character CreateCharacter(Character character)
        {
            Characters.Add(character);
            return character;
        }

        public bool DeleteCharacter(string name)
        {
            var character = Characters.FirstOrDefault(c => c.Name == name);
            if (character == null) { return false; }

            Characters.Remove(character);
            return true;
        }

        public Character GetCharacter(string name)
        {
            return Characters.FirstOrDefault(c => c.Name == name);
        }

        public Character[] GetCharacters()
        {
            return Characters.ToArray();
        }

        public Character UpdateCharacter(Character character)
        {
            var existingCharacter = Characters.FirstOrDefault(c => c.Name == character.Name);
            if (existingCharacter == null) { return null; }

            existingCharacter.LongName = character.LongName;
            existingCharacter.Source = character.Source;
            return existingCharacter;
        }

        #endregion

        private readonly IList<Character> Characters;
    }
}
