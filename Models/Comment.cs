namespace QLBaoDienTu.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid NewsId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Content { get; set; }
        public Guid? ParentCommentId { get; set; } // For nested comments

        public bool IsApproved { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        // Foreign Keys
        public virtual News News { get; set; }
        public virtual Comment ParentComment { get; set; }

        // Navigation - Nested comments
        public virtual ICollection<Comment> ChildComments { get; set; } = new List<Comment>();
    }
}
