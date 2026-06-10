namespace QLBaoDienTu.Dtos.Apis
{
    public class ApiErrorResponse<T> : ApiBaseResponse
    {
        public ApiErrorResponse() : base(false)
        {

        }

        public ApiErrorResponse(string message, int statusCode) :
            base(false, message, statusCode)
        {

        }
    }
}
