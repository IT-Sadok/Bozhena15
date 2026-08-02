using SmartHouseManagment.AppCore.Models;

namespace SmartHouseManagment.AppCore;

public static class Constants
{
    public static class Errors
    {
        public static Error UserNotFound { get; } = new("UserNotFound", ErrorTypes.NotFound, "User not found.");
        public static Error UserRegisterFailed { get; } = new("UserRegisterFailed", ErrorTypes.Failed, "User registration failed.");
        public static Error InvalidPassword { get; } = new("InvalidPassword", ErrorTypes.Unauthorized, "Invalid password.");

        public static Error FailedSavingChanges { get; } = new("FailedSavingChanges", ErrorTypes.Conflict, "An error occurred while saving changes.");
    }

    public static class ValidationErrors
    {
        public static string RequiredField(string fieldName) => $"{fieldName} is required.";
    }
}
