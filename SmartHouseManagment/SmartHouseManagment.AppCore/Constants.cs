using SmartHouseManagment.AppCore.Models;

namespace SmartHouseManagment.AppCore;

public static class Constants
{
    public static class Errors
    {
        public static Error UserNotFound { get; } = new("UserNotFound", ErrorTypes.NotFound, "User not found.");
        public static Error UserRegisterFailed { get; } = new("UserRegisterFailed", ErrorTypes.Failed, "User registration failed.");
        public static Error InvalidPassword { get; } = new("InvalidPassword", ErrorTypes.Unauthorized, "Invalid password.");
    }
}
