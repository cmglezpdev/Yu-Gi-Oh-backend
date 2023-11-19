using backend.Common.Enums;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace backend.Infrastructure.Common;

public class McResult<T>
{
    public bool IsSuccess { get; set; }
    public bool IsFailure => !IsSuccess;
    public T Result { get; set; }
    public string ErrorMessage { get; set; }
    public ErrorCodes ErrorCode { get; set; }
    
    public static McResult<T> Succeed(T result)
    {
        return new McResult<T>()
        {
            IsSuccess = true,
            Result = result
        };
    }
    
    public static McResult<T> Failure(string errorMessage, ErrorCodes code = ErrorCodes.OperationError)
    {
        return new McResult<T>()
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            ErrorCode = code
        };
    }
}