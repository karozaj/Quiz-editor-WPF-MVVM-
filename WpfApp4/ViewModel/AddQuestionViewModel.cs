using Org.BouncyCastle.Math.EC;
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
    class AddQuestionViewModel : ViewModelBase
    {
        private Model.Model model = null;
        private ObservableCollection<Question> questions = null;
        private ObservableCollection<Answer> answers = null;

        private int selectedQuestionIndex = -1;
        private int questionID=-1;
        private string questionText="", questionAnswer1Text="", questionAnswer2Text = "", questionAnswer3Text = "", questionAnswer4Text="";
        private bool isAnswer1Correct=false, isAnswer2Correct = false, isAnswer3Correct = false, isAnswer4Correct = false;

        public AddQuestionViewModel(Model.Model model)
        {
            this.model = model;
            questions = model.Questions;
            answers = model.Answers;
        }

        private void _refreshCollections()
        {
            Questions=model.Questions;
            Answers = model.Answers;
        }
        
        private bool _areStringsTooLong()
        {
            if (QuestionText.Length > 500) return true;
            if(QuestionAnswer1Text.Length > 500) return true;
            if (QuestionAnswer2Text.Length > 500) return true;
            if (QuestionAnswer3Text.Length > 500) return true;
            if (QuestionAnswer4Text.Length > 500) return true;
            return false;
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
            set
            {
                answers = value;
                onPropertyChanged(nameof(Answers));
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


        public Question SelectedQuestion { get; set; }

        public int QuestionID
        {
            get { return questionID; }
            set
            {
                questionID = value;
                onPropertyChanged(nameof(QuestionID));
            }
        }

        public string QuestionText
        {
            get { return questionText; }
            set
            {
                questionText = value;
                onPropertyChanged(nameof(QuestionText));
            }
        }

        public string QuestionAnswer1Text
        {
            get { return questionAnswer1Text; }
            set
            {
                questionAnswer1Text = value;
                onPropertyChanged(nameof(QuestionAnswer1Text));
            }
        }

        public string QuestionAnswer2Text
        {
            get { return questionAnswer2Text; }
            set
            {
                questionAnswer2Text = value;
                onPropertyChanged(nameof(QuestionAnswer2Text));
            }
        }

        public string QuestionAnswer3Text
        {
            get { return questionAnswer3Text; }
            set
            {
                questionAnswer3Text = value;
                onPropertyChanged(nameof(QuestionAnswer3Text));
            }
        }

        public string QuestionAnswer4Text
        {
            get { return questionAnswer4Text; }
            set
            {
                questionAnswer4Text = value;
                onPropertyChanged(nameof(QuestionAnswer4Text));
            }
        }

        public bool IsAnswer1Correct
        {
            get { return isAnswer1Correct; }
            set
            {
                isAnswer1Correct = value;
                onPropertyChanged(nameof(isAnswer1Correct));
            }
        }

        public bool IsAnswer2Correct
        {
            get { return isAnswer2Correct; }
            set
            {
                isAnswer2Correct = value;
                onPropertyChanged(nameof(isAnswer2Correct));
            }
        }

        public bool IsAnswer3Correct
        {
            get { return isAnswer3Correct; }
            set
            {
                isAnswer3Correct = value;
                onPropertyChanged(nameof(isAnswer3Correct));
            }
        }

        public bool IsAnswer4Correct
        {
            get { return isAnswer4Correct; }
            set
            {
                isAnswer4Correct = value;
                onPropertyChanged(nameof(isAnswer4Correct));
            }
        }
        #endregion




        #region Commands

        private ICommand loadAnswers = null;
        public ICommand LoadAnswers
        {
            get
            {
                if (loadAnswers == null)
                {
                    loadAnswers = new RelayCommand(
                        arg =>
                        {
                            if (SelectedQuestion != null)
                                Answers = model.GetAnswersToQuestion(SelectedQuestion);

                        },
                        arg => true);
                }
                return loadAnswers;
            }
        }

        private ICommand loadForm = null;
        public ICommand LoadForm
        {
            get
            {
                if (loadForm == null)
                    loadForm = new RelayCommand(
                        arg =>
                        {
                            if(SelectedQuestionIndex!=-1 && Answers.Count==4)
                            {
                                QuestionID = SelectedQuestion.ID;
                                QuestionText = SelectedQuestion.Text;
                                QuestionAnswer1Text = Answers[0].Text; QuestionAnswer2Text = Answers[1].Text; QuestionAnswer3Text = Answers[2].Text; QuestionAnswer4Text = Answers[3].Text;
                                IsAnswer1Correct = Answers[0].IsCorrect; IsAnswer2Correct = Answers[1].IsCorrect; IsAnswer3Correct = Answers[2].IsCorrect; IsAnswer4Correct = Answers[3].IsCorrect;
                            }
                            else
                            {
                                QuestionID = -1;
                                QuestionText = "";
                                IsAnswer1Correct = false; IsAnswer2Correct = false; IsAnswer3Correct = false; IsAnswer4Correct = false;
                                QuestionAnswer1Text = ""; QuestionAnswer2Text = ""; QuestionAnswer3Text = ""; QuestionAnswer4Text = "";
                            }
                        },
                        arg => true);
                return loadForm;
            }
        }

        private ICommand addQuestion = null;
        public ICommand AddQuestion
        {
            get
            {
                if(addQuestion == null)
                {
                    addQuestion = new RelayCommand(
                        arg =>
                        {
                            int newIndex = model.GetMaxQuestionIndex()+1;
                            if (model.AddQuestion(new Question(newIndex, QuestionText)))
                            {
                                int maxAnswerIndex = model.GetMaxAnswerIndex();
                                model.AddAnswer(new Answer(maxAnswerIndex + 1, QuestionAnswer1Text, IsAnswer1Correct, newIndex));
                                model.AddAnswer(new Answer(maxAnswerIndex + 2, QuestionAnswer2Text, IsAnswer2Correct, newIndex));
                                model.AddAnswer(new Answer(maxAnswerIndex + 3, QuestionAnswer3Text, IsAnswer3Correct, newIndex));
                                model.AddAnswer(new Answer(maxAnswerIndex + 4, QuestionAnswer4Text, IsAnswer4Correct, newIndex));
                                _refreshCollections();
                            }

                        },
                        arg => (QuestionText!="") && (QuestionAnswer1Text != "") && (QuestionAnswer2Text != "") && (QuestionAnswer3Text != "") && (QuestionAnswer4Text != "") &&
                         model.DoesQuestionExist(QuestionText) == false && !(IsAnswer1Correct==false && IsAnswer2Correct==false && IsAnswer3Correct == false && IsAnswer4Correct == false) && !(_areStringsTooLong()));
                }
                return(addQuestion);
            }
        }
        private ICommand removeQuestion = null;
        public ICommand RemoveQuestion 
        {
            get
            {
                if(removeQuestion==null)
                {
                    removeQuestion = new RelayCommand(
                        arg =>
                        {
                            if (model.RemoveQuizQuestionsForQuestion(SelectedQuestion))
                            {
                                if (model.RemoveAnswer(QuestionID))
                                    model.RemoveQuestion(SelectedQuestion);
                            }
                            _refreshCollections();
                        },
                        arg => (QuestionID != -1));
                }
                return removeQuestion;
            }
        }
        private ICommand modifyQuestion = null;
        public ICommand ModifyQuestion
        {
            get
            {
                if(modifyQuestion==null)
                {
                    modifyQuestion=new RelayCommand(
                        arg=>
                        {
                            model.ModifyAnswer(new Answer(Answers[0].ID, QuestionAnswer1Text, IsAnswer1Correct, QuestionID));
                            model.ModifyAnswer(new Answer(Answers[1].ID, QuestionAnswer2Text, IsAnswer2Correct, QuestionID));
                            model.ModifyAnswer(new Answer(Answers[2].ID, QuestionAnswer3Text, IsAnswer3Correct, QuestionID));
                            model.ModifyAnswer(new Answer(Answers[3].ID, QuestionAnswer4Text, IsAnswer4Correct, QuestionID));
                            model.ModifyQuestion(new Question(QuestionID, QuestionText));
                            _refreshCollections();
                        },
                        arg => (QuestionID != -1) && (QuestionText != "") && (QuestionAnswer1Text != "") && (QuestionAnswer2Text != "") && (QuestionAnswer3Text != "") && (QuestionAnswer4Text != "") &&
                        !(IsAnswer1Correct == false && IsAnswer2Correct == false && IsAnswer3Correct == false && IsAnswer4Correct == false) && !(_areStringsTooLong()) );
                }
                return modifyQuestion;
            }
        }
        #endregion

    }
}
