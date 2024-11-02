using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Repositories.Implementations;
using FINALPROJECT.Repositories.Interfaces;
using FINALPROJECT.Services.Interfaces;

namespace FINALPROJECT.Services.Implementations
{
    public class AddressService : IAddresssService
    {
        private readonly IAddressRepo _addressRepo;
        private readonly ICustomerService _customerService;
        public AddressService(IAddressRepo addressRepo, ICustomerService customerService)
        {
            _addressRepo = addressRepo;
            _customerService = customerService;
        }
        public async Task<BaseResponse<AddressResponseModel>> Create(AddressRequestModel request)
        {
            var customer = await _customerService.GetCurrentCustomer();
            var address = new Address()
            {
                City = request.City,
                Street = request.Street,
                PostalCode = request.PostalCode,
                Number = request.Number,
                State = request.State,
                CustomerId = customer.Id,
                DateCreated = DateTime.UtcNow,
            };
           var check = await _addressRepo.Exist(x => x.CustomerId == address.CustomerId && x.Street == address.Street);
            if (check)
            {
                return new BaseResponse<AddressResponseModel>
                {
                    Status = false,
                    Message = "Address already exists",
                    Data = null,
                };
            }
            await _addressRepo.AddAsync(address);
            customer.Addresses.Add(address);
            await _addressRepo.SaveAsync();
            return new BaseResponse<AddressResponseModel>
            {
                Status = true,
                Message = "Address Added Sucessfully",
                Data = new AddressResponseModel
                {
                    City = address.City,
                    Number = address.Number,
                    State = address.State,
                    PostalCode = address.PostalCode,
                    Street = address.Street,
                }
            };

        }




        public async Task<BaseResponse<AddressResponseModel>> DeleteAddress(string id)
        {
            var address = await _addressRepo.Get(x => x.Id == id);
            address.IsDeleted = true;
            await _addressRepo.Update(address);
            return new BaseResponse<AddressResponseModel>
            {
                Status = true,
                Message = "Address Deleted Succesfully",
                Data = null,
            };
        }

        public async Task<BaseResponse<AddressResponseModel>> GetAddress(string id)
        {
            var address = await _addressRepo.Get(x => x.Id == id);
            return new BaseResponse<AddressResponseModel>
            {
                Status = true,
                Data = new AddressResponseModel
                {
                    Id = address.Id,
                    City = address.City,
                    Number = address.Number,
                    State = address.State,
                    PostalCode = address.PostalCode,
                    Street = address.Street,
                 }
            };

        }

        public async Task<BaseResponse<ICollection<AddressResponseModel>>> GetAllAddresss()
        {
            var addresses = await _addressRepo.GetAllAsync();
            if (addresses == null)
            {
                return new BaseResponse<ICollection<AddressResponseModel>> 
                {
                    Message = "You have no Address",
                    Status = false,
                };
            }
            return new BaseResponse<ICollection<AddressResponseModel>>
            {
                Message = "All Addresses",
                Data = addresses.Select(a => new AddressResponseModel
                {
                    Id = a.Id,
                    City = a.City,
                    Number=a.Number,
                    PostalCode = a.PostalCode,
                    State = a.State,
                    Street = a.Street,
                }).ToList(),
                Status = true,
            };
        }

        public async Task<BaseResponse<AddressResponseModel>> UpdateAddress(string id ,AddressRequestModel request)
        {
            var address = await _addressRepo.Get(x => x.Id == id);
            if (address == null)
            {
                return new BaseResponse<AddressResponseModel>
                {
                    Message = "Address Not Found!",
                    Status = false
                };
            }
            address.Street = request.Street ?? address.Street;
            address.City = request.City ?? address.City;
            address.Number = request.Number ?? address.Number;
            address.PostalCode = request.PostalCode ?? address.PostalCode;
            address.State = request.State ?? address.State; 
           await  _addressRepo.Update(address);
            await _addressRepo.SaveAsync();
            return new BaseResponse<AddressResponseModel>
            {
                Message = "Address Updated Successfully!",
                Status = true
            };
        }
    }
}


