using InspireMe.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class CategoriesService : ICategoriesService
{
    private readonly List<Category> _categories = new List<Category>();

    public Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return Task.FromResult(_categories.AsEnumerable());
    }

    public Task<Category> GetCategoryByIdAsync(int id)
    {
        return Task.FromResult(_categories.FirstOrDefault(c => c.Id == id));
    }

    public Task<Category> CreateCategoryAsync(Category category)
    {
        category.Id = _categories.Count + 1;
        _categories.Add(category);
        return Task.FromResult(category);
    }

    public Task<Category> UpdateCategoryAsync(int id, Category category)
    {
        var existingCategory = _categories.FirstOrDefault(c => c.Id == id);
        if (existingCategory != null)
        {
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
        }
        return Task.FromResult(existingCategory);
    }

    public Task<bool> DeleteCategoryAsync(int id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        if (category != null)
        {
            _categories.Remove(category);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}