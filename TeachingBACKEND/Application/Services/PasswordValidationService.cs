using System.Text.RegularExpressions;
using static TeachingBACKEND.Application.Services.PasswordValidationService;

namespace TeachingBACKEND.Application.Services
{
    public class PasswordValidationService : IPasswordValidationService
    {
        public interface IPasswordValidationService
        {
            bool IsValid(string password, out string error);
        }

        public bool IsValid(string password, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(password))
            {
                error = "Password is required.";
                return false;
            }

            if (password.Length < 8)
            {
                error = "Password must be at least 8 characters long.";
                return false;
            }

            if (!password.Any(char.IsUpper))
            {
                error = "Password must contain at least one uppercase letter.";
                return false;
            }

            if (!password.Any(char.IsLower))
            {
                error = "Password must contain at least one lowercase letter.";
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                error = "Password must contain at least one digit.";
                return false;
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
            {
                error = "Password must contain at least one special character.";
                return false;
            }

            return true;
        }
    }
}

