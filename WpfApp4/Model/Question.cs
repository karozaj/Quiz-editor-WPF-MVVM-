using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Model
{
    class Question
    {
        public int ID { get; private set; }
        public string Text { get; private set; }

        public Question(int iD, string text)
        {
            ID = iD;
            Text = text;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var question=obj as Question;
            if (question is null) return false;
            //if(ID!=question.ID) return false;
            if(Text!=question.Text) return false;
            return true;
        }

        public string ToInsert()
        {
            return $"({ID},'{Text}')";
        }

    }
}
