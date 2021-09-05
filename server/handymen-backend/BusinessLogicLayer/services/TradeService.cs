﻿using System;
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
            List<Trade> trades = _tradeRepository.GetAll();
            if (trades == null)
            {
                return new ApiResponse()
                {
                    Message = "Nesto se desilo sa bazom podataka prilikom dobavljanja usluga. Molimo pokusajte ponovo kasnije.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            List<TradeDTO> tradeDtos = new List<TradeDTO>();
            foreach (Trade trade in trades)
            {
                tradeDtos.Add(trade.ToTradeDTO());
            }

            return new ApiResponse()
            {
                Message = "Uspesno dobavljene sve usluge.",
                ResponseObject = tradeDtos,
                Status = 200
            };
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
            Profession profession = _professionService.GetById(professionId);
            if (profession == null)
            {
                return new ApiResponse()
                {
                    Message = "Profesija sa tim id nije pronadjena.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            List<TradeDTO> dtos = new List<TradeDTO>();
            foreach (Trade trade in profession.Trades)
            {
                dtos.Add(trade.ToTradeDTO());
            }

            return new ApiResponse()
            {
                Message = "Uspesno dobavljene usluge za profesiju.",
                ResponseObject = dtos,
                Status = 200
            };
        }

        public ApiResponse GetTradesByProfessionName(string professionName)
        {
            Profession profession = _professionService.GetByName(professionName);
            if (profession == null)
            {
                return new ApiResponse()
                {
                    Message = "Profesija sa tim imenom nije pronadjena.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            List<TradeDTO> dtos = new List<TradeDTO>();
            foreach (Trade trade in profession.Trades)
            {
                dtos.Add(trade.ToTradeDTO());
            }

            return new ApiResponse()
            {
                Message = "Uspesno dobavljene usluge za profesiju.",
                ResponseObject = dtos,
                Status = 200
            };
        }

        public ApiResponse GetCategoryAndProfessionByCurrentHandyman(HandyMan handyMan)
        {
            List<Trade> trades = handyMan.Trades;
            List<Category> categories = _categoryService.GetAll();
            List<Profession> professions = _professionService.GetProfessions();
            ProfessionDTO professionDto = new ProfessionDTO();
            CategoryDTO categoryDto = new CategoryDTO();

            foreach (Profession profession in professions)
            {
                if (CheckTrades(profession.Trades, handyMan.Trades))
                {
                    professionDto = profession.ToProfessionDTO();
                    break;
                }
            }

            foreach (Category category in categories)
            {
                if (CheckProfession(category.Professions, professionDto))
                {
                    categoryDto = category.ToCategoryDTO();
                }
            }

            return new ApiResponse()
            {
                Message = "Uspesno dobavljena kategorija i profesija za majstora.",
                ResponseObject = new HandymanCategoryProfessionDTO()
                {
                    CategoryDto = categoryDto,
                    ProfessionDto = professionDto
                }
            };


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