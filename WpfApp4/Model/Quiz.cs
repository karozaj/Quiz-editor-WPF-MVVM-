using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp4.Model
{
    class Quiz
    {
        public int ID {  get; set; }
        public string Name { get; set; }

        public Quiz(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var quiz = obj as Quiz;
            if (quiz is null) return false;
            if (Name != quiz.Name) return false;
            return true;
        }


        public string ToInsert()
        {
            return $"({ID},'{Name}')";
        }
    }
}
