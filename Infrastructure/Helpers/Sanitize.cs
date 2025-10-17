namespace Infrastructure.Helpers;

public static class Sanitize
{
    public static string Input(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
        string trimmedInput = input.Trim();
        return trimmedInput;
    }
}
