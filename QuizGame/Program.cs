// See https://aka.ms/new-console-template for more information
using QuizGame;

Console.WriteLine("Hello, World!");

var questions = new List<Question>(); //inicializo lista de questions
var answers = new List<Answer>(); //inicializando lista de answers
var scores = new Dictionary<string, int>();

SeedQuestionsAndOptions();

StartGame();

void StartGame()
{
    Console.WriteLine("Are you ready? We are starting now!");
    Console.WriteLine("Whats is your name?");
    
    var player = Console.ReadLine();

    Console.WriteLine($"Welcome {player} let's do this");

    foreach (var item in questions)
    {
        Console.WriteLine(item.QuestionsText);
        Console.WriteLine("Plase, enter 1, 2, 3 or 4 ");

        foreach (var option in item.Options)
        {
            Console.WriteLine($"{option.Id}. {option.Text}");
        }

        var answer = GetSelectecAnswer();
        AddAnswerToList(answer, item);
    }

    int score = GetScore();
    Console.WriteLine($"Nice try {player}! You answered well {score} questions...");

    UpdateScore(player, score);

    ShowScores();

    answers = new List<Answer>();
    Console.WriteLine("Do you want to play again?");
    Console.WriteLine("Enter yes to play again or any other key to stop...");

    var playAgain = Console.ReadLine();
    if (playAgain?.ToLower().Trim() == "yes")
        StartGame();
}

string GetSelectecAnswer()
{
    var answer = Console.ReadLine();
    if (answer != null && (answer == "1") || (answer == "2") || (answer == "3") || (answer == "4"))
        return answer;
    else
    {
        Console.WriteLine("That is a not a valid option, please try again...");
        answer = GetSelectecAnswer();
    }
    return answer;
}

void AddAnswerToList(string answer, Question question)
{
    answers.Add(new Answer
    {
        QuestionId = question.Id,
        SelectedOption = GetSelectedOption(answer, question)

    });
}

Option GetSelectedOption(string answer, Question question)
{
    var selectecOption = new Option();

    foreach (var item in question.Options)
    {
        if (item.Id == int.Parse(answer))
            selectecOption = item;
    }

    return selectecOption;
};

void SeedQuestionsAndOptions()
{
    questions.Add(new Question //agrego question a la clase Question
    {
        Id = 1,
        QuestionsText = "Whats is the biggest country on earth?",
        Options = new List<Option>() // inicializo la clase Option y le agrego la clase Options directamente habro {} no es necesario usar el add
        {
            new Option { Id = 1, Text = "Australia"},
            new Option { Id = 2, Text = "China"},
            new Option { Id = 3, Text = "Canada"},
            new Option { Id = 4, Text = "Russia", IsValid = true}
        }
    });

    questions.Add(new Question
    {
        Id = 2,
        QuestionsText = "Whats is the country with the greatest population?",
        Options = new List<Option>()
        {
            new Option { Id = 1, Text = "India"},
            new Option { Id = 2, Text = "China", IsValid = true},
            new Option { Id = 3, Text = "United State"},
            new Option { Id = 4, Text = "Indonesia"},
        }
    });

    questions.Add(new Question
    {
        Id= 3,
        QuestionsText = "Whats was the less corrupt country in the world in 2021",
        Options= new List<Option>()
        {
            new Option { Id = 1, Text = "Finland"},
            new Option { Id = 2, Text = "New Zeland"},
            new Option { Id = 3, Text = "Denmark", IsValid = true},
            new Option { Id = 4, Text = "Norway"},
        }
    });

    questions.Add(new Question { 
        Id = 4,
        QuestionsText = "Whats was the best country for quality of life in 2021?",
        Options = new List<Option>()
        {
            new Option { Id = 1, Text = "Norway", IsValid = true},
            new Option { Id = 2, Text = "Belgium"},
            new Option { Id = 3, Text = "Sweden"},
            new Option { Id = 4, Text = "Switzerland"},
        }
    });
}

int GetScore()
{
    int score = 0;

    foreach (var item in answers)
    {
        if(item.SelectedOption.IsValid)
            score++;
    }
    return score;
}

void UpdateScore(string player, int score)
{
    bool updated = false;
    foreach (var item in scores)
    {
        if(item.Key == player)
        {
            scores[item.Key] = score;
            updated = true;
        }
    }
        if(!updated)
            scores.Add(player, score);
}

void ShowScores()
{
    Console.WriteLine("Scores:");

    foreach(var item in scores)
    {
        Console.WriteLine($"{item.Key}, Score: {item.Value}");
    }
}