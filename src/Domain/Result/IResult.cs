namespace Domain.Result;

public interface IResult<T>
{
    bool Succeeded { get; set; }
    string[]? Errors { get; set; }
    T Value { get; set; }
    
}