namespace Domain.Result;

public interface IResult
{
    bool Succeeded { get; set; }
    string[]? Errors { get; set; }
}
public interface IResult<T> : IResult {
    T Value { get; set; }
    
}