namespace Volcanion.Sample.MongoDB.Models.Filtes;

public abstract class FilterBase
{
    public string? Search { get; set; }

    public int Page { get; set; } = 1;
    
    public int Limit { get; set; } = 10;

    public bool? IsActive { get; set; }
}

