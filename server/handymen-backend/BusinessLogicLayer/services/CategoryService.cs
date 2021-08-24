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
            List<Category> categories = _categoryRepository.GetCategories();
            List<CategoryDTO> dtos = new List<CategoryDTO>();
            foreach (Category category in categories)
            {
                dtos.Add(category.ToCategoryDTO());
            }

            return new ApiResponse()
            {
                Message = "Uspesno dobavljene sve kategorije.",
                ResponseObject = dtos,
                Status = 200
            };
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetCategories();
        }
    }
}