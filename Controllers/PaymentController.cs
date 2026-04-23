using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetConnect.Application.Services;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentQueryService _queryService;
        private readonly IAdoptionService _adoptionService;
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentQueryService queryService, IAdoptionService adoptionService, IPaymentService paymentService)
        {
            _queryService = queryService;
            _adoptionService = adoptionService;
            _paymentService = paymentService;
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

            var success = await _paymentService.CreateAsync(viewModel);

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

            var success = await _paymentService.UpdateAsync(viewModel);

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
            var success = await _paymentService.DeactivateAsync(id);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
