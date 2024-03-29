﻿using Thing.UnitOfWork;
using Domain.Models;

namespace Thing.Services
{
    public class CustomPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomPropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CustomProperty property)
        {
            if (property != null)
            {
                await _unitOfWork.CustomPropertyRepository.CreateAsync(property);
            }
        }
    }
}