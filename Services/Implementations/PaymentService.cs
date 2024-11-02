using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Enums;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Repositories.Implementations;
using FINALPROJECT.Repositories.Interfaces;
using FINALPROJECT.Services.Interfaces;
using MailKit;
using PayStack.Net;

namespace FINALPROJECT.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IAuctionRepo _auctionRepo;
        private readonly ICarRepo _carRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IEmailSender _mail;
        private readonly ICustomerService _customerService;
        private readonly IPaymentRepo _paymentRepo;
        private readonly IShippingRepo _shippingRepo;
        private readonly IConfiguration _configuration;
        private readonly string Token;
        private PayStackApi PayStack { get; set; }
        

        public PaymentService(IAuctionRepo auctionRepo, ICarRepo carRepo, ICustomerService customerService, IPaymentRepo paymentRepo, IConfiguration configuration,ICustomerRepo customerRepo,IShippingRepo shippingRepo,IEmailSender mail)
        {
            _auctionRepo = auctionRepo;
            _carRepo = carRepo;
            _customerService = customerService;
            _configuration = configuration;
            Token = _configuration["PayStack:SecretKey"];
            PayStack = new PayStackApi(Token);
            _paymentRepo = paymentRepo;
            _customerRepo = customerRepo;
            _shippingRepo = shippingRepo;
            _mail = mail;
        }

        public async Task<BaseResponse<PaymentResponseModel>> Create(double amount, string auctionid, string refrence)
        {
            var auction = await _auctionRepo.GetD(x => x.Id == auctionid);
            var customer = await _customerService.GetCurrentCustomer();
            var payment = new Payment
            {
                Amount = amount,
                AuctionId = auctionid,
                Customer = customer,
                Auction = auction,
                CustomerId = customer.Id,
                Status = PaymentStatus.Paid,
                ReferenceID = refrence
            };
            var car = await _carRepo.Get(x => x.Id == payment.Auction.CarId);
            await _paymentRepo.Add(payment);
            customer.PaymentsMade.Add(payment);
            auction.Payment = payment;
            auction.PaymentId = payment.Id;
            car.Status = CarStatus.Sold;
            customer.OutstandingPayments.Remove(auction);
            await _carRepo.Update(car);
            await _auctionRepo.Update(auction);
            await _customerRepo.Update(customer);
             await _paymentRepo.SaveAsync();
            await _carRepo.SaveAsync();
            await _customerRepo.SaveAsync();
            await _auctionRepo.SaveAsync();
            var address = customer.Addresses.ElementAt(0);
            var shipping = new Shipping
            {
                Address = address,
                Auction = auction,
                AuctionId = auction.Id,
                CustomerId = customer.Id,
                Customer = customer,
                TrackingNumber = GenerateTrackingNumber(),
                PaymentReference = payment.ReferenceID,
            };
            await _shippingRepo.Add(shipping);
            await _shippingRepo.SaveAsync();
            
            var content = $"Dear {customer.LastName}, you have succesfully made payment for `{auction.Name}`. Your Vehincle Will be shipped to {address.Number},{address.Street},{address.City},{address.State}. Please Call {shipping.TrackingNumber} to track your Vehincle. Thank You for Buying with us";
            var request = new EmailRequestModel
            {
                HtmlContent = content,
                ToEmail = customer.Email,
                Subject = "Won Auction",
                ToName = customer.FirstName + " " + customer.LastName,
            };
            _mail.SendEMail(request);
            return new BaseResponse<PaymentResponseModel>
            {
                Data = new PaymentResponseModel
                {
                    Amount = payment.Amount,
                    AuctionId = payment.AuctionId,
                    CustomerId = payment.CustomerId,
                    Date = payment.DateCreated,
                    ReferenceID = payment.ReferenceID,
                },
                Status = true,
            };
        }


        public async Task<BaseResponse<ICollection<PaymentResponseModel>>> GetAllPayments()
        {
            var payments = await _paymentRepo.GetAll();
            if (payments == null)
            {
                return new BaseResponse<ICollection<PaymentResponseModel>>
                {
                    Message = "There are no payments Available",
                    Status = false,
                };
            }
            return new BaseResponse<ICollection<PaymentResponseModel>>
            {
                Data = payments.Select(payment => new PaymentResponseModel
                {
                    Amount = payment.Amount,
                    AuctionId = payment.AuctionId,
                    CustomerId = payment.CustomerId,
                    Date = payment.DateCreated,
                    ReferenceID = payment.ReferenceID,
                    Status = payment.Status,
                }).ToList(),
                Status = true,
            };
        }

        public async Task<BaseResponse<PaymentResponseModel>> GetPayment(string Id)
        {
            var payment = await _paymentRepo.Get(x => x.Id == Id);
            return new BaseResponse<PaymentResponseModel>
            {
                Data = new PaymentResponseModel
                {
                    Amount = payment.Amount,
                    AuctionId = payment.AuctionId,
                    CustomerId = payment.CustomerId,
                    Date = payment.DateCreated,
                    ReferenceID = payment.ReferenceID,
                    Status = payment.Status,
                 },
                Status = true,
            };
        }


        public async Task<BaseResponse<PaymentResponseModel>> MakePayment(string auctionid)
        {

            var customer = await _customerService.GetCurrentCustomer();
            var auction = await _auctionRepo.GetD(x => x.Id == auctionid);
            TransactionInitializeRequest request = new()
            {
                AmountInKobo = (int)(auction.CurrentPrice * 100),
                Email = customer.Email,
                Reference = GeneratePaymentReference(),
                Currency = "NGN",
                CallbackUrl = "https://localhost:5001/customer/viewpayments",
          
            };
            TransactionInitializeResponse response = PayStack.Transactions.Initialize(request);
            if (response.Status)
            {
               await Create(auction.CurrentPrice, auctionid, request.Reference);
                return new BaseResponse<PaymentResponseModel>
                {
                    Status = true,
                    Data = new PaymentResponseModel
                    {
                        ReferenceID = response.Data.AuthorizationUrl,
                    }
                };
            };
            return new BaseResponse<PaymentResponseModel> { Message = response.Message };
        }
        public static string GeneratePaymentReference()
        {
            return Guid.NewGuid().ToString().Substring(0, 10);
        }
        public static string GenerateTrackingNumber()
        {
            var rand = new Random();
            string number = "080" + rand.Next(10000000, 99999999).ToString();
            return number;
        }
    }
}
