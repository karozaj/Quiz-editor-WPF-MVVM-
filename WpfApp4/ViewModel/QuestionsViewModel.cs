using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp4.Model;

namespace WpfApp4.ViewModel
{
    class QuestionsViewModel: ViewModelBase
    {
        private Model.Model model = null;
        private ObservableCollection<Question> questions = null;
        private ObservableCollection<Answer> answers = null;
        private ObservableCollection<Quiz> quizzes = null;

        private int selectedQuestionIndex = -1;
        private int selectedQuizIndex = -1;
        
        public QuestionsViewModel(Model.Model model)
        {
            this.model = model;
            questions = model.Questions;
            answers = model.Answers;
            quizzes = model.Quizzes;
        }

        #region Properties
        public ObservableCollection<Question> Questions
        {
            get { return questions; }
            set
            {
                questions = value;
                onPropertyChanged(nameof(Questions));
            }
        }

        public ObservableCollection<Answer> Answers
        {
            get { return answers; }
            set {
                answers = value;
                onPropertyChanged(nameof(Answers));
            }
        }

        public ObservableCollection<Quiz> Quizzes
        {
            get { return quizzes; }
            set
            {
                quizzes = value;
                onPropertyChanged(nameof(Quizzes));
            }
        }
        public int SelectedQuestionIndex
        {
            get { return selectedQuestionIndex; }
            set
            {
                selectedQuestionIndex = value;
                onPropertyChanged(nameof(SelectedQuestionIndex));
            }
        }

        public int SelectedQuizIndex
        {
            get { return selectedQuizIndex; }
            set
            {
                selectedQuizIndex = value;
                onPropertyChanged(nameof(SelectedQuizIndex));
            }
        }

        public Question SelectedQuestion { get; set; }
        public Quiz SelectedQuiz { get; set; }

        #endregion


        #region Commands

        private ICommand loadAnswers = null;
        public ICommand LoadAnswers
        {
            get
            {
                if(loadAnswers==null)
                {
                    loadAnswers = new RelayCommand(
                        arg =>
                        {
                            if (SelectedQuestion != null)
                                Answers = model.GetAnswersToQuestion(SelectedQuestion);

                        },
                        arg=>true);
                }
                return loadAnswers;
            }
        }

        private ICommand loadQuestions = null;
        public ICommand LoadQuestions
        {
            get
            {
                if(loadQuestions==null)
                {
                    loadQuestions = new RelayCommand(
                        arg =>
                        {
                            if (SelectedQuiz != null)
                                Questions = model.GetQuestionsInQuiz(SelectedQuiz);
                        },
                        arg => true);
                }
                return loadQuestions;
            }
        }

        private ICommand resetView = null;
        public ICommand ResetView
        {
            get
            {
                if (resetView == null)
                {
                    resetView = new RelayCommand(
                        arg =>
                        {
                           SelectedQuestion = null;
                            SelectedQuiz = null;
                            SelectedQuizIndex = -1;
                            SelectedQuestionIndex = -1;
                            Quizzes = model.Quizzes;
                            Questions = model.Questions;
                            Answers = model.Answers;


                        }, arg => true
                        );
                }
                return resetView;
            }
        }
        #endregion
    }
}
