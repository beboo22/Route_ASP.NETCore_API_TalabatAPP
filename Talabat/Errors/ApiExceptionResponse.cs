namespace Talabat.api.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }


        public ApiExceptionResponse(int Scode, string? _details = null, string? msg = null) : base(Scode, msg)
        {
            Details = _details;
        }
    }
}
