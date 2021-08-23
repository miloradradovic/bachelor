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
            Category category = _categoryService.GetById(categoryId);
            if (category == null)
            {
                return new ApiResponse()
                {
                    Message = "Could not find category by id.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            List<ProfessionDTO> dtos = new List<ProfessionDTO>();

            foreach (Profession profession in category.Professions)
            {
                dtos.Add(profession.ToProfessionDTO());
            }

            return new ApiResponse()
            {
                Message = "Successfully fetched professions by category.",
                ResponseObject = dtos,
                Status = 200
            };
        }

        public Profession GetById(int id)
        {
            return _professionRepository.GetById(id);
        }
    }
}