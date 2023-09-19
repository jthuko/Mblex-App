using MblexApp.Context;
using MblexApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexApp.ApiService
{
    public class QuestionService
    {
        private readonly MyDbContext dbContext;

        public QuestionService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await dbContext.Questions.ToListAsync();
        }

        public async Task AddQuestionAsync(Question question)
        {
            dbContext.Questions.Add(question);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            var existingQuestion = await dbContext.Questions.FindAsync(question.Id);

            if (existingQuestion != null)
            {
                // Update the properties of the existing question.
                existingQuestion.Text = question.Text;
                existingQuestion.CorrectAnswer = question.CorrectAnswer;
                // Update other properties as needed.

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task PatchQuestionAsync(int questionId, Dictionary<string, object> changes)
        {
            var existingQuestion = await dbContext.Questions.FindAsync(questionId);

            if (existingQuestion != null)
            {
                // Apply partial updates to the existing question.
                foreach (var change in changes)
                {
                    switch (change.Key)
                    {
                        case "Text":
                            existingQuestion.Text = (string)change.Value;
                            break;
                        case "CorrectAnswer":
                            existingQuestion.CorrectAnswer = (string)change.Value;
                            break;
                            // Add cases for other properties as needed.
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var questionToDelete = await dbContext.Questions.FindAsync(questionId);

            if (questionToDelete != null)
            {
                dbContext.Questions.Remove(questionToDelete);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
