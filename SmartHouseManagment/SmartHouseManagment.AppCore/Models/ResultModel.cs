namespace SmartHouseManagment.AppCore.Models;

public record ResultModel<T>(T Data, bool IsError = false, IEnumerable<string>? Errors = null);