using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Contracts;

namespace PetConnect.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentQueryService _queryService;
        public PaymentController(IPaymentQueryService queryService)
        {
            _queryService = queryService;
        }
        public async Task<IActionResult> Index()
        {
            var PaymentList = await _queryService.GetAllAsync();
            return View(PaymentList);
        }
    }
}
