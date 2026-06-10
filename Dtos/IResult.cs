namespace QLBaoDienTu.Dtos
{
    public interface IResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
    }
}
