using QLBaoDienTu.Models._Users;

namespace QLBaoDienTu.Models
{
    public enum NewsStatus
    {
        Darft = 0,
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }

    public class News : BaseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Slug { get; set; }
        public string? Summary { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }

        public NewsStatus Status { get; set; }
        public int ViewCount { get; set; }
        public bool IsFeatured { get; set; }
        public bool AllowComments { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string? RejectionReason { get; set; }
        public Guid SubmittedByUserId { get; set; }
        public Guid? ApprovedByAdminId { get; set; }

    }
}
