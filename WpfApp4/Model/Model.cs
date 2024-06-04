using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Navigation;
using WpfApp4.Model.DAL.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp4.Model
{
    class Model
    {
        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();
        public ObservableCollection<Answer> Answers { get; set; } = new ObservableCollection<Answer>();
        public ObservableCollection<Quiz> Quizzes { get; set; } = new ObservableCollection<Quiz>();
        public ObservableCollection<QuizQuestion> QuizQuestions { get; set; } = new ObservableCollection<QuizQuestion>();
        public int MaxQuizIndex { get; set; }
        public int MaxQuestionIndex { get; set; }
        public int MaxAnswerIndex { get; set; }



        public Model()
        {
            var questions = QuestionRepository.GetAll();
            foreach (var question in questions)
            {
                Questions.Add(question);
            }

            var answers = AnswerRepository.GetAll();
            foreach (var answer in answers)
            {
                Answers.Add(answer);
            }

            var quizzes = QuizRepository.GetAll();
            foreach(var quiz in quizzes)
            {
                Quizzes.Add(quiz);
            }

            var quizQuestions = QuizQuestionRepository.GetAll(); 
            foreach (var quizQuestion in quizQuestions)
            {
                QuizQuestions.Add(quizQuestion);
            }
        }

        private void _refreshCollections()
        {
            var questions = QuestionRepository.GetAll();
            Questions = new ObservableCollection<Question>();
            foreach (var question in questions)
            {
                if(!Questions.Contains(question))
                    Questions.Add(question);
            }

            var answers = AnswerRepository.GetAll();
            Answers = new ObservableCollection<Answer>();
            foreach (var answer in answers)
            {
                if(!Answers.Contains(answer))
                    Answers.Add(answer);
            }

            var quizzes = QuizRepository.GetAll();
            Quizzes = new ObservableCollection<Quiz>();
            foreach (var quiz in quizzes)
            {
                if(!Quizzes.Contains(quiz))
                    Quizzes.Add(quiz);
            }

            var quizQuestions = QuizQuestionRepository.GetAll();
            QuizQuestions = new ObservableCollection<QuizQuestion>();
            foreach (var quizQuestion in quizQuestions)
            {
                if(!QuizQuestions.Contains(quizQuestion))
                    QuizQuestions.Add(quizQuestion);
            }
        }

        public bool AddQuiz(Quiz quiz)
        {
            if (!Quizzes.Contains(quiz))
            {
                if (QuizRepository.AddQuiz(quiz))
                {
                    Quizzes.Add(quiz);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveQuiz(Quiz quiz)
        {
            if (Quizzes.Contains(quiz))
            {
                if (QuizRepository.RemoveQuiz(quiz))
                {
                    Quizzes.Remove(quiz);
                    return true;
                }
            }
            return false;
        }

        public bool DoesQuizExist(string name)
        {
            foreach (Quiz quiz in Quizzes)
            {
                if (quiz.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public int GetMaxQuizIndex()
        {
            if(Quizzes.Count>0)
                return QuizRepository.GetMaxIndex();
            else
                return 0;
        }

        public ObservableCollection<Question> GetQuestionsInQuiz(Quiz quiz)
        {
            var questions= new ObservableCollection<Question>();
            foreach(var quizQuestion in QuizQuestions)
            {
               if(quizQuestion.QuizID==quiz.ID)
                {
                    foreach (var question in Questions)
                    {
                        if (question.ID == quizQuestion.QuestionID)
                        {
                            questions.Add(question);
                        }
                    }
                }
            }
            return questions;
        }

        public ObservableCollection<Question> GetQuestionsNotInQuiz(Quiz quiz)
        {
            var questions = new ObservableCollection<Question>();
            var questionsInQuiz = GetQuestionsInQuiz(quiz);
            foreach(Question question in Questions)
            {
                if(!questionsInQuiz.Contains(question))
                {
                    questions.Add(question);
                }
            }
            return questions;
        }

        public bool AddQuizQuestion(QuizQuestion quizQuestion)
        {
            if (!QuizQuestions.Contains(quizQuestion))
            {
                if (QuizQuestionRepository.AddQuizQuestion(quizQuestion))
                {
                    QuizQuestions.Add(quizQuestion);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveQuizQuestion(QuizQuestion quizQuestion)
        {
            if (QuizQuestions.Contains(quizQuestion))
            {
                if (QuizQuestionRepository.RemoveQuizQuestion(quizQuestion))
                {
                    QuizQuestions.Remove(quizQuestion);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveQuizQuestionsForQuiz(Quiz quiz)
        {
            if (QuizQuestionRepository.RemoveQuizQuestionByQuizID(quiz))
            {
                for (int i = QuizQuestions.Count - 1; i >= 0; i--)
                {
                    if (QuizQuestions[i].QuizID == quiz.ID)
                    {
                        QuizQuestions.RemoveAt(i);
                    }
                }
                return true;
            }
            return false;
        }

        public bool RemoveQuizQuestionsForQuestion(Question question)
        {
            if (QuizQuestionRepository.RemoveQuizQuestionByQuestionID(question))
            {
                for (int i = QuizQuestions.Count - 1; i >= 0; i--)
                {
                    if (QuizQuestions[i].QuestionID == question.ID)
                    {
                        QuizQuestions.RemoveAt(i);
                    }
                }
                return true;
            }
            return false;
        }

        public bool AddQuestion(Question question)
        {
            if(!Questions.Contains(question))
            {
                //bool result= QuestionRepository.AddQuestion(question);
                //_refreshCollections();
                //return result;
                if(QuestionRepository.AddQuestion(question))
                {
                    Questions.Add(question);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveQuestion(Question question)
        {
            if(Questions.Contains(question))
            {
                if (QuestionRepository.RemoveQuestion(question))
                {
                    Questions.Remove(question);
                    return true;
                }
            }
            return false;
        }

        public bool ModifyQuestion(Question question)
        {
            if(QuestionRepository.ModifyQuestion(question))
            {
                for(int i=0; i<Questions.Count; i++)
                {
                    if (Questions[i].ID==question.ID)
                    {
                        Questions[i] = question;
                        //Questions.RemoveAt(i);
                        //Questions.Add(question);
                        //_refreshCollections();
                        break;
                    }
                }
                return true;
            }
            return false;
        }

        public int GetMaxQuestionIndex()
        {
            if(Questions.Count>0)
                return QuestionRepository.GetMaxIndex();
            else
                return 0;
        }

        public bool DoesQuestionExist(string text)
        {
            foreach(Question question in Questions)
            {
                if(question.Text==text)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddAnswer(Answer answer)
        {
            if(!Answers.Contains(answer))
            {
                //bool result= AnswerRepository.AddAnswer(answer);
                //_refreshCollections();
                //return result;
                if (AnswerRepository.AddAnswer(answer))
                {
                    Answers.Add(answer);
                    return true;
                }
            }
            return false;
        }
        
        public bool RemoveAnswer(int questionID)
        {
            if (AnswerRepository.RemoveAnswer(questionID))
            {
                for (int i =Answers.Count-1; i >= 0; i--)
                {
                    if (Answers[i].QuestionID == questionID) 
                    {
                        Answers.RemoveAt(i);
                    }
                }
                return true;
            }
            return false;
        }

        public bool ModifyAnswer(Answer answer)
        {
            if(AnswerRepository.ModifyAnswer(answer))
            {
                for(int i=0; i<Answers.Count; i++)
                {
                    if (Answers[i].ID==answer.ID)
                    {
                        Answers[i] = answer;
                        //Answers.RemoveAt(i);
                        //Answers.Add(answer);
                        //_refreshCollections();
                        break;
                    }
                }
                return true;
            }
            return false;

        }

        public ObservableCollection<Answer> GetAnswersToQuestion(Question question)
        {
            var answers = new ObservableCollection<Answer>();
            foreach (var answer in Answers)
            {
                if (question.ID == answer.QuestionID)
                {
                    answers.Add(answer);
                }
            }

            return answers;
        }


        public int GetMaxAnswerIndex()
        {
            if (Answers.Count > 0)
                return AnswerRepository.GetMaxIndex();
            else
                return 0;
        }


    }
}
