using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Contracts;

namespace PetConnect.Controllers
{
    public class WarningController : Controller
    {
        private readonly IWarningQueryService _queryService;
        public WarningController(IWarningQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<IActionResult> Index()
        {
            var WarningList = await _queryService.GetAllAsync();
            return View(WarningList);
        }
    }
}
