using System.Security.Claims;
using FINALPROJECT.Domain.Models.RequestModel;
using FINALPROJECT.Services.Implementations;
using FINALPROJECT.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FINALPROJECT.Controllers
{
    public class BidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly IAuctionService _auctionService;
        public BidController(IBidService bidService, IAuctionService auctionService)
        {
            _bidService = bidService;
            _auctionService = auctionService;
        }
        [HttpGet]
        public async Task<IActionResult> Create(string auctionId)
        {
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( BidRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var bid = await _bidService.Create(model);
                if (!bid.Status)
                {
                    TempData["Message"] = "Bid not Successful";
                    return View("Index","Auction");
                }
                TempData["Message"] = "Bid Submitted Succesfully";

                return RedirectToAction("GetAll", "Auction");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var response = await _bidService.GetBid(id);
            if (!response.Status)
            {
                return NotFound();
            }
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _bidService.GetAllBids();
            return View(response.Data);
        }
    }
}

