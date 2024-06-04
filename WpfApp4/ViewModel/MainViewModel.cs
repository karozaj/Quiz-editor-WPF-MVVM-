using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Model;

namespace WpfApp4.ViewModel
{
    class MainViewModel
    {
        private WpfApp4.Model.Model model = new WpfApp4.Model.Model();
        public QuestionsViewModel QuestionsVM { get; set; }
        public AddQuestionViewModel AddQuestionVM{ get; set; }
        public AddQuizViewModel AddQuizVM { get; set; }

        public MainViewModel()
        {
            QuestionsVM = new QuestionsViewModel(model);
            AddQuestionVM = new AddQuestionViewModel(model);
            AddQuizVM = new AddQuizViewModel(model);
        }

    }
}
