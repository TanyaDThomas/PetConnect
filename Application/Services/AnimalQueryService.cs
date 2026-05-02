
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;


namespace PetConnect.Application.Services
{
    public class AnimalQueryService : IAnimalQueryService
    {
        private readonly PetConnectDbContext _context;
        
        public AnimalQueryService(PetConnectDbContext context)
        {
            _context = context;
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - dob.Year;

            if (dob.Date > today.AddYears(-age))
                age--;

            return age;
        }


        public async Task<decimal?> GetAnimalFeeAsync(int animalId)
        {
            return await _context.Animals
                .Where(a => a.Id == animalId)
                .Select(a => a.AdoptionFee)
                .FirstOrDefaultAsync();
        }


        // GET LIST FOR INDEX

        public async Task<AnimalIndexViewModel> GetAnimalListAsync(AnimalSearchFilter filter)
        {
            var baseQuery = _context.Animals
                .AsNoTracking()
                .Include(a => a.Shelter)
                .Include(a => a.AnimalType)
                .Where(a => a.IsActive);

            var totalCount = await baseQuery.CountAsync();

            var filteredQuery = baseQuery;

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                filteredQuery = filteredQuery.Where(a =>
                    a.Name.Contains(filter.Name) ||
                    a.Breed.Contains(filter.Name));
            }

            if (filter.AnimalTypeId.HasValue)
            {
                filteredQuery = filteredQuery.Where(a =>
                    a.AnimalTypeId == filter.AnimalTypeId.Value);
            }

            if (filter.IsAvailable.HasValue)
            {
                filteredQuery = filteredQuery.Where(a =>
                    a.IsAdopted == !filter.IsAvailable.Value);
            }

            if (filter.MinAge.HasValue)
            {
                var maxDob = DateTime.Today.AddYears(-filter.MinAge.Value);
                filteredQuery = filteredQuery.Where(a =>
                    a.DateOfBirth.HasValue && a.DateOfBirth <= maxDob);
            }

            if (filter.MaxAge.HasValue)
            {
                var minDob = DateTime.Today.AddYears(-filter.MaxAge.Value);
                filteredQuery = filteredQuery.Where(a =>
                    a.DateOfBirth.HasValue && a.DateOfBirth >= minDob);
            }

            
            var animalTypes = await _context.AnimalTypes
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .ToListAsync();

            var animals = await filteredQuery.ToListAsync();

            return new AnimalIndexViewModel
            {
                TotalCount = totalCount,
                FilteredCount = animals.Count,

                Animals = animals.Select(a => new AnimalListViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    AnimalTypeName = a.AnimalType.Name,
                    Breed = a.Breed,
                    IsAdopted = a.IsAdopted,
                    ShelterName = a.Shelter.Name
                }).ToList(),

                
                AnimalTypes = animalTypes,
                Filter = filter
            };
        }



        //GET DETAILS
        public async Task<AnimalViewModel?> GetAnimalDetailsAsync(int id)
        {
            var animal = await _context.Animals
                    .AsNoTracking()
                    .AsSplitQuery()
                    .Include(a => a.Shelter)
                    .Include(a => a.AnimalAttributes)
                        .ThenInclude(aa => aa.AttributeDefinition)
                    .Where(a => a.IsActive)
                    .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null) return null;

            var notes = await _context.Notes
                .AsNoTracking()
                .Where(n => n.EntityType == NoteEntityType.Animal
                         && n.EntityId == id)
                .OrderByDescending(n => n.CreatedOn)
                .Take(5)
                .ToListAsync();

            return new AnimalViewModel
            {
                Id = animal.Id,

                ShelterId = animal.ShelterId,
                AnimalTypeId = animal.AnimalTypeId,

                ShelterName = animal.Shelter?.Name ?? "",
                AnimalTypeName = animal.AnimalType?.Name ?? "",

                Name = animal.Name,
                DateOfBirth = animal.DateOfBirth,
                Age = animal.DateOfBirth.HasValue ? CalculateAge(animal.DateOfBirth.Value) : 0,
                Color = animal.Color,
                AdoptionFee = animal.AdoptionFee,
                Breed = animal.Breed,
                IsVaccinated = animal.IsVaccinated,
                HasSpecialCareNeeds = animal.HasSpecialCareNeeds,
                HasSpecialDiet = animal.HasSpecialDiet,
                IsAdopted = animal.IsAdopted,
                IsActive = animal.IsActive,
                RecentNotes = notes,

                Attributes = animal.AnimalAttributes
                .Select(aa => new AnimalAttributeVm
                {
                    Name = aa.AttributeDefinition.Name,
                    Value = aa.Value ?? ""
                })
                .ToList()
                };
        }


        public async Task<AnimalViewModel?> UpdateAsync(int id)
        {
            var animal = await _context.Animals
                .AsNoTracking()
                .Include(a => a.AnimalType)
                .Include(a => a.AnimalAttributes)
                    .ThenInclude(aa => aa.AttributeDefinition)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
                return null;

            return new AnimalViewModel
            {
                Id = animal.Id,
                ShelterId = animal.ShelterId,
                AnimalTypeId = animal.AnimalTypeId,
                Name = animal.Name,
                DateOfBirth = animal.DateOfBirth,
                Breed = animal.Breed,
                Color = animal.Color,
                AdoptionFee = animal.AdoptionFee,
                IsVaccinated = animal.IsVaccinated,
                HasSpecialCareNeeds = animal.HasSpecialCareNeeds,
                HasSpecialDiet = animal.HasSpecialDiet,
                IsActive = animal.IsActive,
                IsAdopted = animal.IsAdopted,

                Attributes = animal.AnimalAttributes
                    .Select(a => new AnimalAttributeVm
                    {
                        AttributeDefinitionId = a.AttributeDefinitionId,
                        Name = a.AttributeDefinition.Name,
                        Value = a.Value
                    })
                    .ToList()
            };
        }

        public async Task<AnimalViewModel?> GetAnimalUpdateAsync(int id)
        {
            var animal = await _context.Animals
                .AsNoTracking()
                .Include(a => a.Shelter)
                .Include(a => a.AnimalType)
                .Include(a => a.AnimalAttributes)
                    .ThenInclude(aa => aa.AttributeDefinition)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
                return null;

            return new AnimalViewModel
            {
                Id = animal.Id,
                ShelterId = animal.ShelterId,
                AnimalTypeId = animal.AnimalTypeId,
                ShelterName = animal.Shelter?.Name ?? "",
                AnimalTypeName = animal.AnimalType?.Name ?? "",
                Name = animal.Name,
                DateOfBirth = animal.DateOfBirth,
                Breed = animal.Breed,
                Color = animal.Color,
                AdoptionFee = animal.AdoptionFee,
                IsVaccinated = animal.IsVaccinated,
                HasSpecialCareNeeds = animal.HasSpecialCareNeeds,
                HasSpecialDiet = animal.HasSpecialDiet,
                IsAdopted = animal.IsAdopted,

                Attributes = animal.AnimalAttributes
                    .Select(a => new AnimalAttributeVm
                    {
                        AttributeDefinitionId = a.AttributeDefinitionId,
                        Name = a.AttributeDefinition.Name,
                        Value = a.Value
                    })
                    .ToList()
            };
        }
 




        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _context.Animals
                .AsNoTracking()
                .Where(a => a.IsActive)
                .ToListAsync();
        }

        public async Task<Animal?> GetByIdAsync(int id)
        {
            return await _context.Animals.FirstOrDefaultAsync(a  => a.Id == id);
        }

        public async Task<List<SelectListItem>> GetSelectListItemsAsync()
        {
            var animal = await GetAllAsync();

            return animal.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
        }

        // SEARCH FILTERS

        public async Task<IEnumerable<Animal>> GetAllAdoptedAsync()
        {
            return await _context.Animals
                .AsNoTracking()
                .Where(a => a.IsAdopted == true && a.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Animal>> GetAllAvailableAsync()
        {
            return await _context.Animals
                .AsNoTracking()
               .Where(a => a.IsAdopted == false && a.IsActive)
               .ToListAsync();
        }

        public async Task<IEnumerable<Animal>> GetAnimalsByShelterId(int shelterId)
        {
            return await _context.Animals
                .AsNoTracking()
                .Where(a => a.ShelterId == shelterId && a.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Animal>> GetAvailableByTypeAsync(int animalTypeId)
        {
            return await _context.Animals
                .AsNoTracking()
                .Where(a => a.AnimalTypeId == animalTypeId
                            && !a.IsAdopted
                            && a.IsActive)
                .ToListAsync();
        }


        public async Task<List<AnimalType>> GetAnimalTypesAsync()
        {
            return await _context.AnimalTypes
                .AsNoTracking()
                .OrderBy(t => t.Name)
                .ToListAsync();
        }
    

        public async Task<IEnumerable<Animal>> GetRecentlyAddedAsync(int count)
        {
            return await _context.Animals
                .AsNoTracking()
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.CreatedOn)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Animal>> SearchAsync(AnimalSearchFilter filter)
        {
            var query = _context.Animals
                .AsNoTracking()
                .Where(a => a.IsActive);

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(a => a.Name.Contains(filter.Name));

        
            if (filter.AnimalTypeId.HasValue)
            {
                query = query.Where(a => a.AnimalTypeId == filter.AnimalTypeId.Value);
            }

            if (filter.IsAvailable.HasValue)
                query = query.Where(a => a.IsAdopted == !filter.IsAvailable.Value);

            if (filter.MinAge.HasValue)
            {
                var maxBirthDate = DateTime.Today.AddYears(-filter.MinAge.Value);
                query = query.Where(a => a.DateOfBirth <= maxBirthDate);
            }

            if (filter.MaxAge.HasValue)
            {
                var minBirthDate = DateTime.Today.AddYears(-filter.MaxAge.Value);
                query = query.Where(a => a.DateOfBirth >= minBirthDate);
            }

            if (filter.HasSpecialCareNeeds.HasValue)
                query = query.Where(a => a.HasSpecialCareNeeds == filter.HasSpecialCareNeeds.Value);

            if (!string.IsNullOrEmpty(filter.Species))
                query = query.Where(a => EF.Property<string>(a, "Species") == filter.Species);

            if (!string.IsNullOrEmpty(filter.Breed))
                query = query.Where(a => EF.Property<string>(a, "Breed") == filter.Breed);

            return await query.ToListAsync();
        }
    }
}
