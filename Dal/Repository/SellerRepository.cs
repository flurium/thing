﻿using Dal.Context;
using Dal.Repository.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repository
{
    public class SellerRepository : BaseRepository<Seller>, ISellerRepository
    {
        public SellerRepository(ThingDbContext context) : base(context)
        {
        }

        public Task Edit(string id, bool isBanned)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string id)
        {
            var seller = await Entities.FirstOrDefaultAsync(x => x.Id == id);
            if (seller != null) Entities.Remove(seller);
        }

        public async Task Edit(Seller seller)
        {
            Entities.Update(seller);
            await _db.SaveChangesAsync();
        }
    }
}