using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FINALPROJECT.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCars();
            return View(cars.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var car = await _carService.GetCarById(id);
            return View(car.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarRequestModel request)
        {
            var result = await _carService.Create(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var car = await _carService.GetCarByIdUpdate(id);
            return View(car.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CarRequestModel request)
        {
            var result = await _carService.UpdateCar(id, request);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
