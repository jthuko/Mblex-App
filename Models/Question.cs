using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexApp.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<string> Choices { get; set; }
        public int CorrectChoiceIndex { get; set; }
        public string CorrectAnswer { get; internal set; }
        public bool IsAnswered { get; set; }
        public bool IsCorrectAnswer { get; set; }

        // Reference to the owning user
        public int UserId { get; set; }
        public UserModel User { get; set; }

        // Reference to the subject (assuming it's defined elsewhere)
        public int SubjectId { get; set; }
        public SubjectModel Subject { get; set; }

        // Indicates whether the question is public or private
        public bool IsPublic { get; set; }
        public Question(string text, List<string> choices, int correctChoiceIndex)
        {
            Text = text;
            Choices = choices;
            CorrectChoiceIndex = correctChoiceIndex;
            IsAnswered = false;
        }
    }
}
