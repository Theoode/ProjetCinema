using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scryn.RepositoryFactory;
using ScrynDataProvider.Entities;
using ScrynDataProvider.Repositories;
using ScrynDomain.Data;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.SecurityUseCases.Create;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(options =>
{
    options.ClearProviders();
    options.AddConsole();
});
// Configuration des connexions à MySql
String connectionString = builder.Configuration.GetConnectionString("MySqlConnection") ?? throw new InvalidOperationException("Connection string 'MySqlConnection' not found.");
// Création du contexte de la base de données en utilisant la connexion MySql que l'on vient de définir
// Ce contexte est rajouté dans les services de l'application, toujours prêt à être utilisé par injection de dépendances
builder.Services.AddDbContext<ScrynDbContext>(options =>options.UseMySQL(connectionString));
// La factory est rajoutée dans les services de l'application, toujours prête à être utilisée par injection de dépendances


builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
// Sécurisation
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<ScrynUser>()
    .AddRoles<ScrynRole>()
    .AddEntityFrameworkStores<ScrynDbContext>() // Ici, on stocke les users dans la même bd que le reste
    .AddApiEndpoints();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Appel du seed ici

/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var repositoryFactory = services.GetRequiredService<IRepositoryFactory>();

    var builderDb = new BasicBdBuilder(repositoryFactory);
    await builderDb.BuildScrynBdAsync(); // ← Création rôles + utilisateurs
}*/
//A commenter si on veut pas créer la base de données a chaque fois....
/*using(var scope = app.Services.CreateScope())
{
    // On récupère le logger pour afficher des messages. On l'a mis dans les services de l'application
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ScrynDbContext>>();
    // On récupère le contexte de la base de données qui est stocké sans les services
    DbContext context = scope.ServiceProvider.GetRequiredService<ScrynDbContext>();
    logger.LogInformation("Initialisation de la base de données");
    // Suppression de la BD
    logger.LogInformation("Suppression de la BD si elle existe");
    await context.Database.EnsureDeletedAsync();
    // Recréation des tables vides
    logger.LogInformation("Création de la BD et des tables à partir des entities");
    await context.Database.EnsureCreatedAsync();
}*/

    // Création des rôles dans la table aspnetroles

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

// Ajoute les points d'entrée dans l'API pour s'authentifier, se connecter et se déconnecter
app.MapIdentityApi<ScrynUser>();

app.Run();
