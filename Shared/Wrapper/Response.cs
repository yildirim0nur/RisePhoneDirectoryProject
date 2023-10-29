namespace Shared.Wrapper;

public class BaseApiResponse<T>
{
    public BaseApiResponse(T data, string message)
    {
        if (data != null) Data = data;
        Message = message;
    }

    public BaseApiResponse(List<T> data, string message)
    {
        if (data.Any())
            Data = data;
        Message = message;
    }

    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public object? Data { get; set; }
}


public class SingleDataResponse<T> : BaseApiResponse<T>
{
    public SingleDataResponse(T data, string message = "") : base(data, message)
    {
    }
}
public class ListDataResponse<T> : BaseApiResponse<T>
{
    public ListDataResponse(List<T> data, string message = "") : base(data, message)
    {
    }
}