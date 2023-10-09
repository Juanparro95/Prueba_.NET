using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using General.Interfaces;
using General.MSSQL.DAL;
using Applications;
using General.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConnectionSQL") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContextTemp>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Prueba Panda Pe")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCandidateModules();
//builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddDbContext<ApplicationDbContextTemp>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSQL")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContextTemp>();
builder.Services.AddControllersWithViews();

// Registrar CRUD
builder.Services.AddScoped<ICandidateDAL, CandidateDAL>();
builder.Services.AddScoped<ICandidateExperienceDAL, CandidateExperienceDAL>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Candidate}/{id?}");
app.MapRazorPages();

app.Run();
