using MongoDB.Driver;
using Volcanion.Sample.MongoDB.Infrastructure.Abstractions;
using Volcanion.Sample.MongoDB.Models.Documents;

namespace Volcanion.Sample.MongoDB.Infrastructure.Implementations;

public class ProductRepository : BaseRepository<ProductDocument>, IProductRepository
{
    public ProductRepository(IMongoDatabase database) : base(database, "Products")
    {
    }
}
