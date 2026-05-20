using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Contracts;
using PetConnect.Domain.Entities;
using PetConnect.Domain.Enums;
using PetConnect.Infrastructure.Identity;
using PetConnect.Infrastructure.Persistence;
using PetConnect.ViewModels;



namespace PetConnect.Application.Services
{
    public class PaymentQueryService : IPaymentQueryService
    {
        private readonly PetConnectDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public PaymentQueryService(PetConnectDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<List<PaymentListViewModel>> GetPaymentListAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new List<PaymentListViewModel>();
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            var userShelterIds = await _context.UserShelters
                .Where(us => us.UserId == userId && us.IsActive)
                .Select(us => us.ShelterId)
                .ToListAsync();

            var query = _context.Payments
                .AsNoTracking()
                .Include(p => p.Adoption)
                    .ThenInclude(a => a.Animal)
                .Where(p => p.IsActive);

            if (!isAdmin)
            {
                query = query.Where(p =>
                    (p.AdoptionId != null &&
                     p.Adoption != null &&
                     userShelterIds.Contains(p.Adoption.Animal.ShelterId))    // shelter-based payments


                );
            }

            return await query.Select(p => new PaymentListViewModel
            {
                Id = p.Id,
                Amount = p.Amount,
                PaymentMethod = p.Type.ToString(),
                Status = p.Status.ToString(),
                PaymentDate = p.PaymentDate
            }).ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .AsNoTracking()
                .Where(p => p.IsActive)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            var payment= await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
            return payment;
        }



  
        //GET DETAILS PAYMENT
        public async Task<PaymentDetailsViewModel?> GetDetailsAsync(int id)
        {
            var payment = await _context.Payments
                .AsNoTracking()
                .AsSplitQuery()
                .Include(p => p.Adoption)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

            if (payment == null)
                return null;

            var notes = await _context.Notes
                .AsNoTracking()
                .Where(n => n.EntityType == NoteEntityType.Payment
                         && n.EntityId == id)
                .OrderByDescending(n => n.CreatedOn)
                .Take(5)
                .ToListAsync();

            return new PaymentDetailsViewModel
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Status = payment.Status,
                Type = payment.Type,
                PaymentDate = payment.PaymentDate,
                AdoptionId = payment.AdoptionId,
                RecentNotes = notes

            };
        }

        //GET UPDATE PAYMENT
        public async Task<PaymentViewModel?> GetPaymentUpdateAsync(int id)
        {
            var payment = _context.Payments.FirstOrDefault(p => p.Id == id);
            if(payment == null) return null;

            return new PaymentViewModel
            {
                Amount = payment.Amount,
                Type = payment.Type,
                AdopterId = payment.AdopterId,
                AdoptionId = payment.AdopterId,
                FirstName = payment.FirstName,
                LastName = payment.LastName,
                LastFourDigits = payment.LastFourDigits,
                PaypalEmail = payment.PaypalEmail,
                BankName = payment.BankName,
                CheckNumber = payment.CheckNumber,
            };
        }


        //GET CREATE PAYMENT
        public async Task<PaymentViewModel> BuildCreateModelAsync(int? adoptionId)
        {
            var viewModel = new PaymentViewModel();

            if (adoptionId.HasValue)
            {
                var adoption = await _context.Adoptions.FirstOrDefaultAsync(a => a.Id == adoptionId.Value);

                if (adoption != null)
                {
                    viewModel.AdoptionId = adoption.Id;
                    viewModel.AdopterId = adoption.AdopterId;
                    viewModel.Amount = adoption.AdoptionFee;
                }
            }

            return viewModel;
        }
    }
}
