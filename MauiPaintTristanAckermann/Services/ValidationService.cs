namespace MauiPaintTristanAckermann.Services;

public class ValidationService
{
    public bool ValidateEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        // Einfache Regex für E-Mail Validierung
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}