using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.repositories;
using Microsoft.EntityFrameworkCore;
using Model.dto;
using Model.models;
using BC = BCrypt.Net.BCrypt;

namespace BusinessLogicLayer.services
{

    public interface IHandymanService
    {
        public HandyMan GetById(int id);
        public HandyMan GetByEmailAndPassword(string email, string password);
        public HandyMan GetByEmail(string email);
        public ApiResponse Register(HandyMan request, List<string> trades);
        public ApiResponse VerifyHandyman(HandymanVerificationData handymanVerificationData);
        public HandyMan Update(HandyMan toUpdate);
        public ApiResponse GetAll();
        public ApiResponse Search(SearchParams searchParams);
    }
    
    public class HandymanService : IHandymanService
    {
        private readonly IHandymanRepository _handymanRepository;
        private readonly ITradeService _tradeService;
        private readonly IMailService _mailService;
        private readonly ILocationService _locationService;

        public HandymanService(IHandymanRepository handymanRepository, ITradeService tradeService, IMailService mailService, ILocationService locationService)
        {
            _handymanRepository = handymanRepository;
            _tradeService = tradeService;
            _mailService = mailService;
            _locationService = locationService;
        }

        public HandyMan GetById(int id)
        {
            return _handymanRepository.GetById(id);
        }

        public HandyMan GetByEmailAndPassword(string email, string password)
        {
            return _handymanRepository.GetByEmailAndPassword(email, password);
        }

        public HandyMan GetByEmail(string email)
        {
            return _handymanRepository.GetByEmail(email);
        }

        public ApiResponse Register(HandyMan request, List<string> trades)
        {

            Location foundLocation =
                _locationService.GetByLatAndLng(request.Address.Latitude, request.Address.Longitude);
            if (foundLocation != null)
            {
                request.Address = foundLocation;
            }
            
            foreach (string trade in trades)
            {
                Trade found = _tradeService.GetByName(trade);
                if (found == null)
                {
                    return new ApiResponse()
                    {
                        Message = "One of the trade names is invalid.",
                        ResponseObject = null,
                        Status = 400
                    };
                }

                try
                {
                    request.Trades.Add(found);
                }
                catch
                {
                    request.Trades = new List<Trade>();
                    request.Trades.Add(found);
                }
            }

            request.Password = BC.HashPassword(request.Password);
            HandyMan saved = _handymanRepository.Create(request);

            if (saved == null)
            {
                return new ApiResponse()
                {
                    Message = "Something went wrong with the database. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            return new ApiResponse()
            {
                Message =
                    "Successfully created registration request. We will email you when your account is verified by our administrators",
                ResponseObject = saved.ToDtoWithTrades(),
                Status = 201
            };
        }

        public ApiResponse VerifyHandyman(HandymanVerificationData handymanVerificationData)
        {
            HandyMan found = _handymanRepository.GetById(handymanVerificationData.Id, false);
            if (found == null)
            {
                return new ApiResponse()
                {
                    Message = "Could not find handyman account by id.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            if (handymanVerificationData.Verify) // if verified
            {
                found.Verified = true;
                HandyMan updated = _handymanRepository.Update(found);
                if (updated == null)
                {
                    return new ApiResponse()
                    {
                        Message =
                            "Something went wrong with the database while verifying handyman account. Please try again later.",
                        ResponseObject = null,
                        Status = 400
                    };
                }
                _mailService.SendEmail(new MailRequest()
                {
                    Body = "Your account has been verified. Now you can log in following this <a href='https://localhost:4200'>LINK</a>",
                    Subject = "Account verified",
                    ToEmail = updated.Email
                });

                return new ApiResponse()
                {
                    Message = "Successfully verified handyman account.",
                    ResponseObject = updated.ToDtoWithoutLists(),
                    Status = 200
                };
            }
            // if request has been denied, then delete it
            bool deleted = _handymanRepository.Delete(found);
            if (!deleted)
            {
                return new ApiResponse()
                {
                    Message =
                        "Something went wrong with the database while deleting the handyman registration request. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Your account has not been approved by our administrators. The reason: \n" + handymanVerificationData.Message,
                Subject = "Account not approved",
                ToEmail = found.Email
            });

            return new ApiResponse()
            {
                Message = "Successfully deleted handyman registration request.",
                ResponseObject = found.ToDtoWithoutLists(),
                Status = 200
            };
        }

        public HandyMan Update(HandyMan toUpdate)
        {
            return _handymanRepository.Update(toUpdate);
        }

        public ApiResponse GetAll()
        {
            List<HandyMan> result = _handymanRepository.GetAll();
            List<HandymanDashboardDTO> handymanDashboardDtos = new List<HandymanDashboardDTO>();

            foreach (HandyMan handyman in result)
            {
                handymanDashboardDtos.Add(handyman.ToDahboardDTO());
            }

            return new ApiResponse()
            {
                Message = "Successfully fetched all handymen for dashboard.",
                ResponseObject = handymanDashboardDtos,
                Status = 200
            };
        }

        public ApiResponse Search(SearchParams searchParams)
        {
            bool searchByFirstName = false;
            if (searchParams.FirstName != null)
            {
                if (searchParams.FirstName != "")
                {
                    searchByFirstName = true;
                }
            }

            bool searchByLastName = false;
            if (searchParams.LastName != null)
            {
                if (searchParams.LastName != "")
                {
                    searchByLastName = true;
                }
            }

            bool searchByTrades = false;
            if (searchParams.Trades != null)
            {
                if (searchParams.Trades.Count != 0)
                {
                    searchByTrades = true;
                }
            }

            List<HandyMan> result = new List<HandyMan>();
            if (!searchByTrades && !searchByFirstName && !searchByLastName && searchParams.AvgRatingFrom == 0 &&
                searchParams.AvgRatingTo == 5)
            {
                result = _handymanRepository.GetAll();
            }
            else
            {
                result = _handymanRepository.Search(searchParams, searchByFirstName, searchByLastName);
                result = FilterByTradesAndAverage(result, searchParams, searchByTrades);
                

            }
            List<HandymanDashboardDTO> handymanDashboardDtos = new List<HandymanDashboardDTO>();
            foreach (HandyMan handyMan in result)
            {
                handymanDashboardDtos.Add(handyMan.ToDahboardDTO());
            }

            return new ApiResponse()
            {
                Message = "Successfully did searching.",
                ResponseObject = handymanDashboardDtos,
                Status = 200
            };

        }

        private List<HandyMan> FilterByTradesAndAverage(List<HandyMan> list, SearchParams searchParams, bool searchByTrades)
        {
            List<HandyMan> result = new List<HandyMan>();
            
            foreach (HandyMan handyMan in list)
            {
                double avg = handyMan.CalculateAverageRate();
                if (searchByTrades)
                {
                    if (CheckTrades(handyMan.Trades, searchParams.Trades) &&
                        avg >= searchParams.AvgRatingFrom &&
                        avg <= searchParams.AvgRatingTo)
                    {
                        result.Add(handyMan);
                    }
                }
                else
                {
                    if (avg >= searchParams.AvgRatingFrom &&
                        avg <= searchParams.AvgRatingTo)
                    {
                        result.Add(handyMan);
                    }
                }
            }

            return result;
        }

        private bool CheckTrades(List<Trade> trades, List<string> tradeNames)
        {

            foreach (string name in tradeNames)
            {
                bool check = false;
                foreach (Trade trade in trades)
                {
                    if (trade.Name == name)
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