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
    }
}
