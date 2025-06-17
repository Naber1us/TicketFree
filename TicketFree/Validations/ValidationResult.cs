
namespace TicketFree.Validations
{
    public record Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public Error? Error { get; }

        private Result(T value) { IsSuccess = true; Value = value; Error = null; }
        private Result(Error error) { IsSuccess = false; Error = error; Value = default; }

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(Error error) => new(error);
    }

    public record Error(string Code, string Message);
}
