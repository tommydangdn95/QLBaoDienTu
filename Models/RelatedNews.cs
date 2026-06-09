namespace QLBaoDienTu.Models
{
    public class RelatedNews
    {
        public Guid Id { get; set; }
        public Guid MainNewsId { get; set; }
        public Guid RelatedNewsId { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
