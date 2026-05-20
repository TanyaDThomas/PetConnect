using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    public class DashboardController : Controller
    {
        private readonly PetConnectDbContext _context;
        public DashboardController(PetConnectDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var viewModel = new DashboardViewModel
            {
                TotalAnimals = _context.Animals.Count(),
                Adoptions = _context.Adoptions.Count(),
                PendingAdoptions = _context.Adoptions.Count(a => a.Status == AdoptionStatus.Pending),
                TotalRevenue = _context.Payments.Sum(p => (decimal?)p.Amount) ?? 0,

            };

            // RIGHT ANIMAL TYPE GRAPH
            viewModel.SpeciesLabels = _context.Animals
                .GroupBy(a => a.AnimalType.Name)
                .Select(g => g.Key)
                .ToList();

            viewModel.SpeciesData = _context.Animals
                .GroupBy(a => a.AnimalType.Name)
                .Select(g => g.Count())
                .ToList();

            // LEFT ANIMAL TRENDS GRAPH
            viewModel.AnimalTrendLabels = _context.Animals
                .GroupBy(a => a.CreatedOn.Month)
                .OrderBy(g => g.Key)
                .Select(g => System.Globalization.CultureInfo.CurrentCulture
                    .DateTimeFormat.GetAbbreviatedMonthName(g.Key))
                .ToList();

            viewModel.AnimalTrendData = _context.Animals
                .GroupBy(a => a.CreatedOn.Month)
                .OrderBy(g => g.Key)
                .Select(g => g.Count())
                .ToList();


            return View(viewModel);
        }
    }
}
