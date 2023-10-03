using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<POSDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DbContext") ?? throw new InvalidOperationException("SQLite connection string is missing."));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<POSDbContext>();
    dbContext.Database.Migrate();

    var dataContext = new POSDbContext(services.GetRequiredService<DbContextOptions<POSDbContext>>());
    dataContext.SeedData();

}



app.Run();
