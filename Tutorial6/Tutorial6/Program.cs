using Tutorial6.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Configure the app configuration to use appsettings.json
var configuration = builder.Configuration;
builder.Services.AddSingleton(configuration);

// Configure the connection string
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Register AnimalRepository service with the connection string
builder.Services.AddScoped<AnimalRepository>(_ => new AnimalRepository(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();