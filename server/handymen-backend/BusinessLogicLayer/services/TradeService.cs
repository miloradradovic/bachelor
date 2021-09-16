using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.dto;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface ITradeService
    {
        public Trade GetByName(string name);
        public Trade Create(Trade trade);
        public ApiResponse GetAll();
        public void UpdateTrades(HandyMan updatedHandyman);
        public ApiResponse GetTradesByProfession(int professionId);
        public ApiResponse GetTradesByProfessionName(string professionName);
        public ApiResponse GetCategoryAndProfessionByCurrentHandyman(HandyMan handyMan);
    }
    
    public class TradeService : ITradeService
    {

        private readonly ITradeRepository _tradeRepository;
        private readonly IProfessionService _professionService;
        private readonly ICategoryService _categoryService;

        public TradeService(ITradeRepository tradeRepository, IProfessionService professionService, ICategoryService categoryService)
        {
            _tradeRepository = tradeRepository;
            _professionService = professionService;
            _categoryService = categoryService;
        }

        public Trade GetByName(string name)
        {
            return _tradeRepository.GetByName(name);
        }

        public Trade Create(Trade trade)
        {
            if (_tradeRepository.GetByName(trade.Name) == null)
            {
                return _tradeRepository.Create(trade);
            }

            return null;
        }

        public ApiResponse GetAll()
        {
            ApiResponse response = new ApiResponse();
            List<Trade> trades = _tradeRepository.GetAll();
            if (trades == null)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom dobavljanja usluga. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }

            response.GotTrades(trades, "Uspesno dobavljene sve usluge.", 200);
            return response;
        }

        public void UpdateTrades(HandyMan updatedHandyman)
        {
            foreach (Trade trade in updatedHandyman.Trades)
            {
                try
                {
                    trade.HandyMen.Add(updatedHandyman);
                }
                catch
                {
                    trade.HandyMen = new List<HandyMan>();
                    trade.HandyMen.Add(updatedHandyman);
                }

                _tradeRepository.Update(trade);
            }
        }

        public ApiResponse GetTradesByProfession(int professionId)
        {
            ApiResponse response = new ApiResponse();
            Profession profession = _professionService.GetById(professionId);
            if (profession == null)
            {
                response.SetError("Profesija sa tim id nije pronadjena.", 400);
                return response;
            }
            
            response.GotTrades(profession.Trades, "Uspesno dobavljene usluge za profesiju.", 200);
            return response;
        }

        public ApiResponse GetTradesByProfessionName(string professionName)
        {
            ApiResponse response = new ApiResponse();
            Profession profession = _professionService.GetByName(professionName);
            if (profession == null)
            {
                response.SetError("Profesija sa tim imenom nije pronadjena.", 400);
                return response;
            }
            
            response.GotTrades(profession.Trades, "Uspesno dobavljene usluge za profesiju.", 200);
            return response;
        }

        public ApiResponse GetCategoryAndProfessionByCurrentHandyman(HandyMan handyMan)
        {
            ApiResponse response = new ApiResponse();
            List<Trade> trades = handyMan.Trades;
            List<Category> categories = _categoryService.GetAll();
            List<Profession> professions = _professionService.GetProfessions();

            Profession prof = new Profession();
            foreach (Profession profession in professions)
            {
                if (CheckTrades(profession.Trades, handyMan.Trades))
                {
                    prof = profession;
                    break;
                }
            }

            Category cat = new Category();
            foreach (Category category in categories)
            {
                if (CheckProfession(category.Professions, prof.ToProfessionDTO()))
                {
                    cat = category;
                    break;
                }
            }

            response.GotCategoryProfession(prof, cat, "Uspesno dobavljena kategorija i profesija za majstora.", 200);
            return response;
        }

        private bool CheckProfession(List<Profession> professions, ProfessionDTO professionDto)
        {
            foreach (Profession profession in professions)
            {
                if (profession.Name == professionDto.Name)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckTrades(List<Trade> professionTrades, List<Trade> handymanTrades)
        {
            foreach (Trade handymanTrade in handymanTrades)
            {
                bool check = false;
                foreach (Trade professionTrade in professionTrades)
                {
                    if (handymanTrade.Name == professionTrade.Name)
                    {
                        check = true;
                        break;
                    }
                }

                if (!check)
                {
                    return false;
                }
            }

            return true;
        }
    }
}