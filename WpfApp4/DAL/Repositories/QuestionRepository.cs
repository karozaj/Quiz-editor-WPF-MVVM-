using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Model.DAL.Repositories
{
    static class QuestionRepository
    {
        private const string GET_ALL = "SELECT * FROM question";
        private const string ADD_QUESTION = "INSERT INTO question (id,text) VALUES ";
        private const string GET_MAX_INDEX = "SELECT max(id) FROM question";
        private const string DELETE_QUESTION = "DELETE FROM question WHERE id=";
        private const string UPDATE_QUESTION = "UPDATE question SET ";

        private static Question ReadQuestion(MySqlDataReader reader)
        {
            int id = int.Parse(reader["id"].ToString());
            string text = reader["text"].ToString();

            return (new Question(id, text));
        }

        public static List<Question> GetAll()
        {
            List<Question> list = new List<Question>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ALL, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(ReadQuestion(reader));
                }
                connection.Close();
            }
            return list;
        }

        public static bool AddQuestion(Question question)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_QUESTION}{question.ToInsert()}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }

        public static bool RemoveQuestion(Question question)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DELETE_QUESTION}{question.ID}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }

        public static bool ModifyQuestion(Question question)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{UPDATE_QUESTION}text='{question.Text}' WHERE id={question.ID}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
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
    }
}
