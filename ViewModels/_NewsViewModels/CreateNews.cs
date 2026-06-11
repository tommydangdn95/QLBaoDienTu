using QLBaoDienTu.Models;

namespace QLBaoDienTu.ViewModels._NewsViewModels
{
    public class CreateNews
    {
        public string Title { get; set; }
        public string? Slug { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }

        public int Status { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? RejectionReason { get; set; }
    }
}
