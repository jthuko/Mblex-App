using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexApp.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>(); // Initialize the Questions list
    }
}
