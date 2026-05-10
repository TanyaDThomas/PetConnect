using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using PetConnect.Application.Services;
using PetConnect.Domain.Contracts;
using PetConnect.Infrastructure.Customs;
using PetConnect.Infrastructure.Identity;
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

//AnimalAttribute
builder.Services.AddScoped<IAttributeDefinitionQueryService, AttributeDefinitionQueryService>();
builder.Services.AddScoped<IAttributeDefinitionService, AttributeDefinitionService>();

//Note
builder.Services.AddScoped<INoteQueryService, NoteQueryService>();
builder.Services.AddScoped<INoteService, NoteService>();

//Payment
builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//Shelter
builder.Services.AddScoped<IShelterQueryService, ShelterQueryService>();
builder.Services.AddScoped<IShelterService, ShelterService>();

//Shelter Identity
builder.Services.AddScoped<IShelterAuthorizationService, ShelterAuthorizationService>();

//Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<PetConnectDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.LogoutPath = "/Identity/Account/Logout";
    options.Cookie.Name = "Auth";
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

builder.Services.AddRazorPages();

//Email
builder.Services.AddScoped<IEmailSender, EmailSender>();

//Admin
builder.Services.AddScoped<IUserShelterQueryService, UserShelterQueryService>();
builder.Services.AddScoped<IUserShelterService, UserShelterService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "Manager", "Staff" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
