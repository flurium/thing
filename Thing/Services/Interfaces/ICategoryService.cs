using Thing.Models;

namespace Thing.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IReadOnlyCollection<Category>> List();

        public Task<bool> Create(Category category);

        /// <returns>Is operation success</returns>
        public Task<bool> Edit(int id, string name);

        public Task<bool> Delete(int id);
    }
}