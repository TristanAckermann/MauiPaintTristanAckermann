namespace MauiPaintTristanAckermann.Services;

public class ValidationService
{
    public bool ValidateEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}