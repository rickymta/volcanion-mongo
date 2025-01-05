namespace Volcanion.Sample.MongoDB.Models.Documents;

public class ProductDocument : BaseDocument
{
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }
}
