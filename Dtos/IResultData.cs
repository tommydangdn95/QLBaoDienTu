namespace QLBaoDienTu.Dtos
{
    public interface IResultData<T> : IResult
    {
        T Data { get; }
    }
}
