using Microsoft.EntityFrameworkCore;
using PetConnect.Domain.Entities;

namespace PetConnect.Infrastructure.Persistence
{
    public class PetConnectDbContext : DbContext
    {
        public PetConnectDbContext(DbContextOptions<PetConnectDbContext> options) 
            : base(options)
        {

        }
        public DbSet<Adopter> Adopters { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalAttribute> AnimalAttributes { get; set; }

        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Warning> Warnings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Adoption>()
                .Property(a => a.AdoptionFee)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Animal>()
                .Property(a => a.AdoptionFee)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.RefundedAmount)
                .HasPrecision(10, 2);
     

            modelBuilder.Entity<AnimalAttribute>()
                .HasKey(x => new { x.AnimalId, x.AttributeDefinitionId });

            modelBuilder.Entity<Note>()
                .HasIndex(x => new { x.EntityType, x.EntityId });

            modelBuilder.Entity<Warning>()
                .HasIndex(x => new { x.EntityType, x.EntityId });


            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.Animal)
                .WithMany(a => a.Adoptions)
                .HasForeignKey(a => a.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.Adopter)
                .WithMany(a => a.Adoptions)
                .HasForeignKey(a => a.AdopterId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.Shelter)
                .WithMany(s => s.Adoptions)
                .HasForeignKey(a => a.ShelterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Adoption)
                .WithMany()
                .HasForeignKey(p => p.AdoptionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Adopter)
                .WithMany()
                .HasForeignKey(p => p.AdopterId)
                .OnDelete(DeleteBehavior.Restrict);
        }

       
    }
}
