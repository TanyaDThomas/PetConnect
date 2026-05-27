
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;


namespace PetConnect.Application.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly PetConnectDbContext _context;
        private readonly ILogger<AnimalService> _logger;
        private readonly IWebHostEnvironment _env;
        public AnimalService(PetConnectDbContext context, ILogger<AnimalService> logger, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
        }

        public async Task<bool> CreateAsync(AnimalViewModel viewModel, string userId)
        {
            try
            {
                // Get wwwroot/images/animals path
                var uploadFolder = Path.Combine(
                    _env.WebRootPath,
                    "images",
                    "animals");

               
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string? imagePath = null;


                // PROFILE IMAGE
                if (viewModel.ImageFile != null &&
                    viewModel.ImageFile.Length > 0)
                {
                    var uniqueFileName =
                        Guid.NewGuid().ToString() +
                        Path.GetExtension(viewModel.ImageFile.FileName);

                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ImageFile.CopyToAsync(stream);
                    }

                    imagePath = "/images/animals/" + uniqueFileName;
                }

                // GALLERY
                var animalImages = new List<AnimalImage>();

                if (viewModel.AnimalImages != null &&
                    viewModel.AnimalImages.Any())
                {
                    foreach (var file in viewModel.AnimalImages)
                    {
                        if (file.Length > 0)
                        {
                            var extension = Path.GetExtension(file.FileName);

                            var uniqueFileName =
                                Guid.NewGuid().ToString() + extension;

                            var filePath = Path.Combine(
                                uploadFolder,
                                uniqueFileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            animalImages.Add(new AnimalImage
                            {
                                FileName = "/images/animals/" + uniqueFileName
                            });
                        }
                    }
                }

                if (string.IsNullOrEmpty(imagePath) && animalImages.Any())
                {
                    imagePath = animalImages.First().FileName;
                }

                var animal = new Animal
                {
                    Id = viewModel.Id,
                    ShelterId = viewModel.ShelterId,
                    AnimalTypeId = viewModel.AnimalTypeId,

                    
                    ImagePath = imagePath, 
                    Images = animalImages,

                    Name = viewModel.Name,
                    DateOfBirth = viewModel.DateOfBirth,
                    Breed = viewModel.Breed,
                    Color = viewModel.Color,
                    AdoptionFee = viewModel.AdoptionFee,
                    IsVaccinated = viewModel.IsVaccinated,
                    HasSpecialCareNeeds = viewModel.HasSpecialCareNeeds,
                    HasSpecialDiet = viewModel.HasSpecialDiet,
                };

                animal.CreatedOn = DateTime.UtcNow;
                animal.CreatedBy = userId;
                animal.IsActive = true;
                animal.IsAdopted = false;

                _context.Animals.Add(animal);

                await _context.SaveChangesAsync();

                var attributeDefinitions = await _context.AnimalTypeAttributes
                    .Where(at => at.AnimalTypeId == animal.AnimalTypeId)
                    .Select(at => at.AttributeDefinition)
                    .ToListAsync();

                var animalAttributes = attributeDefinitions.Select(at =>
                    new AnimalAttribute
                    {
                        AnimalId = animal.Id,
                        AttributeDefinitionId = at.Id,
                        Value = null,
                        IsActive = true
                    }).ToList();

                _context.AnimalAttributes.AddRange(animalAttributes);

                await _context.SaveChangesAsync();

                _logger.LogInformation(
                    "Animal created with Id {AnimalId}",
                    animal.Id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Operation Failed. Animal was not created");

                return false;
            }
        }

        


        public async Task<bool> UpdateAsync(AnimalViewModel viewModel, string userId)
        {
            try
            {
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == viewModel.Id);

                if (animal == null) return false;

               
                animal.ShelterId = viewModel.ShelterId;
                animal.AnimalTypeId = viewModel.AnimalTypeId;
                animal.Name = viewModel.Name;
                animal.DateOfBirth = viewModel.DateOfBirth;
                animal.Breed = viewModel.Breed;
                animal.Color = viewModel.Color;
                animal.AdoptionFee = viewModel.AdoptionFee;
                animal.IsVaccinated = viewModel.IsVaccinated;
                animal.HasSpecialCareNeeds = viewModel.HasSpecialCareNeeds;
                animal.HasSpecialDiet = viewModel.HasSpecialDiet;
                animal.IsAdopted = viewModel.IsAdopted;

                animal.UpdatedOn = DateTime.UtcNow;
                animal.UpdatedBy = userId;

                
                var existingAttributes = await _context.AnimalAttributes
                    .Where(a => a.AnimalId == viewModel.Id)
                    .ToListAsync();

                _context.AnimalAttributes.RemoveRange(existingAttributes);

                var newAttributes = viewModel.Attributes
                    .Where(a => !string.IsNullOrWhiteSpace(a.Value))
                    .Select(a => new AnimalAttribute
                    {
                        AnimalId = viewModel.Id,
                        AttributeDefinitionId = a.AttributeDefinitionId,
                        Value = a.Value
                    });

                await _context.AnimalAttributes.AddRangeAsync(newAttributes);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

    


        public async Task<bool> DeactivateAsync(int id, string userId)
        {
            try
            {
                var existingAnimal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
                if (existingAnimal == null) return false;

                existingAnimal.UpdatedOn = DateTime.UtcNow;
                existingAnimal.UpdatedBy = userId;

                existingAnimal.IsActive = false;
                var rowsAffected = await _context.SaveChangesAsync();

                _logger.LogInformation("Animal Deactivated with Id {AnimalId}",
                    existingAnimal.Id);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Operation Failed. Animal not deactivated");
                return false;
            }
        }



        // ADD IMAGES 
        public async Task AddImagesAsync(int animalId, List<IFormFile> files)
        {
            var animal = await _context.Animals
                .Include(a => a.Images)
                .FirstOrDefaultAsync(a => a.Id == animalId);

            if (animal == null) return;

            var uploadFolder = Path.Combine(_env.WebRootPath, "images", "animals");

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            foreach (var file in files)
            {
                if (file == null || file.Length == 0) continue;

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                animal.Images.Add(new AnimalImage
                {
                    FileName = "/images/animals/" + uniqueFileName
                });
            }

            await _context.SaveChangesAsync();
        }

        // DELETE IMAGE
        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _context.AnimalImages.FirstOrDefaultAsync(x => x.Id == imageId);
            if (image == null) return;

            _context.AnimalImages.Remove(image);
            var fullPath = Path.Combine(_env.WebRootPath, image.FileName.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            await _context.SaveChangesAsync();
        }

        // SET PROFILE PIC

        public async Task SetProfileImageAsync(int animalId, int imageId)
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == animalId);
            var image = await _context.AnimalImages.FirstOrDefaultAsync(i => i.Id == imageId);

            if (animal == null || image == null) return;

            animal.ImagePath = image.FileName;

            await _context.SaveChangesAsync();
        }
   

    }
}
