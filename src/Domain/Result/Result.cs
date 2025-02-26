namespace Domain.Result;

public class Result<T> : IResult<T> {
    public bool Succeeded { get; set; }
    public string[]? Errors { get; set; }
    public T Value { get; set; }
    public static IResult<T> Failed(params string[] errors) {
        var result = new Result<T>{Succeeded = false, Errors = errors};
        return result;
    }

    public static IResult<T> Success(T value) {
        var result =  new Result<T>{Succeeded = true, Value = value};
        return result;
    }
}