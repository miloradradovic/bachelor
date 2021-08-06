using System;
using System.Collections.Generic;
using DataAccessLayer.repositories;
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
    }
    
    public class HandymanService : IHandymanService
    {
        private readonly IHandymanRepository _handymanRepository;
        private readonly ITradeService _tradeService;
        private readonly IMailService _mailService;

        public HandymanService(IHandymanRepository handymanRepository, ITradeService tradeService, IMailService mailService)
        {
            _handymanRepository = handymanRepository;
            _tradeService = tradeService;
            _mailService = mailService;
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
                    Status = 404
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
    }
}