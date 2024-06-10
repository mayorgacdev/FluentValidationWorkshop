namespace Workshop.Api;

/// <summary>
/// General data associated with different entities in the database.
/// </summary>
public abstract record Entity
{
    /// <summary>
    /// Creation date of the record.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Date when the record is being modified.
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// Date when the record is being deleted.
    /// </summary>
    public DateTime? DeletedDate { get; set; }

    /// <summary>
    /// Indicates whether the record has been edited.
    /// </summary>
    public bool IsEdit { get; set; }

    /// <summary>
    /// Indicates whether the record has been deleted.
    /// </summary>
    public bool IsDelete { get; set; }
}

// Path: src/Workshop.Api/Entities/Entity.cs