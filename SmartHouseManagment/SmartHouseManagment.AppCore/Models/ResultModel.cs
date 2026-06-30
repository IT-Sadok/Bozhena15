namespace SmartHouseManagment.AppCore.Models;

public record Error(string Id, ErrorTypes Type, string Description);

public record ResultModel
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    protected ResultModel(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static ResultModel Success() => new(true, null);
    public static ResultModel Failure(Error error) => new(false, error ?? throw new ArgumentNullException(nameof(error)));

    public static implicit operator ResultModel(Error error) => Failure(error);
}

public record ResultModel<T> : ResultModel
{
    public T? Value { get; }

    private ResultModel(T value) : base(true, null) => Value = value;
    private ResultModel(Error error) : base(false, error) { }

    public static implicit operator ResultModel<T>(T value) => new(value);

    public static implicit operator ResultModel<T>(Error error) => new(error);
}