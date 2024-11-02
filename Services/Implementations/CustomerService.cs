using System.Security.Claims;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Repositories.Implementations;
using FINALPROJECT.Repositories.Interfaces;
using FINALPROJECT.Services.Interfaces;

namespace FINALPROJECT.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepo _userRepo;
        private readonly IAuctionRepo _auctionRepo;
        public CustomerService(ICustomerRepo customerRepo,  IHttpContextAccessor contextAccessor, IUserRepo userRepo,IAuctionRepo auctionRepo)
        {
            _customerRepo = customerRepo;
            _contextAccessor = contextAccessor;
            _userRepo = userRepo;
            _auctionRepo = auctionRepo;
        }

        public async Task<BaseResponse<CustomerResponseModel>> Create(CustomerRequestModel request)
        {
            var check = await _customerRepo.Exist(a => a.Email == request.Email);
            if (check)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Status = false,
                    Data = null,
                    Message = "Customer with Email Already Exist"
                };
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.PassWord, salt);
            var user = new User
            {
                Email = request.Email,
                Name = request.FirstName + request.LastName,
                PasswordHash = hashedPassword,
                Salt = salt,
                Role = Domain.Enums.Roles.Customer,
            };
            await _userRepo.Add(user);
            await _userRepo.SaveAsync();
            var customer = new Customer
            {
                Salt = salt,
                Email = user.Email,
                FirstName = request.FirstName,
                Gender = request.Gender,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = hashedPassword,
                User = user,
                UserId = user.Id,
            };
           await _customerRepo.Add(customer);
           await _customerRepo.SaveAsync();
            return new BaseResponse<CustomerResponseModel>
            {
                Status = true,
                Data = new CustomerResponseModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    Email = customer.Email,
                    Gender = customer.Gender,
                    LastName = customer.LastName,
                    PhoneNumber= customer.PhoneNumber,
                }
            };
        }

        public async Task<BaseResponse<CustomerResponseModel>> DeleteCustomer(string id)
        {
            var customer = await _customerRepo.Get(x => x.Id == id);
            if (customer == null)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            customer.IsDeleted = true;
            customer.User.IsDeleted = true;
           await  _customerRepo.Update(customer);
            await _userRepo.Update(customer.User);
            await _customerRepo.SaveAsync();
            await _userRepo.SaveAsync();
            return new BaseResponse<CustomerResponseModel>
            {
                Status = true,
               Message = "Customer has been deleted"
            };
        }

        public async Task<BaseResponse<ICollection<CustomerResponseModel>>> GetAllCustomers()
        {
            var customers = await _customerRepo.GetAll();
            if (customers == null)
            {
                return new BaseResponse<ICollection<CustomerResponseModel>>
                {
                    Status = false,
                    Message = "No Customers yet"
                };
            }
            return new BaseResponse<ICollection<CustomerResponseModel>>
            {
                Status = true,
                Data = customers.Select(customer => new CustomerResponseModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    Email = customer.Email,
                    Gender = customer.Gender,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                }).ToList(),
            };
        }

        public async Task<Customer> GetCurrentCustomer()
        {
            var userId = _contextAccessor.HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var customer = await _customerRepo.Get(a => a.UserId == userId);
            return customer;
        }

        public async Task<BaseResponse<CustomerResponseModel>> GetCustomer(string Id)
        {
            var customer = await _customerRepo.Get(x => x.Id == Id);
            if (customer == null)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            return new BaseResponse<CustomerResponseModel>
            {
                Status = true,
                Data = new CustomerResponseModel
                {
                    Id = customer.Id,
                     FirstName = customer.FirstName,
                    Email = customer.Email,
                    Gender = customer.Gender,
                    LastName = customer.LastName,
                    PhoneNumber= customer.PhoneNumber,
                }
            };
        }
        public async Task<BaseResponse<CustomerResponseModel>> GetProfile()
        {
            var customer = await GetCurrentCustomer();
            if (customer == null)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            return new BaseResponse<CustomerResponseModel>
            {
                Status = true,
                Data = new CustomerResponseModel
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    Email = customer.Email,
                    Gender = customer.Gender,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                }
            };
        }
        public async Task<BaseResponse<CustomerRequestModel>> GetCustomerUpdate(string Id)
        {
            var customer = await _customerRepo.Get(x => x.Id == Id);
            if (customer == null)
            {
                return new BaseResponse<CustomerRequestModel>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            return new BaseResponse<CustomerRequestModel>
            {
                Status = true,
                Data = new CustomerRequestModel
                {
                    FirstName = customer.FirstName,
                    Email = customer.Email,
                    Gender = customer.Gender,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                }
            };
        }

        public async Task<BaseResponse<CustomerResponseModel>> UpdateCustomer(string id, UpdateCustomerRequest request)
        {
            var customer = await _customerRepo.Get(x => x.Id == id);
            if (customer == null)
            {
                return new BaseResponse<CustomerResponseModel>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            var user = await _userRepo.Get(x => x.Id == customer.UserId);
            customer.FirstName = request.FirstName ?? customer.FirstName;
            customer.PhoneNumber = request.PhoneNumber ?? customer.PhoneNumber;
            customer.LastName = request.LastName ?? customer.LastName;
            user.Name = request.FirstName + request.LastName ?? user.Name;
            return new BaseResponse<CustomerResponseModel>
            {
                Data = new CustomerResponseModel
                {
                       FirstName = customer.FirstName,
                    Email = customer.Email,
                    Gender = customer.Gender,
                    LastName = customer.LastName,
                    PhoneNumber= customer.PhoneNumber,
                },
            };
            
        }
        public async Task<BaseResponse<ICollection<AddressResponseModel>>> ViewAddresses()
        {
            var customer = await GetCurrentCustomer();
            if (customer == null)
            {
                return new BaseResponse<ICollection<AddressResponseModel>>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            var addresses = customer.Addresses;
            if (addresses == null)
            {
                return new BaseResponse<ICollection<AddressResponseModel>>
                {
                    Status = false,
                    Message = "You have no Addresses"
                };
            }
            return new BaseResponse<ICollection<AddressResponseModel>>
            {
                Status = true,
                Data = addresses.Select(x => new AddressResponseModel
                {
                    City = x.City,
                    Number = x.Number,
                    PostalCode = x.PostalCode,
                    State = x.State,
                    Street = x.Street
                }).ToList(),
            };
        }
        public async Task<BaseResponse<ICollection<PaymentResponseModel>>> ViewPayments()
        {
            var customer = await GetCurrentCustomer();
            if (customer == null)
            {
                return new BaseResponse<ICollection<PaymentResponseModel>>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            var payments = customer.PaymentsMade;
            if (payments == null)
            {
                return new BaseResponse<ICollection<PaymentResponseModel>>
                {
                    Status = false,
                    Message = "You have no Payments Made"
                };
            }
            return new BaseResponse<ICollection<PaymentResponseModel>>
            {
                Status = true,
                Data = payments.Select(x => new PaymentResponseModel
                {
                  Amount = x.Amount,
                  AuctionId = x.AuctionId,
                  CustomerId = x.CustomerId,
                  Date = x.DateCreated,
                  ReferenceID = x.ReferenceID,
                  Status = x.Status,
                }).ToList(),
            };
        }
        public async Task<BaseResponse<ICollection<AuctionResponseModel>>> ViewAuctions()
        {
            var customer = await GetCurrentCustomer();
            if (customer == null)
            {
                return new BaseResponse<ICollection<AuctionResponseModel>>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            var Auctions = customer.AuctionsPartaken;
            if (Auctions == null)
            {
                return new BaseResponse<ICollection<AuctionResponseModel>>
                {
                    Status = false,
                    Message = "You have no Auctions Partaken"
                };
            }
            return new BaseResponse<ICollection<AuctionResponseModel>>
            {
                Status = true,
                Data = Auctions.Select(x => new AuctionResponseModel
                {
                   Name = x.Name,
                  AuctionEndDate = x.AuctionEndDate,
                  AuctionStartDate = x.AuctionStartDate,
                  CarId = x.CarId,
                  Description = x.Description,
                  ImageURL = x.ImageURL,
                  //Status = x.Status,
                }).ToList(),
            };
        }
        public async Task<BaseResponse<ICollection<AuctionResponseModel>>> ViewOutstandingPayments()
        {
            var customer = await GetCurrentCustomer();
            if (customer == null)
            {
                return new BaseResponse<ICollection<AuctionResponseModel>>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            var Auctions = customer.OutstandingPayments;
            if (Auctions == null)
            {
                return new BaseResponse<ICollection<AuctionResponseModel>>
                {
                    Status = false,
                    Message = "You have no Outstandings "
                };
            }
            return new BaseResponse<ICollection<AuctionResponseModel>>
            {
                Status = true,
                Data = Auctions.Select(x => new AuctionResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    AuctionEndDate = x.AuctionEndDate,
                    AuctionStartDate = x.AuctionStartDate,
                    CarId = x.CarId,
                    Description = x.Description,
                    ImageURL = x.ImageURL,
                    CurrentPrice = x.CurrentPrice,
                }).ToList(),
            };
        }
        public async Task<BaseResponse<ICollection<ShippingResponseModel>>> ViewShipping(string id)
        {
            var customer = await GetCurrentCustomer();
            if (customer == null)
            {
                return new BaseResponse<ICollection<ShippingResponseModel>>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            var shipping = customer.Shippings;
            if (shipping == null)
            {
                return new BaseResponse<ICollection<ShippingResponseModel>>
                {
                    Status = false,
                    Message = "You have no Shipping Made"
                };
            }
            return new BaseResponse<ICollection<ShippingResponseModel>>
            {
                Status = true,
                Data = shipping.Select(x => new ShippingResponseModel
                {
                     Address = new AddressResponseModel
                    {
                        City = x.Address.City,
                        Number = x.Address.Number,
                        PostalCode = x.Address.PostalCode,
                        State = x.Address.State,
                        Street = x.Address.Street,
                    },
                     CustomerId = x.CustomerId,
                     PaymentReference = x.PaymentReference,
                     TrackingNumber = x.TrackingNumber

                }).ToList(),
            };
        }
        public async Task<BaseResponse<ICollection<BidResponseModel>>> ViewBids()
        {
            var customer = await GetCurrentCustomer();
            if (customer == null)
            {
                return new BaseResponse<ICollection<BidResponseModel>>
                {
                    Status = false,
                    Message = "Customer not found",
                };
            }
            var bids = customer.BidsMade;
            if (bids == null)
            {
                return new BaseResponse<ICollection<BidResponseModel>>
                {
                    Status = false,
                    Message = "You have no Bids Made"
                };
            }
            return new BaseResponse<ICollection<BidResponseModel>>
            {
                Status = true,
                Data = bids.Select(x => new BidResponseModel
                {
                    Amount = x.Amount,
                    AuctionId = x.AuctionId,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.FirstName,
                }).ToList(),
            };
        }
        public async Task<BaseResponse<CustomerResponseModel>> CancelOutstanding(string id)
        {
            var customer = await GetCurrentCustomer();
            var auction = await _auctionRepo.Get(x => x.Id == id);
            customer.OutstandingPayments.Remove(auction);
            return new BaseResponse<CustomerResponseModel>
            {
                Status = true,
                Message = "Payment Canceled"
            };
        }
    }
}
