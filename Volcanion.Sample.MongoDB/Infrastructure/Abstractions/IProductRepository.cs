using Volcanion.Sample.MongoDB.Models.Documents;

namespace Volcanion.Sample.MongoDB.Infrastructure.Abstractions;

public interface IProductRepository : IBaseRepository<ProductDocument>
{
}
