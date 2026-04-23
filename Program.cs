using Microsoft.EntityFrameworkCore;
using PetConnect.Application.Services;
using PetConnect.Domain.Contracts;
using PetConnect.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Database
builder.Services.AddDbContext<PetConnectDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//Adopter
builder.Services.AddScoped<IAdopterQueryService, AdopterQueryService>();
builder.Services.AddScoped<IAdopterService, AdopterService>();

//Adoption
builder.Services.AddScoped<IAdoptionQueryService, AdoptionQueryService>();
builder.Services.AddScoped<IAdoptionService, AdoptionService>();

//Animal
builder.Services.AddScoped<IAnimalQueryService, AnimalQueryService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

//AnimalType
builder.Services.AddScoped<IAnimalTypeQueryService, AnimalTypeQueryServce>();
builder.Services.AddScoped<IAnimalTypeService, AnimalTypeService>();

//Payment
builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//Shelter
builder.Services.AddScoped<IShelterQueryService, ShelterQueryService>();
builder.Services.AddScoped<IShelterService, ShelterService>();

//Warning
builder.Services.AddScoped<IWarningQueryService, WarningQueryService>();
builder.Services.AddScoped<IWarningService, WarningService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
