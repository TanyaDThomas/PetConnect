using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetConnect.Application.Services;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;

namespace PetConnect.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AttributeDefinitionController : Controller
    {
        private readonly IAttributeDefinitionQueryService _queryService;
        private readonly IAttributeDefinitionService _attributeService;
        public AttributeDefinitionController(IAttributeDefinitionQueryService queryService, IAttributeDefinitionService attributeService)
        {
            _queryService = queryService;
            _attributeService = attributeService;
        }
        public async Task<IActionResult> Index()
        {
            var attributes = await _queryService.GetAllAsync();
            return View(attributes);
        }

        // GET CREATE ATTRIBUTE
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE ATTRIBUTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttributeDefinition attributeDefinition)
        {
            if (ModelState.IsValid)
            {
                var success = await _attributeService.CreateAsync(attributeDefinition);
                if (!success) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(attributeDefinition);
        }

        //GET UPDATE ATTRIBUTE
        public async Task<IActionResult> Update(int id)
        {
            var attribute = await _queryService.GetByIdAsync(id);
            if(attribute == null) return NotFound();
            return View(attribute);
        }

        //POST UPDATE ATTRIBUTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AttributeDefinition attributeDefinition)
        {
            if(!ModelState.IsValid)
            {
                return View(attributeDefinition);
            }

            var success = await _attributeService.UpdateAsync(attributeDefinition);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        //POST DELETE ATTRIBUTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _attributeService.DeleteAsync(id);
            if(!success ) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
