using System;
using FINALPROJECT.Domain.Entities;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Repositories.Implementations;
using FINALPROJECT.Repositories.Interfaces;
using FINALPROJECT.Services.Interfaces;

namespace FINALPROJECT.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepo _carRepo;
        private readonly IFileRepo _fileRepo;
        public CarService(ICarRepo carRepo,IFileRepo fileRepo)
        {
            _carRepo = carRepo;
            _fileRepo = fileRepo;
        }

        public async Task<BaseResponse<CarResponseModel>> Create(CarRequestModel request)
        {
            var img = await _fileRepo.UploadAsync(request.Image);

            var car = new Car
            {
                Name = request.Name,
                Brand = request.Brand,
                ChasisNumber = request.ChasisNumber,
                Color = request.Color,
                Model = request.Model,
                ImageUrl = img,
                Status = Domain.Enums.CarStatus.Available,
                Auctions = new List<Auction>()
            };
              await _carRepo.Add(car);
            await _carRepo.SaveAsync();
            return new BaseResponse<CarResponseModel>
            {
                //Data =
                //{
                //    Id = car.Id,
                //    Brand = car.Brand,
                //    ChasisNumber = car.ChasisNumber,
                //    Color = car.Color,
                //    Model = car.Model,
                //    Name = car.Name,
                //    Status = car.Status,
                //    ImageUrl = car.ImageUrl,
                //}
                Message = "Successfully Created",
                Status = true,
            };
        }

        public async Task<BaseResponse<CarResponseModel>> DeleteCar(string id)
        {
            var car = await _carRepo.Get(x => x.Id == id);
            car.IsDeleted = true;
           await  _carRepo.Update(car);
            await _carRepo.SaveAsync();
            return new BaseResponse<CarResponseModel>
            {
                Status = true,
                Message = "car Deleted Succesfully",
                Data = null,
            };
        }

        public async Task<BaseResponse<ICollection<CarResponseModel>>> GetAllCars()
        {
            var cars = await _carRepo.GetAll();
            if (cars == null)
            {
                return new BaseResponse<ICollection<CarResponseModel>>
                {
                    Message = "There are no Cars Available",
                    Status = false,
                };
            }
            return new BaseResponse<ICollection<CarResponseModel>>
            {
                Status = true,
                Data = cars.Select(a => new CarResponseModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Status = a.Status,
                    Brand = a.Brand,
                    ImageUrl = a.ImageUrl,
                    Color = a.Color,
                    ChasisNumber = a.ChasisNumber,
                    Model = a.Model,
                }).ToList(),
            };
        }

        public async Task<BaseResponse<CarResponseModel>> GetCarById(string Id)
        {
            var car = await _carRepo.Get(x => x.Id == Id);
            return new BaseResponse<CarResponseModel>
            {
                Status = true,
                Data = new CarResponseModel
                {
                    Brand = car.Brand,
                    ChasisNumber = car.ChasisNumber,
                    Color = car.Color,
                    ImageUrl = car.ImageUrl,
                    Model = car.Model,
                    Name = car.Name,
                    Status = car.Status

                }
            };
        }
        public async Task<BaseResponse<CarRequestModel>> GetCarByIdUpdate(string Id)
        {
            var car = await _carRepo.Get(x => x.Id == Id);
            return new BaseResponse<CarRequestModel>
            {
                Status = true,
                Data = new CarRequestModel
                {
                    Brand = car.Brand,
                    ChasisNumber = car.ChasisNumber,
                    Color = car.Color,
                    Model = car.Model,
                    Name = car.Name,

                }
            };
        }

        public async Task<BaseResponse<ICollection<CarResponseModel>>> GetCarByName(string input)
        {
            var cars = await _carRepo.GetAllSpecified(input);
            if (cars == null)
            {
                return new BaseResponse<ICollection<CarResponseModel>>
                {
                    Message = "There are no Cars Available",
                    Status = false,
                };
            }
            return new BaseResponse<ICollection<CarResponseModel>>
            {
                Status = true,
                Data = cars.Select(a => new CarResponseModel
                {
                    Name = a.Name,
                    Status = a.Status,
                    Brand = a.Brand,
                    ImageUrl = a.ImageUrl,
                    Color = a.Color,
                    ChasisNumber = a.ChasisNumber,
                    Model = a.Model,
                }).ToList(),
            };
        }

        public async Task<BaseResponse<CarResponseModel>> UpdateCar(string id, CarRequestModel request)
        {
            var car = await _carRepo.Get(x => x.Id == id);
            if (car == null)
            {
                return new BaseResponse<CarResponseModel>
                {
                    Message = "Car Not Found!",
                    Status = false
                };
            }
            if (request.Image != null)
            {
                car.Name = request.Name ?? car.Name;
                car.Brand = request.Brand ?? car.Brand;
                car.ChasisNumber = request.ChasisNumber ?? car.ChasisNumber;
                car.Color = request.Color ?? car.Color;
                car.Model = request.Model ?? car.Model;
                var img = await _fileRepo.UploadAsync(request.Image);
                car.ImageUrl = img ?? car.ImageUrl;
             await   _carRepo.Update(car);
              await  _carRepo.SaveAsync();
                return new BaseResponse<CarResponseModel>
                {
                    Message = "Car Updated Successfully!",
                    Status = true
                };
            }
            car.Name = request.Name ?? car.Name;
            car.Brand = request.Brand ?? car.Brand;
            car.ChasisNumber = request.ChasisNumber ?? car.ChasisNumber;
            car.Color = request.Color ?? car.Color;
            car.Model = request.Model ?? car.Model;
            await _carRepo.Update(car);
            await _carRepo.SaveAsync();
            return new BaseResponse<CarResponseModel>
            {
                Message = "Car Updated Successfully!",
                Status = true
            };
        }
    }
}
