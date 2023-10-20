using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

    public ObservableCollection<Question> GetPublicQuestions()
    {
        // Create a list of dummy multiple-choice questions
        var dummyQuestions = new ObservableCollection<Question>
        {
            new Question(
    text: "What is the scientific study of human movement and body mechanics?",
    choices: new List<string> { "A. Physiology", "B. Kinesiology", "C. Psychology", "D. Anthropology" },
    correctAnswer: "B. Kinesiology",
    correctChoiceIndex: 1,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "Which of the following is NOT a type of muscle contraction?",
    choices: new List<string> { "A. Isometric", "B. Concentric", "C. Eccentric", "D. Intrinsic" },
    correctAnswer: "D. Intrinsic",
    correctChoiceIndex: 3,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the term for the point at which a muscle is attached to a bone that does not move during a muscle contraction?",
    choices: new List<string> { "A. Origin", "B. Insertion", "C. Belly", "D. Tendon" },
    correctAnswer: "A. Origin",
    correctChoiceIndex: 0,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "Which component of fitness is related to the ability of the heart and lungs to supply oxygen to the muscles during exercise?",
    choices: new List<string> { "A. Strength", "B. Flexibility", "C. Cardiovascular endurance", "D. Power" },
    correctAnswer: "C. Cardiovascular endurance",
    correctChoiceIndex: 2,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the primary function of the biceps brachii muscle?",
    choices: new List<string> { "A. Flexion of the elbow", "B. Extension of the knee", "C. Abduction of the shoulder", "D. Plantarflexion of the ankle" },
    correctAnswer: "A. Flexion of the elbow",
    correctChoiceIndex: 0,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "In which joint of the human body does the rotation occur?",
    choices: new List<string> { "A. Hinge joint", "B. Ball-and-socket joint", "C. Pivot joint", "D. Gliding joint" },
    correctAnswer: "C. Pivot joint",
    correctChoiceIndex: 2,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the term for the body's ability to sense the position and movement of its body parts without visual or sensory feedback?",
    choices: new List<string> { "A. Proprioception", "B. Perception", "C. Reflex", "D. Kinesthesia" },
    correctAnswer: "A. Proprioception",
    correctChoiceIndex: 0,
    isPublic: true,
    userID: null,
    subjectID: 1
),
new Question(
    text: "Which of the following is not a component of the FITT principle used in exercise prescription?",
    choices: new List<string> { "A. Frequency", "B. Intensity", "C. Time", "D. Terrain" },
    correctAnswer: "D. Terrain",
    correctChoiceIndex: 3,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the term for the point in a muscle's range of motion at which the greatest force can be generated?",
    choices: new List<string> { "A. Endurance point", "B. Insertion point", "C. Agonist point", "D. Strength curve" },
    correctAnswer: "D. Strength curve",
    correctChoiceIndex: 3,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "Which of the following is a common method for measuring body composition in kinesiology and fitness assessments?",
    choices: new List<string> { "A. Blood pressure measurement", "B. Skinfold thickness measurement", "C. Heart rate monitoring", "D. Lung capacity test" },
    correctAnswer: "B. Skinfold thickness measurement",
    correctChoiceIndex: 1,
    isPublic: true,
    userID: null,
    subjectID: 1
),
new Question(
    text: "Which muscle is responsible for extending the knee joint?",
    choices: new List<string> { "A. Biceps brachii", "B. Quadriceps femoris", "C. Hamstring group", "D. Gastrocnemius" },
    correctAnswer: "B. Quadriceps femoris",
    correctChoiceIndex: 1,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the primary function of the triceps brachii muscle?",
    choices: new List<string> { "A. Flexion of the elbow", "B. Extension of the knee", "C. Abduction of the shoulder", "D. Plantarflexion of the ankle" },
    correctAnswer: "B. Extension of the knee",
    correctChoiceIndex: 1,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "Which type of muscle fiber is known for its endurance and is resistant to fatigue?",
    choices: new List<string> { "A. Type I (slow-twitch)", "B. Type IIa (fast-twitch oxidative)", "C. Type IIx (fast-twitch glycolytic)", "D. Type III (intermediate)" },
    correctAnswer: "A. Type I (slow-twitch",
    correctChoiceIndex: 0,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the term for the process of muscles getting shorter and thicker, resulting in joint movement?",
    choices: new List<string> { "A. Concentric contraction", "B. Eccentric contraction", "C. Isometric contraction", "D. Isokinetic contraction" },
    correctAnswer: "A. Concentric contraction",
    correctChoiceIndex: 0,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "Which type of joint allows movement in all planes, including rotation?",
    choices: new List<string> { "A. Hinge joint", "B. Ball-and-socket joint", "C. Pivot joint", "D. Saddle joint" },
    correctAnswer: "B. Ball-and-socket joint",
    correctChoiceIndex: 1,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the term for the bundle of muscle fibers within a muscle?",
    choices: new List<string> { "A. Sarcolemma", "B. Myofibril", "C. Fascicle", "D. Sarcomere" },
    correctAnswer: "C. Fascicle",
    correctChoiceIndex: 2,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "Which term describes the maximum amount of force a muscle or group of muscles can generate?",
    choices: new List<string> { "A. Power", "B. Strength", "C. Endurance", "D. Flexibility" },
    correctAnswer: "B. Strength",
    correctChoiceIndex: 1,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "In kinesiology, what does the acronym 'DOMS' stand for?",
    choices: new List<string> { "A. Delayed Onset Muscle Soreness", "B. Dynamic Overload and Muscle Strain", "C. Direct Observation of Muscle Strength", "D. Daily Occupational Movement Strategies" },
    correctAnswer: "A. Delayed Onset Muscle Soreness",
    correctChoiceIndex: 0,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What type of exercise involves the muscle contracting while it shortens?",
    choices: new List<string> { "A. Concentric exercise", "B. Eccentric exercise", "C. Isometric exercise", "D. Isokinetic exercise" },
    correctAnswer: "A. Concentric exercise",
    correctChoiceIndex: 0,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "Which term is used to describe a muscle that is responsible for producing a particular movement?",
    choices: new List<string> { "A. Antagonist", "B. Synergist", "C. Stabilizer", "D. Agonist" },
    correctAnswer: "D. Agonist",
    correctChoiceIndex: 3,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the term for the range of motion at a joint where no movement occurs?",
    choices: new List<string> { "A. Flexion", "B. Extension", "C. Neutral position", "D. Resting position" },
    correctAnswer: "C. Neutral position",
    correctChoiceIndex: 2,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "Which of the following is an example of an open kinetic chain exercise for the leg muscles?",
    choices: new List<string> { "A. Leg press", "B. Squats", "C. Lunges", "D. Leg curls" },
    correctAnswer: "A. Leg press",
    correctChoiceIndex: 0,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the term for the protective fluid-filled sac found in some joints to reduce friction between moving parts?",
    choices: new List<string> { "A. Ligament", "B. Tendon", "C. Bursa", "D. Synapse" },
    correctAnswer: "C. Bursa",
    correctChoiceIndex: 2,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "What is the term for the attachment point of a muscle that moves during a contraction?",
    choices: new List<string> { "A. Origin", "B. Insertion", "C. Belly", "D. Tendon" },
    correctAnswer: "B. Insertion",
    correctChoiceIndex: 1,
    isPublic: true,
    userID: null,
    subjectID: 1
),

new Question(
    text: "Which component of the nervous system controls voluntary muscle movements and is responsible for conscious actions?",
    choices: new List<string> { "A. Autonomic nervous system", "B. Central nervous system", "C. Peripheral nervous system", "D. Sympathetic nervous system" },
    correctAnswer: "B. Central nervous system",
    correctChoiceIndex: 1,
    isPublic: true,
    userID: null,
    subjectID: 1
),


        // Add more dummy questions here...
        };
        return dummyQuestions;

    }

    public List<Question> GetUserQuestions(int userId)
    {

        // Replace this with your actual logic to retrieve user-specific questions using Entity Framework.
        return dbContext.Questions.Where(q => q.UserID == userId).ToList();
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