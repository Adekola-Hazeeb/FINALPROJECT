using System.Security.Claims;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PayStack.Net;

namespace FINALPROJECT.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuctionController(IAuctionService auctionService, IHttpContextAccessor httpContextAccessor)
        {
            _auctionService = auctionService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if(User.FindFirst(ClaimTypes.NameIdentifier).Value == null)

            {

            }
            var response = await _auctionService.GetAllAuctions();
            if (!response.Status)
            {
                ViewBag.Message = response.Message;
            }
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == null)

            {

            }
            var response = await _auctionService.GetAllAuctions();
            if (!response.Status)
            {
                ViewBag.Message = response.Message;
            }
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var response = await _auctionService.GetBids(id);
            if (!response.Status)
            {
                return NotFound(response.Message);
            }
            return View(response.Data);
        }

        [HttpGet]
        public  IActionResult Create(string CarId)
        {
          var model = new AuctionRequestModel{CarId = CarId};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionRequestModel model)
        {
            //var carsId = ( _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name));
            if (ModelState.IsValid)
            {
                var response = await _auctionService.Create(model);
                if (response.Status)
                {
                    return RedirectToAction("Index","Car");
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _auctionService.GetAuctionUpdate(id);
            if (!response.Status)
            {
                return NotFound(response.Message);
            }
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, AuctionRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _auctionService.UpdateAuction(id, model);
                if (response.Status)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, response.Message);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _auctionService.GetAuction(id);
            if (response.Status)
            {
                return View(response.Data);
            }
            return View("Error", new { message = response.Message });
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var response = await _auctionService.DeleteAuction(id);
            if (response.Status)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
