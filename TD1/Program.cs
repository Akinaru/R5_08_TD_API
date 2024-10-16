using Microsoft.EntityFrameworkCore;
using TD1.Models.DataManager;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;
using TD1.Models.Mapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProdDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ProdDBContext")));
/*Bien mettre la bonne chaine de connexion qui correspond à celle dans le appsettings.json*/

//Data Repository
builder.Services.AddScoped<IDataRepository<Marque>, MarqueManager>();
builder.Services.AddScoped<IDataRepository<Produit>, ProduitManager>();
builder.Services.AddScoped<IDataRepository<TypeProduit>, TypeProduitManager>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

//cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});



var app = builder.Build();

app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseHttpsRedirection();

app.UseAuthorization();*/

app.MapControllers();

app.Run();
