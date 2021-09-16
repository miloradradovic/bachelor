using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.dto;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface ICategoryService
    {
        public ApiResponse GetCategories();
        public Category GetById(int id);
        public List<Category> GetAll();
        public ApiResponse GetCategoriesWithProfessions();
    }
    
    public class CategoryService: ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ApiResponse GetCategories()
        {
            ApiResponse response = new ApiResponse();
            List<Category> categories = _categoryRepository.GetCategories();
            response.GotCategories(categories, "Uspesno dobavljene sve kategorije.", 200);
            return response;
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetCategories();
        }

        public ApiResponse GetCategoriesWithProfessions()
        {
            ApiResponse response = new ApiResponse();
            List<Category> categories = GetAll();
            response.GotCategories(categories, "Uspesno dobavljene kategorije sa profesijama.", 200);
            return response;
        }
    }
}