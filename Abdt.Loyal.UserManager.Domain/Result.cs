namespace Abdt.Loyal.UserManager.Domain
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }

        public Result(bool isSuccess, T value, string error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty);
        public static Result<T> Failure(string error) => new Result<T>(false, default, error);
    }
}
