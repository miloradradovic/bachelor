using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.dto;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IProfessionService
    {
        public ApiResponse GetProfessionsByCategory(int categoryId);
        public Profession GetById(int id);
        public List<Profession> GetProfessions();
        public Profession GetByName(string name);
    }

    public class ProfessionService: IProfessionService
    {

        private readonly IProfessionRepository _professionRepository;
        private readonly ICategoryService _categoryService;

        public ProfessionService(IProfessionRepository professionRepository, ICategoryService categoryService)
        {
            _professionRepository = professionRepository;
            _categoryService = categoryService;
        }

        public List<Profession> GetProfessions()
        {
            List<Profession> professions = _professionRepository.GetAll();
            return professions;
        }

        public ApiResponse GetProfessionsByCategory(int categoryId)
        {
            ApiResponse response = new ApiResponse();
            Category category = _categoryService.GetById(categoryId);
            if (category == null)
            {
                response.SetError("Kategorija sa tim id nije pronadjena.", 400);
                return response;
            }

            response.GotProfessions(category.Professions, "Uspesno dobavljene profesije za kategoriju.", 200);
            return response;
        }

        public Profession GetById(int id)
        {
            return _professionRepository.GetById(id);
        }

        public Profession GetByName(string name)
        {
            return _professionRepository.GetByName(name);
        }
    }
}