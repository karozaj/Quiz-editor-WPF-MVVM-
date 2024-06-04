using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfApp4.Model.DAL.Repositories
{
    static class AnswerRepository
    {
        private const string GET_ALL = "SELECT * FROM answer";
        private const string GET_ANSWERS_TO_QUESTION = "SELECT * FROM answer WHERE questionid LIKE ";
        private const string ADD_ANSWER = "INSERT INTO answer (id,text,iscorrect,questionid) VALUES ";
        private const string GET_MAX_INDEX = "SELECT max(id) FROM answer";
        private const string DELETE_ANSWER = "DELETE FROM answer WHERE questionid=";
        private const string UPDATE_ANSWER = "UPDATE answer SET ";

        private static Answer ReadAnswer(MySqlDataReader reader)
        {
            int id = int.Parse(reader["id"].ToString());
            string text = reader["text"].ToString();
            bool iscorrect = false;
            string isCorrectString = reader["iscorrect"].ToString();
            if(isCorrectString=="t")
            {
                iscorrect = true;
            }
            int questionid = int.Parse(reader["questionid"].ToString());

            return (new Answer(id, text, iscorrect, questionid));
        }

        public static List<Answer> GetAll()
        {
            List<Answer> list = new List<Answer>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ALL, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(ReadAnswer(reader));
                }
                connection.Close();
            }
            return list;
        }

        public static List<Answer> GetAnswersToQuestion(int questionID)
        {
            List<Answer> list = new List<Answer>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ANSWERS_TO_QUESTION+questionID.ToString(), connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(ReadAnswer(reader));
                }
                connection.Close();
            }
            return list;
        }

        public static bool AddAnswer(Answer answer)
        {
            bool result = false;
            using (var connection=DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_ANSWER}{answer.ToInsert()}",connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }

        public static bool RemoveAnswer(int questionID)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DELETE_ANSWER}{questionID}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }

        public static bool ModifyAnswer(Answer answer)
        {
            bool result = false;
            string isCorrectString = "f";
            if(answer.IsCorrect==true)
            {
                isCorrectString = "t";
            }
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{UPDATE_ANSWER}text='{answer.Text}', iscorrect='{isCorrectString}' WHERE id={answer.ID}", connection);
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
                index= int.Parse(reader["max(id)"].ToString());
                connection.Close();
            }
            return index;
        }
    }
}
