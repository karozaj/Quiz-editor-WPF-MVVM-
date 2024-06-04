using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp4.Model;

namespace WpfApp4.ViewModel
{
    class AddQuizViewModel:ViewModelBase
    {
        private Model.Model model;
        private ObservableCollection<Quiz> quizzes = null;
        private ObservableCollection<Question> questionsInQuiz = null;
        private ObservableCollection<Question> questionsNotInQuiz = null;

        private int selectedQuizIndex = -1;
        private int selectedQuestionInQuizIndex = -1;
        private int selectedQuestionNotInQuizIndex = -1;
        private Quiz selectedQuiz = null;
        private Question selectedQuestionInQuiz = null;
        private Question selectedQuestionNotInQuiz = null;
        private string quizName = "";

        public AddQuizViewModel(Model.Model model)
        {
            this.model = model;
            quizzes = model.Quizzes;
            questionsNotInQuiz = model.Questions;
        }

        #region Properties
        public ObservableCollection<Quiz> Quizzes
        {
            get { return quizzes; }
            set
            {
                quizzes = value;
                onPropertyChanged(nameof(Quizzes));
            }
        }

        public ObservableCollection<Question> QuestionsInQuiz
        {
            get { return questionsInQuiz; }
            set
            {
                questionsInQuiz = value;
                onPropertyChanged(nameof(questionsInQuiz));
            }
        }

        public ObservableCollection<Question> QuestionsNotInQuiz
        {
            get { return questionsNotInQuiz; }
            set
            {
                questionsNotInQuiz = value;
                onPropertyChanged(nameof(questionsNotInQuiz));
            }
        }

       public Quiz SelectedQuiz
        {
            get { return selectedQuiz; }
            set
            {
                selectedQuiz = value;
                onPropertyChanged(nameof(selectedQuiz));
            }
        }

        public int SelectedQuizIndex
        {
            get { return selectedQuizIndex;  }
            set
            {
                selectedQuizIndex = value;
                onPropertyChanged(nameof(selectedQuizIndex));
            }
        }

        public Question SelectedQuestionInQuiz
        {
            get { return selectedQuestionInQuiz; }
            set
            {
                selectedQuestionInQuiz = value;
                onPropertyChanged(nameof(selectedQuestionInQuiz));
            }
        }

        public int SelectedQuestionInQuizIndex
        {
            get { return selectedQuestionInQuizIndex; }
            set
            {
                selectedQuestionInQuizIndex = value;
                onPropertyChanged(nameof(selectedQuestionInQuizIndex));
            }
        }


        public Question SelectedQuestionNotInQuiz
        {
            get { return selectedQuestionNotInQuiz; }
            set
            {
                selectedQuestionNotInQuiz = value;
                onPropertyChanged(nameof(selectedQuestionNotInQuiz));
            }
        }

        public int SelectedQuestionNotInQuizIndex
        {
            get { return selectedQuestionNotInQuizIndex; }
            set
            {
                selectedQuestionNotInQuizIndex = value;
                onPropertyChanged(nameof(selectedQuestionNotInQuizIndex));
            }
        }

        public string QuizName
        {
            get { return quizName; }
            set
            {
                quizName = value;
                onPropertyChanged(nameof(quizName));
            }
        }
        #endregion

        #region Methods


        private void _refreshForm()
        {
            QuizName = "";
            SelectedQuestionInQuiz = null;
            SelectedQuestionNotInQuiz = null;
            SelectedQuiz = null;
            QuestionsInQuiz = null;
            QuestionsNotInQuiz = model.Questions;
        }

        private ICommand loadQuestions = null;
        public ICommand LoadQuestions
        {
            get
            {
                if (loadQuestions == null)
                {
                    loadQuestions = new RelayCommand(
                        arg =>
                        {
                            if (SelectedQuiz != null)
                            {
                                QuestionsInQuiz = model.GetQuestionsInQuiz(SelectedQuiz);
                                QuestionsNotInQuiz = model.GetQuestionsNotInQuiz(SelectedQuiz);
                                //MessageBox.Show(QuizName);
                                //MessageBox.Show(SelectedQuestionInQuiz.Text.ToString());                               
                                //MessageBox.Show(SelectedQuestionNotInQuiz.Text.ToString());
                            }
                        },
                        arg => true);
                }
                return loadQuestions;
            }
        }

        private ICommand addQuiz = null;
        public ICommand AddQuiz
        {
            get
            {
                if (addQuiz == null)
                {
                    addQuiz = new RelayCommand(
                        arg =>
                        {
                            int newIndex = model.GetMaxQuizIndex() + 1;
                            model.AddQuiz(new Quiz(newIndex, QuizName));
                            _refreshForm();

                        },
                        arg => QuizName!="" && !model.DoesQuizExist(QuizName) && QuizName.Length<=50);
                }
                return addQuiz;
            }
        }

        private ICommand removeQuiz = null;
        public ICommand RemoveQuiz
        {
            get
            {
                if (removeQuiz == null)
                {
                    removeQuiz = new RelayCommand(
                        arg =>
                        {
                            if(!(SelectedQuiz is null))
                            {
                                if(model.RemoveQuizQuestionsForQuiz(SelectedQuiz))
                                    model.RemoveQuiz(SelectedQuiz);
                                _refreshForm();
                            }
                          
                        },
                        arg => !(SelectedQuiz is null));
                }
                return removeQuiz;
            }
        }

        private ICommand addQuestionToQuiz = null;
        public ICommand AddQuestionToQuiz
        {
            get
            {
                if (addQuestionToQuiz == null)
                {
                    addQuestionToQuiz = new RelayCommand(
                        arg =>
                        {
                            if (!(SelectedQuiz is null) && !(SelectedQuestionNotInQuiz is null))
                            {
                                model.AddQuizQuestion(new QuizQuestion(SelectedQuiz.ID, SelectedQuestionNotInQuiz.ID));
                            }
                            SelectedQuestionInQuiz = null;
                            SelectedQuestionNotInQuiz = null;
                            QuestionsInQuiz = model.GetQuestionsInQuiz(SelectedQuiz);
                            QuestionsNotInQuiz = model.GetQuestionsNotInQuiz(SelectedQuiz);
                        },
                        arg => !(SelectedQuiz is null) && !(SelectedQuestionNotInQuiz is null));
                }
                return addQuestionToQuiz;
            }
        }

        private ICommand removeQuestionFromQuiz = null;
        public ICommand RemoveQuestionFromQuiz
        {
            get
            {
                if (removeQuestionFromQuiz == null)
                {
                    removeQuestionFromQuiz = new RelayCommand(
                        arg =>
                        {
                            if (!(SelectedQuiz is null) && !(SelectedQuestionInQuiz is null))
                            {
                                model.RemoveQuizQuestion(new QuizQuestion(SelectedQuiz.ID, SelectedQuestionInQuiz.ID));
                            }
                            SelectedQuestionInQuiz = null;
                            SelectedQuestionNotInQuiz = null;
                            QuestionsInQuiz = model.GetQuestionsInQuiz(SelectedQuiz);
                            QuestionsNotInQuiz = model.GetQuestionsNotInQuiz(SelectedQuiz);
                        },
                        arg => !(SelectedQuiz is null) && !(SelectedQuestionInQuiz is null));
                }
                return removeQuestionFromQuiz;
            }
        }

        private ICommand test = null;
        public ICommand Test
        {
            get
            {
                if (test == null)
                {
                    test = new RelayCommand(
                        arg =>
                        {
                            MessageBox.Show("Abc");
                        },
                        arg => true);
                }
                return test;
            }
        }

        #endregion
    }
}
