using MongoDB.Bson;
using MongoDB.Driver;
using Volcanion.Sample.MongoDB.Infrastructure.Abstractions;
using Volcanion.Sample.MongoDB.Models.Common;
using Volcanion.Sample.MongoDB.Models.Documents;
using Volcanion.Sample.MongoDB.Models.Filtes;

namespace Volcanion.Sample.MongoDB.Infrastructure.Implementations;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseDocument
{
    private readonly IMongoCollection<T> _collection;

    public BaseRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync()
    {
        var filter = Builders<T>.Filter.Eq(d => d.IsDeleted, false);
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var filter = Builders<T>.Filter.And(
            Builders<T>.Filter.Eq(d => d.Id, id),
            Builders<T>.Filter.Eq(d => d.IsDeleted, false)
        );

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T document)
    {
        document.Id = Guid.NewGuid();
        document.CreatedAt = DateTime.UtcNow;
        await _collection.InsertOneAsync(document);
    }

    public async Task UpdateAsync(T document)
    {
        var update = Builders<T>.Update
            .Set(d => d.Name, document.Name)
            .Set(d => d.IsActive, document.IsActive)
            .Set(d => d.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(d => d.Id == document.Id, update);
    }

    public async Task DeleteAsync(Guid id)
    {
        var update = Builders<T>.Update
            .Set(d => d.IsDeleted, true)
            .Set(d => d.DeletedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(d => d.Id == id, update);
    }

    public async Task<DataPaging<T>> GetPagingAsync(FilterBase filter)
    {
        var builder = Builders<T>.Filter;
        var filters = new List<FilterDefinition<T>>
        {
            // Lọc theo IsDeleted = false để lấy dữ liệu chưa bị xóa
            builder.Eq(d => d.IsDeleted, false)
        };

        // Lọc theo trạng thái hoạt động
        if (filter.IsActive.HasValue)
        {
            filters.Add(builder.Eq(d => d.IsActive, filter.IsActive.Value));
        }

        // Tìm kiếm
        if (!string.IsNullOrEmpty(filter.Search))
        {
            // Search không phân biệt hoa thường
            filters.Add(builder.Regex(d => d.Name, new BsonRegularExpression(filter.Search, "i")));
        }

        var finalFilter = builder.And(filters);

        // Tính tổng số bản ghi trước khi phân trang
        long totalCount = await _collection.CountDocumentsAsync(finalFilter);

        // Lấy dữ liệu phân trang
        var data = await _collection
            .Find(finalFilter)
            .Skip((filter.Page - 1) * filter.Limit)
            .Limit(filter.Limit)
            .ToListAsync();

        return new DataPaging<T>
        {
            Data = data,
            TotalCount = totalCount
        };
    }
}

