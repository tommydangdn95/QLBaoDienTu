namespace QLBaoDienTu.Models
{
    public abstract class BaseModel
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public Guid DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
