using portfolioApi.Model;

namespace portfolioApi.Repository;

public interface IProductRepository
{
    List<Product> GetAll();
    Product? GetById(int id);
    Product Add(Product product);
    Product? Update(int id, Product updatedProduct);
    bool Delete(int id);
}
