using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;

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
    }
}
