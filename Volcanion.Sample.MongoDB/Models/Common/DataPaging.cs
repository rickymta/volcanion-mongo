namespace Volcanion.Sample.MongoDB.Models.Common;

public class DataPaging<T>
{
    public long TotalCount { get; set; } = 0;

    public List<T>? Data { get; set; } = [];
}
