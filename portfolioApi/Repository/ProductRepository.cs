using portfolioApi.Model;

namespace portfolioApi.Repository;

public class ProductRepository : IProductRepository
{
    // Static in-memory product list for testing
    private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 75000m, Category = "Electronics" },
        new Product { Id = 2, Name = "Smartphone", Price = 45000m, Category = "Electronics" },
        new Product { Id = 3, Name = "Wireless Headphones", Price = 3500m, Category = "Accessories" },
        new Product { Id = 4, Name = "Office Chair", Price = 8500m, Category = "Furniture" }
    };

    // 1. Get All Products
    public List<Product> GetAll()
    {
        return products;
    }

    // 2. Get Product By ID
    public Product? GetById(int id)
    {
        return products.FirstOrDefault(p => p.Id == id);
    }

    // 3. Add New Product (POST)
    public Product Add(Product product)
    {
        if (product.Id == 0)
        {
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
        }

        products.Add(product);
        return product;
    }

    // 4. Update Product (PUT)
    public Product? Update(int id, Product updatedProduct)
    {
        var existingProduct = products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
        {
            return null;
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;
        existingProduct.Category = updatedProduct.Category;

        return existingProduct;
    }

    // 5. Delete Product By ID (DELETE)
    public bool Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return false;
        }

        products.Remove(product);
        return true;
    }
}
