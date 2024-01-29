namespace Domain.Services.Common;

public class OperationOutcome<T>
{
    public bool IsSucesseful { get; set; }
    public string ErrorMessage { get; set; }
    public T? Result { get; set; }
}

public class OperationOutcome
{
    public bool IsSucesseful { get; set; }
    public string ErrorMessage { get; set; }
}