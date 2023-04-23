namespace DAL.Models;

public class ResponseModel<TData>
{
    public ResponseModel(string statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message;
       
    }

    public string StatusCode{ get; set; }
    public string? Message { get; set; }
    public TData? Data { get; set; }
}
