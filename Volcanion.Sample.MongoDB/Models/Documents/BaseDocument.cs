using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Volcanion.Sample.MongoDB.Models.Documents;

public class BaseDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid? Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }
}
