using FINALPROJECT.Domain.Models.ResponseModel;
using FINALPROJECT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FINALPROJECT.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var response = await _paymentService.GetPayment(id);
            if (response.Status)
            {
                return View(response.Data);
            }
            return NotFound(response.Message);
        }

        [HttpGet]
        public async Task<IActionResult> MakePayment(string auctionId)
        {
            var response = await _paymentService.MakePayment(auctionId);
            if (response.Status)
            {
                return Redirect(response.Data.ReferenceID); 
            }
            return BadRequest(response.Message);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _paymentService.GetAllPayments();
            if (response.Status)
            {
                return View(response.Data);
            }
            return NotFound(response.Message);
        }
    }
}

