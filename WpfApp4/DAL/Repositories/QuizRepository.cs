using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp4.Model.DAL.Repositories
{
    internal class QuizRepository
    {
        private const string GET_ALL = "SELECT * FROM quiz";
        private const string GET_MAX_INDEX = "SELECT max(id) FROM quiz";
        private const string ADD_QUIZ = "INSERT INTO quiz (id, name) VALUES ";
        private const string DELETE_QUIZ = "DELETE FROM quiz WHERE id=";
        private static Quiz ReadQuiz(MySqlDataReader reader)
        {
            int id = int.Parse(reader["id"].ToString());
            string name = reader["name"].ToString();

            return (new Quiz(id, name));
        }

        public static List<Quiz> GetAll()
        {
            List<Quiz> list = new List<Quiz>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ALL, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(ReadQuiz(reader));
                }
                connection.Close();
            }
            return list;
        }

        public static int GetMaxIndex()
        {
            int index = 0;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_MAX_INDEX, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                index = int.Parse(reader["max(id)"].ToString());
                connection.Close();
            }
            return index;
        }

        public static bool AddQuiz(Quiz quiz)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_QUIZ}{quiz.ToInsert()}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }

        public static bool RemoveQuiz(Quiz quiz)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DELETE_QUIZ}{quiz.ID}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }
    }
}
