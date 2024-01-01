using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MblexApp.Models
{
    //Main public questions list
    public class FlashCards
    {
        public int FlashcardID { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public bool IsPublic { get; set; }
        public int? UserID { get; set; }
        public int SubjectID { get; set; }        
    }
}
