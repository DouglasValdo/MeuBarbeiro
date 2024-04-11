namespace Domain.Common.Service;

public class OperationOutcome<T>
{
    public bool IsSuccessfully { get; init; }
    public string? ErrorMessage { get; init; }
    public T? Result { get; init; }
}

public class OperationOutcome
{
    public bool IsSuccessfully { get; init; }
    public string? ErrorMessage { get; init; }
}