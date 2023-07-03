namespace LibraryManagement.Domain.Entities.Common
{
    public class BaseEntity
    {
        // Unique identifier for the entity
        public Guid Id { get; set; }

        // Date and time when the entity was created
        public virtual DateTime CreatedDate { get; set; }

        // Date and time when the entity was last updated
        public virtual DateTime UpdatedDate { get; set; }

        // Date and time when the entity was soft-deleted
        public virtual DateTime DeletedDate { get; set; }

        // Flag indicating whether the entity is deleted or not
        public bool IsDeleted { get; set; }

        // Flag indicating whether the entity is active or not
        public bool IsActive { get; set; } = true;
    }
}
