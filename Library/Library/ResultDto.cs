namespace Library;

public record ResultDto(bool IsError = false, IEnumerable<string>? Errors = null); 