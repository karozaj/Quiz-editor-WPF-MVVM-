using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp4.Model
{
    //klasa opisujaca polaczenie quizu z pytaniami
    class QuizQuestion
    {
        public int QuizID { get; set; }
        public int QuestionID { get; set; }

        public QuizQuestion(int quizID, int questionID)
        {
            QuizID = quizID;
            QuestionID = questionID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var quizQuestion = obj as QuizQuestion;
            if (quizQuestion is null) return false;
            if (QuizID != quizQuestion.QuizID) return false;
            if (QuestionID != quizQuestion.QuestionID) return false;
            return true;
        }


        public string ToInsert()
        {
            return $"({QuizID},{QuestionID})";
        }
    }
}
