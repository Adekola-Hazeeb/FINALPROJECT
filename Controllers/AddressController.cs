using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FINALPROJECT.Controllers
{

    public class AddressController : Controller
    {
        private readonly IAddresssService _addressService;

        public AddressController(IAddresssService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _addressService.GetAllAddresss();
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _addressService.GetAddress(id);
            if (!response.Status)
            {
                return NotFound();
            }

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Street,City,State,PostalCode")] AddressRequestModel request)
        {
            if (ModelState.IsValid)
            {
                var response = await _addressService.Create(request);
                if (response.Status)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            return View(request);
        }

       [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _addressService.GetAddress(id);
            if (!response.Status)
            {
                return NotFound();
            }

            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Number,Street,City,State,PostalCode")] AddressRequestModel request)
        {
            if (ModelState.IsValid)
            {
                var response = await _addressService.UpdateAddress(id, request);
                if (response.Status)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _addressService.GetAddress(id);
            if (!response.Status)
            {
                return NotFound();
            }

            return View(response.Data);
        }

        [HttpGet]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var response = await _addressService.DeleteAddress(id);
            if (response.Status)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, response.Message);
            return View();
        }
    }
}
