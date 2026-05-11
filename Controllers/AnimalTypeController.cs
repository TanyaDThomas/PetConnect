using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.ViewModels;

namespace PetConnect.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AnimalTypeController : Controller
    {
        private readonly IAnimalTypeQueryService _queryService;
        private readonly IAnimalTypeService _typeService;
        public AnimalTypeController(IAnimalTypeQueryService queryService, IAnimalTypeService typeService)
        {
            _queryService = queryService;
            _typeService = typeService;
        }
        public async Task<IActionResult> Index()
        {
            var animalTypeList = await _queryService.GetAllAsync();
            return View(animalTypeList);
        }

        //GET Create AnimalType
        public IActionResult Create()
        {
            return View();
        }

        //POST Create AnimalType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnimalType animalType)
        {
            if(ModelState.IsValid)
            {
                var success = await _typeService.CreateAsync(animalType);
                if (!success) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(animalType);
        }

        //GET Manage Attributes
        public async Task<IActionResult> ManageAttributes(int Id)
        {
            var viewModel = await _queryService.BuildManageAttributesModelAsync(Id);
            if (viewModel == null) return NotFound();

            return View(viewModel);

        }

        //POST Manage Attributes

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageAttributes(ManageAnimalTypeAttributesVM viewModel)
        {
            if (!ModelState.IsValid)
            {
           
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                throw new Exception("ModelState invalid: " + string.Join(", ", errors));
            }

            var success = await _typeService.UpdateAttributesAsync(viewModel);

            if (!success)
            {
                throw new Exception("UpdateAttributesAsync failed");
            }

            return RedirectToAction(nameof(Index));
        }

     


        //GET VIEW Attributes
        public async Task<IActionResult> ViewAttributes(int id)
        {
            var viewModel = await _queryService.BuildManageAttributesModelAsync(id);
            if (viewModel == null) return NotFound();

            return View(viewModel);
        }



        //POST Delete AnimalType
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _typeService.DeleteAsync(id);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }
       
    }
}
