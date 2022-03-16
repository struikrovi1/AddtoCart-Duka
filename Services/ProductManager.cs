using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductManager
    {

        private readonly AgencyContext _context;

        public ProductManager(AgencyContext context)
        {
            _context = context;
        }

        public List<Product> GetAll(Func<Product, bool>? f = null)
        {
            if (f != null)
            {
                return _context.Products
             .Include(c => c.Category).ThenInclude(c => c.CategoryRecords)
             .Include(c => c.ProductRecords)
             .Include(c => c.ProductPictures).ThenInclude(c => c.Picture)
             .Where(f)
             .ToList();
            }

            return _context.Products
    .Include(c => c.Category)
    .ThenInclude(c => c.CategoryRecords)
    .Include(c => c.ProductRecords)
    .Include(c => c.ProductPictures)
    .ThenInclude(c => c.Picture)

    .ToList();



        }


        public async Task<Product?> GetById(int id)
        {
            return await _context.Products
           .Include(c => c.ProductRecords)
                .Include(c => c.ProductPictures)
                .ThenInclude(c => c.Picture)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Product>>? GetByIds(IEnumerable<int> ids)
        {
            return await _context.Products
           .Include(c => c.ProductRecords)
                .Include(c => c.ProductPictures)
                .ThenInclude(c => c.Picture)
               .Where(c => ids.Contains(c.Id))
               .ToListAsync();
        }

        public async void Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var selectedProduct = await GetById(id);

            if (selectedProduct == null) return;
            selectedProduct.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> SearchProducts(int? categoryId, string? q, decimal? minPrice, decimal? maxPrice, int? sortBy)
        {
            var products = _context.Products
                .Where(c => !c.IsDeleted)
                 .Include(c => c.ProductRecords)
                .Include(c => c.ProductPictures)
                .ThenInclude(c => c.Picture)

                .AsQueryable();

            if (categoryId.HasValue)
            {
                products = products.Where(c => c.CategoryId == categoryId);
            }

            if (minPrice.HasValue && maxPrice.HasValue)
            {
                products = products.Where(c => c.Price >= minPrice && c.Price <= maxPrice);
            }

            if (sortBy.HasValue)
            {
                products = sortBy.Value switch
                {
                    1 => products.OrderByDescending(c => c.Price),
                    2 => products.OrderBy(c => c.Price),
                    _ => products.OrderByDescending(c => c.ModifiedOn),
                };
            }

            return await products.ToListAsync();


        }
    }
}
