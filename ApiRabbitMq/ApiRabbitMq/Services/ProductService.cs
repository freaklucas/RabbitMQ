using ApiRabbitMq.Context;
using ApiRabbitMq.Models;
using ApiRabbitMq.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ApiRabbitMq.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    
    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public Product AddProduct(Product product)
    {
        if (product is null) throw new InvalidOperationException("Produto veio nulo.");

        var result = _context.Products.Add(product);

        _context.SaveChanges();

        return result.Entity;
    }

    public bool DeleteProduct(int id)
    {
        var findProduct = _context
            .Products
            .Where(p => p.ProductId == id)
            .FirstOrDefault();

        if (findProduct is null) throw new InvalidOperationException("Produto não encontrado.");

        _context.Products.Remove(findProduct);
        _context.SaveChanges();

        return true;
    }


    public Product GetProductById(int id)
    {
        var findProduct = _context
            .Products
            .Where(p => p.ProductId == id)
            .FirstOrDefault();

        if(findProduct is null) throw new InvalidOperationException($"Produto de id: {id} não encontrado.");
        

        return findProduct;
    }

    public IEnumerable<Product> GetProductList()
    {
        var products = _context.Products.AsNoTracking().ToList();

        return products.Any() ? products : Enumerable.Empty<Product>();
    }

    public Product UpdateProduct(Product product)
    {
        var result = _context.Products.Update(product);
        _context.SaveChanges();

        return result.Entity;
    }
}
