using Infrastructure.Models.Product;

namespace Infrastructure.Interfaces;

public interface IJsonFileRepository
{
    Task WriteAsync(IEnumerable<Product> products, CancellationToken cancellationToken = default);
    ValueTask<IReadOnlyList<Product>> ReadAsync(CancellationToken cancellationToken = default);

}
