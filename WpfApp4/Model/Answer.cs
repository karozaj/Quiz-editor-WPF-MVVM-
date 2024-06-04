using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Model
{
    class Answer
    {
        public int ID { get; private set; }
        public string Text { get; private set; }
        public bool IsCorrect { get; private set; }
        public int QuestionID { get; private set; }

        public Answer(int iD, string text, bool isCorrect, int questionID)
        {
            ID = iD;
            Text = text;
            IsCorrect = isCorrect;
            QuestionID = questionID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var answer=obj as Answer;
            if(answer is null) return false;
            //if(ID!= answer.ID) return false;
            if(Text != answer.Text) return false;
            if(IsCorrect != answer.IsCorrect) return false;
            if(QuestionID!= answer.QuestionID) return false;
            return true;
        }

        public string ToInsert()
        {
            string isCorrectString = "f";
            if(IsCorrect==true)
                isCorrectString="t";
            return $"({ID},'{Text}','{isCorrectString}',{QuestionID})";
        }
    }
}
