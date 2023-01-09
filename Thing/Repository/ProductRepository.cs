﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Thing.Context;
using Thing.Models;
using Thing.Repository.Interfaces;

namespace Thing.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ThingDbContext context) : base(context)
        {
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            Product res = new Product();
            await Entities.AddAsync(entity).ConfigureAwait(false);
            res = entity;
            await _db.SaveChangesAsync();
            return res;
        }

        public async Task<Product> FirstOfDefult(Expression<Func<Product, bool>> conditon)
        {
            return Entities.FirstOrDefaultAsync(conditon).Result;
        }

        public async Task Delete(int id)
        {
            var product = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null) Entities.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product?> DeleteAndReturn(int id)
        {
            var product = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null) Entities.Remove(product);
            return product;
        }

        public virtual async Task<IReadOnlyCollection<Product>> FindByConditionsAsync(IEnumerable<Expression<Func<Product, bool>>> conditons, bool includeSeller = true)
        {
            IQueryable<Product> res = Entities;
            foreach (var conditon in conditons)
            {
                res = res.Where(conditon);
            }
            if (includeSeller) res = res.Include(x => x.Seller.User);
            return await res.ToListAsync().ConfigureAwait(false);
        }

        public async Task Edit(Product product)
        {
            Entities.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> FindWithImagesProps(int id)
        {
            return await Entities
                .Include(p => p.Images)
                .Include(p => p.CustomProperties)
                .Include(p => p.RequiredPropertyValues).ThenInclude(rpv => rpv.Property)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyCollection<Product>> FindWithImages(Expression<Func<Product, bool>> condition)
        {
            return await Entities
                .Include(p => p.Images)
                .Where(condition)
                .ToListAsync();
        }
    }
}