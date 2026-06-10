namespace QLBaoDienTu.Dtos
{
    public class BaseItemDto
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Guid DeletedBy { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
