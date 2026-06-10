namespace QLBaoDienTu.Dtos.Apis
{
    public class ApiCommandResponse : ApiBaseResponse
    {
        public ApiCommandResponse(string message, int statusCode)
            : base(true, message, statusCode)
        {

        }
    }
}
