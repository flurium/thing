﻿using Thing.Models;
using Thing.Repository;
using Thing.Services.Interfaces;

namespace Thing.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyCollection<Category>> List() => await _categoryRepository.GetAllAsync();

        public async Task<bool> Create(Category category)
        {
            try
            {
                await _categoryRepository.CreateAsync(category);
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <returns>Is operation success</returns>
        public async Task<bool> Edit(int id, string name)
        {
            try
            {
                name = name.Trim();
                var sameName = await _categoryRepository.FindByConditionAsync(c => c.Name.ToLower() == name.ToLower());
                if (sameName != null && sameName.Count != 0) return false;
                await _categoryRepository.Update(id, name);
                return true;
            }
            catch (Exception) { return false; }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await _categoryRepository.Delete(id);
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}