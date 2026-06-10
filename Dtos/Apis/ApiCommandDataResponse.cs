namespace QLBaoDienTu.Dtos.Apis
{
    public class ApiCommandDataResponse<T> : ApiBaseResponse
    {
        public T Data { get; private set; }
        public ApiCommandDataResponse(string message, int statusCode, T data)
            : base(true, message, statusCode)
        {
            this.Data = data;
        }
    }
}
