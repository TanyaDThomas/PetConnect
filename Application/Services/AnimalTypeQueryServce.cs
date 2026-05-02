using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;

namespace PetConnect.Application.Services
{
    public class AnimalTypeQueryServce : IAnimalTypeQueryService
    {
        private readonly PetConnectDbContext _context;
        public AnimalTypeQueryServce(PetConnectDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AnimalType>> GetAllAsync()
        {
            return await _context.AnimalTypes
                .AsNoTracking()
                .ToListAsync();
        }

        //GET ANIMALTYPE
        public async Task<IEnumerable<SelectListItem>> GetSelectListItemsAsync()
        {
            return await _context.AnimalTypes
                .AsNoTracking()
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToListAsync();
        }

        //GET MANAGE ATTRIBUTES
        public async Task<ManageAnimalTypeAttributesVM?> BuildManageAttributesModelAsync(int animalTypeId)
        {
            var animalType = await _context.AnimalTypes
                .Include(a => a.AnimalTypeAttributes)
                .FirstOrDefaultAsync(a => a.Id == animalTypeId);

            if (animalType == null)
                return null;

            var allAttributes = await _context.AttributeDefinitions.ToListAsync();

            return new ManageAnimalTypeAttributesVM
            {
                AnimalTypeId = animalTypeId,
                AnimalTypeName = animalType.Name,

                Attributes = allAttributes.Select(ac => new AttributeCheckboxVM
                {
                    AttributeDefinitionId = ac.Id,
                    Name = ac.Name,
                    IsSelected = animalType.AnimalTypeAttributes.Any(at => at.AttributeDefinitionId == ac.Id)
                }).ToList()
            };
        }
    }
}
