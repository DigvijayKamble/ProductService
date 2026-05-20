namespace ProductService.Domain.Common; 
public abstract class AuditableEntity
{
    //public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    //public DateTime? UpdatedAt { get; protected set; } 
    public bool IsActive { get; protected set; }

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;

    public void MarkDeleted()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}

