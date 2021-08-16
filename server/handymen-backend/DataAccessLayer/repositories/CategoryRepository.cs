using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public Category GetById(int id);
    }
    
    public class CategoryRepository : ICategoryRepository
    {

        private readonly PostgreSqlContext _context;

        public CategoryRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = _context.Categories.Include(category => category.Professions).ToList();
            return categories;
        }

        public Category GetById(int id)
        {
            Category found = _context.Categories.Include(category => category.Professions)
                .SingleOrDefault(category => category.Id == id);
            return found;
        }
    }
}