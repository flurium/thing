﻿using Dal.UnitOfWork;
using Domain.Models;

namespace Dal.Services
{
    public class SellerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SellerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Seller entity) => await _unitOfWork.SellerRepository.CreateAsync(entity);
    }
}