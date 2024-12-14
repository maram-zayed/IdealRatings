namespace MyApplication.Application.Common.Models;

public class BaseResponse<T>
{
    public string? Message { get; set; }

    public T Data { get; set; }
}
