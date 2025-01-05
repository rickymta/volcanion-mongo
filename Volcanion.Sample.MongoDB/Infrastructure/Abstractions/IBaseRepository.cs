using Volcanion.Sample.MongoDB.Models.Common;
using Volcanion.Sample.MongoDB.Models.Documents;
using Volcanion.Sample.MongoDB.Models.Filtes;

namespace Volcanion.Sample.MongoDB.Infrastructure.Abstractions;

public interface IBaseRepository<T>
    where T : BaseDocument
{
    Task<List<T>> GetAllAsync();

    Task<DataPaging<T>> GetPagingAsync(FilterBase filter);

    Task<T> GetByIdAsync(Guid id);

    Task CreateAsync(T document);

    Task UpdateAsync(T document);

    Task DeleteAsync(Guid id);
}
