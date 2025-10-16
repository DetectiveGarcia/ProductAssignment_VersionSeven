namespace Infrastructure.Helpers;

public class Prompt
{
    public static string DisplayAndRead(string prompt)
    {
        Console.Write(prompt);
        var userInput = Console.ReadLine()!;
        return userInput;
    }

    public static string DisplayAndReadUpdate(string prompt, string oldValue)
    {
        Console.Write(prompt + $"(Old value '{oldValue}' - Press enter to keep the old value: )");
        var userInput = Console.ReadLine();
        string validUserInput = string.IsNullOrEmpty(userInput) ? oldValue : userInput;
        return validUserInput;
    }
}
