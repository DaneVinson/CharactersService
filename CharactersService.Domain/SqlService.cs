using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharactersService.Domain
{
    public class SqlService : ICharactersService
    {
        public Character CreateCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCharacter(string name)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand($"delete Things where Id = '{name}'", connection))
            {
                command.CommandType = CommandType.Text;
                var count = command.ExecuteNonQuery();
                connection.Close();
                return count > 0;
            }
        }

        public Character GetCharacter(string name)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (DataSet dataSet = new DataSet())
            using (SqlCommand command = new SqlCommand($"select * from Things where Id = '{name}'", connection))
            {
                command.CommandType = CommandType.Text;
                var adapter = new SqlDataAdapter(command);
                connection.Open();
                var count = adapter.Fill(dataSet);
                Character character = null;
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    var parts = row["Description"].ToString().Split('|');
                    character = new Character()
                    {
                        LongName = parts[0],
                        Name = row["Id"].ToString(),
                        Source = parts[1]
                    };
                    break;
                }
                connection.Close();
                return character;
            }
        }

        public Character[] GetCharacters()
        {
            List<Character> characters = new List<Character>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (DataSet dataSet = new DataSet())
            using (SqlCommand command = new SqlCommand("select * from Things", connection))
            {
                command.CommandType = CommandType.Text;
                var adapter = new SqlDataAdapter(command);
                connection.Open();
                var count = adapter.Fill(dataSet);
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    var parts = row["Description"].ToString().Split('|');
                    characters.Add(new Character()
                    {
                        LongName = parts[0],
                        Name = row["Id"].ToString(),
                        Source = parts[1]
                    });
                }
                connection.Close();
            }
            return characters.ToArray();
        }

        public Character UpdateCharacter(Character character)
        {
            throw new NotImplementedException();
        }


        private static readonly string ConnectionString = ConfigurationManager.AppSettings["SqlConnectionString"];
    }
}
