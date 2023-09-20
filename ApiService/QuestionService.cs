﻿using System;
using System.Collections.Generic;
using System.Linq;
using MblexApp.Context;
using MblexApp.Models;
using Microsoft.EntityFrameworkCore;

public class QuestionService
{
    private readonly MyDbContext dbContext;

    public QuestionService(MyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<Question> GetPublicQuestions()
    {
        // Replace this with your actual logic to retrieve public questions using Entity Framework.
        return dbContext.Questions.Where(q => q.IsPublic).ToList();
    }

    public List<Question> GetUserQuestions(int userId)
    {
        // Replace this with your actual logic to retrieve user-specific questions using Entity Framework.
        return dbContext.Questions.Where(q => q.UserId == userId).ToList();
    }

    public void AddQuestion(Question question)
    {
        // Replace this with your actual logic to add a question using Entity Framework.
        dbContext.Questions.Add(question);
        dbContext.SaveChanges();
    }

    public void UpdateQuestion(Question question)
    {
        // Replace this with your actual logic to update a question using Entity Framework.
        dbContext.Questions.Update(question);
        dbContext.SaveChanges();
    }

    public void PatchQuestion(int questionId, Dictionary<string, object> changes)
    {
        // Replace this with your actual logic to partially update a question using Entity Framework.
        var existingQuestion = dbContext.Questions.FirstOrDefault(q => q.Id == questionId);
        if (existingQuestion != null)
        {
            foreach (var change in changes)
            {
                if (change.Key.Equals("Text", StringComparison.OrdinalIgnoreCase))
                {
                    existingQuestion.Text = change.Value.ToString();
                }
                else if (change.Key.Equals("IsPublic", StringComparison.OrdinalIgnoreCase))
                {
                    existingQuestion.IsPublic = (bool)change.Value;
                }
                // Add more properties to update as needed
            }
            dbContext.SaveChanges();
        }
    }

    public void DeleteQuestion(int questionId)
    {
        // Replace this with your actual logic to delete a question using Entity Framework.
        var questionToDelete = dbContext.Questions.FirstOrDefault(q => q.Id == questionId);
        if (questionToDelete != null)
        {
            dbContext.Questions.Remove(questionToDelete);
            dbContext.SaveChanges();
        }
    }
}