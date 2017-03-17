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
            Characters = new List<Character>()
            {
                new Character() { LongName = "Bilbo Baggins", Name = "Bildo", Source = "Fiction by J. R. R. Tolkien" },
                new Character() { LongName = "Samwise Gamgee", Name = "Sam", Source = "Fiction by J. R. R. Tolkien" },
                new Character() { LongName = "Froto Baggins", Name = "Froto", Source = "Fiction by J. R. R. Tolkien" },
                new Character() { LongName = "Galdalf the Grey", Name = "Gandalf", Source = "Fiction by J. R. R. Tolkien" },
                new Character() { LongName = "Conan the Barbarrian", Name = "Conan", Source = "Fiction by Robert E. Howard" },
                new Character() { LongName = "Hurcules, Son of Zeus", Name = "Hurcules", Source = "Greek Mythology" },
                new Character() { LongName = "Darth Vader, Sith Lord", Name = "Darth Vader", Source = "Fiction by George Lucas" }
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
