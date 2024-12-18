namespace Application.Models
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; }

        private OperationResult(bool isSuccess, T data, string message, string errorMessage)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
            ErrorMessage = errorMessage ?? string.Empty;
        }

        public static OperationResult<T> Success(T data, string message = "Operation successful")
        {
            return new OperationResult<T>(true, data, message, null!);
        }

        public static OperationResult<T> Failure(string errorMessage, string message = "Operation failed")
        {
            return new OperationResult<T>(false, default!, message, errorMessage);
        }
    }
}