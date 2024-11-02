using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FINALPROJECT.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var response = await _customerService.GetCustomer(id);
            if (!response.Status) return NotFound(response.Message);

            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {

           var response = await _customerService.GetProfile();
            if (!response.Status) return NotFound(response.Message);

            return View(response.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerService.Create(model);
                if (response.Status)
                {
                    return RedirectToAction("Login","User");
                }
                ViewBag.Message = response.Message;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var response = await _customerService.GetCustomerUpdate(id);
            if (!response.Status) return NotFound(response.Message);

            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CustomerRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _customerService.UpdateCustomer(id, new UpdateCustomerRequest
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                });

                if (response.Status)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = response.Message;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var response = await _customerService.GetCustomer(id);
            if (!response.Status) return NotFound(response.Message);

            return View(response.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var response = await _customerService.DeleteCustomer(id);
            if (response.Status)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Message = response.Message;
            return View();
        }
        public async Task<IActionResult> ViewAddresses(string id)
        {
            var response = await _customerService.ViewAddresses();
            if (!response.Status) return NotFound(response.Message);

            return View(response.Data);
        }

        public async Task<IActionResult> ViewPayments()
        {
            var response = await _customerService.ViewPayments();
            if (!response.Status) return NotFound(response.Message);

            return View(response.Data);
        }

        public async Task<IActionResult> ViewAuctions(string id)
        {
            var response = await _customerService.ViewAuctions();
            if (!response.Status) return NotFound(response.Message);

            return View(response.Data);
        }

        public async Task<IActionResult> OutstandingPayments(string id)
        {
            var response = await _customerService.ViewOutstandingPayments();
            if (!response.Status) return NotFound(response.Message);

            return View(response.Data);
        }

        public async Task<IActionResult> Bids(string id)
        {
            var response = await _customerService.ViewBids();
            if (!response.Status) return NotFound(response.Message);

            return View(response.Data);
        }
    }
}

