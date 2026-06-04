using QLBaoDienTu.Models._Users;

namespace QLBaoDienTu.Models
{
    public enum NewsStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubmittedByUserId { get; set; }
        public Guid? ApprovedByAdminId { get; set; }

        public NewsStatus Status { get; set; } // Pending, Approved, Rejected
        public int ViewCount { get; set; }
        public bool IsFeatured { get; set; }
        public bool AllowComments { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string RejectionReason { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<NewsImage> NewsImages { get; set; } = new List<NewsImage>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<RelatedNews> RelatedNewsAsMain { get; set; } = new List<RelatedNews>();
        public virtual ICollection<RelatedNews> RelatedNewsAsRelated { get; set; } = new List<RelatedNews>();

        public virtual AppUser SubmittedByUser { get; set; }      // Tác giả submit
        public virtual AppUser ApprovedByAdmin { get; set; }       // Admin duyệt

    }
}
