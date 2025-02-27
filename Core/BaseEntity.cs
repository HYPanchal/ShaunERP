#nullable enable

namespace Core
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Audit Fields: Track who created and last modified the record.
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        // Entity Status: Represents the current state of the record (e.g., Active, Inactive).
        public string? Status { get; set; }
    }
}
