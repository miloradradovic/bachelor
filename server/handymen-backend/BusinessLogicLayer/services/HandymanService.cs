using System;
using System.Collections.Generic;
using DataAccessLayer.repositories;
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
        public ApiResponse Search(SearchFilterParams searchParams);
        public ApiResponse Filter(SearchFilterParams searchParams);
        public Rating GetDetailedRatingProfile(int ratingId);
        public ApiResponse EditProfile(HandyMan toUpdate, List<string> trades);
        public ApiResponse GetUnverifiedHandymen();
        public ApiResponse GetByProfessionName(string professionName);
    }
    
    public class HandymanService : IHandymanService
    {
        private readonly IHandymanRepository _handymanRepository;
        private readonly ITradeService _tradeService;
        private readonly IMailService _mailService;
        private readonly ILocationService _locationService;
        private readonly IProfessionService _professionService;

        public HandymanService(IHandymanRepository handymanRepository, ITradeService tradeService, IMailService mailService, ILocationService locationService, IProfessionService professionService)
        {
            _handymanRepository = handymanRepository;
            _tradeService = tradeService;
            _mailService = mailService;
            _locationService = locationService;
            _professionService = professionService;
        }

        public ApiResponse GetByProfessionName(string professionName)
        {
            ApiResponse response = new ApiResponse();
            List<HandyMan> handymen = _handymanRepository.GetAllVerified();
            List<HandyMan> result = new List<HandyMan>();
            foreach (HandyMan handy in handymen)
            {
                if (FindProfessionByTrade(handy.Trades[0], professionName) == professionName)
                {
                    result.Add(handy);
                }
            }

            response.GotHandymenDashboard(result, "Uspesno dobavljeni majstori po profesiji.", 200);
            return response;
        }

        private string FindProfessionByTrade(Trade trade, string professionName)
        {
            Profession professions = _professionService.GetByName(professionName);
            foreach (Trade trade2 in professions.Trades)
            {
                if (trade2.Name == trade.Name)
                {
                    return professions.Name;
                }
            }

            return "";
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
            ApiResponse response = new ApiResponse();
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
                    response.SetError("Neko od imena usluga nije validno.", 400);
                    return response;
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
            request.AverageRate = 0.0;
            HandyMan saved = _handymanRepository.Create(request);

            if (saved == null)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom registracije. " +
                                  "Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }

            response.RegisteredHandyman(saved,
                "Uspesno kreiran zahtev za registraciju. Poslacemo Vam email kada administrator verifikuje vas zahtev.",
                201);
            return response;
        }

        public ApiResponse VerifyHandyman(HandymanVerificationData handymanVerificationData)
        {
            ApiResponse response = new ApiResponse();
            HandyMan found = _handymanRepository.GetById(handymanVerificationData.Id, false);
            if (found == null)
            {
                response.SetError("Majstor sa tim id nije pronadjen.", 400);
                return response;
            }

            if (handymanVerificationData.Verify) // if verified
            {
                found.Verified = true;
                HandyMan updated = _handymanRepository.Update(found);
                if (updated == null)
                {
                    response.SetError("Nesto se desilo sa bazom podataka prilikom verifikacije zahteva za registraciju. Molimo pokusajte ponovo kasnije.", 400);
                    return response;
                }
                _mailService.SendEmail(new MailRequest()
                {
                    Body = "Pozdrav!<br>Vas nalog je verifikovan. Sada se mozete ulogovati klikom na <a href='https://localhost:4200'>LINK</a>",
                    Subject = "Nalog je verifikovan",
                    ToEmail = updated.Email
                });

                response.VerifiedHandyman(updated, "Uspesno verifikovan nalog majstora.", 200);
                return response;
            }
            // if request has been denied, then delete it
            bool deleted = _handymanRepository.Delete(found);
            if (!deleted)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom brisanja zahteva za registraciju. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Pozdrav!<br>Vas nalog na zalost nije odobren od strane nasih administratora. Razlog: \n" + handymanVerificationData.Message + ".",
                Subject = "Nalog nije odobren",
                ToEmail = found.Email
            });

            response.VerifiedHandyman(found, "Uspesno obrisan zahtev za registraciju.", 200);
            return response;
        }

        public HandyMan Update(HandyMan toUpdate)
        {
            return _handymanRepository.Update(toUpdate);
        }

        public ApiResponse GetAll()
        {
            ApiResponse response = new ApiResponse();
            List<HandyMan> result = _handymanRepository.GetAllVerified();
            response.GotHandymenDashboard(result, "Uspesno dobavljeni svi majstori za prikaz.", 200);
            return response;
        }

        public ApiResponse Search(SearchFilterParams searchParams)
        {
            ApiResponse response = new ApiResponse();
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

            bool searchByAddress = false;
            if (searchParams.Address != null)
            {
                if (searchParams.Address != "")
                {
                    searchByAddress = true;
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
                searchParams.AvgRatingTo == 5 && !searchByAddress)
            {
                result = _handymanRepository.GetAllVerified();
            }
            else
            {
                result = _handymanRepository.Search(searchParams, searchByFirstName, searchByLastName);
                result = FilterByTradesAndAverageAndAddress(result, searchParams, searchByTrades, searchByAddress);
                

            }
            response.GotHandymenDashboard(result, "Uspesno odradjena pretraga.", 200);
            return response;
        }

        public ApiResponse Filter(SearchFilterParams searchParams)
        {
            ApiResponse response = new ApiResponse();
            bool filterByFirstName = false;
            if (searchParams.FirstName != null)
            {
                if (searchParams.FirstName != "")
                {
                    filterByFirstName = true;
                }
            }

            bool filterByLastName = false;
            if (searchParams.LastName != null)
            {
                if (searchParams.LastName != "")
                {
                    filterByLastName = true;
                }
            }

            bool filterByAddress = false;
            if (searchParams.Address != null)
            {
                if (searchParams.Address != "")
                {
                    filterByAddress = true;
                }
            }

            bool filterByTrades = false;
            if (searchParams.Trades != null)
            {
                if (searchParams.Trades.Count != 0)
                {
                    filterByTrades = true;
                }
            }

            List<HandyMan> handymen = new List<HandyMan>();
            foreach (HandymanDashboardDTO dto in searchParams.Handymen)
            {
                handymen.Add(_handymanRepository.GetByEmail(dto.Email));
            }

            if (filterByFirstName)
            {
                int i = 0;
                while (i < handymen.Count)
                {
                    if (handymen[i].FirstName.Contains(searchParams.FirstName) == false)
                    {
                        handymen.Remove(handymen[i]);
                    }
                    else
                    {
                        i = i + 1;
                    }
                }
            }

            if (filterByLastName)
            {
                int i = 0;
                while (i < handymen.Count)
                {
                    if (handymen[i].LastName.Contains(searchParams.LastName) == false)
                    {
                        handymen.Remove(handymen[i]);
                    }
                    else
                    {
                        i = i + 1;
                    }
                }
            }

            List<HandyMan> result =
                FilterByTradesAndAverageAndAddress(handymen, searchParams, filterByTrades, filterByAddress);
            
            response.GotHandymenDashboard(result, "Uspesno filtrirani majstori.", 200);
            return response;
        }

        private List<HandyMan> FilterByTradesAndAverageAndAddress(List<HandyMan> list, SearchFilterParams searchParams, bool searchByTrades, bool searchByAddress)
        {
            List<HandyMan> result = new List<HandyMan>();
            
            foreach (HandyMan handyMan in list)
            {
                double avg = handyMan.AverageRate;
                if (searchByTrades && searchByAddress)
                {
                    if (CheckTrades(handyMan.Trades, searchParams.Trades) &&
                        avg >= searchParams.AvgRatingFrom &&
                        avg <= searchParams.AvgRatingTo && CheckAddress(handyMan, searchParams.Address) && handyMan.Verified)
                    {
                        result.Add(handyMan);
                    }
                } 
                else if (searchByTrades && !searchByAddress)
                {
                    if (CheckTrades(handyMan.Trades, searchParams.Trades) &&
                        avg >= searchParams.AvgRatingFrom &&
                        avg <= searchParams.AvgRatingTo && handyMan.Verified)
                    {
                        result.Add(handyMan);
                    }
                }
                else if (!searchByTrades && searchByAddress)
                {
                    if (CheckAddress(handyMan, searchParams.Address) &&
                        avg >= searchParams.AvgRatingFrom &&
                        avg <= searchParams.AvgRatingTo && handyMan.Verified)
                    {
                        result.Add(handyMan);
                    }
                }
                else
                {
                    if (avg >= searchParams.AvgRatingFrom &&
                        avg <= searchParams.AvgRatingTo && handyMan.Verified)
                    {
                        result.Add(handyMan);
                    }
                }
            }

            return result;
        }

        private bool CheckAddress(HandyMan handyMan, string address)
        {
            Location handymanLocation = _locationService.GetByLatAndLng(handyMan.Address.Latitude, handyMan.Address.Longitude);
            double handymanRadius = handyMan.Radius;

            List<Location> addressToCheck = _locationService.GetLocationsByName(address);
            foreach (Location location in addressToCheck)
            {
                if (location.Id == handymanLocation.Id && DetermineCircle(handymanLocation, handymanRadius, location))
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool DetermineCircle(Location handymanAddress, double handymanRadius, Location adAddress)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(handymanAddress.Latitude - adAddress.Latitude), 2) +
                             Math.Pow(Math.Abs(handymanRadius - adAddress.Longitude), 2)) <= handymanRadius;
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

        public Rating GetDetailedRatingProfile(int ratingId)
        {
            return _handymanRepository.GetDetailedRatingProfile(ratingId);
        }

        public ApiResponse EditProfile(HandyMan toUpdate, List<string> tradesStrings)
        {
            ApiResponse response = new ApiResponse();
            HandyMan found = GetById(toUpdate.Id);
            List<Trade> trades = new List<Trade>();
            foreach (string trade in tradesStrings)
            {
                trades.Add(_tradeService.GetByName(trade));
            }

            Location foundLocation = _locationService.GetByLatAndLng(toUpdate.Address.Latitude, toUpdate.Address.Longitude);
            if (foundLocation != null)
            {
                toUpdate.Address = foundLocation;
            }
            found.Address = toUpdate.Address;
            found.Radius = toUpdate.Radius;
            found.Trades = trades;
            found.Email = toUpdate.Email;
            found.FirstName = toUpdate.FirstName;
            found.LastName = toUpdate.LastName;
            HandyMan updated = Update(found);
            if (updated == null)
            {
                response.SetError("Doslo je do greske prilikom izmene profila.", 400);
                return response;
            }

            response.UpdatedHandymanProfile(updated,
                "Uspesno izmenjen profil. Molimo da se ulogujete ponovo da bi promena imala efekat.", 200);
            return response;
        }

        public ApiResponse GetUnverifiedHandymen()
        {
            ApiResponse response = new ApiResponse();
            List<HandyMan> handyMen = _handymanRepository.GetAllUnverified();
            response.GotRegistrationRequests(handyMen, "Uspesno dobavljeni svi neverifikovani majstori.", 200);
            return response;
        }
    }
}