using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Contracts;
using PetConnect.Infrastructure.Identity;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentQueryService _queryService;
        private readonly IAdoptionService _adoptionService;
        private readonly IPaymentService _paymentService;
        private readonly UserManager<AppUser> _userManager;
        public PaymentController(IPaymentQueryService queryService, IAdoptionService adoptionService, IPaymentService paymentService, UserManager<AppUser> userManager)
        {
            _queryService = queryService;
            _adoptionService = adoptionService;
            _paymentService = paymentService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var PaymentList = await _queryService.GetPaymentListAsync();
            return View(PaymentList);
        }

        //GET DETAILS PAYMENT
        public async Task<IActionResult> Details(int id)
        {
            var payment = await _queryService.GetDetailsAsync(id);
            return View(payment);
        }


        //GET CREATE PAYMENT

        public async Task<IActionResult> Create(int? adoptionId)
        {
            var viewModel = await _queryService.BuildCreateModelAsync(adoptionId);

            return View(viewModel);
        }



        //POST CREATE PAYMENT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var success = await _paymentService.CreateAsync(viewModel, userId);

            if (!success)
            {
                ModelState.AddModelError("", "Payment failed.");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }


        //GET UPDATE PAYMENT
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var viewModel = await _queryService.GetPaymentUpdateAsync(id);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }
    

        //POST UPDATE PAYMENT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PaymentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var userId = _userManager.GetUserId(User);
            if(userId == null) return Unauthorized();

            var success = await _paymentService.UpdateAsync(viewModel, userId);

            if (!success)
            {
                ModelState.AddModelError("", "Update failed.");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));

         
        }

        //POST DEACTIVATE PAYMENT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var success = await _paymentService.DeactivateAsync(id, userId);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
