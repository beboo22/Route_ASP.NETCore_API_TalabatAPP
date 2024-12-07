namespace Talabat.api.Errors
{
    public class ApiBehaviorError : ApiResponse
    {
        public List<string>? Details { get; set; }


        public ApiBehaviorError(int Scode, List<string>? _details, string? msg = null) :base(Scode,msg)
        {
            Details = _details;
        }
    }
}
