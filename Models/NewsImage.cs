namespace QLBaoDienTu.Models
{
    public class NewsImage
    {
        public Guid Id { get; set; }
        public Guid NewsId { get; set; }
        public string ImageUrl { get; set; }
        public string AlternativeText { get; set; }
        public string Caption { get; set; }
        public bool IsThumbnail { get; set; } // Ảnh đại diện
        public int DisplayOrder { get; set; }
        public DateTime CreatedDate { get; set; }

        // Foreign Key
        public virtual News News { get; set; }
    }
}
