using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Model;

namespace WpfApp4.Model.DAL.Repositories
{
     
    class QuizQuestionRepository
    {
        private const string GET_ALL = "SELECT * FROM question_list";
        private const string ADD_QUIZ_QUESTION = "INSERT INTO question_list (id_quiz, id_question) VALUES ";
        private const string DELETE_QUIZ_QUESTION = "DELETE FROM question_list WHERE ";


        private static QuizQuestion ReadQuizQuestion(MySqlDataReader reader)
        {
            int idQuiz = int.Parse(reader["id_quiz"].ToString());
            int idQuestion = int.Parse(reader["id_question"].ToString());

            return (new QuizQuestion(idQuiz, idQuestion));
        }

        public static List<QuizQuestion> GetAll()
        {
            List<QuizQuestion> list = new List<QuizQuestion>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(GET_ALL, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(ReadQuizQuestion(reader));
                }
                connection.Close();
            }
            return list;
        }

        public static bool AddQuizQuestion(QuizQuestion quizQuestion)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_QUIZ_QUESTION}{quizQuestion.ToInsert()}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }

        public static bool RemoveQuizQuestion(QuizQuestion quizQuestion)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DELETE_QUIZ_QUESTION}id_quiz={quizQuestion.QuizID} AND id_question={quizQuestion.QuestionID}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }

        public static bool RemoveQuizQuestionByQuizID(Quiz quiz)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DELETE_QUIZ_QUESTION}id_quiz={quiz.ID}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }

        public static bool RemoveQuizQuestionByQuestionID(Question question)
        {
            bool result = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{DELETE_QUIZ_QUESTION}id_question={question.ID}", connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                result = true;
                connection.Close();
            }
            return result;
        }
    }
}
